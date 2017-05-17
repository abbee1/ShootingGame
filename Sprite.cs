using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShootingGame
{
    public class Sprite
    {
        public Rectangle boundingBox;
        public Texture2D texture;
        public Texture2D bulletTexture;
        public Vector2 position;
        public int health;
        public int speed;
        public int bulletDelay;
        public int currentDifficulLevel;
        public bool isVisible;
        public List<Bullet> bulletList;
        public int playerScore;
        public int screenWidth;
        public int screenHeight;
        public SpriteFont playerScoreFont;
        public Vector2 playerScorePos;
        public bool showHud;
        public Rectangle healthRec;
        public bool isColidning;
        public Vector2 healthPosition;
        public Texture2D healthTexture;
    }
}
