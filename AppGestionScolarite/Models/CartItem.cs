using System.ComponentModel.DataAnnotations;

namespace AppGestionScolarite.Models
{
    public class CartItem
    {        
        public string Id { get; set; }
        public double Prix { get; set; }
        public int NbHeures { get; set; }
        
        public int ModuleId { get; set; }
        public virtual Module Module { get; set; }
    }
}
