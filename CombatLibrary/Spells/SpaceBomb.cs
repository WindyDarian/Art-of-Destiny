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
    /// 空间炸弹，造成范围伤害并炸飞，大地无敌-范若余于2010年6月6日从Skill类中移出
    /// </summary>
    public class SpaceBomb : Skill
    {
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
            List<VioableUnit> dn = GameWorld.GameItemManager.ItemInRange(MargedUnit.Position, FloatValues[1]);
            foreach (VioableUnit u in dn)
            {
                if (u.UnitState == UnitState.alive && u.Group != MargedUnit.Group && u.Heavy == false)
                {
                    u.GetDamage(Damage);
                    if (u.Position != MargedUnit.Position)
                    {
                        u.GetImpulse(FloatValues[0] * Vector3.Normalize(u.Position - MargedUnit.Position));//造成冲量，弹飞
                    }
                    else u.GetImpulse(FloatValues[0] * Vector3.Up);

                }
            }
            GameWorld.ScreenEffectManager.Blink();
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
