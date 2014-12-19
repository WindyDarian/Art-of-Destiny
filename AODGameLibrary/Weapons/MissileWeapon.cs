using System;
using System.Collections.Generic;

using System.Text;
using Microsoft.Xna.Framework;
using AODGameLibrary.Units;
using AODGameLibrary.GamePlay;

namespace AODGameLibrary.Weapons
{
    /// <summary>
    /// 表示发射导弹的武器
    /// 由大地无敌-范若余2009年7月29日创建
    /// </summary>
    public class MissileWeapon
    {
        /// <summary>
        /// 导弹数量
        /// </summary>
        public int num;
        /// <summary>
        /// 发射器类型
        /// </summary>
        public MissileWeaponType missileWeaponType;
        /// <summary>
        /// 发射的导弹类型
        /// </summary>
       public  MissileType missileType;
        /// <summary>
        /// 属于的GameWorld
        /// </summary>
        GameWorld gameWorld;
        /// <summary>
        /// 射击冷却时间剩余
        /// </summary>
        float cooldownRemaining = 0.0f;
        /// <summary>
        /// 同Weapon的position
        /// </summary>
        public int[] position = new int[2];
        int shotPositionNum = 0;
        public int Num
        {
            get
            {
                return num;
            }
            set
            {
                num = (int)MathHelper.Clamp(value, 0, missileWeaponType.maxNum);
            }
        }
        public float CooldownRemaining
        {
            get
            {
                return cooldownRemaining;
            }
            set
            {
                cooldownRemaining = MathHelper.Clamp(value, 0, missileWeaponType.cooldown);
            }
        }

        public MissileWeapon (GameWorld gameWorld,MissileWeaponType missileWeaponType)
        {
            this.missileWeaponType = missileWeaponType.Clone();
            this.missileType = gameWorld.game.Content.Load<MissileType>(missileWeaponType.missileTypeName);
            Num = missileWeaponType.maxNum;
            this.gameWorld = gameWorld;
            position[0] = missileWeaponType.positionIndex1;
            position[1] = missileWeaponType.positionIndex2;
        }
        public void Update(GameTime gameTime)
        {
            float elapsedtime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            CooldownRemaining -= elapsedtime;
        }
        public void Shot(GameTime gameTime,Unit unit,Unit target)
        {
            if (Num > 0 && cooldownRemaining <= 0)
            {
                shotPositionNum += 1;
                shotPositionNum %= position.Length;

                gameWorld.AddNewMissile(new Missile(gameWorld, unit, target, missileType, unit.GetTransformedWeaponPosition(position[shotPositionNum])));
                if (unit.EndlessBullets == false)
                {

                    Num -= 1;
                }
                CooldownRemaining = missileWeaponType.cooldown;
            }
            
        }


    }
}
