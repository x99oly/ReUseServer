using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Domain.Interface
{
    internal interface IServidor
    {
        /// <summary>
        /// Método de contrato de Interface para instânciar um server HTTP ou HTTPS
        /// </summary>
        public void StartServer(string url);

    }
}
