using Contracts;
using System.Collections.Generic;
using Contracts.DTO;

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
