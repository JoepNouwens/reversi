using System;
using System.Windows.Forms;
using System.Drawing;

class ReversiForm : Form
{
    int breedte, hoogte;
    public int beurt;
    Veld[,] velden;

    public ReversiForm()
    {
        int veldomvang;
        //Variabelen om gemakkelijk omvang van het veld aan te passen
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


        //buttons nieuw spel en help
        Button nieuw;
        nieuw = new Button();

        Button help;
        help = new Button();

        nieuw.Location = new Point();
        nieuw.Location = new Point();


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

class Reversi
{
    static void Main()
    {
        ReversiForm scherm;
        scherm = new ReversiForm();
        Application.Run(scherm);
    }
}