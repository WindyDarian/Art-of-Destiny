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
    //震波炮，由大地无敌-范若余于2010年6月14日制作
    public class Shockgun : Skill
    {
        ///// <summary>
        ///// 震波炮火
        ///// </summary>
        //private ParticleLine pl;
        public string EffectLine;
        /// <summary>
        /// 判断是否符合施放条件
        /// </summary>
        /// <returns></returns>
        public override bool ConditionCheck()
        {
            if (Target != null)
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
            int n = (int)AODGameLibrary.Helpers.RandomHelper.RandomInt(0, 7);
            Vector3 m;
            if (MargedUnit.Target.Position - MargedUnit.SkillPosition(n) != Vector3.Zero)
            {
                m = Vector3.Normalize(MargedUnit.Target.Position - MargedUnit.SkillPosition(n));
            }
            else
            {
                m = MargedUnit.Face;
            }

            ParticleLine pl;
            pl = (ParticleLine)GameWorld.Content.Load<ParticleLine>(EffectLine).Clone();

            pl.Origin = MargedUnit.SkillPosition(n);
            pl.Direction = m;
            GameWorld.AddParticleShape(pl);
            pl.Flash();

            Barrel b = new Barrel(MargedUnit.SkillPosition(n), m, FloatValues[0], FloatValues[1], FloatValues[2]);
            foreach (VioableUnit u in GameWorld.GameItemManager.BoundingCollection)
            {
                if (u != null && u != MargedUnit)
                {
                    if (Vector3.Distance(u.Position, MargedUnit.Position) < Range)
                    {
                        if (Collision.IsCollided(u, b))
                        {
                            u.GetDamage(Damage.CreateFromDamage(Damage.CreateFromDamage(Damage, MargedUnit), MargedUnit));
                            if (!u.Heavy)
                            {
                                u.GetImpulse(m * FloatValues[3]);
                            }
                        }
                    }
                }
                Color c = Color.BlueViolet;
                c.A = (byte)(0.1f * c.A);
                GameWorld.ScreenEffectManager.Blink(c, 0.2f);
            }
        }
        /// <summary>
        /// Update时的动作
        /// </summary>
        public override void UpdateAction(GameTime gameTime)
        {
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
