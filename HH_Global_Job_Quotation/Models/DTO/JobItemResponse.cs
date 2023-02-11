namespace HH_Global_Job_Quotation.Models.DTO
{
    public class JobItemResponse
    {
        public string JobItemId { get; set; }
        public List<ResponseItem> Items { get; set; }
        public double Total { get; set; }

    }

    public class ResponseItem
    {
        public string Name { get; set; }
        public double Price { get; set; }

        
    }
}
