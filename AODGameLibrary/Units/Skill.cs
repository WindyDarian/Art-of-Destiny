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
using AODGameLibrary.Weapons;
using AODGameLibrary.AODObjects;
using AODGameLibrary.Gamehelpers;
using AODGameLibrary.Effects;

using AODGameLibrary.Effects.ParticleShapes;
using AODGameLibrary.CollisionChecking;

namespace AODGameLibrary.Units
{
    /// <summary>
    /// 表示一个技能（可由特殊装备触发），由大地无敌-范若余在2009年8月19日创建（这个类由于未使用继承所以非常不便于管理需改进）
    /// </summary>
    public abstract class Skill
    {
        private string skillName;
        /// <summary>
        /// 技能名
        /// </summary>
        public string SkillName
        {
            get
            {
                return skillName;
            }
            set
            {
                skillName = value;
            }
        }

        private string skillText;
        /// <summary>
        /// 技能说明
        /// </summary>
        public string SkillText
        {
            get
            {
                return skillText;
            }
            set
            {
                skillText = value;
            }
        }



        private float castTime;
        /// <summary>
        /// 施法时间,秒,若为0则瞬间发动
        /// </summary>
        public float CastTime
        {
            get
            {
                return castTime;
            }
            set
            {
                castTime = MathHelper.Clamp(value, 0, float.MaxValue);
            }
        }

        private float channelTime;
        /// <summary>
        /// 引导时间,秒
        /// </summary>
        public float ChannelTime
        {
            get
            {
                return channelTime;
            }
            set
            {
                channelTime = MathHelper.Clamp(value, 0, float.MaxValue);
            }

        }

        private float cooldown;
        /// <summary>
        /// 冷却时间
        /// </summary>
        public float Cooldown
        {
            get
            {
                return cooldown;
            }
            set
            {
                cooldown = MathHelper.Clamp(value, 0, float.MaxValue);
            }
        }

        private float range;
        /// <summary>
        /// 技能施放距离
        /// </summary>
        public float Range
        {
            get { return range; }
            set { range = value; }
        }

        private int skillPointNum;

        public int SkillPointNum
        {
            get { return skillPointNum; }
            set { skillPointNum = value; }
        }

        private Damage damage;
        /// <summary>
        /// 技能伤害
        /// </summary>
        public Damage Damage
        {
            get
            {
                return damage;
            }
            set
            {
                damage = value;
            }
        }

        private float channelCycle;
        /// <summary>
        /// 通道技能判断周期,只在引导技能上生效
        /// </summary>
        public float ChannelCycle
        {
            get
            {
                return channelCycle;
            }
            set
            {
                channelCycle = value;
            }
        }



        /// <summary>
        /// 自定义的值，不同种技能有不同的意义
        /// </summary>
        public List<float> FloatValues;
        ///// <summary>
        ///// 施法粒子效果AssetName
        ///// </summary>
        //public string CastEffectAssetName;
        ///// <summary>
        ///// 引导粒子效果AssetName
        ///// </summary>
        //public string ChannelEffectAssetName;
        ///// <summary>
        ///// 效果粒子效果AssetName
        ///// </summary>
        //public string DoEffectAssetName;
        /// <summary>
        /// 技能的AssetName（不含目录）
        /// </summary>
        public string AssetName;
        //private ParticleEffect CastEffect;
        //private ParticleEffect ChannelEffect;
        //private ParticleEffect DoEffect;

        private Unit margedUnit;
        /// <summary>
        /// 技能所属的单位
        /// </summary>
        [ContentSerializerIgnore]
        public Unit MargedUnit
        {
            get
            {
                return margedUnit;
            }
            set
            {
                margedUnit = value;
            }
        }

        private float cooldownRemain;
        /// <summary>
        /// 冷却时间剩余
        /// </summary>
        [ContentSerializerIgnore]
        public float CooldownRemain
        {
            get
            {
                return cooldownRemain;
            }
            set
            {
                cooldownRemain = MathHelper.Clamp(value, 0, float.MaxValue);
            }
        }

        private bool isCasting;
        /// <summary>
        /// 是否正在施放技能
        /// </summary>
        [ContentSerializerIgnore]
        public bool IsCasting
        {
            get
            {
                return isCasting;
            }
         
        }

        
        private float castedTime;
        /// <summary>
        /// 已施法时间
        /// </summary>
        [ContentSerializerIgnore]
        public float CastedTime
        {
            get
            {
                return castedTime;
            }
            set
            {
                castedTime = MathHelper.Clamp(value, 0, CastTime);
            }
        }

        private bool isChannelling;
        /// <summary>
        /// 是否正在引导技能
        /// </summary>
        [ContentSerializerIgnore]
        public bool IsChannelling
        {
            get
            {
                return isChannelling;
            }
        }

        private float channelledTime;
        /// <summary>
        /// 已引导时间
        /// </summary>
        [ContentSerializerIgnore]
        public float ChannelledTime
        {
            get
            {
                return channelledTime;
            }
            set
            {
                channelledTime = MathHelper.Clamp(value, 0, ChannelTime);
            }
        }
        

        /// <summary>
        /// 技能是否正在被施放或被引导
        /// </summary>
        [ContentSerializerIgnore]
        public bool IsActive
        {
            get
            {
                return isChannelling || IsCasting;
            }
        }

        /// <summary>
        /// 技能是否可用
        /// </summary>
        [ContentSerializerIgnore]
        public bool IsCooldowned
        {
            get
            {
                return cooldownRemain <= 0;
            }
        }
        /// <summary>
        /// 技能是否可用,包括冷却时间和其它条件
        /// </summary>
        [ContentSerializerIgnore]
        public bool IsSkillUsable
        {
            get
            {
                return IsCooldowned && SkillConditionCheck();
            }
        }
        /// <summary>

        /// <summary>
        /// 临时目标
        /// </summary>
        private Unit tempTar;
        /// <summary>
        /// 正在施放中的技能的目标
        /// </summary>
        private Unit target;
        [ContentSerializerIgnore]
        public Unit Target
        {
            get { return target; }
            set { target = value; }
        }
        private GameWorld gameWorld;

        [ContentSerializerIgnore]
        public GameWorld GameWorld
        {
            get { return gameWorld; }
            set { gameWorld = value; }
        }
        /// <summary>
        /// 是否将在该帧发射
        /// </summary>
        private bool tocast;
        private float lchanneledtime;
        
        private int channelDone = 0;

        /// <summary>
        /// 对指定目标施放一个技能
        /// </summary>
        /// <param name="target">目标</param>
        public void Cast(Unit target)
        {
            this.tempTar = target;
            this.Cast();
        }
        /// <summary>
        /// 施放一个技能
        /// </summary>
        public void Cast()
        {
            
            if (IsCooldowned && (IsActive == false))
            {
                tocast = true;
                

            }
            
        }
        
        /// <summary>
        /// 更新施法时间等
        /// </summary>
        /// <param name="gameTime">当前GameTime</param>
        public void Update(GameTime gameTime)
        {
            float elapsedTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            
            if (tocast)
            {
                if (IsCooldowned && (IsActive == false))
                {
                    target = tempTar;
                    if (SkillConditionCheck())
                    {
                        if (CastTime <= 0)
                        {

                            CastOut();
                        }
                        else
                        {
                            StartCasting();
                        }
                        
                    }



                }
                tocast = false;

                
            }
            if (isCasting)
            {
                CastedTime += elapsedTime;
                if (CastedTime>= CastTime)
                {
                    if (SkillConditionCheck())
                    {

                        CastOut();
                    }
                    else
                    {
                        Interrupt();
                    }

                }
            }
            if (isChannelling)
            {
                lchanneledtime = channelledTime;
                ChannelledTime += elapsedTime;
                if (lchanneledtime < channelCycle * (channelDone + 1) - 0.001 && channelledTime >= channelCycle * (channelDone + 1) - 0.001)//0.001用来解决一些float乘法的精度上的问题
                {
                    channelDone++;
                    if (SkillConditionCheck())
                    {

                        Do();
                    }
                    else
                    {
                        Interrupt();
                    }
                }

                if (ChannelledTime >= ChannelTime)
                {
                    Interrupt();
                }

            }
            CooldownRemain -= elapsedTime;
            tempTar = null;
 
            UpdateAction(gameTime);
            
        }
        public abstract void UpdateAction(GameTime gameTime);
        /// <summary>
        /// 技能未成功发出或者强行中断
        /// </summary>
        public void Interrupt()
        {
            InterruptAction();

            this.isCasting = false;
            this.castedTime = 0;
            this.isChannelling = false;
            this.channelledTime = 0;
            this.channelDone = 0;
            this.target = null;
       
            
            
        }
        public abstract void InterruptAction();
        /// <summary>
        /// 直接释放或开始引导
        /// </summary>
        private void CastOut()
        {
            if (channelTime <= 0)
            {
                Do();
                Casted();
            }
            else
            {
                StartChannelling();
                Casted();
            }
        }
        /// <summary>
        /// 技能已发出或开始引导后的的收尾工作
        /// </summary>
        private void Casted()
        {
            CooldownRemain = Cooldown;
            this.isCasting = false;
            this.castedTime = 0;
        }
        private void StartChannelling()
        {
            StartChannellingAction();
            Casted();
            isChannelling = true;
            channelledTime = 0;
        }
        public abstract void StartChannellingAction();
        private void StartCasting()
        {
            StartCastingAction();
            isCasting = true;
            castedTime = 0;
            isChannelling = false;
            channelledTime = 0;
            channelDone = 0;

        }
        public abstract void StartCastingAction();
        /// <summary>
        /// 检测技能正常施放或引导的条件(冷却时间除外)
        /// </summary>
        private bool SkillConditionCheck()
        {
            
            if (margedUnit.UnitState == UnitState.alive)
            {
                if (ConditionCheck())
                {
                    return true;
                } 
          
                return false;
            }
            return false;


        }
        public abstract bool ConditionCheck();

        /// 造成技能效果
        /// </summary>
        private void Do()
        {
            SkillAction();
         
            this.isCasting = false;
            this.castedTime = 0;
        }
        public abstract void SkillAction();


        /// <summary>
        /// 使用当前技能设置赋予创建一个技能
        /// </summary>
        /// <returns>技能</returns>
        public Skill Clone()
        {
            Skill sk = (Skill)this.MemberwiseClone();
            
            return sk;
        }

        /// <summary>
        /// 通过技能XML创建一个技能
        /// </summary>
        /// <param name="unit">属于的单位</param>
        /// <param name="skillAssetName">技能类别的AssetName</param>
        /// <returns>创建的技能</returns>
        public static Skill CreateSkill(Unit unit, string skillAssetName, GameWorld gameWorld)
        {

            Skill sk = gameWorld.Content.Load<Skill>(skillAssetName).Clone();

            sk.MargedUnit = unit;
            sk.gameWorld = gameWorld;
            //if (sk.DoEffectAssetName != "")
            //{
            //    sk.DoEffect = new ParticleEffect(gameWorld, gameWorld.Content.Load<ParticleEffectType>(sk.DoEffectAssetName), unit.Model);

            //}
            //if (sk.CastEffectAssetName != "")
            //{
            //    sk.CastEffect = new ParticleEffect(gameWorld, gameWorld.Content.Load<ParticleEffectType>(sk.CastEffectAssetName), unit.Model);

            //}
            //if (sk.ChannelEffectAssetName != "")
            //{
            //    sk.ChannelEffect = new ParticleEffect(gameWorld, gameWorld.Content.Load<ParticleEffectType>(sk.ChannelEffectAssetName), unit.Model);

            //}

            return sk;
        }
        
    }

}
