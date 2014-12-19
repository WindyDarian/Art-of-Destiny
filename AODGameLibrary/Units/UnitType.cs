using System;
using System.Collections.Generic;

using System.Text;
using Microsoft.Xna.Framework;
using AODGameLibrary.GamePlay;
using AODGameLibrary.AIs;

namespace AODGameLibrary.Units
{
    [Serializable]
    /// <summary>
    /// 表示一种单位，由大地无敌-范若余在2009年8月8日改良并使其支持XML
    /// </summary>
    public class UnitType
    {
        #region 声明字段
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
        public float Scale = 1;
        /// <summary>
        /// 模型3轴旋转角度
        /// </summary>
        public Vector3 ModelRotation = Vector3.Zero;
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
        /// 最大功率,数值上等于frictionForce与最大速度的乘积
        /// </summary>
        public float MaxPower = 60000;
        /// <summary>
        /// move()方法向前移动中的最大推力大小，即在达到最大功率前的推力,应大于frictionForce
        /// </summary>
        public float ThrustForce = 2000; 
        /// <summary>
        /// 只要在运动中就一直会受到的摩擦力大小
        /// </summary>
        public float FrictionForce = 200;
        /// <summary>
        /// 转向速率,转向时每秒旋转角度
        /// </summary>
        public float AngularRate = 540;
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
        /// 武器,4个
        /// </summary>
        public List<string> Weapons = new List<string>(4);
        /// <summary>
        /// 导弹
        /// </summary>
        public List<string> MissileWeapons = new List<string>(4);
        /// <summary>
        /// 技能列表
        /// </summary>
        public List<string> Skills = new List<string>(8);
        /// <summary>
        /// 单位AI选项名
        /// </summary>
        public AISettings AISettings;
        public bool Bounding = false;
        public bool Heavy = false;
        public int MaxWeaponNum;
        public int MaxMissileWeaponNum;
        public int MaxSkillNum;
        public List<LootEntry> Loots;
        
        #endregion
        public UnitType Clone()
        {
            return (UnitType)this.MemberwiseClone();
        }
    }
    [Serializable]
    /// <summary>
    /// 大地无敌-范若余在2009年10月5日建立，表示一个Loot的参数
    /// </summary>
    public class LootEntry
    {
        /// <summary>
        /// 属于类型的别名
        /// </summary>
        public string LootAssetName;
        /// <summary>
        /// 掉落率(0到1之间)
        /// </summary>
        public float Prob;
        /// <summary>
        /// 距离掉落单位中心的最大距离
        /// </summary>
        public float MaxRadius;
    }
}
