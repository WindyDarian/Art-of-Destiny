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
using AODGameLibrary.Texts;

namespace AODGameLibrary.Interface
{
    /// <summary>
    /// 对话框（不加入UI集合），由大地无敌-范若余于2010年5月9日创建
    /// </summary>
    public class GameMessageBox:UI 
    {
        Texture2D blank;
        public GameWorld gameWorld;
        SpriteBatch spriteBatch;
        public Vector2 Size;
        public Color Color2 = Color.White;
        public Color Color = new Color(0, 0, 255, 210);
        public bool Visible = false;
        public List<AODText> Text = new List<AODText>(50);
        private int countNumber = 0;
        Texture2D gbm;
     

        /// <summary>
        /// 剩余计数条数
        /// </summary>
        public int CountNumber
        {
            get { return countNumber; }
            set
            {
                if (value >= 0)
                {
                    countNumber = value;
                }
                else countNumber = 0;
            }
        }

        float r = 0;
        float time = 0.4f;

        bool prot = false;
       
        public GameMessageBox(GameWorld gameWorld)
        {
            this.gameWorld = gameWorld;
            blank = gameWorld.game.Content.Load<Texture2D>(@"blank");
            gbm = gameWorld.game.Content.Load<Texture2D>(@"Interface\Textbox_NoTalker");
            position = new Vector2(gameWorld.game.GraphicsDevice.Viewport.Width/2 - gbm.Width/2, gameWorld.game.GraphicsDevice.Viewport.Height - 80 - gbm.Height);
            Size = new Vector2(gbm.Width, gbm.Height);
           
            spriteBatch = gameWorld.spriteBatch;
        }
        public void AddText(AODText t)
        {
            Text.Add(t);
        }
        public override void Update(GameTime gameTime)
        {
            if (Visible)
            {
                if (Text.Count == 0)
                {
                    Visible = false;
                    prot = false;
                    r=0;

                }
                else if ((InputState.IsKeyPressed(Keys.Enter) ||
                    InputState.IsKeyPressed(Keys.Space) ||
                    InputState.IsMouseButtonPressed(MouseButton.LeftButton) ||
                    InputState.IsPadButtonPressed(Buttons.A) ||
                    InputState.IsPadButtonPressed(Buttons.B))
                    && prot && r / time >= 0.5f)
                {

                    Text.RemoveAt(0);
                    prot = false;
                    r = 0;
                    CountNumber -= 1;
                }
                else
                {
                    prot = true;
                    r = MathHelper.Clamp(r + (float)gameTime.ElapsedGameTime.TotalSeconds, 0, time);
                }
               
            }
            

        }
        public override void Draw(GameTime gameTime)
        {

            if (Visible)
            {
                Rectangle t = GetRectangle();
                t.Width = (int)(t.Width * r / time);
                t.Height = (int)(t.Height * r / time);
                spriteBatch.Begin();
                spriteBatch.Draw(gbm, t, Color);
                spriteBatch.End();
                if (r/ time >=0.9f)
                {
                    if (Text.Count >= 1)
                    {
                        // spriteBatch.DrawString(gameWorld.GameFont, Text[0].Text , position + new Vector2(20, 20), c);
                        Text[0].position = position + new Vector2(50, 150);
                        Text[0].Draw(gameTime);
                    }
                }
         
                
            }
        }
        /// <summary>
        /// 开始计数~~
        /// </summary>
        public void StartToCount(int n)
        {
            CountNumber = n;
        }
        Rectangle GetRectangle()
        {
            return new Rectangle((int)position.X, (int)position.Y, (int)Size.X, (int)Size.Y);
        }
    }
}
