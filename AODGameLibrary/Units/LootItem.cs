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
using AODGameLibrary.Weapons;
using AODGameLibrary;
using AODGameLibrary.Cameras;
using AODGameLibrary.Models;
using AODGameLibrary.AIs;
using AODGameLibrary.Gamehelpers;
using AODGameLibrary.AODObjects;
using AODGameLibrary.GamePlay;
using AODGameLibrary.CollisionChecking;

namespace AODGameLibrary.Units
{
    public class LootItem:ObjectUnit
    {
        private GameWorld gameWorld;
        private LootSettings lootSettings;
        public LootSettings LootSettings
        {
            get { return lootSettings; }
            set { lootSettings = value; }
        }
        private int bulletNum;
        public int BulletNum
        {
            get { return bulletNum; }
            set { bulletNum = value; }
        }
        public LootItem(Vector3 position, LootSettings settings, GameWorld gameWorld)
        {
            
            this.Position = position;
            this.modelRotation = Matrix.CreateRotationX((float)AODGameLibrary.Helpers.RandomHelper.Random.NextDouble() * MathHelper.Pi) * Matrix.CreateRotationY((float)AODGameLibrary.Helpers.RandomHelper.Random.NextDouble() * MathHelper.Pi);
            this.Scale = settings.ModelScale; 
            this.lootSettings = settings;

            this.gameWorld = gameWorld;
            if (settings.ModelName!= "" )
            {
                Model = new AODModel(gameWorld, gameWorld.Content.Load<AODModelType>(settings.ModelName), position, modelRotation, Scale);
            }
            
            BulletNum = (int)MathHelper.Lerp(lootSettings.MinBulletNum, lootSettings.MaxBulletNum, (float)AODGameLibrary.Helpers.RandomHelper.Random.NextDouble());
        }
        public void Loot(Unit u)
        {
            Loot(u, this);
        }
        public bool IsLootable(Unit u)
        {
            return IsLootable(u, this);
        }
        public bool IsAutolootable(Unit u)
        {
            return IsAutolootable(u, this);
        }

        public override void SUpdate()
        {
            foreach (Unit u in gameWorld.units)
            {
                if (u!= null)
                {
                    if (u.IsLootAble && u.Dead == false && this.Dead == false)
                    {
                        BoundingSphere s = new BoundingSphere(this.Position, lootSettings.LootRadius + u.Speed / 2);
                        if (Collision.IsCollided(u, s))
                        {

                            if (this.IsAutolootable(u))
                            {
                                Loot(u);
                            }
                            else if (this.IsLootable(u))
                            {
                                u.lootingItem = this;
                            }
                        }
                    }
                }
            }
            base.SUpdate();
        }
        /// <summary>
        /// 是否可拾取
        /// </summary>
        /// <param name="u"></param>
        /// <param name="loot"></param>
        /// <returns></returns>
        public static bool IsLootable(Unit u, LootItem loot)
        {
            if (loot.Dead != true && u.Dead != true && u.IsLootAble == true)
            {
         
                switch (loot.LootSettings.LootType)
                {
                    case LootType.Weapon:
                        {
                            Weapon w = u.WeaponFromAssetName(loot.LootSettings.ObjectName);
                            if (w != null)
                            {
                                if (w.AmmoNum < w.maxAmmo)
                                {
                                    return true;
                                }
                            }
                            else if (loot.lootSettings.IsWeapon)
                            {
                                return true;
                            }
                        }
                        break;
                    case LootType.MissileWeapon:
                        {
                            MissileWeapon w = u.MissileWeaponFromAssetName(loot.LootSettings.ObjectName);
                            if (w != null)
                            {
                                if (w.Num < w.missileWeaponType.maxNum)
                                {
                                    return true;
                                }
                            }
                            else if (loot.lootSettings.IsWeapon)
                            {
                               

                                    return true;
                                
                            }

                        }
                        break;
                    case LootType.SkillItem:
                        Skill s = u.SkillFromAssetName(loot.lootSettings.ObjectName);
                        
                            if (s == null)
                            {
                                return true;
                            }

                        
                        
                        break;
                    case LootType.QuestItem:
                        return true;
                        break;
                    default:
                        break;
                }
            }
            return false;
        }
        /// <summary>
        /// 返回该物体对目标单位来说是否可以自动拾取(即拾取后不会丢掉原来的东西)
        /// </summary>
        /// <param name="u"></param>
        public static bool IsAutolootable(Unit u ,LootItem loot)
        {
            if (loot.Dead != true && u.Dead != true && u.IsLootAble == true)
            {
                
                switch (loot.LootSettings.LootType)
                {
                    case LootType.Weapon:
                        {
                            Weapon w = u.WeaponFromAssetName(loot.LootSettings.ObjectName);
                            if (w != null)
                            {
                                if (w.AmmoNum < w.maxAmmo)
                                {
                                    return true;
                                }
                            }
                            else if (loot.lootSettings.IsWeapon)
                            {
                                if (u.weapons.Count < u.maxWeaponNum)
                                {

                                    return true;
                                }
                            }
                        }
                        break;
                    case LootType.MissileWeapon:
                        {
                            MissileWeapon w = u.MissileWeaponFromAssetName(loot.LootSettings.ObjectName);
                            if (w != null)
                            {
                                if (w.Num < w.missileWeaponType.maxNum)
                                {
                                    return true;
                                }
                            }
                            else if (loot.lootSettings.IsWeapon)
                            {
                                if (u.missiles.Count < u.maxMissileWeaponNum)
                                {

                                    return true;
                                }
                            }

                        }
                        break;
                    case LootType.SkillItem:
                        if (u.skills.Count < u.maxSkillNum)
                        {

                            return true;
                        }
                        break;
                    case LootType.QuestItem:
                        return true;
                        break;
                    default:
                        break;
                }
            }
            return false;
        }
        public static void Loot(Unit u, LootItem loot)
        {
            if (loot.Dead != true && u.Dead != true && u.IsLootAble == true)
            {
              
                switch (loot.LootSettings.LootType)
                {
                    case LootType.Weapon:
                        {
                            Weapon w = u.WeaponFromAssetName(loot.LootSettings.ObjectName);
                            if (w != null)
                            {
                                if (w.AmmoNum < w.maxAmmo)
                                {
                                    w.AmmoNum += loot.BulletNum;
                                    loot.BeginToDie();
                                    loot.gameWorld.ShowLoot(u, loot);
                                }
                            }
                            else if (loot.lootSettings.IsWeapon)
                            {
                                u.GetWeapon(loot.LootSettings.ObjectName, loot.bulletNum);
                                loot.BeginToDie();
                                loot.gameWorld.ShowLoot(u, loot);
                                loot.gameWorld.ShowLootWeapon(u, loot);
                            }
                        }
                        break;
                    case LootType.MissileWeapon:
                        {
                            MissileWeapon w = u.MissileWeaponFromAssetName(loot.LootSettings.ObjectName);
                            if (w != null)
                            {
                                if (w.Num<w.missileWeaponType.maxNum)
                                {
                                    w.Num += (int)loot.BulletNum;
                                    loot.BeginToDie();
                                    loot.gameWorld.ShowLoot(u, loot);
                                }
                            }
                            else if (loot .lootSettings.IsWeapon)
                            {
                                u.GetMissileWeapon(loot.LootSettings.ObjectName, loot.bulletNum);
                                loot.BeginToDie();
                                loot.gameWorld.ShowLoot(u, loot);
                                loot.gameWorld.ShowLootWeapon(u, loot);
                            }
                            
                        }
                        break;
                    case LootType.SkillItem:
                        {
                            Skill s = u.SkillFromAssetName(loot.lootSettings.ObjectName);
                            
                                if (s == null)
                                {
                                    if (u.skills.Count < u.maxSkillNum)
                                    {

                                        u.AddSkill(loot.LootSettings.ObjectName);
                                        loot.BeginToDie();
                                        loot.gameWorld.ShowLoot(u, loot);
                                    }
                                    else
                                    {
                                        u.Interrupt();
                                        if (u.CurrentSkill != null)
                                        {

                                            u.skills.Remove(u.CurrentSkill);
                                        }
                                        u.AddSkill(loot.LootSettings.ObjectName);
                                        loot.BeginToDie();
                                        loot.gameWorld.ShowLoot(u, loot);
                                    }
                                }
                
                            
                        }
                        break;
                    case LootType.QuestItem:
                        break;
                    default:
                        break;
                }

            }
            
        }
        
    }

}
