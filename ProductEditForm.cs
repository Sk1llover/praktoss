using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Microsoft.Data.SqlClient;

namespace komfort
{
    public partial class ProductEditForm : Form
    {
        private Product currentProduct;
        private bool isEditMode;
        private bool isFormDirty = false;
        private static bool isEditFormOpen = false;
        private string currentUserLogin;

        public event EventHandler<Product> ProductSaved;

        public ProductEditForm(Product product = null, string userLogin = "")
        {
            if (isEditFormOpen)
            {
                MessageBox.Show("Форма редактирования товара уже открыта.", "Внимание",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.DialogResult = DialogResult.Cancel;
                return;
            }

            isEditFormOpen = true;

            InitializeComponent();
            this.FormClosed += ProductEditForm_FormClosed;

            currentProduct = product != null ? product.Clone() : new Product();
            currentUserLogin = userLogin;
            isEditMode = product != null;

            InitializeForm();
            LoadComboBoxes();
            LoadProductData();
            WireUpEvents();
        }

        private void InitializeForm()
        {
            this.Text = isEditMode ? "Редактирование товара" : "Добавление товара";

            if (isEditMode)
            {
                txtArticle.ReadOnly = true;
                txtArticle.BackColor = SystemColors.Control;
            }

            numCost.Minimum = 0.01m;
            numTime.Minimum = 0.1m;
            numQuantity.Minimum = 0;
            numWorkers.Minimum = 1;
            numWorkshopTime.Minimum = 0.1m;

            if (!isEditMode)
            {
                numCost.Value = numCost.Minimum;
                numTime.Value = numTime.Minimum;
                numQuantity.Value = numQuantity.Minimum;
                numWorkers.Value = numWorkers.Minimum;
                numWorkshopTime.Value = numWorkshopTime.Minimum;
            }
        }

        private void LoadComboBoxes()
        {
            try
            {
                comboType.DataSource = DataAccess.ExecuteQuery(
                    "SELECT Тип_продукции FROM Тип_продукта ORDER BY Тип_продукции");
                comboType.DisplayMember = "Тип_продукции";

                comboMaterial.DataSource = DataAccess.ExecuteQuery(
                    "SELECT Тип_материала FROM Тип_материала ORDER BY Тип_материала");
                comboMaterial.DisplayMember = "Тип_материала";

                comboSupplier.DataSource = DataAccess.ExecuteQuery(
                    "SELECT DISTINCT Поставщик FROM Продукты WHERE Поставщик IS NOT NULL ORDER BY Поставщик");
                comboSupplier.DisplayMember = "Поставщик";

                comboWorkshop.DataSource = DataAccess.ExecuteQuery(
                    "SELECT DISTINCT Название_цеха FROM Цеха_продукты ORDER BY Название_цеха");
                comboWorkshop.DisplayMember = "Название_цеха";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки справочников:\n{ex.Message}",
                    "Ошибка базы данных", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadProductData()
        {
            if (!isEditMode || currentProduct == null)
            {
                txtCreator.Text = currentUserLogin;
                numCost.Value = numCost.Minimum;
                numTime.Value = numTime.Minimum;
                numQuantity.Value = numQuantity.Minimum;
                numWorkers.Value = numWorkers.Minimum;
                numWorkshopTime.Value = numWorkshopTime.Minimum;
                return;
            }

            txtArticle.Text = currentProduct.Артикул;
            txtName.Text = currentProduct.Наименование;
            comboType.Text = currentProduct.Тип_продукции;
            comboMaterial.Text = currentProduct.Основной_материал;

            numCost.Value = Math.Max(numCost.Minimum, Math.Min(numCost.Maximum, currentProduct.Минимальная_стоимость_для_партнера));
            numTime.Value = Math.Max(numTime.Minimum, Math.Min(numTime.Maximum, currentProduct.Время_изготовления_ч));
            numQuantity.Value = Math.Max(numQuantity.Minimum, Math.Min(numQuantity.Maximum, currentProduct.Количество));
            numWorkers.Value = Math.Max(numWorkers.Minimum, Math.Min(numWorkers.Maximum, currentProduct.КоличествоРабочих));
            numWorkshopTime.Value = Math.Max(numWorkshopTime.Minimum, Math.Min(numWorkshopTime.Maximum, currentProduct.ВремяВЦехе_ч));

            comboSupplier.Text = currentProduct.Поставщик;
            comboWorkshop.Text = currentProduct.НазваниеЦеха;
            txtCreator.Text = currentProduct.Создатель;
        }

        private void WireUpEvents()
        {
            txtArticle.TextChanged += MarkFormAsDirty;
            txtName.TextChanged += MarkFormAsDirty;
            comboType.SelectedIndexChanged += MarkFormAsDirty;
            comboMaterial.SelectedIndexChanged += MarkFormAsDirty;
            comboSupplier.SelectedIndexChanged += MarkFormAsDirty;
            comboWorkshop.SelectedIndexChanged += MarkFormAsDirty;
            numCost.ValueChanged += MarkFormAsDirty;
            numTime.ValueChanged += MarkFormAsDirty;
            numQuantity.ValueChanged += MarkFormAsDirty;
            numWorkers.ValueChanged += MarkFormAsDirty;
            numWorkshopTime.ValueChanged += MarkFormAsDirty;
            txtCreator.TextChanged += MarkFormAsDirty;

            btnSave.Click += btnSave_Click;
            btnCancel.Click += btnCancel_Click;
            btnWorkshops.Click += btnWorkshops_Click;
        }

        private void MarkFormAsDirty(object sender, EventArgs e)
            => isFormDirty = true;

        private bool ValidateForm()
        {
            if (string.IsNullOrWhiteSpace(txtArticle.Text))
            {
                MessageBox.Show("Артикул обязателен.", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtArticle.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                MessageBox.Show("Наименование товара обязательно.", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtName.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(comboWorkshop.Text))
            {
                MessageBox.Show("Выберите цех.", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                comboWorkshop.Focus();
                return false;
            }

            return ValidateArticleUniqueness();
        }

        private bool ValidateArticleUniqueness()
        {
            try
            {
                string sql = isEditMode
                    ? "SELECT COUNT(*) FROM Продукты WHERE Артикул = @Article AND Артикул != @Current"
                    : "SELECT COUNT(*) FROM Продукты WHERE Артикул = @Article";

                var result = DataAccess.ExecuteScalar<int>(sql,
                    new SqlParameter("@Article", txtArticle.Text.Trim()),
                    new SqlParameter("@Current", currentProduct?.Артикул ?? ""));

                if (result > 0)
                {
                    MessageBox.Show("Такой артикул уже существует.", "Ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtArticle.Focus();
                    return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка проверки артикула: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!ValidateForm()) return;

            try
            {
                var product = new Product
                {
                    Артикул = txtArticle.Text.Trim(),
                    Наименование = txtName.Text.Trim(),
                    Тип_продукции = comboType.Text,
                    Основной_материал = comboMaterial.Text,
                    Минимальная_стоимость_для_партнера = numCost.Value,
                    Время_изготовления_ч = numTime.Value,
                    Поставщик = comboSupplier.Text,
                    НазваниеЦеха = comboWorkshop.Text,

                    Количество = (int)numQuantity.Value,
                    КоличествоРабочих = (int)numWorkers.Value,
                    ВремяВЦехе_ч = numWorkshopTime.Value,

                    Создатель = txtCreator.Text.Trim()
                };

                bool success;
                if (isEditMode)
                    success = UpdateProduct(product);
                else
                    success = InsertProduct(product);

                if (success)
                {
                    ProductSaved?.Invoke(this, product);
                    isFormDirty = false;
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при сохранении товара: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool InsertProduct(Product p)
        {
            using (var connection = new SqlConnection(DataAccess.ConnectionString))
            {
                connection.Open();
                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        // Вставка в Продукты
                        using (var command = new SqlCommand(
        @"INSERT INTO Продукты (Артикул, Наименование_продукции, Тип_продукции, Основной_материал,
                       Минимальная_стоимость_для_партнера, Создатель, Поставщик)
      VALUES (@a, @n, @t, @m, @c, @cr, @s)", connection, transaction))
                        {
                            command.Parameters.AddRange(new[]
                            {
                        new SqlParameter("@a", p.Артикул),
                        new SqlParameter("@n", p.Наименование),
                        new SqlParameter("@t", p.Тип_продукции),
                        new SqlParameter("@m", p.Основной_материал),
                        new SqlParameter("@c", p.Минимальная_стоимость_для_партнера),
                        new SqlParameter("@cr", p.Создатель),
                        new SqlParameter("@s", string.IsNullOrWhiteSpace(p.Поставщик) ? (object)DBNull.Value : p.Поставщик)
                    });
                            command.ExecuteNonQuery();
                        }

                        using (var command = new SqlCommand(
        @"INSERT INTO Цеха_продукты (Наименование_продукции, Название_цеха, Время_изготовления_ч, Количество)
      VALUES (@pname, @ws, @time, @q)", connection, transaction))
                        {
                            command.Parameters.AddRange(new[]
                            {
                        new SqlParameter("@pname", p.Наименование),
                        new SqlParameter("@ws", p.НазваниеЦеха),
                        new SqlParameter("@time", p.ВремяВЦехе_ч),
                        new SqlParameter("@q", p.Количество)
                    });
                            command.ExecuteNonQuery();
                        }

                        using (var checkCommand = new SqlCommand(
                            "SELECT COUNT(*) FROM Цеха WHERE Название_цеха = @ws", connection, transaction))
                        {
                            checkCommand.Parameters.Add(new SqlParameter("@ws", p.НазваниеЦеха));
                            var exists = (int)checkCommand.ExecuteScalar();

                            using (var updateCommand = new SqlCommand())
                            {
                                updateCommand.Connection = connection;
                                updateCommand.Transaction = transaction;

                                if (exists > 0)
                                {
                                    updateCommand.CommandText = "UPDATE Цеха SET Количество_человек_для_производства = @workers WHERE Название_цеха = @ws";
                                }
                                else
                                {
                                    updateCommand.CommandText = "INSERT INTO Цеха (Название_цеха, Количество_человек_для_производства) VALUES (@ws, @workers)";
                                }

                                updateCommand.Parameters.Add(new SqlParameter("@workers", p.КоличествоРабочих));
                                updateCommand.Parameters.Add(new SqlParameter("@ws", p.НазваниеЦеха));
                                updateCommand.ExecuteNonQuery();
                            }
                        }

                        transaction.Commit();
                        MessageBox.Show("Товар успешно добавлен!", "Успех",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return true;
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        MessageBox.Show($"Ошибка при добавлении товара: {ex.Message}", "Ошибка",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                }
            }
        }

        private bool UpdateProduct(Product p)
        {
            using (var connection = new SqlConnection(DataAccess.ConnectionString))
            {
                connection.Open();
                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        string oldProductName = currentProduct.Наименование;

                        using (var command = new SqlCommand(
            @"UPDATE Цеха_продукты
      SET Наименование_продукции = @newName, Время_изготовления_ч = @time, 
          Количество = @q, Название_цеха = @ws
      WHERE Наименование_продукции = @oldName", connection, transaction))
                        {
                            command.Parameters.AddRange(new[]
                            {
                        new SqlParameter("@newName", p.Наименование),
                        new SqlParameter("@time", p.ВремяВЦехе_ч),
                        new SqlParameter("@q", p.Количество),
                        new SqlParameter("@ws", p.НазваниеЦеха),
                        new SqlParameter("@oldName", oldProductName)
                    });
                            command.ExecuteNonQuery();
                        }

                        try
                        {
                            using (var command = new SqlCommand(
                @"UPDATE dbo.Lieva_продукты 
          SET Наименование_продукции = @newName 
          WHERE Наименование_продукции = @oldName", connection, transaction))
                            {
                                command.Parameters.AddRange(new[]
                                {
                            new SqlParameter("@newName", p.Наименование),
                            new SqlParameter("@oldName", oldProductName)
                        });
                                command.ExecuteNonQuery();
                            }
                        }
                        catch (Exception ex)
                        {
                            System.Diagnostics.Debug.WriteLine($"Не удалось обновить Lieva_продукты: {ex.Message}");
                        }

                        using (var command = new SqlCommand(
            @"UPDATE Продукты SET
      Наименование_продукции = @n, Тип_продукции = @t, Основной_материал = @m,
      Минимальная_стоимость_для_партнера = @c, Создатель = @cr, Поставщик = @s
  WHERE Артикул = @a", connection, transaction))
                        {
                            command.Parameters.AddRange(new[]
                            {
                        new SqlParameter("@n", p.Наименование),
                        new SqlParameter("@t", p.Тип_продукции),
                        new SqlParameter("@m", p.Основной_материал),
                        new SqlParameter("@c", p.Минимальная_стоимость_для_партнера),
                        new SqlParameter("@cr", p.Создатель),
                        new SqlParameter("@s", string.IsNullOrWhiteSpace(p.Поставщик) ? (object)DBNull.Value : p.Поставщик),
                        new SqlParameter("@a", p.Артикул)
                    });
                            command.ExecuteNonQuery();
                        }

                        using (var checkCommand = new SqlCommand(
                            "SELECT COUNT(*) FROM Цеха WHERE Название_цеха = @ws", connection, transaction))
                        {
                            checkCommand.Parameters.Add(new SqlParameter("@ws", p.НазваниеЦеха));
                            var exists = (int)checkCommand.ExecuteScalar();

                            using (var updateCommand = new SqlCommand())
                            {
                                updateCommand.Connection = connection;
                                updateCommand.Transaction = transaction;

                                if (exists > 0)
                                {
                                    updateCommand.CommandText = "UPDATE Цеха SET Количество_человек_для_производства = @workers WHERE Название_цеха = @ws";
                                }
                                else
                                {
                                    updateCommand.CommandText = "INSERT INTO Цеха (Название_цеха, Количество_человек_для_производства) VALUES (@ws, @workers)";
                                }

                                updateCommand.Parameters.Add(new SqlParameter("@workers", p.КоличествоРабочих));
                                updateCommand.Parameters.Add(new SqlParameter("@ws", p.НазваниеЦеха));
                                updateCommand.ExecuteNonQuery();
                            }
                        }

                        transaction.Commit();
                        MessageBox.Show("Товар успешно обновлен!", "Успех",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return true;
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        MessageBox.Show($"Ошибка при обновлении товара: {ex.Message}", "Ошибка",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (isFormDirty && MessageBox.Show("Есть несохраненные изменения. Закрыть форму без сохранения?",
                    "Подтверждение", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                return;

            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void ProductEditForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            isEditFormOpen = false;
        }

        private void btnWorkshops_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                MessageBox.Show("Введите наименование товара перед просмотром цехов.",
                    "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            new WorkshopsForm(txtName.Text).ShowDialog();
        }

        public static bool DeleteProduct(string article, string productName)
        {
            using (var connection = new SqlConnection(DataAccess.ConnectionString))
            {
                connection.Open();
                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        try
                        {
                            using (var command = new SqlCommand(
                @"DELETE FROM dbo.Lieva_продукты 
          WHERE Наименование_продукции = @name", connection, transaction))
                            {
                                command.Parameters.Add(new SqlParameter("@name", productName));
                                command.ExecuteNonQuery();
                            }
                        }
                        catch (Exception ex)
                        {
                            System.Diagnostics.Debug.WriteLine($"Не удалось удалить из Lieva_продукты: {ex.Message}");
                        }

                        using (var command = new SqlCommand(
            @"DELETE FROM Цеха_продукты 
      WHERE Наименование_продукции = @name", connection, transaction))
                        {
                            command.Parameters.Add(new SqlParameter("@name", productName));
                            command.ExecuteNonQuery();
                        }

                        using (var command = new SqlCommand(
            @"DELETE FROM Продукты 
      WHERE Артикул = @article", connection, transaction))
                        {
                            command.Parameters.Add(new SqlParameter("@article", article));
                            int rowsAffected = command.ExecuteNonQuery();

                            if (rowsAffected == 0)
                            {
                                transaction.Rollback();
                                return false;
                            }
                        }

                        transaction.Commit();
                        return true;
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        throw new Exception($"Ошибка при удалении товара: {ex.Message}");
                    }
                }
            }
        }

        private decimal SafeConvertToDecimal(object value)
        {
            if (value == null || value == DBNull.Value)
                return 0m;

            if (value is decimal)
                return (decimal)value;

            if (decimal.TryParse(value.ToString(), out decimal result))
                return result;

            return 0m;
        }
    }
}