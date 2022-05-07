using System.Text.Json.Serialization;

namespace Domain.Entities
{
    public class Resume
    {
        public int Id { get; set; }
        //[Display(Name = "Leito")]
        //[Required(ErrorMessage = "O leito é obrigatório.")]
        public string Bed { get; set; } = string.Empty;
        //[Display(Name = "Data de admissão")]
        //[Required(ErrorMessage = "A data de admissão é obrigatória.")]
        public DateTime? AdmissionDate { get; set; } = DateTime.UtcNow;
        //[Display(Name = "Cirurgia")]
        //[Required(ErrorMessage = "A cirurgia é obrigatória.")]
        public string Surgeries { get; set; } = string.Empty;
        //[Display(Name = "Diagnóstico principal")]
        //[Required(ErrorMessage = "O diagnóstico principal é obrigatório.")]
        public string MainDiagnosis { get; set; } = string.Empty;
        //[Display(Name = "Intercorrência")]
        //[Required(ErrorMessage = "A intercorrência é obrigatória.")]
        public string Complications { get; set; } = string.Empty;
        //[Display(Name = "Proposta do dia")]
        //[Required(ErrorMessage = "O propósito do dia é obrigatório.")]
        public string ProposalOfTheDay { get; set; } = string.Empty;
        public Patient Patient { get; set; }
        [JsonIgnore]
        public List<User> Doctors { get; set; }
        [JsonIgnore]
        public List<Event> Events { get; set; }
    }
}
