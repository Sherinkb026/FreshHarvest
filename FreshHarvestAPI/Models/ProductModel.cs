using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace FreshHarvestAPI.Models
{
    public class ProductModel
    {

        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }

       


        [ForeignKey("Category")]
        public int CategoryId { get; set; }

        public CategoryModel? Category { get; set; }
    }
}
