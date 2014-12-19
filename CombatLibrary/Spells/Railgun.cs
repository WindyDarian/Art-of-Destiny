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
    //轨道炮，由大地无敌-范若余于2010年6月6日从Skill类中移出
    public class Railgun : Skill
    {
        /// <summary>
        /// 轨道炮火
        /// </summary>
        private ParticleLine pl;
        public string EffectLine;
        public bool DamageAll;
        /// <summary>
        /// 判断是否符合施放条件
        /// </summary>
        /// <returns></returns>
        public override bool ConditionCheck()
        {
            if (true)
            {
                return true;
            }
            else return false;
        }
        /// <summary>
        /// 技能行为发生时的动作
        /// </summary>
        public override void SkillAction()
        {
            Barrel b = new Barrel(MargedUnit.SkillPosition(SkillPointNum), MargedUnit.Face, FloatValues[0], FloatValues[1], FloatValues[2]);
            foreach (VioableUnit u in GameWorld.GameItemManager.BoundingCollection)
            {
                if (u != null && u != MargedUnit)
                {
                    if (Vector3.Distance(u.Position, MargedUnit.Position) < Range)
                    {
                        if (DamageAll || u.Group != MargedUnit.Group)
                        {


                            if (Collision.IsCollided(u, b))
                            {
                                u.GetDamage(Damage.CreateFromDamage(Damage.CreateFromDamage(Damage, MargedUnit), MargedUnit));

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
            if (pl != null)
            {
                pl.Origin = MargedUnit.SkillPosition(SkillPointNum);
                pl.Direction = MargedUnit.Face;
            }
        }
        /// <summary>
        /// 被中断时的动作
        /// </summary>
        public override void InterruptAction()
        {
            if (pl != null)
            {
                pl.BeginToDie();

                pl = null;
            }
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
            ParticleLine k = pl;
            //pl = (ParticleLine)GameWorld.Content.Load<ParticleLine>(@"ParticleEffectTypes\ParticleLines\Railgun").Clone();
            pl = (ParticleLine)GameWorld.Content.Load<ParticleLine>(EffectLine).Clone();
            pl.Origin = MargedUnit.SkillPosition(SkillPointNum);
            pl.Direction = MargedUnit.Face;
            GameWorld.AddParticleShape(pl);
        }

    }
}
