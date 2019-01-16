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
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Windows.Forms;
using System.Windows.Navigation;

namespace SingleProject
{
    public partial class WebBrowserControl : Window
    {
        string finalBigCity = "";
        string finalInnerCity = "";
        public WebBrowserControl(string passBigCity, string passInnerCity)
        {
            InitializeComponent();
            finalBigCity = passBigCity;
            finalInnerCity = passInnerCity;

            try
            {
                this.myWeb.Source = new Uri("http://localhost:80/index.html?" + "outerCity=" + passBigCity + "&" + "innerCity=" + passInnerCity);
            }
            catch (Exception err)
            {
                System.Windows.Forms.MessageBox.Show(err.ToString());
            }

           // System.Windows.Forms.MessageBox.Show(finalBigCity + ", " + finalInnerCity);
        }

        private void Button_Back(object sender, RoutedEventArgs e)
        {
            try
            {
                myWeb.GoBack();

            }
            catch(Exception err)
            {
                System.Windows.Forms.MessageBox.Show(err.ToString());
            }
        }

        private void Button_Forward(object sender, RoutedEventArgs e)
        {
            try
            {
                myWeb.GoForward();

            }
            catch (Exception err)
            {
                System.Windows.Forms.MessageBox.Show(err.ToString());
            }
        }

        private void Button_Go(object sender, RoutedEventArgs e)
        {
            try
            {
                myWeb.Source = new Uri("http://" + address.Text);
            }
            catch(Exception err)
            {
                System.Windows.Forms.MessageBox.Show(err.ToString());
            }
        }
    }
}
