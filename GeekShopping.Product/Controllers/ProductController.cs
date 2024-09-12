using GeekShopping.ProductAPI.Data.ValueObjects;
using GeekShopping.ProductAPI.Repository;
using GeekShopping.ProductAPI.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GeekShopping.ProductAPI.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private IProductRepository _repository;
        private IImageRepository _imageRepository;
       
        public ProductController(IProductRepository repository, IImageRepository imageRepository)
        {
            _repository = repository ?? throw new  ArgumentNullException(nameof(repository));
            _imageRepository = imageRepository ?? throw new ArgumentNullException(nameof(imageRepository));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductVO>>> FindAll(long id)
        {
            var product = await _repository.FindAll();
            if (product == null) return NotFound();

            return Ok(product);
        }

        [HttpGet ("{id}")]
        [Authorize]
        public async Task<ActionResult<ProductVO>> FindById(long id)
        {
            var products = await _repository.FindById(id);
            if (products.Id <= 0) return NotFound();

            return Ok(products);
        }

        
        [HttpPost]
        [Authorize]
        public async Task<ActionResult<ProductVO>> Create([FromBody] ProductVO productVO)
        {
             if (productVO == null) return BadRequest();
             var product = await _repository.Create(productVO);

            return Ok(product);
        }


        [HttpPut]
        [Authorize]
        public async Task<ActionResult<ProductVO>> Update([FromBody] ProductVO productVO)
        {
            if (productVO == null) return BadRequest();
            var product = await _repository.Update(productVO);

            return Ok(product);
        }

       
        [HttpDelete ("{id}")]
        [Authorize(Roles = Role.Admin)]
        public async Task<ActionResult> Delete(long id)
        {
            var status = await _repository.Delete(id);

            if (!status) return BadRequest(); 

            return Ok(status);
        }



        //[HttpGet("GetImageBase/{id}")]
        //public async Task<ActionResult<ImageVO>> GetImageBase(string id)
        //{
        //    var images = await _imageRepository.GetByIdAsync(id);
        //    if (images == null) return NotFound();

        //    return Ok(images);
        //}

        //[HttpGet("GetImageBase")]
        //public async Task<ActionResult<ImageVO>> GetImageBaseAll()
        //{
        //    var images = await _imageRepository.GetAllAsync();
        //    if (images == null) return NotFound();

        //    return Ok(images);
        //}


        //[HttpPost("PostImageBase")]
        //public async Task<ActionResult<ProductVO>> CreateImage([FromBody] IEnumerable<ImageVO> imageVO)
        //{
        //    if (imageVO == null) return BadRequest();
        //    var product = await _imageRepository.CreateImages(imageVO);

        //    return Ok(product);
        //}

        //[HttpDelete("DeleteImageBase/{id}")] 
        //public async Task<ActionResult> DeleteImage(string id)
        //{
        //    var status = await _imageRepository.DeleteImage(id);

        //    if (!status) return BadRequest();

        //    return Ok(status);
        //}


    }
}
