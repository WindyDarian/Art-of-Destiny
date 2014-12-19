using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Net;
using Microsoft.Xna.Framework.Storage;
using AODGameLibrary.AODObjects;
using AODGameLibrary.Units;


namespace AODGameLibrary.Ambient
{
    /// <summary>
    /// 装饰物类型
    /// </summary>
    public class DecorationType
    {
        /// <summary>
        /// 模型文件的位置
        /// </summary>
        public string Modelname = @"AODModelTypes\TestModel";
        /// <summary>
        /// 单位名称
        /// </summary>
        public string Name = "未知目标";
        /// <summary>
        /// 模型缩放值
        /// </summary>
        public float MinScale = 1;
        public float MaxScale = 1;
        /// <summary>
        /// 旋转
        /// </summary>
        public Matrix ModelRotation = Matrix.Identity;
        /// <summary>
        /// 是否随机旋转
        /// </summary>
        public bool RandomModelRotation = true;
        /// <summary>
        /// 能量护盾最大值
        /// </summary>
        public int MaxShield = 100;
        /// <summary>
        /// 护甲最大值
        /// </summary>
        public int MaxArmor = 100;
        /// <summary>
        /// 飞船的质量,和运动时的速度等量有关
        /// </summary>
        public float Mass = 10;
        /// <summary>
        /// 只要在运动中就一直会受到的摩擦力大小
        /// </summary>
        public float FrictionForce = 200;
        /// <summary>
        /// 绕Y轴旋转速度(角)
        /// </summary>
        public float AngularRate = 10;
        /// <summary>
        /// 未受伤害时开始恢复护盾所需要的时间
        /// </summary>
        public float ShieldRestoreTime = 5.0f;
        /// <summary>
        /// 护盾恢复状态下每秒回复的护盾值
        /// </summary>
        public float ShieldRestoreRate = 500.0f;
        /// <summary>
        /// 每秒回复的护甲值(护甲一直回复但速度慢)
        /// </summary>
        public float ArmorRestoreRate = 18.0f;
        /// <summary>
        /// 是否可以在很远的地方看见
        /// </summary>
        public bool Far = false;
        public bool Bounding = false;
        public bool Heavy = false;
    }
}
