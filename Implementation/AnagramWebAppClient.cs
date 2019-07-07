﻿using Core.Domain;
using Interfaces;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;

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

        public List<string> GetAnagrams(string word)
        {
            _httpClient.DefaultRequestHeaders.Add("word", word);

            var anagramsResponse = _httpClient
                .GetAsync("anagrams").Result.Content
                .ReadAsStringAsync().Result;

            var anagrams = JArray.Parse(anagramsResponse)
                .Select(jt => jt.ToString())
                .ToList();

            return anagrams;
        }
    }
}
