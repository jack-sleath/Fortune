using Fortune.Models.Enums;
using Fortune.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fortune.Services {
    public class ShortFormService : IShortFormService {
        public Task<string> GetImageTopics(EFortuneType eFortuneType, string longFortune) {
            throw new NotImplementedException();
        }

        public Task<string> GetLongFortune(EFortuneType eFortuneType) {
            throw new NotImplementedException();
        }

        public Task<string> GetShortFortune(EFortuneType eFortuneType, string longFortune) {
            throw new NotImplementedException();
        }
    }
}
