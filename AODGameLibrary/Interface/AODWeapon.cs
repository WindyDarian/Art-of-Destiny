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
    public class AODWeaponUI : UI
    {
        public GameWorld gameWorld;

        public Texture2D background;
        public Vector2 weaponInfPosition = new Vector2(10, 10);
        public Vector2 missileInfPosition = new Vector2(45, 101);
        public Vector2 skillInfPosition = new Vector2(69, 141);
        string[] dpadShotcut = { "(↑)", "(→)", "(↓)", "(←)" };

        public AODWeaponUI(GameWorld gameWorld)
        {
            this.gameWorld = gameWorld;

            position = new Vector2(gameWorld.game.GraphicsDevice.Viewport.Width - 260, 0);
            background = gameWorld.Content.Load<Texture2D>(@"Interface\AODWeapon");
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
                    spriteBatch.Draw(background, position, Color.White);
                    if (!u.SkillControlUnit)
                    {
                        if (u.CurrentWeapon != null)
                        {
                            for (int i = 1; i <= u.maxWeaponNum; i++)
                            {
                                string k = i.ToString();
                                if (i <= 4) k = k + dpadShotcut[i - 1];

                                if (i == u.CurrentWeaponNumber + 1)
                                {
                                    spriteBatch.DrawString(gameWorld.GameFont, k, position + weaponInfPosition + new Vector2(40 * i, -10), Color.Yellow, 0, Vector2.Zero, 0.6f, SpriteEffects.None, 0.3f);
                                }
                                else if (i <= u.weapons.Count)
                                {
                                    spriteBatch.DrawString(gameWorld.GameFont, k, position + weaponInfPosition + new Vector2(40 * i, -10), Color.White, 0, Vector2.Zero, 0.6f, SpriteEffects.None, 0.3f);

                                }
                                else spriteBatch.DrawString(gameWorld.GameFont, k, position + weaponInfPosition + new Vector2(40 * i, -10), Color.Gray, 0, Vector2.Zero, 0.6f, SpriteEffects.None, 0.3f);

                            }
                            spriteBatch.DrawString(gameWorld.GameFont, u.CurrentWeapon.name, position + weaponInfPosition + new Vector2(10, 0), Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 0.3f);
                            spriteBatch.DrawString(gameWorld.GameFont, u.CurrentWeapon.ammoName + " ", position + weaponInfPosition + new Vector2(30, 26), Color.White, 0, Vector2.Zero, 0.6f, SpriteEffects.None, 0.3f);
                            spriteBatch.DrawString(gameWorld.GameFont, ((int)u.CurrentWeapon.AmmoNum).ToString(), position + weaponInfPosition + new Vector2(25, 40), Color.White, 0, Vector2.Zero, 1.7f, SpriteEffects.None, 0.3f);
                        }
                        if (u.CurrentMissileWeapon != null)
                        {
                            spriteBatch.DrawString(gameWorld.GameFont, "(F/RB)" + u.CurrentMissileWeapon.missileWeaponType.name, position + missileInfPosition, Color.White, 0, Vector2.Zero, 0.6f, SpriteEffects.None, 0.3f);
                            spriteBatch.DrawString(gameWorld.GameFont, u.CurrentMissileWeapon.Num.ToString(), position + missileInfPosition + new Vector2(115, 0), Color.White, 0, Vector2.Zero, 1.1f, SpriteEffects.None, 0.3f);

                        }
                        if (u.CurrentSkill != null)
                        {
                            spriteBatch.DrawString(gameWorld.GameFont, "(R/LB)" + u.CurrentSkill.SkillName, position + skillInfPosition, Color.White, 0, Vector2.Zero, 0.6f, SpriteEffects.None, 0.3f);
                            if (u.CurrentSkill.CooldownRemain > 0)
                            {
                                spriteBatch.DrawString(gameWorld.GameFont, Math.Round(u.CurrentSkill.CooldownRemain, 1).ToString() + " / " + Math.Round(u.CurrentSkill.Cooldown, 1).ToString(), position + skillInfPosition + new Vector2(85, 0), Color.White, 0, Vector2.Zero, 1.1f, SpriteEffects.None, 0.3f);

                            }
                        }
                    }
                    else
                    {
                        if (u.CurrentSkill != null)
                        {
                            for (int i = 1; i <= u.maxSkillNum; i++)
                            {
                                if (i == u.CurrentSkillNumber + 1)
                                {
                                    spriteBatch.DrawString(gameWorld.GameFont, i.ToString(), position + weaponInfPosition + new Vector2(10 * i, -10), Color.Yellow, 0, Vector2.Zero, 0.6f, SpriteEffects.None, 0.3f);
                                }
                                else if (i <= u.skills.Count)
                                {
                                    spriteBatch.DrawString(gameWorld.GameFont, i.ToString(), position + weaponInfPosition + new Vector2(10 * i, -10), Color.White, 0, Vector2.Zero, 0.6f, SpriteEffects.None, 0.3f);

                                }
                                else spriteBatch.DrawString(gameWorld.GameFont, i.ToString(), position + weaponInfPosition + new Vector2(10 * i, -10), Color.Gray, 0, Vector2.Zero, 0.6f, SpriteEffects.None, 0.3f);

                            }
                            spriteBatch.DrawString(gameWorld.GameFont, u.CurrentSkill.SkillName, position + weaponInfPosition + new Vector2(10, 0), Color.White, 0, Vector2.Zero, 1.4f, SpriteEffects.None, 0.3f);
                            if (u.CurrentSkill.CooldownRemain>0)
                            {
                                 spriteBatch.DrawString(gameWorld.GameFont, Math.Round(u.CurrentSkill.CooldownRemain, 1).ToString() + " / " + Math.Round(u.CurrentSkill.Cooldown, 1).ToString(), position + weaponInfPosition + new Vector2(25, 40), Color.White, 0, Vector2.Zero, 1.3f, SpriteEffects.None, 0.3f);

                           
                            }
                
                        }

                        if (u.CurrentMissileWeapon != null)
                        {
                            spriteBatch.DrawString(gameWorld.GameFont, "(F/RB)" + u.CurrentMissileWeapon.missileWeaponType.name, position + missileInfPosition, Color.White, 0, Vector2.Zero, 0.6f, SpriteEffects.None, 0.3f);
                            spriteBatch.DrawString(gameWorld.GameFont, u.CurrentMissileWeapon.Num.ToString(), position + missileInfPosition + new Vector2(85, 0), Color.White, 0, Vector2.Zero, 1.1f, SpriteEffects.None, 0.3f);

                        }
                    }
                   

                    spriteBatch.End();

                }
            }
            
        }
    }
}
