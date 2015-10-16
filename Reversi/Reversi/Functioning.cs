using System.Windows.Forms;

namespace Reversi
{
    partial class ReversiForm : Form
    {
                //Methode die de stenen telt van beide spelers in een array. ook lege velden worden geteld
        private int[] TelStenen()
        {
            int[] teller = { 0, 0, 0 };

            for (int x = 0; x < breedte; x++)
                for (int y = 0; y < hoogte; y++)
                    teller[velden[x, y].Toestand]++;
            return teller;
        }

        //Losse startpositiefunctie, zodat hij aangehaald kan worden bij een nieuw spel
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
            rstenen.Text = telling[1].ToString();
            bstenen.Text = telling[2].ToString();

            //if bord staat vol, meeste stenen heeft gewonnen. dan messagebox
            if (telling[0] == 0)
            {
                Winstbericht(telling);
                return;
            }

            //Als geenzetten nog true is, moet de beurt (na melding) teruggaan na de eerste speler
            if (geenzetten)
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
}