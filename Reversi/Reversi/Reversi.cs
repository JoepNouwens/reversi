using System;
using System.Windows.Forms;
using System.Drawing;

class ReversiForm : Form
{
    int breedte, hoogte, beurt;
    Veld[,] velden;

    public ReversiForm()
    {
        int veldomvang;
        //Variabelen om gemakkelijk omvang van het veld aan te passen
        // gerben heeft iets aangepast
        breedte = 6;
        hoogte = 6;
        veldomvang = 80;
        //Rode speler is 1, blauwe speler is 2
        beurt = 1;
        //Array met het hele speelbord, opgedeeld in velden
        velden = new Veld[breedte, hoogte];

        //Form opmaken
        this.Text = "Reversi";
        this.Size = new Size((breedte+1)*veldomvang, (hoogte+2)*veldomvang);
        this.BackColor = Color.DimGray;
        this.Paint += ReversiForm_Paint;

        //Velden initialiseren
        for (int x = 0; x < breedte; x++)
        {
            for (int y = 0; y < hoogte; y++)
            {
                velden[x, y] = new Veld(this, x, y, veldomvang);
                velden[x, y].Location = new Point(x*veldomvang+veldomvang/2, y*veldomvang+veldomvang/2);
                Controls.Add(velden[x, y]);
            }
        }

    }

    //Tekenaar
    void ReversiForm_Paint(object o, PaintEventArgs pea)
    {

    }
}

class Veld : Panel
{
    int toestand;
    bool legaal;
    object parent;
    int x, y, omvang;

    public Veld(object o, int xpos, int ypos, int veldomvang)
    {
        parent = o;
        x = xpos;
        y = ypos;
        omvang = veldomvang;

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
        if ((x % 2 + y % 2) % 2 == 0)
            g.FillRectangle(Brushes.LightBlue, veld);
        else
            g.FillRectangle(Brushes.LightCoral, veld);

    }

    public void Veld_Play(object o, MouseEventArgs mea)
    {
        //Functie die moet gaan checken: is de zet legaal (voor de huidige speler),
        //zo ja, speel hem uit (en draai alle gevangen stenen om)
    }
}

class Reversi
{
    static void Main()
    {
        ReversiForm scherm;
        scherm = new ReversiForm();
        Application.Run(scherm);
    }
}