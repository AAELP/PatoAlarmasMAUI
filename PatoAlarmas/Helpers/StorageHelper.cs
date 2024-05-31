using PatoAlarmas.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace PatoAlarmas.Helpers

{
    public class StorageHelper
    {
        public static async Task GuardarArchivoAsync<T>(string filename, T data)
        {
            var filePath = Path.Combine(FileSystem.AppDataDirectory, filename);
            using (var writer = File.CreateText(filePath))
            {
                var json = JsonSerializer.Serialize(data);
                await writer.WriteAsync(json);
            }
        }

        public static async Task<T> LeerArchivoAsync<T>(string filename)
        {
            var filePath = Path.Combine(FileSystem.AppDataDirectory, filename);
            if (File.Exists(filePath))
            {
                using (var reader = File.OpenText(filePath))
                {
                    var json = await reader.ReadToEndAsync();
                    return JsonSerializer.Deserialize<T>(json);
                }
            }
            return default(T);
        }

        public static void BorrarArchivo(string filename)
        {
            var filePath = Path.Combine(FileSystem.AppDataDirectory, filename);
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
        }

        public static async Task<List<T>> LeerListaAsync<T>(string filename)
        {
            var list = await LeerArchivoAsync<List<T>>(filename);
            return list ?? new List<T>();
        }

        public static async Task GuardarListaAsync<T>(string filename, List<T> data)
        {
            await GuardarArchivoAsync(filename, data);
        }

        public async Task AgregarUsuarioAsync(Usuario nuevoUsuario)
        {
            var usuarios = await StorageHelper.LeerListaAsync<Usuario>("usuarios.json");
            usuarios.Add(nuevoUsuario);
            await StorageHelper.GuardarListaAsync("usuarios.json", usuarios);
        }


    }
}
