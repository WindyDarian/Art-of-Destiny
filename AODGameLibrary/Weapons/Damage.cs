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
using AODGameLibrary.Gamehelpers;
using AODGameLibrary.Cameras;
using AODGameLibrary.Units;
using AODGameLibrary.Weapons;
using AODGameLibrary.GamePlay;
using AODGameLibrary.AODObjects;

namespace AODGameLibrary.Weapons
{
    /// <summary>
    /// 表示一个伤害的类
    /// </summary>
    public struct Damage
    {
        /// <summary>
        /// 基本伤害
        /// </summary>
        public float BasicDamage;
        /// <summary>
        /// 对护盾造成的伤害倍数
        /// </summary>
        public float FoldShield;
        /// <summary>
        /// 对护甲造成的伤害倍数
        /// </summary>
        public float FoldArmor;
        /// <summary>
        /// 穿甲系数。
        /// </summary>
        public float CrossValue;
        /// <summary>
        /// 是否无视护盾。若值为true则crossValue无效化，伤害计算式为护甲伤害=BasicDamage*foldArmor
        /// </summary>
        public bool IsShieldUseless;
        /// <summary>
        /// 伤害倍数
        /// </summary>
        public float Fold;
        /// <summary>
        /// 造成伤害的单位
        /// </summary>
        [ContentSerializerIgnore]
        public Unit attacker;
        /// <summary>
        /// 一个普通武器造成的伤害
        /// </summary>
        /// <param name="weapon">武器</param>
        public Damage(Weapon weapon)
        {
            BasicDamage = weapon.damage.BasicDamage;
            FoldArmor = weapon.damage.FoldArmor;
            FoldShield = weapon.damage.FoldShield;
            CrossValue = weapon.damage.CrossValue;
            IsShieldUseless = weapon.damage.IsShieldUseless;
            Fold = 1.0f;
            attacker = weapon.damage.attacker;
        }
        /// <summary>
        /// 一个普通武器造成的伤害
        /// </summary>
        /// <param name="weapon">武器</param>
        /// <param name="weapon">伤害倍数</param>
        public Damage(Weapon weapon,float fold)
        {
            BasicDamage = weapon.damage.BasicDamage;
            FoldArmor = weapon.damage.FoldArmor;
            FoldShield = weapon.damage.FoldShield;
            CrossValue = weapon.damage.CrossValue;
            IsShieldUseless = weapon.damage.IsShieldUseless;
            this.Fold = fold ;
            attacker = weapon.damage.attacker;
        }
        /// <summary>
        /// 一个导弹造成的伤害
        /// </summary>
        /// <param name="missile"></param>
        public Damage(MissileType missileType , Unit attacker)
        {
            this.BasicDamage = missileType.basicDamage;
            this.attacker = attacker;
            this.CrossValue = missileType.crossValue;
            this.FoldArmor = missileType.foldArmor;
            this.FoldShield = missileType.foldShield;
            this.IsShieldUseless = missileType.isShieldUseless;
            this.Fold = 1.0f;
        }
        /// <summary>
        /// 使 伤害改变武器类型
        /// </summary>
        /// <param name="weapon"></param>
        public void SetDamage(Weapon weapon)
        {
            BasicDamage = weapon.damage.BasicDamage;
            FoldArmor = weapon.damage.FoldArmor;
            FoldShield = weapon.damage.FoldShield;
            CrossValue = weapon.damage.CrossValue;
            IsShieldUseless = weapon.damage.IsShieldUseless;
        }
        /// <summary>
        /// 计算并输出一个伤害.若只对护盾造成伤害而无法对护甲造成伤害则为0，若需要对护甲按
        /// 剩余护盾比例造成伤害则介于0~1之间 默认为0.02 若m为单位护盾值与最大护盾值得比对单位的伤害计算式为：
        /// 护盾伤害=foldShield*BasicDamage*(1-crossValue)护甲伤害=foldArmor*BasicDamage*crossValue
        /// </summary>
        /// <param name="unit">伤害的单位</param>
        /// <param name="shield">对护盾的伤害</param>
        /// <param name="armor">对护甲的伤害</param>
        public void OutDamage(VioableUnit unit, ref float shield, ref float armor)
        {
           
            shield = 0.0f;
            armor = 0.0f;
            float dmg = BasicDamage * Fold;
            if (IsShieldUseless == false)
            {
                if (unit.Shield > FoldShield * dmg *  (1 - CrossValue))
                {
                    shield = FoldShield * dmg * (1 - CrossValue);
                    armor = FoldArmor * dmg * CrossValue;
                }
                else
                {
                    shield = unit.Shield;
                    if (FoldShield != 0 && CrossValue != 0 && CrossValue != 1)
                    {
                        float d = shield / FoldShield;
                        armor = (dmg - d) * FoldArmor;
                    }
                }
            }
            else
            {
                shield = 0.0f;
                armor = dmg * FoldArmor;
            }
             
        }
        public static Damage Zero
        {
            get
            {
                Damage dmg = new Damage();
                dmg.BasicDamage = 0;
                dmg.attacker = null;
                dmg.CrossValue = 0;
                dmg.Fold = 0;
                dmg.FoldArmor = 0;
                dmg.FoldShield = 0;
                dmg.IsShieldUseless = false;
                return dmg;
            }
        }
        public static Damage CreateFromDamage(Damage d,Unit attacker)
        {
            Damage dmg = d;
            dmg.attacker = attacker;
            return dmg;
        }


        
    }
}
