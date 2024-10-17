using Server.Domain.DTO;
using System;
using System.Runtime.ConstrainedExecution;
using System.Text.Json;

namespace Server.Domain.Model
{
    internal class Address
    {
        public string UserId { get; set; }
        public string PostalCode { get; set; }
        public string Street { get; set; }
        public string Number { get; set; }
        public string? Complement { get; set; } // Nullable (optional)
        public string Neighborhood { get; set; }
        public string City { get; set; }
        public string State { get; set; }

        public Address(string userId, string cep, string rua, string numero, string bairro, string cidade, string estado, string? complemento = null)
        {
            UserId = userId;
            PostalCode = cep;
            Street = rua;
            Number = numero;
            Neighborhood = bairro;
            City = cidade;
            State = estado;
            Complement = complemento;
        }

        public Address()
        {
        }

        public override string ToString()
        {
            return $"{Street}, {Number} {Complement} - {Neighborhood}, {City}/{State}, {PostalCode}";
        }

        /// <summary>
        /// Converte uma string JSON contendo os dados de um endereço em um objeto <see cref="Address"/>.
        /// </summary>
        /// <param name="request">Uma string JSON que representa os dados de um endereço.</param>
        /// <returns>Um objeto <see cref="Address"/> criado a partir dos dados JSON fornecidos.</returns>
        /// <exception cref="JsonException">Lançada se o JSON não for válido ou estiver malformado.</exception>
        /// <remarks>
        /// Esta função realiza a extração manual dos campos a partir do JSON, garantindo que os dados 
        /// estejam completos e corretos antes de criar o objeto <see cref="Address"/>.
        /// Campos obrigatórios no JSON:
        /// - userId: Representa o ID do usuário.
        /// - cep: Código postal (CEP) do endereço.
        /// - street: Nome da rua do endereço.
        /// - number: Número do endereço.
        /// - neighborhood: Bairro do endereço.
        /// - city: Cidade do endereço.
        /// - state: Estado do endereço.
        /// 
        /// Campo opcional:
        /// - complement: Complemento do endereço (pode ser null se não for fornecido).
        /// 
        /// Exemplo de JSON esperado:
        /// {
        ///   "userId": "12345",
        ///   "cep": "12345-678",
        ///   "street": "Rua Exemplo",
        ///   "number": "100",
        ///   "neighborhood": "Centro",
        ///   "city": "Cidade Exemplo",
        ///   "state": "EX",
        ///   "complement": "Apartamento 101"
        /// }
        /// </remarks>
        public static Address ParseAddressJson(string request)
        {
            var parsedData = JsonDocument.Parse(request).RootElement;

            string userId = parsedData.GetProperty("userId").GetString();
            string postalCode = parsedData.GetProperty("cep").GetString();
            string street = parsedData.GetProperty("street").GetString();
            string number = parsedData.GetProperty("number").GetString();
            string neighborhood = parsedData.GetProperty("neighborhood").GetString();
            string city = parsedData.GetProperty("city").GetString();
            string state = parsedData.GetProperty("state").GetString();
            string complement = parsedData.TryGetProperty("complement", out JsonElement complementElement) ? complementElement.GetString() : null;

            return new Address(userId, postalCode, street, number, neighborhood, city, state, complement);
        }

    }
}
