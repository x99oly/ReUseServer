using Server.Service;
using Server.Interface;
using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;

string pathToKey = "E:/LocalRepository/c-sharp/ConsoleServer/Server/Keys/FirebaseKey.json";

FirebaseApp.Create(new AppOptions()
{
    Credential = GoogleCredential.FromFile(pathToKey)
});
Console.WriteLine("Firebase SDK inicializado!");

IServidor server = new HttpServer();

server.StartServer("http://localhost:8001/"); // Substituir para interface