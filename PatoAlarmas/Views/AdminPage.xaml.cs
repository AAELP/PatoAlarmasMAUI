using System;
using System.Linq;
using System.Threading.Tasks;
using PatoAlarmas.Models;
using PatoAlarmas.Helpers;
using Microsoft.Maui.Controls;

namespace PatoAlarmas.Views
{
    public partial class AdminPage : TabbedPage
    {
        public AdminPage()
        {
            InitializeComponent();
            CargarDatos();
        }

        private async void CargarDatos()
        {
            UsuariosListView.ItemsSource = await Helpers.StorageHelper.LeerListaAsync<Usuario>("usuarios.json");
            VehiculosListView.ItemsSource = await Helpers.StorageHelper.LeerListaAsync<Vehiculo>("vehiculos.json");
            CitasListView.ItemsSource = await Helpers.StorageHelper.LeerListaAsync<Cita>("citas.json");
        }

        private async void OnAgregarUsuarioButtonClicked(object sender, EventArgs e)
        {
            var nombre = await DisplayPromptAsync("Nuevo Usuario", "Ingresa el nombre del usuario:");
            var correo = await DisplayPromptAsync("Nuevo Usuario", "Ingresa el correo del usuario:");

            if (!string.IsNullOrWhiteSpace(nombre) && !string.IsNullOrWhiteSpace(correo))
            {
                var nuevoUsuario = new Usuario { Id = Guid.NewGuid().ToString(), Nombre = nombre, Correo = correo };
                var usuarios = await Helpers.StorageHelper.LeerListaAsync<Usuario>("usuarios.json");
                usuarios.Add(nuevoUsuario);
                await Helpers.StorageHelper.GuardarListaAsync("usuarios.json", usuarios);
                UsuariosListView.ItemsSource = usuarios;
            }
        }

        private async void OnUsuarioSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null) return;

            var usuario = (Usuario)e.SelectedItem;
            bool eliminar = await DisplayAlert("Eliminar Usuario", $"¿Deseas eliminar a {usuario.Nombre}?", "Sí", "No");
            if (eliminar)
            {
                var usuarios = await Helpers.StorageHelper.LeerListaAsync<Usuario>("usuarios.json");
                usuarios.Remove(usuario);
                await Helpers.StorageHelper.GuardarListaAsync("usuarios.json", usuarios);
                UsuariosListView.ItemsSource = usuarios;
            }

            ((ListView)sender).SelectedItem = null;
        }

        private async void OnAgregarVehiculoButtonClicked(object sender, EventArgs e)
        {
            var marca = await DisplayPromptAsync("Nuevo Vehículo", "Ingresa la marca del vehículo:");
            var modelo = await DisplayPromptAsync("Nuevo Vehículo", "Ingresa el modelo del vehículo:");

            if (!string.IsNullOrWhiteSpace(marca) && !string.IsNullOrWhiteSpace(modelo))
            {
                var nuevoVehiculo = new Vehiculo { Id = Guid.NewGuid().ToString(), Marca = marca, Modelo = modelo };
                var vehiculos = await Helpers.StorageHelper.LeerListaAsync<Vehiculo>("vehiculos.json");
                vehiculos.Add(nuevoVehiculo);
                await Helpers.StorageHelper.GuardarListaAsync("vehiculos.json", vehiculos);
                VehiculosListView.ItemsSource = vehiculos;
            }
        }

        private async void OnVehiculoSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null) return;

            var vehiculo = (Vehiculo)e.SelectedItem;
            bool eliminar = await DisplayAlert("Eliminar Vehículo", $"¿Deseas eliminar el vehículo {vehiculo.Marca} {vehiculo.Modelo}?", "Sí", "No");
            if (eliminar)
            {
                var vehiculos = await Helpers.StorageHelper.LeerListaAsync<Vehiculo>("vehiculos.json");
                vehiculos.Remove(vehiculo);
                await Helpers.StorageHelper.GuardarListaAsync("vehiculos.json", vehiculos);
                VehiculosListView.ItemsSource = vehiculos;
            }

            ((ListView)sender).SelectedItem = null;
        }

        private async void OnAgregarCitaButtonClicked(object sender, EventArgs e)
        {
            var usuarios = await Helpers.StorageHelper.LeerListaAsync<Usuario>("usuarios.json");
            var usuario = await DisplayActionSheet("Selecciona un usuario", "Cancelar", null, usuarios.Select(u => u.Nombre).ToArray());

            if (usuario != null && usuario != "Cancelar")
            {
                var vehiculos = await Helpers.StorageHelper.LeerListaAsync<Vehiculo>("vehiculos.json");
                var vehiculo = await DisplayActionSheet("Selecciona un vehículo", "Cancelar", null, vehiculos.Select(v => v.Marca + " " + v.Modelo).ToArray());

                if (vehiculo != null && vehiculo != "Cancelar")
                {
                    var fecha = await DisplayPromptAsync("Nueva Cita", "Ingresa la fecha de la cita (YYYY-MM-DD):");
                    if (!string.IsNullOrWhiteSpace(fecha) && DateTime.TryParse(fecha, out DateTime fechaCita))
                    {
                        var vehiculoSeleccionado = vehiculos.First(v => v.Marca + " " + v.Modelo == vehiculo);
                        var usuarioSeleccionado = usuarios.First(u => u.Nombre == usuario);
                        var nuevaCita = new Cita { Id = Guid.NewGuid().ToString(), UsuarioId = usuarioSeleccionado.Id, VehiculoId = vehiculoSeleccionado.Id, Fecha = fechaCita };
                        var citas = await Helpers.StorageHelper.LeerListaAsync<Cita>("citas.json");
                        citas.Add(nuevaCita);
                        await Helpers.StorageHelper.GuardarListaAsync("citas.json", citas);
                        CitasListView.ItemsSource = citas;
                    }
                }
            }
        }

        private async void OnCitaSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null) return;

            var cita = (Cita)e.SelectedItem;
            bool eliminar = await DisplayAlert("Eliminar Cita", $"¿Deseas eliminar la cita del {cita.Fecha.ToShortDateString()}?", "Sí", "No");
            if (eliminar)
            {
                var citas = await Helpers.StorageHelper.LeerListaAsync<Cita>("citas.json");
                citas.Remove(cita);
                await Helpers.StorageHelper.GuardarListaAsync("citas.json", citas);
                CitasListView.ItemsSource = citas;
            }

            ((ListView)sender).SelectedItem = null;
        }
    }
}
