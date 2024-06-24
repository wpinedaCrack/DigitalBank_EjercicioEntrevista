using Newtonsoft.Json;
using System;
using System.Configuration;
using System.Net.Http;
using System.Text;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using EntidadesNegocio;
using System.Net;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PresentacionWeb.Controllers
{
    public class HomeController : Controller
    {
        public string url_Api = ConfigurationManager.AppSettings["API_REST"].ToString();

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(string correo, string clave)
        {
            string mensajeError = string.Empty;

            Autenticar(correo, clave, out mensajeError);

            if (!string.IsNullOrEmpty(mensajeError))
            {
                ViewBag.ErrorMessage = mensajeError;
                return View();
            }
            
           return RedirectToAction("ListaUsuarios", "Usuarios");

        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public void Autenticar(string correo, string clave, out string mensajeError){
            mensajeError = "";
            string urlServicio = string.Empty;

            using (HttpClient client = new HttpClient())
            {
                var input = new
                {
                    Email = correo,
                    Passworld = clave
                };

                urlServicio = url_Api + "api/Login/Autenticar";

                string inputJson = (new JavaScriptSerializer()).Serialize(input);
                HttpContent inputContent = new StringContent(inputJson, Encoding.UTF8, "application/json");
                string resultado = string.Empty;

                HttpResponseMessage response = null;
                response = client.PostAsync(urlServicio, inputContent).Result;

                var result = JsonConvert.DeserializeObject<RequestTransaccion>(response.Content.ReadAsStringAsync().Result);

                if (response.IsSuccessStatusCode)
                {
                    string token = result.token;
                    string userId = result.userId.ToString();
                    Session["token"] = token;
                    Session["userId"] = userId;
                    //string valor = Session["NombreDeVariable"] as string;               
                }
                else if (response.StatusCode == HttpStatusCode.BadRequest)
                {
                   mensajeError = result.message;
                }
            }
        }

    }
}