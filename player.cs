using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ShootingGame
{
    public class Player
    {
        public Texture2D texture, healthTexture;
        public Texture2D bulletTexture;
        public Vector2 position, healthPosition;
        public int speed;
        public float bulletDelay;
        public List<Bullet> bulletList;
        public int health;

        //collision
        public Rectangle boundingBox, healthRec;
        public bool isColidning;

        //constructor
        public Player()
        {
            bulletList = new List<Bullet>();
            texture = null;
            position = new Vector2(300, 300);
            speed = 10;
            isColidning = false;
            bulletDelay = 20;
            health = 200;
            healthPosition = new Vector2(50, 50);
        }

        //load content
        public void LoadContent(ContentManager Content)
        {
            texture = Content.Load<Texture2D>("pirateShip");
            bulletTexture = Content.Load<Texture2D>("kanonkula2");
            healthTexture = Content.Load<Texture2D>("healthbar");
        }

        //draw
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, Color.White);
            spriteBatch.Draw(healthTexture, healthRec, Color.White);

            foreach(Bullet b in bulletList)
            {
                b.Draw(spriteBatch);
            }
        }

        //update
        public void Update(GameTime gametime)
        {
            //getting keyboarde state
            KeyboardState keyState = Keyboard.GetState();

            //player bounding box
            boundingBox = new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);

            //set healtrec
            healthRec = new Rectangle((int)healthPosition.X, (int)healthPosition.Y, health, 25);

            //fire bullet
            if (keyState.IsKeyDown(Keys.Space))
            {
                shoot();
            }
            updateBullet();

            // ship controllers
            if (keyState.IsKeyDown(Keys.Up))
            {
                position.Y = position.Y - speed;
            }
            if (keyState.IsKeyDown(Keys.Down))
            {
                position.Y = position.Y + speed;
            }
            if (keyState.IsKeyDown(Keys.Right))
            {
                position.X = position.X + speed;
            }
            if (keyState.IsKeyDown(Keys.Left))
            {
                position.X = position.X - speed;
            }
            //check collison with walls
            if (position.X <= 0 )
            {
                position.X = 0;
            }
            if (position.X >= 800 - texture.Width)
            {
                position.X = 800 - texture.Width;
            }
            if (position.Y <= 0)
            {
                position.Y = 0;
            }
            if (position.Y >= 600 - texture.Height)
            {
                position.Y = 600 - texture.Height;
            }
        }

        //shot method starting position of bullet
        public void shoot()
        {
            if (bulletDelay >= 0)
            {
                bulletDelay--;
            }
            Console.WriteLine(bulletDelay);

            if(bulletDelay <= 0)
            {
                Bullet newBullet = new Bullet(bulletTexture, position);
                newBullet.position = new Vector2(position.X + texture.Width / 2 - newBullet.texture.Width / 2,position.Y + texture.Height / 2);
                
                newBullet.isVisible = true;

                bulletList.Add(new Bullet(bulletTexture, newBullet.position));
            }

            //reset bullet delay
            if (bulletDelay == 0)
            {
                bulletDelay = 20;
            }
        }

        public void updateBullet()
        {
            foreach (Bullet b in bulletList)
            {
                //bullet bounding box
                b.boundingBox = new Rectangle((int)b.position.X, (int)b.position.Y, b.texture.Width, b.texture.Height);

                b.position.Y = b.position.Y - b.speed;

                if (b.position.Y >= 600)
                {
                    b.isVisible = false;
                }
            }

            for (int i = 0; i < bulletList.Count; i++)
            {
                if (bulletList[i].isVisible)
                {
                    Console.WriteLine("antal kulor: " + bulletList.Count);
                    bulletList.RemoveAt(i);
                    i--;
                }
            }
        }

    }
}
