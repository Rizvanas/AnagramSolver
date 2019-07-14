using Core.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.DTO.Responses
{
    public class UserLogResponse
    {
        public string UserIp { get; set; }
        public int SearchTime { get; set; }
        public string SearchPhrase { get; set; }
        public Word Anagram { get; set; }
    }
}
