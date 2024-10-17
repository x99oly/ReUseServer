using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;
using Google.Cloud.Firestore;
using MySql.Data.MySqlClient;
using Server.Domain.Interface;
using Server.Domain.Model;
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
    // String de conexão com o banco de dados MySQL (substitua 'senha' pela senha do MySQL)
    string connectionString = "Server=localhost;Database=reuse_address;User=root;Password=senha;";

    // Query SQL para selecionar todos os campos da tabela "address"
    string query = "SELECT user_id, street, number, cep, neighborhood, city, state FROM address LIMIT 1;";

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

            string userId = "someUserId";
            string street = "Rua ABC";
            string number = "123";
            string cep = "12345-678";
            string neighborhood = "Bairro XYZ";
            string city = "Cidade XYZ";
            string state = "ST";
            string complement = "Apto 101";  // Opcional

            var address = new Address(userId,cep,street,number,neighborhood,city,state,complement);
            // Ler e exibir os resultados
            Console.WriteLine($"\n{address.ToString()}");

            reader.Close();
        }
        catch (Exception ex)
        {
            // Tratar possíveis erros de conexão
            Console.WriteLine($"Erro ao conectar: {ex.Message}");
        }
    }
}
