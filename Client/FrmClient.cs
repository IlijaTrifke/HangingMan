using Common;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Windows.Forms;

namespace Client
{

    public partial class FrmClient : Form
    {
        List<char> koriscenaslova = new List<char>();
        int brPreostalihPokusaja = 10;
        int brojpogodjenih = 0;
        public List<int> pozicije { get; set; }
        public string pobednikJe { get; set; }
        public FrmClient()
        {
            InitializeComponent();
            Communication.Instance.Connect();
            //PRVO CONNECT 
            Thread thread = new Thread(Osluskuj);
            thread.Start();
            textBoxPreostaliBrPokusaja.Text = $"{brPreostalihPokusaja}";
        }

        public void Osluskuj()
        {
            try
            {
                while (true)
                {
                    Response res = Communication.Instance.Primaj();
                    switch (res.Operacija)
                    {
                        case Operation.Pogodi:
                            pozicije = res.PozicijeSlova;
                            break;
                        case Operation.Gotovo:
                            MessageBox.Show($"Pobednik je: {res.PobednikJe}!");
                            break;
                        default:
                            break;
                    }
                }
            }
            //kad koristis elemente forme u threadu nekom van glavnog sistemskog treba invoke!!!!!!!!!!!!!!!!!!!!!!!!
            catch (Exception ex)
            {

                Debug.WriteLine(">>>" + ex.Message);
            }
        }

        private void button1_Click(object sender, System.EventArgs e)
        {

            if (isValid())
            {
                //br pokusaja, br pogodfjenih
                Communication.Instance.Pogodi(textBoxPokusaj.Text, brPreostalihPokusaja, brojpogodjenih);
                //ako pogodis ne smanjuju ti se pokusaji
                while (pozicije == null)
                    Thread.Sleep(100);
                if (pozicije.Count == 0)
                {
                    //znaci ako nije nasao
                    brPreostalihPokusaja--;
                    textBoxPreostaliBrPokusaja.Text = $"{brPreostalihPokusaja}";
                }

                else
                {
                    foreach (int clan in pozicije)
                    {
                        if (clan == 0)
                        {
                            textBox1.Text = textBoxPokusaj.Text;
                            brojpogodjenih++;
                        }
                        if (clan == 1)
                        {
                            textBox2.Text = textBoxPokusaj.Text;
                            brojpogodjenih++;

                        }
                        if (clan == 2)
                        {
                            textBox3.Text = textBoxPokusaj.Text;
                            brojpogodjenih++;

                        }
                        if (clan == 3)
                        {
                            textBox4.Text = textBoxPokusaj.Text;
                            brojpogodjenih++;

                        }
                        if (clan == 4)
                        {
                            textBox5.Text = textBoxPokusaj.Text;
                            brojpogodjenih++;

                        }
                    }
                }
                if (textBox1.Text != "" && textBox2.Text != "" && textBox3.Text != "" && textBox4.Text != "" && textBox5.Text != "")
                {
                    Communication.Instance.ZavrsilaSam(10 - brPreostalihPokusaja);
                    //response ko je pobedio (onaj sa manje pokusaja) prihvata i zatvara se forma ili brise sve
                    //ne ide while petlja ovde ejr nikad necemo dobiti od 2,. igraca
                    //Communication.Instance.KoJePobedio();
                    return;

                }
                if (brPreostalihPokusaja == 0)
                {
                    MessageBox.Show("Iskoristio si svih svojih 10 pokusaja");
                    Communication.Instance.ZavrsilaSam(10 - brPreostalihPokusaja);
                    //zatvara se forma ili brise sve
                    return;
                }
                pozicije = null;
            }


        }
        public bool isValid()
        {
            //da li je slovo i da l je vec iskorisceno..to proveravamo za textbox pogodi
            if (!int.TryParse(textBoxPokusaj.Text, out int x))
            {
                if (textBoxPokusaj.Text.Length != 1)
                {

                    MessageBox.Show("To slovo si vec uneo");
                    return false;

                }
                if (!koriscenaslova.Contains(char.Parse(textBoxPokusaj.Text)))
                {
                    koriscenaslova.Add(char.Parse(textBoxPokusaj.Text));
                    textBoxKoriscenaSlova.AppendText(textBoxPokusaj.Text + " , ");

                    return true;
                }
                else
                {
                    MessageBox.Show("To slovo si vec uneo");
                }

                //}



            }
            else
            {
                MessageBox.Show("Nisi upisao slovo");

            }
            return false;

        }

    }
}
