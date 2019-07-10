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
        private readonly SqlCommand _command;
        private readonly IAppConfig _appConfig;

        public SqlWordRepository(IAppConfig appConfig)
        {
            _appConfig = appConfig;
            _connection = new SqlConnection {ConnectionString = "Server=localhost\\SQLEXPRESS;Database=WordsDB;Trusted_Connection=True;"};
            _command = new SqlCommand
            {
                Connection = _connection,
                CommandType = CommandType.Text
            };
        }

        public bool AddCachedWord(Word word, List<string> anagrams)
        {
            _command.CommandText = "INSERT INTO WordsDB.dbo.Phrases(Phrase) VALUES(@phrase)";
            _command.Parameters.AddWithValue("@phrase", word); 

        }

        public bool AddWord(Word word)
        {
            throw new NotImplementedException();
        }

        public Word GetCachedWord(string phrase)
        {
            _command.CommandText = "SELECT * FROM WordsDB.dbo.Words";
            throw new NotImplementedException();
        }

        public IEnumerable<Word> GetWords(PaginationFilter filter)
        {
            var words = new List<Word>();
            try
            {
                if (filter == null)
                {
                    _command.CommandText = "SELECT * FROM WordsDB.dbo.Words";
                }
                else
                {
                    _command.CommandText = 
                        "SELECT * FROM WordsDB.dbo.Words " +
                        "ORDER BY WordsDB.dbo.Words.Word " +
                        "OFFSET (@page - 1) * @pageSize ROWS " + 
                        "FETCH NEXT @pageSize ROWS ONLY";

                    _command.Parameters.AddWithValue("@page", filter.Page);
                    _command.Parameters.AddWithValue("@pageSize", filter.PageSize);
                }

                _connection.Open();
                SqlDataReader reader = _command.ExecuteReader();
                while (reader.Read())
                {
                    words.Add(new Word { Text = reader.GetString(1) });
                }
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                _connection.Close();
            }

            return words;
        }

        public bool PutWords(string words)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Word> SearchWords(string phrase)
        {
            var minWordLen = Convert.ToInt32(_appConfig.GetConfiguration()
                .GetSection("ConstantValues")["MinWordLength"]);

            _command.CommandText = "SELECT * FROM WordsDB.dbo.Words " +
                                   "WHERE LEN(WordsDB.dbo.Words.Word) <= LEN(@phrase) " +
                                   "AND LEN(WordsDB.dbo.Words.Word) >= @minLen";

            _command.Parameters.AddWithValue("@phrase", phrase);
            _command.Parameters.AddWithValue("@minLen", minWordLen );

            var searchWords = new List<Word>();
            try
            {
                _connection.Open();
                SqlDataReader reader = _command.ExecuteReader();
                while (reader.Read())
                {
                    searchWords.Add(new Word { Text = reader.GetString(1) });
                }
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                _connection.Close();
            }

            var searchWord = phrase.Replace(" ", "");

            return searchWords
                .Where(w => phrase.GetSearchWord(w.Text) != searchWord)
                .OrderByDescending(w => w.Text.Length);
        }
    }
}
