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

        private readonly AnagramSolver _anagramSolver1 = new AnagramSolver
            (new TxtWordLoader { FilePath = @"..\AnagramGenerator\testzodynas1.txt" }.Load().ToList());

        private readonly AnagramSolver _anagramSolver2 = new AnagramSolver
            (new TxtWordLoader { FilePath = @"..\AnagramGenerator\testzodynas2.txt" }.Load().ToList());

        private readonly AnagramSolver _anagramSolver3 = new AnagramSolver
            (new TxtWordLoader { FilePath = @"..\AnagramGenerator\testzodynas3.txt" }.Load().ToList());

        [Test]
        public void GetAnagrams_WordContainsLithuanianChars_ResultCountIs0()
        {
            var testAnagrams = _anagramSolver1.GetAnagrams("Šilutė");

            Assert.IsEmpty(testAnagrams);
        }

        [Test]
        public void GetAnagrams_WordIsSmallerThan5_ResultCountIs0 ()
        {
            var testAnagrams = _anagramSolver1.GetAnagrams("Šilutė");

            Assert.IsEmpty(testAnagrams);
        }

        [Test]
        public void GetAnagrams_WordValueIsAlus_ResultCountIs1()
        {
            var testAnagrams = _anagramSolver1.GetAnagrams("Alus");

            Assert.AreEqual(testAnagrams.Count, 1);
        }

        [Test]
        public void GetAnagrams_WordValueIsAlus_ResultIsSula()
        {
            var testAnagrams = _anagramSolver1
                .GetAnagrams("Alus").Select(a => String.Join(' ', a.Select(t => t.Text)))
                .ToList();

            Assert.AreEqual(testAnagrams[0].ToLower(), "sula");
        }

        [Test]
        public void GetAnagrams_WordValueIsEmptyString_ResultIsEmptyString()
        {
            var testAnagrams = _anagramSolver1
                .GetAnagrams("").Select(a => String.Join(' ', a.Select(t => t.Text)))
                .ToList();

            Assert.AreEqual(testAnagrams[0].ToLower(), "");
        }

        [Test]
        public void GetAnagrams_WordValueIsNull_ResultCOuntIs0()
        {
            Assert.That(() => _anagramSolver1.GetAnagrams(null), Throws.ArgumentNullException);
        }

    }
}
