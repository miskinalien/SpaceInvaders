using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;


namespace SpaceInvaders
{
    class Missile
    {
        #region Fields
        public Vecteur2D Position {get; set;} 
        public Vecteur2D Vitesse {get; set;} 
        public int Lives {get; set;}  
        public bool Alive { get{return Lives>0;}}
        public Bitmap image;
        #endregion

         #region Constructor
        /// <summary>
        /// Simple constructor
        /// </summary>
        public Missile(Vecteur2D _Pos,Vecteur2D _Vit, int _Liv)
        {
            this.Position = _Pos;
            this.Vitesse = _Vit;
            this.Lives = _Liv;
            this.image = SpaceInvaders.Properties.Resources.shoot1;
        }
        #endregion

        public void Draw (Graphics g){
            g.DrawImage(this.image, (int)(this.Position.x), (int)(this.Position.y), this.image.Width, this.image.Height);
        }

        public void Move(double deltaT)
        {
            this.Position.x += Vitesse.x * deltaT;
            this.Position.y += Vitesse.y * deltaT;
        }

    }
}
