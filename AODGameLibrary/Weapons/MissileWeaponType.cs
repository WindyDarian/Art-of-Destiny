using System;
using System.Collections.Generic;

using System.Text;

namespace AODGameLibrary.Weapons
{
    /// <summary>
    /// 表示发射导弹的武器类别
    /// 由大地无敌-范若余2009年7月29日创建
    /// </summary>
    public class MissileWeaponType
    {
        /// <summary>
        /// 最大载弹量
        /// </summary>
        public int maxNum;
        /// <summary>
        /// 射击冷却时间
        /// </summary>
        public float cooldown;
        /// <summary>
        /// 发射的导弹类型名
        /// </summary>
        public string missileTypeName;
        /// <summary>
        /// 名字
        /// </summary>
        public string name;
        /// <summary>
        /// 位置序号1
        /// </summary>
        public int positionIndex1;
        /// <summary>
        /// 位置序号2
        /// </summary>
        public int positionIndex2;
        /// <summary>
        /// 武器的AssetName（不含目录）
        /// </summary>
        public string AssetName;
        /// <summary>
        /// 克隆一个导弹类型
        /// </summary>
        public MissileWeaponType Clone()
        {
            MissileWeaponType mw = (MissileWeaponType)this.MemberwiseClone();
            return mw;
            
        }
    }
}
