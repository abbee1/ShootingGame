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
    public class cButton : Sprite
    {
        Rectangle rec;

        Color colour = new Color(255, 255, 255, 255);

        public Vector2 size;

        public cButton( Texture2D newTexture, GraphicsDevice graphics)
        {
            texture = newTexture;
            size = new Vector2(graphics.Viewport.Width / 8, graphics.Viewport.Height / 30);
        }

        bool down;
        public bool isClicked;
        public void Update(MouseState mouse)
        {
            rec = new Rectangle((int)position.X, (int)position.Y, (int)size.X, (int)size.Y);

            Rectangle mouseRec = new Rectangle(mouse.X, mouse.Y, 1, 1);

            if (mouseRec.Intersects(rec))
            {
                //Change of color
                if (colour.A == 255)
                    down = false;
                if (colour.A == 0)
                    down = true;
                if (down) colour.A += 3; else colour.A -= 3;

                //whats gonna happend when pressed
                if (mouse.LeftButton == ButtonState.Pressed) isClicked = true;
            }
            else if (colour.A < 255)
            {
                colour.A += 3;
                isClicked = false;
            }
        }

        public void setPosition(Vector2 newPosition)
        {
            position = newPosition;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, rec, colour);
        }
    }
}
