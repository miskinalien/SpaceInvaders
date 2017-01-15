using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpaceInvaders
{
    class Vecteur2D
    {

        #region Fields
        /// <summary>
        /// vector coordonates 
        /// </summary>
        public double x{ get;  set; }
        public double y{ get;  set; }
        private double Norme { get;  set; }  
        #endregion

         #region Constructor
        /// <summary>
        /// Simple constructor
        /// </summary>
        /// <param name="x">start position x</param>
        /// <param name="y">start position y</param>
        public Vecteur2D(double x, double y)
        {
            this.x = x;
            this.y = y;
            this.Norme = Math.Sqrt(this.x * this.x + this.y * this.y);

        }

        public Vecteur2D()
        {
            this.x = 0;
            this.y = 0;
            this.Norme = Math.Sqrt(this.x * this.x + this.y * this.y);
        }

        #endregion

        #region Methods



        public static  Vecteur2D operator +(Vecteur2D V1, Vecteur2D V2)
        {
            return new Vecteur2D(V1.x + V2.x, V1.y + V2.y);
        }

        public static  Vecteur2D operator -(Vecteur2D V1, Vecteur2D V2)
        {
            return new Vecteur2D(V1.x - V2.x, V1.y - V2.y);
        }

        public static  Vecteur2D operator -(Vecteur2D V1)
        {
            return new Vecteur2D(-V1.x ,-V1.y);
        }

        public static  Vecteur2D operator *(Vecteur2D V1, double a)
        {
            return new Vecteur2D(V1.x *a, V1.y *a);
        }

        public static  Vecteur2D operator *(double a, Vecteur2D V1)
        {
            return new Vecteur2D(V1.x * a, V1.y * a);
        }

        public static  Vecteur2D operator /(Vecteur2D V1, double a)
        {
            return new Vecteur2D(V1.x / a, V1.y / a);
        }

        #endregion

    }

}
