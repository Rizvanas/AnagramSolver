using System;
using System.Collections.Generic;
using System.Text;

namespace Contracts.DTO
{
    public class UserLog
    {
        public int Id { get; set; }
        public User User { get; set; }
        public Anagram Anagram { get; set; }
        public Phrase Phrase { get; set; }
        public int SearchTime { get; set; }
    }
}
