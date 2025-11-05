using Fortune.Models.Enums;
using Fortune.Models.SaveObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fortune.Services.Interfaces
{
    public interface ITicketService
    {

        Task<int> CreateNewFortunes(int fortunesToCreate = 1, EFortuneType eFortuneType = EFortuneType.Generic);
        Task<List<FortuneModel>> GetFortunes(int fortunesToGet = 1);
        Task<FortuneModel> GetRandomFortune();
        Task<int> ClaimAndGenerateFortunes(List<Guid> usedFortunes);
        Task<bool> UnreadAllFortunes();
    }
}
