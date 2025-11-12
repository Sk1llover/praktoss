using System;
using System.Collections.Generic;

namespace komfort
{
    public class Product
    {
        public string Артикул { get; set; } = "";
        public string Наименование { get; set; } = "";
        public string Тип_продукции { get; set; } = "";
        public string Основной_материал { get; set; } = "";
        public decimal Минимальная_стоимость_для_партнера { get; set; }
        public decimal Время_изготовления_ч { get; set; }
        public string Поставщик { get; set; } = "";
        public string НазваниеЦеха { get; set; } = "";
        public int Количество { get; set; }
        public int КоличествоРабочих { get; set; }
        public decimal ВремяВЦехе_ч { get; set; }
        public string Создатель { get; set; } = "";

        public Dictionary<string, string> ExtraFields { get; set; } = new Dictionary<string, string>();

        public Product Clone()
        {
            return new Product
            {
                Артикул = this.Артикул,
                Наименование = this.Наименование,
                Тип_продукции = this.Тип_продукции,
                Основной_материал = this.Основной_материал,
                Минимальная_стоимость_для_партнера = this.Минимальная_стоимость_для_партнера,
                Время_изготовления_ч = this.Время_изготовления_ч,
                Поставщик = this.Поставщик,
                НазваниеЦеха = this.НазваниеЦеха,
                Количество = this.Количество,
                КоличествоРабочих = this.КоличествоРабочих,
                ВремяВЦехе_ч = this.ВремяВЦехе_ч,
                Создатель = this.Создатель,
                ExtraFields = new Dictionary<string, string>(this.ExtraFields)
            };
        }
    }
}
    

