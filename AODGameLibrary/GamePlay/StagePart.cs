using System;
using System.Collections.Generic;

using System.Text;
using AODGameLibrary.Units;
using Microsoft.Xna.Framework;
using AODGameLibrary.Cameras;
using AODGameLibrary.Weapons;
using AODGameLibrary.Gamehelpers;
using AODGameLibrary.Effects;
using Microsoft.Xna.Framework.Graphics;
using AODGameLibrary.Interface;
using AODGameLibrary.AODObjects;
using AODGameLibrary.GamePlay;
using Microsoft.Xna.Framework.Content;

namespace AODGameLibrary.GamePlay
{
    /// <summary>
    /// 关卡的一个部分,由大地无敌-范若余于2009年10月26日建立
    /// </summary>
    public abstract class StagePart
    {
        private Stage stage;
        /// <summary>
        /// 父关卡
        /// </summary>
        public Stage Stage
        {
            get { return stage; }
            set { stage = value; }
        }
        public WorldVars Variables
        {
            get { return stage.Variables; }
        }
        /// <summary>
        /// 关卡部分初始化（除玩家之外）
        /// </summary>
        public virtual void Initialize()
        {
           
        }
        /// <summary>
        /// 如果全新开始或失败之后或者中途退出之后从这里开始那么需要执行的代码(摆放玩家位置等)
        /// </summary>
        public virtual void StartFormThis()
        {

        }
        /// <summary>
        /// 使关卡尝试触发事件
        /// </summary>
        public abstract void Touch();

        /// <summary>
        /// 单位死亡后触发的事件
        /// </summary>
        /// <param name="deadUnit">死去的单位</param>
        public virtual void Event_UnitDied(Unit deadUnit)
        {
            
        }
        /// <summary>
        /// 计时器时间到
        /// </summary>
        /// <param name="timer">时间到的计时器</param>
        public virtual void Event_TimerRing(Timer timer)
        {

        }
        public virtual void Event_PlayerDied(Unit player)
        {

        }
        public ContentManager Content
        {
            get
            {
                return stage.Content;
            }
        }

    }
}
