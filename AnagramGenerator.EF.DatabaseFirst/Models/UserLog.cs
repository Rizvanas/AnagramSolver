using System;
using System.Collections.Generic;

namespace AnagramGenerator.EF.DatabaseFirst.Models
{
    public partial class UserLog
    {
        public string UserIp { get; set; }
        public int SearchPhraseId { get; set; }
        public int SearchTime { get; set; }
        public int Id { get; set; }

        public Phrases SearchPhrase { get; set; }
    }
}
