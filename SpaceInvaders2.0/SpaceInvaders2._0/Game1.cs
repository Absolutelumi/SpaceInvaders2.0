using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;


namespace SpaceInvaders2._0
{
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        #region
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Rectangle mainFrame;
        Texture2D spaceship;
        Texture2D background;
        Texture2D laser; 
        Texture2D asteroid;
        Texture2D Healthbar;
        SpriteFont Reg;
        SpriteFont gameover;
        SoundEffect milktea;


        static private Random random = new Random();

        static int spaceshipx = 250;
        int distance = spaceshipx + 100;

        int healthx = 350;
        int GameScore = 0;
        int Health = 5;

        int damage = 350 / 5; 
 
        int shootx;
        int shooty = 600;
        int whenShot; 

        bool alive = true;
        bool shoot = false;
        bool pause = false;
        bool ses = false;

        int min = 0;
        float seconds = 0f; 
       
        static int asteroidx1 = random.Next(0, 600);
        static int asteroidx2 = random.Next(0, 600);
        static int asteroidx3 = random.Next(0, 600);

        static int asteroidy = random.Next(-200, -50); 
        static int asteroidy2 = random.Next(-200, -50);
        static int asteroidy3 = random.Next(-200, -50);
        int asteroidSpeed = 3;
        bool asteroid2 = false;
        bool asteroid3 = false;

        Rectangle Hitbox = new Rectangle(asteroidx1, asteroidy, 100, 100);
        Rectangle Hitbox2 = new Rectangle(asteroidx2, asteroidy2, 100, 100); 
        Rectangle Hitbox3;
        #endregion


        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            graphics.PreferredBackBufferHeight = 800;
            graphics.PreferredBackBufferWidth = 700;
            graphics.ApplyChanges();
            IsMouseVisible = false;
            IsFixedTimeStep = false;
            

            base.Initialize();
        }

        protected override void LoadContent()
        {
            Reg = Content.Load<SpriteFont>("reg");
            gameover = Content.Load<SpriteFont>("gameover"); 
            background = Content.Load<Texture2D>("space");
            spaceship = Content.Load<Texture2D>("spaceship");
            asteroid = Content.Load<Texture2D>("rock4_b");
            Healthbar = Content.Load<Texture2D>("Healthbar");
            laser = Content.Load<Texture2D>("Red_laser");
            milktea = Content.Load<SoundEffect>("milk");
            milktea.Play(); 
            spriteBatch = new SpriteBatch(GraphicsDevice);
        }

        protected override void UnloadContent()
        {

        }

        protected override void Update(GameTime gameTime)
        {
            KeyboardState NewKeyState = Keyboard.GetState();
            
            // Movement
            if (Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                spaceshipx += (int)(500 * gameTime.ElapsedGameTime.TotalSeconds);
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                spaceshipx += (int)(-500 * gameTime.ElapsedGameTime.TotalSeconds);
            }

            // Shooting
            if (Keyboard.GetState().IsKeyDown(Keys.Space))
            {
                DateTime Fired = DateTime.Now; 
                shoot = true;  
            }
            if (shooty <= -25)
            {
                shoot = false;
                shooty = 600; 
            }
            if (laser.Equals(Hitbox))
            {
                asteroidy = random.Next(-200, -50);
                GameScore++; 
            }
            if (laser.Equals(Hitbox2))
            {
                asteroidy2 = random.Next(-200, -50);
                GameScore++; 
            }
            if (laser.Equals(Hitbox3))
            {
                asteroidy3 = random.Next(-200, -50);
                GameScore++; 
            }
            if (ses == true)
            {
                whenShot = spaceshipx + 40; 
            }
            if (shoot == true)
            {
                shootx = whenShot;
                shooty = shooty - 9;
            }
            

            // Pause
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                pause = true;
                this.Exit(); 
            }

            // Borders
            if (spaceshipx >= 600)
            {
                spaceshipx = 600; 
            }
            if(spaceshipx <= 0)
            {
                spaceshipx = 0; 
            }

            // Damage
            if(asteroidy >= 800)
            {
                Health = Health - 1;
                healthx = healthx - damage;
                asteroidy = -50;
                asteroidx1 = random.Next(0, 600);
            }
            if (asteroidy2 >= 800)
            {
                Health = Health - 1;
                healthx = healthx - damage;
                asteroidy2 = -50;
                asteroidx2 = random.Next(0, 600);
            }
            if (asteroidy3 >= 800)
            {
                Health = Health - 1;
                healthx = healthx - damage;
                asteroidy3 = -50;
                asteroidx3 = random.Next(0, 600);
            }

            if (Health <= 0)
            {
                alive = false; 
            }

            if (spaceshipx <= asteroidx1 && distance >= asteroidx1 && asteroidy >= 700)
            {
                asteroidy = random.Next(-200, -50);
                asteroidx1 = random.Next(0, 600);
                Health = Health - 2;
                healthx = healthx - damage * 2;
            }
            if (spaceshipx <= asteroidx2 && distance >= asteroidx2 && asteroidy2 >= 700)
            {
                asteroidy2 = random.Next(-200, -50);
                asteroidx2 = random.Next(0, 600);
                Health = Health - 2;
                healthx = healthx - damage * 2;
            }
            if (spaceshipx <= asteroidx3 && distance >= asteroidx3 && asteroidy3 >= 700)
            {
                asteroidy3 = random.Next(-200, -50);
                asteroidx3 = random.Next(0, 600);
                Health = Health - 2;
                healthx = healthx - damage * 2;
            }

            // Time
            if (seconds >= 60)
            {
                min++;
                seconds = 0; 
            }
            if(shooty <= -50)
            {
                shooty = 600; 
            }
            if(min == 1)
            {
                asteroid2 = true; 
            }
            if(min == 2 && seconds == 0)
            {
                asteroid3 = true;
                asteroidSpeed = asteroidSpeed + 1;
            }

            // Retry
            if (alive == false)
            {
                
            }

            // Asteroid Transformations
            asteroidy = asteroidy + asteroidSpeed;
            asteroidy2 = asteroidy2 + asteroidSpeed;
            asteroidy3 = asteroidy3 + asteroidSpeed; 

            // Time
            seconds += (float)gameTime.ElapsedGameTime.TotalSeconds;
            

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            Texture2D[] Bullets = new Texture2D[5];
            
            for (int i = 0; i < 5; i++)
            {
                Bullets[i] = laser; 
            }
            Vector2 FontOrigin = Reg.MeasureString(GameScore.ToString()) / 2;
            GraphicsDevice.Clear(Color.CornflowerBlue);
            mainFrame = new Rectangle(0, 0, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height);
            Hitbox = new Rectangle(asteroidx1, asteroidy, 100, 100);
            Hitbox2 = new Rectangle(asteroidx2, asteroidy2, 100, 100);
            Hitbox3 = new Rectangle(asteroidx3, asteroidy3, 100, 100);
            spriteBatch.Begin();

            // Main
            spriteBatch.Draw(background, mainFrame, Color.White);
            spriteBatch.Draw(spaceship, new Rectangle(spaceshipx, 700, 100, 100), Color.White);

            // Asteroid
            spriteBatch.Draw(asteroid, new Rectangle(asteroidx1, asteroidy, 100, 100), Color.White);
            if(asteroid2 == true)
            {
                spriteBatch.Draw(asteroid, new Rectangle(asteroidx2, asteroidy2, 100, 100), Color.White);
            }
            if(asteroid3 == true)
            {
                spriteBatch.Draw(asteroid, new Rectangle(asteroidx3, asteroidy3, 100, 100), Color.White);
            }

            // UI
            spriteBatch.Draw(Healthbar, new Rectangle(0, 0, healthx, 25), Color.White);

            spriteBatch.DrawString(Reg, gameTime.ElapsedGameTime.ToString("mmss"), new Vector2(580, 150), Color.White, 0, FontOrigin, 1.0f, SpriteEffects.None, 0.5f);

            if (shoot == true)
            {
                for (int i = 0; i < 5; i++)
                {
                    spriteBatch.Draw(Bullets[i], new Rectangle(shootx, shooty, 25, 75), Color.White); 
                }
            }
            spriteBatch.DrawString(Reg, GameScore.ToString(), new Vector2(620, 50), Color.White, 0, FontOrigin, 1.0f, SpriteEffects.None, 0.5f);
            if(pause == false)
            {

            }
            if (alive == false)
            {
                spriteBatch.Draw(background, new Rectangle(0, 0, 700, 800), Color.Black);
                spriteBatch.DrawString(gameover, "Game Over", new Vector2(150, 200), Color.White, 0, FontOrigin, 1.0f, SpriteEffects.None, 0.5f);
                spriteBatch.DrawString(Reg, "Retry?", new Vector2(280, 350), Color.White, 0, FontOrigin, 1.0f, SpriteEffects.None, 0.5f);
                spriteBatch.DrawString(Reg, "Yes", new Vector2(250, 500), Color.White, 0, FontOrigin, 1.0f, SpriteEffects.None, 0.5f);
                spriteBatch.DrawString(Reg, "No", new Vector2(450, 500), Color.White, 0, FontOrigin, 1.0f, SpriteEffects.None, 0.5f);
            }
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }

}
