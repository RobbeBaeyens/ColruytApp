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
        public string Id { get; set; }
        public Lijst Lijst { get; set; }
        public SolidColorBrush HexValue { get; set; }
        public SolidColorBrush ColorInitial { get; set; }
        public string Initial { get; set; }
    }
}
