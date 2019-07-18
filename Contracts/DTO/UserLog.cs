using System;
using System.Collections.Generic;
using System.Text;

namespace Contracts.DTO
{
    public class UserLog
    {
        public int Id { get; set; }
        public string UserIp { get; set; }
        public string Anagram { get; set; }
        public string SearchPhrase { get; set; }
        public int SearchTime { get; set; }
    }
}
