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
using System.Diagnostics;

namespace Commando
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Player player;
        Texture2D playerUp;
        Texture2D playerLeft;
        Texture2D playerRight;
        Texture2D playerDown;
        PlayerBullet playerBullet;
        Enemy enemy;
        // PlayerBullet playerBullet;
        Vector2 scroll = new Vector2();
        Vector2 distance;
        SpriteFont font;
        Stopwatch stopwatch = new Stopwatch();
        Random randomizer = new
        Random();
        //Screen parameters
        int screenWidth;
        int screenHeight;

        int score;
        int playerAmmo;
        int lives = 3;
        string direction = "up";

        //Lists
        List<Enemy> enemies = new List<Enemy>();
        List<Pebble> pebbles = new List<Pebble>();
        List<Palmtree> palmtrees = new List<Palmtree>();
        List<PalmtreeGroup> palmtreeGroups = new List<PalmtreeGroup>();
        List<Ammo> ammos = new List<Ammo>();
        List<Boulder> boulders = new List<Boulder>();
        List<PlayerBullet> playerBullets = new List<PlayerBullet>();

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            scroll.X = 0;
            scroll.Y = 0;
            stopwatch.Start();
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            font = Content.Load<SpriteFont>("Spritefont1");
            for (int i = 0; i < 200; i++) //Pebbles
            {
                pebbles.Add(new Pebble(Content.Load<Texture2D>("pebble"), new Rectangle(randomizer.Next(840), randomizer.Next(-1000, 480), 4, 4)));
            }
            for (int i = 0; i < 20; i++) //Palmtrees
            {
                palmtrees.Add(new Palmtree(Content.Load<Texture2D>("palmtree"), new Rectangle(randomizer.Next(840), randomizer.Next(-1000, 480), 76, 96)));
            }
            for (int i = 0; i < 5; i++) //Palmtree groups
            {
                palmtreeGroups.Add(new PalmtreeGroup(Content.Load<Texture2D>("palmtree_group"), new Rectangle(randomizer.Next(840), randomizer.Next(-1000, 480), 152, 112)));
            }
            for (int i = 0; i < 5; i++) //Ammo
            {
                ammos.Add(new Ammo(Content.Load<Texture2D>("ammo"), new Rectangle(randomizer.Next(840), randomizer.Next(-1000, 480), 48, 40)));
            }
            for (int i = 0; i < 100; i++) //Boulder
            {
                boulders.Add(new Boulder(Content.Load<Texture2D>("boulder"), new Rectangle(randomizer.Next(840), randomizer.Next(-1000, 480), 16, 14)));
            }
            for (int i = 0; i < 10; i++) //Enemy
            {
                enemies.Add(new Enemy(Content.Load<Texture2D>("enemy_running"), new Rectangle(randomizer.Next(840), randomizer.Next(-1000, 480), 40, 42)));
            }
            player = new Player(Content.Load<Texture2D>("hero_up"), new Rectangle(240, 300, 36, 42));
            playerDown = Content.Load<Texture2D>("hero_down");
            playerUp = Content.Load<Texture2D>("hero_up");
            playerRight = Content.Load<Texture2D>("hero_right");
            playerLeft = Content.Load<Texture2D>("hero_left");
            playerBullet = new PlayerBullet(Content.Load<Texture2D>("shot"), new Rectangle(0, 0, 0, 0));
            screenWidth = GraphicsDevice.Viewport.Width;
            screenHeight = GraphicsDevice.Viewport.Height;
            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                this.Exit();
            // TODO: Add your update logic here
            

            player.Update(ref scroll, ref direction);
            // Förhindrar spelaren från att gå utanför skärmen
            if (player.rectangle.X <= 0)
                player.rectangle.X = 0;
            if (player.rectangle.X + player.texture.Width >= screenWidth)
                player.rectangle.X = screenWidth - player.texture.Width;

            if (player.rectangle.Y <= 0)
                player.rectangle.Y = 0;
            if (player.rectangle.Y + player.texture.Height >= screenHeight)
                player.rectangle.Y = screenHeight - player.texture.Height;

            for (int i = 0; i < ammos.Count; i++)
            {
                if (ammos[i].Collide((int)player.rectangle.X, (int)player.rectangle.Y - (int)scroll.Y))
                {
                    ammos.RemoveAt(i);
                    playerAmmo += 5;
                    i--;
                }
            }
            if (stopwatch.ElapsedMilliseconds > 250) //Låter bara spelaren skjuta 1 skott per 250 ms
            {
                if (Keyboard.GetState().IsKeyDown(Keys.Space))
                {
                    playerBullets.Add(new PlayerBullet(playerBullet.bulletTexture, new Rectangle(player.rectangle.X - (int)scroll.X, player.rectangle.Y - (int)scroll.Y, 4, 4)));
                    stopwatch.Restart();
                }

            }
            UpdateBullets();
            base.Update(gameTime);
        }
        public void UpdateBullets()
        {
            foreach (PlayerBullet playerBullet in playerBullets)
            {
                playerBullet.Update();
                if (Vector2.Distance(playerBullet.position, player.position) > 500)
                {
                    playerBullet.isVisible = false;
                }

            }
            for (int i = 0; i < playerBullets.Count; i++)
            {
                if (!playerBullets[i].isVisible)
                {
                    playerBullets.RemoveAt(i);
                    i--;
                }
            }
        }
        public void Shoot()
        {

        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.SandyBrown);
            spriteBatch.Begin();

            switch (direction)
            {
                case ("right"):
                    player.texture = playerRight;
                    break;
                case ("left"):
                    player.texture = playerLeft;
                    break;
                case ("up"):
                    player.texture = playerUp;
                    break;
                case ("down"):
                    player.texture = playerDown;
                    break;
            }
            player.Draw(spriteBatch);
            foreach (Ammo ammo in ammos)
                ammo.Draw(spriteBatch, scroll);
            foreach (Boulder boulder in boulders)
                boulder.Draw(spriteBatch, scroll);
            foreach (Pebble pebble in pebbles)
                pebble.Draw(spriteBatch, scroll);
            foreach (Palmtree palmtree in palmtrees)
                palmtree.Draw(spriteBatch, scroll);
            foreach (PalmtreeGroup palmtreeGroup in palmtreeGroups)
                palmtreeGroup.Draw(spriteBatch, scroll);
            foreach (PlayerBullet playerBullet in playerBullets)
                playerBullet.Draw(spriteBatch, scroll);
            foreach (Enemy enemy in enemies)
                enemy.Draw(spriteBatch, scroll);


            player.Draw(spriteBatch);

            spriteBatch.DrawString(font, "Score: " + score, new Vector2(20, 440), Color.White);
            spriteBatch.DrawString(font, "Ammo: " + playerAmmo, new Vector2(350, 440), Color.White);
            spriteBatch.DrawString(font, "Lives: " + lives, new Vector2(700, 440), Color.White);

            spriteBatch.End();
            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
