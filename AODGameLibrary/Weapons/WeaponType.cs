using System;
using System.Collections.Generic;

using System.Text;

namespace AODGameLibrary.Weapons
{
    [Serializable]
    public class WeaponType
    {
        
       
        /// <summary>
        /// 武器名
        /// </summary>
        public string weaponName = "新武器";
        /// <summary>
        /// 弹药名
        /// </summary>
        public string ammoName = "50mm新弹药";
        /// <summary>
        /// 最大弹药数量
        /// </summary>
        public float maxAmmo = 6000.0f;
        /// <summary>
        /// 基本伤害
        /// </summary>
        public float basicDamage = 2.0f;
        /// <summary>
        /// 对护盾造成的伤害倍数
        /// </summary>
        public float foldShield = 1.5f;
        /// <summary>
        /// 对护甲造成的伤害倍数
        /// </summary>
        public float foldArmor = 0.8f;
        /// <summary>
        /// 穿甲系数。若只对护盾造成伤害而无法对护甲造成伤害则为0，若需要对护甲按
        /// 剩余护盾比例造成伤害则介于0~1之间 默认为0.02 若m为单位护盾值与最大护盾值得比对单位的伤害计算式为：
        /// 护盾伤害=foldShield*damage*(1-crossValue)护甲伤害=foldArmor*damage*crossValue
        /// </summary>
        public float crossValue = 0.02f;
        /// <summary>
        /// 是否无视护盾。若值为true则crossValue无效化，伤害计算式为护甲伤害=damage*foldArmor
        /// </summary>
        public bool isShieldUseless = false;
        /// <summary>
        /// 每秒射弹量
        /// </summary>
        public float shotSpeed = 150.0f;


        /// <summary>
        /// 相对于射出弹药的Unit的弹体初速
        /// </summary>
        public float basicSpeed = 10000.0f;
        /// <summary>
        /// 基础射程
        /// </summary>
        public float range = 10000.0f;
        /// <summary>
        /// 弹药粒子效果
        /// </summary>
        public string bulletModelName = @"ParticleEffectTypes\NormalBullet";
        public float maxAngle = 1.0f;
        public bool isInstant = true;
        /// <summary>
        /// 射击间隔
        /// </summary>
        public float shotSpan;
        /// <summary>
        /// 是否快速射击武器,若是可以按住鼠标左键连发若为false则不会有supershot状态
        /// </summary>
        public bool isFastWeapon;
        /// <summary>
        /// 该武器是否在射击后会有最佳状态(射速加倍）
        /// </summary>
        public bool supershotModeEnabled;
        /// <summary>
        /// 最佳状态持续时间
        /// </summary>
        public float supershotTime;
        /// <summary>
        /// 最佳状态冷却时间
        /// </summary>
        public float supershotCooldown;
        /// <summary>
        /// 最佳状态冷却完成后进入所需的射击时间
        /// </summary>
        public float supershotEnterTime;
        public string shotEffectName;
        public string supershotEffectName;
        /// <summary>
        /// 位置序号1
        /// </summary>
        public int positionIndex1;
        /// <summary>
        /// 位置序号2
        /// </summary>
        public int positionIndex2;

        /// <summary>
        /// 射击声音
        /// </summary>
        public string shotSound;
        /// <summary>
        ///  是否一次只发射一枚弹药，若为true则shotSpeed无效化且不会有supershot状态
        /// </summary>
        public bool singleShotWeapon;
        /// <summary>
        /// 武器的AssetName（不含目录）
        /// </summary>
        public string AssetName;
        

    }
}
