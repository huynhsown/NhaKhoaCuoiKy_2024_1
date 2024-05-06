using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NhaKhoaCuoiKy.Models
{
    internal class ComboBoxItem
    {
        public int Id { get; private set; }
        public string Text { get; private set; }

        public ComboBoxItem(int id, string text)
        {
            Id = id;
            Text = text;
        }

        public override string ToString()
        {
            return Text;
        }
    }

    internal class ComboBoxItemString
    {
        public string Id { get; private set; }
        public string Text { get; private set; }

        public ComboBoxItemString(string id, string text)
        {
            Id = id;
            Text = text;
        }

        public override string ToString()
        {
            return Text;
        }
    }
}
