using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data;

namespace Курсовая
{
    class Authenticator
    {
        public void Registrator(string username, string pass1, string pass2, string name, string sername)
        {
            try
            {
                Setter set = new Setter();
                if (username == "" || pass1 == "" || pass2 == "" || name == "" || sername == "")
                    throw new Exception("Заполните все обязателбные поля!");
                if (pass2 != pass1)
                    throw new Exception("Пароли не совпадают!");
                string login = set.GetUserInfo(username, "user");
                if (login == username)
                    throw new Exception("Такое имя пользователя уже существует");               
                
                set.setUser("dbo.Пользователи", username, pass1, name, sername);
                Form regform = Application.OpenForms[Application.OpenForms.Count];
                regform.Close();
                set.setLog(Convert.ToInt32(set.GetUserInfo(username, "id")), "Регистрация", DateTime.Now);
                Form ifrm = Application.OpenForms[0];
                ifrm.Show();
            }
            catch (Exception ex)
            {
                    if (ex.Message != "В позиции 0 строка отсутствует." && ex.Message != "Ссылка на объект не указывает на экземпляр объекта.")
                        MessageBox.Show(ex.Message);
                    else
                    {
                        Setter set = new Setter();
                        set.setUser("dbo.Пользователи", username, pass1, name, sername);
                        Form regform = Application.OpenForms[Application.OpenForms.Count- 1];
                        regform.Close();
                        set.setLog(Convert.ToInt32(set.GetUserInfo(username, "id")), "Регистрация", DateTime.Now);
                        Form ifrm = Application.OpenForms[0];
                        ifrm.Show();
                    }
            }
        }

        public int SignUp(string username, string pass)
        {
            Setter set = new Setter();
            string truepass = set.GetUserInfo(username, "pass");
            if (truepass == pass)
            {
                return Convert.ToInt32(set.GetUserInfo(username, "id"));
            }
            else
                return 0;
        }
    }
}
