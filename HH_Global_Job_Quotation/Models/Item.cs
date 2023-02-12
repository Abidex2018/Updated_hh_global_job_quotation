using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace HH_Global_Job_Quotation.Models
{
    public class Item
    {
        [Required(ErrorMessage = "Item Name is Required")]
        public string ItemName { get; set; }

        
        [DisplayName("Cost Price")]
        [Required(ErrorMessage = "Item Name is Required")]
        public decimal Price { get; set; }
       
        public bool IsExempt { get; set; }
    }
}
