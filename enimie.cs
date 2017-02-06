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
    class enimie
    {
        public Rectangle rec;
        public Texture2D texture;
        public Vector2 position;
        public int speed;
        public bool isCollide, destroyd;

        //constructor
        public enimie()
        {
            position = new Vector2(400, -50);
            texture = null;
            speed = 4;
            isCollide = false;
            destroyd = false;
        }

        //load content
        public void loadContent(ContentManager Content)
        {
            texture = Content.Load<Texture2D>("boot");
        }

        //update
        public void Update(GameTime gameTime)
        {
            //set collison
            rec = new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);

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
            if (!destroyd)
            {
                spriteBatch.Draw(texture, position, Color.White);
            }
        }
    }
}
