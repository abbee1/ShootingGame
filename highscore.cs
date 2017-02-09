using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ShootingGame
{
    class Highscore
    {
        private Keys[] lastPressedKeys = new Keys[1];
        public string name = string.Empty;
        private bool caps;

        public void GetKeys()
        {
            KeyboardState kbState = Keyboard.GetState();
            Keys[] pressedKeys = kbState.GetPressedKeys();

            //check if any of the previous update's keys are no longer pressed
            foreach (Keys key in lastPressedKeys)
            {
                if (!pressedKeys.Contains(key))
                    OnKeyUp(key);
            }

            //check if the currently pressed keys were already pressed
            foreach (Keys key in pressedKeys)
            {
                if (!lastPressedKeys.Contains(key))
                    OnKeyDown(key);
            }
            //save the currently pressed keys so we can compare on the next update
            lastPressedKeys = pressedKeys;
        }

        private void OnKeyDown(Keys key)
        {
            //do stuff
            if (key == Keys.Back && name.Length > 0) //Removes a letter from the name if there is a letter to remove
            {
                name = name.Remove(name.Length - 1);
            }
            else if (key == Keys.LeftShift || key == Keys.RightShift)//Sets caps to true if a shift key is pressed
            {
                caps = true;
            }
            else if (!caps && name.Length < 16) //If the name isn't too long, and !caps the letter will be added without caps
            {
                if (key == Keys.Space)
                {
                    name += " ";
                }
                else
                {
                    name += key.ToString().ToLower();
                }
            }
            else if (name.Length < 16) //Adds the letter to the name in CAPS
            {
                name += key.ToString();
            }
        }

        private void OnKeyUp(Keys key)
        {
            //Sets caps to false if one of the shift keys goes up
            if (key == Keys.LeftShift || key == Keys.RightShift)
            {
                caps = false;
            }
        }
    }
}
