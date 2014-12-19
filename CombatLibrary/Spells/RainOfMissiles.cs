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
    /// 一个技能，大地无敌-范若余于2010年6月6日从Skill类中移出
    /// </summary>
    public class RainOfMissiles : Skill
    {
        /// <summary>
        /// 判断是否符合施放条件
        /// </summary>
        /// <returns></returns>
        public override bool ConditionCheck()
        {
            if (Target != null)
            {

                if (Target.UnitState == UnitState.alive && Unit.Distance(MargedUnit, Target) <= Range)
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
            if (MargedUnit.CurrentMissileWeapon != null)
            {
                for (int i = 0; i < 3; i++)
                {
                    if (MargedUnit.CurrentMissileWeapon.Num > 0)
                    {

                        GameWorld.AddNewMissile(new Missile(GameWorld, MargedUnit, Target, MargedUnit.CurrentMissileWeapon.missileType, MargedUnit.SkillPosition(i % 3)));
                        if (!MargedUnit.EndlessBullets)
                        {
                            MargedUnit.CurrentMissileWeapon.Num -= 1;
                        }
                    }
                }

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
