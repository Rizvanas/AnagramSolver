using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Implementation.Tests
{
    [TestFixture]
    public class TxtWordLoaderTests
    {
        [Test]
        public void TxtWordLoader_FileContainsOnlySktv_ResultCountIs0()
        {
            var txtWordLoader = new TxtWordLoader { FilePath = @"..\AnagramGenerator\onlySktvZodynas.txt" };
            var words = txtWordLoader.Load();

            Assert.IsEmpty(words);
        }

        [Test]
        public void TxtWordLoader_LongestWordsFileLoaded_ContainsNoDuplicates()
        {
            var txtWordLoader = new TxtWordLoader { FilePath = @"..\AnagramGenerator\longestWords.txt" };
            var words = txtWordLoader.Load().ToList();

            Assert.AreEqual(words.Count, words.Distinct().ToList().Count);
        }

    }
}
