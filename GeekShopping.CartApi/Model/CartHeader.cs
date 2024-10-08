﻿using GeekShopping.CartApi.Model.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace GeekShopping.CartApi.Model
{
    [Table("cart_header")]
    public class CartHeader : BaseEntity
    {
        [Column("user_id")]
        public string userId { get; set; }
        [Column("coupon_code")]
        public string? CouponCode { get; set; }
    }
}
