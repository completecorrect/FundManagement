using System;
using System.Collections.Generic;
using System.Diagnostics;
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

namespace FundManagement.UI.View
{
    /// <summary>
    /// Interaction logic for LeftPane.xaml
    /// </summary>
    public partial class LeftPane : UserControl
    {
        public LeftPane()
        {
            InitializeComponent();
        }

        private void btnAddFund_Click(object sender, RoutedEventArgs e)
        {
            if (ViewModel.HasErrors)
            {
                Trace.WriteLine("Due to errors, btnAddFund_Click returns without any action.");
                return;
            }

            ViewModel.Assets.Add(new Entity.Asset
            {
                Price = ViewModel.Price,
                Quantity = ViewModel.Quantity,
                Type = ViewModel.SelectedAssetType.Value
            });
        }
        
    }
}
