using Core.Domain;
using Interfaces;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Implementation.Tests
{
    [TestFixture]
    public class AnagramSolverTests
    {
        private readonly AnagramSolver _anagramSolver = new AnagramSolver(
            new List<Word>
            {
                new Word { Text = "Labas", Type="bdv" },
                new Word { Text = "Laibas", Type="bdv" },
                new Word { Text = "Lakta", Type="daikt" },
                new Word { Text = "Vilnius", Type="tikr. daikt" },
                new Word { Text = "Miestas", Type="bdv" },
                new Word { Text = "Silute", Type="bdv" },
                new Word {Text = "Sula", Type="daikt" }
            });

        [Test]
        public void GetAnagrams_WordContainsLithuanianChars_ResultCountIs0()
        {
            var testAnagrams = _anagramSolver.GetAnagrams("Šilutė");

            Assert.IsEmpty(testAnagrams);
        }

        [Test]
        public void GetAnagrams_WordIsSmallerThan5_ResultCountIs0 ()
        {
            var testAnagrams = _anagramSolver.GetAnagrams("Šilutė");

            Assert.IsEmpty(testAnagrams);
        }

        [Test]
        public void GetAnagrams_WordValueIsAlus_ResultCountIs1()
        {
            var testAnagrams = _anagramSolver.GetAnagrams("Alus");

            Assert.AreEqual(testAnagrams.Count, 1);
        }

        [Test]
        public void GetAnagrams_WordValueIsAlus_ResultIsSula()
        {
            var testAnagrams = _anagramSolver
                .GetAnagrams("Alus").Select(a => String.Join(' ', a.Select(t => t.Text)))
                .ToList();

            Assert.AreEqual(testAnagrams[0].ToLower(), "sula");
        }

        [Test]
        public void GetAnagrams_WordValueIsEmptyString_ResultIsEmptyString()
        {
            var testAnagrams = _anagramSolver
                .GetAnagrams("").Select(a => String.Join(' ', a.Select(t => t.Text)))
                .ToList();

            Assert.AreEqual(testAnagrams[0].ToLower(), "");
        }

        [Test]
        public void GetAnagrams_WordValueIsNull_ResultCOuntIs0()
        {
            Assert.That(() => _anagramSolver.GetAnagrams(null), Throws.ArgumentNullException);
        }

    }
}
