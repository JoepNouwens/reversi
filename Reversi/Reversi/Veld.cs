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