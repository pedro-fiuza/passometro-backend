using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Domain.Entities
{
    public class Hospital
    {
        public int Id { get; set; }
        //[Display(Name = "Nome")]
        //[Required(ErrorMessage = "O nome é obrigatório.")]
        public string Name { get; set; } = string.Empty;
        //[Display(Name = "Endereço")]
        //[Required(ErrorMessage = "O endereço é obrigatório.")]
        public string Address { get; set; } = string.Empty;
        //[Display(Name = "Cidade")]
        //[Required(ErrorMessage = "A cidade é obrigatória.")]
        public string City { get; set; } = string.Empty;
        //[Display(Name = "UF")]
        //[Required(ErrorMessage = "A unidade federativa é obrigatória.")]
        public string Uf { get; set; } = string.Empty;
        [NotMapped]
        public bool Editing { get; set; } = false;
        [NotMapped]
        public bool IsNew { get; set; } = false;
        [JsonIgnore]
        public List<Patient>? Patients { get; set; }
    }
}
