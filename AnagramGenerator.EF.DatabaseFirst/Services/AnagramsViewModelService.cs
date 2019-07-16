using Contracts;
using Contracts.DTO;
using Contracts.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AnagramGenerator.EF.DatabaseFirst.Services
{
    public class AnagramsViewModelService : IAnagramsViewModelService
    {
        private readonly IAnagramSolver _anagramSolver;

        public AnagramsViewModelService(IAnagramSolver anagramsSolver)
        {
            _anagramSolver = anagramsSolver;
        }

        public List<Anagram> GetAnagrams(string phrase, string ip)
        {
            return _anagramSolver
                .GetAnagrams(phrase, ip)
                .Select(a => new Anagram { Text = a.Anagram })
                .ToList();
        }
    }
}
