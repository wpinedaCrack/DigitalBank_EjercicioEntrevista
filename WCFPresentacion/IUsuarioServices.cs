using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using WCFNegocio.Models;

namespace WCFNegocio
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de interfaz "IUsuarioServices" en el código y en el archivo de configuración a la vez.
    [ServiceContract]
    public interface IUsuarioServices
    {
        [OperationContract]
        bool GuardarUsuario(Usuario usuario);
        [OperationContract]
        bool ActualizarUsuario(Usuario usuario);
        [OperationContract]
        bool EliminarUsuario(int id);
        [OperationContract]
        Usuario ConsultarUsuario(int id);
        [OperationContract]
        //campo nombre es opcional
        List<Usuario> ListartUsuarios(string nombre);
    }
}
