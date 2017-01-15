using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Diagnostics;
using System.Windows.Forms;

namespace SpaceInvaders
{
    class Game
    {

        #region fields and properties

        public enum GameState { Play, Pause, Win, Lost } 
        public GameState currentState; 

        #region gameplay elements
        /// <summary>
        /// A dummy object just for demonstration
        /// </summary>
        /// <summary>
        /// SpaceShip of the player 
        /// </summary>
        public SpaceShip playerShip;
        public Missile playerMissile; 
        public List<Bunker> bunkers; 
        public EnnemyBlock ennemies; 

        /// <summary>
        /// Speed of the player's spaceship
        /// </summary>
        public double playerSpeed=500;

        #endregion
        
        #region game technical elements
        /// <summary>
        /// Size of the game area
        /// </summary>
        public Size gameSize;

        public bool pHasBeenReleased;



        /// <summary>
        /// State of the keyboard
        /// </summary>
        public HashSet<Keys> keyPressed = new HashSet<Keys>();


        #endregion

        #region static fields (helpers)

        /// <summary>
        /// Singleton for easy access
        /// </summary>
        public static Game game { get; private set; }

        /// <summary>
        /// A shared black brush
        /// </summary>
        private static Brush blackBrush = new SolidBrush(Color.Black);

        /// <summary>
        /// A shared simple font
        /// </summary>
        private static Font defaultFont = new Font("Verdana", 24, FontStyle.Bold, GraphicsUnit.Pixel);
        #endregion

        #endregion

        #region constructors
        /// <summary>
        /// Singleton constructor
        /// </summary>
        /// <param name="gameSize">Size of the game area</param>
        /// 
        /// <returns></returns>
        public static Game CreateGame(Size gameSize)
        {
            if (game == null)
                game = new Game(gameSize);
            return game;
        }

        /// <summary>
        /// Private constructor
        /// </summary>
        /// <param name="gameSize">Size of the game area</param>
        private Game(Size gameSize)
        {
            this.gameSize = gameSize;
            this.playerShip = new SpaceShip(new Vecteur2D(gameSize.Width / 2, gameSize.Height - SpaceInvaders.Properties.Resources.ship4.Height - 20), 5, SpaceInvaders.Properties.Resources.ship4);
            this.pHasBeenReleased = true; 
            currentState = GameState.Play;


            this.bunkers = new List<Bunker>();
            int bunkercenter = SpaceInvaders.Properties.Resources.bunker.Width / 2;

            this.bunkers.Add(new Bunker(new Vecteur2D(gameSize.Width / 4 - bunkercenter, gameSize.Height - playerShip.image.Height * 6)));
            this.bunkers.Add(new Bunker(new Vecteur2D(gameSize.Width / 2 - bunkercenter, gameSize.Height - playerShip.image.Height * 6)));
            this.bunkers.Add(new Bunker(new Vecteur2D((gameSize.Width / 4) * 3 - bunkercenter, gameSize.Height - playerShip.image.Height * 6)));

            this.ennemies = new EnnemyBlock(new Vecteur2D(10, 10));
            ennemies.AddLine(200, 5, 2, SpaceInvaders.Properties.Resources.ship2);
            ennemies.AddLine(200, 5, 2, SpaceInvaders.Properties.Resources.ship3);
            ennemies.AddLine(200, 5, 2, SpaceInvaders.Properties.Resources.ship5);

            

        }

        #endregion

        #region methods

        /// <summary>
        /// Draw the whole game
        /// </summary>
        /// <param name="g">Graphics to draw in</param>
        public void Draw(Graphics g)
        {

            switch (currentState)
            {
                case GameState.Play:
                    g.DrawString("PLAY", defaultFont, blackBrush, 30, 30);
                    break;
                case GameState.Pause:
                    g.DrawString("PAUSE", defaultFont, blackBrush, 30, 30);
                    break;
                case GameState.Win:
                    g.DrawString("WIN", defaultFont, blackBrush, 30, 30);
                    break;
                case GameState.Lost:
                    g.DrawString("LOST", defaultFont, blackBrush, 30, 30);
                    break;
            }    
            
            playerShip.Draw(g);

            if (playerMissile != null)
            {
                playerMissile.Draw(g);
            }

            foreach (Bunker bunk in bunkers)
            {
                bunk.Draw(g);
            }

            ennemies.Draw(g);
            
        }

        /// <summary>
        /// Update game
        /// </summary>
        public void Update(double deltaT)
        {

            if (ennemies.Ships.Count == 0)
            {
                currentState = GameState.Win;
            }

            if (ennemies.IsKillingPlayer(playerShip))
            {
                currentState = GameState.Lost;
            }


            if (currentState == GameState.Pause || currentState == GameState.Win || currentState == GameState.Lost)
            {
                deltaT = 0;
            }

            
            
            if (keyPressed.Contains(Keys.Left))
            {
                if (playerShip.Position.x > 0)
                {
                    playerShip.MoveLeft(deltaT, playerSpeed);
                }
            }

            if (keyPressed.Contains(Keys.Right))
            {
                if (playerShip.Position.x < gameSize.Width-playerShip.image.Width)
                {
                    playerShip.MoveRight(deltaT, playerSpeed);
                }
            }

            if (keyPressed.Contains(Keys.Space))
            {
                if (playerMissile == null)
                {
                    this.playerMissile = new Missile(new Vecteur2D(playerShip.Position.x + playerShip.image.Width / 2, playerShip.Position.y), new Vecteur2D(0, -700), 5);
                }

            }



            if (playerMissile != null)
            {


                playerMissile.Move(deltaT);
                
                foreach (Bunker bunk in bunkers)
                {
                    if (playerMissile != null)
                    {
                        if (bunk.Collision(playerMissile) && playerMissile.Lives<0)
                        {
                            playerMissile = null;
                        }
                    }
                }

                // Je dois retester si le missile existe à chaque fois car il peut être détruit lors du for each 

                if (playerMissile != null)
                {
                    if (playerMissile.Position.y < 0)
                    {
                        playerMissile = null;
                    }
                }
            }



            if (keyPressed.Contains(Keys.P))
            {
                if (pHasBeenReleased)
                {
                    pHasBeenReleased = false;

                    switch (currentState)
                    {
                        case GameState.Play:
                            currentState = GameState.Pause;
                            break;
                        case GameState.Pause:
                            currentState = GameState.Play;
                            break;
                    }

                }
            }
            else
            {
                pHasBeenReleased = true;
            }


            if (playerMissile != null)
            {
                if(ennemies.collision(playerMissile))
                {
                    playerMissile = null;
                }
            }

            ennemies.Move(deltaT, gameSize.Width);
          
        }
        #endregion


    }
}
