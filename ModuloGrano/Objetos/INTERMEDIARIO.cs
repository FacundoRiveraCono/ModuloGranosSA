using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModuloGrano.Objetos
{
    public class INTERMEDIARIO
    {
        public class Root
        {
           
            public List<Value> value { get; set; }
        }

        public class Value
        {
            public string CardName { get; set; }
        }


    }
}
