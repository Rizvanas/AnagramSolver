using Contracts;
using Contracts.DTO;
using Contracts.Extensions;
using Contracts.Repositories;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace AnagramGenerator.RawSQL.Repositories
{
    public class WordsRepositorySQL : IWordsRepository
    {
        private readonly SqlConnection _connection;
        private readonly IAppConfig _appConfig;

        public WordsRepositorySQL(IAppConfig appConfig)
        {
            _appConfig = appConfig;
            _connection = new SqlConnection
            { ConnectionString = _appConfig.GetConnectionString() };
        }

        public void AddWord(Word word)
        {
            var wordInsertRequest = "INSERT INTO Words VALUES(@word)";

            using (var command = new SqlCommand(wordInsertRequest, _connection)
            { CommandType = CommandType.Text })
            {
                command.Parameters.AddWithValue("@word", word.Text);

                command.Connection.Open();
                command.ExecuteNonQuery();
                command.Connection.Close();
            }
        }

        public void AddWords(params Word[] words)
        {
            throw new NotImplementedException();
        }

        public void DeleteWord(int id)
        {
            throw new NotImplementedException();
        }
        
        /*public IList<Word> GetSearchWords(PhraseEntity phrase)
        {
            var wordsQuery = new StringBuilder()
                .Append("SELECT Word ")
                .Append("FROM Words ")
                .Append("WHERE LEN(Words.Word) <= LEN(@phrase) ")
                .Append("AND LEN(Words.Word) >= @minLen;")
                .ToString();

            var minWordLen = Convert.ToInt32(_appConfig.GetConfiguration()
                .GetSection("ConstantValues")["MinWordLength"]);

            using (var command = new SqlCommand(wordsQuery, _connection)
            { CommandType = CommandType.Text })
            {
                command.Parameters.AddWithValue("@phrase", phrase.Phrase);
                command.Parameters.AddWithValue("@minLen", minWordLen);

                command.Connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    var searchWords = new List<WordEntity>();
                    while (reader.Read())
                        searchWords.Add(new WordEntity { Word = reader.GetString(0) });

                    command.Connection.Close();
                    return searchWords
                        .Where(w => phrase.Phrase.GetSearchWord(w.Word) != phrase.Phrase)
                        .OrderByDescending(w => w.Word.Length).ToList();
                }
            }
        }*/
/*
        public WordEntity GetWord(int id)
        {
            var wordsQuery = new StringBuilder()
                .Append("SELECT Word")
                .Append("FROM Words")
                .Append("WHERE WordId = @id")
                .ToString();

            using (var command = new SqlCommand(wordsQuery, _connection)
            { CommandType = CommandType.Text })
            {
                command.Parameters.AddWithValue("@id", id);

                command.Connection.Open();
                var word = command.ExecuteScalar() as WordEntity;
                command.Connection.Close();

                return word;
            }
        }

        public WordEntity GetWord(string word)
        {
            var wordsQuery = new StringBuilder()
                .Append("SELECT Word")
                .Append("FROM Words")
                .Append("WHERE LOWER(Word) = LOWER(@word)")
                .ToString();

            using (var command = new SqlCommand(wordsQuery, _connection)
            { CommandType = CommandType.Text })
            {
                command.Parameters.AddWithValue("@word", word);

                command.Connection.Open();
                var result = command.ExecuteScalar() as WordEntity;
                command.Connection.Close();

                return result;
            }
        }

        public IEnumerable<WordEntity> GetWords()
        {
            var wordsQuery = "SELECT Word FROM Words";
            using (var command = new SqlCommand(wordsQuery, _connection)
            { CommandType = CommandType.Text })
            {
                command.Connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    var words = new List<WordEntity>();
                    while (reader.Read())
                        words.Add(new WordEntity { Word = reader.GetString(0) });

                    command.Connection.Close();
                    return words;
                }
            }
        }

        public IEnumerable<WordEntity> GetWords(PaginationFilter filter)
        {
            var wordsQuery = new StringBuilder()
                .Append("SELECT Word ")
                .Append("FROM Words ")
                .Append("ORDER BY Words.Word ")
                .Append("OFFSET(@page - 1) * @pageSize ROWS ")
                .Append("FETCH NEXT @pageSize ROWS ONLY;")
                .ToString();

            filter.Page = filter.Page ?? 1;

            using (var command = new SqlCommand(wordsQuery, _connection)
            { CommandType = CommandType.Text })
            {
                command.Parameters.AddWithValue("@page", filter.Page);
                command.Parameters.AddWithValue("@pageSize", filter.PageSize);

                command.Connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    var words = new List<WordEntity>();
                    while (reader.Read())
                        words.Add(new WordEntity { Word = reader.GetString(0) });

                    command.Connection.Close();
                    return words;
                }
            }
        }

        public IEnumerable<WordEntity> GetWords(PhraseEntity phrase)
        {
            var wordsQuery = new StringBuilder()
                .Append("SELECT Word ")
                .Append("FROM Words ")
                .Append("WHERE LOWER(Word) LIKE LOWER(@searchPhrase) + '%';")
                .ToString();

            using (var command = new SqlCommand(wordsQuery, _connection)
            { CommandType = CommandType.Text })
            {
                command.Parameters.AddWithValue("@searchPhrase", phrase.Phrase);

                command.Connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    var words = new List<WordEntity>();
                    while (reader.Read())
                        words.Add(new WordEntity { Word = reader.GetString(0) });

                    command.Connection.Close();
                    return words;
                }
            }

        }*/

        Word IWordsRepository.GetWord(int id)
        {
            throw new NotImplementedException();
        }

        IList<Word> IWordsRepository.GetWords()
        {
            throw new NotImplementedException();
        }
    }
}
