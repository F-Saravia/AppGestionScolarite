using System.ComponentModel.DataAnnotations.Schema;

namespace AppGestionScolarite.Models
{
    public class Module
    {

        //un module: logo, id, nom, resume, infos
        public int Id { get; set; }
        public string Nom { get; set; }
        public string? Resume { get; set; }
        public string? Infos { get; set; }
        public string? Logo { get; set; }

        //[InverseProperty(nameof(ParcourModule))]
        //public virtual ICollection<ParcourModule> Parcours { get; set; }
        
        public int ParcoursId { get; set; }
        public virtual ICollection<Parcour>? Parcours { get; set; }
       
        public int UnitePedagogiqueId { get; set; }
       // public virtual ICollection<UnitePedagogique>? UnitePedagogiques { get; set; }

       public UnitePedagogique? UnitePedagogique { get; set; }

    }
}
