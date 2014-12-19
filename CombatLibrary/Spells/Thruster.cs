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
    public class Thruster : Skill
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
            MargedUnit.GetThrust(0.01f * MargedUnit.Face);
            MargedUnit.GetImpulse(FloatValues[0] * MargedUnit.Face);
            if (MargedUnit == GameWorld.CurrentStage.Player)
            {

                Color c = Color.Orange;
                c.A = (byte)(0.15f * c.A);
                GameWorld.ScreenEffectManager.Blink(c, 0.4f);
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
