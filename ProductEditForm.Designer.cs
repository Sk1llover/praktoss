namespace komfort
{
    partial class ProductEditForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Label lblArticle;
        private System.Windows.Forms.TextBox txtArticle;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Label lblType;
        private System.Windows.Forms.ComboBox comboType;
        private System.Windows.Forms.Label lblMaterial;
        private System.Windows.Forms.ComboBox comboMaterial;
        private System.Windows.Forms.Label lblCost;
        private System.Windows.Forms.NumericUpDown numCost;
        private System.Windows.Forms.Label lblTime;
        private System.Windows.Forms.NumericUpDown numTime;
        private System.Windows.Forms.Label lblSupplier;
        private System.Windows.Forms.ComboBox comboSupplier;
        private System.Windows.Forms.Label lblQuantity;
        private System.Windows.Forms.NumericUpDown numQuantity;
        private System.Windows.Forms.Label lblCreator;
        private System.Windows.Forms.TextBox txtCreator;
        private System.Windows.Forms.Label lblWorkshop;
        private System.Windows.Forms.ComboBox comboWorkshop;

        // 🔹 Добавлено
        private System.Windows.Forms.Label lblWorkers;
        private System.Windows.Forms.NumericUpDown numWorkers;

        // 🔹 Добавлено: время в цехе (Время_изготовления_ч из таблицы Цеха_продукты)
        private System.Windows.Forms.Label lblWorkshopTime;
        private System.Windows.Forms.NumericUpDown numWorkshopTime;

        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnWorkshops;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ProductEditForm));
            lblArticle = new Label();
            txtArticle = new TextBox();
            lblName = new Label();
            txtName = new TextBox();
            lblType = new Label();
            comboType = new ComboBox();
            lblMaterial = new Label();
            comboMaterial = new ComboBox();
            lblCost = new Label();
            numCost = new NumericUpDown();
            lblTime = new Label();
            numTime = new NumericUpDown();
            lblSupplier = new Label();
            comboSupplier = new ComboBox();
            lblQuantity = new Label();
            numQuantity = new NumericUpDown();
            lblCreator = new Label();
            txtCreator = new TextBox();
            lblWorkshop = new Label();
            comboWorkshop = new ComboBox();
            lblWorkers = new Label();
            numWorkers = new NumericUpDown();
            lblWorkshopTime = new Label();
            numWorkshopTime = new NumericUpDown();
            btnSave = new Button();
            btnCancel = new Button();
            btnWorkshops = new Button();
            ((System.ComponentModel.ISupportInitialize)numCost).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numTime).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numQuantity).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numWorkers).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numWorkshopTime).BeginInit();
            SuspendLayout();
            // 
            // lblArticle
            // 
            lblArticle.AutoSize = true;
            lblArticle.Location = new Point(17, 19);
            lblArticle.Name = "lblArticle";
            lblArticle.Size = new Size(53, 14);
            lblArticle.TabIndex = 0;
            lblArticle.Text = "Артикул:";
            // 
            // txtArticle
            // 
            txtArticle.Location = new Point(153, 12);
            txtArticle.Name = "txtArticle";
            txtArticle.Size = new Size(172, 22);
            txtArticle.TabIndex = 1;
            // 
            // lblName
            // 
            lblName.AutoSize = true;
            lblName.Location = new Point(17, 47);
            lblName.Name = "lblName";
            lblName.Size = new Size(127, 14);
            lblName.TabIndex = 2;
            lblName.Text = "Наименование товара:";
            // 
            // txtName
            // 
            txtName.Location = new Point(153, 40);
            txtName.Name = "txtName";
            txtName.Size = new Size(172, 22);
            txtName.TabIndex = 3;
            // 
            // lblType
            // 
            lblType.AutoSize = true;
            lblType.Location = new Point(17, 75);
            lblType.Name = "lblType";
            lblType.Size = new Size(88, 14);
            lblType.TabIndex = 4;
            lblType.Text = "Тип продукции:";
            // 
            // comboType
            // 
            comboType.DropDownStyle = ComboBoxStyle.DropDownList;
            comboType.FormattingEnabled = true;
            comboType.Location = new Point(153, 68);
            comboType.Name = "comboType";
            comboType.Size = new Size(172, 22);
            comboType.TabIndex = 5;
            // 
            // lblMaterial
            // 
            lblMaterial.AutoSize = true;
            lblMaterial.Location = new Point(17, 103);
            lblMaterial.Name = "lblMaterial";
            lblMaterial.Size = new Size(114, 14);
            lblMaterial.TabIndex = 6;
            lblMaterial.Text = "Основной материал:";
            // 
            // comboMaterial
            // 
            comboMaterial.DropDownStyle = ComboBoxStyle.DropDownList;
            comboMaterial.FormattingEnabled = true;
            comboMaterial.Location = new Point(153, 96);
            comboMaterial.Name = "comboMaterial";
            comboMaterial.Size = new Size(172, 22);
            comboMaterial.TabIndex = 7;
            // 
            // lblCost
            // 
            lblCost.AutoSize = true;
            lblCost.Location = new Point(17, 131);
            lblCost.Name = "lblCost";
            lblCost.Size = new Size(97, 14);
            lblCost.TabIndex = 8;
            lblCost.Text = "Стоимость (руб):";
            // 
            // numCost
            // 
            numCost.DecimalPlaces = 2;
            numCost.Location = new Point(154, 124);
            numCost.Maximum = new decimal(new int[] { 1000000, 0, 0, 0 });
            numCost.Name = "numCost";
            numCost.Size = new Size(171, 22);
            numCost.TabIndex = 9;
            // 
            // lblTime
            // 
            lblTime.AutoSize = true;
            lblTime.Location = new Point(17, 159);
            lblTime.Name = "lblTime";
            lblTime.Size = new Size(118, 14);
            lblTime.TabIndex = 10;
            lblTime.Text = "Время изготовления:";
            // 
            // numTime
            // 
            numTime.DecimalPlaces = 1;
            numTime.Location = new Point(154, 152);
            numTime.Maximum = new decimal(new int[] { 1000, 0, 0, 0 });
            numTime.Name = "numTime";
            numTime.Size = new Size(171, 22);
            numTime.TabIndex = 11;
            // 
            // lblSupplier
            // 
            lblSupplier.AutoSize = true;
            lblSupplier.Location = new Point(17, 187);
            lblSupplier.Name = "lblSupplier";
            lblSupplier.Size = new Size(69, 14);
            lblSupplier.TabIndex = 12;
            lblSupplier.Text = "Поставщик:";
            // 
            // comboSupplier
            // 
            comboSupplier.DropDownStyle = ComboBoxStyle.DropDownList;
            comboSupplier.FormattingEnabled = true;
            comboSupplier.Location = new Point(154, 180);
            comboSupplier.Name = "comboSupplier";
            comboSupplier.Size = new Size(172, 22);
            comboSupplier.TabIndex = 13;
            // 
            // lblQuantity
            // 
            lblQuantity.AutoSize = true;
            lblQuantity.Location = new Point(17, 215);
            lblQuantity.Name = "lblQuantity";
            lblQuantity.Size = new Size(125, 14);
            lblQuantity.TabIndex = 14;
            lblQuantity.Text = "Количество на складе:";
            // 
            // numQuantity
            // 
            numQuantity.Location = new Point(154, 208);
            numQuantity.Maximum = new decimal(new int[] { 1000000, 0, 0, 0 });
            numQuantity.Name = "numQuantity";
            numQuantity.Size = new Size(171, 22);
            numQuantity.TabIndex = 15;
            // 
            // lblCreator
            // 
            lblCreator.AutoSize = true;
            lblCreator.Location = new Point(17, 243);
            lblCreator.Name = "lblCreator";
            lblCreator.Size = new Size(65, 14);
            lblCreator.TabIndex = 16;
            lblCreator.Text = "Создатель:";
            // 
            // txtCreator
            // 
            txtCreator.Location = new Point(154, 236);
            txtCreator.Name = "txtCreator";
            txtCreator.ReadOnly = true;
            txtCreator.Size = new Size(172, 22);
            txtCreator.TabIndex = 17;
            // 
            // lblWorkshop
            // 
            lblWorkshop.AutoSize = true;
            lblWorkshop.Location = new Point(17, 271);
            lblWorkshop.Name = "lblWorkshop";
            lblWorkshop.Size = new Size(45, 14);
            lblWorkshop.TabIndex = 18;
            lblWorkshop.Text = "Цех №:";
            // 
            // comboWorkshop
            // 
            comboWorkshop.DropDownStyle = ComboBoxStyle.DropDownList;
            comboWorkshop.FormattingEnabled = true;
            comboWorkshop.Location = new Point(154, 264);
            comboWorkshop.Name = "comboWorkshop";
            comboWorkshop.Size = new Size(172, 22);
            comboWorkshop.TabIndex = 19;
            // 
            // lblWorkers
            // 
            lblWorkers.AutoSize = true;
            lblWorkers.Location = new Point(17, 299);
            lblWorkers.Name = "lblWorkers";
            lblWorkers.Size = new Size(137, 14);
            lblWorkers.TabIndex = 24;
            lblWorkers.Text = "Количество работников:";
            // 
            // numWorkers
            // 
            numWorkers.Location = new Point(154, 292);
            numWorkers.Maximum = new decimal(new int[] { 1000, 0, 0, 0 });
            numWorkers.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            numWorkers.Name = "numWorkers";
            numWorkers.Size = new Size(171, 22);
            numWorkers.TabIndex = 25;
            numWorkers.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // lblWorkshopTime
            // 
            lblWorkshopTime.AutoSize = true;
            lblWorkshopTime.Location = new Point(17, 327);
            lblWorkshopTime.Name = "lblWorkshopTime";
            lblWorkshopTime.Size = new Size(97, 14);
            lblWorkshopTime.TabIndex = 26;
            lblWorkshopTime.Text = "Время в цехе (ч):";
            // 
            // numWorkshopTime
            // 
            numWorkshopTime.DecimalPlaces = 1;
            numWorkshopTime.Location = new Point(154, 320);
            numWorkshopTime.Maximum = new decimal(new int[] { 1000, 0, 0, 0 });
            numWorkshopTime.Name = "numWorkshopTime";
            numWorkshopTime.Size = new Size(171, 22);
            numWorkshopTime.TabIndex = 27;
            numWorkshopTime.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // btnSave
            // 
            btnSave.BackColor = Color.FromArgb(210, 223, 255);
            btnSave.FlatStyle = FlatStyle.Popup;
            btnSave.Location = new Point(136, 364);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(81, 28);
            btnSave.TabIndex = 20;
            btnSave.Text = "Сохранить";
            btnSave.UseVisualStyleBackColor = false;
            // 
            // btnCancel
            // 
            btnCancel.BackColor = Color.FromArgb(210, 223, 255);
            btnCancel.FlatStyle = FlatStyle.Popup;
            btnCancel.Location = new Point(244, 364);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(81, 28);
            btnCancel.TabIndex = 21;
            btnCancel.Text = "Отмена";
            btnCancel.UseVisualStyleBackColor = false;
            // 
            // btnWorkshops
            // 
            btnWorkshops.BackColor = Color.FromArgb(210, 223, 255);
            btnWorkshops.FlatStyle = FlatStyle.Popup;
            btnWorkshops.Location = new Point(17, 364);
            btnWorkshops.Name = "btnWorkshops";
            btnWorkshops.Size = new Size(94, 28);
            btnWorkshops.TabIndex = 23;
            btnWorkshops.Text = "Цеха";
            btnWorkshops.UseVisualStyleBackColor = false;
            // 
            // ProductEditForm
            // 
            AutoScaleDimensions = new SizeF(6F, 14F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(332, 439);
            Controls.Add(numWorkshopTime);
            Controls.Add(lblWorkshopTime);
            Controls.Add(numWorkers);
            Controls.Add(lblWorkers);
            Controls.Add(btnWorkshops);
            Controls.Add(btnCancel);
            Controls.Add(btnSave);
            Controls.Add(comboWorkshop);
            Controls.Add(lblWorkshop);
            Controls.Add(txtCreator);
            Controls.Add(lblCreator);
            Controls.Add(numQuantity);
            Controls.Add(lblQuantity);
            Controls.Add(comboSupplier);
            Controls.Add(lblSupplier);
            Controls.Add(numTime);
            Controls.Add(lblTime);
            Controls.Add(numCost);
            Controls.Add(lblCost);
            Controls.Add(comboMaterial);
            Controls.Add(lblMaterial);
            Controls.Add(comboType);
            Controls.Add(lblType);
            Controls.Add(txtName);
            Controls.Add(lblName);
            Controls.Add(txtArticle);
            Controls.Add(lblArticle);
            Font = new Font("Candara", 9F);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "ProductEditForm";
            Text = "Редактирование товара";
            ((System.ComponentModel.ISupportInitialize)numCost).EndInit();
            ((System.ComponentModel.ISupportInitialize)numTime).EndInit();
            ((System.ComponentModel.ISupportInitialize)numQuantity).EndInit();
            ((System.ComponentModel.ISupportInitialize)numWorkers).EndInit();
            ((System.ComponentModel.ISupportInitialize)numWorkshopTime).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }
    }
}
