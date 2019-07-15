using Contracts.DTO;
using System.Collections.Generic;

namespace Contracts
{
    public interface IPrinter
    {
        void Print(List<IPrintable> Printables);
    }
}
 