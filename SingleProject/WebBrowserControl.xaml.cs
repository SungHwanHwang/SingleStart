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


            System.Windows.Forms.MessageBox.Show(finalBigCity + ", " + finalInnerCity);
        }

        public void Click_Button(object sender, RoutedEventArgs e)
        {
            try  
            {  
                this.myWeb.InvokeScript("JavaScriptFunctionWithoutParameters", finalBigCity+","+finalInnerCity);  
               
            }  
            catch (Exception ex)  
            {  
                string msg = "Could not call script: " +  
                            ex.Message +  
                            "Please click the 'Load HTML Document with Script' button to load.";  
                System.Windows.Forms.MessageBox.Show(msg);  
            }  

        }

        public void ForceClick(object sender, ExecutedRoutedEventHandler e)
        {
            btn.RaiseEvent(new RoutedEventArgs(System.Windows.Controls.Button.ClickEvent, btn));
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
