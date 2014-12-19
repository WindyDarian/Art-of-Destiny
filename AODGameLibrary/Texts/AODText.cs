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
    /// 表示一个文本
    /// </summary>
    public class AODText
    {
        public Vector2 position = Vector2.Zero;
        public Color color = Color.White;
        public SpriteBatch spriteBatch;
        public string Text = "";
        public GameWorld gameWorld;
        public SpriteFont font;
        public float rotation = 0.0f;
        public float scale = 1.0f;
        public Vector2 origin = Vector2.Zero;
        public SpriteEffects effect = SpriteEffects.None;
        public float layerDepth = 0.3f;
        public float lifeTime = 2.0f;
        private float passedTime = 0.0f;
        private Vector2 velocity = Vector2.Zero;
        private FadeOutState fadeOutState = FadeOutState.Normal;
        /// <summary>
        /// 居中
        /// </summary>
        public bool centerize = true;
        public Game game;
        /// <summary>
        /// 是否淡出淡出方式i
        /// </summary>
        public FadeOutState FadeOutState
        {
            get
            {
                return fadeOutState;
            }
            set
            {
                fadeOutState = value;
            }
        }

        private bool isDead = false;
        /// <summary>
        /// 是否已死亡
        /// </summary>
        public bool IsDead
        {
            get
            {
                return isDead;
            }
            set
            {
                isDead = value;
            }
        }
        
        private AODText(GameWorld gameWorld)
        {
            this.gameWorld = gameWorld;

            this.game = gameWorld.game;
            font = gameWorld.GameFont;
            position = new Vector2(gameWorld.game.GraphicsDevice.Viewport.Width /2,gameWorld.game.GraphicsDevice.Viewport.Height-100);
           
        }
        public  AODText(GameWorld gameWorld,string text, float lifeTime, Color color, Vector2 position,  FadeOutState fadeOutState,Vector2 velocity,bool centerize)
        {
            this.gameWorld = gameWorld;
            this.game = gameWorld.game;
            font = gameWorld.GameFont;
            this.Text = text;
            this.color = color;
            this.position = position;
            this.lifeTime = lifeTime;
            this.FadeOutState = fadeOutState;
            this.velocity = velocity;
            this.centerize = centerize;
        }
        public AODText(Game game, string text, float lifeTime, Color color, Vector2 position, FadeOutState fadeOutState, Vector2 velocity,bool centerize)
        {
            font = game.Content.Load<SpriteFont>(@"msyh");

            this.game = game;
            this.Text = text;
            this.color = color;
            this.position = position;
            this.lifeTime = lifeTime;
            this.FadeOutState = fadeOutState;
            this.velocity = velocity;
            this.centerize = centerize;
        }
        public AODText(GameWorld gameWorld, string text, float lifeTime, Color color, Vector2 position, FadeOutState fadeOutState, Vector2 velocity, bool centerize,float scale)
            : this(gameWorld, text, lifeTime, color, position, fadeOutState, velocity, centerize)
        {
            this.scale = scale;
        }
         
        public virtual void Update(GameTime gameTime)
        {
            float elapsedTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (lifeTime > 0)
            {

                passedTime += elapsedTime;
                passedTime = MathHelper.Clamp(passedTime, 0, lifeTime);
                if (passedTime >= lifeTime)
                {
                    IsDead = true;
                }
            }
            position += velocity * elapsedTime;
            
        }
        public virtual void Draw(GameTime gameTime)
        {
            if (gameWorld != null)
            {
                spriteBatch = gameWorld.spriteBatch;
            }
            else spriteBatch = (SpriteBatch)game.Services.GetService(typeof(SpriteBatch));
            Color tColor = color;
            if (lifeTime > 0)
            {


                switch (fadeOutState)
                {
                    case FadeOutState.OneFourth:
                        {
                            if (passedTime > lifeTime * 0.75f)
                            {
                                tColor.A = (byte)((lifeTime - passedTime) / (lifeTime * 0.25f) * (float)tColor.A);
                            }
                            else if (passedTime < lifeTime * 0.25f)
                            {
                                tColor.A = (byte)(passedTime / (lifeTime * 0.25f) * (float)tColor.A);
                            }

                        }
                        break;
                    case FadeOutState.OneSecShowAndFade:
                        {
                            if (lifeTime - passedTime < 1)
                            {
                                tColor.A = (byte)((lifeTime - passedTime) / 1 * (float)tColor.A);
                            }
                            else if (passedTime < 1)
                            {
                                tColor.A = (byte)(passedTime / 1 * (float)tColor.A);
                            }
                        }
                        break;
                    case FadeOutState.HalfFade:
                        {
                            float x = lifeTime / 2;
                            if (passedTime >= lifeTime / 2)
                                tColor.A = (byte)(MathHelper.Clamp((x - (passedTime - lifeTime / 2)) / x, 0, 1) * (float)tColor.A);
                        }
                        break;
                    case FadeOutState.Normal:
                        {
                            if (lifeTime - passedTime < 1)
                            {
                                tColor.A = (byte)((lifeTime - passedTime) / 1 * (float)tColor.A);
                            }
                        }
                        break;
                    case FadeOutState.FadeOut:
                        {
                            tColor.A = (byte)(MathHelper.Clamp((lifeTime - passedTime) / lifeTime, 0, 1) * (float)tColor.A);
                        }
                        break;
                    case FadeOutState.None:
                        break;
                    default:
                        break;
                }
            }
            if (centerize)
            {

                origin = font.MeasureString(Text) / 2;
            }
            else
            {
                origin = new Vector2(0, font.MeasureString(Text).Y / 2);
            }
            spriteBatch.Begin(0,BlendState.NonPremultiplied);
            Vector2 pos = new Vector2((int)position.X, (int)position.Y);
            spriteBatch.DrawString(font, Text, pos, tColor, rotation, origin, scale, effect, layerDepth);
            spriteBatch.End();
            

        }
        /// <summary>
        /// 创建一行信息
        /// </summary>
        /// <param name="text">内容</param>
        /// <param name="lifeTime">存在时间</param>
        /// <param name="color">颜色</param>
        /// <param name="position">位置</param>
        public static AODText CreateText(string text, float lifeTime, Color color, Vector2 position,GameWorld gameWorld,FadeOutState fadeOutState)
        {
            AODText a = new AODText(gameWorld);
            a.Text = text;
            a.color = color;
            a.position = position;
            a.lifeTime = lifeTime;
            a.FadeOutState = fadeOutState;
            return a;
        }
        public static AODText CreateText(string text, float lifeTime, Color color, Vector2 position, GameWorld gameWorld, FadeOutState fadeOutState,Vector2 velocity)
        {
            AODText a = AODText.CreateText(text, lifeTime, color, position, gameWorld, fadeOutState);
            a.velocity = velocity;
            return a;
        }

    }
    public enum FadeOutState
    {
        /// <summary>
        /// 使用1/4出现然后使用1/4的时间消失
        /// </summary>
        OneFourth,
        /// <summary>
        /// 使用一秒钟的时间出现然后使用1秒钟的时间消失
        /// </summary>
        OneSecShowAndFade,
        /// 先持续一半的时间再消失
        /// </summary>
        HalfFade,
        /// <summary>
        /// 一般,在最后一秒时淡出
        /// </summary>
        Normal,
        /// <summary>
        /// 一直渐隐
        /// </summary>
        FadeOut,
        /// <summary>
        /// 无渐变时间到后突然消失
        /// </summary>
        None,
    }
}
