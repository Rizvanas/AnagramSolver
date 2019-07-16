using Contracts;
using Contracts.DTO;
using Contracts.Models;
using Contracts.Services;
using System.Linq;

namespace AnagramGenerator.EF.DatabaseFirst.Services
{
    public class AnagramsViewModelService : IAnagramsViewModelService
    {
        private readonly IAnagramSolver _anagramSolver;

        public AnagramsViewModelService(IAnagramSolver anagramsSolver)
        {
            _anagramSolver = anagramsSolver;
        }

        public AnagramsViewModel GetAnagramsViewModel(string phrase, string ip)
        {
            return new AnagramsViewModel
            {
                Phrase = new Phrase { Text = phrase ?? "" },
                Anagrams = _anagramSolver
                .GetAnagrams(phrase, ip)
                .Select(a => new Anagram { Text = a.Anagram })
                .ToList(),
            };
        }
        
    }
}
