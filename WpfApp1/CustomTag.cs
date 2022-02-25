using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1
{
    public class CustomTag : Tag
    {
        public bool IsSelected { get; set; }
        public CustomTag(Tag tag, bool isSelected = false)
        {
            ID = tag.ID;
            Title = tag.Title;
            Color = tag.Color;
            IsSelected = isSelected;
        }
    }
}
