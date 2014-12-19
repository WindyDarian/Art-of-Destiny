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
    /// 单位信息显示，由大地无敌-范若余于2010年2月26日扩建
    /// </summary>
    public class UnitInf:UI 
    {
        public Texture2D background;
        public Texture2D front;
        public Texture2D shield;
        public Texture2D armor;
        public Unit linkedUnit;
        public GameWorld gameWorld;
        SpriteBatch spriteBatch;
        public Unit Target;
        public bool Filp = true;
        public string name;
        public UnitInf(GameWorld gameWorld)
        {
            this.gameWorld = gameWorld;
            background = gameWorld.game.Content.Load<Texture2D>(@"Interface\TargetInf_Background");
            front = gameWorld.game.Content.Load<Texture2D>(@"Interface\TargetInf_Front");
            shield = gameWorld.game.Content.Load<Texture2D>(@"Interface\TargetInf_Shield");
            armor = gameWorld.game.Content.Load<Texture2D>(@"Interface\TargetInf_Armor");
            position = new Vector2(5, 210);
        }
        public override void Update(GameTime gameTime)
        {
            
        }
        public override void Draw(GameTime gameTime)
        {
            if (Target != null)
            {

                linkedUnit = Target;
                spriteBatch = gameWorld.spriteBatch;
                float s = 0.0f;
                float a = 0.0f;
                if (linkedUnit != null)
                {
                    s = MathHelper.Clamp(linkedUnit.Shield / linkedUnit.MaxShield, 0, 1);
                    a = MathHelper.Clamp(linkedUnit.Armor / linkedUnit.MaxArmor, 0, 1);
                }
                spriteBatch.Begin();
                if (Filp)
                {
                    spriteBatch.Draw(background, new Rectangle((int)base.position.X, (int)base.position.Y, (int)background.Width, (int)background.Height)
                                            , null, Color.White, 0, Vector2.Zero, SpriteEffects.FlipHorizontally, 0.5f);
                    spriteBatch.Draw(shield, new Rectangle((int)base.position.X, (int)base.position.Y, (int)(shield.Width * s), (int)shield.Height)
                                            , new Rectangle((int)(shield.Width * (1 - s)), 0, (int)(shield.Width * s), (int)shield.Height), Color.White, 0, Vector2.Zero, SpriteEffects.FlipHorizontally, 0.3f);
                    spriteBatch.Draw(armor, new Rectangle((int)base.position.X, (int)base.position.Y, (int)(armor.Width * a), (int)armor.Height)
                                , new Rectangle((int)(shield.Width * (1 - a)), 0, (int)(armor.Width * a), (int)armor.Height), Color.White, 0, Vector2.Zero, SpriteEffects.FlipHorizontally, 0.3f);
                    spriteBatch.Draw(front, new Rectangle((int)base.position.X, (int)base.position.Y, (int)front.Width, (int)front.Height)
                            , null, Color.White, 0, Vector2.Zero, SpriteEffects.FlipHorizontally, 0.2f);
                }
                else
                {
                    spriteBatch.Draw(background, new Rectangle((int)base.position.X, (int)base.position.Y, (int)background.Width, (int)background.Height)
                                             , null, Color.White, 0, Vector2.Zero, SpriteEffects.None, 0.5f);
                    spriteBatch.Draw(shield, new Rectangle((int)base.position.X + (int)(shield.Width * (1 - s)), (int)base.position.Y, (int)(shield.Width * s), (int)shield.Height)
                                            , new Rectangle((int)(shield.Width * (1 - s)), 0, (int)(shield.Width * s), (int)shield.Height), Color.White, 0, Vector2.Zero, SpriteEffects.None, 0.3f);
                    spriteBatch.Draw(armor, new Rectangle((int)base.position.X + (int)(shield.Width * (1 - a)), (int)base.position.Y, (int)(armor.Width * a), (int)armor.Height)
                                , new Rectangle((int)(shield.Width * (1 - a)), 0, (int)(armor.Width * a), (int)armor.Height), Color.White, 0, Vector2.Zero, SpriteEffects.None, 0.3f);
                    spriteBatch.Draw(front, new Rectangle((int)base.position.X, (int)base.position.Y, (int)front.Width, (int)front.Height)
                                            , null, Color.White, 0, Vector2.Zero, SpriteEffects.None, 0.2f);
                }
                Color c;
                
                if (linkedUnit.Group == gameWorld.CurrentStage.Player.Group)
                {
                    c = Color.CornflowerBlue;
                }
                else c = Color.Orange;
                if (name != null)
                {
                    spriteBatch.DrawString(gameWorld.GameFont, name, position + new Vector2(75, -20), c);
                }
                else if (linkedUnit.RiderName == "")
                {
                    spriteBatch.DrawString(gameWorld.GameFont, linkedUnit.Name, position + new Vector2(75, -20), c);
                }
                else spriteBatch.DrawString(gameWorld.GameFont, linkedUnit.RiderName, position + new Vector2(75, -20), c);
                spriteBatch.End();

            }

        }
    }
}
