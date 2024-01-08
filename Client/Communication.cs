using Common;
using System.Net.Sockets;

namespace Client
{
    public class Communication
    {
        Socket socket;
        CommunicationHelper helper;
        private static Communication _instance;
        public static Communication Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new Communication();
                }
                return _instance;
            }
        }
        private Communication()
        {

        }
        public void Connect()
        {
            socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            socket.Connect("127.0.0.1", 9999);
            helper = new CommunicationHelper(socket);

        }
        public void Pogodi(string slovo, int brpokusaja, int brpogodjenih)
        {
            Request req = new Request()
            {
                Operacija = Operation.Pogodi,
                Slovo = slovo,
                BrPok = brpokusaja,
                BrPog = brpogodjenih
                //brpok i brpogodjenih uz ovo serveru
            };
            helper.Send(req);
            //Response res = (Response)helper.Recieve();
            //return res.PozicijeSlova;
        }

        internal void ZavrsilaSam(int brPok)
        {
            Request req = new Request()
            {
                Operacija = Operation.Gotovo,
                BrPok = brPok
            };
            helper.Send(req);
            //Response res = (Response)helper.Recieve();
            //return res.PobednikJe;
        }

        public Response Primaj()
        {
            return (Response)helper.Recieve();
        }

    }
}
