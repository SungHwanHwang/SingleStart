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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;
using EncDec;
using System.Runtime.InteropServices;

namespace SingleProject
{
    public partial class Login : UserControl
    {
        //  오라클 접속
        string connectInfo;
        string DBdataSource;
        string DBuserId;
        string DBpassword;

        string value = "";

        OracleConnection conn;
        OracleCommand cmd;
        OracleDataReader reader;

        //  UserInfo
        UserInfo info = new UserInfo();

        public Login()
        {
            InitializeComponent();
            InitDBSetting();
            OracleConnection();
        }

        ~Login()
        {
            Console.WriteLine("Oracle is Closed");
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

        public void OracleIdPasswordSelection()
        {
            Security se = new Security();
            try
            {
                string SQL = "Select password, userValue from userInfo where userId = " + "'" + userId.Text + "'";
                cmd.CommandText = SQL;
                reader = cmd.ExecuteReader(System.Data.CommandBehavior.CloseConnection);

                if(reader.Read())
                {
                    if(password.Text != se.decryptAES256(reader["password"].ToString()))
                    {
                        MessageBox.Show("패스워드 오류입니다.");
                        return;    
                    }
                }
                else
                {
                    MessageBox.Show("아이디 오류입니다.");
                    return; 
                }

                value = reader["userValue"].ToString();
                MessageBox.Show("접속 성공");
                Console.WriteLine("접속 성공");
             
            }
            catch(Exception err)
            {
                Console.WriteLine(err.ToString());
            }
        }

        public void Login_Click(object sender, EventArgs e)
        {
            OracleIdPasswordSelection();

            //  새창 띄우기, 새창에서 작업하기(위치 기반 시작)
            PersonalWindow pw = new PersonalWindow(userId.Text, value);
            pw.ShowDialog();
            Window.GetWindow(this).Close();
        }

        public void Register_Click(object sender, EventArgs e)
        {
            Registration res = new Registration();
            res.ShowDialog();
        }
    }
}
