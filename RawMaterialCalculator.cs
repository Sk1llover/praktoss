using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace komfort
{
    public static class RawMaterialCalculator
    {
        /// <summary>
        /// Рассчитывает количество сырья, необходимое для производства заданного количества продукции.
        /// </summary>
        public static int CalculateRawMaterial(
            int productTypeId, int materialTypeId, int quantity,
            double param1, double param2)
        {
            try
            {
                if (productTypeId <= 0 || materialTypeId <= 0 ||
                    quantity <= 0 || param1 <= 0 || param2 <= 0)
                    return -1;

                double productCoef = DataAccess.GetProductTypeCoefficient(productTypeId);
                double materialLoss = DataAccess.GetMaterialLossPercent(materialTypeId);

                if (productCoef <= 0 || materialLoss < 0)
                    return -1;

                double rawPerUnit = param1 * param2 * productCoef;

                double total = rawPerUnit * quantity * (1 + materialLoss / 100.0);

                return (int)Math.Ceiling(total);
            }
            catch
            {
                return -1;
            }
        }
    }
}
