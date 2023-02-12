using System.ComponentModel.DataAnnotations;

namespace HH_Global_Job_Quotation.Models.DTO
{
    public class JobItemResponse
    {
        public string JobItemId { get; set; }
        public List<ResponseItem> Items { get; set; }
        public string Total { get; set; }

    }

    public class ResponseItem
    {
       
        
        public string Name { get; set; }
        public string Price { get; set; }

        
    }
}
