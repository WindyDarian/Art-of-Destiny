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
    public class AODBar:UI 
    {
        public Texture2D background;
        public Texture2D front;
        public Texture2D shield;
        public Texture2D armor;
        public Unit linkedUnit;
        public GameWorld gameWorld;
        SpriteBatch spriteBatch;
        public AODBar(GameWorld gameWorld)
        {
            this.gameWorld = gameWorld;
            background = gameWorld.game.Content.Load<Texture2D>(@"Interface\AODBar_Background");
            front = gameWorld.game.Content.Load<Texture2D>(@"Interface\AODBar_Front");
            shield = gameWorld.game.Content.Load<Texture2D>(@"Interface\AODBar_Shield");
            armor = gameWorld.game.Content.Load<Texture2D>(@"Interface\AODBar_Armor");
        }
        public override void Update(GameTime gameTime)
        {
            
        }
        public override void Draw(GameTime gameTime)
        {
            linkedUnit = gameWorld.Variables.Player;
            spriteBatch = gameWorld.spriteBatch;
            float s = 0.0f;
            float a = 0.0f;
            if (linkedUnit != null )
            {
                s = MathHelper.Clamp(linkedUnit.Shield / linkedUnit.MaxShield, 0, 1);
                a = MathHelper.Clamp(linkedUnit.Armor / linkedUnit.MaxArmor, 0, 1);
            }
            spriteBatch.Begin();
            spriteBatch.Draw(background, position,Color.White);
            spriteBatch.Draw(shield, new Rectangle((int)base.position.X, (int)base.position.Y, (int)(shield.Width * s), (int)shield.Height)
                                    , new Rectangle(0, 0, (int)(shield.Width * s), (int)shield.Height), Color.White);
            spriteBatch.Draw(armor, new Rectangle((int)base.position.X, (int)base.position.Y, (int)(armor.Width * a), (int)armor.Height)
                        , new Rectangle(0, 0, (int)(armor.Width * a), (int)armor.Height), Color.White);
            spriteBatch.Draw(front, position,Color.White);
            spriteBatch.DrawString(gameWorld.GameFont, linkedUnit.Name + " " + linkedUnit.RiderName, position + new Vector2(112, 88), Color.CornflowerBlue);
            spriteBatch.End();
        }
    }
}
