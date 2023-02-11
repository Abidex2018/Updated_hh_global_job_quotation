using HH_Global_Job_Quotation.Models.DTO;

namespace HH_Global_Job_Quotation.Repository
{
    public interface IGetCostInvoice
    {
        JobItemResponse GetCostInvoiceJobItem(JobItemCreateRequest jobItemCreateRequest);
    }
}
