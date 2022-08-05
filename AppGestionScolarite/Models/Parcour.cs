using System.ComponentModel.DataAnnotations.Schema;

namespace AppGestionScolarite.Models
{
    public class Parcour
    {
        public int Id { get; set; }
        public string Nom { get; set; }
        public string? Resume { get; set; }
        public string? Infos { get; set; }
        public string? Logo { get; set; }

        //[InverseProperty(nameof(ParcourModule))]
        //public virtual ICollection<ParcourModule> Modules { get; set; }

        //[InverseProperty(nameof(Session))]
        //public virtual ICollection<Session> Sessions { get; set; }
        public virtual ICollection<Module>? Modules { get; set; }
        public virtual ICollection<Session> ?Sessions { get; set; }



    }
}
