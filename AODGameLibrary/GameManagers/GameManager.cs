using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Net;
using Microsoft.Xna.Framework.Storage;

namespace AODGameLibrary.GameManagers
{
    /// <summary>
    /// 物件集合管理器
    /// </summary>
    public class GameManager
    {
        private GameWorld gameWorld;

        public GameWorld GameWorld
        {
            get { return gameWorld; }
            set { gameWorld = value; }
        }
        public GameManager(GameWorld gw)
        {
            this.gameWorld = gw;
        }
        public virtual void Update(GameTime gameTime)
        {

        }
        public virtual void SUpdate()
        {

        }
        public virtual void DrawGameScene(GameTime gameTime,Cameras.Camera camera)
        {

        }
        public virtual void DrawUI(GameTime gameTime)
        {

        }
       
    }
}
