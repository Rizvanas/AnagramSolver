using System;
using System.Collections.Generic;

namespace AnagramGenerator.EF.CodeFirst.Entities
{
    public partial class UserEntity
    {
        public UserEntity()
        {
            UserLogs = new HashSet<UserLogEntity>();
            UserWords = new HashSet<UserWordEntity>();
        }

        public int Id { get; set; }
        public string Ip { get; set; }

        public virtual ICollection<UserLogEntity> UserLogs { get; set; }
        public virtual ICollection<UserWordEntity> UserWords { get; set; }
    }
}
