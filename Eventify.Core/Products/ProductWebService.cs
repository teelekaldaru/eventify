using Eventify.Common.Classes.Logger;
using Eventify.Common.Classes.Messages.RequestResult;
using Eventify.Common.Classes.Products;
using Eventify.Core.Base.Services;
using Eventify.DAL.Products;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Eventify.Core.Products
{
    public interface IProductWebService
    {
        Task<RequestResult<IEnumerable<Product>>> GetProducts();
    }

    internal class ProductWebService : BaseWebService, IProductWebService
    {
        private readonly IProductRepository _productRepository;

        public ProductWebService(
            ILogger logger,
            IProductRepository productRepository) : base(logger)
        {
            _productRepository = productRepository;
        }

        public async Task<RequestResult<IEnumerable<Product>>> GetProducts()
        {
            try
            {
                var products = await _productRepository.GetProducts();
                return RequestResult<IEnumerable<Product>>.CreateSuccess(products);
            }
            catch (Exception e)
            {
                return HandleException<IEnumerable<Product>>(e);
            }
        }
    }
}
