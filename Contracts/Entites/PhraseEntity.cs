using System;
using System.Collections.Generic;

namespace Contracts.Entities
{
    public partial class PhraseEntity
    {
        public PhraseEntity()
        {
            CachedWords = new HashSet<CachedWordEntity>();
            UserLog = new HashSet<UserLogEntity>();
        }

        public int Id { get; set; }
        public string Phrase { get; set; }

        public virtual ICollection<CachedWordEntity> CachedWords { get; set; }
        public virtual ICollection<UserLogEntity> UserLog { get; set; }
    }
}
