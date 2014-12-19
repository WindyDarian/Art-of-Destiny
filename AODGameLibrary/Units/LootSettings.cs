using System;
using System.Collections.Generic;

using System.Text;

namespace AODGameLibrary.Units
{
    /// <summary>
    /// 可拾取的物件，由大地无敌-范若余于2009年10月2日建立
    /// </summary>
    public class LootSettings
    {
        /// <summary>
        /// 名字
        /// </summary>
        public string Name;
        /// <summary>
        /// 可拾取物件类别
        /// </summary>
        public LootType LootType;
        /// <summary>
        /// 得到的武器或技能或任务物品的AssetName(含目录)
        /// </summary>
        public string ObjectName;
        /// <summary>
        /// 是否为武器，如果为false则只能在有武器的情况下得到弹药，只在LootType为Weapon或MissileWeapon时有效
        /// </summary>
        public bool IsWeapon;
        /// <summary>
        /// 最小弹药数，只在LootType为Weapon或MissileWeapon时有效
        /// </summary>
        public int MinBulletNum;
        /// <summary>
        /// 最大弹药数，只在LootType为Weapon或MissileWeapon时有效
        /// </summary>
        public int MaxBulletNum;
        /// <summary>
        /// 模型名
        /// </summary>
        public string ModelName;
        /// <summary>
        /// 模型缩放
        /// </summary>
        public float ModelScale;
        /// <summary>
        /// 拾取半径
        /// </summary>
        public float LootRadius;



    }
    public enum LootType
    {
        /// <summary>
        /// 武器
        /// </summary>
        Weapon,
        /// <summary>
        /// 导弹武器
        /// </summary>
        MissileWeapon,
        /// <summary>
        /// 技能
        /// </summary>
        SkillItem,
        /// <summary>
        /// 任务物品（未使用）
        /// </summary>
        QuestItem,

    }
}
