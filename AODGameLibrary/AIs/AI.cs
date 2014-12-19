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
using AODGameLibrary.Gamehelpers;
using AODGameLibrary.AODObjects;
using AODGameLibrary.GamePlay;


namespace AODGameLibrary.AIs
{
    /// <summary>
    /// 重新扩充的AI基类，由大地无敌-范若余于2010年2月20日制作
    /// </summary>
    public class AI
    {
        /// <summary>
        /// 这个AI所绑定的单位
        /// </summary>
        public Unit margedUnit;
        public GameWorld gameWorld;
        /// <summary>
        /// 得到或改变目标
        /// </summary>
        public Unit Target
        {
            get
            {
                if (margedUnit== null)
                {
                    return null;
                }
                else return margedUnit.Target;
            }
            set
            {

               
                margedUnit.Target = value;
                TargetChanged(this, EventArgs.Empty);
              
            }
        }

        public AI()
        {

        }
        public void Bound(Unit unit, GameWorld gameworld)
        {
            this.gameWorld = gameworld;
            margedUnit = unit;
        }
        public virtual void Update(GameTime gameTime)
        {

        }
        /// <summary>
        /// 受到伤害更新仇恨
        /// </summary>
        /// <param name="attacker">伤害的制造者</param>
        /// <param name="damagevalue">护盾和护甲伤害的总和除以护盾和护甲最大值</param>
        public virtual void GetDamage(Unit attacker, float threatvalue)
        {

        }
        public event EventHandler TargetChanged;
        public void ChangeTarget()
        {
            TargetChanged(this, EventArgs.Empty);
        }
    }
}
