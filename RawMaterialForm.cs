using System;
using System.Data;
using System.Windows.Forms;
using Microsoft.Data.SqlClient;

namespace komfort
{
    public partial class RawMaterialForm : Form
    {
        public RawMaterialForm()
        {
            InitializeComponent();
            LoadProducts();
            LoadMaterialTypes();
            LoadProductTypeCoefficientList();
        }

        private void LoadProducts()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(DataAccess.ConnectionString))
                {
                    con.Open();

                    SqlDataAdapter da = new SqlDataAdapter(
                        "SELECT Артикул, Наименование_продукции, Тип_продукции FROM Продукты",
                        con);

                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    comboProduct.DataSource = dt;
                    comboProduct.DisplayMember = "Наименование_продукции";
                    comboProduct.ValueMember = "Артикул";

                    comboProduct.SelectedIndexChanged += ComboProduct_SelectedIndexChanged;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка загрузки товаров: " + ex.Message);
            }
        }

        private void ComboProduct_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboProduct.SelectedItem is DataRowView selectedRow)
            {
                string productType = selectedRow["Тип_продукции"].ToString();
                SetCoefficientByProductType(productType);
            }
        }

        private void SetCoefficientByProductType(string productType)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(DataAccess.ConnectionString))
                {
                    con.Open();

                    SqlCommand cmd = new SqlCommand(
                        "SELECT Коэффициент_типа_продукции FROM Тип_продукта WHERE Тип_продукции = @ProductType",
                        con);
                    cmd.Parameters.AddWithValue("@ProductType", productType);

                    object result = cmd.ExecuteScalar();

                    if (result != null)
                    {
                        decimal coefficient = Convert.ToDecimal(result);

                        foreach (DataRowView item in comboParam2.Items)
                        {
                            if (Convert.ToDecimal(item["Коэффициент_типа_продукции"]) == coefficient)
                            {
                                comboParam2.SelectedItem = item;
                                break;
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show($"Не найден коэффициент для типа продукции: {productType}",
                            "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка загрузки коэффициента: " + ex.Message);
            }
        }

        private void LoadMaterialTypes()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(DataAccess.ConnectionString))
                {
                    con.Open();

                    SqlDataAdapter da = new SqlDataAdapter(
                        "SELECT Тип_материала, Процент_потерь_сырья FROM Тип_материала",
                        con);

                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    comboMaterial.DataSource = dt;
                    comboMaterial.DisplayMember = "Тип_материала";
                    comboMaterial.ValueMember = "Тип_материала";

                    comboMaterial.SelectedIndexChanged += ComboMaterial_SelectedIndexChanged;

                    if (comboMaterial.SelectedValue != null)
                    {
                        UpdateLossPercentage();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка загрузки типов сырья: " + ex.Message);
            }
        }

        private void ComboMaterial_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateLossPercentage();
        }

        private void UpdateLossPercentage()
        {
            if (comboMaterial.SelectedValue != null && comboMaterial.SelectedItem is DataRowView rowView)
            {
                decimal lossPercent = Convert.ToDecimal(rowView["Процент_потерь_сырья"]);
                labelLoss.Text = $"{lossPercent:P2}";
            }
        }

        private void LoadProductTypeCoefficientList()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(DataAccess.ConnectionString))
                {
                    con.Open();

                    SqlDataAdapter da = new SqlDataAdapter(
                        "SELECT Коэффициент_типа_продукции FROM Тип_продукта",
                        con);

                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    comboParam2.DataSource = dt;
                    comboParam2.DisplayMember = "Коэффициент_типа_продукции";
                    comboParam2.ValueMember = "Коэффициент_типа_продукции";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка загрузки коэффициентов: " + ex.Message);
            }
        }

        private void btnCalc_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(txtQuantity.Text, out int quantity))
            {
                MessageBox.Show("Введите корректное количество!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (comboParam2.SelectedValue == null)
            {
                MessageBox.Show("Выберите коэффициент типа продукции!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (comboMaterial.SelectedValue == null)
            {
                MessageBox.Show("Выберите тип материала!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            decimal coefficient = Convert.ToDecimal(comboParam2.SelectedValue);

            decimal lossPercent = 0;
            if (comboMaterial.SelectedItem is DataRowView selectedRow)
            {
                lossPercent = Convert.ToDecimal(selectedRow["Процент_потерь_сырья"]) / 100m; 
            }

            decimal rawMaterialNeeded = quantity * coefficient;
            decimal totalRawMaterial = rawMaterialNeeded / (1 - lossPercent);

            lblResult.Text = $"Необходимое сырьё: {totalRawMaterial:F2}";
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}