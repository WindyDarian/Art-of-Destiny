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
using AODGameLibrary.GamePlay;

namespace AODGameLibrary.AODObjects
{
    /// <summary>
    /// 拥有AOD模型和一些共有物理属性的物体的父类
    /// 由大地无敌-范若余2009年7月28日创建，终于体会到继承的好处了
    /// </summary>
    public class ObjectUnit
    {
        private string name ="";
        /// <summary>
        /// 名字
        /// </summary>
        public string Name 
        {
            get { return name; }
            set { name = value; }
        }
        private Vector3 position = Vector3.Zero;
        /// <summary>
        /// 位置
        /// </summary>
        public Vector3 Position
        {
            get { return position; }
            set { position = value; }
        }
        private Vector3 velocity = Vector3.Zero;
        /// <summary>
        /// 速度
        /// </summary>
        public Vector3 Velocity
        {
            get { return velocity; }
            set { velocity = value; }
        }

        private float scale = 1.0f;
        /// <summary>
        /// 缩放
        /// </summary>
        public float Scale
        {
            get { return scale; }
            set { scale = value; }
        }
        public Matrix rotation = Matrix.Identity;
        private AODModel model;
        /// <summary>
        /// 模型
        /// </summary>
        public AODModel Model
        {
            get { return model; }
            set { model = value; }
        }
        public Matrix modelRotation = Matrix.CreateRotationY(MathHelper.Pi);
        private float frictionForce;
        /// <summary>
        /// 最大摩擦力大小
        /// </summary>
        public float FrictionForce
        {
            get { return frictionForce; }
            set { frictionForce = value; }
        }
        private float mass;
        /// <summary>
        /// 质量
        /// </summary>
        public float Mass
        {
            get { return mass; }
            set { mass = value; }
        }
        private UnitState unitState;
        /// <summary>
        /// 状态
        /// </summary>
        public UnitState UnitState
        {
            get { return unitState; }
            set { unitState = value; }
        }
        private bool bounding = false;
        /// <summary>
        /// 是否计算碰撞
        /// </summary>
        public bool Bounding
        {
            get { return bounding; }
            set { bounding = value; }
        }
        private bool heavy = false;
        /// <summary>
        /// 是否无可撼动 
        /// </summary>
        public bool Heavy
        {
            get { return heavy; }
            set { heavy = value; }
        }

        private Vector3 thrust = Vector3.Zero;
        /// <summary>
        /// 该帧受到的推力
        /// </summary>
        public Vector3 Thrust
        {
            get { return thrust; }
            set { thrust = value; }
        }
        private Vector3 impulse;
        /// <summary>
        /// 该桢受到的冲量
        /// </summary>
        public Vector3 Impulse
        {
            get { return impulse; }
            set { impulse = value; }
        }
        public Matrix ModelWorld
        {
            get
            {
                Matrix world = Matrix.CreateScale(Scale)
                               * modelRotation//前两个矩阵是对模型的转换，须使正面对准(0,0,-1)
                               * ExtraRotation
                               * World;
                return world;
            }
        }
        /// <summary>
        /// 得到面朝方向的向量
        /// </summary>
        public Vector3 Face
        {
            get
            {
                return Vector3.Normalize(Vector3.TransformNormal(Vector3.Forward, World));
            }
        }
        /// <summary>
        /// 得到上方向量
        /// </summary>
        public Vector3 Up
        {
            get
            {
                return Vector3.Normalize(Vector3.TransformNormal(Vector3.Up, World));
            }
        }
        
        public Matrix ModelRotation
        {
            get
            {
                Matrix r = modelRotation
                           * ExtraRotation
                           * rotation;
                return r;
            }
        }
        public float Speed
        {
            get
            {
                return Velocity.Length();
            }
            set
            {
                if (Velocity != Vector3.Zero)
                {
                    Velocity = Vector3.Normalize(Velocity) * value;
                }
            }
        }
        public Matrix World
        {
            get
            {
                Matrix world = Rotation
                               * Matrix.CreateTranslation(Position);
                return world;
            }
        }
        public Matrix Rotation
        {
            get
            {
                return rotation;
            }
            set
            {
                rotation = value;
            }
        }
        /// <summary>
        /// 返回该单位是否死亡
        /// </summary>
        public bool Dead
        {
            get
            {
                if (UnitState != UnitState.alive)
                {
                    return true;
                }
                else return false;
            }
        }
        float extraRotationZ = 0;
        //Z轴偏航
        public float ExtraRotationZ
        {
            get { return extraRotationZ; }
            set { extraRotationZ = value; }
        }
        public Matrix ExtraRotation = Matrix.Identity;
        public virtual void Update(GameTime gameTime)
        {
            if (UnitState == UnitState.dying && Model.IsDead)
            {
                UnitState = UnitState.dead;
            }
        }
        public ObjectUnit()
        {
       
        }
        /// <summary>
        /// 施加一个推力，作用时间为一桢
        /// </summary>
        /// <param name="th"></param>
        public void GetThrust(Vector3 th)
        {
            this.thrust += th;
        }


        float addictionalFictionForce = 0;
        bool ignoreNormalFiction = false;

        /// <summary>
        /// 加额外摩擦力
        /// </summary>
        /// <param name="force"></param>
        protected void AddFictionForceFrame(float force)
        {
            addictionalFictionForce += force;
        }

        /// <summary>
        /// 设置摩擦力，并忽视内置摩擦力
        /// </summary>
        /// <param name="force"></param>
        protected void SetFictionForceFrame(float force)
        {
            addictionalFictionForce += force;
            ignoreNormalFiction = true;
        }

        /// <summary>
        /// 施加一个瞬时冲量
        /// </summary>
        /// <param name="impulse"></param>
        public void GetImpulse(Vector3 impulse)
        {

            this.impulse += impulse;
        }
        public void UpdateLocation(GameTime gameTime,Vector3 th)
        {
           
            this.thrust += th;
            UpdateLocation(gameTime);
        }
        public void UpdateLocation(GameTime gameTime)
        {
            if (UnitState != UnitState.dead)
            {
                ExtraRotation = Matrix.CreateRotationZ(MathHelper.ToRadians(extraRotationZ));
                float elapsedTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (elapsedTime != 0)
                    thrust += impulse / elapsedTime;
                if (Mass!= 0)
                {
                    #region 计算摩擦力
                    Vector3 frictionDirection = Vector3.Zero;
                    Vector3 acceleration = Vector3.Zero;
                    Vector3 friction = Vector3.Zero;
                    float totalFiction;
                    if (!ignoreNormalFiction) totalFiction = addictionalFictionForce + FrictionForce;
                    else totalFiction = addictionalFictionForce;
                    if (Velocity.Length() != 0)
                    {
                        if (Velocity.Length() * Mass <= totalFiction * elapsedTime)
                        {
                            frictionDirection = (Vector3.Normalize(Velocity)) * (-1);
                            if (elapsedTime > 0)
                                friction = (Velocity.Length() * Mass) / elapsedTime * frictionDirection;

                        }
                        else
                        {
                            frictionDirection = (Vector3.Normalize(Velocity)) * (-1);
                            friction = totalFiction * frictionDirection;

                        }

                    }
                    #endregion

                    acceleration = (thrust + friction) / Mass;//计算加速度
                    Velocity += acceleration * elapsedTime;


                    Position += Velocity * elapsedTime;
                }

                if (thrust != Vector3.Zero)
                {
                    UpdateModel(gameTime, MoveState.Moving);
                }
                else
                {
                    UpdateModel(gameTime, MoveState.NotMoving);
                }

            }
            impulse = Vector3.Zero;
            thrust = Vector3.Zero;

            addictionalFictionForce = 0;
            ignoreNormalFiction = false;
        }
        /// <summary>
        /// 超级更新
        /// </summary>
        /// <param name="gameTime"></param>
        public virtual void SUpdate()
        {

        }
        void UpdateModel(GameTime gameTime,MoveState moveState)
        {
          
                Model.Update(gameTime, Scale, ModelRotation, Position,moveState,Velocity);
            

        }
        public virtual void DrawModels(GameTime gameTime,Camera camera)
        {
            if (UnitState != UnitState.dead)
            {

                Model.DrawModels(gameTime, camera);
            }
        }
        public virtual void DrawEffects(GameTime gameTime, Camera camera)
        {
            if (UnitState != UnitState.dead)
            {

                Model.DrawEffects(gameTime, camera);
            }
        }
        public virtual void BeginToDie()
        {

            if (UnitState != UnitState.dead)
            {
                
                UnitState = UnitState.dying;
                Model.BeginToDie();
            }
        }
        /// <summary>
        /// 获得目标相对于源单位前方的夹角的COS值
        /// </summary>
        /// <param name="o">源单位</param>
        /// <param name="target">目标</param>
        /// <returns>夹角的COS值</returns>
        public static float AngleCos(ObjectUnit o, ObjectUnit target)
        {

            return Vector3.Dot(o.Face, Vector3.Normalize(target.Position - o.Position));
        }
        /// <summary>
        /// 返回两个单位的距离
        /// </summary>
        /// <param name="a">单位a</param>
        /// <param name="b">单位b</param>
        /// <returns>单位间的距离</returns>
        public static float Distance(ObjectUnit a, ObjectUnit b)
        {
            float m = 0;
            if (a.Model != null)
            {
                m += a.Model.TransformedMajorSphere.Radius;
            }
            if (b.Model != null)
            {
                m += b.Model.TransformedMajorSphere.Radius;
            }
            return MathHelper.Clamp(Vector3.Distance(a.Position, b.Position) - m, 0, float.MaxValue);
        }
        public void SetModel(string modelName,GameWorld gameWorld)
        {
            Model = new AODModel(gameWorld, gameWorld.game.Content.Load<AODModelType>(modelName), Position, ModelRotation, Scale);
        }
        public void SetRotation(Vector3 r)
        {
            
            rotation = Matrix.CreateRotationX(MathHelper.ToRadians(r.X));
            rotation = Matrix.CreateRotationY(MathHelper.ToRadians(r.Y)) * rotation;
            rotation = Matrix.CreateRotationZ(MathHelper.ToRadians(r.Z)) * rotation;
        }
        public void SetMoveState(Vector3 velocity, Vector3 rotation)
        {
            this.velocity = velocity;
            SetRotation(rotation);
        }
    }
    /// <summary>
    /// 表示单位的状态
    /// </summary>
    public enum UnitState
    {
        /// <summary>
        /// 活着
        /// </summary>
        alive,
        /// <summary>
        /// 死了
        /// </summary>
        dead,
        /// <summary>
        /// 正在死亡
        /// </summary>
        dying,//未使用
    }
}
