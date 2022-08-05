using System.ComponentModel.DataAnnotations.Schema;

namespace AppGestionScolarite.Models
{
    public class Session
    {

        public int Id { get; set; }
        public DateTime DateDebut { get; set; }
        public DateTime DateFin { get; set; }
        public string? Intitule { get; set; }

        //[ForeignKey(nameof(Parcours))]
        //public int ParcoursId { get; set; }
        public Parcour? Parcour { get; set; }

      
        public int ParcoursId { get; set; }
       

        public ICollection<Utilisateur>? Utilisateurs { get; set; }

    }
}
