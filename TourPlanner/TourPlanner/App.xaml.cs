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

            SearchbarViewModel Searchbar = new SearchbarViewModel();

            HomeViewModel HomeView = new HomeViewModel();

            CreateRoutesViewModel CreateRoutesView = new CreateRoutesViewModel();

            BrowseRoutesViewModel BrowseRoutesView = new BrowseRoutesViewModel();


            var wnd = new MainWindow()
            {
                DataContext = new MainViewModel
                {
                    SearchbarInstance = Searchbar,
                    HomeViewInstance = HomeView,
                    CreateRoutesViewInstance = CreateRoutesView,
                    BrowseRoutesViewInstance = BrowseRoutesView,

                    CurrentView = HomeView,
                    CurrentHotbar = Searchbar,
                },
                // Content = { DataContext = HomeView.Content },
                // Hotbar = { DataContext = HomeView.Hotbar },
                
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
