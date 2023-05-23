using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using TourPlanner.Core;
using TourPlanner.BL;
using TourPlanner.MVVM.View;

namespace TourPlanner.MVVM.ViewModel
{
    public class MainViewModel : ObservableObject
    {
        private String _title = "TourPlanner";
        public String Title
        {
            get { return _title; }
            set
            {
                _title = value;
                OnPropertyChanged();
            }
        }

        private object _currentHotbar;
        public object CurrentHotbar
        {
            get { return _currentHotbar; }
            set
            {
                _currentHotbar = value;
                OnPropertyChanged();
            }
        }

        private object _currentView;
        public object CurrentView
        {
            get { return _currentView; }
            set
            {
                _currentView = value;
                OnPropertyChanged();
            }
        }

        public DepictedTourList TourListInstance;

        public SearchbarViewModel SearchbarInstance;


        
        public HomeViewModel HomeViewInstance;
        public ICommand SwapToHomeView { get; }


        public CreateToursViewModel CreateToursViewInstance;
        public ICommand SwapToCreateTours { get; }


        public BrowseToursViewModel BrowseToursViewInstance;
        public ICommand SwapToBrowseTours { get; }


        public ImportToursViewModel ImportToursViewInstance;
        public ICommand SwapToImportTours { get; }


        public ExportToursViewModel ExportToursViewInstance;
        public ICommand SwapToExportTours { get; }


        public TourInformationViewModel TourInformationViewInstance;

        // Behavior, UI event handlers
        public MainViewModel(IBackgroundLogic BackGroundLogic, 
                            SearchbarViewModel Searchbar, HomeViewModel HomeView, 
                            CreateToursViewModel CreateToursView, ExportToursViewModel ExportToursView,
                            BrowseToursViewModel BrowseToursView, ImportToursViewModel ImportToursView,
                            DepictedTourList TourListItem, TourInformationViewModel TourInformationView)

        {
            SearchbarInstance = Searchbar;
            HomeViewInstance = HomeView;
            CreateToursViewInstance = CreateToursView;
            BrowseToursViewInstance = BrowseToursView;
            ImportToursViewInstance = ImportToursView;
            ExportToursViewInstance = ExportToursView;
            TourInformationViewInstance = TourInformationView;

            TourListInstance = TourListItem;

            CurrentView = HomeView;
            CurrentHotbar = Searchbar;

            SwapToHomeView = new RelayCommand(param => CurrentView = HomeViewInstance);
            SwapToCreateTours = new RelayCommand(param => { CurrentView = CreateToursViewInstance; CreateToursViewInstance.OpenTour(null); } );
            SwapToBrowseTours = new RelayCommand(param => CurrentView = BrowseToursViewInstance);
            SwapToImportTours = new RelayCommand(param => CurrentView = ImportToursViewInstance);
            SwapToExportTours = new RelayCommand(param => CurrentView = ExportToursViewInstance);

            SearchbarInstance.SearchClicked += async (_, searchtext) => 
            { 
                CurrentView = BrowseToursViewInstance; 
                TourListItem.SetTours(await BackGroundLogic.FullTextSearchAsync(searchtext)); 
                TourListItem.Refresh(); 
            };
            SearchbarInstance.SwapClicked += () => { SearchbarInstance.IsOnline = BackGroundLogic.SwapOnlineMode(); };
            
            BrowseToursViewInstance.NewTourClicked += () => { CurrentView = CreateToursViewInstance; CreateToursViewInstance.OpenTour(null); };
        
            BrowseToursViewInstance.ViewClicked += (_, ID) => { CurrentView = TourInformationViewInstance; TourInformationViewInstance.OpenTour(ID); };
            BrowseToursViewInstance.EditClicked += async (_, ID) => { CurrentView = CreateToursViewInstance; CreateToursViewInstance.OpenTour(TourListItem.GetTour(ID)); await TourListItem.UpdateToursAsync(); };
            BrowseToursViewInstance.DeleteClicked += async (_, ID) => { await BackGroundLogic.DeleteTourAsync(ID); await TourListItem.UpdateToursAsync(); };

            ExportToursViewInstance.SubmitClicked += async (_, Tours) => { await BackGroundLogic.ExportToursAsync(Tours); await TourListItem.UpdateToursAsync(); };

            CreateToursViewInstance.NewTourSubmitted += async (_, Tour) => { await BackGroundLogic.CreateNewTourAsync(Tour); await TourListItem.UpdateToursAsync(); };
            CreateToursViewInstance.OldTourSubmitted += async (_, Tour) => { await BackGroundLogic.EditTourAsync(Tour); await TourListItem.UpdateToursAsync(); };

            var updateToursTask = TourListInstance.UpdateToursAsync();
            updateToursTask.Wait();
        }
    }
}
