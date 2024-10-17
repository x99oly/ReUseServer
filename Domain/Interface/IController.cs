using System.Net;
using System.Threading.Tasks;

namespace Server.Domain.Interface
{
    public interface IController
    {
        /// <summary>
        /// Processa uma requisição HTTP e a direciona para o método adequado (GET, POST, DELETE, PUT).
        /// </summary>
        /// <param name="context">O contexto da requisição HTTP.</param>
        Task ProcessRequest(HttpListenerContext context);
    }
}
