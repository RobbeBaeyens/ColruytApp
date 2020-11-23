using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Colruyt_DAL
{
    public class CustomLijst
    {
        public Lijst Lijst { get; set; }
        public SolidColorBrush HexValue { get; set; }
        public SolidColorBrush InvertedHexValue { get; set; }
        public string ColorInitial { get; set; }
    }
}
