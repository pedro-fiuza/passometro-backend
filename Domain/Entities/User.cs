using System.Text.Json.Serialization;

namespace Domain.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Email { get; set; } = string.Empty;
        public string Crm { get; set; } = string.Empty;
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        [JsonIgnore]
        public List<Resume> Resumes { get; set; }

    }
}
