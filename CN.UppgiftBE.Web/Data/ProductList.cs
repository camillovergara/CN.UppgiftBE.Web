using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using CN.UppgiftBE.Web.Models;
using CN.UppgiftBE.Web.Repository;
using CN.UppgiftBE.Web.Util;

namespace CN.UppgiftBE.Web.Data
{
	public interface IProductList
	{
		IReadOnlyList<Product> Get();
	}

	public class ProductList : IProductList
	{
		IReadOnlyList<Product> _products;
        private readonly IDataRepository _datarepository;
        private readonly IFormatUtils _iformatUtils;
        public ProductList(IDataRepository DataRepository, IFormatUtils FormatUtils)
		{
           _datarepository = DataRepository;
            _iformatUtils = FormatUtils;
            var productDB = _datarepository.GetProductListDB();
            var mapedProduct= Map(productDB);
            _products = mapedProduct;
        }
		public IReadOnlyList<Product> Get()=> _products;
        private List<Product> Map(List<EntityModels.ProductDB> products)
        {
            var filteredProduct = products.Select(p => p).Where(pp => !string.IsNullOrEmpty(pp.Id)).Where(n => !string.IsNullOrEmpty(n.Name)).Where(pr => !string.IsNullOrEmpty(pr.Price));

            var productItems = filteredProduct?.Select(p => new Product
            {
                Id =_iformatUtils.TryParseInt(p.Id),
                Name = p.Name,
                Price = (int)_iformatUtils.TryParseFloat(p.Price)
            }) ?? new List<Product>();

            return productItems.ToList();
        }
    }
}