using Microsoft.Extensions.DependencyInjection;
using System;
using TourPlanner.BL;
using TourPlanner.DAL;
using TourPlanner.MVVM.ViewModel;

namespace TourPlanner
{
    public class IoCContainerConfig
    {
        private readonly IServiceProvider serviceProvider;

        public IoCContainerConfig()
        {
            var services = new ServiceCollection();

            var repo = new APITourRepository();
            repo.Connect(new Uri("http://localhost:5000/"));

            // create all layers
            services.AddSingleton<ITourRepository>(repo);
            services.AddSingleton<IBackgroundLogic, BackgroundLogic>();
            
            // create all viewmodels
            services.AddSingleton<SearchbarViewModel>();
            services.AddSingleton<HomeViewModel>();
            services.AddSingleton<CreateToursViewModel>();
            services.AddSingleton<BrowseToursViewModel>();
            services.AddSingleton<ImportToursViewModel>();
            services.AddSingleton<ExportToursViewModel>();
            services.AddSingleton<MainViewModel>();

            services.AddSingleton<TourList>();

            serviceProvider = services.BuildServiceProvider();
        }

        public SearchbarViewModel SearchBarInstance => serviceProvider.GetRequiredService<SearchbarViewModel>();

        public HomeViewModel HomeViewInstance => serviceProvider.GetRequiredService<HomeViewModel>();

        public CreateToursViewModel CreateToursInstance => serviceProvider.GetRequiredService<CreateToursViewModel>();

        public BrowseToursViewModel BrowseToursInstance => serviceProvider.GetRequiredService<BrowseToursViewModel>();

        public ImportToursViewModel ImportToursInstance => serviceProvider.GetRequiredService<ImportToursViewModel>();

        public ExportToursViewModel ExportToursInstance => serviceProvider.GetRequiredService<ExportToursViewModel>();

        public MainViewModel MainViewInstance => serviceProvider.GetRequiredService<MainViewModel>();
    }
}
