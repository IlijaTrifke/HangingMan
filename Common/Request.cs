using System;

namespace Common
{
    [Serializable]
    public class Request
    {
        public string Slovo { get; set; }
        public Operation Operacija { get; set; }
        public int BrPok { get; set; }
        public int BrPog { get; set; }
    }
}
