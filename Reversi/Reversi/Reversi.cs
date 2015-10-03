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

        //Velden initialiseren
        for (int x = 0; x < breedte; x++)
        {
            for (int y = 0; y < hoogte; y++)
            {
                velden[x, y] = new Veld(this);
            }
        }






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