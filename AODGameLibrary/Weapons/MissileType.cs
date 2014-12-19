using System;
using System.Collections.Generic;

using System.Text;
using Microsoft.Xna.Framework;

namespace AODGameLibrary.Weapons
{
    /// <summary>
    /// 表示一个导弹类型
    /// 由大地无敌-范若余2009年7月26日创建
    /// </summary>
    public class MissileType
    {

        /// <summary>
        /// 爆炸半径
        /// </summary>
        public float explosionRadius = 30.0f;
        /// <summary>
        /// 质量
        /// </summary>
        public float mass = 1.0f;
        /// <summary>
        /// 最大推力
        /// </summary>
        public float maxThrustForce = 120.0f;
        /// <summary>
        /// 最大功率
        /// </summary>
        public float maxPower = 1500.0f;
        /// <summary>
        /// 摩擦力
        /// </summary>
        public float frictionForce = 5.0f;
        /// <summary>
        /// 相对于射出单位的初速度
        /// </summary>
        public float basicSpeed = 10.0f;
        /// <summary>
        /// 基本伤害
        /// </summary>
        public float basicDamage = 455.0f;
        /// <summary>
        /// 对护盾造成的伤害倍数
        /// </summary>
        public float foldShield = 0.1f;
        /// <summary>
        /// 对护甲造成的伤害倍数
        /// </summary>
        public float foldArmor = 1.5f;
        /// <summary>
        /// 穿甲系数。
        /// </summary>
        public float crossValue = 0.02f;
        /// <summary>
        /// 是否无视护盾。若值为true则crossValue无效化，伤害计算式为护甲伤害=BasicDamage*foldArmor
        /// </summary>
        public bool isShieldUseless = false;
        /// <summary>
        /// 模型缩放
        /// </summary>
        public float scale = 1.0f;
        /// <summary>
        /// 模型名
        /// </summary>
        public string modelName;
        /// <summary>
        /// 模型旋转
        /// </summary>
        public Matrix modelRotation = Matrix.CreateRotationY(MathHelper.Pi);
        /// <summary>
        /// 最大存在时间,时间到了会爆炸
        /// </summary>
        public float lifeTime;
        
    }
}
