using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.IO;
using Microsoft.Xna.Framework.Content;

namespace ShootingGame
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        public enum State
        {
            Menu,
            Playing,
            Gameover,
            EnterName
        }

        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        private SpriteFont sf;
        private SpriteFont fontScore;
        private bool runOnce = false;

        //random
        Random random = new Random();
        
        // player
        Player player = new Player();
        public int enemyBulletDamage;

        //HUD
        HUD hud = new HUD();

        //Highscore ska ändras
        Highscore high = new Highscore();

        //list highscore
        List<HighscoreItem> highscoreItems = new List<HighscoreItem>();

        //lists
        //list with boats
        List<Boats> boatsList = new List<Boats>();
        //list with enemy
        List<Enemy> enemyList = new List<Enemy>();

        //set game state
        State gameStatus = State.Menu;

        //button
        cButton btnPlay;
        cButton btnHighscore;
        cButton btnDone;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.IsFullScreen = false;
            graphics.PreferredBackBufferHeight = 600;
            graphics.PreferredBackBufferWidth = 800;
            this.Window.Title = "Pirate shooting";
            Content.RootDirectory = "Content";
            enemyBulletDamage = 1;
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
            IsMouseVisible = true;
            btnPlay = new cButton(Content.Load<Texture2D>("playBtn"), graphics.GraphicsDevice);
            btnPlay.setPosition(new Vector2(350, 300));
            btnHighscore = new cButton(Content.Load<Texture2D>("highscoreBtn"), graphics.GraphicsDevice);
            btnHighscore.setPosition(new Vector2(350, 350));
            btnDone = new cButton(Content.Load<Texture2D>("done"), graphics.GraphicsDevice);
            btnDone.setPosition(new Vector2(350, 400));
            sf = Content.Load<SpriteFont>("nameEnter");
            fontScore = Content.Load<SpriteFont>("fontScore");
            player.LoadContent(Content);
            hud.LoadContent(Content);

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
            MouseState mouse = Mouse.GetState();

            switch (gameStatus)
            {
                case State.Menu:
                    btnPlay.Update(mouse);
                    if (btnPlay.isClicked == true) {
                        gameStatus = State.Playing;
                    }
                    btnHighscore.Update(mouse);
                    if (btnHighscore.isClicked == true)
                    {
                        gameStatus = State.Gameover;
                    }
                        
                    break;
                case State.Playing:

                    // for each enamy in enemylist check if colliding
                    foreach (Enemy e in enemyList)
                    {
                        //check if enemy collides with player
                        if (e.boundingBox.Intersects(player.boundingBox))
                        {
                            player.health -= 40;
                            e.isVisible = false;
                        }

                        //chek if a enemy bullet is colliding with player
                        for (int i = 0; i < e.bulletList.Count(); i++)
                        {
                            if (player.boundingBox.Intersects(e.bulletList[i].boundingBox))
                            {
                                player.health -= enemyBulletDamage;
                                e.bulletList[i].isVisible = false;
                            }
                        }

                        //check if a player bullet is hitning enemy
                        for (int i = 0; i < player.bulletList.Count; i++)
                        {
                            if (player.bulletList[i].boundingBox.Intersects(e.boundingBox))
                            {
                                hud.playerScore += 20;
                                player.bulletList[i].isVisible = false;
                                e.isVisible = false;
                            }
                        }
                        if (e.isVisible)
                            e.Update(gameTime);
                    }

                    //for each boat in boatList call boat.update(gameTime)
                    foreach (Boats b in boatsList)
                    {
                        //check if boat is colliding with player
                        if (b.boundingBox.Intersects(player.boundingBox))
                        {
                            b.isVisible = false;
                            player.health -= 5;
                        }

                        //check if player bullets colliding with a boat
                        for (int i = 0; i < player.bulletList.Count(); i++)
                        {
                            if (b.boundingBox.Intersects(player.bulletList[i].boundingBox))
                            {
                                hud.playerScore += 5;
                                b.isVisible = false;
                                player.bulletList[i].isVisible = false;
                            }
                        }


                        b.Update(gameTime);
                    }

                    //getting keyboarde state
                    KeyboardState keyState = Keyboard.GetState();

                    if (keyState.IsKeyDown(Keys.P))
                    {
                        gameStatus = State.Menu;
                        btnPlay.isClicked = false;
                    }
                    if (player.health <=0)
                    {
                        gameStatus = State.EnterName;
                        break;
                    }
                    int randY = random.Next(-600, -50);
                    int randX = random.Next(0, 550);
                    LoadBoats(randX, randY);
                    LoadEnemies(randX, randY);
                    player.Update(gameTime);
                    //hud.Update(gameTime);
                    break;

                case State.Gameover:
                    KeyboardState keyState2 = Keyboard.GetState();
                    if (keyState2.IsKeyDown(Keys.Enter))
                    {
                        gameStatus = State.Menu;
                        btnPlay.isClicked = false;
                        btnHighscore.isClicked = false;
                        reset();
                    }
                    break;
                case State.EnterName:
                    btnDone.Update(mouse);
                    high.GetKeys();
                    if (btnDone.isClicked == true)
                    {
                        gameStatus = State.Gameover;
                        Save(high.name, hud.playerScore);
                    }
                    break;
            }

           

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
            switch (gameStatus)
            {
                case State.Menu:
                    spriteBatch.Draw(Content.Load<Texture2D>("mainMenu"), new Rectangle(0, 0,800,600),Color.White);
                    btnPlay.Draw(spriteBatch);
                    btnHighscore.Draw(spriteBatch);
                    break;
                case State.Playing:
                    hud.Draw(spriteBatch);
                    player.Draw(spriteBatch);
                    foreach (Boats b in boatsList)
                    {
                        b.Draw(spriteBatch);
                    }
                    foreach (Enemy e in enemyList)
                    {
                        if (e.isVisible)
                            e.Draw(spriteBatch);
                    }
                    break;

                case State.Gameover:
                    spriteBatch.Draw(Content.Load<Texture2D>("highscore"), new Rectangle(0, 0, 800, 600), Color.White);
                    if(!runOnce)
                        loadHighscore(fontScore);
                    runOnce = true;
                    foreach (HighscoreItem item in highscoreItems)
                    {
                        item.Draw(spriteBatch);
                    }
                    break;
                case State.EnterName:
                    
                    spriteBatch.Draw(Content.Load<Texture2D>("name"), new Rectangle(0, 0, 800, 600), Color.White);
                    spriteBatch.DrawString(sf, high.name, new Vector2(305, 325), Color.Red);
                    btnDone.Draw(spriteBatch);
                    break;
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
                if (!boatsList[i].isVisible || boatsList[i].position.Y == 650)
                {
                    boatsList.RemoveAt(i);
                    i--;
                }
            }
        }

        //load enimie
        public void LoadEnemies(int x, int y)
        {
            //if enimies is less then 5 add a new enimie
            if (enemyList.Count() < 3)
            {
                enemyList.Add(new Enemy(Content.Load<Texture2D>("helicopter2"), new Vector2(x, y), Content.Load<Texture2D>("enemyBullet")));
            }
            
            //if any of the enimies are dead or not visible so shall it be removed from the list
            for (int i = 0; i < enemyList.Count(); i++)
            {
                
                if (!enemyList[i].isVisible || enemyList[i].position.Y == 650)
                {
                    enemyList.RemoveAt(i);
                    i--;
                }
            }
        }

        public void reset()
        {
            player.health = 200;
            hud.playerScore = 0;
            enemyList.Clear();
            boatsList.Clear();
            player.bulletList.Clear();
        }

        public void loadHighscore(SpriteFont spriteFont)
        {
            int postion = 200;
            int i = 0;
            
            string[] lines = System.IO.File.ReadAllLines("score.txt");
            
            foreach (string line in lines)
            {
                if (!string.IsNullOrEmpty(line))
                {
                    i++;
                    string[] args = line.Split('+');
                    highscoreItems.Add(new HighscoreItem(spriteFont, new Vector2(300, postion + 30), i.ToString()+ ": " +args[0] + " ", int.Parse(args[1])));
                    postion += 20;
                }
            }
        }
        public void Save(string name, int score)
        {
            using (StreamWriter sw = new StreamWriter("score.txt"))
            {
                sw.WriteLine(name + "+" + score);
                
            }
        }
    }
}
