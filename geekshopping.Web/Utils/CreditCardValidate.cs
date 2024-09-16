using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace geekshopping.Web.Utils
{
    public static class CreditCardValidate
    {
        public static ValidationResult ValidateExpiryDate(string expireMothYear, ValidationContext context)
        {
            if (string.IsNullOrEmpty(expireMothYear))
            {
                return new ValidationResult("Expiration date is required.");
            }

            // Verificar o formato MM/YY
            var regex = new Regex(@"^(0[1-9]|1[0-2])\/([0-9]{2})$");
            if (!regex.IsMatch(expireMothYear))
            {
                return new ValidationResult("Invalid format. Use MM/YY.");
            }

            // Separar o mês e o ano
            var parts = expireMothYear.Split('/');
            int month = int.Parse(parts[0]);
            int year = int.Parse("20" + parts[1]);  // Para tratar o ano como 20XX

            // Verificar se a data é válida
            DateTime now = DateTime.Now;
            if (year < now.Year || (year == now.Year && month < now.Month))
            {
                return new ValidationResult("Expiration date cannot be in the past.");
            }

            // Se a data for maior que 5 anos no futuro (ou outra lógica específica que você desejar)
            if (year > now.Year + 5)
            {
                return new ValidationResult("Expiration date cannot be more than 5 years in the future.");
            }

            return ValidationResult.Success;
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

        public static string GetCreditCardIcons(string cardNumber)
        {
            
            var visaRegex = new Regex(@"^4[0-9]{12}(?:[0-9]{3})?$");
            var mastercardRegex = new Regex(@"^(5[1-5][0-9]{14}|2[2-7][0-9]{14})$");
            var amexRegex = new Regex(@"^3[47][0-9]{13}$");
            var discoverRegex = new Regex(@"^6(?:011|5[0-9]{2}|4[4-9][0-9]|22[1-9][0-9]{2})[0-9]{12}$");
            var dinersClubRegex = new Regex(@"^3(?:0[0-5]|[68][0-9])[0-9]{11}$");
            var jcbRegex = new Regex(@"^35(?:2[89]|[3-8][0-9])[0-9]{12}$");
       
            if (visaRegex.IsMatch(cardNumber))
                return "fa fa-cc-visa";
            if (mastercardRegex.IsMatch(cardNumber))
                return "fa fa-cc-mastercard";
            if (amexRegex.IsMatch(cardNumber))
                return "fa fa-cc-amex";
            if (discoverRegex.IsMatch(cardNumber))
                return "fa fa-cc-discover";
            if (dinersClubRegex.IsMatch(cardNumber))
                return "fa fa-cc-diners-club";
            if (jcbRegex.IsMatch(cardNumber))
                return "fa fa-cc-jcb";

            return "fa fa-credit-card";
        }

    }
}
