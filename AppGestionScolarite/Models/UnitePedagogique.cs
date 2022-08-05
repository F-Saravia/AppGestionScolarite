namespace AppGestionScolarite.Models
{
    public class UnitePedagogique
    {
        public int Id { get; set; }
        public string Designation { get; set; }
        public ICollection<Module>? Modules { get; set; }
    }
}
