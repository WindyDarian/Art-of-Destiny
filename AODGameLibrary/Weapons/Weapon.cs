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
using AODGameLibrary.Cameras;
using AODGameLibrary.Units;
using AODGameLibrary.Effects;
using AODGameLibrary.AODObjects;
using AODGameLibrary.Models;
using AODGameLibrary.GamePlay;

namespace AODGameLibrary.Weapons
{
    /// <summary>
    /// 表示一种武器
    /// </summary>
    public class Weapon
    {
        #region 声明字段
        /// <summary>
        /// 武器名
        /// </summary>
        public string name;
        /// <summary>
        /// 弹药名
        /// </summary>
        public string ammoName;
        /// <summary>
        /// 最大弹药数量
        /// </summary>
        public float maxAmmo;
        /// <summary>
        /// 弹药数量
        /// </summary>
        private float ammoNum;

        public float AmmoNum
        {
            get { return ammoNum; }
            set { ammoNum = MathHelper.Clamp(value, 0, maxAmmo); }
        }
        /// <summary>
        /// 表示弹药的伤害
        /// </summary>
        public Damage damage;
        /// 弹药类型
        /// </summary>
        public WeaponType weaponType;
        /// <summary>
        /// 每秒射弹量
        /// </summary>
        public float shotSpeed;
        /// <summary>
        /// 是否快速射击武器,若是可以按住鼠标左键连发
        /// </summary>
        public bool isFastWeapon;
        /// <summary>
        /// 射击间隔,0.025的倍数最佳，否则容易出现一些问题
        /// </summary>
        public float shotSpan;
        /// <summary>
        /// 冷却时间
        /// </summary>
        float timeSinceLastShot;
        /// <summary>
        /// 相对于射出弹药的Unit的弹体初速,只有在Type为Normal时有效
        /// </summary>
        public float basicSpeed;
        /// <summary>
        /// 射程
        /// </summary>
        public float range;
        

       
        /// <summary>
        /// 弹药模型(使用粒子效果)
        /// </summary>
        public AODModelType bulletEffectType;
        
        /// <summary>
        /// 弹药模型缩放
        /// </summary>
        public float bulletScale;
        /// <summary>
        /// 最大偏角
        /// </summary>
        public float maxAngle;
        Game game;
        GameWorld gameWorld;
        /// <summary>
        /// 是否忽略子弹飞行，如果为false则在发射的瞬间进行碰撞检测，飞行过程无效果如果是快速连发的武器关闭需谨慎
        /// </summary>
        public bool isInstant = true;
        /// <summary>
        /// 相对于父单位的位置(单位世界转换矩阵前)可以有两个，每次射击时取随机位置,注意若武器只在一个位置则position[1]和position[0]相同
        /// </summary>
        public int[] position = new int[2];
        /// <summary>
        /// 属于的单位
        /// </summary>
        public Unit unit;
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
        /// <summary>
        ///  是否一次只发射一枚弹药，若为true则shotSpeed无效化
        /// </summary>
        public bool singleShotWeapon;
        /// <summary>
        /// 武器的AssetName（不含目录）
        /// </summary>
        public string assetName;
        /// <summary>
        /// 是否正在最佳状态冷却
        /// </summary>
        bool superShotCooldownUpdating = true;
        /// <summary>
        /// 已经经过的冷却时间
        /// </summary>
        float superShotCooldowntimelast = 0.0f;
        float shotTime = 0.0f;
        /// <summary>
        /// 该轮射击中是否有最佳射击状态
        /// </summary>
        bool superShoting = false;
        /// <summary>
        /// 武器位置
        /// </summary>
        Vector3[] pst = new Vector3[2];


        float soundplayspan = 0.2f;
        float soundspanleft = 0.2f;

        void ResetSuperShotCooldown()
        {
            superShotCooldownUpdating = true;
            superShotCooldowntimelast = 0.0f;
            superShoting = false;
            shotTime = 0.0f;
        }
        /// <summary>
        /// 射击效果
        /// </summary>
        public ParticleEffect[] shotEffect = new ParticleEffect[2];
        /// <summary>
        /// 最佳状态射击效果
        /// </summary>
        public ParticleEffect[] superShotEffect = new ParticleEffect[2];
        public SoundEffect shotSound;
        public SoundEffectInstance shotSoundInstance;
        int shotPositionNum = 0;
       
        #endregion

        #region 构造函数
        public Weapon(GameWorld gameWorld,WeaponType weaponType,Unit unit)
        {
            this.unit = unit;
            LoadType(gameWorld, weaponType);
            ResetEffects();
        }
        void LoadType(GameWorld gameWorld, WeaponType weaponType)
        {
            this.singleShotWeapon = weaponType.singleShotWeapon;
            this.damage = new Damage();
            this.game = gameWorld.game;
            this.gameWorld = gameWorld;
            this.name = weaponType.weaponName;
            this.ammoName = weaponType.ammoName;
            this.maxAmmo = weaponType.maxAmmo;
            this.AmmoNum = this.maxAmmo;
            this.damage.BasicDamage = weaponType.basicDamage;
            this.damage.FoldArmor = weaponType.foldArmor;
            this.damage.FoldShield = weaponType.foldShield;
            this.damage.CrossValue = weaponType.crossValue;
            this.damage.IsShieldUseless = weaponType.isShieldUseless;
            this.damage.attacker = unit;
            this.shotSpeed = weaponType.shotSpeed;
            this.basicSpeed = weaponType.basicSpeed;
            this.range = weaponType.range;
            this.maxAngle = weaponType.maxAngle;
            this.isInstant = weaponType.isInstant;
            if (weaponType.shotSound != "")
            {
                shotSound = gameWorld.Content.Load<SoundEffect>(weaponType.shotSound);
                shotSoundInstance = shotSound.CreateInstance();
            }
          
            if (weaponType.bulletModelName != "")
                this.bulletEffectType = gameWorld.game.Content.Load<AODModelType>(weaponType.bulletModelName);
            else this.bulletEffectType = null;
            this.shotSpan = weaponType.shotSpan;
            this.isFastWeapon = weaponType.isFastWeapon;
            this.supershotModeEnabled = weaponType.supershotModeEnabled;
            this.supershotTime = weaponType.supershotTime;
            this.supershotEnterTime = weaponType.supershotEnterTime;
            this.supershotCooldown = weaponType.supershotCooldown;
            if (weaponType.shotEffectName != "")
            {
                this.shotEffect[0] = new ParticleEffect(gameWorld, gameWorld.game.Content.Load<ParticleEffectType>(weaponType.shotEffectName));
                this.shotEffect[1] = new ParticleEffect(gameWorld, gameWorld.game.Content.Load<ParticleEffectType>(weaponType.shotEffectName));
               
                foreach (ParticleEffect pe in shotEffect)
                {
                    if (pe != null)
                    {
                        pe.BeginToDie();
                    }
                }

            }
            if (weaponType.supershotEffectName != "")
            {
                this.superShotEffect[0] = new ParticleEffect(gameWorld, gameWorld.game.Content.Load<ParticleEffectType>(weaponType.supershotEffectName));
                this.superShotEffect[1] = new ParticleEffect(gameWorld, gameWorld.game.Content.Load<ParticleEffectType>(weaponType.supershotEffectName));
                foreach (ParticleEffect pe in superShotEffect)
                {
                    if (pe != null)
                    {
                        pe.BeginToDie();
                    }
                }
            }
            position[0] = weaponType.positionIndex1;
            position[1] = weaponType.positionIndex2;
            this.assetName = weaponType.AssetName;
            //if (weaponType.bulletModelName != "")
            //    this.bulletModel = new ParticleEffect(game, game.Content.Load<ParticleEffectType>(weaponType.bulletModelName));
            //else this.bulletModel = null;
        }
        #endregion

        public void Update(GameTime gameTime)
        {
            float elapsedTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            timeSinceLastShot = MathHelper.Clamp(timeSinceLastShot + elapsedTime, 0, shotSpan * 1.59f);
            if (timeSinceLastShot >= shotSpan * 1.58f)
            {
                if (shotSoundInstance != null)
                {
                    shotSoundInstance.Stop();
                }
            }
            soundspanleft = MathHelper.Clamp(soundspanleft - elapsedTime, 0, soundplayspan);
            if (supershotModeEnabled && isFastWeapon == true && singleShotWeapon == false)
            {
                if (timeSinceLastShot >= shotSpan * 1.58f)
                {

                    shotTime = 0.0f;
                    if (superShotCooldownUpdating == false)
                    {

                        ResetSuperShotCooldown();

                    }
                    else
                    {
                        superShotCooldowntimelast = MathHelper.Clamp(superShotCooldowntimelast + elapsedTime, 0, supershotCooldown);
                        if (superShotCooldowntimelast >= supershotCooldown)
                        {
                            superShoting = true;
                        }
                    }

                }
                else
                {
                    shotTime += elapsedTime;
                }
            }

            pst[0] = unit.GetTransformedWeaponPosition(position[0]);
            pst[1] = unit.GetTransformedWeaponPosition(position[1]);
            UpdateEffects(gameTime);

        }

        #region 射击
        public void Shot(GameTime gameTime)
        {

            float num;
            if (singleShotWeapon)
            {
                num = 1;
            }
            else
            {
                num = shotSpeed * shotSpan;
            }
            if (AmmoNum<= num)
            {
                num = AmmoNum;   
            }
            if (timeSinceLastShot >= shotSpan && num > 0)
            {

                superShotCooldownUpdating = false;
                shotPositionNum += 1;
                shotPositionNum %= 2;
                if (shotEffect[shotPositionNum] != null)
                {
                    this.shotEffect[shotPositionNum].ReBirth();
                }

                if (superShoting)
                {
                    if (shotTime >= supershotEnterTime && shotTime <= supershotEnterTime + supershotTime && num < AmmoNum)
                    {
                        //CreateBullet(num / 2, unit.WeaponPosition(position[0]));
                        //CreateBullet(num / 2, unit.WeaponPosition(position[1]));
                        //CreateBullet(num / 2, unit.WeaponPosition(position[0]));
                        //CreateBullet(num / 2, unit.WeaponPosition(position[1]));
                        CreateBullet(num, unit.GetTransformedWeaponPosition(position[0]));
                        CreateBullet(num, unit.GetTransformedWeaponPosition(position[1]));
               
                        num *= 1.5f;
                        if (shotEffect[shotPositionNum] != null)
                        {
                            this.superShotEffect[shotPositionNum].ReBirth();
                        }
                    }
                    else
                    {
                        CreateBullet(num, pst[shotPositionNum]);
                    }
                }
                else
                {
                    CreateBullet(num, pst[shotPositionNum]);
             
                }
                foreach (ParticleEffect pe in shotEffect)
                {
                    if (pe!= null )
                    {
                        pe.BeginToDie();
                    }
                }
                foreach (ParticleEffect pe in superShotEffect)
                {
                    if (pe != null)
                    {
                        pe.BeginToDie();
                    }
                }
                if (unit.EndlessBullets == false)
                {

                    AmmoNum -= num;
                }
                timeSinceLastShot -= shotSpan;
       
                
            }
      
            
        }
        public void UpdateEffects(GameTime gameTime)
        {
       
            if (shotEffect[0] != null)
            {
               
                shotEffect[0].Update(gameTime, unit.Velocity);
            }
            if (shotEffect[1] != null)
            {
              
                shotEffect[1].Update(gameTime, unit.Velocity);
            }

            if (superShotEffect[0] != null)
            {
              
                superShotEffect[0].Update(gameTime, unit.Velocity);
            }
            if (superShotEffect[1] != null)
            {
           
                superShotEffect[1].Update(gameTime, unit.Velocity);
            }
        }
        public void ResetEffects()
        {
            
            if (shotEffect[0] != null)
            {
                this.shotEffect[0].position = unit.GetWeaponPosition(position[0]);
                shotEffect[0].parentModel = unit.Model;
            }
            if (shotEffect[1] != null)
            {
                this.shotEffect[1].position = unit.GetWeaponPosition(position[1]);
                shotEffect[1].parentModel = unit.Model;
            }

            if (superShotEffect[0] != null)
            {
                this.superShotEffect[0].position = unit.GetWeaponPosition(position[0]);
                superShotEffect[0].parentModel = unit.Model;
            }
            if (superShotEffect[1] != null)
            {
                this.superShotEffect[1].position = unit.GetWeaponPosition(position[1]);
                superShotEffect[1].parentModel = unit.Model;
            }
        }
        public void DrawEffects(GameTime gameTime,Camera camera)
        {
          
            foreach (ParticleEffect pe in shotEffect)
            {
                if (pe != null)
                {
                    pe.Draw(gameTime, camera);
                }
            }
            foreach (ParticleEffect pe in superShotEffect)
            {
                if (pe != null)
                {
                    pe.Draw(gameTime, camera);
                }
            }
        }
        /// <summary>
        /// 创建新弹药
        /// </summary>
        void CreateBullet(float num ,Vector3 position)
        {
            Bullet result = new Bullet(gameWorld, this, num,position );
            
            if (isInstant)
            {

                result.CheckCollision(gameWorld.GameItemManager.BoundingCollection);
                   
            }
            gameWorld.AddNewBullet(result);
            if (/*soundspanleft <= 0 &&*/ shotSound!= null)
            {
                soundspanleft = soundplayspan;
                gameWorld.Play3DSound(shotSoundInstance, position );
            }
        }
        #endregion 




    }
}
