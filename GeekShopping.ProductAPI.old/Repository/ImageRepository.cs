using MongoDB.Driver;
using GeekShopping.ProductAPI.Data.ValueObjects;
using GeekShopping.ProductAPI.Data;

namespace GeekShopping.ProductAPI.Repository
{
    public class ImageRepository : IImageRepository
    {
        private readonly IMongoCollection<ImageVO> _imageCollection;

        public ImageRepository(MongoDbContext mongoDbContext)
        {
            _imageCollection = mongoDbContext.Images;
        }

        public async Task<List<ImageVO>> GetByIdAsync(string id)
        {
           var filter = Builders<ImageVO>.Filter.Eq(img => img.IdProduct, id);
           return await _imageCollection.Find(filter).ToListAsync();                       
        }

        public async Task<IEnumerable<ImageVO>> GetAllAsync()
        {
            return await _imageCollection.Find(_ => true).ToListAsync();
        }

        public async Task<IEnumerable<ImageVO>> CreateImages(IEnumerable<ImageVO> images)
        {
            await _imageCollection.InsertManyAsync(images);
            return images;
        }

        public Task<ImageVO> UpdateImage(ImageVO image)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteImage(string id)
        {
            var ret = _imageCollection.DeleteOne(img => img._Id == id);
            if (ret != null) return Task.FromResult(true);
            return Task.FromResult(false);
        }
    }
}
