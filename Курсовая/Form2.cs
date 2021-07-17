using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using System.IO;

namespace Курсовая
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            // TODO: данная строка кода позволяет загрузить данные в таблицу "weatherDataSet.Погода". При необходимости она может быть перемещена или удалена.
            this.погодаTableAdapter.Fill(this.weatherDataSet.Погода);

        }

        private void chart1_Click(object sender, EventArgs e)
        {
           
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
            chart1.Series[0].Points.Clear();
            chart1.Series[1].Points.Clear();
            DateTime d1 = dateTimePicker1.Value;
            DateTime d2 = dateTimePicker2.Value;
            List<DateTime> x = new List<DateTime>();
            DataTable dt = weatherDataSet.Tables[0];
            dt.Rows[0].Table.PrimaryKey = new DataColumn[] { dt.Columns[1] };
            DataRow num1 = dt.Rows.Find(d1);
            DataRow num2 = dt.Rows.Find(d2);
            if (num1 == null)
                MessageBox.Show("В день начальной даты данные не фиксировались");
            else if (num2 == null)
                MessageBox.Show("В день конечной даты данные не фиксировались");
            else if (d1 >= d2)
            {
                MessageBox.Show("Вы выбрали недопустимый диапозон");
            }
            else
            {
                for (int i = Convert.ToInt32(num1["Номер дня"]); i <= Convert.ToInt32(num2["Номер дня"]); i++)
                {
                    dt.Rows[0].Table.PrimaryKey = new DataColumn[] { dt.Columns[0] };
                    x.Add(Convert.ToDateTime(dt.Rows.Find(i)["Дата"]));
                }
                List<string> y = new List<string>();

                dt.Rows[0].Table.PrimaryKey = new DataColumn[] { dt.Columns[1] };
                if (radioButton1.Checked)
                    chart1.Series[0].Color = Color.Red;
                else if (radioButton2.Checked)
                    chart1.Series[0].Color = Color.DarkBlue;
                else if (radioButton3.Checked)
                    chart1.Series[0].Color = Color.Green;
                else if (radioButton4.Checked)
                    chart1.Series[0].Color = Color.Purple;
                try
                {
                for (int i = 0; i < x.Count; i++)
                {
                    DataRow row = dt.Rows.Find(x[i]);
                    if (radioButton5.Checked)
                    {
                        y.Add(row["Максимальная температура"].ToString());
                        chart1.Legends[0].Title = "Максимальная температура";
                        chart1.ChartAreas[0].AxisY.Title = "Максимальная температура";
                    }
                    else if (radioButton6.Checked)
                    {
                        y.Add(row["Минимальная температура"].ToString());
                        chart1.Legends[0].Title = "Минимальная температура";
                        chart1.ChartAreas[0].AxisY.Title = "Минимальная температура";
                    }
                    else if (radioButton7.Checked)
                    {
                        y.Add(row["Средняя температура"].ToString());
                        chart1.Legends[0].Title = "Средняя температура";
                        chart1.ChartAreas[0].AxisY.Title = "Средняя температура";
                    }
                }
                for (int i = 0; i < x.Count; i++)
                    chart1.Series[0].Points.AddXY(x[i].ToShortDateString(), y[i]);
                    TrandLine p = new TrandLine();
                    List<double> X = new List<double>();
                    X.Add(0);
                    List<double> ydouble = new List<double>();
                    for (int i = 0; i < y.Count; i++)
                    {
                        ydouble.Add(Convert.ToDouble(y[i]));
                        if (i != 0)
                            X.Add(X[i - 1] + 1);
                    }
                    List<double> Y = p.F(X, ydouble);
                    for (int i = 0; i < X.Count; i++)
                        chart1.Series[1].Points.AddXY(x[i].ToShortDateString(), Y[i]);

                    //ScrollBar
                    int blockSize = 31;
                    Random rand = new Random();
                     chart1.ChartAreas[0].AxisX.Minimum = 0;
                     chart1.ChartAreas[0].AxisX.Maximum = x.Count;
                     chart1.ChartAreas[0].CursorX.AutoScroll = true;
                     chart1.ChartAreas[0].AxisX.ScaleView.Zoomable = true;
                     chart1.ChartAreas[0].AxisX.ScaleView.SizeType = DateTimeIntervalType.Number;
                     int position = 0;
                     int size = blockSize;
                     chart1.ChartAreas[0].AxisX.ScaleView.Zoom(position, size);
                     chart1.ChartAreas[0].AxisX.ScrollBar.ButtonStyle = ScrollBarButtonStyles.SmallScroll;
                     chart1.ChartAreas[0].AxisX.ScaleView.SmallScrollSize = blockSize;

                    StreamReader sr = new StreamReader(@"D:\VisualStudio\Проекты\КФУ\Информатика\Лабы\Курсовая\currentuser.txt");
                    string currentuser = sr.ReadLine();
                    sr.Close();
                    Setter set = new Setter();
                    set.setLog(Convert.ToInt32(set.GetUserInfo(currentuser, "id")), "График", DateTime.Now);
                }
                catch (ArgumentOutOfRangeException)
                {
                    MessageBox.Show("Выберете отображаемые данные");
                }
            }
        }
    }
}
