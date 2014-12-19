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
    //特殊导弹攻击，大地无敌-范若余于2010年6月6日从Skill类中移出
    public class SpecialMissile_AE : Skill
    {
        /// <summary>
        /// 发射导弹类型的AssetName,只对SpecialMissile有效
        /// </summary>
        public string MissileTypeAssetName;
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
            return false;
        }
        /// <summary>
        /// 技能行为发生时的动作
        /// </summary>
        public override void SkillAction()
        {
            foreach (Unit u in GameWorld.GameItemManager.units)
            {
                if (u.Dead == false && u.Group != MargedUnit.Group)
                {

                    GameWorld.AddNewMissile(new Missile(GameWorld, MargedUnit, u, GameWorld.Content.Load<MissileType>(MissileTypeAssetName), MargedUnit.SkillPosition(SkillPointNum)));

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
