﻿using AutoMapper;
using Restaurant.Services.ProductsAPI.DbContexts.Models;
using Restaurant.Services.ProductsAPI.DbContexts.Models.Dtos;

namespace Restaurant.Services.ProductsAPI
{
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mappingConfig = new MapperConfiguration(config =>
            {
                config.CreateMap<ProductDto, Product>();
                config.CreateMap<Product, ProductDto>();
            });
            return mappingConfig;
        }
    }
}
