using PatoAlarmas.Models;

namespace PatoAlarmas.Views;

public partial class CitasPage : ContentPage
{
    private Usuario _usuario;

    public CitasPage(Usuario usuario)
	{
		InitializeComponent();
        _usuario = usuario;
        VehiculoPicker.ItemsSource = _usuario.Vehiculos.Select(v => v.Marca + " " + v.Modelo).ToList();

    }
    private async void OnAgendarButtonClicked(object sender, EventArgs e)
    {
        var vehiculoSeleccionado = _usuario.Vehiculos[VehiculoPicker.SelectedIndex];
        var nuevaCita = new Cita
        {
            Id = Guid.NewGuid().ToString(),
            UsuarioId = _usuario.Id,
            VehiculoId = vehiculoSeleccionado.Id,
            Fecha = FechaPicker.Date
        };

        var citas = await StorageHelper.LeerListaAsync<Cita>("citas.json");
        citas.Add(nuevaCita);
        await StorageHelper.GuardarListaAsync("citas.json", citas);

        await DisplayAlert("Cita Agendada", "Tu cita ha sido agendada exitosamente.", "OK");
        await Navigation.PopAsync();
    }




}