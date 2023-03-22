using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _102210247_LeVanTienDat.DTO
{
    internal class CBBItems
    {
        public int Value { get; set; }
        public string Text { get; set; }
        public override string ToString()
        {
            return Text;
        }
    }
}
