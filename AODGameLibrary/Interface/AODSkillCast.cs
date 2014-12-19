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
using AODGameLibrary.AODObjects;
namespace AODGameLibrary.Interface
{
    /// <summary>
    /// 技能提示条,由大地无敌-范若余在2009年8月25日建立
    /// </summary>
    public class AODSkillCast : UI
    {
        public GameWorld gameWorld;

        public AODSkillCast(GameWorld gameWorld)
        {
            this.gameWorld = gameWorld;
            position = new Vector2(gameWorld.game.GraphicsDevice.Viewport.Width / 2, gameWorld.game.GraphicsDevice.Viewport.Height - 200);

        }
        public override void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            
        }
        public override void Draw(Microsoft.Xna.Framework.GameTime gameTime)
        {
            Unit u = gameWorld.Variables.Player;
            if (u != null)
            {
                if (u.UnitState != UnitState.dead)
                {
                    SpriteBatch spriteBatch = gameWorld.spriteBatch;
                    spriteBatch.Begin();

                    if (u.UsingSkill != null)
                    {
                        string a = "";
                        if (u.UsingSkill.IsCasting)
                        {
                            a = Math.Round(u.UsingSkill.CastedTime, 1).ToString() + " / " + Math.Round(u.UsingSkill.CastTime, 1).ToString();
                        }
                        else if (u.UsingSkill.IsChannelling)
                        {
                            a = Math.Round(u.UsingSkill.ChannelTime - u.UsingSkill.ChannelledTime, 1).ToString() + " / " + Math.Round(u.UsingSkill.ChannelTime, 1).ToString();
                        }
                        string s =  u.UsingSkill.SkillName +  "  " + a;
                        Vector2 o = gameWorld.GameFont.MeasureString(s)/2;
                        spriteBatch.DrawString(gameWorld.GameFont, s, position, Color.White, 0, o, 1, SpriteEffects.None, 0.2f);
                    }

                    
                    spriteBatch.End();

                }
            }
            
        }
    }
}
