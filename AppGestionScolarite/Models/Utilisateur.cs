namespace AppGestionScolarite.Models
{
    public class Utilisateur
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string? Nom { get; set; }
        public string? Prenom { get; set; }
        public string? Adresse { get; set; }
        public string? Role { get; set; }
        public DateTime? DateNaissance { get; set; }
        public int SessionId { get; set; }
        public Session? Sessions { get; set; }


     
    }
}
