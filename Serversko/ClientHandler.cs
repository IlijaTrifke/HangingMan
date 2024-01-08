using Common;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Sockets;

namespace Serversko
{
    public class ClientHandler
    {
        Socket soket;
        CommunicationHelper helper;
        //  public event EventHandler<Request> IgracZavrsio;
        public string Username { get; set; }
        public int BrojPokusaja { get; set; }
        public int brpok = 0;
        public int brpog = 0;
        public ClientHandler(Socket socket)
        {
            soket = socket;
            helper = new CommunicationHelper(soket);
        }
        public void HandleRequest()
        {
            bool kraj = false;
            try
            {
                while (!kraj)
                {
                    Request req = (Request)helper.Recieve();
                    switch (req.Operacija)
                    {

                        case Operation.Pogodi:
                            brpok = req.BrPok;
                            brpog = req.BrPog;

                            List<int> pozicije = new List<int>();
                            Response res = new Response();
                            res.Operacija = Operation.Pogodi;
                            for (int i = 0; i < Server.slova.Length; i++)
                            {

                                if (req.Slovo == Server.slova[i].ToString())
                                {
                                    //i
                                    pozicije.Add(i);
                                }
                            }
                            res.PozicijeSlova = pozicije;
                            if (pozicije.Count > 0)
                            {
                                brpog++;
                            }
                            else
                            {
                                brpok--;
                            }
                            helper.Send(res);



                            break;
                        case Operation.Gotovo:
                            BrojPokusaja = req.BrPok;
                            kraj = true;
                            break;
                        default:
                            break;
                    }

                }
            }
            catch (Exception ex)
            {

                Debug.WriteLine(ex.Message);
            }
        }
        public void PosaljiKraj(string pobednik)
        {
            Response res = new Response()
            {
                PobednikJe = pobednik,
                Operacija = Operation.Gotovo,
            };
            helper.Send(res);
        }
        //public int InfoBrPok()
        //{
        //    Response res = new Response()
        //    {
        //        PreostaliBrojPokusaja = brpok,

        //    };
        //    return res.PreostaliBrojPokusaja;
        //}
        //public int InfoBrPog()
        //{
        //    Response res = new Response()
        //    {

        //        BrojPogodjenihZaSad = brpog
        //    };
        //    return res.BrojPogodjenihZaSad;
        //}
    }
}
