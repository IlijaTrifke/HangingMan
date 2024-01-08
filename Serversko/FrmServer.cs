using System;
using System.Threading;
using System.Windows.Forms;

namespace Serversko
{
    public partial class FrmServer : Form
    {


        string[] niz = { "ivona", "ilija", "kosta", "stefi", "govno" };
        Server server;
        public FrmServer()
        {
            InitializeComponent();

            server = new Server();
            server.Start();
            System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();
            timer.Interval = 500;
            timer.Tick += (s, a) =>
            {
                if (server.handler1 != null && server.handler2 != null)
                {
                    textPrviKorisnikBrPokusajaBrPogodjenih.Text = $"{server.handler1.brpok}/{server.handler1.brpog}";
                    textDrugiKorisnikBrPokusajaBrPogodjenih.Text = $"{server.handler2.brpok}/{server.handler2.brpog}";
                }
            };
            timer.Start();
        }

        private void button1_Click(object sender, System.EventArgs e)
        {

            Thread thread = new Thread(server.Listen);
            thread.Start();
            Random rnd = new Random();

            int x = rnd.Next(0, 4);
            char[] karakteri = niz[x].ToCharArray();
            textBox1.Text = karakteri[0].ToString();
            textBox2.Text = karakteri[1].ToString();
            textBox3.Text = karakteri[2].ToString();
            textBox4.Text = karakteri[3].ToString();
            textBox5.Text = karakteri[4].ToString();
            server.Pogodi(karakteri);


            //sve dok primam requestove od klijenta za pogodi, treba da updateujem boxove




        }
    }
}
