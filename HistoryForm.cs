using System;
using System.Data;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace komfort
{
    public partial class HistoryForm : Form
    {
        public HistoryForm()
        {
            InitializeComponent();
        }

        private void HistoryForm_Load(object sender, EventArgs e)
        {
            LoadHistory();
            SetupSorting();
        }

        private void LoadHistory()
        {
            var f = "login_history.csv";
            var dt = new DataTable();
            dt.Columns.Add("Время", typeof(DateTime));
            dt.Columns.Add("Логин");
            dt.Columns.Add("Успех");

            if (!File.Exists(f))
            {
                dataGridView1.DataSource = dt;
                return;
            }

            var lines = File.ReadAllLines(f);
            foreach (var l in lines.Reverse())
            {
                var parts = l.Split(';');
                if (parts.Length >= 3)
                {
                    if (DateTime.TryParse(parts[0], out DateTime timestamp))
                    {
                        dt.Rows.Add(timestamp, parts[1], parts[2] == "1" ? "Успешно" : "Ошибка");
                    }
                }
            }

            dataGridView1.DataSource = dt;

            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.Columns["Время"].DefaultCellStyle.Format = "dd.MM.yyyy HH:mm:ss";
            dataGridView1.Columns["Время"].Width = 150;
            dataGridView1.Columns["Логин"].Width = 120;
            dataGridView1.Columns["Успех"].Width = 80;
        }

        private void SetupSorting()
        {
            if (dataGridView1.DataSource != null)
            {
                var dv = (dataGridView1.DataSource as DataTable)?.DefaultView;
                dv.Sort = "Время DESC";
            }
        }

        private void txtFilter_TextChanged(object sender, EventArgs e)
        {
            ApplyFilter();
        }

        private void ApplyFilter()
        {
            var dv = (dataGridView1.DataSource as DataTable)?.DefaultView;
            if (dv != null)
            {
                var filterText = txtFilter.Text.Trim();
                if (string.IsNullOrWhiteSpace(filterText))
                {
                    dv.RowFilter = "";
                }
                else
                {
                    dv.RowFilter = $"Логин LIKE '%{filterText.Replace("'", "''")}%'";
                }
            }
        }

        private void btnSortByDateAsc_Click(object sender, EventArgs e)
        {
            var dv = (dataGridView1.DataSource as DataTable)?.DefaultView;
            if (dv != null)
            {
                dv.Sort = "Время ASC";
            }
        }

        private void btnSortByDateDesc_Click(object sender, EventArgs e)
        {
            var dv = (dataGridView1.DataSource as DataTable)?.DefaultView;
            if (dv != null)
            {
                dv.Sort = "Время DESC";
            }
        }

        private void btnClearFilter_Click(object sender, EventArgs e)
        {
            txtFilter.Text = "";
            ApplyFilter();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadHistory();
            ApplyFilter();
        }
    }
}