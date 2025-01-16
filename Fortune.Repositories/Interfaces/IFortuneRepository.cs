using Fortune.Models.SaveObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fortune.Repositories.Interfaces
{
    public interface IFortuneRepository
    {
        Task<bool> MarkFortuneRead();
        Task<bool> SaveFortune(FortuneModel fortuneModel);
        Task<List<FortuneModel>> GetFortunes();
    }
}
