using System;
using System.Collections.Generic;

using System.Text;
using AODGameLibrary.Units;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;
using AODGameLibrary.Cameras;
using AODGameLibrary.Weapons;
using AODGameLibrary.Gamehelpers;
using AODGameLibrary.Effects;
using AODGameLibrary.Effects.ParticleShapes;
using Microsoft.Xna.Framework.Graphics;
using AODGameLibrary.Interface;
using AODGameLibrary.AODObjects;
using AODGameLibrary.GamePlay;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
using AODGameLibrary.Texts;

namespace AODGameLibrary
{
    /// <summary>
    /// 屏幕效果管理器，由大地无敌-范若余在2009年11月12日创建
    /// </summary>
    public class ScreenEffectManager
    {
        Texture2D blank;
        GameWorld gameWorld;
        GraphicsDevice device;
        float blinkDuraion;
        float blinkDurationLeft;
        Color blinkColor = Color.White;
        Color keepColor = Color.Black;
        float keepDuration;
        float keepDurationLeft;
        public bool Blinking
        {
            get
            {
                return blinkDuraion > 0 && blinkDurationLeft > 0;
            }
        }
        public ScreenEffectManager(GameWorld gameWorld)
        {
            blank = gameWorld.Content.Load<Texture2D>(@"blank");
            this.gameWorld = gameWorld;
            device = gameWorld.game.GraphicsDevice;
        }
        public void Draw(GameTime gameTime)
        {
            float elapsedTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            blinkDurationLeft = MathHelper.Clamp(blinkDurationLeft - elapsedTime, 0, blinkDuraion);
            if (keepDuration != -1)
            {

                keepDurationLeft = MathHelper.Clamp(keepDurationLeft - elapsedTime, 0, keepDuration);
            }
            Color c = blinkColor;
            SpriteBatch sb = gameWorld.spriteBatch;
            if ((keepDurationLeft > 0 && keepDuration > 0) || keepDuration==-1)
            {
                 sb.Begin();
                 sb.Draw(blank, GetFullScreenRectangle(), keepColor);
                 sb.End();
            }
            if (blinkDuraion> 0&& blinkDurationLeft> 0)
            {
                c.A = (byte)(blinkDurationLeft / blinkDuraion * c.A);
                sb.Begin(SpriteSortMode.BackToFront,BlendState.NonPremultiplied);
                sb.Draw(blank, GetFullScreenRectangle(), c);
                sb.End();
         
            }

        }

        Rectangle GetFullScreenRectangle()
        {
            return new Rectangle(0, 0, device.Viewport.Width, device.Viewport.Height);

        }
        /// <summary>
        /// 闪烁
        /// </summary>
        public void Blink(Color color, float duration)
        {
            blinkColor = color;
            blinkDurationLeft = duration;
            blinkDuraion = duration;
        }
        /// <summary>
        /// 闪烁
        /// </summary>
        public void Blink()
        {
            Blink(Color.White, 0.6f);
        }
        public void KeepColor(Color color, float? duration)
        {
            keepColor = color;
            if (duration.HasValue)
            {
                keepDuration = duration.Value;
                keepDurationLeft = duration.Value;
            }
            else keepDuration = -1;

          
        }

    }
}
