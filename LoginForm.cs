using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace komfort
{
    public partial class LoginForm : Form
    {
        private int failedAttempts = 0;
        private bool captchaRequired = false;
        private string currentCaptcha = "";
        private System.Windows.Forms.Timer blockTimer = new System.Windows.Forms.Timer();
        private int blockSecondsRemaining = 0;
        private const string HistoryFile = "login_history.csv";

        public LoginForm()
        {
            InitializeComponent();

            bool inDesignMode = LicenseManager.UsageMode == LicenseUsageMode.Designtime;

            if (!inDesignMode)
            {
                chkShowPassword.CheckedChanged += CbShow_CheckedChanged;
                btnLogin.Click += btnLogin_Click;
                btnRefreshCaptcha.Click += btnShowCaptcha_Click;
                this.Load += LoginForm_Load;

                this.AcceptButton = btnLogin;

                try { this.Icon = new Icon("Resources/app.ico"); }
                catch { }

                blockTimer.Interval = 1000;
                blockTimer.Tick += BlockTimer_Tick;
            }
        }

        private void CbShow_CheckedChanged(object sender, EventArgs e)
        {
            txtPassword.UseSystemPasswordChar = !chkShowPassword.Checked;
        }

        private void LoginForm_Load(object sender, EventArgs e) { }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (blockSecondsRemaining > 0) return;

            var login = txtLogin.Text.Trim();
            var pass = txtPassword.Text;

            if (captchaRequired)
            {
                if (string.IsNullOrWhiteSpace(txtCaptcha.Text) ||
                    !string.Equals(txtCaptcha.Text.Trim(), currentCaptcha, StringComparison.OrdinalIgnoreCase))
                {
                    ProcessFailedAttempt(login, "captcha");
                    MessageBox.Show("Неверная капча", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }

            try
            {
                var dt = DataAccess.ExecuteQuery(
                    "SELECT Логин, Пароль, ФИО, Роль_сотрудника FROM Пользователи WHERE Логин = @login",
                    new Microsoft.Data.SqlClient.SqlParameter("@login", login));

                if (dt.Rows.Count == 0)
                {
                    ProcessFailedAttempt(login, "no_user");
                    MessageBox.Show("Неверный логин или пароль", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var dbPass = dt.Rows[0]["Пароль"].ToString();
                if (dbPass != pass)
                {
                    ProcessFailedAttempt(login, "bad_pass");
                    MessageBox.Show("Неверный логин или пароль", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                SaveLoginHistory(login, true);

                var fio = dt.Rows[0]["ФИО"].ToString();
                var role = dt.Rows[0]["Роль_сотрудника"].ToString();

                var mf = new MainForm(login, fio, role);
                this.Hide();
                mf.ShowDialog();
                this.Show();

                failedAttempts = 0;
                captchaRequired = false;

                picCaptcha.Visible = false;
                txtCaptcha.Visible = false;
                btnRefreshCaptcha.Visible = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при подключении к БД: " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnShowCaptcha_Click(object sender, EventArgs e)
        {
            GenerateCaptcha();
        }

        private void GenerateCaptcha()
        {
            var (img, code) = CaptchaGenerator.Generate();
            currentCaptcha = code;
            picCaptcha.Image?.Dispose();
            picCaptcha.Image = img;
        }

        private void ProcessFailedAttempt(string login, string reason)
        {
            failedAttempts++;
            SaveLoginHistory(login ?? "", false);

            if (!captchaRequired)
            {
                captchaRequired = true;

                picCaptcha.Visible = true;
                txtCaptcha.Visible = true;
                btnRefreshCaptcha.Visible = true;

                GenerateCaptcha();
            }

            MessageBox.Show("Неуспешная авторизация", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            if (failedAttempts == 1)
                StartBlock(5);
            else if (failedAttempts == 2)
                StartBlock(3 * 60);
            else
            {
                MessageBox.Show("Попытки входа исчерпаны. Перезапустите приложение.",
                                "Блокировка", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                btnLogin.Enabled = false;
            }
        }

        private void StartBlock(int seconds)
        {
            blockSecondsRemaining = seconds;
            btnLogin.Enabled = false;

            lblTimer.Visible = true;

            blockTimer.Start();
            UpdateBlockLabel();
        }

        private void BlockTimer_Tick(object sender, EventArgs e)
        {
            blockSecondsRemaining--;
            UpdateBlockLabel();

            if (blockSecondsRemaining <= 0)
            {
                blockTimer.Stop();
                btnLogin.Enabled = true;

                lblTimer.Visible = false;
            }
        }

        private void UpdateBlockLabel()
        {
            lblTimer.Text = blockSecondsRemaining > 0
                ? $"Блокировка входа: {blockSecondsRemaining} сек."
                : "";
        }

        private void SaveLoginHistory(string login, bool success)
        {
            try
            {
                var line = $"{DateTime.Now:O};{login};{(success ? "1" : "0")}";
                File.AppendAllLines(HistoryFile, new[] { line });
            }
            catch { }
        }
    }
}
