using PresentacionWeb.Models;
using PresentacionWeb.ServiceWCFNegocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PresentacionWeb.ViewModels
{
    public class IndexViewModel:BaseModelo
    {
        public List<Usuario> Usuarios { get; set; }
    }
}