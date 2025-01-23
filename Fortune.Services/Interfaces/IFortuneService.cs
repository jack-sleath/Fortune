using Fortune.Models.SaveObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fortune.Services.Interfaces
{
    public interface IFortuneService
    {
        bool SaveUsedFortune();
        Task<bool> CreateNewFortune();
        Task<List<FortuneModel>> GetFortunes();
        Task<int> ClaimAndGenerateFortunes(List<Guid> usedFortunes);
    }
}
