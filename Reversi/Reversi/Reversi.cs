using System;
using System.Windows.Forms;
using System.Drawing;

namespace Reversi
{
    class ReversiForm : Form
    {
        int breedte, hoogte;
        public int beurt;
        Veld[,] velden;
        Label zet, bstenen, rstenen;
        public Images sprites;

        public ReversiForm()
        {
            int veldomvang;
            // variabelen xpos en ypos om makkelijk de locatie van de buttons en labels aan te passen
            int xpos;
            int ypos = 20;
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

            //Form opmaken
            this.Text = "Reversi";
            this.Size = new Size((breedte + 1) * veldomvang, 110 + (hoogte + 1) * veldomvang);
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
            this.Controls.Add(bstenen);

            rstenen = new Label();
            rstenen.Location = new Point(xpos + 100, ypos + 70);
            this.Controls.Add(rstenen);


            //Velden initialiseren
            for (int x = 0; x < breedte; x++)
            {
                for (int y = 0; y < hoogte; y++)
                {
                    velden[x, y] = new Veld(this, x, y, veldomvang);
                    velden[x, y].Location = new Point(x * veldomvang + veldomvang / 2 - 8, 80 + y * veldomvang + veldomvang / 2);
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
        //Methode die de stenen telt van beide spelers in een array. ook lege velden worden geteld
        private int[] TelStenen()
        {
            int[] teller = { 0, 0, 0 };

            for (int x = 0; x < breedte; x++)
                for (int y = 0; y < hoogte; y++)
                    teller[velden[x, y].Toestand]++;
            return teller;
        }

        //Losse functie, zodat hij aangehaald kan worden bij een nieuw spel
        public void StartPositie()
        {
            int midx, midy;
            midx = breedte / 2 - 1;
            midy = hoogte / 2 - 1;
            for (int y = 0; y < hoogte; y++)
                for (int x = 0; x < breedte; x++)
                    velden[x, y].Toestand = 0;
            velden[midx, midy].Toestand = 1;
            velden[midx + 1, midy].Toestand = 2;
            velden[midx, midy + 1].Toestand = 2;
            velden[midx + 1, midy + 1].Toestand = 1;
            // BeurtWissel() wordt aangevraagd om de legaliteit te checken als er een nieuw spel wordt aangevraagd
            BeurtWissel();
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

        }

        //Wisselt de beurt
        public void BeurtWissel(bool opnieuw = false)
        {
            if (beurt == 1)
                beurt = 2;
            else if (beurt == 2)
                beurt = 1;
            zet.Text = Beurttext() + " is aan de beurt";

            //Hier de legaliteit herchecken, en veld hertekenen
            bool geenzetten = true;
            for (int x = 0; x < breedte; x++)
            {
                for (int y = 0; y < hoogte; y++)
                {
                    //Als er nog geen legale zetten zijn, moet geenzetten gecheckt worden
                    if (geenzetten)
                        if (CheckLegal(x, y))
                        {
                            geenzetten = false;
                            velden[x, y].Legaal = true;
                        }
                    velden[x, y].Legaal = CheckLegal(x, y);
                }
            }

            int[] telling = TelStenen();

            //bijhouden van aantal stenen
            rstenen.Text = telling[1].ToString(); // nog steen erachter zetten (bitmap)
            bstenen.Text = telling[2].ToString();

            //if bord staat vol, stenen tellen. meeste stenen heeft gewonnen. Dan messagebox
            if (telling[0] == 0)
            {
                Winstbericht(telling);
                return;
            }

            //Als geenzetten nog true is, moet de beurt (na melding) teruggaan na de eerste speler
            if(geenzetten)
            {
                if (!opnieuw)
                {
                    //Eén speler kan geen zetten, beurt gaat terug
                    MistBeurt();
                    BeurtWissel(true);
                }
                else
                {
                    //Beide spelers hebben geen zetten meer
                    //Geef winstmessage
                    Winstbericht(telling);
                }
            }
        }

        //////////////////////////////////////////////////////////////////////////
        //Functionaliteit voor het checken van legaliteit en spelen van een zet///
        //////////////////////////////////////////////////////////////////////////

        //Check de legaliteit van het vlak op x, y in elke richting
        //Zet is illegaal, behalve als er een legale richting gevonden wordt
        public bool CheckLegal(int x, int y)
        {
            if (velden[x, y].Toestand != 0)
                return false;
            for (int m = -1; m <= 1; m++)
                for (int n = -1; n <= 1; n++)
                    if (!(n == 0 && m == 0))
                        if (VindInsluiter(x + m, y + n, m, n))
                            return true;
            return false;
        }

        //Vindt in elke richting een insluiter (stopt dus niet bij de eerste vinder) en speelt als gevonden
        public void Speel(int x, int y)
        {
            //We gaan in elke richting een insluiter zoeken, en als die wordt gevonden,
            //wordt speelstenen vanaf daar getriggert
            for (int m = -1; m <= 1; m++)
                for (int n = -1; n <= 1; n++)
                    if (!(n == 0 && m == 0))
                        VindInsluiter(x + m, y + n, m, n, true);
        }

        //Vind een insluitende steen met minstends één rode ertussen
        public bool VindInsluiter(int x, int y, int dx, int dy, bool play = false, bool first = true)
        {
            if (x < 0 || y < 0 || x >= breedte || y >= hoogte)
            {
                //Hij zoekt buiten het veld, dus niet op tijd gevonden
                return false;
            }
            if (velden[x, y].Toestand == beurt)
            {
                //Als de aanliggende steen meteen van dezelfde kleur is, is insluiter false
                if (first == true)
                    return false;

                //Er is een insluitende steen gevonden, als het een zet is moet er worden gespeeld
                //Anders alleen return
                if (play)
                    SpeelStenen(-1 * dx + x, -1 * dy + y, -1 * dx, -1 * dy);

                return true;
            }
            else
            {
                //Check of deze plek niet leeg is
                if (velden[x, y].Toestand == 0)
                    return false;

                //Er ligt een steen van de tegenstander
                //zet de plaats een richting verder
                x = x + dx;
                y = y + dy;

                return VindInsluiter(x, y, dx, dy, play, false);
            }
        }

        //Speel stenen terug tot de beurt wordt teruggevonden
        public void SpeelStenen(int x, int y, int dx, int dy)
        {
            //Blijf stenen van naar eigen kleur zetten tot eigen kleur wordt teruggevonden
            //Speelstenen wordt aangeroepen door een legaliteitscheck, dat hoeft hier niet dubbel
            if (velden[x, y].Toestand != beurt)
            {
                //In toestand.set zit een automatische invalidate, dus wordt meteen getekend
                velden[x, y].Toestand = beurt;
                SpeelStenen(x + dx, y + dy, dx, dy);
            }
        }
    }
    

    class Program
    {
        static void Main()
        {
            ReversiForm scherm;
            scherm = new ReversiForm();
            Application.Run(scherm);
        }
    }
}