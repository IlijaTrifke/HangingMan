using System;
using System.Collections.Generic;

namespace Common
{
    [Serializable]
    public class Response
    {
        public List<int> PozicijeSlova { get; set; }
        public List<string> IskoriscenaSlova { get; set; }
        public int PreostaliBrojPokusaja { get; set; }
        public int BrojPogodjenihZaSad { get; set; }
        public string PobednikJe { get; set; }
        public Operation Operacija { get; set; }

    }
}
