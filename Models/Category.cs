using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppMVC.Models
{
    [Table("Category")]
    public class Category
   {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; } = "Default-Category-Name";
   }
}
