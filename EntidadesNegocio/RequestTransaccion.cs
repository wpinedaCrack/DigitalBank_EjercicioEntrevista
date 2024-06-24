using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntidadesNegocio
{
    public class RequestTransaccion
    {
        public string token { get; set; }
        public string message { get; set; }
        public int userId { get; set; }
    }
}
