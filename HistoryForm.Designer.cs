using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace komfort
{
    partial class HistoryForm
    {
        private IContainer components = null;
        private DataGridView dataGridView1;
        private TextBox txtFilter;
        private Label lblFilter;
        private Button btnClose;

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
            ComponentResourceManager resources = new ComponentResourceManager(typeof(HistoryForm));
            txtFilter = new TextBox();
            dataGridView1 = new DataGridView();
            lblFilter = new Label();
            btnClose = new Button();
            ((ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // txtFilter
            // 
            txtFilter.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtFilter.Location = new Point(65, 12);
            txtFilter.Name = "txtFilter";
            txtFilter.Size = new Size(607, 22);
            txtFilter.TabIndex = 1;
            txtFilter.TextChanged += txtFilter_TextChanged;
            // 
            // dataGridView1
            // 
            dataGridView1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.BackgroundColor = Color.FromArgb(210, 223, 255);
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new Point(12, 41);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.ReadOnly = true;
            dataGridView1.RowHeadersVisible = false;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.Size = new Size(660, 380);
            dataGridView1.TabIndex = 0;
            // 
            // lblFilter
            // 
            lblFilter.AutoSize = true;
            lblFilter.Location = new Point(12, 15);
            lblFilter.Name = "lblFilter";
            lblFilter.Size = new Size(42, 14);
            lblFilter.TabIndex = 2;
            lblFilter.Text = "Поиск:";
            // 
            // btnClose
            // 
            btnClose.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnClose.BackColor = Color.FromArgb(53, 92, 189);
            btnClose.FlatStyle = FlatStyle.Popup;
            btnClose.Location = new Point(597, 427);
            btnClose.Name = "btnClose";
            btnClose.Size = new Size(75, 23);
            btnClose.TabIndex = 3;
            btnClose.Text = "Закрыть";
            btnClose.UseVisualStyleBackColor = false;
            btnClose.Click += btnClose_Click;
            // 
            // HistoryForm
            // 
            BackColor = Color.White;
            ClientSize = new Size(684, 461);
            Controls.Add(btnClose);
            Controls.Add(lblFilter);
            Controls.Add(dataGridView1);
            Controls.Add(txtFilter);
            Font = new Font("Candara", 9F);
            Icon = (Icon)resources.GetObject("$this.Icon");
            MinimumSize = new Size(700, 500);
            Name = "HistoryForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "История входов";
            Load += HistoryForm_Load;
            ((ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}