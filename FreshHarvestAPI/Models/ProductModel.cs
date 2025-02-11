using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace FreshHarvestAPI.Models
{
    public class ProductModel
    {

        [Key]//key denotes the database that it is the primary key
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]//means the number can have upto 18 didits wth 2 decimal places
        public decimal Price { get; set; }


        [ForeignKey("Category")]
        public int CategoryId { get; set; }

        public CategoryModel? Category { get; set; }//holds the detailed information of the category
    }
}
