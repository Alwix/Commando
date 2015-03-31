using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Commando
{
    class Enemy
    {
        public Texture2D enemyTexture;
        public Rectangle rectangle;
        public Vector2 position;
        public Vector2 velocity;
        public Vector2 origin;
        public bool isVisible = true;




        public Enemy(Texture2D newTexture, Rectangle newRectangle)
        {
            enemyTexture = newTexture;
            rectangle = newRectangle;
            
        }
        public void Update()
        {
            
        }
        public void Draw(SpriteBatch spriteBatch, Vector2 scroll)
        {
            Rectangle scrolledRectangle = rectangle;
            scrolledRectangle.Y += (int)scroll.Y;
            spriteBatch.Draw(enemyTexture, scrolledRectangle, Color.White);

        }
    }
}
