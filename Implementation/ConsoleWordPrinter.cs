using Core.Interfaces;
using Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace Implementation
{
    public class ConsoleWordPrinter : IPrinter
    {
        public void Print(List<IPrintable> Printables)
        {
            foreach (var printable in Printables)
            {
                printable.Print();
            }
        }
    }
}
