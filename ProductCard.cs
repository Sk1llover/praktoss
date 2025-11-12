using System;
using System.Drawing;
using System.Windows.Forms;

namespace komfort
{
    public partial class ProductCard : UserControl
    {
        public event EventHandler? Clicked;

        public ProductCard()
        {
            InitializeComponent();
            WireUpClickEvents();
        }

        private void WireUpClickEvents()
        {
            // Делаем всю карточку кликабельной
            foreach (Control control in this.Controls)
            {
                control.Click += (s, e) => Clicked?.Invoke(this, e);
                control.MouseEnter += (s, e) => this.BackColor = Color.LightGray;
                control.MouseLeave += (s, e) => this.BackColor = SystemColors.Control;
            }

            this.Click += (s, e) => Clicked?.Invoke(this, e);
            this.MouseEnter += (s, e) => this.BackColor = Color.LightGray;
            this.MouseLeave += (s, e) => this.BackColor = SystemColors.Control;
        }

        public void SetData(Product p)
        {
            lblTitle.Text = $"{p.Тип_продукции} | {p.Наименование}";
            var left = $"Артикул: {p.Артикул}\nМинимальная стоимость: {p.Минимальная_стоимость_для_партнера:C}\nОсновной материал: {p.Основной_материал}";
            if (!string.IsNullOrEmpty(p.Поставщик)) left += $"\nПоставщик: {p.Поставщик}";
            if (!string.IsNullOrEmpty(p.Создатель)) left += $"\nСоздатель: {p.Создатель}";
            lblLeft.Text = left;
            var right = p.Количество != 0 ? $"Кол-во на складе: {p.Количество}\n" : "";
            right += $"Время изготовления: {p.Время_изготовления_ч}";
            lblRight.Text = right;

            // Сохраняем ссылку на товар в Tag для последующего использования
            this.Tag = p;
        }
    }
}