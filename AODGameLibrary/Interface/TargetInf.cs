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
    /// <summary>
    /// 目标信息显示，由大地无敌-范若余于2009年10月5日建立
    /// </summary>
    public class TargetInf:UnitInf 
    {
        SpriteBatch sb;
        public TargetInf(GameWorld gameWorld):base(gameWorld)
        {
            position = new Vector2(gameWorld.game.GraphicsDevice.Viewport.Width - 260, 210);
            Filp = false;
        }
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }
        public override void Draw(GameTime gameTime)
        {
            sb = gameWorld.spriteBatch;
            if (gameWorld.Variables.Player != null)
            {

                Target = gameWorld.Variables.Player.Target;
                if (Target!= null)
                    if (gameWorld.PlayerLockedTarget == null)
                    {
                        string s3 = "Q(Y): Lock";
                        sb.Begin();
                        sb.DrawString(gameWorld.GameFont, s3, position + new Vector2(0, 100), Color.White, 0, Vector2.Zero, 1f, SpriteEffects.None, 0.35f);
                        sb.End();




                    }
                    else if (Target == gameWorld.PlayerLockedTarget)
                    {
                        string s3 = "Space(A): Auto-aim";
                        sb.Begin();
                        sb.DrawString(gameWorld.GameFont, s3, position + new Vector2(0, 100), Color.White, 0, Vector2.Zero, 1f, SpriteEffects.None, 0.35f);
                        sb.End();
                    }
            }
            else Target = null;
            base.Draw(gameTime);
        }
    }
}
