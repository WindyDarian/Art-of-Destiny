using System;
using System.Collections.Generic;

using System.Text;
using AODGameLibrary.Units;
using AODGameLibrary.Cameras;

namespace AODGameLibrary.GamePlay
{
    /// <summary>
    /// 存放游戏中的变量，由大地无敌-范若余在2009年8月9日搞定
    /// </summary>
    public class WorldVars
    {
        public Unit[] Unit = new Unit[1000];
        public bool[] Switch = new bool[1000];

        Unit lastCreatedUnit;
        /// <summary>
        /// 得到最后一个创建的单位
        /// </summary>
        public Unit LastCreatedUnit
        {
            get
            {
                return lastCreatedUnit;
            }
            set
            {
                lastCreatedUnit = value;
            }
        }
        Unit player;
        public Unit Player
        {
            get
            {
                return player;
            }
            set
            {
                player = value;
            }
        }
        Camera playerCamera;
        public Camera PlayerCamera
        {
            get
            {
                return playerCamera;
            }
            set
            {
                playerCamera = value;
            }
        }
        /// <summary>
        /// 游戏中经过的时间（秒）
        /// </summary>
        float totalGameTime;
        public float TotalGameTime
        {
            get { return totalGameTime; }
            set { totalGameTime = value; }
        }
        public Ambient.Decoration LastCreatedDecoration;
    }
}