using AutoMapper;
using GeekShopping.ProductAPI.Context;
using GeekShopping.ProductAPI.Data.ValueObjects;
using GeekShopping.ProductAPI.Model;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
using System.Collections.Generic;
using static System.Net.Mime.MediaTypeNames;

namespace GeekShopping.ProductAPI.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly MySqlContext _context;
        private readonly IImageRepository _imageRepository;
        private readonly IMapper _mapper;

        public ProductRepository(MySqlContext context, IImageRepository imageRepository, IMapper mapper)
        {
            _context = context;
            _imageRepository = imageRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ProductVO>> FindAll()
        {
            List<Product> products = await _context.Products.ToListAsync();
            var productsList = _mapper.Map<List<ProductVO>>(products);

            foreach (var product in productsList)
            {
                var images = await _imageRepository.GetByIdAsync(product.Id.ToString());

                var count = images.Count();

                if (images != null && images.Any())
                {
                    var img = new List<ImageVO>();
                    for (int i = 0; i < count; i++)
                    {
                        img.Add(images[i]);
                    }
                    product.Images = img;
                }
            }

            return productsList;
        }



        public async Task<ProductVO> FindById(long id)
        {
            Product product = await _context.Products.Where(p => p.Id == id).FirstOrDefaultAsync() ?? new Product();
            
            var images = await _imageRepository.GetByIdAsync(product.Id.ToString());

            var addImageProduct = _mapper.Map<ProductVO>(product);
            if (images != null)
            {
                foreach (var image in images)
                {
                    addImageProduct.Images = new List<ImageVO> { image };
                }
            }
            return addImageProduct;
        }

        public async Task<ProductVO> Create(ProductVO productVO)
        {
            Product product = _mapper.Map<Product>(productVO);
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
            //Create a LIST IMAGES
            productVO.Images.ToList().ForEach(img => img.IdProduct = product.Id.ToString());
            await _imageRepository.CreateImages(productVO.Images);
            return _mapper.Map<ProductVO>(product);
        }

        public async Task<ProductVO> Update(ProductVO productVO)
        {
            Product product = _mapper.Map<Product>(productVO);
            _context.Products.Update(product);
            await _context.SaveChangesAsync();
            return _mapper.Map<ProductVO>(product);
        }

        public async Task<bool> Delete(long id)
        {
            try
            {
                Product product = await _context.Products.Where(p => p.Id == id).FirstOrDefaultAsync() ?? new Product();

                if (product.Id <= 0) return false;

                _context.Products.Remove(product);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }
     }
}
