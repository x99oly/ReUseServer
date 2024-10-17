using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Domain.Interface
{
    internal interface IDisposeble
    {
        /// <summary>
        /// Encerra uma conexão - Descarta objetos não gerenciados pelo Garbage Collector
        /// </summary>
        void Dispose();
    }
}
