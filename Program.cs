using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;
using Google.Cloud.Firestore;
using MySql.Data.MySqlClient;
using Server.Domain.Interface;
using Server.Service.Host;

// Caminho para o arquivo JSON da chave privada
string pathToKey = "E:/LocalRepository/c-sharp/ConsoleServer/Server/Keys/FirebaseKey.json";

// Inicializa o Firebase Admin SDK
FirebaseApp.Create(new AppOptions()
{
    Credential = GoogleCredential.FromFile(pathToKey)
});
Console.WriteLine("Firebase SDK inicializado!");


// Inicializa o servidor HTTP
IServidor server = new HttpServer();
server.StartServer("http://localhost:8001/");



void conectar()
{
    // String de conexão com o banco de dados MySQL (substitua SUA_SENHA)
    string connectionString = "Server=localhost;Database=sakila;User=root;Password=senha;";

    // Query SQL para selecionar os 10 primeiros filmes
    string query = "SELECT title, release_year FROM film LIMIT 10;";

    // Criar a conexão com o MySQL
    using (MySqlConnection connection = new MySqlConnection(connectionString))
    {
        try
        {
            // Abrir a conexão
            connection.Open();
            Console.WriteLine("Conexão estabelecida com sucesso!");

            // Executar a consulta
            MySqlCommand command = new MySqlCommand(query, connection);
            MySqlDataReader reader = command.ExecuteReader();

            // Ler e exibir os resultados
            Console.WriteLine("Filmes no banco de dados Sakila:");
            while (reader.Read())
            {
                string title = reader.GetString("title");
                int releaseYear = reader.GetInt32("release_year");
                Console.WriteLine($"Título: {title}, Ano de Lançamento: {releaseYear}");
            }
            reader.Close();
        }
        catch (Exception ex)
        {
            // Tratar possíveis erros de conexão
            Console.WriteLine($"Erro ao conectar: {ex.Message}");
        }
    }

}