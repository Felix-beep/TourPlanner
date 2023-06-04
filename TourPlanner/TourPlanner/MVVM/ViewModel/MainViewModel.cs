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
using TourPlanner.Models;

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

        private object _lastView = null;

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
                if (value == null) return;
                _lastView = _currentView;
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

        public CreateTourLogsViewModel CreateTourLogsViewInstance;

        // Behavior, UI event handlers
        public MainViewModel(IBackendLogic BackendLogic, 
                            SearchbarViewModel Searchbar, HomeViewModel HomeView, 
                            CreateToursViewModel CreateToursView, ExportToursViewModel ExportToursView,
                            BrowseToursViewModel BrowseToursView, ImportToursViewModel ImportToursView,
                            DepictedTourList TourListItem, TourInformationViewModel TourInformationView,
                            CreateTourLogsViewModel CreateTourLogsView)

        {
            SearchbarInstance = Searchbar;
            HomeViewInstance = HomeView;
            CreateToursViewInstance = CreateToursView;
            CreateTourLogsViewInstance = CreateTourLogsView;
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

            // Searchbar Buttons
            SearchbarInstance.SearchClicked += async (_, searchtext) => 
            { 
                CurrentView = BrowseToursViewInstance; 
                TourListItem.SetTours(await BackendLogic.FullTextSearchAsync(searchtext)); 
                TourListItem.Refresh(); 
            };
            SearchbarInstance.SwapClicked += () => { SearchbarInstance.IsOnline = BackendLogic.SwapOnlineMode(); };
            SearchbarInstance.ReturnClicked += () => { 
                CurrentView = _lastView; 
            };
            
            // TourBrowser Buttons
            BrowseToursViewInstance.CreateClicked += () => { CurrentView = CreateToursViewInstance; CreateToursViewInstance.OpenTour(null); };
        
            BrowseToursViewInstance.ViewClicked += (_, ID) => { CurrentView = TourInformationViewInstance; TourInformationViewInstance.OpenTour(ID); };
            BrowseToursViewInstance.EditClicked += async (_, ID) => { CurrentView = CreateToursViewInstance; CreateToursViewInstance.OpenTour(TourListItem.GetTour(ID)); await TourListItem.UpdateToursAsync(); };
            BrowseToursViewInstance.DeleteClicked += async (_, ID) => { await BackendLogic.DeleteTourAsync(ID); await TourListItem.UpdateToursAsync(); };

            // Tourinformation Buttons
            TourInformationViewInstance.CreateReportClicked += (_, ID) => { /* generate report*/ return; };
            TourInformationViewInstance.CreateClicked += (_, ID) => { CurrentView = CreateTourLogsViewInstance; CreateTourLogsViewInstance.OpenTour(ID, null); };

            TourInformationViewInstance.EditClicked += async (_, Args) => { CurrentView = CreateTourLogsViewInstance; CreateTourLogsViewInstance.OpenTour(Convert.ToInt32(Args.Args[0]), TourListItem.GetTourLog(Convert.ToInt32(Args.Args[0]), Convert.ToInt32(Args.Args[1]))); await TourListItem.UpdateToursAsync(); };
            TourInformationViewInstance.DeleteClicked += async (_, Args) => { await BackendLogic.DeleteTourLogAsync(Convert.ToInt32(Args.Args[0]), Convert.ToInt32(Args.Args[1])); await TourListItem.UpdateToursAsync(); };

            // HomeView Buttons
            HomeViewInstance.ViewClicked += (_, ID) => { CurrentView = TourInformationViewInstance; TourInformationViewInstance.OpenTour(ID); };

            ExportToursViewInstance.SubmitClicked += async (_, Tours) => { await BackendLogic.ExportToursAsync(Tours); await TourListItem.UpdateToursAsync(); };

            CreateToursViewInstance.NewTourSubmitted += async (_, Tour) => { await BackendLogic.CreateNewTourAsync(Tour); await TourListItem.UpdateToursAsync(); };
            CreateToursViewInstance.OldTourSubmitted += async (_, Tour) => { await BackendLogic.EditTourAsync(Tour); await TourListItem.UpdateToursAsync(); };

            CreateTourLogsViewInstance.NewTourSubmitted += async (_, Args) => { await BackendLogic.CreateNewTourLogAsync(Convert.ToInt32(Args.Args[0]), (TourLog)Args.Args[1]); await TourListItem.UpdateToursAsync(); };
            CreateTourLogsViewInstance.OldTourSubmitted += async (_, Args) => { await BackendLogic.EditTourLogAsync(Convert.ToInt32(Args.Args[0]), (TourLog)Args.Args[1]); await TourListItem.UpdateToursAsync(); };

            var updateToursTask = TourListInstance.UpdateToursAsync();
            updateToursTask.Wait();
        }
    }
}
