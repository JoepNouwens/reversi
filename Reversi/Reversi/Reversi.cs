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
        nieuw.Location = new Point(50,20);
        nieuw.Text = "Nieuw Spel";
        nieuw.Click += this.kliknieuw;
        this.Controls.Add(nieuw);

        Button help;
        help = new Button();
        help.Location = new Point(150, 20);
        help.Text = "Help!";
        help.Click += this.klikhelp;
        this.Controls.Add(help);


        //Velden initialiseren
        for (int x = 0; x < breedte; x++)
        {
            for (int y = 0; y < hoogte; y++)
            {
                velden[x, y] = new Veld(this, x, y, veldomvang);
                velden[x, y].Location = new Point(x*veldomvang+veldomvang/2, 30+y*veldomvang+veldomvang/2);
                Controls.Add(velden[x, y]);
            }
        }

    }

    private void klikhelp(object sender, EventArgs e)
    {
        //hier moet info komen over wat het scherm moet gaan doen na het klik-event
        for (int x = 0; x < breedte; x++)
        {
            for (int y = 0; y < hoogte; y++)
            {
                velden[x, y].showleg = !velden[x, y].showleg;
                velden[x, y].Invalidate();
            }
        }
    }   
    private void kliknieuw(object sender, EventArgs e)
    {
        //hier moet info komen over wat het scherm moet gaan doen na het klik-event
    }

    //Tekenaar
    void ReversiForm_Paint(object o, PaintEventArgs pea)
    {

    }
    public void BeurtWissel()
    {
        if (beurt == 1)
            beurt = 2;
        else if (beurt == 2)
            beurt = 1;
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