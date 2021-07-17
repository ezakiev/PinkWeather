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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form registration = new Form4();
            registration.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Authenticator aut = new Authenticator();
            try
            {
                Setter set = new Setter();
                DataSet ds = set.Shower("dbo.Журнал", null);
                DataTable dt = ds.Tables[0];

                StreamReader sr = new StreamReader(@"D:\VisualStudio\Проекты\КФУ\Информатика\Лабы\Курсовая\lastuser.txt");
                string lastuser = sr.ReadLine();
                sr.Close();
                StreamReader sre = new StreamReader(@"D:\VisualStudio\Проекты\КФУ\Информатика\Лабы\Курсовая\currentuser.txt");
                string currentuser = sre.ReadLine();
                sre.Close();

                if (lastuser != "")
                {
                    textBox1.Text = lastuser;
                    textBox2.Text = set.GetUserInfo(lastuser, "pass");
                    textBox1.Enabled = false;
                    textBox2.Enabled = false;
                    checkBox1.Checked = true;
                }
                if (textBox1.Text == "")
                    throw new Exception("Введите логин пользователя и пароль!");
                else if (textBox2.Text == "")
                    throw new Exception("Введите пароль!");
                else
                {
                    int userid = aut.SignUp(textBox1.Text, textBox2.Text);
                    if (userid == 0)
                        throw new Exception("Неверные имя пользователя или пароль.");

                    StreamWriter swr = new StreamWriter(@"D:\VisualStudio\Проекты\КФУ\Информатика\Лабы\Курсовая\currentuser.txt", false);
                    swr.WriteLine(textBox1.Text);
                    swr.Close();

                    if (lastuser == "")
                        set.setLog(Convert.ToInt32(set.GetUserInfo(textBox1.Text, "id")), "Вход", DateTime.Now);
                    else
                        set.setLog(Convert.ToInt32(set.GetUserInfo(textBox1.Text, "id")), "Плэй", DateTime.Now);

                    Hide();
                    if (checkBox1.Checked)
                    {
                        StreamWriter sw = new StreamWriter(@"D:\VisualStudio\Проекты\КФУ\Информатика\Лабы\Курсовая\lastuser.txt", false);
                        sw.WriteLine(textBox1.Text);
                        sw.Close();
                    }
                    else
                    {
                        textBox1.Text = "";
                        textBox2.Text = "";
                    }
                    
                    
                    Form ifrm = new Form3();
                    ifrm.Show();
                }
            }
            catch(Exception ex)
            {
                if (ex.Message != "В позиции 0 строка отсутствует." && ex.Message != "Ссылка на объект не указывает на экземпляр объекта.")
                    MessageBox.Show(ex.Message);
                else
                    MessageBox.Show("Неверные имя пользователя или пароль.");
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            textBox1.Text = "";
            textBox2.Text = "";
        }

        private void button3_Click(object sender, EventArgs e)
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

        private void Form1_Load(object sender, EventArgs e)
        {
            Setter set = new Setter();
            StreamReader sr = new StreamReader(@"D:\VisualStudio\Проекты\КФУ\Информатика\Лабы\Курсовая\lastuser.txt");
            string lastuser = sr.ReadLine();
            sr.Close();
            try
            {
                if (lastuser != "")
                {
                    textBox1.Text = lastuser;
                    textBox2.Text = set.GetUserInfo(lastuser, "pass");
                    textBox1.Enabled = false;
                    textBox2.Enabled = false;
                    checkBox1.Checked = true;
                }
            }
            catch
            { }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}
