using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Microsoft.Data.SqlClient;

namespace komfort
{
    public partial class MainForm : Form
    {
        private string currentLogin;
        private string currentFIO;
        private string currentRole;
        private List<Product> allProducts = new List<Product>();
        private System.Threading.Timer? debounceTimer;

        private bool IsAdmin => currentRole?.ToLower().Contains("администратор") == true;
        private bool IsManager => currentRole?.ToLower().Contains("менеджер") == true;


        public MainForm(string login, string fio, string role)
        {
            currentLogin = login;
            currentFIO = fio;
            currentRole = role;

            InitializeComponent();
            InitializeAdminFeatures();

            this.Load += MainForm_Load;
            if (txtSearch != null) txtSearch.TextChanged += TxtSearch_TextChanged;
            if (comboSupplier != null) comboSupplier.SelectedIndexChanged += ComboSupplier_SelectedIndexChanged;
            if (rbAsc != null) rbAsc.CheckedChanged += RbAsc_CheckedChanged;
            if (rbDesc != null) rbDesc.CheckedChanged += RbDesc_CheckedChanged;
            if (btnRefresh != null) btnRefresh.Click += BtnRefresh_Click;
            if (btnHistory != null) btnHistory.Click += BtnHistory_Click;
            if (btnLogout != null) btnLogout.Click += BtnLogout_Click;
        }

        private void InitializeAdminFeatures()
        {
            if (btnAddProduct != null)
            {
                btnAddProduct.Visible = IsAdmin;
                btnAddProduct.Enabled = IsAdmin;
                btnAddProduct.Click += BtnAddProduct_Click;
            }

            if (btnHistory != null)
            {
                btnHistory.Visible = IsAdmin || IsManager;
                btnHistory.Enabled = IsAdmin || IsManager;
                btnHistory.Click += BtnHistory_Click;
            }

            if (btnRawMaterial != null)
            {
                btnRawMaterial.Visible = IsAdmin || IsManager;
                btnRawMaterial.Enabled = IsAdmin || IsManager;
                btnRawMaterial.Click += BtnRawMaterial_Click;
            }


            if (btnHistory != null)
                btnHistory.Click += BtnHistory_Click;

            if (editToolStripMenuItem != null) editToolStripMenuItem.Click += EditProductMenuItem_Click;
            if (deleteToolStripMenuItem != null) deleteToolStripMenuItem.Click += DeleteProductMenuItem_Click;

            if (flowLayoutPanelProducts != null && contextMenuProducts != null)
                flowLayoutPanelProducts.ContextMenuStrip = contextMenuProducts;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            SetUserRoleIcon();

            InitializeSupplierComboBox();

            LoadProducts();
            ApplyFilterAndRender();
        }

        private void InitializeSupplierComboBox()
        {
            if (comboSupplier == null) return;

            comboSupplier.Items.Clear();
            comboSupplier.Items.Add("Все поставщики");

            try
            {
                var dt = DataAccess.ExecuteQuery("SELECT DISTINCT Поставщик FROM Продукты WHERE Поставщик IS NOT NULL");
                foreach (DataRow r in dt.Rows)
                {
                    var s = r[0]?.ToString();
                    if (!string.IsNullOrWhiteSpace(s))
                        comboSupplier.Items.Add(s);
                }
            }
            catch
            {

            }

            comboSupplier.SelectedIndex = 0;
        }

        private void LoadProducts()
        {
            allProducts.Clear();
            try
            {
                var dt = DataAccess.ExecuteQuery(@"
            SELECT p.*, cp.Время_изготовления_ч, cp.Количество, cp.Название_цеха
            FROM Продукты p
            LEFT JOIN Цеха_продукты cp ON p.Наименование_продукции = cp.Наименование_продукции;
        ");

                foreach (DataRow r in dt.Rows)
                {
                    var p = new Product
                    {
                        Артикул = r["Артикул"]?.ToString() ?? "",
                        Наименование = r["Наименование_продукции"]?.ToString() ?? "",
                        Тип_продукции = r["Тип_продукции"]?.ToString() ?? "",
                        Основной_материал = r["Основной_материал"]?.ToString() ?? "",
                        Минимальная_стоимость_для_партнера = SafeConvertToDecimal(r["Минимальная_стоимость_для_партнера"]),
                        Время_изготовления_ч = SafeConvertToDecimal(r["Время_изготовления_ч"]),
                        Поставщик = r["Поставщик"]?.ToString() ?? "",
                        Количество = SafeConvertToInt(r["Количество"]),
                        НазваниеЦеха = r["Название_цеха"]?.ToString() ?? "",
                        Создатель = r["Создатель"]?.ToString() ?? ""
                    };

                    allProducts.Add(p);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при загрузке товаров: " + ex.Message, "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private decimal SafeConvertToDecimal(object value)
        {
            if (value == null || value == DBNull.Value)
                return 0m;

            try
            {
                if (value is decimal)
                    return (decimal)value;

                string stringValue = value.ToString().Replace(',', '.');
                if (decimal.TryParse(stringValue, System.Globalization.NumberStyles.Any,
                    System.Globalization.CultureInfo.InvariantCulture, out decimal result))
                    return result;

                return 0m;
            }
            catch
            {
                return 0m;
            }
        }

        private int SafeConvertToInt(object value)
        {
            if (value == null || value == DBNull.Value)
                return 0;

            try
            {
                if (value is int)
                    return (int)value;

                if (int.TryParse(value.ToString(), out int result))
                    return result;

                if (decimal.TryParse(value.ToString().Replace(',', '.'), out decimal decimalResult))
                    return (int)decimalResult;

                return 0;
            }
            catch
            {
                return 0;
            }
        }

        private void BtnAddProduct_Click(object? sender, EventArgs e)
        {
            if (!IsAdmin)
            {
                MessageBox.Show("Доступ запрещен. Только администратор может добавлять товары.",
                    "Ошибка доступа", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                var editForm = new ProductEditForm(null, currentLogin);
                editForm.ProductSaved += (s, product) =>
                {
                    LoadProducts();
                    ApplyFilterAndRender();
                    ShowNotification("Товар успешно добавлен", "Информация", MessageBoxIcon.Information);
                };

                editForm.ShowDialog();
            }
            catch (InvalidOperationException ex)
            {
                MessageBox.Show(ex.Message, "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при открытии формы добавления товара: {ex.Message}",
                    "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void EditProductMenuItem_Click(object? sender, EventArgs e)
        {
            if (!IsAdmin)
            {
                MessageBox.Show("Доступ запрещен. Только администратор может редактировать товары.",
                    "Ошибка доступа", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var selectedProduct = GetSelectedProduct();
            if (selectedProduct != null)
            {
                EditProduct(selectedProduct);
            }
        }

        private void DeleteProductMenuItem_Click(object? sender, EventArgs e)
        {
            if (!IsAdmin)
            {
                MessageBox.Show("Доступ запрещен. Только администратор может удалять товары.",
                    "Ошибка доступа", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var selectedProduct = GetSelectedProduct();
            if (selectedProduct != null)
            {
                DeleteProduct(selectedProduct);
            }
        }

        private Product? GetSelectedProduct()
        {
            if (contextMenuProducts?.Tag is Product product)
                return product;

            return allProducts.FirstOrDefault();
        }

        private void EditProduct(Product product)
        {
            try
            {
                var editForm = new ProductEditForm(product, currentLogin);
                editForm.ProductSaved += (s, updatedProduct) =>
                {
                    LoadProducts();
                    ApplyFilterAndRender();
                    ShowNotification("Товар успешно обновлен", "Информация", MessageBoxIcon.Information);
                };

                editForm.ShowDialog();
            }
            catch (InvalidOperationException ex)
            {
                MessageBox.Show(ex.Message, "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при открытии формы редактирования: {ex.Message}",
                    "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DeleteProduct(Product product)
        {
            if (IsProductInOrder(product.Артикул))
            {
                MessageBox.Show($"Невозможно удалить товар '{product.Наименование}', так как он присутствует в заказе.",
                    "Ошибка удаления", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var result = MessageBox.Show($"Вы уверены, что хотите удалить товар '{product.Наименование}'? Это действие нельзя отменить.",
                "Подтверждение удаления", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                try
                {
                    bool success = ProductEditForm.DeleteProduct(product.Артикул, product.Наименование);

                    if (success)
                    {
                        LoadProducts();
                        ApplyFilterAndRender();
                        ShowNotification("Товар успешно удален", "Информация", MessageBoxIcon.Information);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при удалении товара: {ex.Message}",
                        "Ошибка удаления", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private bool IsProductInOrder(string article)
        {
            try
            {
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception($"Ошибка при проверке товара в заказах: {ex.Message}", ex);
            }
        }

        private void ShowNotification(string message, string title, MessageBoxIcon icon)
        {
            MessageBox.Show(message, title, MessageBoxButtons.OK, icon);
        }

        private List<Product> GetFilteredProducts()
        {
            var search = txtSearch?.Text?.Trim().ToLower() ?? "";
            var supplier = comboSupplier?.SelectedItem?.ToString() ?? "Все поставщики";
            var sortAsc = rbAsc?.Checked ?? true;

            var filtered = allProducts.Where(p => FilterProduct(p, search, supplier));

            if (rbAsc != null)
            {
                filtered = sortAsc
                    ? filtered.OrderBy(p => p.Количество)
                    : filtered.OrderByDescending(p => p.Количество);
            }

            return filtered.ToList();
        }

        private bool FilterProduct(Product p, string search, string supplier)
        {
            if (supplier != "Все поставщики" && !string.Equals(p.Поставщик, supplier, StringComparison.OrdinalIgnoreCase))
                return false;

            if (string.IsNullOrEmpty(search))
                return true;

            if ((p.Артикул?.ToLower().Contains(search) == true) ||
                (p.Наименование?.ToLower().Contains(search) == true) ||
                (p.Тип_продукции?.ToLower().Contains(search) == true) ||
                (p.Основной_материал?.ToLower().Contains(search) == true) ||
                (p.Поставщик?.ToLower().Contains(search) == true) ||
                (p.НазваниеЦеха?.ToLower().Contains(search) == true))
                return true;

            foreach (var kv in p.ExtraFields)
            {
                if (kv.Value?.ToLower().Contains(search) == true)
                    return true;
            }

            return false;
        }

        private void ApplyFilterAndRenderDebounced()
        {
            debounceTimer?.Dispose();
            debounceTimer = new System.Threading.Timer(_ =>
            {
                this.Invoke((Action)(() => ApplyFilterAndRender()));
            }, null, 250, System.Threading.Timeout.Infinite);
        }

        private void ApplyFilterAndRender()
        {
            if (flowLayoutPanelProducts == null)
            {
                MessageBox.Show("Панель продуктов не инициализирована.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var products = GetFilteredProducts();
            RenderProducts(products);
        }

        private void RenderProducts(List<Product> products)
        {
            if (flowLayoutPanelProducts == null) return;

            flowLayoutPanelProducts.SuspendLayout();
            flowLayoutPanelProducts.Controls.Clear();

            foreach (var p in products)
            {
                var card = new ProductCard
                {
                    Width = Math.Max(700, flowLayoutPanelProducts.ClientSize.Width - 40)
                };
                card.SetData(p);
                card.Clicked += (s, e) =>
                {
                    if (IsAdmin)
                    {
                        if (contextMenuProducts != null)
                        {
                            contextMenuProducts.Tag = p;
                            contextMenuProducts.Show(card, new Point(card.Width / 2, card.Height / 2));
                        }
                    }
                };
                flowLayoutPanelProducts.Controls.Add(card);
            }

            flowLayoutPanelProducts.ResumeLayout();
        }

        private void BtnRefresh_Click(object? sender, EventArgs e)
        {
            LoadProducts();
            ApplyFilterAndRender();
        }

        private void BtnHistory_Click(object? sender, EventArgs e)
        {
            var hf = new HistoryForm();
            hf.ShowDialog();
        }

        private void TxtSearch_TextChanged(object? sender, EventArgs e)
        {
            ApplyFilterAndRenderDebounced();
        }

        private void ComboSupplier_SelectedIndexChanged(object? sender, EventArgs e)
        {
            ApplyFilterAndRender();
        }

        private void RbAsc_CheckedChanged(object? sender, EventArgs e)
        {
            if (rbAsc?.Checked == true) ApplyFilterAndRender();
        }

        private void RbDesc_CheckedChanged(object? sender, EventArgs e)
        {
            if (rbDesc?.Checked == true) ApplyFilterAndRender();
        }

        private void BtnLogout_Click(object? sender, EventArgs e)
        {
            this.Close();
        }

        private void SetUserRoleIcon()
        {
            string currentRoleLower = currentRole?.ToLower() ?? "";

            string imageName = currentRoleLower switch
            {
                string r when r.Contains("администратор") => "admin_icon.png",
                string r when r.Contains("менеджер") => "manager_icon.png",
                string r when r.Contains("клиент") || r.Contains("авториз") => "client_icon.png",
                _ => "client_icon.png"
            };

            try
            {
                string imagePath = Path.Combine(Application.StartupPath, "Resources", imageName);

                if (File.Exists(imagePath))
                {
                    var originalImage = Image.FromFile(imagePath);

                    var resizedImage = new Bitmap(originalImage, new Size(40, 40));

                    btnUser.Image = resizedImage;
                    btnUser.Text = $"{currentFIO} ({currentRole})";

                    btnUser.Size = new Size(300, 50);
                    btnUser.ImageAlign = ContentAlignment.MiddleLeft;
                    btnUser.TextAlign = ContentAlignment.MiddleRight;
                }
                else
                {
                    btnUser.Text = $"{currentFIO} ({currentRole})";
                    UseSystemIcons();
                }
            }
            catch (Exception)
            {
                btnUser.Text = $"{currentFIO} ({currentRole})";
                UseSystemIcons();
            }
        }

        private void UseSystemIcons()
        {
            string currentRoleLower = currentRole?.ToLower() ?? "";

            Icon roleIcon = currentRoleLower switch
            {
                string r when r.Contains("администратор") => SystemIcons.Shield,
                string r when r.Contains("менеджер") => SystemIcons.Warning, 
                string r when r.Contains("клиент") || r.Contains("авториз") => SystemIcons.Application,
                _ => SystemIcons.Application
            };

            var bitmap = roleIcon.ToBitmap();
            btnUser.Image = new Bitmap(bitmap, new Size(40, 40));
            btnUser.Size = new Size(300, 50);
        }

        private void BtnRawMaterial_Click(object? sender, EventArgs e)
        {
            new RawMaterialForm().ShowDialog();
        }


    }
}