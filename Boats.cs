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
    public class Boats : Sprite
    {
        Random random = new Random();
        public float randX, randY;

        //constructor
        public Boats(Texture2D newTexture, Vector2 newPosition)
        {
            position = newPosition;
            texture = newTexture;
            speed = 4;
            isVisible = true;
            randX = random.Next(0, 550);
            randY = random.Next(-600, -50);
        }

        //load content
        public void loadContent(ContentManager Content)
        {
            
        }

        //update
        public void Update(GameTime gameTime)
        {
            //set collison
            boundingBox = new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);

            //update movement
            position.Y = position.Y + speed;
            if (position.Y >= 600)
            {
                position.Y = -50;
            }
        }

        //draw
        public void Draw(SpriteBatch spriteBatch)
        {
            if (isVisible)
            {
                spriteBatch.Draw(texture, position, Color.White);
            }
        }
    }
}
