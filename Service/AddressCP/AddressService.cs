using Google.Cloud.Firestore;
using Server.Domain.Model;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace Server.Service.AddressCP
{
    internal class AddressService
    {
        private static FirestoreDb _firestoreDb = FirestoreDb.Create("your-project-id"); // Substitua pelo seu ID do projeto no Google Cloud

        /// <summary>
        /// Cria um novo endereço no Firestore.
        /// </summary>
        /// <param name="address">Objeto Address a ser salvo</param>
        /// <returns>Retorna o ID do documento criado.</returns>
        public static async Task<string> CreateAddressAsync(Address address)
        {
            DocumentReference docRef = _firestoreDb.Collection("addresses").Document();
            await docRef.SetAsync(address);
            return docRef.Id;
        }
        
        /// <summary>
        /// Recupera um endereço do Firestore pelo ID do usuário.
        /// </summary>
        /// <param name="userId">ID do usuário</param>
        /// <returns>Retorna um objeto Address se encontrado.</returns>
        public static async Task<Address> GetAddressByUserIdAsync(string userId)
        {
            Query query = _firestoreDb.Collection("addresses").WhereEqualTo("UserId", userId);
            QuerySnapshot snapshot = await query.GetSnapshotAsync();

            if (snapshot.Documents.Count > 0)
            {
                return snapshot.Documents[0].ConvertTo<Address>();
            }

            return null;
        }

        /// <summary>
        /// Atualiza um endereço existente no Firestore com base no ID do usuário.
        /// </summary>
        /// <param name="userId">ID do usuário</param>
        /// <param name="address">Objeto Address atualizado</param>
        public static async Task UpdateAddressAsync(string userId, Address address)
        {
            Query query = _firestoreDb.Collection("addresses").WhereEqualTo("UserId", userId);
            QuerySnapshot snapshot = await query.GetSnapshotAsync();

            if (snapshot.Documents.Count > 0)
            {
                DocumentReference docRef = snapshot.Documents[0].Reference;
                await docRef.SetAsync(address, SetOptions.Overwrite);
            }
        }

        /// <summary>
        /// Deleta um endereço do Firestore pelo ID do usuário.
        /// </summary>
        /// <param name="userId">ID do usuário</param>
        public static async Task DeleteAddressAsync(string userId)
        {
            Query query = _firestoreDb.Collection("addresses").WhereEqualTo("UserId", userId);
            QuerySnapshot snapshot = await query.GetSnapshotAsync();

            if (snapshot.Documents.Count > 0)
            {
                DocumentReference docRef = snapshot.Documents[0].Reference;
                await docRef.DeleteAsync();
            }
        }

        /// <summary>
        /// Lê o corpo da requisição HTTP e o retorna como uma string.
        /// </summary>
        /// <param name="req">O objeto HttpListenerRequest que representa a requisição HTTP.</param>
        /// <param name="resp">O objeto HttpListenerResponse que representa a resposta HTTP.</param>
        /// <returns>A string contendo o corpo da requisição.</returns>
        public static async Task<string> JsonToString(HttpListenerRequest req, HttpListenerResponse resp)
        {
            using var reader = new StreamReader(req.InputStream, req.ContentEncoding);
            return await reader.ReadToEndAsync();
        }
    }
}
