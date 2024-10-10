using Server.Service;
using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;

string pathToKey = "E:/LocalRepository/c-sharp/ConsoleServer/Server/Keys/FirebaseKey.json";

FirebaseApp.Create(new AppOptions()
{
    Credential = GoogleCredential.FromFile(pathToKey)
});
Console.WriteLine("Firebase SDK inicializado!");

HttpServer.StartHttpServer();