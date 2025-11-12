namespace komfort
{
    partial class LoginForm
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.TextBox txtLogin;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.CheckBox chkShowPassword;
        private System.Windows.Forms.Button btnLogin;

        // CAPTCHA
        private System.Windows.Forms.PictureBox picCaptcha;
        private System.Windows.Forms.TextBox txtCaptcha;
        private System.Windows.Forms.Button btnRefreshCaptcha;

        // TIMER LABEL
        private System.Windows.Forms.Label lblTimer;

        /// <summary>
        /// Clean resources
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LoginForm));
            txtLogin = new TextBox();
            txtPassword = new TextBox();
            chkShowPassword = new CheckBox();
            btnLogin = new Button();
            picCaptcha = new PictureBox();
            txtCaptcha = new TextBox();
            btnRefreshCaptcha = new Button();
            lblTimer = new Label();
            pictureBox1 = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)picCaptcha).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // txtLogin
            // 
            txtLogin.BackColor = Color.FromArgb(210, 223, 255);
            txtLogin.BorderStyle = BorderStyle.FixedSingle;
            txtLogin.Font = new Font("Candara", 11F);
            txtLogin.Location = new Point(130, 167);
            txtLogin.Margin = new Padding(3, 2, 3, 2);
            txtLogin.Name = "txtLogin";
            txtLogin.PlaceholderText = "логин";
            txtLogin.Size = new Size(188, 25);
            txtLogin.TabIndex = 0;
            // 
            // txtPassword
            // 
            txtPassword.BackColor = Color.FromArgb(210, 223, 255);
            txtPassword.BorderStyle = BorderStyle.FixedSingle;
            txtPassword.Font = new Font("Candara", 11F);
            txtPassword.Location = new Point(130, 202);
            txtPassword.Margin = new Padding(3, 2, 3, 2);
            txtPassword.Name = "txtPassword";
            txtPassword.PlaceholderText = "пароль";
            txtPassword.Size = new Size(188, 25);
            txtPassword.TabIndex = 1;
            txtPassword.UseSystemPasswordChar = true;
            // 
            // chkShowPassword
            // 
            chkShowPassword.AutoSize = true;
            chkShowPassword.Font = new Font("Candara", 9F);
            chkShowPassword.Location = new Point(325, 202);
            chkShowPassword.Margin = new Padding(3, 2, 3, 2);
            chkShowPassword.Name = "chkShowPassword";
            chkShowPassword.Size = new Size(73, 32);
            chkShowPassword.TabIndex = 2;
            chkShowPassword.Text = "показать\nпароль";
            // 
            // btnLogin
            // 
            btnLogin.BackColor = Color.FromArgb(53, 92, 189);
            btnLogin.FlatStyle = FlatStyle.Popup;
            btnLogin.Font = new Font("Candara", 11F);
            btnLogin.Location = new Point(175, 236);
            btnLogin.Margin = new Padding(3, 2, 3, 2);
            btnLogin.Name = "btnLogin";
            btnLogin.Size = new Size(98, 24);
            btnLogin.TabIndex = 3;
            btnLogin.Text = "Войти";
            btnLogin.UseVisualStyleBackColor = false;
            // 
            // picCaptcha
            // 
            picCaptcha.Location = new Point(130, 285);
            picCaptcha.Margin = new Padding(3, 2, 3, 2);
            picCaptcha.Name = "picCaptcha";
            picCaptcha.Size = new Size(135, 49);
            picCaptcha.SizeMode = PictureBoxSizeMode.StretchImage;
            picCaptcha.TabIndex = 4;
            picCaptcha.TabStop = false;
            picCaptcha.Visible = false;
            // 
            // txtCaptcha
            // 
            txtCaptcha.BackColor = Color.FromArgb(210, 223, 255);
            txtCaptcha.BorderStyle = BorderStyle.FixedSingle;
            txtCaptcha.Font = new Font("Candara", 11F);
            txtCaptcha.Location = new Point(130, 337);
            txtCaptcha.Margin = new Padding(3, 2, 3, 2);
            txtCaptcha.Name = "txtCaptcha";
            txtCaptcha.PlaceholderText = "ввод капчи";
            txtCaptcha.Size = new Size(245, 25);
            txtCaptcha.TabIndex = 6;
            txtCaptcha.Visible = false;
            // 
            // btnRefreshCaptcha
            // 
            btnRefreshCaptcha.BackColor = Color.FromArgb(53, 92, 189);
            btnRefreshCaptcha.FlatStyle = FlatStyle.Popup;
            btnRefreshCaptcha.Font = new Font("Candara", 9F);
            btnRefreshCaptcha.Location = new Point(273, 295);
            btnRefreshCaptcha.Margin = new Padding(3, 2, 3, 2);
            btnRefreshCaptcha.Name = "btnRefreshCaptcha";
            btnRefreshCaptcha.Size = new Size(101, 21);
            btnRefreshCaptcha.TabIndex = 5;
            btnRefreshCaptcha.Text = "обновить капчу";
            btnRefreshCaptcha.UseVisualStyleBackColor = false;
            btnRefreshCaptcha.Visible = false;
            // 
            // lblTimer
            // 
            lblTimer.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblTimer.Location = new Point(130, 259);
            lblTimer.Name = "lblTimer";
            lblTimer.Size = new Size(243, 19);
            lblTimer.TabIndex = 7;
            lblTimer.TextAlign = ContentAlignment.MiddleCenter;
            lblTimer.Visible = false;
            // 
            // pictureBox1
            // 
            pictureBox1.Image = Properties.Resources.Комфорт;
            pictureBox1.Location = new Point(184, 44);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(88, 92);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.TabIndex = 8;
            pictureBox1.TabStop = false;
            // 
            // LoginForm
            // 
            AutoScaleDimensions = new SizeF(6F, 14F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(472, 442);
            Controls.Add(pictureBox1);
            Controls.Add(txtLogin);
            Controls.Add(txtPassword);
            Controls.Add(chkShowPassword);
            Controls.Add(btnLogin);
            Controls.Add(picCaptcha);
            Controls.Add(btnRefreshCaptcha);
            Controls.Add(txtCaptcha);
            Controls.Add(lblTimer);
            Font = new Font("Candara", 9F);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Icon = (Icon)resources.GetObject("$this.Icon");
            Margin = new Padding(3, 2, 3, 2);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "LoginForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Авторизация";
            ((System.ComponentModel.ISupportInitialize)picCaptcha).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private PictureBox pictureBox1;
    }
}
