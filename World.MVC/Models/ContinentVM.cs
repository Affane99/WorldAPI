using System.ComponentModel.DataAnnotations;

namespace World.MVC.Models
{
    public class ContinentVM : CreateContinentVM
    {
        public Guid Id { get; set; }
    }

    public class CreateContinentVM
    {
        [Required]
        [Display(Name = "Nom du Continent")]
        public string Name { get; set; }
    }
}
