using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;
using System.Data.Common;
using System.Data.SqlClient;
using System.Runtime.InteropServices;
using EncDec;

namespace SingleProject
{
    public partial class Registration : Window
    {
        //  암호화 복호화 객체
        Security se = new Security();

        //  For Oracle Connection
        string connectInfo;
        string DBdataSource;
        string DBuserId;
        string DBpassword;

        OracleConnection conn;
        OracleCommand cmd;
        OracleDataReader reader;

        //  UserInfo 객체
        UserInfo info = new UserInfo();
        List<UserInfo> ui;


        public Registration()
        {
            InitializeComponent();
            InitDBSetting();
            OracleConnection();
            OracleSelection();
        }
        
        ~Registration()
        {
            Console.WriteLine("Oracle is closed");
            conn.Close();
        }

        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filePath);

        public string ReadIniFile(string section, string key, string path)
        {
            StringBuilder sb = new StringBuilder(255);
            GetPrivateProfileString(section, key, "", sb, sb.Capacity, path);

            return sb.ToString();
        }

        public void InitDBSetting()
        {
            string settingPath = @"C:\C\SingleProject\Login.ini";
            string dbsection = "DBSETUP";
            DBdataSource = ReadIniFile(dbsection, "data Source", settingPath);
            DBuserId = ReadIniFile(dbsection, "User Id", settingPath);
            DBpassword = ReadIniFile(dbsection, "password", settingPath);

            connectInfo = "data Source=" + DBdataSource + ";" + "User Id=" + DBuserId + ";" + "password=" + DBpassword;
        }

        public void OracleConnection()
        {
            try
            {
                conn = new OracleConnection(connectInfo);
                cmd = new OracleCommand();
                conn.Open();
                cmd.Connection = conn;
            }
            catch(Exception err)
            {
                Console.WriteLine(err.ToString());
            }
        }

        public void OracleSelection()
        {
            try
            {
                string SQL = "Select userId, email from userInfo";
                cmd.CommandText = SQL;
                reader = cmd.ExecuteReader(System.Data.CommandBehavior.CloseConnection);

                ui = new List<UserInfo>();
                while(true)
                {
                    if (reader.Read() == false) break;
                    ui.Add(new UserInfo()
                    {
                        UserId = reader.GetString(0),
                        Email = reader.GetString(1)
                    });
                }
            }
            catch(Exception err)
            {
                Console.WriteLine(err.ToString());
            }
        }

        public bool CheckId()
        {

            if (userId.Text.Equals(""))
            {
                
                MessageBox.Show("ID를 입력해주세요");
                return false;
            }

            if(ui.Any(x=>x.UserId.Equals(userId.Text)))
            {
                MessageBox.Show("이미 존재하는 아이디입니다.");
                return false;
            }

            info.UserId = userId.Text;
            return true;
        }

        public bool CheckPassword()
        {
            if(password.Text.Equals(""))
            {
                MessageBox.Show("Password를 입력해주세요");
                return false;
            }

            if(password.Text!=repPassword.Text)
            {
                MessageBox.Show("재입력 Passwrod와 일치하지 않습니다");
                return false; 
            }

            se.encryptAES256(password.Text);

            info.Passwrod = se.encryptAES256(password.Text);
            return true;
        }

        public bool CheckName()
        {
            if (userName.Text.Equals(""))  
                  
            {
                MessageBox.Show("이름을 입력해주세요");
                return false;
            }

            info.UserName = userName.Text;
            return true;
        }

        public bool CheckEmail()
        {
            if(email.Text.Equals(""))
            {
                MessageBox.Show("이메일을 입력해주세요");
                return false;
            }

            if(ui.Any(x=>x.Email.Equals(email.Text)))
            {
                MessageBox.Show("이미 존재하는 이메일입니다.");
                return false;
            }
            
            if(!(email.Text.Contains("@")))
            {
                MessageBox.Show("이메일 형식이 아닙니다.");
                return false;
            }

            info.Email = email.Text;
            return true;
        }

        public bool CheckBirth()
        {
            int i=0;
            if(userBirth.Text.Count() != 8)
            {
                MessageBox.Show("생일은 8자리로 입력해주세요( ex) 19881009)");
                return false;
            }

            if(!(int.TryParse(userBirth.Text, out i)))
            {
                MessageBox.Show("생일로 사용할 수 없는 숫자가 입력되었습니다.");
                return false;
            }

            info.UserBirth = userBirth.Text;
            return true;
        }

        public void Finish_Register(object sender, EventArgs e)
        {
            bool id = CheckId();
            if(!id)
            {
                userId.Focus();
                return;
            }

            bool pass = CheckPassword();
            if(!pass)
            {
                password.Focus();
                return;
            }

            bool name = CheckName();
            if(!name)
            {
                userName.Focus();
                return;
            }

            bool mail = CheckEmail();
            if(!mail)
            {
                email.Focus();
                return;
            }

            bool birth = CheckBirth();
            if(!birth)
            {
                userBirth.Focus();
                return;
            }

            try
            {
                string SQL = "INSERT INTO USERINFO(userId, password, userName, email, userBirth) Values (" +"'" + info.UserId + "'," + "'" + info.Passwrod + "'," +"'" + info.UserName + "'," +"'" + info.Email + "'," +"'" + info.UserBirth + "'" + ")";
                cmd.CommandText = SQL;

                cmd.ExecuteNonQuery();
                MessageBox.Show("가입 완료");
                Close();
            }
            catch(Exception err)
            {
                Console.WriteLine(err.ToString());
            }
        }

        public void Close_Window(object sender, EventArgs e)
        {
            Close();
        }
    }
}
