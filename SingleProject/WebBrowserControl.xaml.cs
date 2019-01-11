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
    public partial class WebBrowserControl : Window
    {
        public WebBrowserControl()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                myWeb.Source = new Uri("http://localhost:80");
            }
            catch(Exception err)
            {
                MessageBox.Show(err.ToString());
            }
        }

        private void Button_Back(object sender, RoutedEventArgs e)
        {
            try
            {
                myWeb.GoBack();

            }
            catch(Exception err)
            {
                MessageBox.Show(err.ToString());
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
                MessageBox.Show(err.ToString());
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
                MessageBox.Show(err.ToString());
            }
        }
    }
}
