namespace komfort
{
    partial class RawMaterialForm
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.ComboBox comboProduct;
        private System.Windows.Forms.ComboBox comboMaterial;
        private System.Windows.Forms.ComboBox comboParam2;
        private System.Windows.Forms.TextBox txtQuantity;
        private System.Windows.Forms.Button btnCalc;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Label lblResult;
        private System.Windows.Forms.Label lblProduct;
        private System.Windows.Forms.Label lblMaterial;
        private System.Windows.Forms.Label lblQuantity;
        private System.Windows.Forms.Label lblParam2;

        /// <summary>
        /// Clean up resources
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            comboProduct = new ComboBox();
            comboMaterial = new ComboBox();
            comboParam2 = new ComboBox();
            txtQuantity = new TextBox();
            btnCalc = new Button();
            btnClose = new Button();
            lblResult = new Label();
            lblProduct = new Label();
            lblMaterial = new Label();
            lblQuantity = new Label();
            lblParam2 = new Label();
            label1 = new Label();
            labelLoss = new Label();
            SuspendLayout();
            // 
            // comboProduct
            // 
            comboProduct.DropDownStyle = ComboBoxStyle.DropDownList;
            comboProduct.Font = new Font("Segoe UI", 10F);
            comboProduct.Location = new Point(180, 25);
            comboProduct.Name = "comboProduct";
            comboProduct.Size = new Size(230, 25);
            comboProduct.TabIndex = 1;
            // 
            // comboMaterial
            // 
            comboMaterial.DropDownStyle = ComboBoxStyle.DropDownList;
            comboMaterial.Font = new Font("Segoe UI", 10F);
            comboMaterial.Location = new Point(180, 80);
            comboMaterial.Name = "comboMaterial";
            comboMaterial.Size = new Size(230, 25);
            comboMaterial.TabIndex = 3;
            // 
            // comboParam2
            // 
            comboParam2.DropDownStyle = ComboBoxStyle.DropDownList;
            comboParam2.Font = new Font("Segoe UI", 10F);
            comboParam2.Location = new Point(180, 190);
            comboParam2.Name = "comboParam2";
            comboParam2.Size = new Size(230, 25);
            comboParam2.TabIndex = 7;
            // 
            // txtQuantity
            // 
            txtQuantity.Font = new Font("Segoe UI", 10F);
            txtQuantity.Location = new Point(180, 135);
            txtQuantity.Name = "txtQuantity";
            txtQuantity.Size = new Size(230, 25);
            txtQuantity.TabIndex = 5;
            // 
            // btnCalc
            // 
            btnCalc.BackColor = Color.FromArgb(53, 92, 189);
            btnCalc.FlatStyle = FlatStyle.Popup;
            btnCalc.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnCalc.ForeColor = Color.White;
            btnCalc.Location = new Point(110, 340);
            btnCalc.Name = "btnCalc";
            btnCalc.Size = new Size(120, 40);
            btnCalc.TabIndex = 9;
            btnCalc.Text = "Рассчитать";
            btnCalc.UseVisualStyleBackColor = false;
            btnCalc.Click += btnCalc_Click;
            // 
            // btnClose
            // 
            btnClose.BackColor = Color.FromArgb(210, 223, 255);
            btnClose.FlatStyle = FlatStyle.Popup;
            btnClose.Font = new Font("Segoe UI", 10F);
            btnClose.Location = new Point(250, 340);
            btnClose.Name = "btnClose";
            btnClose.Size = new Size(120, 40);
            btnClose.TabIndex = 10;
            btnClose.Text = "Закрыть";
            btnClose.UseVisualStyleBackColor = false;
            btnClose.Click += btnClose_Click;
            // 
            // lblResult
            // 
            lblResult.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            lblResult.ForeColor = Color.FromArgb(53, 92, 189);
            lblResult.Location = new Point(30, 255);
            lblResult.Name = "lblResult";
            lblResult.Size = new Size(380, 60);
            lblResult.TabIndex = 8;
            lblResult.Text = "Результат появится здесь";
            lblResult.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblProduct
            // 
            lblProduct.AutoSize = true;
            lblProduct.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblProduct.Location = new Point(30, 30);
            lblProduct.Name = "lblProduct";
            lblProduct.Size = new Size(54, 19);
            lblProduct.TabIndex = 0;
            lblProduct.Text = "Товар:";
            // 
            // lblMaterial
            // 
            lblMaterial.AutoSize = true;
            lblMaterial.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblMaterial.Location = new Point(30, 85);
            lblMaterial.Name = "lblMaterial";
            lblMaterial.Size = new Size(85, 19);
            lblMaterial.TabIndex = 2;
            lblMaterial.Text = "Тип сырья:";
            // 
            // lblQuantity
            // 
            lblQuantity.AutoSize = true;
            lblQuantity.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblQuantity.Location = new Point(30, 140);
            lblQuantity.Name = "lblQuantity";
            lblQuantity.Size = new Size(94, 19);
            lblQuantity.TabIndex = 4;
            lblQuantity.Text = "Количество:";
            // 
            // lblParam2
            // 
            lblParam2.AutoSize = true;
            lblParam2.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblParam2.Location = new Point(30, 195);
            lblParam2.Name = "lblParam2";
            lblParam2.Size = new Size(110, 19);
            lblParam2.TabIndex = 6;
            lblParam2.Text = "Коэффициент:";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            label1.Location = new Point(30, 236);
            label1.Name = "label1";
            label1.Size = new Size(127, 19);
            label1.TabIndex = 11;
            label1.Text = "Процент потерь:";
            // 
            // labelLoss
            // 
            labelLoss.AutoSize = true;
            labelLoss.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            labelLoss.Location = new Point(180, 236);
            labelLoss.Name = "labelLoss";
            labelLoss.Size = new Size(0, 19);
            labelLoss.TabIndex = 12;
            // 
            // RawMaterialForm
            // 
            BackColor = Color.White;
            ClientSize = new Size(450, 430);
            Controls.Add(labelLoss);
            Controls.Add(label1);
            Controls.Add(lblProduct);
            Controls.Add(comboProduct);
            Controls.Add(lblMaterial);
            Controls.Add(comboMaterial);
            Controls.Add(lblQuantity);
            Controls.Add(txtQuantity);
            Controls.Add(lblParam2);
            Controls.Add(comboParam2);
            Controls.Add(lblResult);
            Controls.Add(btnCalc);
            Controls.Add(btnClose);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "RawMaterialForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Расчёт сырья";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Label labelLoss;
    }
}
