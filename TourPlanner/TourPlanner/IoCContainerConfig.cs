using Microsoft.Extensions.DependencyInjection;
using System;
using TourPlanner.BL;
using TourPlanner.Core;
using TourPlanner.DAL;
using TourPlanner.MVVM.View;
using TourPlanner.MVVM.ViewModel;

namespace TourPlanner
{
    public class IoCContainerConfig
    {
        private readonly IServiceProvider serviceProvider;

        public IoCContainerConfig()
        {
            var services = new ServiceCollection();

            // create all layers
            services.AddSingleton<MemoryTourRepository>(new MemoryTourRepository());
            services.AddSingleton<APITourRepository>(new  APITourRepository());
            services.AddSingleton<ConnectionModeFactory>();
            services.AddSingleton<IBackendLogic, BackendLogic>();
            
            // create all viewmodels
            services.AddSingleton<SearchbarViewModel>();
            services.AddSingleton<HomeViewModel>();
            services.AddSingleton<CreateToursViewModel>();
            services.AddSingleton<CreateTourLogsViewModel>();
            services.AddSingleton<BrowseToursViewModel>();
            services.AddSingleton<ImportToursViewModel>();
            services.AddSingleton<ExportToursViewModel>();
            services.AddSingleton<MainViewModel>();
            services.AddSingleton<TourInformationViewModel>();

            services.AddSingleton<DepictedTourList>();

            serviceProvider = services.BuildServiceProvider();
        }

        public SearchbarViewModel SearchBarInstance => serviceProvider.GetRequiredService<SearchbarViewModel>();

        public HomeViewModel HomeViewInstance => serviceProvider.GetRequiredService<HomeViewModel>();

        public CreateToursViewModel CreateToursInstance => serviceProvider.GetRequiredService<CreateToursViewModel>();

        public CreateTourLogsViewModel CreateTourLogsInstance => serviceProvider.GetRequiredService<CreateTourLogsViewModel>();

        public BrowseToursViewModel BrowseToursInstance => serviceProvider.GetRequiredService<BrowseToursViewModel>();

        public ImportToursViewModel ImportToursInstance => serviceProvider.GetRequiredService<ImportToursViewModel>();

        public ExportToursViewModel ExportToursInstance => serviceProvider.GetRequiredService<ExportToursViewModel>();

        public TourInformationViewModel TourInformationInstance => serviceProvider.GetRequiredService<TourInformationViewModel>();

        public MainViewModel MainViewInstance => serviceProvider.GetRequiredService<MainViewModel>();
    }
}
