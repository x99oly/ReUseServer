using Server.Service.Host;
using Server.Interface;
using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;
using Google.Cloud.Firestore;
using System.IO;

// Caminho para o arquivo JSON da chave privada
string pathToKey = "E:/LocalRepository/c-sharp/ConsoleServer/Server/Keys/FirebaseKey.json";

// Inicializa o Firebase Admin SDK
FirebaseApp.Create(new AppOptions()
{
    Credential = GoogleCredential.FromFile(pathToKey)
});
Console.WriteLine("Firebase SDK inicializado!");

// Inicializa o Firestore
//FirestoreDb firestoreDb = FirestoreDb.Create("6dc9c6cf59aa94eb7a1294b6c997524a1e3087d9");
Console.WriteLine("Conectado ao Firestore!");

// Inicializa o servidor HTTP
IServidor server = new HttpServer();
server.StartServer("http://localhost:8001/");
