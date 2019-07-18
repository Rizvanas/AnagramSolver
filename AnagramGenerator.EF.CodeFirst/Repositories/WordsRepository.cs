using Contracts.DTO;
using Contracts.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace AnagramGenerator.EF.CodeFirst.Repositories
{
    public class WordsRepository : IWordsRepository
    {
        public void AddWord(Word word)
        {
            throw new NotImplementedException();
        }

        public void AddWords(params Word[] words)
        {
            throw new NotImplementedException();
        }

        public void DeleteWord(int id)
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
