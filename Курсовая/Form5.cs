using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Курсовая
{
    public partial class Form5 : Form
    {
        public Form5()
        {
            InitializeComponent();

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            double windspeed = Convert.ToDouble(textBox2.Text);
            double mm = Convert.ToDouble(textBox3.Text);
            double avrtemp = Convert.ToDouble(textBox4.Text);
            if (windspeed < 0 || mm < 0)
            {
                MessageBox.Show("Скорость ветра и количество осадков не могут быть отрицательными");
            }
            Setter set = new Setter();
            List<List<double>> input_layer = set.GetInput();
            List<double> output_layer = set.GetOutput();
            NeuralNetwork nn = new NeuralNetwork();
            List<double> pred = nn.fit(input_layer, output_layer, 10000);           
           
            double prediction = nn.predict(new List<List<double>> { new List<double> { windspeed, mm, avrtemp } });
            textBox1.Text = String.Format("{0:F1}", prediction);
            StreamReader sr = new StreamReader(@"D:\VisualStudio\Проекты\КФУ\Информатика\Лабы\Курсовая\currentuser.txt");
            string currentuser = sr.ReadLine();
            sr.Close();
            DateTime now = DateTime.Now;
            set.setLog(Convert.ToInt32(set.GetUserInfo(currentuser, "id")), "Прогноз", now);
            set.setPredict(Convert.ToInt32(set.GetUserInfo(currentuser, "id")), textBox1.Text, now);
        }

        private void Form5_Load(object sender, EventArgs e)
        {

        }
    }
}
