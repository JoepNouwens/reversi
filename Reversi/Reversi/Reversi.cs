using System;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace Reversi
{
    partial class ReversiForm : Form
    {
        int breedte, hoogte;
        public int beurt;
        Veld[,] velden;
        Label zet, bstenen, rstenen;
        public Images sprites;
        int xpos, ypos;

        public ReversiForm()
        {
            int veldomvang;
            // variabelen xpos en ypos om makkelijk de locatie van de buttons en labels aan te passen
            ypos = 20;
            //Variabelen om gemakkelijk omvang van het veld aan te passen
            breedte = 6;
            hoogte = 6;
            veldomvang = 80;
            //Rode speler is 1, blauwe speler is 2
            beurt = 1;
            //Array met het hele speelbord, opgedeeld in velden
            velden = new Veld[breedte, hoogte];
            //Maak de bitmaps
            sprites = new Images();

            if(breedte > 6 || hoogte > 6)
            {
                veldomvang = 50;
            }


            //Form opmaken
            this.Text = "Reversi";
            this.Size = new Size((breedte + 1) * veldomvang, 110 + (hoogte + 1) * veldomvang + veldomvang / 2);
            this.BackColor = Color.White;
            this.Paint += ReversiForm_Paint;

            xpos = this.Width/2 - 95;

            //buttons nieuw spel en help
            Button nieuw;
            nieuw = new Button();
            nieuw.Location = new Point(xpos, ypos);
            nieuw.Text = "Nieuw Spel";
            nieuw.Click += this.kliknieuw;
            this.Controls.Add(nieuw);

            Button help;
            help = new Button();
            help.Location = new Point(xpos + 100, ypos);
            help.Text = "Help!";
            help.Click += this.klikhelp;
            this.Controls.Add(help);
            
            //labels voor beurt en aantal stenen van beide spelers
            zet = new Label();
            zet.Location = new Point(xpos+nieuw.Width/2, ypos+40);
            this.Controls.Add(zet);
            zet.ClientSize = new Size(200, 20);

            bstenen = new Label();
            bstenen.Location = new Point(xpos, ypos + 70);
            bstenen.Width = 20;
            this.Controls.Add(bstenen);

            rstenen = new Label();
            rstenen.Location = new Point(xpos + 100, ypos + 70);
            rstenen.Width = 20;
            this.Controls.Add(rstenen);


            //Velden initialiseren
            for (int x = 0; x < breedte; x++)
            {
                for (int y = 0; y < hoogte; y++)
                {
                    velden[x, y] = new Veld(this, x, y, veldomvang);
                    velden[x, y].Location = new Point(x * veldomvang + veldomvang / 2 - 8, 80 + y * veldomvang + veldomvang);
                    Controls.Add(velden[x, y]);
                }
            }

            StartPositie();
        }

        //methode om van een int voor de speler een string te maken
        private string Beurttext()
        {
            if (beurt == 1)
                return "Rood";
            else
                return "Blauw";
        }

        //methode die messagebox laat zien wanneer het spel is afgelopen
        public void Winstbericht(int[] telling)
        {
            if (telling[1] > telling[2])
                zet.Text = "Rood heeft gewonnen!";
            else if (telling[2] > telling[1])
                zet.Text = "Blauw heeft gewonnen!";
            else
                zet.Text = "Het is remise!";

            //Messagebox maken
            string message = "Er zijn geen zetten meer mogelijk. " + zet.Text;
            string caption = "Uitslag";
            MessageBoxButtons buttons = MessageBoxButtons.OK;

            // Displays the MessageBox.
            MessageBox.Show(message, caption, buttons);
        }

        //methode die de beurt doorgeeft als een speler geen zet kan doen en een messagebox laat zien
        public void MistBeurt()
        {
            string message, caption;
            if (beurt == 1)
                message = "Rood kan geen zetten doen, de beurt gaat terug naar blauw.";
            else
                message = "Blauw kan geen zetten doen, de beurt gaat terug naar rood.";

            caption = "Geen zetten";

            MessageBoxButtons buttons = MessageBoxButtons.OK;

            MessageBox.Show(message, caption, buttons);
        }

        //klikmethode om helpfunctie van het programma om speler te tonen welke velden legaal zijn
        private void klikhelp(object sender, EventArgs e)
        {
            for (int x = 0; x < breedte; x++)
            {
                for (int y = 0; y < hoogte; y++)
                {
                    velden[x, y].Showleg = !velden[x, y].Showleg;
                    velden[x, y].Invalidate();
                }
            }
        }

        //klikmethode die startpositie aanroept bij klikevent
        private void kliknieuw(object sender, EventArgs e)
        {
            beurt = 1;
            StartPositie();
        }

        //Tekenaar
        void ReversiForm_Paint(object o, PaintEventArgs pea)
        {
            Graphics g;
            g = pea.Graphics;

            g.SmoothingMode = SmoothingMode.HighQuality;
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            g.PixelOffsetMode = PixelOffsetMode.HighQuality;

            g.DrawImage(sprites.licht[2], xpos + 20, ypos + 60, 30, 30);
            g.DrawImage(sprites.licht[1], xpos + 120, ypos + 60, 30, 30);
        }
    }
}