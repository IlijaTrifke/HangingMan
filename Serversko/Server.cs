using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace Serversko
{
    public class Server
    {
        Socket osluskujucisoket;
        public static int brojac = 0;
        public static int brPokusajaIlije = 0;
        public static int brPokusajaIvone = 0;
        public static int brPogodjenihIlije = 0;
        public static int brPogodjenihIvone = 0;
        public static char[] slova;

        public static int brpok = 0;
        public static int brpog = 0;
        public ClientHandler handler1;
        public ClientHandler handler2;

        public Server()
        {
            osluskujucisoket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        }
        public void Start()
        {
            IPEndPoint ipendpoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), int.Parse("9999"));
            osluskujucisoket.Bind(ipendpoint);
            osluskujucisoket.Listen(5);
            //Listen();
        }
        public void Listen()
        {
            Socket klijent1 = osluskujucisoket.Accept();
            Socket klijent2 = osluskujucisoket.Accept();
            handler1 = new ClientHandler(klijent1);
            handler1.Username = "Ilija";
            handler2 = new ClientHandler(klijent2);
            handler2.Username = "Ivona";
            Thread thread1 = new Thread(handler1.HandleRequest);
            thread1.Start();
            Thread thread2 = new Thread(handler2.HandleRequest);
            thread2.Start();
            thread1.Join();
            thread2.Join();

            string pobednik = "";
            if (handler1.BrojPokusaja <= handler2.BrojPokusaja)
            {
                pobednik = handler1.Username;
            }
            else
            {
                pobednik = handler2.Username;
            }
            handler1.PosaljiKraj(pobednik);
            handler2.PosaljiKraj(pobednik);
            //handler1.IgracZavrsio += Handler1_IgracZavrsio;
            //handler2.IgracZavrsio += Handler2_IgracZavrsio;

        }

        //private void Handler2_IgracZavrsio(object sender, Common.Request e)
        //{
        //    string pobednik = "";
        //    if (handler1.BrojPokusaja <= handler2.BrojPokusaja)
        //    {
        //        pobednik = handler1.Username;
        //    }
        //    else
        //    {
        //        pobednik = handler2.Username;
        //    }
        //    handler1.PosaljiKraj(pobednik);
        //    handler2.PosaljiKraj(pobednik);
        //}

        //private void Handler1_IgracZavrsio(object sender, Common.Request e)
        //{
        //    string pobednik = "";
        //    if (handler1.BrojPokusaja <= handler2.BrojPokusaja)
        //    {
        //        pobednik = handler1.Username;
        //    }
        //    else
        //    {
        //        pobednik = handler2.Username;
        //    }
        //    handler1.PosaljiKraj(pobednik);
        //    handler2.PosaljiKraj(pobednik);
        //}

        internal void Pogodi(char[] karakteri)
        {
            slova = karakteri;

            //moram da skaldistim na serveru
        }
    }
}
