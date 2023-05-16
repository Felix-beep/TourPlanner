using System;
using System.Collections.Generic;
using System.DirectoryServices.ActiveDirectory;
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
using TourPlanner.MVVM.ViewModel;

namespace TourPlanner.MVVM.View
{
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class ExportToursView : UserControl
    {
        public ExportToursView()
        {
            InitializeComponent();
        }

        // from https://stackoverflow.com/a/55313871
        //
        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.DataContext == null) return;
            ((ExportToursViewModel)this.DataContext).SelectedItems = ExportDataGrid.SelectedItems;
        }
    }
}
