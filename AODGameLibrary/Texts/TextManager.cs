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
using AODGameLibrary.GamePlay;


namespace AODGameLibrary.Texts
{
    /// <summary>
    /// 管理浮动文字，由大地无敌-范若余于2009年11月创建。
    /// </summary>
    public class TextManager
    {
        private List<AODText> items = new List<AODText> (20);

        public List<AODText> Items
        {
            get { return items; }
            set { items = value; }
        }
        private List<AODText> removingItems = new List<AODText>(10);
        public void Update(GameTime gameTime)
        {
            
            foreach (AODText text in items)
            {
                text.Update(gameTime);
                if (text.IsDead)
                {
                    removingItems.Add(text);
                }
            }
            foreach (AODText text in removingItems)
            {
                items.Remove(text);
            }
            removingItems.Clear();
        }
        public void Draw(GameTime gameTime)
        {
            foreach (AODText text in items)
            {
                text.Draw(gameTime);
            }
        }
        public void AddText(AODText text)
        {
            items.Add(text);
        }
    }
}
