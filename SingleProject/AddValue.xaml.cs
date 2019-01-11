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
using System.Runtime.InteropServices;

namespace SingleProject
{
    public partial class AddValue : Window
    {

        public event Action<string> UpdatePoint;

        //  Value 변수
        int currentValue = 0;
        int finalValue = 0;

        //  아이디 변수
        string idForAddMoney ="";

        //  오라클 변수
        OracleConnection conn;
        OracleCommand cmd;

        string connectInfo;
        string DBdataSource;
        string DBuserId;
        string DBpassword;

        public AddValue(string id, string value)
        {
            InitializeComponent();
            InitDBSetting();
            OracleConnection();
            //receiveId.Text = id;
            this.idForAddMoney = id;
            this.currentValue = Convert.ToInt32(value);
            //receiveId.Text = currentValue.ToString();
        }

        ~AddValue()
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

        public void InitDBSetting()
        {
            string settingPath = @"C:\C\SingleProject\Login.ini";
            string dbsection = "DBSETUP";
            DBdataSource = ReadIniFile(dbsection, "data Source", settingPath);
            DBuserId = ReadIniFile(dbsection, "User Id", settingPath);
            DBpassword = ReadIniFile(dbsection, "password", settingPath);
            connectInfo = "data Source=" + DBdataSource + ";" + "User Id=" + DBuserId + ";" + "password=" + DBpassword;
        }

        public void Add_Money(object sender, EventArgs e)
        {
            finalValue = currentValue + Convert.ToInt32(writeValue.Text);

            try
            {
                string SQL = "Update userInfo set userValue = " + finalValue + "where userId = " + "'" + idForAddMoney + "'";
                cmd.CommandText = SQL;
                cmd.ExecuteNonQuery();
                MessageBox.Show("충전완료(잔액 :" + finalValue + ")");

                UpdatePoint(finalValue.ToString());
                
            }
            catch(Exception err)
            {
                Console.WriteLine(err.ToString());
            }
        }
        
        public void Data2()
        {

        }

        private void closing_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
