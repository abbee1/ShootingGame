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
    class HighscoreItem
    {
        private SpriteFont _spriteFont;
        private Vector2 _position;
        private string _name;
        private int _score;

        public HighscoreItem(SpriteFont font, Vector2 position, string name, int score)
        {
            _spriteFont = font;
            _position = position;
            _score = score;
            _name = name;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(_spriteFont, _name + _score, _position, Color.White);
        }
    }
}
