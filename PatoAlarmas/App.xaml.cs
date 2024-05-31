using Microsoft.Maui.Controls;

namespace PatoAlarmas
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new AppShell();
            Routing.RegisterRoute(nameof(Views.LoginPage), typeof(Views.LoginPage));
            Routing.RegisterRoute(nameof(Views.AdminPage), typeof(Views.AdminPage));

            // Navegar a la página de login al iniciar
            Shell.Current.GoToAsync("//LoginPage");
        }
    }
}
