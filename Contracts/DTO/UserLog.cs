using System;
using System.Collections.Generic;
using System.Text;

namespace Contracts.DTO
{
    public class UserLog
    {
        public int Id { get; set; }
        public string UserIp { get; set; }
        public Anagram Anagram { get; set; }
        public Phrase SearchPhrase { get; set; }
        public int SearchTime { get; set; }
    }
}
