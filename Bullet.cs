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
    class Bullet
    {
        public Rectangle rec;
        public Texture2D texture;
        public Vector2 position;
        public bool isVisible;
        public float speed;

        //constructor
        public Bullet(Texture2D newTexture, Vector2 position)
        {
            speed = 10;
            texture = newTexture;
            isVisible = false;
            this.position = position;
        }

        //update 
        public void Update(GameTime gameTime)
        {

        }

        //Draw
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, Color.White);
        }
    }
}
