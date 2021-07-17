using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HtmlAgilityPack;
using System.Net;
using System.IO;
using System.Xml.Serialization;

namespace Курсовая
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBox1.Text == "")
                    throw new Exception("Введите ссылку сайта с данными");
                string filexml = @"D:\VisualStudio\Проекты\КФУ\Информатика\Лабы\Курсовая\kop.txt";
                string url = textBox1.Text;
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8);
                string result = reader.ReadToEnd();
                StreamWriter sw = new StreamWriter(filexml);
                sw.Write(filexml);
                sw.Close();
                Setter setter = new Setter();
                dataGridView1.DataSource = setter.Set(result, "dbo.Погода").Tables[0];
                StreamReader sr = new StreamReader(@"D:\VisualStudio\Проекты\КФУ\Информатика\Лабы\Курсовая\currentuser.txt");
                string currentuser = sr.ReadLine();
                sr.Close();
                Setter set = new Setter();
                set.setLog(Convert.ToInt32(set.GetUserInfo(currentuser, "id")), "Показать", DateTime.Now);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form form = new Form2();
            form.Show();
        }

        private void Form3_FormClosed(object sender, FormClosedEventArgs e)
        {
            StreamReader sr = new StreamReader(@"D:\VisualStudio\Проекты\КФУ\Информатика\Лабы\Курсовая\currentuser.txt");
            string currentuser = sr.ReadLine();
            sr.Close();
            Setter set = new Setter();
            DataSet ds = set.Shower("dbo.Журнал", "[Дата и Время]");
            DataTable dt = ds.Tables[0];
            int trueid = Convert.ToInt32(set.GetUserInfo(currentuser, "id"));
            if (dt.Rows[dt.Rows.Count - 1]["Действие"].ToString() != "Конец сессии")
            {
                set.setLog(trueid, "Пауза", DateTime.Now);
                Form ifrm = Application.OpenForms[0];
                ifrm.Show();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form predict = new Form5();
            predict.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            StreamReader sr = new StreamReader(@"D:\VisualStudio\Проекты\КФУ\Информатика\Лабы\Курсовая\currentuser.txt");
            string currentuser = sr.ReadLine();
            sr.Close();
            Setter set = new Setter();
            set.setLog(Convert.ToInt32(set.GetUserInfo(currentuser, "id")), "Выход", DateTime.Now);
            StreamWriter swr = new StreamWriter(@"D:\VisualStudio\Проекты\КФУ\Информатика\Лабы\Курсовая\lastuser.txt", false);
            swr.WriteLine("");
            swr.Close();
            Application.Restart();
        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {

        }
    }
}
