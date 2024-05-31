using PatoAlarmas.Models;

namespace PatoAlarmas.Views;

public partial class VehiculosPage : ContentPage
{
    private Usuario _usuario;
    public VehiculosPage(Usuario usuario)
	{
		InitializeComponent();
        _usuario = usuario;
        VehiculosListView.ItemsSource = _usuario.Vehiculos;
    }

    private async void OnAgregarVehiculoButtonClicked(object sender, EventArgs e)
    {
        var marca = await DisplayPromptAsync("Nuevo Vehículo", "Ingresa la marca del vehículo:");
        var modelo = await DisplayPromptAsync("Nuevo Vehículo", "Ingresa el modelo del vehículo:");

        if (!string.IsNullOrWhiteSpace(marca) && !string.IsNullOrWhiteSpace(modelo))
        {
            var nuevoVehiculo = new Vehiculo
            {
                Id = Guid.NewGuid().ToString(),
                Marca = marca,
                Modelo = modelo,
                UsuarioId = _usuario.Id
            };

            _usuario.Vehiculos.Add(nuevoVehiculo);
            VehiculosListView.ItemsSource = null;
            VehiculosListView.ItemsSource = _usuario.Vehiculos;

            var usuarios = await StorageHelper.LeerListaAsync<Usuario>("usuarios.json");
            var usuario = usuarios.Find(u => u.Id == _usuario.Id);
            usuario.Vehiculos.Add(nuevoVehiculo);
            await StorageHelper.GuardarListaAsync("usuarios.json", usuarios);
        }
    }

}