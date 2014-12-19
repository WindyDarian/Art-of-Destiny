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
    /// 准心,由大地无敌-范若余在2009年10月9日建立
    /// </summary>
    public class AimPoint:UI
    {
        public GameWorld gameWorld;
        public Texture2D texture;
        public float scale = 0.5f;
        public byte maxAlpha = 200;
        public float showSpeed = 1;
        public float alphaValue = 0;
        public bool showing;
        public AimPoint(GameWorld gameWorld)
        {
            this.gameWorld = gameWorld;
            texture = gameWorld.Content.Load<Texture2D>(@"Textures\aodaim");
        }
        public override void Update(GameTime gameTime)
        {
           
        }
        public override void Draw(GameTime gameTime)
        {
            float elapsedTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            Unit u = gameWorld.Variables.Player;
            if (showing)
            {
                alphaValue = MathHelper.Clamp(alphaValue + showSpeed * elapsedTime, 0, 1);
            }
            else
            {
                alphaValue = MathHelper.Clamp(alphaValue - showSpeed * elapsedTime, 0, 1);
            }
            if (u != null)
            {
                if (u.UnitState != UnitState.dead)
                {
                    if (u.enemyUnintsInFront > 0)
                    {
                        ShowAimPoint();
                    }
                    else
                    {
                        HideAimPoint();
                    }

                    Vector3 p = u.Position + u.Face * 2000;
                    p = gameWorld.ViewProTransform(p);
                    position.X = p.X;
                    position.Y = p.Y;
                    SpriteBatch spriteBatch = gameWorld.spriteBatch;
                    Color c = Color.CornflowerBlue;
                    c.A = (byte)(maxAlpha * alphaValue);
                    spriteBatch.Begin();


                    spriteBatch.Draw(texture, position, null, c, 0, new Vector2(texture.Width / 2, texture.Height / 2), scale, SpriteEffects.None, 0.4f);
                    if (u.facingUnit != null)
                    {

                        Color x;
                        string s = "";
                        string s2 = "";
                        if (u.facingUnit.GetType() == typeof(LootItem))
                        {
                            x = Color.White;
                            s = ((LootItem)u.facingUnit).LootSettings.Name;
                        }
                        else if (((Unit)u.facingUnit).Group != u.Group)
                        {
                            x = Color.Red;
                            s = ((Unit)u.facingUnit).Name;
                            s2 = ((Unit)u.facingUnit).RiderName;
                        }
                        else
                        {

                            x = Color.CornflowerBlue;

                            s = ((Unit)u.facingUnit).Name;
                            s2 = ((Unit)u.facingUnit).RiderName;
                        }
                        x.A = 200;

                        Vector2 o = gameWorld.gameFont.MeasureString(s) / 2;
                        Vector2 o2 = gameWorld.gameFont.MeasureString(s2) / 2;
                    
                        spriteBatch.DrawString(gameWorld.GameFont, s, position + new Vector2(0, texture.Height * scale / 2), x, 0, o, 0.7f, SpriteEffects.None, 0.35f);
                        spriteBatch.DrawString(gameWorld.GameFont, s2, position + new Vector2(0, texture.Height * scale / 2 + 18), x, 0, o2, 0.6f, SpriteEffects.None, 0.35f);
                
                    
                    }


                    spriteBatch.End();

                }
                else HideAimPoint();
            }
            else HideAimPoint();
        }

        void HideAimPoint()
        {

            this.showing = false;
        }
        void ShowAimPoint()
        {
            this.showing = true;
        }
    }
}
