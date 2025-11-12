using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace komfort
{
    partial class ProductCard
    {
        private IContainer components = null;
        private Label lblTitle;
        private Label lblLeft;
        private Label lblRight;

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
            lblTitle = new Label();
            lblLeft = new Label();
            lblRight = new Label();
            SuspendLayout();
            // 
            // lblTitle
            // 
            lblTitle.Dock = DockStyle.Top;
            lblTitle.Font = new Font("Candara", 10F, FontStyle.Bold);
            lblTitle.Location = new Point(8, 8);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(744, 28);
            lblTitle.TabIndex = 2;
            lblTitle.Text = "Тип | Наименование";
            // 
            // lblLeft
            // 
            lblLeft.Dock = DockStyle.Left;
            lblLeft.Font = new Font("Candara", 9F);
            lblLeft.Location = new Point(8, 36);
            lblLeft.Name = "lblLeft";
            lblLeft.Padding = new Padding(4);
            lblLeft.Size = new Size(520, 77);
            lblLeft.TabIndex = 1;
            lblLeft.Text = "Артикул...";
            // 
            // lblRight
            // 
            lblRight.Dock = DockStyle.Fill;
            lblRight.Font = new Font("Candara", 9F);
            lblRight.Location = new Point(528, 36);
            lblRight.Name = "lblRight";
            lblRight.Padding = new Padding(4);
            lblRight.Size = new Size(224, 77);
            lblRight.TabIndex = 0;
            lblRight.TextAlign = ContentAlignment.TopRight;
            // 
            // ProductCard
            // 
            BackColor = Color.White;
            BorderStyle = BorderStyle.FixedSingle;
            Controls.Add(lblRight);
            Controls.Add(lblLeft);
            Controls.Add(lblTitle);
            Font = new Font("Candara", 9F);
            Margin = new Padding(6);
            Name = "ProductCard";
            Padding = new Padding(8);
            Size = new Size(760, 121);
            ResumeLayout(false);
        }
    }
}
