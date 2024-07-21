using hotel_hill.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace hotel_river.src.infrastructure.database.models
{
    public class Employee : Person
    {
        public Employee() 
        { }

        public Employee(string? name, DateTime birth, Gender gender, string? cpf,
            string? email, string? phone, Address address, Role role, Hotel hotel)
            : base(name, birth, gender, cpf, email, phone, address)
        {

            Hotel = hotel;
            Role = role;
        }

        [Display(Name = "Cargo")]
        [Required(ErrorMessage = "Favor informar o cargo")]
        public Role Role { get; private set; }

        [Required]
        public Guid HotelId { get; private set; }    // FK
        [ForeignKey("HotelId")]
        public Hotel? Hotel { get; }

        public void SetRole(Role role)
        {
            //if (role is null)
            //{
            //    throw new ArgumentNullException(nameof(role), "role cannot be null"); ;
            //}

            Role = role;
        }
    }

    public enum Role
    {
        Gerente, Camareiro, Atendente, Cozinheiro, Manutenção
    }
}

