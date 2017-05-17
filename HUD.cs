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
    public class HUD : Sprite
    {
        public HUD()
        {
            playerScore = 0;
            showHud = true;
            screenHeight = 600;
            screenWidth = 800;
            playerScoreFont = null;
            playerScorePos = new Vector2(screenWidth / 2, 50);
        }

        public void LoadContent(ContentManager Content)
        {
            playerScoreFont = Content.Load<SpriteFont>("playerScore");
        }

        public void Update(GameTime gameTime)
        {
            // get keyborde state
            KeyboardState keyState = Keyboard.GetState();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (showHud)
            {
                spriteBatch.DrawString(playerScoreFont, "Score: " + playerScore, playerScorePos, Color.Black);
            }

        }
    }
}
