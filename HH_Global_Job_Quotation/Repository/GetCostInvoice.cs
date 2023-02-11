using HH_Global_Job_Quotation.Helper;
using HH_Global_Job_Quotation.Models;
using HH_Global_Job_Quotation.Models.DTO;
using System.Text;

namespace HH_Global_Job_Quotation.Repository
{
    public class GetCostInvoice : IGetCostInvoice
    {
        private  HelperLookup _getHelperLook;
        private  double tax;
        private double exempt = 0.00;
        private  double margin;
        private  double extraMargin;
        private  double Total = 0.00;

        private  double totalMargin = 0.00;
        private  double totalCost_With_Margin = 0.00;
        private  double totalCost_With_ExtraMargin = 0.00;
        private double PriceCost;
        private double newPriceCostWithTax;
       

        public GetCostInvoice(HelperLookup getHelperLook)
        {
            _getHelperLook = getHelperLook;
        }
        public JobItemResponse GetCostInvoiceJobItem(JobItemCreateRequest jobItemCreateRequest)
        {
            var responseItem = new JobItemResponse();
            var jobCode = "JB";
            var jobID = new StringBuilder();
            var rand = new Random();
            var jobItemId = rand.Next(0, 1000);
            jobID.Append(jobCode);
            jobID.Append(jobItemId.ToString());
            responseItem.JobItemId = jobID.ToString();
            responseItem.Items = new List<ResponseItem>();



            if (jobItemCreateRequest.IsExtraMargin)
            {
                foreach (var item in jobItemCreateRequest.Items)
                {
                    if (!item.IsExempt)
                    {
                        tax = _getHelperLook.GetCostWithTax(item.Price);
                        item.Price = _getHelperLook.RoundUpToNearestCent(item.Price);

                        totalCost_With_ExtraMargin += item.Price;
                        totalCost_With_Margin = _getHelperLook.RoundUpToNearestCent(totalCost_With_ExtraMargin);
                        tax = _getHelperLook.GetCostWithTax(item.Price);
                        newPriceCostWithTax = item.Price + tax;
                        newPriceCostWithTax = _getHelperLook.RoundUpToNearestCent(newPriceCostWithTax);
                        Total += newPriceCostWithTax;

                       
                        item.ItemName = item.ItemName = $"{item.ItemName}: ${newPriceCostWithTax}";
                        responseItem.Items.Add(new ResponseItem
                        {
                            Name = item.ItemName,
                            Price = newPriceCostWithTax,
                        });


                    }
                    else if (item.IsExempt)
                    {

                        item.Price = _getHelperLook.RoundUpToNearestCent(item.Price);
                        totalCost_With_Margin += item.Price;
                        totalCost_With_Margin = _getHelperLook.RoundUpToNearestCent(totalCost_With_Margin);
                       
                        newPriceCostWithTax = item.Price;
                        newPriceCostWithTax = _getHelperLook.RoundUpToNearestCent(newPriceCostWithTax);
                        Total += newPriceCostWithTax;

                       
                        item.ItemName = item.ItemName = $"{item.ItemName}: ${newPriceCostWithTax}";
                        responseItem.Items.Add(new ResponseItem
                        {
                            Name = item.ItemName, 
                            Price = newPriceCostWithTax,
                        });

                    }
                }

                extraMargin = _getHelperLook.GetCostWithExtraMargin(totalCost_With_Margin);
                Total += extraMargin;
                Total = _getHelperLook.RoundUpToNearestEvenCent(Total);
                responseItem.Total = Total;
                responseItem.Total = _getHelperLook.RoundUpToNearestEvenCent(responseItem.Total);

            }
            else
            {
                foreach (var item in jobItemCreateRequest.Items)
                {
                    if (item.IsExempt)
                    {
                        item.Price = _getHelperLook.RoundUpToNearestCent(item.Price);


                        newPriceCostWithTax = item.Price;
                        newPriceCostWithTax = _getHelperLook.RoundUpToNearestCent(newPriceCostWithTax);
                        Total += newPriceCostWithTax;
                        
                       
                        item.ItemName =  $"{item.ItemName}: ${newPriceCostWithTax}";
                        item.Price = newPriceCostWithTax;
                        responseItem.Items.Add(new ResponseItem
                        {
                            Name = item.ItemName,
                            Price = newPriceCostWithTax,
                        });

                        Total = _getHelperLook.RoundUpToNearestEvenCent(Total);
                       
                    }
                    else
                    {
                        item.Price = _getHelperLook.RoundUpToNearestCent(item.Price);
                        totalCost_With_Margin += item.Price;
                        totalCost_With_Margin = _getHelperLook.RoundUpToNearestCent(totalCost_With_Margin);
                        tax = _getHelperLook.GetCostWithTax(item.Price);
                        newPriceCostWithTax = item.Price + tax;
                        newPriceCostWithTax = _getHelperLook.RoundUpToNearestCent(newPriceCostWithTax);
                        Total += item.Price + tax;
                        
                        item.ItemName = $"{item.ItemName}: ${newPriceCostWithTax}";
                        item.Price = newPriceCostWithTax;
                        responseItem.Items.Add(new ResponseItem
                        {
                            Name = item.ItemName,
                            Price = newPriceCostWithTax,
                        });

                        margin = _getHelperLook.GetCostWithMargin(totalCost_With_Margin);
                        Total += margin;
                        Total = _getHelperLook.RoundUpToNearestEvenCent(Total);

                    }
                        responseItem.Total  = Total;
                    responseItem.Total = _getHelperLook.RoundUpToNearestEvenCent(responseItem.Total);


                }


            }

            return responseItem;
            
        }
    }
}
