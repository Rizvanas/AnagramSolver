using Implementation;
using System.Text;

namespace MaipApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var words = string.Join(' ', args);

            var wordRepository = new WordRepository(new TxtWordLoader());
            var anagramSolver = new AnagramSolver(wordRepository);
            var wordai = wordRepository.GetWords();
            var anagrams = anagramSolver.GetAnagrams(words);
        }
    }
}
