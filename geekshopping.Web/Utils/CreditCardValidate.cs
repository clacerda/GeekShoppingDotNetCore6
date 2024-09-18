using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace geekshopping.Web.Utils
{
    public static class CreditCardValidate
    {
        public static bool ValidateExpiryDate(string expireMothYear)
        {
            if (string.IsNullOrEmpty(expireMothYear))
            {
                return false;
            }

            var regex = new Regex(@"^(0[1-9]|1[0-2])\/([0-9]{2})$");
            if (!regex.IsMatch(expireMothYear))
            {
                return false;
            }

            var parts = expireMothYear.Split('/');
            int month = int.Parse(parts[0]);
            int year = int.Parse("20" + parts[1]);

            DateTime now = DateTime.Now;
            if (year < now.Year || (year == now.Year && month < now.Month))
            {
                return false;
            }

            if (year > now.Year + 5)
            {
                return false;
            }

            return true;
        }

        public static bool IsCreditCardValid(string cardNumber)
        {
            cardNumber = cardNumber.Replace(" ", "");

            if (!cardNumber.All(char.IsDigit))
            {
                return false;
            }

            int sum = 0;
            bool isAlternate = false;

            for (int i = cardNumber.Length - 1; i >= 0; i--)
            {
                int digit = int.Parse(cardNumber[i].ToString());

                if (isAlternate)
                {
                    digit *= 2;
                    if (digit > 9)
                    {
                        digit -= 9;
                    }
                }

                sum += digit;
                isAlternate = !isAlternate;
            }
            return (sum % 10 == 0);
        }
    }
}
