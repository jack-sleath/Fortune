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
        bool MarkFortuneRead();
        bool SaveFortune();
        List<FortuneModel> GetFortunes();
    }
}
