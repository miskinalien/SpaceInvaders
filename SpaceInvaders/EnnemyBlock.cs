using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace SpaceInvaders
{
    class EnnemyBlock
    {
        public List<SpaceShip> Ships { get; set; }
        public Vecteur2D position { get; set; }
        public Size size;
        public bool Alive { get; set; }
        public int speedX;
        public int speedY;
        public List<int> lines { get; set; }


        public EnnemyBlock(Vecteur2D _position)
        {
            this.position = _position;
            Ships = new List<SpaceShip>();
            lines = new List<int>(); 
            speedX = 100;
            speedY = 5000; 
        }

        public void AddLine(int width, int nbShips, int lives, Bitmap im)
        {
            int num = lines.Count;
            int i;
            for (i = 1; i <= nbShips; i++)
            {
                Ships.Add(new SpaceShip(new Vecteur2D(i * width / nbShips - im.Width/2, 10 * num), lives, im));
                lines.Add(width);
            }
            lines.Add(width);

        }

        public void Draw(Graphics g)
        {
            foreach (SpaceShip ship in this.Ships)
            {
                if (ship.Alive)
                {
                    g.DrawImage(ship.image, (int)(ship.Position.x + this.position.x), (int)(ship.Position.y + this.position.y), ship.image.Width, ship.image.Height);
                    this.size.Width = lines.Max();
                    this.size.Height = 10 * lines.Count;
                }
                
            }

            this.Ships.RemoveAll(item => item.Alive == false);
  
        }

        public void Move(double deltaT, int gamesize)
        {


            this.position.x += this.speedX * deltaT;
            bool isOut = (int)(this.position.x) <= 0 - this.size.Width/5 || (int)(this.position.x + size.Width) >= gamesize;
            if(isOut)
            {
                this.speedX = -this.speedX;
                this.position.y += this.speedY* deltaT;
                this.position.x += this.speedX*10 * deltaT;

            }

            
        }

        public bool IsKillingPlayer(SpaceShip player)
        {
            return this.position.y + this.size.Height >= player.Position.y;
        }

        public bool collision(Missile monMissile)
        {
            bool isCollision = false;
            foreach (SpaceShip ennemy in this.Ships)
            {
                

                    int x1, x2, y1, y2, lx1, lx2, ly1, ly2;
                    x1 = (int)(ennemy.Position.x + this.position.x);
                    y1 = (int)(ennemy.Position.y + this.position.y);
                    x2 = (int)(monMissile.Position.x);
                    y2 = (int)(monMissile.Position.y);
                    lx1 = (int)(ennemy.image.Width);
                    ly1 = (int)(ennemy.image.Height);
                    lx2 = (int)(monMissile.image.Width);
                    ly2 = (int)(monMissile.image.Height);

                    bool r2Droiter1 = x2 > x1 + lx1;
                    bool r2Hautr1 = y2 > y1 + ly1;
                    bool r1Droiter2 = x1 > x2 + lx2;
                    bool r1Hautr2 = y1 > y2 + ly2;

                    if (r2Droiter1 || r2Hautr1 || r1Droiter2 || r1Hautr2)
                    {
                        isCollision = false;
                    }
                    else
                    {

                        isCollision = true;
                        ennemy.Lives = -1;
                        break;
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

                
            }
            return isCollision;
        }

    }
}
