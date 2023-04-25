using System.Windows;
using System.Windows.Controls;
using TourPlanner.BL;
using TourPlanner.MVVM.View;
using TourPlanner.MVVM.ViewModel;

namespace TourPlanner
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            var ioCConfig = (IoCContainerConfig)Application.Current.Resources["IoCConfig"];


            var wnd = new MainWindow()
            {
                DataContext = ioCConfig.MainViewInstance
            };
            
            wnd.Show();
        }
    }
}
