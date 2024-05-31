using PatoAlarmas.Models;

namespace PatoAlarmas.Views;

public partial class UserPage : ContentPage
{
    private Usuario _usuario;
    public UserPage(Usuario usuario)
	{
		InitializeComponent();
        _usuario = usuario;

    }
    private async void OnAgendarCitaButtonClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new CitasPage(_usuario));
    }

    private async void OnMisVehiculosButtonClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new VehiculosPage(_usuario));
    }
}