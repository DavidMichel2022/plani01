using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SistemaPlanificacion.BLL.Interfaces;
using Firebase.Auth;
using Firebase.Storage;
using SistemaPlanificacion.Entity;
using SistemaPlanificacion.DAL.Interfaces;

namespace SistemaPlanificacion.BLL.Implementacion
{
    public class FireBaseService : IFireBaseService
    {
        private readonly IGenericRepository<Configuracion> _repositorio;
        public FireBaseService(IGenericRepository<Configuracion> repositorio)
        {
            _repositorio = repositorio;
        }
        public async Task<string> SubirStorage(Stream StreamArchivo, string CarpetaDestino, string NombreArchivo)
        {
            string urlImagen = "";

            try
            {
                IQueryable<Configuracion> query = await _repositorio.Consultar(c => c.Recurso.Equals("FireBase_Storage"));
                Dictionary<string, string> Config = query.ToDictionary(keySelector: c => c.Propiedad, elementSelector: c => c.Valor);

                var auth=new FirebaseAuthProvider(new FirebaseConfig(Config["api_key"]));
                var a = await auth.SignInWithEmailAndPasswordAsync(Config["email"], Config["clave"]);
                var cancellation = new CancellationTokenSource();

                var task = new FirebaseStorage(
                    Config["ruta"],
                    new FirebaseStorageOptions
                    {
                        AuthTokenAsyncFactory = () => Task.FromResult(a.FirebaseToken),
                        ThrowOnCancel = true
                    })
                    .Child(Config[CarpetaDestino])
                    .Child(NombreArchivo)
                    .PutAsync(StreamArchivo, cancellation.Token);

                urlImagen = await task;
            }
            catch
            {
                urlImagen = "";
            }
            return urlImagen;
        }

        public async Task<bool> EliminarStorage(string CarpetaDestino, string NombreArchivo)
        {
            try
            {
                IQueryable<Configuracion> query = await _repositorio.Consultar(c => c.Recurso.Equals("FireBase_Storage"));
                Dictionary<string, string> Config = query.ToDictionary(keySelector: c => c.Propiedad, elementSelector: c => c.Valor);
                var auth = new FirebaseAuthProvider(new FirebaseConfig(Config["api_key"]));
                var a = await auth.SignInWithEmailAndPasswordAsync(Config["email"], Config["clave"]);
                var cancellation = new CancellationTokenSource();

                var task = new FirebaseStorage(
                    Config["ruta"],
                    new FirebaseStorageOptions
                    {
                        AuthTokenAsyncFactory = () => Task.FromResult(a.FirebaseToken),
                        ThrowOnCancel = true
                    })
                    .Child(Config[CarpetaDestino])
                    .Child(NombreArchivo)
                    .DeleteAsync();

                await task;
                return true;

            }
            catch
            {
                return false;
            }
        }
    }
}
