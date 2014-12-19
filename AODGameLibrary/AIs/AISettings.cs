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
using AODGameLibrary.Units;
using AODGameLibrary.Gamehelpers;
using AODGameLibrary.AODObjects;
using AODGameLibrary.GamePlay;


namespace AODGameLibrary.AIs
{
    [Serializable]
    /// <summary>
    /// AI设置，由范若余-大地无敌在2009年8月8日创建
    /// </summary>
    public struct AISettings
    {
        /// <summary>
        /// 视野范围，远离视野范围威胁归0
        /// </summary>
        public float rangeOfView; //= 10000;
        /// <summary>
        /// 主动攻击范围,一般来说接近视野范围
        /// </summary>
        public float rangeOfAttack;// = 9000;
        /// <summary>
        /// 射击范围范围内将开始射击
        /// </summary>
        public float rangeOfShot;// = 600;
        /// <summary>
        /// 最大环绕范围
        /// </summary>
        public float rangeOfCycle;// = 450;
        /// <summary>
        /// 最小距离，在这个距离内将远离
        /// </summary>
        public float minRange;// = 200;
        /// <summary>
        /// 射击角度，在这个角度之内则射击
        /// </summary>
        public float shotJudgmentAngel;// = 30f;
        /// <summary>
        /// 更新时间间隔，秒
        /// </summary>
        public float updateTimeSpan;// = 2.0f;
        /// <summary>
        /// 是否射击
        /// </summary>
        public bool isShotAble;// = true;
        /// <summary>
        /// 是否可以自主旋转
        /// </summary>
        public bool isRotateAble;// = true;
        /// <summary>
        /// 是否可以自主移动
        /// </summary>
        public bool isMoveAble;// = true;
        /// <summary>
        /// 是否开启智能射击(射击是否考虑弹药的惯性的影响)
        /// </summary>
        public bool isSmartShot;// = false;
        /// <summary>
        /// 是否射导弹
        /// </summary>
        public bool isMissileLaunchable;// = false;
        /// <summary>
        /// 导弹发射间隔
        /// </summary>
        public float MissileLaunchSpan;// = 10.0f;
        /// <summary>
        /// 最小导弹范围
        /// </summary>
        public float MinMissileRange;// = 200.0f;
        /// <summary>
        /// 最远导弹范围
        /// </summary>
        public float MaxMissileRange;// = 400.0f;
        /// <summary>
        /// 是否随时改变战术偏好旋转方向
        /// </summary>
        public bool isLoveDirectionChangeAble;// = true;
        /// <summary>
        /// 是否在无目标时漫游
        /// </summary>
        public bool isSwimable;// = true;
        /// <summary>
        /// 是否环绕攻击
        /// </summary>
        public bool isCycleAble;// = true;
        /// <summary>
        /// 是否可用技能
        /// </summary>
        public bool isSkillUsable;// = false;
        /// <summary>
        /// 是否可以换技能
        /// </summary>
        public bool isSkillSwitchable;// = false;

      
        ///// <summary>
        ///// 通过模板克隆一个AI设置,避免两个单位使用的AI设置是同一个对象
        ///// </summary>
        //public AISettings Clone()
        //{
        //    AISettings clone = (AISettings)this.MemberwiseClone();
        //    return clone;
        //    //clone.isLoveDirectionChangeAble = isLoveDirectionChangeAble;
        //    //clone.isMissileLaunchable = isMissileLaunchable;
        //    //clone.isMoveAble = isMoveAble;
        //    //clone.isRotateAble = isRotateAble;
        //    //clone.isShotAble = isShotAble;
        //    //clone.isSmartShot = isSmartShot;
          
        //}

    }
}
