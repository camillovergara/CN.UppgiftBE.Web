using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using Apoteket.UppgiftBE.Web.EntityModels;
using Apoteket.UppgiftBE.Web.Models;

namespace Apoteket.UppgiftBE.Web.App_Start
{
    public class AutoMapperConfig
    {
        // ReSharper disable once InconsistentNaming
        private static readonly Lazy<AutoMapperConfig> instance = new Lazy<AutoMapperConfig>(() => new AutoMapperConfig());
        public static AutoMapperConfig Instance => instance.Value;

        public void RegisterMappings()
        {
           
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<EntityModels.Product,Models.Product>();
                
            });
            
            configuration.AssertConfigurationIsValid();
            var mapper = configuration.CreateMapper();
        }
    }
}