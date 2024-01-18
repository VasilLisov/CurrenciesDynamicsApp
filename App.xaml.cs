using CurrenciesDynamicsApp.Commands;
using CurrenciesDynamicsApp.Interfaces;
using CurrenciesDynamicsApp.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Windows;

namespace CurrenciesDynamicsApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly IHost _host;
        public App()
        {
           this.InitializeComponent();
            _host = Host.CreateDefaultBuilder()
                .ConfigureHostConfiguration(builder => builder.AddJsonFile("appsettings.json"))
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddSingleton<IHttpService, CurrenciesHttpService>();
                    services.AddTransient<IFileService, JsonFileService>();
                    services.AddTransient<IDialogService, DialogService>();
                    services.AddTransient<DownloadFromRemoteCommand>();
                    services.AddSingleton<ApplicationViewModel>();

                    services.AddSingleton(s => new MainWindow()
                    {
                        DataContext = s.GetRequiredService<ApplicationViewModel>()
                    });
                })
                .Build();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            _host.Start();

            MainWindow = _host.Services.GetRequiredService<MainWindow>();
            MainWindow.Show();

            base.OnStartup(e);
        }
    }
}
