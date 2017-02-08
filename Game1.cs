using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ShootingGame
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        //random
        Random random = new Random();
        
        // player
        Player player = new Player();
        public int enemyBulletDamage;

        //lists
        //list with boats
        List<Boats> boatsList = new List<Boats>();
        //list with enemy
        List<Enemy> enemyList = new List<Enemy>();

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.IsFullScreen = false;
            graphics.PreferredBackBufferHeight = 600;
            graphics.PreferredBackBufferWidth = 800;
            this.Window.Title = "Pirate shooting";
            Content.RootDirectory = "Content";
            enemyBulletDamage = 10;
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

            player.LoadContent(Content);

            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
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
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            foreach(Enemy e in enemyList){
                if (e.boundingBox.Intersects(player.boundingBox)){
                    player.health -= 40;
                    e.isVisible = false;
                }

                for(int i = 0; i < e.bulletList.Count(); i++)
                {
                    if (player.boundingBox.Intersects(e.bulletList[i].boundingBox))
                    {
                        player.health -= enemyBulletDamage;
                        e.bulletList[i].isVisible = false;  
                    }
                }

                for (int i = 0; i < player.bulletList.Count; i++)
                {
                    if (player.bulletList[i].boundingBox.Intersects(e.boundingBox))
                    {
                        player.bulletList[i].isVisible = false;
                        e.isVisible = false;
                    }
                }

                e.Update(gameTime);
            }
            
            //for each enimie in enemiesList call Enimie.update(gameTime)
            foreach (Boats b in boatsList)
            {
                //check if enimie is colliding with player
                if (b.boundingBox.Intersects(player.boundingBox))
                {
                    b.isVisible = false;
                }

                for (int i = 0; i < player.bulletList.Count(); i++)
                {
                    if (b.boundingBox.Intersects(player.bulletList[i].boundingBox))
                    {
                        b.isVisible = false;
                        player.bulletList[i].isVisible = false;
                    }
                }


                b.Update(gameTime);
            }
            int randY = random.Next(-600, -50);
            int randX = random.Next(0, 550);
            LoadBoats(randX,randY);
            LoadEnemies(randX, randY);
            player.Update(gameTime);

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();
            
            player.Draw(spriteBatch);
            foreach (Boats b in boatsList)
            {
                b.Draw(spriteBatch);
            }
            foreach (Enemy e in enemyList)
            {
                e.Draw(spriteBatch);
            }
            spriteBatch.End();
            
            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }

        //load enimie
        public void LoadBoats(int x, int y) {

            //creating random varibles for the enimie starts position
            

            //if enimies is less then 5 add a new enimie
            if (boatsList.Count() < 5)
            {
                boatsList.Add(new Boats(Content.Load<Texture2D>("boot"), new Vector2(x, y)));
            }

            //if any of the enimies are dead or not visible so shall it be removed from the list
            for (int i = 0; i < boatsList.Count(); i++)
            {
                if (!boatsList[i].isVisible)
                {
                    boatsList.RemoveAt(i);
                    i--;
                }
            }
        }

        //load enimie
        public void LoadEnemies(int x, int y)
        {

            //creating random varibles for the enimie starts position


            //if enimies is less then 5 add a new enimie
            if (enemyList.Count() < 3)
            {
                enemyList.Add(new Enemy(Content.Load<Texture2D>("helicopter2"), new Vector2(x, y), Content.Load<Texture2D>("enemyBullet")));
            }

            //if any of the enimies are dead or not visible so shall it be removed from the list
            for (int i = 0; i < enemyList.Count(); i++)
            {
                if (!enemyList[i].isVisible)
                {
                    enemyList.RemoveAt(i);
                    i--;
                }
            }
        }
    }
}
