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
using AODGameLibrary.Units;
using AODGameLibrary.Effects.ParticleShapes;
using AODGameLibrary.CollisionChecking;

namespace CombatLibrary.Spells
{
    /// <summary>
    /// 冲击波，由大地无敌-范若余于2010年6月13日创建
    /// </summary>
    public class Shockwave : Skill
    {
        List<ParticleEffect> pes = new List<ParticleEffect>(5);
        List<VioableUnit> damaged = new List<VioableUnit>(10);
        List<ParticleEffect> re = new List<ParticleEffect>(5);
        Vector3 direction;
        /// <summary>
        /// 判断是否符合施放条件
        /// </summary>
        /// <returns></returns>
        public override bool ConditionCheck()
        {
            if (Target!= null  )
            {
                if (Unit.Distance(MargedUnit, Target) <= Range)
                {
                     return true;
                  
                }
               
            }
           return false;
        }
        /// <summary>
        /// 技能行为发生时的动作
        /// </summary>
        public override void SkillAction()
        {
             
            ParticleEffect pe = new ParticleEffect(GameWorld, GameWorld.Content.Load<ParticleEffectType>(@"ParticleEffectTypes\BlueWave"),null,MargedUnit.SkillPosition(SkillPointNum),1);
            Vector3 v = Target.Position - MargedUnit.SkillPosition(SkillPointNum);
            damaged.Clear();
            if (v != Vector3.Zero)
            {

                direction = Vector3.Normalize(v);
            }
            else direction = Vector3.Forward;
            //pe.ReBirth();
            pes.Add(pe);
            GameWorld.AddParticleEffect(pe);
            
            
        }
        /// <summary>
        /// Update时的动作
        /// </summary>
        public override void UpdateAction(GameTime gameTime)
        {

            for (int i=0; i < pes.Count; i++)
            {
                ParticleEffect pe = pes[i];
                if (Vector3.Distance(pe.position, MargedUnit.SkillPosition(SkillPointNum)) < base.FloatValues[3])
                {
                    Vector3 p0 = pe.position;
                    float s0 = pe.Scale;
                    pe.position += direction * FloatValues[0] * (float)gameTime.ElapsedGameTime.TotalMilliseconds;
                    pe.Scale *= FloatValues[1] * (float)gameTime.ElapsedGameTime.TotalMilliseconds;
                    foreach (VioableUnit u in GameWorld.GameItemManager.BoundingCollection)
                    {
                        if (u.Group != MargedUnit.Group)
                        {
                            if (Collision.IsCollided(u, new Barrel(p0, pe.position, 32 * s0, 32 * pe.Scale)))
                            {
                                if (damaged.Contains(u) == false)
                                {
                                    u.GetDamage(Damage);
                                    u.GetImpulse(direction * FloatValues[2]);
                                    damaged.Add(u);
                                }

                            }

                        }
                    }
                }
                else
                {
                    pe.BeginToDie();
                    re.Add(pe);
                }
            }
            if (re.Count != 0)
            {
                damaged.Clear();
            }
            foreach (ParticleEffect pe in re)
            {

                pes.Remove(pe);
            }
         
            re.Clear();
        }
        /// <summary>
        /// 被中断时的动作
        /// </summary>
        public override void InterruptAction()
        {

        }
        /// <summary>
        /// 开始准备施放的动作
        /// </summary>
        public override void StartCastingAction()
        {

        }
        /// <summary>
        /// 开始通道施放的动作
        /// </summary>
        public override void StartChannellingAction()
        {

        }

    }
}
