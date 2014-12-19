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
using AODGameLibrary.Units;
using AODGameLibrary.GamePlay;

namespace AODGameLibrary.Gamehelpers
{
    public class StateShower 
    {
        Game game;
        SpriteFont msyh;
        SpriteBatch spriteBatch;
        /// <summary>
        /// 控制单位状态是否显示
        /// </summary>
        bool IsStateShow;
        float t;
        /// <summary>
        /// 显示状态的位置
        /// </summary>
        public Vector2 position = Vector2.Zero;
        /// <summary>
        /// 显示状态的单位
        /// </summary>
        public Unit followUnit = null;
        /// <summary>
        /// 间距
        /// </summary>
        Vector2 space;
        public StateShower(Game game)
        {
            this.game = game;
            Initialize();
        }

        void Initialize()
        {
            msyh = game.Content.Load<SpriteFont>("msyh");
            IsStateShow = true;
            position = new Vector2(0, 100);
            space = new Vector2(0, 28);
        }

        public void Update(GameTime gameTime)
        {
            float elapsedTime = (float)gameTime.ElapsedGameTime.TotalSeconds ;

            KeyboardState keyboardState = Keyboard.GetState();
            t += elapsedTime;

            #region 设置是否显示
            if (keyboardState.IsKeyDown(Keys.F10))
            {
                if (t>0.2)
                {
                    IsStateShow = (IsStateShow != true);
                    t = 0;
                }


            }
            t = MathHelper.Clamp(t, 0.0f, 1.0f);
            #endregion

           


            
        }

        public void Draw(GameTime gameTime)
        {
            spriteBatch = (SpriteBatch)game.Services.GetService(typeof(SpriteBatch));
            if (IsStateShow && followUnit != null )
            {
                spriteBatch.Begin();


                spriteBatch.DrawString(msyh, "名字：" + followUnit.Name, position, Color.White);

                spriteBatch.DrawString(msyh, "速率：" + ((int)followUnit.Velocity.Length()).ToString(), position + space, Color.White);
                spriteBatch.DrawString(msyh,
                    string.Format("位置：({0}, {1}, {2})",
                        ((int)followUnit.Position.X),
                        ((int)followUnit.Position.Y),
                        ((int)followUnit.Position.Z)),
                    position + space * 2, Color.White);
                spriteBatch.DrawString(msyh, "护盾：" + ((int)followUnit.Shield).ToString(), position + space * 3, Color.White);
                spriteBatch.DrawString(msyh, "护甲：" + ((int)followUnit.Armor).ToString(), position + space * 4, Color.White);
                if (followUnit.CurrentWeapon != null)
                {
                    spriteBatch.DrawString(msyh,
                                           string.Format("弹药：{0} / {1}",
                                           ((int)followUnit.CurrentWeapon.AmmoNum),
                                           ((int)followUnit.CurrentWeapon.maxAmmo)),
                                           position + space * 5, Color.White);
                }

                if (followUnit.CurrentMissileWeapon != null)
                {
                    spriteBatch.DrawString(msyh,
                                           string.Format("导弹: {0} / {1}",
                                           ((int)followUnit.CurrentMissileWeapon.Num),
                                           ((int)followUnit.CurrentMissileWeapon.missileWeaponType.maxNum)),
                                           position + space * 6, Color.White);
                }
                if (followUnit.Target != null)
                {
                    spriteBatch.DrawString(msyh,"目标：" + followUnit.Target.Name,
                       position + space * 7, Color.White);
                    spriteBatch.DrawString(msyh, "距离: " + (int)Unit.Distance(followUnit, followUnit.Target),
                       position + space * 8, Color.White);
                }
                spriteBatch.End();
            }


        }
    }
}