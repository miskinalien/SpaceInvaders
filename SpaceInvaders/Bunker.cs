using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace SpaceInvaders
{
    class Bunker
    {
        public Vecteur2D Position { get; set; }
        public Bitmap image;

        public Bunker(Vecteur2D _Position)
        {
            Position = _Position;
            image = SpaceInvaders.Properties.Resources.bunker;
        }

        public void Draw(Graphics g)
        {
            g.DrawImage(this.image, (int)(this.Position.x), (int)(this.Position.y), this.image.Width, this.image.Height);
        }

        public bool Collision(Missile monMissile)
        {

            int x1, x2, y1, y2, lx1, lx2, ly1, ly2;
            x1 = (int)(this.Position.x);
            y1 = (int)(this.Position.y);
            x2 = (int)(monMissile.Position.x);
            y2 = (int)(monMissile.Position.y);
            lx1 = (int)(this.image.Width);
            ly1 = (int)(this.image.Height);
            lx2 = (int)(monMissile.image.Width);
            ly2 = (int)(monMissile.image.Height);

            bool r2Droiter1 = x2 > x1 + lx1;
            bool r2Hautr1 = y2 > y1 + ly1;
            bool r1Droiter2 = x1 > x2 + lx2;
            bool r1Hautr2 = y1 > y2 + ly2;

            bool isCollision = false;

            if (r2Droiter1 || r2Hautr1 || r1Droiter2 || r1Hautr2)
            {
                isCollision = false;
            }
            else
            {
                int x, y;
                for (x = 0; x < lx2; x++)
                {
                    for (y = 0; y < ly2; y++)
                    {
                        int xBunker, yBunker;
                        xBunker = x + (int)(monMissile.Position.x) - (int)(this.Position.x);
                        yBunker = y + (int)(monMissile.Position.y) - (int)(this.Position.y);
                        bool pixelBunkerNoir = false;
                        if (xBunker < lx1 && yBunker < ly2 && xBunker > 0 && yBunker > 0)
                        {                           
                            pixelBunkerNoir = this.image.GetPixel(xBunker, yBunker) == Color.FromArgb(255, 0, 0, 0);
                        }
                       
                        if (pixelBunkerNoir)
                        {
                            monMissile.Lives -= 1;
                            this.image.SetPixel(xBunker, yBunker, Color.FromArgb(0, 255, 255, 255));
                            return true;
                        }
                        
                    }

                }
            }
            return isCollision;
        }





    }
}
