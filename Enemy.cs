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
    public class Enemy
    {
        public Rectangle boundingBox;
        public Texture2D texture, bulletTexture;
        public Vector2 position;
        public int health, speed, bulletDelay, currentDifficulLevel;
        public bool isVisible;
        public List<Bullet> bulletList;

        public Enemy(Texture2D newTexture, Vector2 newPosition, Texture2D newBulletTexture)
        {
            bulletList = new List<Bullet>();
            texture = newTexture;
            bulletTexture = newBulletTexture;
            health = 5;
            position = newPosition;
            currentDifficulLevel = 1;
            bulletDelay = 40;
            speed = 5;
            isVisible = true;
        }

        public void Update(GameTime gameTime){
            // update collison
            boundingBox = new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);

            //update movment
            position.Y += speed;

            if (position.Y >= 800)
            {
                position.Y = -75;
            }

            EnemyShoot();
            updateBullet();
        }

        public void Draw(SpriteBatch spriteBatch){
            spriteBatch.Draw(texture, position, Color.White);

            foreach (Bullet b in bulletList)
            {
                b.Draw(spriteBatch);
            }
        }

        public void updateBullet()
        {
            foreach (Bullet b in bulletList)
            {
                //bullet bounding box
                b.boundingBox = new Rectangle((int)b.position.X, (int)b.position.Y, b.texture.Width, b.texture.Height);

                b.position.Y = b.position.Y + b.speed;

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

        public void EnemyShoot()
        {
            if (bulletDelay >= 0)
            {
                bulletDelay--;
            }
            Console.WriteLine(bulletDelay);

            if (bulletDelay <= 0)
            {
                Bullet newBullet = new Bullet(bulletTexture, position);
                newBullet.position = new Vector2(position.X + texture.Width / 2 - newBullet.texture.Width / 2, position.Y + texture.Height / 2);

                newBullet.isVisible = true;
                
                bulletList.Add(new Bullet(bulletTexture, newBullet.position));
                
            }

            //reset bullet delay
            if (bulletDelay == 0)
            {
                bulletDelay = 20;
            }
        }

    }
}
