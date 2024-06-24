using Antlr.Runtime.Misc;
using EntidadesNegocio;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PresentacionWeb.Models;
using PresentacionWeb.ServiceWCFNegocio;
using PresentacionWeb.ViewModels;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Script.Serialization;

namespace PresentacionWeb.Controllers
{
    public class UsuariosController : Controller
    {
        ServiceWCFNegocio.UsuarioServicesClient clientWCF = null;
        public string url_Api = null;
        Usuario usuarioWCF = null;
        public static List<Usuario> usuariosReporte = null;
        public UsuariosController()
        {
            clientWCF = new ServiceWCFNegocio.UsuarioServicesClient();
            url_Api = ConfigurationManager.AppSettings["API_REST"].ToString();
            usuarioWCF = new Usuario();
        }

        // GET: Usuarios
        public ActionResult ListaUsuarios(int pagina = 1)
        {            
            int cantidadRegistrosPorPagina = 10;             

            List<Usuario> listaUsuarios = clientWCF.ListartUsuarios("")
                .Skip((pagina - 1) * cantidadRegistrosPorPagina)
                .Take(cantidadRegistrosPorPagina).ToList();

            usuariosReporte = listaUsuarios;
            int totalDeRegistros = clientWCF.ListartUsuarios("").Count();

            var modelo = new IndexViewModel();
            modelo.Usuarios = listaUsuarios;
            modelo.PaginaActual = pagina;
            modelo.TotalDeRegistros = totalDeRegistros;
            modelo.RegistrosPorPagina = cantidadRegistrosPorPagina;
            modelo.ValoresQueryString = new RouteValueDictionary();
            //modelo.ValoresQueryString["edad"] = edad;          

            return View(modelo);
        }


        [HttpPost]
        public ActionResult ListaUsuarios(int pagina = 1,string buscar="")
        {            
            int cantidadRegistrosPorPagina = 10;
            string outmensaje = string.Empty;
            string contenidoReporte = "Nombre|Email|Sexo\n";

            string botonPresionado = Request.Form["GenerarReporte"];

            List<Usuario> listaUsuarios = clientWCF.ListartUsuarios(buscar)
               .Skip((pagina - 1) * cantidadRegistrosPorPagina)
               .Take(cantidadRegistrosPorPagina).ToList();

            usuariosReporte = listaUsuarios;

            int totalDeRegistros = clientWCF.ListartUsuarios(buscar).Count();

            var modelo = new IndexViewModel();
            modelo.Usuarios = listaUsuarios;
            modelo.PaginaActual = pagina;
            modelo.TotalDeRegistros = totalDeRegistros;
            modelo.RegistrosPorPagina = cantidadRegistrosPorPagina;
            modelo.ValoresQueryString = new RouteValueDictionary();
            //modelo.ValoresQueryString["edad"] = edad;
            
            if (botonPresionado != null)
            {
                if (usuariosReporte.Count > 0)
                {
                    foreach (var item in usuariosReporte)
                    {
                        contenidoReporte += item.Nombre + "|" + item.Email + "|" + item.Sexo + "\n";
                    }

                    GenerarReporte("archivoReporte.txt", contenidoReporte, out outmensaje);
                }
                else
                {
                    ViewBag.ErrorMessage = "No hay datos a Generar";
                }

                return View(modelo);
            }           

            return View(modelo);
        }

        public ActionResult Guardar()
        {
            List<SelectListItem> Generos = new List<SelectListItem>
            {
                new SelectListItem { Value = "1", Text = "M" },
                new SelectListItem { Value = "2", Text = "F" }
            };

            ViewData["Generos"] = Generos;

            return View();
        }

        [HttpPost]
        public ActionResult Guardar(UsuarioModel usuarioModls)
        {

            if (!ModelState.IsValid)
                return View();

            usuarioWCF.Nombre = usuarioModls.Nombre;
            usuarioWCF.FechaNacimiento = (DateTime)usuarioModls.FechaNacimiento;
            usuarioWCF.Sexo = usuarioModls.Sexo;
            usuarioWCF.Email = usuarioModls.Email;
            usuarioWCF.Passworld = (string.IsNullOrEmpty(usuarioModls.Passworld)) ? "admin123" : usuarioModls.Passworld;

            var respuesta = clientWCF.GuardarUsuario(usuarioWCF);

            if (respuesta)
            {
                string userId =  (string)Session["userId"];
                GuardarLog("Guardar", "Insertar", Int32.Parse(userId));
                return RedirectToAction("ListaUsuarios", "Usuarios");
            }                
          
            return View();
        }
        public ActionResult Editar(int Id)
        {
            //METODO SOLO DEVUELVE LA VISTA
            var result = clientWCF.ConsultarUsuario(Id);

            UsuarioModel usuarioModel = new UsuarioModel();
            usuarioModel.Id = result.Id;
            usuarioModel.Nombre = result.Nombre;
            usuarioModel.FechaNacimiento = result.FechaNacimiento;
            usuarioModel.FechaFormateada = result.FechaNacimiento?.ToString("dd-MM-yyyy");;
            usuarioModel.Sexo = result.Sexo;
            usuarioModel.Email = result.Email;
            usuarioModel.Passworld = result.Passworld;

            return View(usuarioModel);
        }

        [HttpPost]
        public ActionResult Editar(UsuarioModel usuarioModls)
        {
            
            if (!ModelState.IsValid)
            {
                return View();
            }

            usuarioWCF = new Usuario();

            usuarioWCF.Id = usuarioModls.Id;
            usuarioWCF.Nombre = usuarioModls.Nombre;
            usuarioWCF.FechaNacimiento = DateTime.Parse(usuarioModls.FechaFormateada);
            usuarioWCF.Sexo = usuarioModls.Sexo;
            usuarioWCF.Email = usuarioModls.Email;           

            var respuesta = clientWCF.ActualizarUsuario(usuarioWCF);

            if (respuesta)
            {
                string userId = (string)Session["userId"];
                GuardarLog("Editar", "Modificar", Int32.Parse(userId));

                return RedirectToAction("ListaUsuarios", "Usuarios");
            }
            return View();
                    
        }


        public ActionResult Eliminar(int Id)
        {
            //METODO SOLO DEVUELVE LA VISTA
            var result = clientWCF.ConsultarUsuario(Id);
            UsuarioModel usuarioModel = new UsuarioModel();
            usuarioModel.Id = result.Id;
            usuarioModel.Nombre = result.Nombre;
            usuarioModel.FechaNacimiento = result.FechaNacimiento;
            usuarioModel.Sexo = result.Sexo;
            usuarioModel.Email = result.Email;

            return View(usuarioModel);
        }


        [HttpPost]
        public ActionResult Eliminar(UsuarioModel usuarioModls)
        {
            var result = clientWCF.EliminarUsuario(usuarioModls.Id);
            
            if (result)
            {
                string userId = (string)Session["userId"];
                GuardarLog("Eliminar", "Eliminar", Int32.Parse(userId));
                return RedirectToAction("ListaUsuarios");

            }
            return View();
        }


        public void GuardarLog(string nombreMetodo,string accion, int userId)
        {
            try
            {
                string urlServicio = string.Empty;

                using (HttpClient client = new HttpClient())
                {
                    var input = new
                    {
                        NombreMetodo = nombreMetodo,
                        Accion= accion,
                        UserId = userId
                    };

                    urlServicio = url_Api + "api/Login/GuardarLog";

                    string inputJson = (new JavaScriptSerializer()).Serialize(input);
                    HttpContent inputContent = new StringContent(inputJson, Encoding.UTF8, "application/json");
                    string resultado = string.Empty;

                    HttpResponseMessage response = null;
                    response = client.PostAsync(urlServicio, inputContent).Result;                   

                    if (response.IsSuccessStatusCode)
                    {
                        string token = (string)Session["token"];
                    }
                   
                }
            }
            catch (Exception x)
            {
                throw;
            }
        }

        private void GenerarReporte(string nombreArchivo, string contenidoReporte, out string outmensaje)
        {
            outmensaje = string.Empty;
            try
            {
                Response.Clear();
                Response.ContentType = "text/plain";
                Response.AppendHeader("Content-Disposition", string.Format("attachment; filename={0}", nombreArchivo));
                Response.Write(contenidoReporte);
                Response.End();
            }
            catch (ThreadAbortException x)
            {
                outmensaje = x.Message;
            }
            catch (Exception x)
            {
                outmensaje = x.Message;
            }
        }

    }
}
