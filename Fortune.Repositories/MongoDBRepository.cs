using Fortune.Models.SaveObject;
using Fortune.Repositories.Interfaces;
using Fortune.Repositories.MongoDB;
using Fortune.Repositories.MongoDB.MappedSaveObject;
using Fortune.Repositories.MongoDB.Mapper;
using MongoDB.Driver;

namespace Fortune.Repositories
{
    public class MongoDBRepository : IFortuneRepository
    {

        private readonly IMongoCollection<MongoDbFortuneModel> _mongoCollection;

        public MongoDBRepository(MongoDbContext context)
        {
            _mongoCollection = context.Fortunes;
        }

        public Task<List<FortuneModel>> GetFortunes()
        {
            throw new NotImplementedException();
        }

        public Task<bool> MarkFortuneRead()
        {
            throw new NotImplementedException();
        }

        public async Task<bool> SaveFortune(FortuneModel fortuneModel)
        {
            var mongoModel = MongoDbFortuneModelMapper.ToMongoDBModel(fortuneModel);
            await _mongoCollection.InsertOneAsync(mongoModel);
            return true;
        }
    }
}
