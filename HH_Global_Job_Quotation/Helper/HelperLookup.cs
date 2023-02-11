using Microsoft.Extensions.Options;
using HH_Global_Job_Quotation.Helper;

namespace HH_Global_Job_Quotation.Helper
{
    public  class HelperLookup
    {
       // private readonly IConfiguration _configuration;
        private  AdditionalCost _additionalCost;

       
        public HelperLookup(IOptions<AdditionalCost> additionalCost)
        {
               
            _additionalCost = additionalCost.Value;
           
        }
        public  double GetCostWithTax(double price)
        {
            if(price > 0)
            {
                price = price * _additionalCost.Tax;

                var costWithTax = Math.Round(price, 2);


                return costWithTax;
            }
            else
            {
                return 0;
            }
           
        }

        public  double GetCostWithMargin(double price)
        {
            if (price > 0)
            {
                price = price * _additionalCost.Margin;

                var costWithMargin = Math.Round(price, 2);


                return costWithMargin;
            }
            else
            {
                return 0;
            }

        }

        public  double GetCostWithExtraMargin(double price)
        {
            if (price > 0)
            {
                price = price * (_additionalCost.ExtratraMargin + _additionalCost.Margin);

                var costWithExtraMargin = Math.Round(price, 2);


                return costWithExtraMargin;
            }
            else
            {
                return 0;
            }

        }

        public double RoundUpToNearestEvenCent(double price)
        {
            var roundUpVlaue = Math.Round(price, 2,MidpointRounding.ToEven);
            return roundUpVlaue;

        }

        public double RoundUpToNearestCent(double price)
        {
            var roundUpVlaue = Math.Round(price, 2);
            return roundUpVlaue;

        }


    }
}
