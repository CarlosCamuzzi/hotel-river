using System.ComponentModel.DataAnnotations;

namespace hotel_river.src.infrastructure.database.models
{
    public class Address
    {
        public Address()
        { }

        public Address(string? cep, string? street, string? neighborhood,
            int number, string? complement)
        {
            Id = Guid.NewGuid(); // PK
            CEP = cep;
            Street = street;
            Neighborhood = neighborhood;
            Number = number;
            Complement = complement;
        }

        [Key]
        public Guid Id { get; set; }  // PK        

        [Display(Name = "CEP")] //Mask
        [Required(ErrorMessage = "Informe o CEP")]
        [MaxLength(9)]
        public string? CEP { get; private set; }

        [Display(Name = "Rua")]
        [Required(ErrorMessage = "Informe a rua")]
        [MaxLength(30)]
        public string? Street { get; private set; }

        [Display(Name = "Bairro")]
        [Required(ErrorMessage = "Informe a bairro")]
        [MaxLength(20)]
        public string? Neighborhood { get; private set; }

        [Display(Name = "Número")]
        [Required(ErrorMessage = "Informe a número")]
        [MaxLength(6)]
        public int Number { get; private set; }

        [Display(Name = "Complemento")]
        [MaxLength(30)]
        public string? Complement { get; private set; }

        public void SetCep(string? cep)
        {
            CEP = cep ?? throw new
                ArgumentNullException(nameof(cep), "CEP cannot be null"); ;
        }

        public void SetStreet(string? street)
        {
            Street = street ?? throw new
                ArgumentNullException(nameof(street), "Street cannot be null"); ;
        }

        public void SetNeighborhood(string? neighborhood)
        {
            Neighborhood = neighborhood ?? throw new
                ArgumentNullException(nameof(neighborhood), "Neighborhood cannot be null"); ;
        }

        public void SetNumber(int number)
        {
            if (number < 0)
            {
                throw new ArgumentException(nameof(number), "Number cannot be less than zero");
            }

            Number = number;
        }

        public void SetComplement(string? complement)
        {
            Complement = complement;
        }
    }
}
