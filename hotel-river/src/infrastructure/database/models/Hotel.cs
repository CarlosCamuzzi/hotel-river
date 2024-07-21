using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace hotel_river.src.infrastructure.database.models
{
    public class Hotel
    {
        public Hotel()
        { }
        public Hotel(string name, int bedRoomQuantity, Address? address)
        {
            if (address is null)
            {
                throw new ArgumentNullException(nameof(address), "Address cannot be null");
            }

            Id = Guid.NewGuid();
            Name = name;
            BedRoomQuantity = bedRoomQuantity;
            _address = address;
        }

        private readonly Address? _address;

        [Key]
        [Required]
        public Guid Id { get; private set; } // PK

        [Required]
        public Guid AddressId;                       // FK Address
        [ForeignKey("AddressId")]
        public Address? Address { get; }

        [Display(Name = "Razão Social")]
        [Required(ErrorMessage = "Favor informar a razão social")]
        [MaxLength(40)]
        public string? Name { get; private set; }

        [Display(Name = "Quantidade de quartos")]
        [Required(ErrorMessage = "Favor informar a quantidade de quartos")]
        [MaxLength(4)]
        public int BedRoomQuantity { get; private set; }

        public ICollection<Client>? Clients { get; }
        public ICollection<Employee>? Employees { get; }
        public ICollection<Room>? Rooms { get; }

        public void SetName(string? hotelName)
        {
            Name = hotelName ?? throw new
                ArgumentNullException(nameof(hotelName), "Hotel name cannot be null"); ;
        }

        public void SetBedRoomQuantity(int bedRoomQuantity)
        {
            if (bedRoomQuantity < 0)
            {
                throw new ArgumentException(nameof(bedRoomQuantity), "Bedroom quantity cannot be less than zero ");
            }

            BedRoomQuantity += bedRoomQuantity;
        }
    }
}
