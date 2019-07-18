using Contracts;
using Contracts.DTO;
using Contracts.Extensions;
using Contracts.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AnagramGenerator.EF.DatabaseFirst.Repositories
{
    public class WordsRepository : IWordsRepository
    {
        public bool AddWord(Word word)
        {
            throw new NotImplementedException();
        }

        public bool AddWords(params Word[] words)
        {
            throw new NotImplementedException();
        }

        public Word GetWord(int id)
        {
            throw new NotImplementedException();
        }

        public IList<Word> GetWords()
        {
            throw new NotImplementedException();
        }
    }
}
