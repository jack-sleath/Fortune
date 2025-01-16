using Fortune.Models.Enums;
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

        public async Task<List<FortuneModel>> GetFortunes(int fortunesToGet)
        {
            var filter = Builders<MongoDbFortuneModel>.Filter.Eq(f => f.FortuneUsed, false);

            var mongoFortunes = await _mongoCollection.Find(filter).ToListAsync();

            return mongoFortunes.Select(MongoDbFortuneModelMapper.ToFortuneModel).ToList();
        }

        public async Task<bool> MarkFortuneRead(Guid id)
        {
            var filter = Builders<MongoDbFortuneModel>.Filter.Eq(f => f.MongoId, id);
            var update = Builders<MongoDbFortuneModel>.Update
                .Set(f => f.FortuneUsed, true);

            await _mongoCollection.UpdateOneAsync(filter, update);
            return true;
        }

        public async Task<bool> SaveFortune(FortuneModel fortuneModel)
        {
            var mongoModel = MongoDbFortuneModelMapper.ToMongoDBModel(fortuneModel);
            await _mongoCollection.InsertOneAsync(mongoModel);
            return true;
        }
    }
}
