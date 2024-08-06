using System.Windows;
using Microsoft.Extensions.DependencyInjection;
using Alexandria.LibraryApp.Data;
using Alexandria.LibraryApp.ViewModel;

namespace Alexandria.LibraryApp
{
    public partial class App : Application
    {
        private readonly ServiceProvider _serviceProvider;

        public App()
        {
            ServiceCollection services = new();
            ConfigureServices(services);
            _serviceProvider = services.BuildServiceProvider();
        }

        private void ConfigureServices(ServiceCollection services)
        {
            services.AddTransient<MainWindow>();

            services.AddTransient<MainViewModel>();
            services.AddTransient<PatronsViewModel>();
            services.AddTransient<ProductsViewModel>();

            services.AddTransient<IPatronDataProvider, PatronDataProvider>();
            services.AddTransient<IProductDataProvider, ProductDataProvider>();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var mainWindow = _serviceProvider.GetService<MainWindow>();
            mainWindow?.Show();
        }
    }
}
