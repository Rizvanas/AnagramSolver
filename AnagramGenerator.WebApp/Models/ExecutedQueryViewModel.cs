using Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AnagramGenerator.WebApp.Models
{
    public class ExecutedQueryViewModel
    {
        public string UserIp { get; set; }
        public int SearchTime { get; set; }
        public string SearchPhrase { get; set; }
        public List<Word> Anagrams { get; set; }
    }
}
