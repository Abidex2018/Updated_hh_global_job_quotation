namespace HH_Global_Job_Quotation.Models
{
    public class JobItem
    {
        public Guid JobItemId { get; set; }
        public bool IsExtraMargin { get; set; }
        public List<Item> Items { get; set;}
       
    }

   
}
