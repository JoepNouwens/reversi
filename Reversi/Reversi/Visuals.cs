using System;
using System.Drawing;

namespace Reversi
{
    class Images
    {
        public Bitmap[] licht;
        public Bitmap[] donker;
        public Images()
        {
            licht = new Bitmap[4];
            licht[0] = new Bitmap(Properties.Resources.licht);
            licht[1] = new Bitmap(Properties.Resources.roodlicht);
            licht[2] = new Bitmap(Properties.Resources.blauwlicht);
            licht[3] = new Bitmap(Properties.Resources.zwartlicht);
            donker = new Bitmap[4];
            donker[0] = new Bitmap(Properties.Resources.donker);
            donker[1] = new Bitmap(Properties.Resources.rooddonker);
            donker[2] = new Bitmap(Properties.Resources.blauwdonker);
            donker[3] = new Bitmap(Properties.Resources.zwartdonker);
        }
    }
}