﻿using AutoMapper;
using GeekShopping.CouponApi.Data.Value_Objects;
using GeekShopping.CouponApi.Model;

namespace GeekShopping.CouponApi.Config
{
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mappingConfig = new MapperConfiguration(config =>
            {
                config.CreateMap<CouponVO, Coupon>().ReverseMap();
            });

            return mappingConfig;
        }
    }
}
