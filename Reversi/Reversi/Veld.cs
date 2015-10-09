using System;
using System.Windows.Forms;
using System.Drawing;

namespace Reversi
{
    class Veld : Panel
    {
        private int toestand;
        private bool legaal;
        public int Toestand
        {
            get
            {
                return this.toestand;
            }
            set
            {
                this.toestand = value;
                this.Invalidate();
            }
        }
        public bool Legaal
        {
            get
            {
                return this.legaal;
            }
            set
            {
                legaal = value;
                //Als de legaliteitsverandering visueel is (help staat aan), moet hij hertekend worden
                if (Showleg)
                    this.Invalidate();
            }
        }
        public bool Showleg;
        ReversiForm parent;
        int x, y, omvang;
        private Bitmap[] images;

        public Veld(ReversiForm o, int xpos, int ypos, int veldomvang)
        {
            parent = o;
            x = xpos;
            y = ypos;
            omvang = veldomvang;
            toestand = 0;
            Showleg = false;
            legaal = false;

            //Bepaal of de achtergrondkleur licht of donker is
            if ((x % 2 + y % 2) % 2 == 0)
                images = parent.sprites.donker;
            else
                images = parent.sprites.licht;

                this.Size = new Size(omvang, omvang);
            this.Paint += Veld_Paint;
            this.MouseClick += Veld_Play;
        }

        public void Veld_Paint(object o, PaintEventArgs pea)
        {
            //Functie die het tekenen van het veld moet gaan handelen
            //(Dus: checken wat de staat is, e.d., vervolgens juiste staat tekenen)
            Graphics g;
            Rectangle veld;
            g = pea.Graphics;

            veld = new Rectangle(0, 0, omvang, omvang);

            //Teken de toestand in de eigen sprite (achtegrondkleur)
            if (Showleg && legaal)
                g.DrawImage(images[3], 0, 0);
            else
                g.DrawImage(images[toestand], 0, 0);
        }

        public void Veld_Play(object o, MouseEventArgs mea)
        {
            //Functie die moet gaan checken: is de zet legaal (voor de huidige speler),
            //zo ja, speel hem uit (en draai alle gevangen stenen om)
            //Rood is 1, blauw is 2;

            if (this.legaal)
            {
                //Zet deze steen
                toestand = parent.beurt;
                this.Invalidate();
                //Voer zijn effect uit
                parent.Speel(this.x, this.y);

                //Verander de beurt
                parent.BeurtWissel();

                //Moet dan de gespeelde zetten invalidaten?
                //Nee: doe centraal, zodat alle legaliteit herzet kan worden
            }
        }
    }
}