using Microsoft.AspNetCore.Mvc;
using Net5Api.Context;
using Net5Api.Models;
using Net5Api.ProductMaster;
using System.Threading.Tasks;

namespace Net5Api.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class ProductController : ControllerBase
    {
        private IProductService _service;

        public ProductController(IProductService service)
        {
            this._service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var pros = await _service.GetAll();
            return Ok(pros);
        }

        [HttpGet]
        public async Task<IActionResult> GetSingle(string id)
        {
            if (string.IsNullOrEmpty(id))
                return BadRequest("The field Id is empty!");


            var pro = await _service.GetSingle(id);
            if (pro == null)
                return BadRequest("Product is not exist!");

            return Ok(pro);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Product product)
        {
            if (product == null)
                return BadRequest("The Product is empty!");

            if (string.IsNullOrEmpty(product.Id))
                return BadRequest("The field Id is empty!");

            if (string.IsNullOrEmpty(product.Name))
                return BadRequest("The field Name is empty!");

            var pro = await _service.GetSingle(product.Id);
            if (pro != null)
            {
                return BadRequest("Product is exist!");
            }
            else
            {
                _service.Create(product);
                _service.SaveChanges();
                return Ok(pro);
            }
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] Product product)
        {
            if (product == null)
                return BadRequest("The Product is empty!");

            if (string.IsNullOrEmpty(product.Id))
                return BadRequest("The field Id is empty!");

            if (string.IsNullOrEmpty(product.Name))
                return BadRequest("The field Name is empty!");

            var pro = await _service.GetSingle(product.Id);
            if (pro == null)
            {
                return BadRequest("Product is not exist!");
            }
            else
            {
                pro.Name = product.Name;
                pro.Desciption = product.Desciption;
                _service.Update(pro);
                _service.SaveChanges();
                return Ok();
            }
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(string id)
        {
            if (string.IsNullOrEmpty(id))
                return BadRequest("The id is empty!");

            var pro = await _service.GetSingle(id);
            if (pro == null)
            {
                return BadRequest("Product is not exist!");
            }
            else
            {
                _service.Delete(pro);
                _service.SaveChanges();
                return Ok();
            }
        }

        [HttpGet]
        [Route("")]
        public IActionResult Status()
        {
            return Ok("The api is running!");
        }
    }
}
