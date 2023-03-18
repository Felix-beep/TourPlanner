using System.Windows;
using System.Windows.Controls;
using TourPlanner.MVVM.Model;
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
            // create needed viewmodelobjects

            ViewModelObject HomeView = new ViewModelObject
            {
                Title = "Homepage",
                Hotbar = new SearchbarViewModel(),
                Content = new HomeViewModel(),
            };

            var wnd = new MainWindow()
            {
                DataContext = new MainViewModel
                {
                    HomeViewObject = HomeView,
                    CurrentView = HomeView,
                },
                /*
                 * Content = { DataContext = HomeView.ContentModel },
                Hotbar = { DataContext = HomeView.HotbarModel },
                */
            };

            ((MainViewModel)wnd.DataContext).UpdateViews();
            
            wnd.Show();
        }
    }
}

/* 
    <DataTemplate DataType="{x:Type viewModel:HomeViewModel}">
        <view:HomeView/>
    </DataTemplate>

    <DataTemplate DataType="{x:Type viewModel:SearchbarViewModel}">
        <view:Searchbar/>
    </DataTemplate>
*/
