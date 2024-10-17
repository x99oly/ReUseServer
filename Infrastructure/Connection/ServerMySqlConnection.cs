using MySql.Data.MySqlClient;
using Server.Domain.Model;

namespace Server.Infrastructure.Connection
{
    /// <summary>
    /// Classe para gerenciar a conexão com o banco de dados MySQL.
    /// Implementa a interface IDisposable para garantir que os recursos de conexão sejam liberados corretamente.
    /// </summary>
    internal class ServerMySqlConnection : IDisposable
    {
        private string _connectionString = "Server=localhost;Database=reuse_address;User=root;Password=senha;";
        private readonly MySqlConnection _connection;

        /// <summary>
        /// Inicializa uma nova instância da classe <see cref="ServerMySqlConnection"/> e cria uma nova conexão com o banco de dados.
        /// 
        /// ** Deve ser instanciada dentro de bloco 'using' para garantir o encerramento correto da conexão **
        /// 
        /// </summary>
        /// <exception cref="Exception">Lança uma exceção caso não seja possível se conectar ao banco de dados.</exception>
        public ServerMySqlConnection()
        {
            _connection = new MySqlConnection(_connectionString);

            // Verifica se a conexão foi inicializada corretamente
            if (_connection == null) throw new Exception("Não foi possível se conectar ao banco de dados.");
        }

        /// <summary>
        /// Obtém a instância da conexão com o banco de dados.
        /// </summary>
        /// <returns>Retorna a instância <see cref="MySqlConnection"/>.</returns>
        public MySqlConnection GetConnection()
        {
            return _connection;
        }

        /// <summary>
        /// Abre a conexão com o banco de dados.
        /// </summary>
        /// <exception cref="Exception">Lança uma exceção se a conexão já estiver aberta.</exception>
        public void OpenConnection()
        {
            // Verifica se a conexão já está aberta
            if (_connection.State == System.Data.ConnectionState.Open)
            {
                throw new Exception("Conexão já está aberta.");
            }

            _connection.Open();
        }

        /// <summary>
        /// Libera os recursos da conexão, fechando-a se estiver aberta.
        /// Implementa o padrão IDisposable para garantir que a conexão seja fechada corretamente.
        /// </summary>
        /// <exception cref="Exception">Lança uma exceção se a conexão não foi iniciada ou não estiver aberta.</exception>
        public void Dispose()
        {
            // Verifica se a conexão foi inicializada
            if (_connection == null) throw new Exception("Conexão não foi iniciada.");
            // Verifica se a conexão está aberta antes de fechar
            if (_connection.State != System.Data.ConnectionState.Open) throw new Exception("Conexão não foi aberta.");

            _connection.Close();
        }
    }
}
