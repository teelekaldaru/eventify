using Eventify.Core.Products;
using Microsoft.AspNetCore.Mvc;

namespace Eventify.Web.Controllers
{
    [Route("api/products")]
    public class ProductController : ControllerBase
    {

        private readonly IProductWebService _productWebService;

        public ProductController(IProductWebService productWebService)
        {
            _productWebService = productWebService;
        }

        [HttpGet]
        public async Task<JsonResult> GetProducts()
        {
            var result = await _productWebService.GetProducts();
            return new JsonResult(result);
        }
    }
}
