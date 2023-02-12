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
        public decimal GetCostWithTax(decimal price)
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

        public decimal GetCostWithMargin(decimal price)
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

        public decimal GetCostWithExtraMargin(decimal price)
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

        public decimal RoundUpToNearestEvenCent(decimal price)
        {
            //var roundUpVlaue = (0.02m / 1.00m) * decimal.Round(price * (1.00m / 0.02m));
            
           
            var roundUpVlaue = decimal.Round(price,2, MidpointRounding.ToEven);
            var priceToString = roundUpVlaue.ToString();
            var indexValue = priceToString.Length - 1;
            string lastChar = priceToString.ToCharArray()[indexValue].ToString();
            var actualValue = int.Parse(lastChar);
            if(actualValue % 2 != 0)
            {
                priceToString = priceToString.Remove(priceToString.Length - 1) + "0";
                roundUpVlaue = decimal.Parse(priceToString);
            }
            return roundUpVlaue;


        }

        public decimal RoundUpToNearestCent(decimal price)
        {
            var roundUpVlaue = Math.Round(price, 2);
            return roundUpVlaue;

        }

        public string ConvertToCurrency(decimal price)
        {
            var roundUpVlaue = Math.Round(price, 2);
            var currencyValue = String.Format("{0:c}", roundUpVlaue);
            return currencyValue;

        }


    }
}
