using System;

namespace Server.Domain.Model
{
    internal class Address
    {
        public string UserId { get; set; }
        public string PostalCode { get; set; }
        public string Street { get; set; }
        public string Number { get; set; }
        public string Complement { get; set; } // Nullable (optional)
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

        public override string ToString()
        {
            return $"{Street}, {Number} {Complement} - {Neighborhood}, {City}/{State}, {PostalCode}";
        }
    }
}
