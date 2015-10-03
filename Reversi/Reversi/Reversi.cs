using System;
using System.Windows.Forms;
using System.Drawing;

class ReversiForm : Form
{
    int breedte, hoogte, beurt;
    Veld[,] velden;

    public ReversiForm()
    {
        //Variabelen om gemakkelijk omvang van het veld aan te passen
        breedte = 6;
        hoogte = 6;
        //Rode speler is 1, blauwe speler is 2
        beurt = 1;
        //Array met het hele speelbord, opgedeeld in velden
        velden = new Veld[breedte, hoogte];

        //Form opmaken
        this.Text = "Reversi";
        this.Size = new Size(breedte * 40 + 20, hoogte * 40 + 80);
        this.BackColor = Color.AntiqueWhite;
        this.Paint += ReversiForm_Paint;

        //Velden initialiseren
        for (int x = 0; x < breedte; x++)
        {
            for (int y = 0; y < hoogte; y++)
            {
                velden[x, y] = new Veld(this);
                velden[x, y].Location = new Point(10 + x * 40, 10 + y * 40);
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

    public Veld(object o)
    {
        parent = o;

        this.Size = new Size(40, 40);
        this.Paint += Veld_Paint;
        this.MouseClick += Veld_Play;
    }

    public void Veld_Paint(object o, PaintEventArgs pea)
    {
        //Functie die het tekenen van het veld moet gaan handelen
        //(Dus: checken wat de staat is, e.d., vervolgens juiste staat tekenen)
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