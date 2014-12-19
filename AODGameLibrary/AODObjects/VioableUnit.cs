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
using AODGameLibrary.Weapons;
using AODGameLibrary;
using AODGameLibrary.Cameras;
using AODGameLibrary.Models;
using AODGameLibrary.AIs;
using AODGameLibrary.Gamehelpers;
using AODGameLibrary.GamePlay;
using AODGameLibrary.Units;

namespace AODGameLibrary.AODObjects
{
    /// <summary>
    /// 可破坏单位,由大地无敌-范若余在2010年1月4日添加,以在Unit和ObjectUnit之间多加一个继承层级供环境物体继承
    /// </summary>
    public class VioableUnit:ObjectUnit
    {
        /// <summary>
        /// 单位所属的阵营，0~8,环境物为-1
        /// </summary>
        public int Group;
        /// <summary>
        /// 是否在短时间内发生过碰撞
        /// </summary>
        public bool collided = false;
        float maxShield;
        /// <summary>
        /// 能量护盾最大值
        /// </summary>
        public float MaxShield
        {
            get
            {
                return maxShield;
            }
            set
            {
                maxShield = MathHelper.Clamp(value, 0, float.MaxValue);
            }
        }
        float maxArmor;
        /// <summary>
        /// 护甲最大值
        /// </summary>
        public float MaxArmor
        {
            get
            {
                return maxArmor;
            }
            set
            {
                maxArmor = MathHelper.Clamp(value, 1, float.MaxValue);
            }
        }

        float shield;
        /// <summary>
        /// 护盾值
        /// </summary>
        public float Shield
        {
            get
            {
                return shield;
            }
            set
            {
                shield = MathHelper.Clamp(value, 0, MaxShield);
            }
        }
        float armor;
        /// <summary>
        /// 护甲值
        /// </summary>
        public float Armor
        {
            get
            {
                return armor;
            }
            set
            {
                armor = MathHelper.Clamp(value, 0, MaxArmor);
            }
        }

        private float shieldRestoreTime;
        /// <summary>
        /// 未受伤害时开始恢复护盾所需要的时间
        /// </summary>
        public float ShieldRestoreTime
        {
            get { return shieldRestoreTime; }
            set { shieldRestoreTime = value; }
        }

        private float shieldRestoreRate;
        /// <summary>
        /// 护盾恢复状态下每秒回复的护盾值
        /// </summary>
        public float ShieldRestoreRate
        {
            get { return shieldRestoreRate; }
            set { shieldRestoreRate = value; }
        }
        private float shieldRestoreTimeleft;
        /// <summary>
        /// 护盾恢复剩余时间
        /// </summary>
        public float ShieldRestoreTimeleft
        {
            get { return shieldRestoreTimeleft; }
            set { shieldRestoreTimeleft = value; }
        }
        private float armorRestoreRate;
        /// <summary>
        /// 每秒回复的护甲值(护甲一直回复但速度慢)
        /// </summary>
        public float ArmorRestoreRate
        {
            get { return armorRestoreRate; }
            set { armorRestoreRate = value; }
        }
        bool isInvincible = false;
        /// <summary>
        /// 单位是否无敌（不会受到伤害）
        /// </summary>
        public bool IsInvincible
        {
            get
            {
                return isInvincible;
            }
            set
            {
                isInvincible = value;
            }
        }
        private bool wontDie = false;
        /// <summary>
        /// 是否护甲为0也不会死亡
        /// </summary>
        public  bool WontDie
        {
            get
            {
                return wontDie;
            }
            set{wontDie = value;}
        }
        public VioableUnit():base()
        {

        }
        /// <summary>
        /// 使单位受到一个伤害
        /// </summary>
        /// <param name="damage">伤害的实体</param>
        public virtual void GetDamage(Damage damage)
        {

            float armor = 0.0f;
            float shield = 0.0f;
            if (UnitState == UnitState.alive && IsInvincible == false)
            {
                damage.OutDamage(this, ref shield, ref armor);
                Shield -= shield;
                Armor -= armor;
                if (shield > 0 || armor > 0)
                {
                    ResetShieldRestore();
                    GetDamage(shield, armor, damage.attacker);
                }


            }


        }
        public virtual void GetDamage(float shield, float armor,Unit attacker)
        {

        }
        /// <summary>
        /// 将各字段的值限制在一定范围内并判断单位是否已死亡
        /// </summary>
        public virtual void Check()
        {
            if (UnitState == UnitState.alive)
            {
                if (Armor <= 0.0f )
                {
                    if (wontDie == false)
                    {

                        BeginToDie();
                    }
                    else
                    {
                        armor = 1;
                    }
                }
                
            }
            //Scale = MathHelper.Clamp(Scale, 0.001f, 1000.0f);
            //forwardThrustForce = MathHelper.Clamp(forwardThrustForce, 0.001f, float.MaxValue);
            //FrictionForce = MathHelper.Clamp(FrictionForce, 0.0f, float.MaxValue);

            //angularRate = MathHelper.Clamp(angularRate, 0.01f, float.MaxValue);
     

        }
        public void ResetShieldRestore()
        {
            ShieldRestoreTimeleft = ShieldRestoreTime;
        }
        public override void Update(GameTime gameTime)
        {
            float elapsedTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            #region 计算护盾护甲回复
            if (Dead == false)
            {
                Check();
                if (ShieldRestoreRate != 0)
                {
                    ShieldRestoreTimeleft = MathHelper.Clamp(ShieldRestoreTimeleft - elapsedTime, 0, ShieldRestoreTime);
                    if (ShieldRestoreTimeleft == 0)
                    {
                        Shield += ShieldRestoreRate * elapsedTime;
                    }

                }
                if (ArmorRestoreRate != 0 && Armor > 0)
                {
                    Armor += ArmorRestoreRate * elapsedTime;
                }
            }


            #endregion
            base.Update(gameTime);
        }
        public override void SUpdate()
        {
            collided = false;
            base.SUpdate();
        }
    }
}
