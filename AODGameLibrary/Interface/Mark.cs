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
    /// 游戏中的一个标记,由大地无敌-范若余在2009年11月7日建立
    /// </summary>
    public class Mark:UI
    {
        MarksManager manager;
        GameWorld gameWorld;
        Unit targetUnit;
        /// <summary>
        /// 标记的单位
        /// </summary>
        public Unit TargetUnit
        {
            get { return targetUnit; }
            set { targetUnit = value; }
        }
        Vector3 targetPosition;
        /// <summary>
        /// 标记的位置
        /// </summary>
        public Vector3 TargetPosition
        {
            get { return targetPosition; }
            set { targetPosition = value; }
        }
        Color markColor = Color.Yellow;
        /// <summary>
        /// 标记颜色
        /// </summary>
        public Color MarkColor
        {
            get { return markColor; }
            set { markColor = value; }
        }
        private bool blink;
        /// <summary>
        /// 闪烁
        /// </summary>
        public bool Blink
        {
            get { return blink; }
            set { blink = value; }
        }
        bool backOnly = false;
        /// <summary>
        /// 是否只在后方时才可见
        /// </summary>
        public bool BackOnly
        {
            get { return backOnly; }
            set { backOnly = value; }
        }
        private Texture2D markTexture;
        /// <summary>
        /// 标记纹理
        /// </summary>
        public Texture2D MarkTexture
        {
            get { return markTexture; }
            set { markTexture = value; }
        }
        Vector2 rotateOrigin = new Vector2(16, -40);
        /// <summary>
        /// 每秒旋转的角度数
        /// </summary>
        float rotateSpeed = 45;
        /// <summary>
        /// 旋转角度数
        /// </summary>
        float rotation = 0;
        bool visable = true;
        /// <summary>
        /// 是否可见
        /// </summary>
        public bool Visable
        {
            get { return visable; }
            set { visable = value; }
        }
        bool enabled = true;
        /// <summary>
        /// 是否活动
        /// </summary>
        public bool Enabled
        {
            get { return enabled; }
            set { enabled = value; }
        }
        public Mark(GameWorld gameWorld,MarksManager manager)
        {
            this.gameWorld = gameWorld;
            this.manager = manager;
            markTexture = gameWorld.Content.Load<Texture2D>(GameConsts.MarksTexture);
            visable = true;
           
        }
        public Mark(GameWorld gameWorld)
        {

            this.gameWorld = gameWorld;
            markTexture = gameWorld.Content.Load<Texture2D>(GameConsts.MarksTexture);
      
        }
        public override void Update(GameTime gameTime)
        {

        }
        public override void Draw(GameTime gameTime)
        {
            Viewport v = gameWorld.game.GraphicsDevice.Viewport;
            Vector3 t;
            if (targetUnit != null)
            {
                t = targetUnit.Position;
       
            }
            else
            {
                t = targetPosition;
             }



            if (gameWorld.currentCamera.PositionInCamera(t))
            {
                if (backOnly == false)
                {

                    Vector3 p = v.Project(t, gameWorld.currentCamera.Projection, gameWorld.currentCamera.View, Matrix.Identity);
                    rotation = (rotation + (float)gameTime.ElapsedGameTime.TotalSeconds * rotateSpeed) % 360;
                    position.X = p.X;
                    position.Y = p.Y;
                    DrawFrontMark();
                }
            }
            else
            {

                float x = gameWorld.currentCamera.GetPlaneAngle(t);
                Vector2 ce;
                ce = new Vector2(v.Width / 2, v.Height / 2);

                // position = Vector2.Normalize(px) * MathHelper.Min(v.Height, v.Width) / 3 + ce;
                DrawBackMark(ce,x);


            }
            
        }

        /// <summary>
        /// 绘制在视野中的Mark
        /// </summary>
        void DrawFrontMark()
        {
            SpriteBatch sb = gameWorld.spriteBatch;
            sb.Begin();
            float r = MathHelper.ToRadians(rotation);
            float r2 = r+MathHelper.Pi*2/3;
            float r3 = r- MathHelper.Pi*2/3;
            sb.Draw(markTexture, position, null, markColor, r, rotateOrigin, 1,SpriteEffects.None, 0.4f);
            sb.Draw(markTexture, position, null, markColor, r2, rotateOrigin, 1, SpriteEffects.None, 0.4f);
            sb.Draw(markTexture, position, null, markColor, r3, rotateOrigin, 1, SpriteEffects.None, 0.4f);
            sb.End();
        }
        /// <summary>
        /// 绘制在当前视野外的Mark
        /// </summary>
        void DrawBackMark(Vector2 center, float angle)
        {
            Viewport v = gameWorld.game.GraphicsDevice.Viewport;
            SpriteBatch sb = gameWorld.spriteBatch;
            Vector2 x = Vector2.TransformNormal(Vector2.UnitY, Matrix.CreateRotationZ(-angle));
            x.Y *= -1;//2D坐标Y轴正方向向下


            position = x * MathHelper.Min(v.Height, v.Width) / 3 + center;
            position.X = (int)position.X;
            position.Y = (int)position.Y;



            sb.Begin();
            sb.Draw(markTexture, position, null, markColor, angle, new Vector2(markTexture.Width / 2, markTexture.Height / 2), 1, SpriteEffects.None, 0.4f);

            sb.End();


        }
        /// <summary>
        /// 结束
        /// </summary>
        public void End()
        {
            visable = false;
            enabled = false;
        }
    }
}
