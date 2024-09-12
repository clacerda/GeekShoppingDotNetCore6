using GeekShopping.CouponApi.Model.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GeekShopping.CouponApi.Model
{
    [Table("Coupon")]
    public class Coupon : BaseEntity
    {

        [Column("Coupon_Code")]
        [Required]
        [StringLength(150)]
        public string CouponCode { get; set; }


        [Column("Discount_Amount")]
        [Required] 
        public decimal DiscountAmount { get; set; }

    }
}
