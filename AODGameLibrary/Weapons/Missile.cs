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
using AODGameLibrary.Models;
using AODGameLibrary.Gamehelpers;
using AODGameLibrary.AODObjects;
using AODGameLibrary.GamePlay;
using AODGameLibrary.CollisionChecking;


namespace AODGameLibrary.Weapons
{

    /// <summary>
    /// 表示一个导弹
    /// 由大地无敌-范若余2009年7月26日创建,此外该类的结构和以前的Unit、Weapon不同，不再从对应的xxxType中复制信息而是直接使用一个字段来引用MissileType
    /// </summary>
    public class Missile:ObjectUnit
    {
        /// <summary>
        /// 目标
        /// </summary>
        public Unit target;
        /// <summary>
        /// 所属导弹类型
        /// </summary>
        public MissileType missileType;
        /// <summary>
        /// 所属 GameWorld
        /// </summary>
        GameWorld gameWorld;
        /// <summary>
        /// 导弹发射者
        /// </summary>
        Unit attacker;
        /// <summary>
        /// 剩余存在时间
        /// </summary>
        float timeRemain;
        float TimeRemain
        {
            get
            {
                return timeRemain;
            }
            set
            {
                timeRemain = MathHelper.Clamp(value, 0, missileType.lifeTime);
            }
        }

        public Missile(GameWorld gameWorld , Unit attacker,Unit target, MissileType missileType,Vector3 position)
        {
            this.gameWorld = gameWorld;
            this.attacker = attacker;
            this.target = target;
            this.missileType = missileType;
            timeRemain = missileType.lifeTime;
            base.Position = position;
            base.Mass = missileType.mass;

            base.Model = new AODModel(gameWorld, gameWorld.game.Content.Load<AODModelType>(missileType.modelName),position,ModelRotation,Scale);//需改
            base.Scale = missileType.scale;
            base.FrictionForce = missileType.frictionForce;
            base.modelRotation = missileType.modelRotation;
            base.UnitState = UnitState.alive;

            if (target.Position != attacker.Position)
            {
                this.Velocity = attacker.Velocity + Vector3.Normalize(this.target.Position - this.attacker.Position) * this.missileType.basicSpeed;
            }
            else this.Velocity = attacker.Velocity + attacker.Face * this.missileType.basicSpeed;
     

        }
        public override void Update(GameTime gameTime)
        {
            float elapsedTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            
            Vector3 thrust = Vector3.Zero;
            if (UnitState == UnitState.alive)
            {



                TimeRemain -= elapsedTime;
                if (TimeRemain == 0)
                {
                    Explode();
                }
                else
                {
                    #region 计算推力
                    if (target.Position != Position)
                    {
                        if (missileType.maxPower > 0)
                        {

                            Vector3 thrustDirection = Vector3.Normalize(target.Position - Position);
                            if (Vector3.Dot(Velocity, thrustDirection * missileType.maxThrustForce) < missileType.maxPower)
                            {

                                thrust = thrustDirection * missileType.maxThrustForce;
                            }
                            else
                            {

                                thrust = missileType.maxPower / Vector3.Dot(Velocity, thrustDirection) * thrustDirection;
                            }

                        }
                    }
                    else thrust = Vector3.Zero;
                    #endregion
                }

            }

            base.GetThrust(thrust);
            base.Update(gameTime);

        }
        public override void SUpdate()
        {
            if (UnitState == UnitState.alive)
            {

                //if (Collision.IsCollided(target, this, 0.6f) && (attacker.Dead || Collision.IsCollided(attacker,this,0.9f)!= true))
                if (Collision.IsCollided(target, this, 0.6f))
                {
                    Explode();
                }
            }
            base.SUpdate();
        }
        void Explode()
        {
            if (UnitState == UnitState.alive)
            {

                BeginToDie();
                foreach (VioableUnit u in gameWorld.GameItemManager.BoundingCollection)
                {
                    if (u.Dead == false && u.Group != attacker.Group)
                    {
                        if (Collision.IsCollided(u, this, 1.0f))
                        {
                            u.GetDamage(new Damage(missileType, attacker));
                        }

                    }
                }
            }
        }


    }
}
