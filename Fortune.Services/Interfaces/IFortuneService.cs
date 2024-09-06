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
        bool CreateNewFortune();
        List<FortuneModel> GetFortunes();
    }
}
