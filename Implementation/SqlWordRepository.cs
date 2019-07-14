using Core.Domain;
using Core.DTO;
using Implementation.Extensions;
using Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace Implementation
{
    public class SqlWordRepository : ISqlWordRepository
    {
        private readonly SqlConnection _connection;
        private readonly IAppConfig _appConfig;

        public SqlWordRepository(IAppConfig appConfig)
        {
            _appConfig = appConfig;
            _connection = new SqlConnection
            { ConnectionString = _appConfig.GetConnectionString() };
        }

        public bool AddCachedWord(Word word, List<Word> anagrams)
        {
            bool success = true;
            var phraseInsertionQuery = "INSERT INTO Phrases(Phrase) VALUES(@phrase);";
            var cacheInsertionQuery = new StringBuilder()
                .Append("INSERT INTO Anagrams(Anagram) VALUES(@anagram);")
                .Append("INSERT INTO CachedWords(PhraseId, AnagramId)")
                .Append("VALUES((SELECT Id FROM Phrases WHERE Phrase = @phrase),")
                .Append("(SELECT Id From Anagrams WHERE LOWER(REPLACE(Anagram, ' ', '')) = LOWER(REPLACE(@anagram, ' ', ''))));")
                .ToString();

            using (var command = new SqlCommand(phraseInsertionQuery, _connection)
            { CommandType = CommandType.Text })
            {
                command.Parameters.AddWithValue("@phrase", word.Text);

                command.Connection.Open();
                command.ExecuteNonQuery();


                command.CommandText = cacheInsertionQuery;
                foreach (var anagram in anagrams)
                {
                    command.Parameters.AddWithValue("@anagram", anagram.Text);
                    command.ExecuteNonQuery();
                    command.Parameters.RemoveAt(command.Parameters.Count - 1);
                }
                command.Connection.Close();
            }

            return success;
        }

        public List<Word> GetCachedAnagrams(string phrase)
        {
            var cachedWordsSelectQuery = new StringBuilder()
                .Append("SELECT Anagram ")
                .Append("FROM CachedWords ")
                .Append("JOIN Phrases ON PhraseId = Phrases.Id ")
                .Append("JOIN Anagrams ON AnagramId = Anagrams.Id ")
                .Append("WHERE LOWER(REPLACE(Phrase, ' ', '')) = LOWER(REPLACE(@phrase, ' ', ''));")
                .ToString();
                                         
            using (var command = new SqlCommand(cachedWordsSelectQuery, _connection)
            { CommandType = CommandType.Text })
            {
                command.Parameters.AddWithValue("@phrase", phrase);
                command.Connection.Open();

                using (var reader = command.ExecuteReader())
                {
                    var anagrams = new List<Word>();
                    while (reader.Read())
                        anagrams.Add(new Word { Text = reader.GetString(0) });

                    command.Connection.Close();

                    return anagrams;
                }
            }
        }

        public IEnumerable<Word> GetWords(PaginationFilter filter)
        {
            filter.Page = filter.Page ?? 1;
            using (var command = new SqlCommand(filter == null ? "GetWords" : "GetPaginizedWords", _connection)
            { CommandType = CommandType.StoredProcedure }) 
            {
                if (filter != null)
                {
                    command.Parameters.AddWithValue("@page", filter.Page);
                    command.Parameters.AddWithValue("@pageSize", filter.PageSize);
                }

                command.Connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    var words = new List<Word>();
                    while (reader.Read())
                        words.Add(new Word { Text = reader.GetString(1) });

                    command.Connection.Close();
                    return words;
                }
            }
        }

        public IEnumerable<Word> SearchWords(string phrase)
        {
            var minWordLen = Convert.ToInt32(_appConfig.GetConfiguration()
                .GetSection("ConstantValues")["MinWordLength"]);

            using (var command = new SqlCommand("GetSearchWords", _connection)
            { CommandType = CommandType.StoredProcedure }) 
            {
                command.Parameters.AddWithValue("@phrase", phrase);
                command.Parameters.AddWithValue("@minLen", minWordLen);

                command.Connection.Open();
                using (var reader = command.ExecuteReader()) 
                {
                    var searchWords = new List<Word>();
                    while (reader.Read())
                        searchWords.Add(new Word { Text = reader.GetString(1) });

                    command.Connection.Close();

                    return searchWords
                        .Where(w => phrase.GetSearchWord(w.Text) != phrase)
                        .OrderByDescending(w => w.Text.Length).ToList();
                }
            }
        }

        public List<Word> GetWords(string searchPhrase)
        {
            var wordsQuery = new StringBuilder()
                .Append("SELECT Word ")
                .Append("FROM Words ")
                .Append("WHERE LOWER(Word) LIKE @searchPhrase + '%';")
                .ToString();

            using (var command = new SqlCommand(wordsQuery, _connection)
            { CommandType = CommandType.Text })
            {
                command.Parameters.AddWithValue("@searchPhrase", searchPhrase);
                command.Connection.Open();

                using (var reader = command.ExecuteReader())
                {
                    var words = new List<Word>();
                    while (reader.Read())
                        words.Add(new Word { Text = reader.GetString(0) });

                    command.Connection.Close();

                    return words;
                }
            }
        }

        public bool AddWord(Word word)
        {
            var wordInsertRequest = "INSERT INTO Words(Word) VALUES(@word)";
            var affectedLinesCount = 0;

            using (var command = new SqlCommand(wordInsertRequest, _connection)
            { CommandType = CommandType.Text })
            {
                command.Parameters.AddWithValue("@word", word.Text);

                command.Connection.Open();
                affectedLinesCount = command.ExecuteNonQuery();
                command.Connection.Close();
            }

            return affectedLinesCount != 0;
        }
    }
}
