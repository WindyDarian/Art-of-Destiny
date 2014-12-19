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
using AODGameLibrary.Cameras;
using AODGameLibrary.GamePlay;

//废弃 2011/12/15 大地无敌-范若余
namespace AODGameLibrary.Effects
{
    [Serializable]
    public class CPUParticleGroupType
    {
        /// <summary>
        /// 粒子的纹理资源
        /// </summary>
        public string texture = "textures\\fire.png";
        /// <summary>
        /// 粒子的位置
        /// </summary>
        public Vector3 groupPosition = Vector3.Zero;
        /// <summary>
        /// 每个粒子存活时间
        /// </summary>
        public float parLifetime = 1.0f;
        /// <summary>
        /// 每个粒子的初始颜色
        /// </summary>
        public Vector4 startColor = new Vector4(255, 255, 255, 255);
        /// <summary>
        /// 每个粒子的结束颜色
        /// </summary>
        public Vector4 endColor = new Vector4(255, 255, 255, 0);
        /// <summary>
        ///  每个粒子的可能最小速度
        /// </summary>
        public float minInitialSpeed = 10.0f;
        /// <summary>
        ///  每个粒子的可能最大速度
        /// </summary>
        public float maxInitialSpeed = 100.0f;
        /// <summary>
        /// 每个粒子的可能最小倍数
        /// </summary>
        public float minScale = 0.5f;
        /// <summary>
        /// 每个粒子的可能最大倍数
        /// </summary>
        public float maxScale = 1.5f;
        /// <summary>
        /// 最终缩放大小与初始大小的比
        /// </summary>
        public float scaleRate = 0.3f;
        /// <summary>
        /// 每桢添加的粒子数目,可以不为整数
        /// </summary>
        public float addParticleNum = 0.4f;
        /// <summary>
        /// 点的基本大小
        /// </summary>
        public float pointSize = 32.0f;
        /// <summary>
        /// 持续产生新粒子的总时间，若isLasting为false时间到后将不再产生新粒子
        /// </summary>
        public float Deathtime = 1.0f;
        /// <summary>
        /// 粒子是否随群组一起运动
        /// </summary>
        public bool particleMoveWithGroup = false;
        /// <summary>
        /// 粒子群组的名称
        /// </summary>
        public string name = "新粒子群组";
        public CPUParticleGroupType()
        {
            
        }
    }
}
