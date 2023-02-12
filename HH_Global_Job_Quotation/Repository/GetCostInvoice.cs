using HH_Global_Job_Quotation.Helper;
using HH_Global_Job_Quotation.Models;
using HH_Global_Job_Quotation.Models.DTO;
using System.Text;

namespace HH_Global_Job_Quotation.Repository
{
    public class GetCostInvoice : IGetCostInvoice
    {
        private  HelperLookup _getHelperLookup;
        private decimal tax;
        private decimal margin;
        private decimal extraMargin;
        private decimal Total = 0.00m;
        private decimal totalCost_With_Margin = 0.00m;
        private decimal totalCost_With_ExtraMargin = 0.00m;
        private decimal newPriceCostWithTax;
       

        public GetCostInvoice(HelperLookup getHelperLookup)
        {
            _getHelperLookup = getHelperLookup;
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

            try
            {
               
                if (jobItemCreateRequest.IsExtraMargin)
                {
                    foreach (var item in jobItemCreateRequest.Items)
                    {
                        if((!string.IsNullOrEmpty(item.ItemName) || item.ItemName != "") && item.Price > 0)
                        {
                            if (!item.IsExempt)
                            {
                                tax = _getHelperLookup.GetCostWithTax(item.Price);
                                item.Price = _getHelperLookup.RoundUpToNearestCent(item.Price);

                                totalCost_With_ExtraMargin += item.Price;
                                totalCost_With_Margin = _getHelperLookup.RoundUpToNearestCent(totalCost_With_ExtraMargin);
                                tax = _getHelperLookup.GetCostWithTax(item.Price);
                                newPriceCostWithTax = item.Price + tax;
                                newPriceCostWithTax = _getHelperLookup.RoundUpToNearestCent(newPriceCostWithTax);
                                Total += newPriceCostWithTax;
                                var costPriceInCurrency = _getHelperLookup.ConvertToCurrency(newPriceCostWithTax);


                                item.ItemName = item.ItemName = $"{item.ItemName}: {costPriceInCurrency}";
                                responseItem.Items.Add(new ResponseItem
                                {
                                    Name = item.ItemName,
                                    Price = _getHelperLookup.ConvertToCurrency(newPriceCostWithTax),
                                });
                                responseItem.Message = "Successfully Created";
                                responseItem.IsSuccesful = true;

                            }
                            else if (item.IsExempt)
                            {

                                item.Price = _getHelperLookup.RoundUpToNearestCent(item.Price);
                                totalCost_With_Margin += item.Price;
                                totalCost_With_Margin = _getHelperLookup.RoundUpToNearestCent(totalCost_With_Margin);

                                newPriceCostWithTax = item.Price;
                                newPriceCostWithTax = _getHelperLookup.RoundUpToNearestCent(newPriceCostWithTax);
                                Total += newPriceCostWithTax;
                                var costPriceInCurrency = _getHelperLookup.ConvertToCurrency(newPriceCostWithTax);

                                item.ItemName = item.ItemName = $"{item.ItemName}: {costPriceInCurrency}";
                                responseItem.Items.Add(new ResponseItem
                                {
                                    Name = item.ItemName,
                                    Price = _getHelperLookup.ConvertToCurrency(newPriceCostWithTax),
                                });
                                responseItem.Message = "Successfully Created";
                                responseItem.IsSuccesful = true;
                            }
                        }
                        else
                        {
                            responseItem = new JobItemResponse();
                            responseItem.Message = "Failed, Fields are Required";
                            responseItem.IsSuccesful = false;
                            return responseItem;
                        }
                       
                    }

                    extraMargin = _getHelperLookup.GetCostWithExtraMargin(totalCost_With_Margin);
                    Total += extraMargin;
                    Total = _getHelperLookup.RoundUpToNearestEvenCent(Total);
                    responseItem.Total = _getHelperLookup.ConvertToCurrency(Total);


                }
                else
                {
                    foreach (var item in jobItemCreateRequest.Items)
                    {
                        if (!string.IsNullOrEmpty(item.ItemName) && item.Price > 0)
                        {
                            if (item.IsExempt)
                            {
                                item.Price = _getHelperLookup.RoundUpToNearestCent(item.Price);


                                newPriceCostWithTax = item.Price;
                                newPriceCostWithTax = _getHelperLookup.RoundUpToNearestCent(newPriceCostWithTax);
                                Total += newPriceCostWithTax;
                                var costPriceInCurrency = _getHelperLookup.ConvertToCurrency(newPriceCostWithTax);

                                item.ItemName = item.ItemName = $"{item.ItemName}: {costPriceInCurrency}";
                                item.Price = newPriceCostWithTax;
                                responseItem.Items.Add(new ResponseItem
                                {
                                    Name = item.ItemName,
                                    Price = _getHelperLookup.ConvertToCurrency(newPriceCostWithTax),
                                });

                                Total = _getHelperLookup.RoundUpToNearestEvenCent(Total);

                            }
                            else
                            {
                                item.Price = _getHelperLookup.RoundUpToNearestCent(item.Price);
                                totalCost_With_Margin += item.Price;
                                totalCost_With_Margin = _getHelperLookup.RoundUpToNearestCent(totalCost_With_Margin);
                                tax = _getHelperLookup.GetCostWithTax(item.Price);
                                newPriceCostWithTax = item.Price + tax;
                                newPriceCostWithTax = _getHelperLookup.RoundUpToNearestCent(newPriceCostWithTax);
                                Total += item.Price + tax;
                                var costPriceInCurrency = _getHelperLookup.ConvertToCurrency(newPriceCostWithTax);

                                item.ItemName = item.ItemName = $"{item.ItemName}: {costPriceInCurrency}";
                                item.Price = newPriceCostWithTax;
                                responseItem.Items.Add(new ResponseItem
                                {
                                    Name = item.ItemName,
                                    Price = _getHelperLookup.ConvertToCurrency(newPriceCostWithTax),
                                });

                                
                                margin = _getHelperLookup.GetCostWithMargin(totalCost_With_Margin);
                                Total += margin;
                                Total = _getHelperLookup.RoundUpToNearestEvenCent(Total);

                            }
                            responseItem.Message = "Successfully Created";
                            responseItem.IsSuccesful = true;
                            responseItem.Total = _getHelperLookup.ConvertToCurrency(Total);

                        }
                        else
                        {
                            responseItem = new JobItemResponse();
                            responseItem.Message = "Failed, Fields are Required";
                            responseItem.IsSuccesful = false;
                            return responseItem;

                        }


                    }


                }

                return responseItem;
               
            }
            catch (Exception)
            {

                throw;
            }

           
            
        }
    }
}
