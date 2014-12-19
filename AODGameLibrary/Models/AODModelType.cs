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
using AODGameLibrary.Effects;
using AODGameLibrary.GamePlay;

namespace AODGameLibrary.Models
{
    /// <summary>
    /// 表示一种模型，由大地无敌-范若余在2009年8月10日重写
    /// </summary>
    public class AODModelType
    {
        #region 抛弃的代码
        //public string dModel1 = "";
        //public float dModel1_scale = 1.0f;
        //public Matrix dModel1_rotation = Matrix.Identity;
        //public Vector3 dModel1_position = Vector3.Zero;
        //public string dModel2 = "";
        //public float dModel2_scale = 1.0f;
        //public Matrix dModel2_rotation = Matrix.Identity;
        //public Vector3 dModel2_position = Vector3.Zero;
        //public string dModel3 = "";
        //public float dModel3_scale = 1.0f;
        //public Matrix dModel3_rotation = Matrix.Identity;
        //public Vector3 dModel3_position = Vector3.Zero;
        //public string dModel4 = "";
        //public float dModel4_scale = 1.0f;
        //public Matrix dModel4_rotation = Matrix.Identity;
        //public Vector3 dModel4_position = Vector3.Zero;
        //public string dModel5 = "";
        //public float dModel5_scale = 1.0f;
        //public Matrix dModel5_rotation = Matrix.Identity;
        //public Vector3 dModel5_position = Vector3.Zero;
        //public string pEffect1 = "";
        //public Vector3 pEffect1_position = Vector3.Zero;
        //public string pEffect2 = "";
        //public Vector3 pEffect2_position = Vector3.Zero;
        //public string pEffect3 = "";
        //public Vector3 pEffect3_position = Vector3.Zero;
        //public string pEffect4 = "";
        //public Vector3 pEffect4_position = Vector3.Zero;
        //public string pEffect5 = "";
        //public Vector3 pEffect5_position = Vector3.Zero;
        //public string pEffect6 = "";
        //public Vector3 pEffect6_position = Vector3.Zero;
        //public string pEffect7 = "";
        //public Vector3 pEffect7_position = Vector3.Zero;
        //public string pEffect8 = "";
        //public Vector3 pEffect8_position = Vector3.Zero;
        //public string pEffect9 = "";
        //public Vector3 pEffect9_position = Vector3.Zero;
        //public string pEffect10 = "";
        //public Vector3 pEffect10_position = Vector3.Zero;
        //public string deathEffect1 = "";
        //public Vector3 deathEffect1_position = Vector3.Zero;
        //public string deathEffect2 = "";
        //public Vector3 deathEffect2_position = Vector3.Zero;
        //public string moveEffect1 = "";
        //public Vector3 moveEffect1_position = Vector3.Zero;
        //public string moveEffect2 = "";
        //public Vector3 moveEffect2_position = Vector3.Zero;
        //public string moveEffect3 = "";
        //public Vector3 moveEffect3_position = Vector3.Zero;
        //public string moveEffect4 = "";
        //public Vector3 moveEffect4_position = Vector3.Zero;
        //public string moveEffect5 = "";
        //public Vector3 moveEffect5_position = Vector3.Zero;
        //public string moveEffect6 = "";
        //public Vector3 moveEffect6_position = Vector3.Zero;
        #endregion
        /// <summary>
        /// 从属于该模型的DModel
        /// </summary>
        public List<DModelEntry> ChildModels;
        /// <summary>
        /// 正常状态的效果
        /// </summary>
        public List<ParticleEffectEntry> NormalEffects;
        /// <summary>
        /// 只在死亡前显示的效果
        /// </summary>
        public List<ParticleEffectEntry> DeathEffects;
        /// <summary>
        /// 只在移动中显示的效果
        /// </summary>
        public List<ParticleEffectEntry> MovementEffects;
        /// <summary>
        /// 只在出生时一段时间内显示的效果
        /// </summary>
        public List<ParticleEffectEntry> BirthEffects;
        /// <summary>
        /// 武器位置
        /// </summary>
        public List<Vector3> WeaponPositions = new List<Vector3>(8);
        /// <summary>
        /// 技能位置
        /// </summary>
        public List<Vector3> SkillPositions = new List<Vector3>(8);

        /// <summary>
        /// 出生音效
        /// </summary>
        public string BirthSound;
        /// <summary>
        /// 死亡音效
        /// </summary>
        public string DeathSound;

        /// <summary>
        /// 一般碰撞球半径
        /// </summary>
        public float ExtraBoundingSphereRadius = 0;
        /// <summary>
        /// 是否附加模型碰撞球
        /// </summary>
        public bool ModelCollision = true;
        /// <summary>
        /// 碰撞检测等级，0为只和粗略碰撞球进行球形碰撞检测，1要和
        /// 每个部分的碰撞球进行检测，2要进行OBB碰撞检测.....
        /// </summary>
        public int CollisionCheckLevel = 0;

        /// <summary>
        /// 名字
        /// </summary>
        public string name = "未命名AOD模型";
       
    

    }
    /// <summary>
    /// 大地无敌-范若余在2009年8月10日建立，更好地描述一个有位置参数的粒子效果
    /// </summary>
    public class ParticleEffectEntry
    {
        /// <summary>
        /// 属于粒子效果类别名
        /// </summary>
        public string AssetName;
        /// <summary>
        /// 相对位置
        /// </summary>
        public Vector3 Position;
        /// <summary>
        /// 缩放
        /// </summary>
        public float Scale;
    }
    /// <summary>
    /// 大地无敌-范若余在2009年8月10日建立，更好地描述一个有位置、旋转参数的DModel
    /// </summary>
    public class DModelEntry
    {
        /// <summary>
        /// 次级模型
        /// </summary>
        public DModelType DModel;
        /// <summary>
        /// 相对位置
        /// </summary>
        public Vector3 Position;
        /// <summary>
        /// 旋转
        /// </summary>
        public Vector3 Rotation;
        /// <summary>
        /// 缩放
        /// </summary>
        public float Scale;
        public float Alpha;
    }
}
