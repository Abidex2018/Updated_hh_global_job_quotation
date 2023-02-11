namespace HH_Global_Job_Quotation.Models.DTO
{
    public class JobItemCreateRequest 
    {
        public bool IsExtraMargin { get; set; }
        public List<Item> Items { get; set;}

    }
}
