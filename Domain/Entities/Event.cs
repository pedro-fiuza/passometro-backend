namespace Domain.Entities
{
    public class Event
    {
        public int Id { get; set; }
        //[Display(Name = "Título")]
        //[Required(ErrorMessage = "O título do evento é obrigatório.")]
        public string Title { get; set; } = string.Empty;
        //[Display(Name = "Descrição")]
        //[Required(ErrorMessage = "A descrição do evento é obrigatória.")]
        public string Description { get; set; } = string.Empty;
        //[Display(Name = "Data do evento")]
        //[Required(ErrorMessage = "A data do evento é obrigatória.")]
        public DateTime EventDate { get; set; } = DateTime.UtcNow;
        public Resume Resume { get; set; }
    }
}
