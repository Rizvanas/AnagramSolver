using System;
using System.Collections.Generic;

namespace Contracts.Entities
{
    public partial class UserLogEntity
    {
        public string UserIp { get; set; }
        public int SearchPhraseId { get; set; }
        public int SearchTime { get; set; }
        public int Id { get; set; }

        public virtual PhraseEntity SearchPhrase { get; set; }
    }
}
