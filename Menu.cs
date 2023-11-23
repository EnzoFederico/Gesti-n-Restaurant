using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Parcial_1
{
    public class Menu
    {
        public int IdM { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public int Time { get; set; }

        public Menu(int idm, string name, int price, int time) 
        {
            IdM = idm;
            Name = name;
            Price = price;
            Time = time;
        }


    }
}
