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

namespace SingleProject
{
    public partial class PersonalWindow : Window
    {
        //  지역 명 배열
        string[]seoul = {"강남구","강동구","강북구","강서구","관악구","광진구","구로구","금천구","노원구","도봉구","동대문구","동작구","마포구","서대문구","서초구","성동구","성북구","송파구","양천구","영등포구","용산구","은평구","종로구","중구","중랑구"};
        string[]gangWon = {"테스트1", "테스트2","테스트3"};

        //  접속 유저명 변수
        string personalUser;
        string personalValue;
        
        public PersonalWindow(string idForPassToPersonal, string currentValue)
        {
            InitializeComponent();
            connectUser.Text = idForPassToPersonal +"님 ";
            this.personalUser = idForPassToPersonal;
            cash.Text = currentValue;
            this.personalValue = currentValue;
        }

        public void Add_Value(object sender, EventArgs e)
        {
            AddValue avalue = new AddValue(personalUser, personalValue);
            avalue.UpdatePoint += value => cash.Text = value;
            avalue.ShowDialog();
        }

        private void comboCity_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            ComboBoxItem selectedItem = (ComboBoxItem)comboCity.SelectedItem;
            string city = selectedItem.Name;
            //string city = comboCity.SelectedItem.ToString();

            switch (city)
            {
                case "GangWon":
                    comboGuGun.ItemsSource = gangWon;
                    break;
                case "GyunGi":
                    break;
                case "GyungNam":
                    break;
                case "GyungBuk":
                    break;
                case "GwangJu":
                    break;
                case "DaeGu":
                    break;
                case "DaeJeon":
                    break;
                case "Busan":
                    break;
                case "Seoul":
                    comboGuGun.ItemsSource = seoul;
                    break;
                case "Sejong":
                    break;
                case "UlSan":
                    break;
                case "Incheon":
                    break;
                case "JeonNam":
                    break;
                case "JeonBuk":
                    break;
                case "Jeju":
                    break;
                case "ChungNam":
                    break;
                case "ChungBuk":
                    break;
            }
        }

        public void Search_Map(object sender, EventArgs e)
        {
            WebBrowserControl web = new WebBrowserControl();
            web.ShowDialog();
        }

        public void Closing_Click(object sender, EventArgs e)
        {
            this.Close();
        }


    }
}
