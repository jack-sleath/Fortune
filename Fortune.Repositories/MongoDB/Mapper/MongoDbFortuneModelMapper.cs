using Fortune.Models.SaveObject;
using Fortune.Repositories.MongoDB.MappedSaveObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fortune.Repositories.MongoDB.Mapper
{
    public class MongoDbFortuneModelMapper
    {
        public static MongoDbFortuneModel ToMongoDBModel(FortuneModel model)
        {
            return new MongoDbFortuneModel
            {
                Id = model.id,
                LongFortune = model.LongFortune,
                ShortFortune = model.ShortFortune,
                ImageTopics = model.ImageTopics,
                QrImage = model.QrImage,
                Audio = model.Audio,
                FortuneImage = model.FortuneImage,
                LuckyNumbers = model.LuckyNumbers,
                FortuneUsed = model.FortuneUsed,
                FortuneType = model.FortuneType
            };
        }

        public static FortuneModel ToFortuneModel(MongoDbFortuneModel mongoModel)
        {
            return new FortuneModel
            {
                id = mongoModel.Id,
                LongFortune = mongoModel.LongFortune,
                ShortFortune = mongoModel.ShortFortune,
                ImageTopics = mongoModel.ImageTopics,
                QrImage = mongoModel.QrImage,
                Audio = mongoModel.Audio,
                FortuneImage = mongoModel.FortuneImage,
                LuckyNumbers = mongoModel.LuckyNumbers,
                FortuneUsed = mongoModel.FortuneUsed,
                FortuneType = mongoModel.FortuneType
            };
        }
    }
}
