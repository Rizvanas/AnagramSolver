using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Interfaces.DTO
{
    public interface IWordModel
    {
        string Text { get; set; }
        string Type { get; set; }
    }
}
