using System;
using System.Drawing;

namespace Reversi
{
    class Images
    {
        public Bitmap[] licht;
        public Bitmap[] donker;
        public Images(int size = 80)
        {
            Size omvang;
            omvang = new Size(size, size);
            licht = new Bitmap[4];
            licht[0] = new Bitmap(Properties.Resources.licht, omvang);
            licht[1] = new Bitmap(Properties.Resources.roodlicht, omvang);
            licht[2] = new Bitmap(Properties.Resources.blauwlicht, omvang);
            licht[3] = new Bitmap(Properties.Resources.zwartlicht, omvang);
            donker = new Bitmap[4];
            donker[0] = new Bitmap(Properties.Resources.donker, omvang);
            donker[1] = new Bitmap(Properties.Resources.rooddonker, omvang);
            donker[2] = new Bitmap(Properties.Resources.blauwdonker, omvang);
            donker[3] = new Bitmap(Properties.Resources.zwartdonker, omvang);
        }
    }
}