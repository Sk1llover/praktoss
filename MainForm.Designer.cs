using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace komfort
{
    partial class MainForm
    {
        private IContainer components = null;
        private Button btnUser;
        private TextBox txtSearch;
        private ComboBox comboSupplier;
        private RadioButton rbAsc;
        private RadioButton rbDesc;
        private Button btnRefresh;
        private Button btnLogout;
        private Button btnHistory;
        private Button btnAddProduct;
        private Button btnRawMaterial;      // ✅ добавлено
        private FlowLayoutPanel flowLayoutPanelProducts;
        private Panel topPanel;
        private PictureBox pictureBox1;
        private ContextMenuStrip contextMenuProducts;
        private ToolStripMenuItem editToolStripMenuItem;
        private ToolStripMenuItem deleteToolStripMenuItem;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();

            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            components = new Container();
            ComponentResourceManager resources = new ComponentResourceManager(typeof(MainForm));
            btnAddProduct = new Button();
            btnHistory = new Button();
            contextMenuProducts = new ContextMenuStrip(components);
            editToolStripMenuItem = new ToolStripMenuItem();
            deleteToolStripMenuItem = new ToolStripMenuItem();
            topPanel = new Panel();
            pictureBox1 = new PictureBox();
            btnUser = new Button();
            txtSearch = new TextBox();
            comboSupplier = new ComboBox();
            rbAsc = new RadioButton();
            rbDesc = new RadioButton();
            btnRefresh = new Button();
            btnRawMaterial = new Button();
            btnLogout = new Button();
            flowLayoutPanelProducts = new FlowLayoutPanel();
            contextMenuProducts.SuspendLayout();
            topPanel.SuspendLayout();
            ((ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // btnAddProduct
            // 
            btnAddProduct.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnAddProduct.BackColor = Color.FromArgb(53, 92, 189);
            btnAddProduct.FlatStyle = FlatStyle.Popup;
            btnAddProduct.Location = new Point(470, 68);
            btnAddProduct.Name = "btnAddProduct";
            btnAddProduct.Size = new Size(103, 21);
            btnAddProduct.TabIndex = 10;
            btnAddProduct.Text = "Добавить товар";
            btnAddProduct.UseVisualStyleBackColor = false;
            // 
            // btnHistory
            // 
            btnHistory.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnHistory.BackColor = Color.FromArgb(53, 92, 189);
            btnHistory.FlatStyle = FlatStyle.Popup;
            btnHistory.Location = new Point(470, 6);
            btnHistory.Name = "btnHistory";
            btnHistory.Size = new Size(81, 39);
            btnHistory.TabIndex = 11;
            btnHistory.Text = "История входов";
            btnHistory.UseVisualStyleBackColor = false;
            // 
            // contextMenuProducts
            // 
            contextMenuProducts.Items.AddRange(new ToolStripItem[] { editToolStripMenuItem, deleteToolStripMenuItem });
            contextMenuProducts.Name = "contextMenuProducts";
            contextMenuProducts.Size = new Size(155, 48);
            // 
            // editToolStripMenuItem
            // 
            editToolStripMenuItem.Name = "editToolStripMenuItem";
            editToolStripMenuItem.Size = new Size(154, 22);
            editToolStripMenuItem.Text = "Редактировать";
            // 
            // deleteToolStripMenuItem
            // 
            deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
            deleteToolStripMenuItem.Size = new Size(154, 22);
            deleteToolStripMenuItem.Text = "Удалить";
            // 
            // topPanel
            // 
            topPanel.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            topPanel.BackColor = Color.FromArgb(210, 223, 255);
            topPanel.Controls.Add(pictureBox1);
            topPanel.Controls.Add(btnUser);
            topPanel.Controls.Add(txtSearch);
            topPanel.Controls.Add(comboSupplier);
            topPanel.Controls.Add(rbAsc);
            topPanel.Controls.Add(rbDesc);
            topPanel.Controls.Add(btnRefresh);
            topPanel.Controls.Add(btnRawMaterial);
            topPanel.Controls.Add(btnAddProduct);
            topPanel.Controls.Add(btnHistory);
            topPanel.Controls.Add(btnLogout);
            topPanel.Font = new Font("Candara", 9F);
            topPanel.Location = new Point(9, 0);
            topPanel.Name = "topPanel";
            topPanel.Size = new Size(833, 95);
            topPanel.TabIndex = 1;
            // 
            // pictureBox1
            // 
            pictureBox1.Image = Properties.Resources.Комфорт;
            pictureBox1.Location = new Point(9, 11);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(62, 62);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.TabIndex = 12;
            pictureBox1.TabStop = false;
            // 
            // btnUser
            // 
            btnUser.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnUser.BackColor = Color.Transparent;
            btnUser.Enabled = false;
            btnUser.FlatAppearance.BorderSize = 0;
            btnUser.FlatStyle = FlatStyle.Flat;
            btnUser.Font = new Font("Candara", 9F, FontStyle.Bold);
            btnUser.Location = new Point(576, 11);
            btnUser.Name = "btnUser";
            btnUser.Size = new Size(105, 28);
            btnUser.TabIndex = 0;
            btnUser.Text = "Пользователь (Роль)";
            btnUser.UseVisualStyleBackColor = false;
            // 
            // txtSearch
            // 
            txtSearch.Location = new Point(100, 11);
            txtSearch.Name = "txtSearch";
            txtSearch.PlaceholderText = "Поиск...";
            txtSearch.Size = new Size(172, 22);
            txtSearch.TabIndex = 1;
            // 
            // comboSupplier
            // 
            comboSupplier.BackColor = Color.FromArgb(53, 92, 189);
            comboSupplier.DropDownStyle = ComboBoxStyle.DropDownList;
            comboSupplier.ForeColor = Color.Black;
            comboSupplier.Location = new Point(280, 11);
            comboSupplier.Name = "comboSupplier";
            comboSupplier.Size = new Size(129, 22);
            comboSupplier.TabIndex = 2;
            // 
            // rbAsc
            // 
            rbAsc.AutoSize = true;
            rbAsc.Location = new Point(100, 44);
            rbAsc.Name = "rbAsc";
            rbAsc.Size = new Size(111, 18);
            rbAsc.TabIndex = 3;
            rbAsc.TabStop = true;
            rbAsc.Text = "По возрастанию";
            rbAsc.UseVisualStyleBackColor = true;
            // 
            // rbDesc
            // 
            rbDesc.AutoSize = true;
            rbDesc.Location = new Point(217, 44);
            rbDesc.Name = "rbDesc";
            rbDesc.Size = new Size(97, 18);
            rbDesc.TabIndex = 4;
            rbDesc.TabStop = true;
            rbDesc.Text = "По убыванию";
            rbDesc.UseVisualStyleBackColor = true;
            // 
            // btnRefresh
            // 
            btnRefresh.BackColor = Color.FromArgb(53, 92, 189);
            btnRefresh.FlatStyle = FlatStyle.Popup;
            btnRefresh.Location = new Point(328, 43);
            btnRefresh.Name = "btnRefresh";
            btnRefresh.Size = new Size(81, 21);
            btnRefresh.TabIndex = 5;
            btnRefresh.Text = "Обновить";
            btnRefresh.UseVisualStyleBackColor = false;
            // 
            // btnRawMaterial
            // 
            btnRawMaterial.BackColor = Color.FromArgb(53, 92, 189);
            btnRawMaterial.FlatStyle = FlatStyle.Popup;
            btnRawMaterial.Location = new Point(328, 68);
            btnRawMaterial.Name = "btnRawMaterial";
            btnRawMaterial.Size = new Size(120, 21);
            btnRawMaterial.TabIndex = 15;
            btnRawMaterial.Text = "Расчёт сырья";
            btnRawMaterial.UseVisualStyleBackColor = false;
            btnRawMaterial.Click += BtnRawMaterial_Click;
            // 
            // btnLogout
            // 
            btnLogout.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnLogout.BackColor = Color.FromArgb(53, 92, 189);
            btnLogout.FlatStyle = FlatStyle.Popup;
            btnLogout.Location = new Point(743, 68);
            btnLogout.Name = "btnLogout";
            btnLogout.Size = new Size(86, 21);
            btnLogout.TabIndex = 7;
            btnLogout.Text = "Выход";
            btnLogout.UseVisualStyleBackColor = false;
            // 
            // flowLayoutPanelProducts
            // 
            flowLayoutPanelProducts.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            flowLayoutPanelProducts.AutoScroll = true;
            flowLayoutPanelProducts.BackColor = SystemColors.Window;
            flowLayoutPanelProducts.BorderStyle = BorderStyle.FixedSingle;
            flowLayoutPanelProducts.Font = new Font("Candara", 9F);
            flowLayoutPanelProducts.Location = new Point(9, 101);
            flowLayoutPanelProducts.Name = "flowLayoutPanelProducts";
            flowLayoutPanelProducts.Padding = new Padding(4, 5, 4, 5);
            flowLayoutPanelProducts.Size = new Size(833, 400);
            flowLayoutPanelProducts.TabIndex = 2;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(6F, 14F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(850, 504);
            Controls.Add(flowLayoutPanelProducts);
            Controls.Add(topPanel);
            Font = new Font("Candara", 9F);
            Icon = (Icon)resources.GetObject("$this.Icon");
            MinimumSize = new Size(517, 376);
            Name = "MainForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Главный экран - Комфорт";
            WindowState = FormWindowState.Maximized;
            contextMenuProducts.ResumeLayout(false);
            topPanel.ResumeLayout(false);
            topPanel.PerformLayout();
            ((ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
        }
    }
}
