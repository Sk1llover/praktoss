using System;
using System.Data;
using System.Windows.Forms;
using Microsoft.Data.SqlClient;

namespace komfort
{
    public partial class WorkshopsForm : Form
    {
        private readonly string _productName;

        public WorkshopsForm(string productName)
        {
            InitializeComponent();
            _productName = productName;
            Text = $"Цеха для продукта: {productName}";
        }

        private void WorkshopsForm_Load(object sender, EventArgs e)
        {
            try
            {
                DataTable dt;

                var joinSql =
                @"SELECT 
    cp.Название_цеха,
    cp.Время_изготовления_ч,
    cp.Количество AS Количество_товара,
    c.Количество_человек_для_производства
FROM Цеха_продукты cp
LEFT JOIN Цеха c ON cp.Название_цеха = c.Название_цеха
WHERE cp.Наименование_продукции = @pname;";


                try
                {
                    dt = DataAccess.ExecuteQuery(joinSql, new SqlParameter("@pname", _productName));
                }
                catch (SqlException ex)
                {
                    if (ex.Message.IndexOf("Invalid column name", StringComparison.OrdinalIgnoreCase) >= 0 ||
                        ex.Message.IndexOf("недопустимое имя столбца", StringComparison.OrdinalIgnoreCase) >= 0)
                    {
                        dt = DataAccess.ExecuteQuery(
                        @"SELECT 
    Название_цеха,
    Время_изготовления_ч,
    Количество AS Количество_товара
FROM Цеха_продукты
WHERE Наименование_продукции = @pname;",
                        new SqlParameter("@pname", _productName));

                    }
                    else
                    {
                        throw;
                    }
                }

                if (dt == null)
                {
                    dt = DataAccess.ExecuteQuery($@"
SELECT Название_цеха, Время_изготовления_ч, Количество AS Количество_товара
FROM Цеха_продукты
WHERE Наименование_продукции = '{_productName.Replace("'", "''")}'");
                }

                dataGridView1.DataSource = dt;
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dataGridView1.ReadOnly = true;
                dataGridView1.AllowUserToAddRows = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки списка цехов: {ex.Message}",
                    "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
