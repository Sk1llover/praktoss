using System;
using System.Data;
using System.Windows.Forms;
using Microsoft.Data.SqlClient;

namespace komfort
{
    public static class DataAccess
    {
        public const string ConnectionString =
            @"Data Source=ADCLG1;Initial Catalog=Z_УП_Z;Integrated Security=true;TrustServerCertificate=true;";

        public static DataTable ExecuteQuery(string sql, params SqlParameter[] parameters)
        {
            var dt = new DataTable();
            using (var conn = new SqlConnection(ConnectionString))
            using (var cmd = new SqlCommand(sql, conn))
            {
                if (parameters != null && parameters.Length > 0)
                    cmd.Parameters.AddRange(parameters);

                var da = new SqlDataAdapter(cmd);
                da.Fill(dt);
            }
            return dt;
        }

        public static int ExecuteNonQuery(string sql, params SqlParameter[] parameters)
        {
            using (var conn = new SqlConnection(ConnectionString))
            using (var cmd = new SqlCommand(sql, conn))
            {
                if (parameters != null && parameters.Length > 0)
                    cmd.Parameters.AddRange(parameters);

                conn.Open();
                return cmd.ExecuteNonQuery();
            }
        }

        public static T ExecuteScalar<T>(string sql, params SqlParameter[] parameters)
        {
            using (var conn = new SqlConnection(ConnectionString))
            using (var cmd = new SqlCommand(sql, conn))
            {
                if (parameters != null && parameters.Length > 0)
                    cmd.Parameters.AddRange(parameters);

                conn.Open();
                var result = cmd.ExecuteScalar();
                return result == DBNull.Value ? default(T) : (T)Convert.ChangeType(result, typeof(T));
            }
        }

        public static int GetNextProductId()
        {
            try
            {
                var sql = "SELECT ISNULL(MAX(ID_товара), 0) + 1 FROM Продукты";
                return ExecuteScalar<int>(sql);
            }
            catch (Exception ex)
            {
                throw new Exception($"Ошибка при получении следующего ID товара: {ex.Message}", ex);
            }
        }

        public static void TestConnection()
        {
            try
            {
                using (var conn = new SqlConnection(ConnectionString))
                {
                    conn.Open();
                    MessageBox.Show("Подключение к БД успешно!", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    var tables = ExecuteQuery(@"
                        SELECT TABLE_NAME 
                        FROM INFORMATION_SCHEMA.TABLES 
                        WHERE TABLE_TYPE = 'BASE TABLE'");

                    string tableList = "Таблицы в БД:\n";
                    foreach (DataRow row in tables.Rows)
                        tableList += $"- {row[0]}\n";

                    MessageBox.Show(tableList, "Таблицы", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка подключения: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public static DataTable GetWorkshopsForProduct(string productName)
        {
            string sql = @"
                SELECT Название_цеха AS [Название цеха],
                       Количество_человек_для_производства AS [Количество человек],
                       Время_изготовления_ч AS [Время, ч]
                FROM Цеха_продукты
                WHERE Наименование_продукции = @productName";

            return ExecuteQuery(sql, new SqlParameter("@productName", productName));
        }

        public static double GetProductTypeCoefficient(int productTypeId)
        {
            string sql = "SELECT Коэффициент FROM Тип_продукта WHERE ID_типа = @id";
            return ExecuteScalar<double>(sql, new SqlParameter("@id", productTypeId));
        }

        public static double GetMaterialLossPercent(int materialTypeId)
        {
            string sql = "SELECT Процент_потерь FROM Тип_материала WHERE ID_типа = @id";
            return ExecuteScalar<double>(sql, new SqlParameter("@id", materialTypeId));
        }


    }
}