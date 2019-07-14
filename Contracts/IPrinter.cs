using Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contracts
{
    public interface IPrinter
    {
        void Print(List<IPrintable> Printables);
    }
}
 