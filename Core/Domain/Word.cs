using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Domain
{
    public class Word
    {
        public string Text { get; set; }
        public string Type { get; set; }

        public override bool Equals(object obj)
        {
            Word w = obj as Word;
            return w != null && w.Text.Equals(this.Text);
        }

        public override int GetHashCode()
        {
            return this.Text.GetHashCode() ^ this.Type.GetHashCode();
        }
    }
}
