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

namespace FundManagement.UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnShowHideReportPane_Click(object sender, RoutedEventArgs e)
        {
            switch (ReportPane.Visibility)
            {
                case Visibility.Visible:
                    ReportPane.Visibility = Visibility.Hidden;
                    break;

                case Visibility.Hidden:
                    ReportPane.Visibility = Visibility.Visible;
                    break;
            }
            

        }
    }
}
