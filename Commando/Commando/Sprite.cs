using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Commando
{
    class Sprite
    {
        public Texture2D texture;
        public Rectangle rectangle;
        public Vector2 position;

        public virtual void Update()
        {

        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, rectangle, Color.White);
        }
    }
    class Player : Sprite
    {
        public Player(Texture2D newTexture, Rectangle newRectangle)
        {
            texture = newTexture;
            rectangle = newRectangle;
        }
        public void Update(ref Vector2 refScroll, ref string direction)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                rectangle.X += 3;
                direction = "right";
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                rectangle.X -= 3;
                direction = "left";
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Up))
            {
                refScroll.Y += 3;
                direction = "up";
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Down))
            {
                refScroll.Y -= 3;
                direction = "down";
            }
        }
    }
    class Pebble : Sprite
    {
        public Pebble(Texture2D newTexture, Rectangle newRectangle)
        {
            texture = newTexture;
            rectangle = newRectangle;
        }
        public void Draw(SpriteBatch spriteBatch, Vector2 scroll)
        {
            Rectangle scrolledRectangle = rectangle;
            scrolledRectangle.Y += (int)scroll.Y;
            spriteBatch.Draw(texture, scrolledRectangle, Color.White);
        }

    }
    class Palmtree : Sprite
    {
        public Palmtree(Texture2D newTexture, Rectangle newRectangle)
        {
            texture = newTexture;
            rectangle = newRectangle;
        }
        public void Draw(SpriteBatch spriteBatch, Vector2 scroll)
        {
            Rectangle scrolledRectangle = rectangle;
            scrolledRectangle.Y += (int)scroll.Y;
            spriteBatch.Draw(texture, scrolledRectangle, Color.White);
        }
    }
    class PalmtreeGroup : Sprite
    {
        public PalmtreeGroup(Texture2D newTexture, Rectangle newRectangle)
        {
            texture = newTexture;
            rectangle = newRectangle;
        }
        public void Draw(SpriteBatch spriteBatch, Vector2 scroll)
        {
            Rectangle scrolledRectangle = rectangle;
            scrolledRectangle.Y += (int)scroll.Y;
            spriteBatch.Draw(texture, scrolledRectangle, Color.White);
        }
    }
    class Ammo : Sprite
        {
        public Ammo(Texture2D newTexture, Rectangle newRectangle)
        {
            texture = newTexture;
            rectangle = newRectangle;
        }
        public void Draw(SpriteBatch spriteBatch, Vector2 scroll)
        {
            Rectangle scrolledRectangle = rectangle;
            scrolledRectangle.Y += (int)scroll.Y;
            spriteBatch.Draw(texture, scrolledRectangle, Color.White);
        }
        public bool Collide(int X, int Y)
        {
            Vector2 point1 = new Vector2(X, Y);
            Vector2 point2 = new Vector2(rectangle.X, rectangle.Y);
            float distance = Vector2.Distance(point1, point2);
            if (distance > 50)
            {
                return false;
            }
            return true;
        }
    }
    class Boulder : Sprite
    {
        public Boulder(Texture2D newTexture, Rectangle newRectangle)
        {
            texture = newTexture;
            rectangle = newRectangle;
        }
        public void Draw(SpriteBatch spriteBatch, Vector2 scroll)
        {
            Rectangle scrolledRectangle = rectangle;
            scrolledRectangle.Y += (int)scroll.Y;
            spriteBatch.Draw(texture, scrolledRectangle, Color.White);
        }
    }
}
