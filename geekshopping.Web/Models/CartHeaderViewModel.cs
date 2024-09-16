using System.ComponentModel.DataAnnotations;
using geekshopping.Web.Utils;

namespace GeekShopping.web.Models
{
    public class CartHeaderViewModel
    {
        public long Id { get; set; }
        public string UserId { get; set; }
        public string? CouponCode { get; set; }
        public decimal PurchaseAmount { get; set; }
        public decimal DiscountTotal { get; set; }
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "The first name must contain only letters and spaces.")]
        [Required(ErrorMessage = "First name is required.")]
        public string FirstName { get; set; }

        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "The last name must contain only letters and spaces.")]
        [Required(ErrorMessage = "Last name is required.")]
        public string LastName { get; set; }
        public DateTime Time { get; set;}

        [Required(ErrorMessage = "Phone number is required.")]
        public string Phone { get; set; }

        [EmailAddress(ErrorMessage = "Invalid email format.")]
        [Required(ErrorMessage = "E-mail is required.")]
        public string Email { get; set; }

        [StringLength(19, MinimumLength = 13, ErrorMessage = "Card Number feel wrong. Verify your number card and try now.")]
        [RegularExpression(@"^\d{13,19}$", ErrorMessage = "Card number must contain between 13 and 19 digits and consist only of numbers.")]
        public string CardNumber { get; set; }

        [Required(ErrorMessage = "CVV is required.")]
        [StringLength(4, MinimumLength = 3, ErrorMessage = "CVV must be between 3 and 4 digits.")]
        [RegularExpression(@"^\d{3,4}$", ErrorMessage = "CVV must consist only of numbers.")]
        public string CVV { get; set; }

        [Required(ErrorMessage = "Expiration date is required.")]
        [RegularExpression(@"\d{2}/\d{2}", ErrorMessage = "Invalid format. Use MM/YY.")]
        [StringLength(5, MinimumLength = 5, ErrorMessage = "Invalid expiration date format. Use MM/YY.")]
        public string ExpireMothYear { get; set; }
    }
}
