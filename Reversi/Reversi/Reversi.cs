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

        StartPositie();
        BeurtWissel();
    }

    //Losse functie, zodat hij aangehaald kan worden bij een nieuw spel
    public void StartPositie()
    {
        int midx, midy;
        midx = breedte / 2 - 1;
        midy = hoogte / 2 - 1;
        velden[midx, midy].Toestand = 1;
        velden[midx + 1, midy].Toestand = 2;
        velden[midx, midy + 1].Toestand = 2;
        velden[midx + 1, midy + 1].Toestand = 1;
    }

    private void klikhelp(object sender, EventArgs e)
    {
        //hier moet info komen over wat het scherm moet gaan doen na het klik-event
        for (int x = 0; x < breedte; x++)
        {
            for (int y = 0; y < hoogte; y++)
            {
                velden[x, y].Showleg = !velden[x, y].Showleg;
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

    //Wisselt de beurt
    public void BeurtWissel()
    {
        if (beurt == 1)
            beurt = 2;
        else if (beurt == 2)
            beurt = 1;

        //Hier de legaliteit herchecken, en veld hertekenen
        for(int x = 0; x < breedte; x++)
        {
            for(int y = 0; y< hoogte; y++)
            {
                //In 'legaal' zit een interne invalidate;
                velden[x, y].Legaal = CheckLegal(x, y);
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
        for(int m = -1; m <= 1; m++)
            for(int n = -1; n <= 1; n++)
                if(!(n==0 && m == 0))
                    if(VindInsluiter(x+m, y+n, m, n))
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
        if(x<0 || y<0 || x>=breedte || y >= hoogte)
        {
            //Hij zoekt buiten het veld, dus niet op tijd gevonden
            return false;
        }
        //Deze try verhelpt een fout die niet voor zou mogen komen, namelijk x, y buiten de range
        try
        {
            if (velden[x, y].Toestand == beurt)
            {
                //Als de aanliggende steen meteen blauw is, is insluiter false
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
        catch (IndexOutOfRangeException e)
        {
            //Zoekt buiten de array, is ergens fout gegaan, in elk geval geen legale zet
            return false;
        }
        return false;
    }

    //Speel stenen terug tot de beurt wordt teruggevonden
    public void SpeelStenen(int x, int y, int dx, int dy)
    {
        //Blijf stenen van naar eigen kleur zetten tot eigen kleur wordt teruggevonden
        //Speelstenen wordt aangeroepen door een legaliteitscheck, dat hoeft hier niet dubbel
        if(velden[x ,y].Toestand != beurt)
        {
            //In toestand.set zit een automatische invalidate, dus wordt meteen getekend
            velden[x, y].Toestand = beurt;
            SpeelStenen(x + dx, y + dy, dx, dy);
        }
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