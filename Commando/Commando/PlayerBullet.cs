using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Commando
{
    class PlayerBullet
    {
        public Texture2D bulletTexture;
        public Rectangle rectangle;
        public Vector2 position;
        public Vector2 velocity;
        public Vector2 origin;
        public bool isVisible = true;




        public PlayerBullet(Texture2D newTexture, Rectangle newRectangle)
        {
            bulletTexture = newTexture;
            rectangle = newRectangle;
            velocity = new Vector2(0, -4);
        }
        public void Update()
        {
            rectangle.X += (int)velocity.X;
            rectangle.Y += (int)velocity.Y;

        }
        public void Draw(SpriteBatch spriteBatch, Vector2 scroll)
        {
            Rectangle scrolledRectangle = rectangle;
            scrolledRectangle.Y += (int)scroll.Y;
            spriteBatch.Draw(bulletTexture, scrolledRectangle, Color.White);

        }
    }
}
