using Contracts.DTO;
using Contracts.Entities;
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

        public IEnumerable<Word> GetSearchWords(Phrase phrase)
        {
            throw new NotImplementedException();
        }

        public Word GetWord(int id)
        {
            throw new NotImplementedException();
        }

        public Word GetWord(string word)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Word> GetWords()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Word> GetWords(PaginationFilter filter)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Word> GetWords(Phrase phrase)
        {
            throw new NotImplementedException();
        }
    }
}
