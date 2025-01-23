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
      
        Task<int> CreateNewFortunes(int fortunesToCreate = 1);
        Task<List<FortuneModel>> GetFortunes(int fortunesToGet = 1);
        Task<int> ClaimAndGenerateFortunes(List<Guid> usedFortunes);
    }
}
