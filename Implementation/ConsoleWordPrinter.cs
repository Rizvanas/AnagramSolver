using Core.Interfaces;
using Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Implementation
{
    public class ConsoleWordPrinter : IPrinter
    {
        public List<IPrintable> Printables { get; set; }
        public ConsoleWordPrinter(List<IPrintable> printables)
        {
            Printables = printables;
        }

        public void Print()
        {
            foreach (var printable in Printables)
            {
                printable.Print();
            }
        }
    }
}
