using hotel_hill.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace hotel_river.src.infrastructure.database.models
{
    public class Room
    {
        public Room()
        { }

        public Room(Hotel hotel, Person person, Floor floor,
            int[] numberRoom, TypeRoom typeRoom, string? description
            //  List<AdditionalRoomItem>? additionalRoomItem = null
            )
        {
            Id = Guid.NewGuid();
            Hotel = hotel;
            Person = person;
            Floor = floor;
            NumberRoom = numberRoom;
            TypeRoom = typeRoom;
            Description = description;
            // Create List if additionalRoomItem is not null
            // _additionalRoomItem = additionalRoomItem ?? new List<AdditionalRoomItem>();
        }

        //  private readonly List<AdditionalRoomItem>? _additionalRoomItem;

        [Key]
        [Required]
        public Guid Id { get; private set; }    // PK

        [Required]
        public Guid HotelId;                     // FK     
        [ForeignKey("HotelId")]
        public Hotel? Hotel { get; }

        [Required]
        public Guid PersonId;                     // FK     
        [ForeignKey("PersonId")]
        public Person? Person { get; }

        [Display(Name = "Andar do Quarto")] // Select front
        [Required(ErrorMessage = "Favor selecionar o andar do quarto")]
        public Floor Floor { get; private set; }

        [Display(Name = "Número do Quarto")]    // Select front
        [Required(ErrorMessage = "Favor selecionar o Número do Quarto")]
        [MaxLength(2)]
        public int[] NumberRoom { get; } = [1, 2, 3, 4, 5, 6, 7, 8, 9, 10];

        [Display(Name = "Tipo")] // Select front
        [Required(ErrorMessage = "Favor selecionar o tipo de quarto")]
        public TypeRoom TypeRoom { get; private set; }

        [Display(Name = "Descrição")]
        [Required(ErrorMessage = "Favor inserir descrição do quarto")]
        [MaxLength(100)]
        public string? Description { get; private set; }

        public void SetDescription(string? description)
        {
            Description = description ?? throw new
                 ArgumentNullException(nameof(description), "Description cannot be null"); ;
        }

        //public void SetAdditionalItem(AdditionalRoomItem item)
        //{
        //  //  _additionalRoomItem?.Add(item);
        //}
    }

    public enum Floor
    {
        Primeiro = 1,
        Segundo = 2,
        Terceiro = 3,
        Quarto = 4,
        Quinto = 5,
        Sexto = 6,
        Sétimo = 7,
        Oitavo = 8
    }

    public enum TypeRoom
    {
        SingleRoom = 1,
        TwinRoom = 2,
        DoubleRoom = 3,
        SingleDoubleRoom = 4,
    }

}


