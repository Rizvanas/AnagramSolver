using Contracts;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Contracts.Entities;

namespace Implementation
{
    public class AnagramWebAppClient : IAnagramWebAppClient
    {
        private readonly HttpClient _httpClient;
        public AnagramWebAppClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("http://localhost:55944");
        }

        public async Task<List<WordEntity>> GetAnagramsAsync(string word)
        {
            _httpClient.DefaultRequestHeaders.Add("word", word);

            var anagramsResponse = await _httpClient
                .GetAsync($"anagrams").Result.Content
                .ReadAsStringAsync();

            var anagrams = JArray.Parse(anagramsResponse)
                .Select(jt => new WordEntity { Word = jt.ToString() })
                .ToList();

            return anagrams;
        }

    }
}
