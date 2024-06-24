using Microsoft.Extensions.DependencyInjection;
using PasswordBox.MVVM.View;
using PasswordBox.Core;
using PasswordBox.MVVM.ViewModel;
using PasswordBox.Services;
using System.Windows;
using System.Globalization;

namespace PasswordBox
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly ServiceProvider _serviceProvider;

        public App()
        {
            IServiceCollection services = new ServiceCollection();

            services.AddSingleton<MainWindow>(provider => new MainWindow
            {
                DataContext = provider.GetRequiredService<MainViewModel>()
            });

            services.AddSingleton<MainViewModel>();
            services.AddSingleton<PasswordsViewModel>();
            services.AddSingleton<LoadingScreenViewModel>();
            services.AddSingleton<LoginViewModel>();
            services.AddSingleton<RegistrationViewModel>();

            services.AddSingleton<INavigationService, NavigationService>();

            services.AddSingleton<Func<Type, ViewModel>>(serviceProvider => viewModelType => (ViewModel)serviceProvider.GetRequiredService(viewModelType));

            _serviceProvider = services.BuildServiceProvider();

            CultureInfo.CurrentUICulture = new CultureInfo("en-US");
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            var mainWindow = _serviceProvider.GetRequiredService<MainWindow>();
            mainWindow.Show();
            base.OnStartup(e);
        }
    }

}
