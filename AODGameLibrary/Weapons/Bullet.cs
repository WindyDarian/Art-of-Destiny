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
using AODGameLibrary.Effects;
using AODGameLibrary.AODObjects;
using AODGameLibrary.Models;
using AODGameLibrary.GamePlay;
using AODGameLibrary.CollisionChecking;

namespace AODGameLibrary.Weapons
{
    public class Bullet
    {
        #region 变量声明
    
        public Weapon weapon;
        /// <summary>
        /// 子弹的位置
        /// </summary>
        public Vector3 position;
        /// <summary>
        /// 表示子弹当前速度
        /// </summary>
        public Vector3 velocity;
        /// <summary>
        /// 原位置
        /// </summary>
        private Vector3 position0;
        /// <summary>
        /// 旋转矩阵
        /// </summary>
        public Matrix rotation;
        /// <summary>
        /// 表示弹药声明是否已结束
        /// </summary>
        public bool bulletOver = false;
        /// <summary>
        /// 这个弹药串中子弹的数量
        /// </summary>
        public float num;
        /// <summary>
        /// 上次超级更新时子弹的位置
        /// </summary>
        public Vector3 positionl;
        /// <summary>
        /// 单位所属的阵营，1~8
        /// </summary>
        public int Group;
        /// <summary>
        /// 弹药模型
        /// </summary>
        public AODModel bulletModel;
        ///// <summary>
        ///// 只有当弹药和单位中心在这个距离内才进行碰撞检测
        ///// </summary>
        //public const float CheckRange = 1000.0f;
        /// <summary>
        /// 根据初速度获得的距离奖励
        /// </summary>
        public float bonusRange;
        public float Range
        {
            get
            {
                return weapon.range + bonusRange;
            }
        }
        #endregion

        public bool IsDead
        {
            get
            {
                if (bulletOver)
                {
                    if (bulletModel != null)
                    {

                        return bulletModel.IsDead;
                    }
                    else return true;
                }
                else return false;
            }
        }
        public Vector3 Face
        {
            get
            {
                if (velocity != Vector3.Zero)
                {
                    return Vector3.Normalize(velocity);
                }
                else return Vector3.Forward;
            }
        }
        GameWorld gameWorld;
      
        /// <summary>
        /// 创建一个飞行的实体弹药
        /// </summary>
        /// <param name="position">位置</param>
        /// <param name="velocity">发射者初速度</param>
        /// <param name="rotation">表示子弹旋转方向的矩阵</param>
        /// <param name="weapon">子弹所属武器类型</param>
        /// <param name="number">这个弹药串中子弹的数量</param>
        public Bullet(GameWorld gameWorld,Weapon weapon,float num,Vector3 position)
        {
            this.gameWorld = gameWorld;
            this.weapon = weapon;
            this.position0 = position;
            this.position = position;
            this.positionl = position;
            this.rotation = weapon.unit.Rotation;
            this.num = num;
            Random random = new Random();
            float angel = ((float)random.NextDouble()) * weapon.maxAngle;
            float rZ = ((float)random.NextDouble()) * MathHelper.Pi;
            
            this.velocity = weapon.unit.Velocity + Vector3.TransformNormal(Vector3.Forward
                , Matrix.CreateRotationY(MathHelper.ToRadians(angel)) * Matrix.CreateRotationZ(rZ) 
                                                                      * weapon.unit.Rotation) 
                                                                      * weapon.basicSpeed;
            this.Group =weapon.unit.Group;
            this.bonusRange = weapon.range / weapon.basicSpeed * weapon.unit.Velocity.Length();
            if (weapon.bulletEffectType != null)
            {

                this.bulletModel = new AODModel(gameWorld, weapon.bulletEffectType, position, Matrix.Identity, 1.0f);//需改
                bulletModel.NormalEffectsBeginToDie();
            }
            
        }
        /// <summary>
        /// 执行更新
        /// </summary>
        /// <param name="gameTime">请赋予一个GameTime</param>
        public void Update(GameTime gameTime)
        {
            
            if (bulletOver == false)
            {
                float elapsedTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
                if ((position - position0).Length() <= Range)
                {
                    position += velocity * elapsedTime;
                }
                else
                {
                    Disappear();
                }
            }

            if(bulletModel!= null)
            bulletModel.Update(gameTime, 1, Matrix.Identity, position, MoveState.Moving, velocity);

            
        }
        public void SUpdate()
        {
            if (bulletOver == false)
            {

                UpdatePositionL();
            }
        }
        /// <summary>
        /// 更新上一次碰撞检测时的位置
        /// </summary>
        /// <param name="gameTime"></param>
        public void UpdatePositionL()
        {
            if (bulletOver == false)
            {
                positionl = position;
            }
        }
        public void Draw(GameTime gameTime, Camera camera)
        {
            //if (this.bulletOver == false)
            //{
            //    /*
            //    // Copy any parent transforms.
            //    Matrix[] transforms = new Matrix[weapon.bulletModel.Bones.Count];
            //    weapon.bulletModel.CopyAbsoluteBoneTransformsTo(transforms);

            //    foreach (ModelMesh mesh in weapon.bulletModel.Meshes)
            //    {
            //        // This is where the mesh orientation is set, as well as our camera and projection.
            //        foreach (BasicEffect effect in mesh.Effects)
            //        {
            //            effect.EnableDefaultLighting();
            //            effect.World = transforms[mesh.ParentBone.Index]
            //                * Matrix.CreateScale(weapon.bulletScale)
            //                * Matrix.CreateRotationY(MathHelper.ToRadians(180.0f))
            //                * rotation
            //                * Matrix.CreateTranslation(position);
            //            effect.View = camera.view;
            //            effect.Projection = camera.projection;
            //        }
            //        // Draw the mesh, using the effects set above.
            //        mesh.Draw();
                 
            //        weapon .bulletModel .Draw (gametime ,camera );
            //    }
            //     */
                
            //}
            if (IsDead != true)
            {
                if (bulletModel!= null)
                {
                    
                bulletModel.DrawEffects(gameTime, camera);
                }
            }
                  
        }
        /// <summary>
        /// 将弹药和附近的单位单位做碰撞检测，对是敌方且发生碰撞的最近单位造成伤害且弹药消失
        /// </summary>
        /// <param name="unit">做碰撞检测的目标</param>
        public void CheckCollision(List<VioableUnit> units)
        {
            VioableUnit hitingUnit = null;
            float hitingUnitRange = 0;
            Vector3 hittingPoint = Vector3.Zero;
            Vector3? v1;
            foreach (VioableUnit unit in units)
            {
                if (unit != null)
                {

                    if (Vector3.Distance(unit.Position, this.position) <= GameConsts.BulletCheckingRange)
                    {

                        //if (weapon.isInstant == false)
                        //{

                        //    if (bulletOver == false)
                        //    {

                        //        if (Collision.IsCollided(unit, this) && unit.Group != Group && unit.Dead != true)
                        //        {
                        //            Damage result;
                        //            result = new Damage(weapon, num);
                        //            unit.GetDamage(result);
                        //            Hit();
                        //        }

                        //    }


                        //}
                        //else{}
                        v1 = Collision.IsCollided(unit, this);
                        if (v1 != null && unit.Group != Group && unit.UnitState != UnitState.dead)
                        {
                            if (hitingUnit != null)
                            {
                                float k = Vector3.Dot(v1.Value - this.positionl, this.Face);
                                if (k < hitingUnitRange)
                                {
                                    hitingUnitRange = k;
                                    hitingUnit = unit;
                                    hittingPoint = v1.Value;
                                }
                            }
                            else
                            {
                                hitingUnit = unit;
                                hitingUnitRange = Vector3.Dot(unit.Position - this.positionl, this.Face);
                                hittingPoint = v1.Value;
                            }


                        }

                    }
                }

            }
            if (hitingUnit != null)
            {

                Damage result;
                result = new Damage(weapon, num);
                hitingUnit.GetDamage(result);
                //position = (positionl + position) / 2;
                if (hitingUnit.Model != null && position0 != position)
                {
                    Ray br = new Ray(position0, Vector3.Normalize(position - position0));
                    Vector3? v = hittingPoint;
                    if (v!= null )
                    {

                        position = v.Value;
                        positionl = v.Value;
                    }
                    
                }
                Hit();

            }
        }
        void Disappear()
        {
            if (bulletModel!= null)
            {
                
            bulletModel.BeginToDieWithoutDeathEffect();
            }
            bulletOver = true;
        }
        void Hit()
        {
            if (bulletModel != null)
            bulletModel.BeginToDie();
            bulletOver = true;
        }

    }
}
