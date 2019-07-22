using System;
using System.Collections.Generic;

namespace AnagramGenerator.EF.CodeFirst.Entities
{
    public partial class AnagramEntity
    {
        public AnagramEntity()
        {
            CachedWords = new HashSet<CachedWordEntity>();
            UserLogs = new HashSet<UserLogEntity>();
        }

        public int Id { get; set; }
        public string Anagram { get; set; }

        public virtual ICollection<CachedWordEntity> CachedWords { get; set; }
        public virtual ICollection<UserLogEntity> UserLogs { get; set; }
    }
}
