using System.Windows;
using System.Windows.Controls;
using TourPlanner.DAL;
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

            CreateToursViewModel CreateToursView = new CreateToursViewModel();

            BrowseToursViewModel BrowseToursView = new BrowseToursViewModel();

            ImportToursViewModel ImportToursView = new();

            ExportToursViewModel ExportToursView = new();

            // create DAL access 

            ITourRepository tourRepository;

            // create mainviewmodel

            MainViewModel MainViewModelInstance = new MainViewModel
            {
                SearchbarInstance = Searchbar,
                HomeViewInstance = HomeView,
                CreateToursViewInstance = CreateToursView,
                BrowseToursViewInstance = BrowseToursView,
                ImportToursViewInstance = ImportToursView,
                ExportToursViewInstance = ExportToursView,

                CurrentView = HomeView,
                CurrentHotbar = Searchbar,
            };

            var wnd = new MainWindow()
            {
                DataContext = MainViewModelInstance
            };

            MainViewModelInstance.UpdateViews();
            
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
