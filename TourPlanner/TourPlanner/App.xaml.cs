using System.Windows;
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
            // create needed viewmodels

            var wnd = new MainWindow
            {
                DataContext = new MainViewModel(),
            };

            wnd.Show();
        }
    }
}
