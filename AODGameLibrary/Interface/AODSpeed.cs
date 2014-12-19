using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Net;
using Microsoft.Xna.Framework.Storage;
using AODGameLibrary.Gamehelpers;
using AODGameLibrary.Units;
using AODGameLibrary.Weapons;
using AODGameLibrary.Effects;
using AODGameLibrary;
using System.IO;
using AODGameLibrary.GamePlay;

namespace AODGameLibrary.Interface
{
    public class AODSpeed:UI 
    {
        public Texture2D background;
        public Texture2D front;
        public Unit linkedUnit;
        public GameWorld gameWorld;
        SpriteBatch spriteBatch;
        public AODSpeed(GameWorld gameWorld)
        {
            this.gameWorld = gameWorld;
            background = gameWorld.game.Content.Load<Texture2D>(@"Interface\AODSpeed_Background");
            front = gameWorld.game.Content.Load<Texture2D>(@"Interface\AODSpeed_Front");
            position = new Vector2(gameWorld.game.GraphicsDevice.Viewport.Width - 150, gameWorld.game.GraphicsDevice.Viewport.Height - 150);
        }
        public override void Update(GameTime gameTime)
        {
            
        }
        public override void Draw(GameTime gameTime)
        {
            linkedUnit = gameWorld.Variables.Player;

            if (linkedUnit==null)return;
            spriteBatch = gameWorld.spriteBatch;
          
            spriteBatch.Begin(0,BlendState.NonPremultiplied);
            spriteBatch.Draw(background, position,Color.White);
            if (linkedUnit.MaxSpeed > 0)
            {

                spriteBatch.Draw(front, position, new Color(255, 255, 255, (byte)(MathHelper.Clamp(linkedUnit.Speed / linkedUnit.MaxSpeed, 0, 1) * 255)));
            }
            spriteBatch.DrawString(gameWorld.GameFont, "Speed:", position + new Vector2(-20, 50), Color.White, 0, Vector2.Zero, 0.6f, SpriteEffects.None, 0.3f);
            string s = ((int)linkedUnit.Speed).ToString();
            Vector2 o = gameWorld.GameFont.MeasureString(s)/2;
            spriteBatch.DrawString(gameWorld.GameFont,s , position + new Vector2(64, 64), Color.White, 0, o, 1.2f, SpriteEffects.None, 0.3f);
            spriteBatch.DrawString(gameWorld.GameFont, "m/s", position + new Vector2(100, 65), Color.White, 0, Vector2.Zero, 0.6f, SpriteEffects.None, 0.3f);
            spriteBatch.DrawString(gameWorld.GameFont, "W(LS): Engine On/off\n S(B):Brake", position + new Vector2(-8, 90), Color.White, 0, Vector2.Zero, 0.6f, SpriteEffects.None, 0.3f);
            
            spriteBatch.End();
        }
    }
}
