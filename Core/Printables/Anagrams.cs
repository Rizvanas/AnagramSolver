using Core.Domain;
using Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Printables
{
    public class Anagrams : IPrintable
    {
        private readonly List<string> _anagrams;
        public Anagrams(List<string> anagrams)
        {
            _anagrams = anagrams;
        }

        public void Print()
        {
            foreach(var anagram in _anagrams)
            {
                Console.WriteLine(anagram);
            }
        }
    }
}
