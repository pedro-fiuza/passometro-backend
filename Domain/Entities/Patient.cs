using System.Text.Json.Serialization;

namespace Domain.Entities
{
    public class Patient
    {
        public int Id { get; set; }
        //[Display(Name = "Nome")]
        //[Required(ErrorMessage = "O nome é obrigatório.")]
        public string Name { get; set; }
        //[Display(Name = "Idade")]
        //[Required(ErrorMessage = "A idade é obrigatória.")]
        public int? Age { get; set; }
        //[Display(Name = "Nome da mãe")]
        //[Required(ErrorMessage = "O nome da mãe é obrigatório.")]
        public string MotherName { get; set; }
        //[Display(Name = "Data de nascimento")]
        //[Required(ErrorMessage = "A data de nascimento é obrigatório.")]
        public DateTime? BirthDate { get; set; }
        [JsonIgnore]
        public List<Hospital>? Hospitals { get; set; }
        [JsonIgnore]
        public List<Resume>? Resumes { get; set; }
    }
}
