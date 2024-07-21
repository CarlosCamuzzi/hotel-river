using hotel_river.src.infrastructure.database.models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace hotel_hill.Models
{
    [Table("Person")]
    public class Person
    {
        public Person()
        { }
        public Person(string? name, DateTime birth, Gender gender, string? cpf,
            string? email, string? phone, Address address)
        {         
            Id = Guid.NewGuid(); // PK                           
            Name = name;
            Birth = birth;
            Gender = gender;
            CPF = cpf;
            Email = email;
            Phone = phone;            
            Address = address;   // verify instance (params)
            AddressId = address.Id;
        }        

        [Key]
        [Required]
        public Guid Id { get; set; }         // PK

        [Required]
        public Guid AddressId;                       // FK Address
        [ForeignKey("AddressId")]
        public Address? Address { get; }       

        [Display(Name = "Nome")]
        [Required(ErrorMessage = "Favor informar o nome")]
        [MinLength(5), MaxLength(30)]
        public string? Name { get; private set; }

        [Display(Name = "Sobrenome")]
        [Required(ErrorMessage = "Favor informar o sobrenome")]
        [MinLength(2), MaxLength(30)]
        public string? LastName { get; private set; }

        [Display(Name = "Data de Nascimento")]
        [Required(ErrorMessage = "Favor informar a data de nascimento")]
        [DataType(DataType.Date)]
        public DateTime Birth { get; private set; }

        [Display(Name = "Gênero")]  // Type Select in front
        [Required(ErrorMessage = "Favor informar o seu gênero")]
        [MaxLength(14)]
        public Gender Gender { get; private set; }

        [Display(Name = "CPF")] // mask
        [Required(ErrorMessage = "Favor informar o CPF")]
        [MaxLength(14)]
        public string? CPF { get; private set; }

        [Display(Name = "Email")]
        [Required(ErrorMessage = "Favor informar o email")]
        [MinLength(7)]
        [EmailAddress]
        public string? Email { get; private set; }

        [Display(Name = "Telefone")]    // mask
        [Required(ErrorMessage = "Favor informar o número do telefone")]
        [Phone]
        public string? Phone { get; private set; }
       
        public void SetName(string? name)
        {
            Name = name ?? throw new
                ArgumentNullException(nameof(name), "Name cannot be null"); ;
        }

        public void SetBirth(DateTime birth)
        {
            // DateTime.MinValue: Minimum valid value for DateTime type
            if (birth != DateTime.MinValue)
            {
                throw new ArgumentException(nameof(birth), "Invalid birth value");
            }

            Birth = birth;
        }

        public void SetGender(Gender genderValue)
        {
            // True if genderValue is one Gender defined
            if (!Enum.IsDefined(typeof(Gender), genderValue))
            {
                throw new ArgumentException(nameof(genderValue), "Invalid gender value");
            }

            Gender = genderValue;
        }

        public void SetCPF(string? cpf) // MASK
        {
            CPF = cpf ?? throw new
                ArgumentNullException(nameof(cpf), "CPF cannot be null"); ;
        }

        public void SetEmail(string? email) // MASK
        {
            Email = email ?? throw new
                ArgumentNullException(nameof(email), "Email cannot be null");
        }

        public void SetTelefone(string? telefone) // MASK
        {
            Phone = telefone ?? throw new
                ArgumentNullException(nameof(telefone), "Telefone cannot be null"); ;
        }
    }

    public enum Gender
    {
        Masculino,
        Feminino,
        Outros
    }
}
