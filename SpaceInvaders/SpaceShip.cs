using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace SpaceInvaders
{
    class SpaceShip
    {
        #region Fields
        /// <summary>
        /// Membres spaceship 
        /// </summary>
        public Vecteur2D Position {get; set;} 
        public int Lives {get; set;}  
        public bool Alive { get{return Lives>0;}}
        public Bitmap image;
        #endregion

         #region Constructor
        /// <summary>
        /// Simple constructor
        /// </summary>
        public SpaceShip(Vecteur2D _Pos, int _Liv, Bitmap _img)
        {
            this.Position = _Pos;
            this.Lives = _Liv;
            this.image = _img;
        }
        #endregion

        public void Draw (Graphics g){
            g.DrawImage(this.image, (int)(this.Position.x), (int)(this.Position.y), this.image.Width, this.image.Height);
        }

        public void MoveLeft(double deltaT, double speed)
        {
            this.Position.x -= speed * deltaT;
        }

        public void MoveRight(double deltaT, double speed)
        {
            this.Position.x += speed * deltaT;
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

                isCollision = true;
                this.Lives = -1; 
                //int x, y;
                //for (x = 0; x < lx2; x++)
                //{
                //    for (y = 0; y < ly2; y++)
                //    {
                //        if (monMissile != null)
                //        {
                //            int xShip, yShip;
                //            xShip = x + (int)(monMissile.Position.x) - (int)(this.Position.x);
                //            yShip = y + (int)(monMissile.Position.y) - (int)(this.Position.y);
                //            bool pixelShipNoir;
                //            if (xShip <= lx1 && yShip <= ly2 && xShip >= 0 && yShip >= 0)
                //            {
                //                pixelShipNoir = this.image.GetPixel(xShip, yShip) == Color.FromArgb(255, 0, 0, 0);
                //            }
                //            else
                //            {
                //                pixelShipNoir = false;
                //            }
                //            if (pixelShipNoir)
                //            {                               
                //                isCollision = true;
                //            }
                //        }

                //    }

                //}
            }
            return isCollision;
        }


    }
}
