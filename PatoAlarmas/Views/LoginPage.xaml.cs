using Microsoft.Maui.Controls;
using PatoAlarmas.Helpers;

namespace PatoAlarmas.Views
{
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
        }

        private async void OnLoginButtonClicked(object sender, EventArgs e)
        {
            string email = EmailEntry.Text;
            string password = PasswordEntry.Text;

            if (email == Config.AdminEmail && password == Config.AdminPassword)
            {
                await Navigation.PushAsync(new AdminPage());
            }
            else
            {
                await DisplayAlert("Error", "Correo o contraseña incorrectos", "OK");
            }
        }
    }
}
