using System;
using System.Collections.Generic;
using System.Text;

namespace Core.DTO
{
    public class UserLog
    {
        public long SearchTime { get; set; }
        public string SearchPhrase { get; set; }
        public string UserIP { get; set; }
    }
}
