using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace Курсовая
{
    class Setter
    {
        string connectionString = @"Data Source=.\SQLSERVER;Initial Catalog=Weather;Integrated Security=True";
        public DataSet Set(string text, string table)
        {
            string sqlstring = "SELECT*FROM " + table;
            Parser p = new Parser();
            string[] data = p.parse(text).Split(',');
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlDataAdapter adapter = new SqlDataAdapter(sqlstring, connection);
                DataSet ds = new DataSet();
                adapter.Fill(ds);
                /*DataTable dt = ds.Tables[0];
                for (int i = 9; ; i += 8)
                {
                            try
                            {
                                DataRow newRow = dt.NewRow();
                                newRow["Максимальная температура"] = data[i + 1];
                                newRow["Дата"] = data[i];
                                newRow["Минимальная температура"] = data[i + 2];
                                newRow["Средняя температура"] = data[i + 3];
                                newRow["Скорость ветра"] = data[i + 5];
                                newRow["Осадки"] = data[i + 6];
                                newRow["Эффективная температура"] = data[i + 7];
                                dt.Rows.Add(newRow);
                            }
                            catch (IndexOutOfRangeException)
                            {
                                break;
                            }
                }
                
                 SqlCommandBuilder commandBuilder = new SqlCommandBuilder(adapter);
                    adapter.Update(ds);
                    ds.Clear();
                    adapter.Fill(ds);   */
                return ds;
            }
        }

        public string GetDBValues(string table, int row, string column)
        {
            string sqlstring = "SELECT*FROM " + table;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlDataAdapter adapter = new SqlDataAdapter(sqlstring, connection);
                DataSet ds = new DataSet();
                adapter.Fill(ds);
                DataTable dt = ds.Tables[0];
                return dt.Rows[row][column].ToString();
            }
        }

        public void setUser(string table, string username, string pass, string name, string sername)
        {
            string sqlstring = "SELECT*FROM " + table;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlDataAdapter adapter = new SqlDataAdapter(sqlstring, connection);
                DataSet ds = new DataSet();
                adapter.Fill(ds);
                DataTable dt = ds.Tables[0];
                DataRow newRow = dt.NewRow();
                newRow["Логин"] = username;
                newRow["Пароль"] = pass;
                newRow["Имя"] = name;
                newRow["Фамилия"] = sername;
                dt.Rows.Add(newRow);
                SqlCommandBuilder commandBuilder = new SqlCommandBuilder(adapter);
                adapter.Update(ds);
            }
        }
        public DataSet Shower(string table, string order)
        {
            string sqlstring;
            if (order == null)
                sqlstring = "SELECT * FROM " + table;
            else
                sqlstring = "SELECT * FROM " + table + " ORDER BY " + order;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlDataAdapter adapter = new SqlDataAdapter(sqlstring, connection);
                DataSet ds = new DataSet();
                adapter.Fill(ds);
                return ds;
            }
        }

        public string GetUserInfo(string username, string info)
        {
                DataSet ds = Shower("dbo.Пользователи", null);
                DataTable dt = ds.Tables[0];
                dt.Rows[0].Table.PrimaryKey = new DataColumn[] { dt.Columns[1] };
                DataRow login = dt.Rows.Find(username);
                if (info == "user")
                    return login["Логин"].ToString();
                else if (info == "pass")
                    return login["Пароль"].ToString();
                else if (info == "id")
                    return login["Id"].ToString();
                else
                    return "";
        }

        public List<List<double>> GetInput()
        {
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
            List<List<double>> input_layer = new List<List<double>>();
            DateTime d1 = new DateTime(2016, 12, 1);
            DateTime d2 = new DateTime(2017, 4, 11);
            Setter set = new Setter();
            DataSet ds = set.Shower("dbo.Погода", null);
            DataTable dt = ds.Tables[0];
            dt.Rows[0].Table.PrimaryKey = new DataColumn[] { dt.Columns[1] };
            DataRow num1 = dt.Rows.Find(d1);
            DataRow num2 = dt.Rows.Find(d2);
            for (int i = Convert.ToInt32(num1["Номер дня"]); i <= Convert.ToInt32(num2["Номер дня"]); i++)
            {
                dt.Rows[0].Table.PrimaryKey = new DataColumn[] { dt.Columns[0] };
                /*
                x.Add(Convert.ToDouble(dt.Rows.Find(i)["Скорость ветра"].ToString()));
                x.Add(Convert.ToDouble(dt.Rows.Find(i)["Осадки"].ToString()));
                x.Add(Convert.ToDouble(dt.Rows.Find(i)["Средняя температура"].ToString()));
                */
                input_layer.Add(new List<double> { Convert.ToDouble(dt.Rows.Find(i)["Скорость ветра"].ToString()),
                                                   Convert.ToDouble(dt.Rows.Find(i)["Осадки"].ToString()),
                                                   Convert.ToDouble(dt.Rows.Find(i)["Средняя температура"].ToString()) });
            }
            return input_layer;
        }

        public List<double> GetOutput()
        {
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
            List<double> output_layer = new List<double>();
            DateTime d1 = new DateTime(2016, 12, 2);
            DateTime d2 = new DateTime(2017, 4, 12);
            Setter set = new Setter();
            DataSet ds = set.Shower("dbo.Погода", null);
            DataTable dt = ds.Tables[0];
            dt.Rows[0].Table.PrimaryKey = new DataColumn[] { dt.Columns[1] };
            DataRow num1 = dt.Rows.Find(d1);
            DataRow num2 = dt.Rows.Find(d2);
            for (int i = Convert.ToInt32(num1["Номер дня"]); i <= Convert.ToInt32(num2["Номер дня"]); i++)
            {
                dt.Rows[0].Table.PrimaryKey = new DataColumn[] { dt.Columns[0] };
                output_layer.Add(Convert.ToDouble(dt.Rows.Find(i)["Средняя температура"]));
            }
            return output_layer;
        }

        public void setLog(int id, string action, DateTime datetime)
        {
            string sqlstring = "SELECT*FROM dbo.Журнал";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlDataAdapter adapter = new SqlDataAdapter(sqlstring, connection);
                DataSet ds = new DataSet();
                adapter.Fill(ds);
                DataTable dt = ds.Tables[0];
                DataRow newRow = dt.NewRow();
                newRow["userId"] = id;
                newRow["Дата и Время"] = datetime;
                if (action == "Вход")
                    newRow["Действие"] = "Начало сессии";
                if (action == "Выход")
                    newRow["Действие"] = "Конец сессии";
                if (action == "Пауза")
                    newRow["Действие"] = "Сессия приостановлена";
                if (action == "Плэй")
                    newRow["Действие"] = "Сессия возобновлена";
                if (action == "График")
                    newRow["Действие"] = "Построен график";
                if (action == "Прогноз")
                    newRow["Действие"] = "Прогноз";
                if (action == "Показать")
                    newRow["Действие"] = "Выведена таблица";
                if (action == "Регистрация")
                    newRow["Действие"] = "Пользователь зарегистрирован";
                dt.Rows.Add(newRow);
                SqlCommandBuilder commandBuilder = new SqlCommandBuilder(adapter);
                adapter.Update(ds);
            }
        }

        public void setPredict(int id, string prediction, DateTime datetime)
        {
            string sqlstring = "SELECT*FROM dbo.Прогноз";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlDataAdapter adapter = new SqlDataAdapter(sqlstring, connection);
                DataSet ds = new DataSet();
                adapter.Fill(ds);
                DataTable dt = ds.Tables[0];
                DataRow newRow = dt.NewRow();
                newRow["UId"] = id;
                newRow["Прогноз"] = prediction;
                newRow["Дата и Время"] = datetime;
                dt.Rows.Add(newRow);
                SqlCommandBuilder commandBuilder = new SqlCommandBuilder(adapter);
                adapter.Update(ds);
            }
        }
    }
}
