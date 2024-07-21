using hotel_hill.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace hotel_river.src.infrastructure.database.models
{
    public class Client : Person
    {
        public Client()
        { }

        public Client(string? name, DateTime birth, Gender gender,
            string? cpf, string? email, string? phone, Address address, Hotel hotel)
             : base(name, birth, gender, cpf, email, phone, address)
        {
            if (hotel is not null)
                Hotel = hotel;
        }

        [Required]
        public Guid HotelId { get; private set; }    // FK
        [ForeignKey("HotelId")]
        [JsonIgnore]
        public Hotel? Hotel { get; }
    }
}
