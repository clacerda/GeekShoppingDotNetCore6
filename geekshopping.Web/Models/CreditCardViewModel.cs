using System.ComponentModel.DataAnnotations;

namespace geekshopping.Web.Models
{
    public class CreditCardViewModel
    {
        [Required(ErrorMessage = "CVV is required!")]
        [StringLength(4, MinimumLength = 3, ErrorMessage = "CVV most have between 3 and 4 numbers!")]
        [RegularExpression(@"^\d{3,4}$", ErrorMessage = "CVV most have only numbers.")]
        public string CVV { get; set; }
        public string? CardType { get; set; }

        [RegularExpression(@"^\d{3,4}$", ErrorMessage = "Card number most have only numbers.")]
        public string CardNumber { get; set; }

        [Required(ErrorMessage = "Expire date is necessary.")]
        [RegularExpression(@"\d{2}/\d{2}", ErrorMessage = "Has been an error -> Format right (MM/YY).")]
        [StringLength(5, MinimumLength = 5, ErrorMessage = "Has been an error -> Format right (MM/YY).")]
        public string ExpireMothYear { get; set; }
    }
}
