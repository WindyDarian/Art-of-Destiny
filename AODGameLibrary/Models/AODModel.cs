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
using AODGameLibrary.Effects;
using AODGameLibrary.Cameras;
using AODGameLibrary.GamePlay;

namespace AODGameLibrary.Models
{
    /// <summary>
    /// 大地无敌发明的AOD复合模型，最后注释2009年7月17日BY大地无敌-范若余
    /// </summary>
    public class AODModel
    {
        /// <summary>
        /// 模型列表
        /// </summary>
        public List<DModel> models = new List<DModel>(2);
        /// <summary>
        /// 普通状态下粒子效果
        /// </summary>
        public List<ParticleEffect> effects = new List<ParticleEffect> (2);
        /// <summary>
        /// 只在死亡时播放的效果
        /// </summary>
        public List<ParticleEffect> deathEffects = new List<ParticleEffect>(2);
        /// <summary>
        /// 只在加力移动时播放的粒子效果
        /// </summary>
        public List<ParticleEffect> movementEffects = new List<ParticleEffect>(2);
        /// <summary>
        /// 出生粒子效果
        /// </summary>
        public List<ParticleEffect> birthEffects = new List<ParticleEffect>(2);
        /// <summary>
        /// 位置
        /// </summary>
        public Vector3 position = Vector3.Zero;
        /// <summary>
        /// 缩放
        /// </summary>
        public float scale = 1.0f;
        /// <summary>
        /// 旋转
        /// </summary>
        public Matrix rotation = Matrix.Identity;
        /// <summary>
        /// 仅供绘出粒子效果时参考
        /// </summary>
        public Vector3 velocity = Vector3.Zero;

        public List<Vector3> WeaponPositions
        {
            get { return modelType.WeaponPositions; }
        }

        public List<Vector3> SkillPositions
        {
            get { return modelType.SkillPositions; }
        }
        public SoundEffectInstance birthsound;
        public SoundEffectInstance deathsound;
        public GameWorld gameWorld;
        private AODModelType modelType;
        /// <summary>
        /// 模型类型
        /// </summary>
        public AODModelType ModelType
        {
            get { return modelType; }
            set { modelType = value; }
        }
        ModelUsage modelUsage = ModelUsage.Normal;
        
        private BoundingSphere majorSphere;
        /// <summary>
        /// 粗略判断碰撞时的碰撞球
        /// </summary>
        public BoundingSphere MajorSphere
        {
            get { return majorSphere; }
            set { majorSphere = value; }
        }
        private BoundingSphere transformedMajorSphere;
        public BoundingSphere TransformedMajorSphere
        {
            get { return transformedMajorSphere; }
        }
        private List<BoundingSphere> boundingSpheres = new List<BoundingSphere>(25);
        /// <summary>
        /// 详细判断碰撞检测时的碰撞球
        /// </summary>
        public List<BoundingSphere> BoundingSpheres
        {
            get { return boundingSpheres; }
        }
        private List<BoundingSphere> transformedBoundingSpheres = new List<BoundingSphere>(25);
        /// <summary>
        /// 世界空间内的BoundingSpheres
        /// </summary>
        public List<BoundingSphere> TransformedBoundingSpheres
        {
            get { return transformedBoundingSpheres; }
        }

        /// <summary>
        /// 一般碰撞球半径
        /// </summary>
        public float ExtraBoundingSphereRadius = 0;
        /// <summary>
        /// 是否附加模型碰撞球
        /// </summary>
        public bool ModelCollision = true;
        /// <summary>
        /// 碰撞检测等级，0为只和粗略碰撞球进行球形碰撞检测，1要和
        /// 每个部分的碰撞球进行检测，2要进行OBB碰撞检测.....
        /// </summary>
        public int CollisionCheckLevel = 0;


        #region BOX暂时不支持
        private List<BoundingBox> boundingBoxes = new List<BoundingBox>(20);
        /// <summary>
        /// 详细判断碰撞检测时的碰撞方块
        /// </summary>
        public List<BoundingBox> BoundingBoxes
        {
            get { return boundingBoxes; }
        }
        private List<BoundingBox> transformedBoundingBoxes = new List<BoundingBox>(25);
        /// <summary>
        /// 世界空间内的BoundingBoxes
        /// </summary>
        public List<BoundingBox> TransformedBoundingBoxes
        {
            get { return transformedBoundingBoxes; }
        }
        #endregion
        public bool isDying = false;
        bool isDeathEffectVisable = false;
        bool isDead = false;
        public bool IsDead
        {
            get { return isDead; }
            set { isDead = value; }
        }

        /// <summary>
        /// 获得该模型的世界矩阵
        /// </summary>
        public Matrix World
        {
            get
            {
                Matrix mt1 = Matrix.CreateScale(scale);
                Matrix mt2 = rotation;
                Matrix mt3 = Matrix.CreateTranslation(position);
                return mt1 * mt2 * mt3;
            }
        }
        /// <summary>
        /// 获得旋转*平移矩阵
        /// </summary>
        public Matrix RotationTranslation
        {
            get
            {
               
                Matrix mt2 = rotation;
                Matrix mt3 = Matrix.CreateTranslation(position);
                return mt2 * mt3;
            }
        }
        public Matrix ScaleMatrix
        {
            get
            {
                return Matrix.CreateScale(scale);
            }
        }
        /// <summary>
        /// 新建一个AOD模型
        /// </summary>
        /// <param name="game">所属的Game</param>
        /// <param name="modelType">AOD模型类型</param>
        public AODModel(GameWorld gameWorld, AODModelType modelType, Vector3 position, Matrix rotation, float scale)
        {
            this.scale = scale;
            this.rotation = rotation;
            this.position = position;
            effects = new List<ParticleEffect>(2);
            deathEffects = new List<ParticleEffect>(2);
            movementEffects = new List<ParticleEffect>(2);
            LoadType(gameWorld, modelType);
            foreach (ParticleEffect e in birthEffects)
            {
                if (e != null)
                {
                    e.BeginToDie();
                }
            }
            if (birthsound!= null )
            {

                gameWorld.Play3DSound(birthsound, position);
            }
            ResetBounding();
            
        }
        /// <summary>
        /// 新建一个AOD模型
        /// </summary>
        /// <param name="game">所属的Game</param>
        /// <param name="modelType">AOD模型类型</param>
        public AODModel(GameWorld gameWorld, AODModelType modelType, Vector3 position, Matrix rotation, float scale,ModelUsage usage)
        {
            this.scale = scale;
            this.rotation = rotation;
            this.position = position;
            this.modelUsage = usage;
            effects = new List<ParticleEffect>(2);
            deathEffects = new List<ParticleEffect>(2);
            movementEffects = new List<ParticleEffect>(2);
            LoadType(gameWorld, modelType);
            foreach (ParticleEffect e in birthEffects)
            {
                if (e != null)
                {
                    e.BeginToDie();
                }
            }
            if (birthsound != null)
            {

                gameWorld.Play3DSound(birthsound, position);
            }
            ResetBounding();

        }
        /// <summary>
        /// 重置碰撞体
        /// </summary>
        public void ResetBounding()
        {
            if (modelUsage == ModelUsage.Effect)
            {
                return;
            }
            this.boundingSpheres.Clear();
            this.boundingBoxes.Clear();
            transformedBoundingBoxes.Clear();
            transformedBoundingSpheres.Clear();
            this.majorSphere = new BoundingSphere();
            this.transformedMajorSphere = new BoundingSphere();

            if (modelType.ModelCollision == true)
            {
                foreach (DModel dModel in this.models)
                {
                    if (dModel != null)
                    {

                        Matrix[] transforms = new Matrix[dModel.model.Bones.Count];
                        dModel.model.CopyAbsoluteBoneTransformsTo(transforms);
                        foreach (ModelMesh mesh in dModel.model.Meshes)
                        {


                            BoundingSphere bS = mesh.BoundingSphere.Transform(transforms[mesh.ParentBone.Index]
                            * dModel.RelativeWorld);

                            boundingSpheres.Add(bS);
                            if (majorSphere != new BoundingSphere())
                            {
                                majorSphere = BoundingSphere.CreateMerged(majorSphere, bS);
                            }
                            else majorSphere = bS;
                        }

                    }
                }
            }
            if (modelType.ExtraBoundingSphereRadius > 0)
            {

                BoundingSphere extra = new BoundingSphere(Vector3.Zero, modelType.ExtraBoundingSphereRadius);
                boundingSpheres.Add(extra);
                majorSphere = BoundingSphere.CreateMerged(majorSphere, extra);
               
            }

            if (modelType.CollisionCheckLevel == 0)
            {
                boundingSpheres.Clear();
                boundingSpheres.Add(majorSphere);
            }

            for (int i = 0; i < boundingBoxes.Count; i++)
            {
                transformedBoundingBoxes.Add(new BoundingBox());
            }
            for (int i = 0; i < boundingSpheres.Count; i++)
            {
                transformedBoundingSpheres.Add(new BoundingSphere());
            }
            TransformBounding();

        }
        /// <summary>
        /// 更新碰撞体
        /// </summary>
        void TransformBounding()
        {
            if (modelUsage == ModelUsage.Effect)
            {
                return;
            }
            for (int i = 0; i < boundingSpheres.Count; i++)
            {
                transformedBoundingSpheres[i] = boundingSpheres[i].Transform(this.World);
            }

            //BOX暂时不支持

            transformedMajorSphere = majorSphere.Transform(this.World);
        
        }
        public void SetRotation(Matrix rotation)
        {
            this.rotation = rotation;
        }
        /// <summary>
        /// 更新模型，并改变模型在世界坐标中的参数
        /// </summary>
        /// <param name="gameTime"></param>
        /// <param name="scale">缩放</param>
        /// <param name="rotation">旋转</param>
        /// <param name="position">位置</param>
        public void Update(GameTime gameTime,float scale,Matrix rotation ,Vector3 position,MoveState moveState)
        {
            
            this.scale = scale;
            this.rotation = rotation;
            this.position = position;

         
            this.UpdateEffects(gameTime,moveState);

            this.TransformBounding();
        }
        /// <summary>
        /// 更新模型，并改变模型在世界坐标中的参数
        /// </summary>
        /// <param name="gameTime"></param>
        /// <param name="rotation">旋转</param>
        /// <param name="position">位置</param>
        public void Update(GameTime gameTime, Matrix rotation, Vector3 position,MoveState moveState)
        {
     
            this.rotation = rotation;
            this.position = position;
            this.UpdateEffects(gameTime,moveState);

            this.TransformBounding();
        }
        /// <summary>
        /// 更新模型，并改变模型在世界坐标中的参数，注意Velocity仅供绘出效果时参考
        /// </summary>
        /// <param name="gameTime"></param>
        /// <param name="rotation">旋转</param>
        /// <param name="position">位置</param>
        public void Update(GameTime gameTime, float scale, Matrix rotation, Vector3 position, MoveState moveState,Vector3 velocity)
        {

            this.scale = scale;
            this.rotation = rotation;
            this.position = position;
            this.velocity = velocity;
            this.UpdateEffects(gameTime, moveState);

            this.TransformBounding();
        }

        /// <summary>
        /// 绘出子模型
        /// </summary>
        /// <param name="gameTime"></param>
        /// <param name="camera">使用的相机</param>
        public void DrawModels(GameTime gameTime,Camera camera)
        {
            if (AODGameLibrary.Helpers.RandomHelper.WithinRange(camera.ObjPosition,position,GameConsts.ModelDrawDistance))
            {
                foreach (DModel dModel in models)
                {
                    if (dModel != null)
                    {
                        dModel.Draw(gameTime, camera);
                    }
                }
            }
           
        }
        /// <summary>
        /// 绘出粒子效果
        /// </summary>
        /// <param name="gameTime"></param>
        /// <param name="camera">使用的相机</param>
        public void DrawEffects(GameTime gameTime, Camera camera)
        {
            if (AODGameLibrary.Helpers.RandomHelper.WithinRange(camera.ObjPosition, position, GameConsts.ParticleDrawDistance))
            {
                foreach (ParticleEffect effect in effects)
                {
                    if (effect != null)
                    {
                        effect.Draw(gameTime, camera);

                    }
                }
                foreach (ParticleEffect effect in birthEffects)
                {
                    if (effect != null)
                    {
                        effect.Draw(gameTime, camera);

                    }
                }
                if (isDying)
                {
                    foreach (ParticleEffect effect in deathEffects)
                    {
                        if (effect != null)
                        {
                            effect.Draw(gameTime, camera);

                        }
                    }
                }
                foreach (ParticleEffect pe in movementEffects)
                {
                    if (pe != null)
                    {
                        pe.Draw(gameTime, camera);
                    }
                }
            }
   
          

        }
        /// <summary>
        /// 开始死亡,播放死亡粒子效果
        /// </summary>
        public void BeginToDie()
        {
            isDying = true;
            isDeathEffectVisable = true;
            foreach (ParticleEffect pe in effects)
            {
                if (pe != null)
                {

                    pe.BeginToDie();
                }
            }
            foreach (ParticleEffect pe in deathEffects)
            {
                if (pe != null)
                {

                    pe.BeginToDie();
                }
            }
            foreach (ParticleEffect pe in movementEffects)
            {
                if (pe != null)
                {
                    pe.BeginToDie();
                }
            }
            foreach (DModel dm in models)
            {
                if (dm != null)
                {


                    dm.BeginToDie();
                }
            }
            if (deathsound != null)
            {

                    gameWorld.Play3DSound(deathsound, position);
                
                
            }
        }
        /// <summary>
        /// 只有正常情况下显示的例子开始死亡,不算模型死亡
        /// </summary>
        public void NormalEffectsBeginToDie()
        {
            foreach (ParticleEffect pe in effects)
            {
                if (pe != null)
                {

                    pe.BeginToDie();
                }
            }
        }
        /// <summary>
        /// 开始死亡但不播放死亡粒子效果
        /// </summary>
        public void BeginToDieWithoutDeathEffect()
        {
            isDying = true;
            isDeathEffectVisable = false;
            foreach (ParticleEffect pe in effects)
            {
                if (pe != null)
                {

                    pe.BeginToDie();
                }
            }
            foreach (ParticleEffect pe in deathEffects)
            {

                if (pe != null)
                {



                    pe.BeginToDie();
                }

            }
            foreach (ParticleEffect pe in movementEffects)
            {
                if (pe != null)
                {
                    pe.BeginToDie();
                }
            }
            foreach (DModel dm in models)
            {
                if (dm != null)
                {


                    dm.BeginToDie();
                }
            }
        }
        /// <summary>
        /// 更新粒子效果
        /// </summary>
        /// <param name="gameTime"></param>
        void UpdateEffects(GameTime gameTime,MoveState moveState)
        {
            if (AODGameLibrary.Helpers.RandomHelper.WithinRange(position, gameWorld.currentCamera.Position, GameConsts.ParticleDrawDistance))
            {
                foreach (ParticleEffect effect in effects)
                {
                    if (effect != null)
                    {
                        effect.Update(gameTime, velocity);

                    }
                }
                foreach (ParticleEffect effect in birthEffects)
                {
                    if (effect != null)
                    {
                        effect.Update(gameTime, velocity);

                    }
                }
                foreach (DModel dModel in models)
                {
                    if (dModel != null)
                    {
                        dModel.Update(gameTime);
                    }
                }
                if (isDying)
                {
                    if (isDeathEffectVisable)
                    {
                        foreach (ParticleEffect pe in deathEffects)
                        {
                            if (pe != null)
                            {

                                pe.Update(gameTime, velocity);
                            }
                        }
                    }

                    isDead = SeeifIsDead();
                }

                foreach (ParticleEffect pe in movementEffects)
                {


                    if (pe != null)
                    {
                        if (isDying != true)
                        {
                            if (moveState == MoveState.Moving)
                            {
                                pe.ReBirth();
                            }
                            else if (moveState == MoveState.NotMoving)
                            {
                                pe.BeginToDie();
                            }
                        }

                        pe.Update(gameTime, velocity);
                    }

                }
            }
            else if (isDying) isDead = true;

        }
        /// <summary>
        /// 从AODModelType读取相关参数
        /// </summary>
        /// <param name="game"></param>
        /// <param name="modelType">需要读取的AODModelType</param>
        void LoadType(GameWorld gameWorld, AODModelType modelType)
        {
            this.modelType = modelType;
            #region 已抛弃的代码
            //if (modelType.dModel1 != "")
            //{
            //    models.Add(new DModel(this,
            //                          gameWorld.game,
            //                          gameWorld.game.Content.Load<DModelType>(modelType.dModel1),
            //                          modelType.dModel1_scale,
            //                          modelType.dModel1_rotation,
            //                          modelType.dModel1_position));

            //}
            //if (modelType.dModel2 != "")
            //{
            //    models.Add(new DModel(this,
            //                          gameWorld.game,
            //                          gameWorld.game.Content.Load<DModelType>(modelType.dModel2),
            //                          modelType.dModel2_scale,
            //                          modelType.dModel2_rotation,
            //                          modelType.dModel2_position));

            //}
            //if (modelType.dModel3 != "")
            //{
            //    models.Add(new DModel(this,
            //                          gameWorld.game,
            //                          gameWorld.game.Content.Load<DModelType>(modelType.dModel3),
            //                          modelType.dModel3_scale,
            //                          modelType.dModel3_rotation,
            //                          modelType.dModel3_position));

            //}
            //if (modelType.dModel4 != "")
            //{
            //    models.Add(new DModel(this,
            //                          gameWorld.game,
            //                          gameWorld.game.Content.Load<DModelType>(modelType.dModel4),
            //                          modelType.dModel4_scale,
            //                          modelType.dModel4_rotation,
            //                          modelType.dModel4_position));

            //}
            //if (modelType.dModel5 != "")
            //{
            //    models.Add(new DModel(this,
            //                          gameWorld.game,
            //                          gameWorld.game.Content.Load<DModelType>(modelType.dModel5),
            //                          modelType.dModel5_scale,
            //                          modelType.dModel5_rotation,
            //                          modelType.dModel5_position));

            //}
            //if (modelType.pEffect1 != "")
            //{
            //    effects.Add(new ParticleEffect(gameWorld,
            //                                   gameWorld.game.Content.Load<ParticleEffectType>(modelType.pEffect1),
            //                                   this,
            //                                   modelType.pEffect1_position));

            //}
            //if (modelType.pEffect2 != "")
            //{
            //    effects.Add(new ParticleEffect(gameWorld,
            //                                   gameWorld.game.Content.Load<ParticleEffectType>(modelType.pEffect2),
            //                                   this,
            //                                   modelType.pEffect2_position));

            //}
            //if (modelType.pEffect3 != "")
            //{
            //    effects.Add(new ParticleEffect(gameWorld,
            //                                   gameWorld.game.Content.Load<ParticleEffectType>(modelType.pEffect3),
            //                                   this,
            //                                   modelType.pEffect3_position));

            //}
            //if (modelType.pEffect4 != "")
            //{
            //    effects.Add(new ParticleEffect(gameWorld,
            //                                   gameWorld.game.Content.Load<ParticleEffectType>(modelType.pEffect4),
            //                                   this,
            //                                   modelType.pEffect4_position));

            //}
            //if (modelType.pEffect5 != "")
            //{
            //    effects.Add(new ParticleEffect(gameWorld,
            //                                   gameWorld.game.Content.Load<ParticleEffectType>(modelType.pEffect5),
            //                                   this,
            //                                   modelType.pEffect5_position));

            //}
            //if (modelType.pEffect6 != "")
            //{
            //    effects.Add(new ParticleEffect(gameWorld,
            //                                   gameWorld.game.Content.Load<ParticleEffectType>(modelType.pEffect6),
            //                                   this,
            //                                   modelType.pEffect6_position));

            //}
            //if (modelType.pEffect7 != "")
            //{
            //    effects.Add(new ParticleEffect(gameWorld,
            //                                   gameWorld.game.Content.Load<ParticleEffectType>(modelType.pEffect7),
            //                                   this,
            //                                   modelType.pEffect7_position));

            //}
            //if (modelType.pEffect8 != "")
            //{
            //    effects.Add(new ParticleEffect(gameWorld,
            //                                   gameWorld.game.Content.Load<ParticleEffectType>(modelType.pEffect8),
            //                                   this,
            //                                   modelType.pEffect8_position));

            //}
            //if (modelType.pEffect9 != "")
            //{
            //    effects.Add(new ParticleEffect(gameWorld,
            //                                   gameWorld.game.Content.Load<ParticleEffectType>(modelType.pEffect9),
            //                                   this,
            //                                   modelType.pEffect9_position));

            //}
            //if (modelType.pEffect10 != "")
            //{
            //    effects.Add(new ParticleEffect(gameWorld,
            //                                   gameWorld.game.Content.Load<ParticleEffectType>(modelType.pEffect10),
            //                                   this,
            //                                   modelType.pEffect10_position));

            //}
            //if (modelType.deathEffect1 != "")
            //{
            //    deathEffects.Add(new ParticleEffect(gameWorld,
            //                                   gameWorld.game.Content.Load<ParticleEffectType>(modelType.deathEffect1),
            //                                   this,
            //                                   modelType.deathEffect1_position));

            //}
            //if (modelType.deathEffect2 != "")
            //{
            //    deathEffects.Add(new ParticleEffect(gameWorld,
            //                                   gameWorld.game.Content.Load<ParticleEffectType>(modelType.deathEffect2),
            //                                   this,
            //                                   modelType.deathEffect2_position));

            //}
            //if (modelType.moveEffect1 != "")
            //{
            //    movingEffects.Add(new ParticleEffect(gameWorld,
            //                                   gameWorld.game.Content.Load<ParticleEffectType>(modelType.moveEffect1),
            //                                   this,
            //                                   modelType.moveEffect1_position));

            //}
            //if (modelType.moveEffect2 != "")
            //{
            //    movingEffects.Add(new ParticleEffect(gameWorld,
            //                                   gameWorld.game.Content.Load<ParticleEffectType>(modelType.moveEffect2),
            //                                   this,
            //                                   modelType.moveEffect2_position));

            //}
            //if (modelType.moveEffect3 != "")
            //{
            //    movingEffects.Add(new ParticleEffect(gameWorld,
            //                                   gameWorld.game.Content.Load<ParticleEffectType>(modelType.moveEffect3),
            //                                   this,
            //                                   modelType.moveEffect3_position));

            //}
            //if (modelType.moveEffect4 != "")
            //{
            //    movingEffects.Add(new ParticleEffect(gameWorld,
            //                                   gameWorld.game.Content.Load<ParticleEffectType>(modelType.moveEffect4),
            //                                   this,
            //                                   modelType.moveEffect4_position));

            //}
            //if (modelType.moveEffect5 != "")
            //{
            //    movingEffects.Add(new ParticleEffect(gameWorld,
            //                                   gameWorld.game.Content.Load<ParticleEffectType>(modelType.moveEffect5),
            //                                   this,
            //                                   modelType.moveEffect5_position));

            //}
            //if (modelType.moveEffect6 != "")
            //{
            //    movingEffects.Add(new ParticleEffect(gameWorld,
            //                                   gameWorld.game.Content.Load<ParticleEffectType>(modelType.moveEffect6),
            //                                   this,
            //                                   modelType.moveEffect6_position));

            //}
            #endregion
            foreach (DModelEntry de in modelType.ChildModels)
            {
                if (de != null)
                {
                    Matrix r = AODGameLibrary.Helpers.RandomHelper.RotationVector3ToMatrix(de.Rotation);

                    models.Add(new DModel(this,
                                          gameWorld.game,
                                          de.DModel,
                                          de.Scale,
                                          r,
                                          de.Position, de.Alpha));
                    
                }
            }
            foreach (ParticleEffectEntry pE in modelType.NormalEffects)
            {
                if (pE != null)
                {
                    effects.Add(new ParticleEffect(gameWorld, gameWorld.game.Content.Load<ParticleEffectType>(pE.AssetName), this, pE.Position,pE.Scale));
                }
            }
            foreach (ParticleEffectEntry pE in modelType.MovementEffects)
            {
                if (pE != null)
                {
                    movementEffects.Add(new ParticleEffect(gameWorld, gameWorld.game.Content.Load<ParticleEffectType>(pE.AssetName), this, pE.Position, pE.Scale));
                }
            }
            foreach (ParticleEffectEntry pE in modelType.DeathEffects)
            {
                if (pE != null)
                {
                    deathEffects.Add(new ParticleEffect(gameWorld, gameWorld.game.Content.Load<ParticleEffectType>(pE.AssetName), this, pE.Position, pE.Scale));
                }
            }
            foreach (ParticleEffectEntry pE in modelType.BirthEffects)
            {
                if (pE!= null )
                {
                    birthEffects.Add(new ParticleEffect(gameWorld, gameWorld.game.Content.Load<ParticleEffectType>(pE.AssetName), this, pE.Position, pE.Scale));
                }
            }
            if (modelType.BirthSound !="")
            {
                birthsound = gameWorld.Content.Load<SoundEffect>(modelType.BirthSound).CreateInstance();
            }
            if (modelType.DeathSound != "")
            {
                deathsound = gameWorld.Content.Load<SoundEffect>(modelType.DeathSound).CreateInstance();
            }
            this.gameWorld = gameWorld;
        }
        bool SeeifIsDead()
        {
            if (isDeathEffectVisable)
            {
                foreach (ParticleEffect p in deathEffects)
                {
                    if (p != null)
                    {
                        if (p.IsDead == false)
                        {
                            return false;
                        }
                    }
                }
            }

            foreach (ParticleEffect p in effects)
            {
                if (p != null)
                {
                    if (p.IsDead == false)
                    {
                        return false;
                    }
                }
            }
            foreach (DModel dm in models)
            {
                if (dm != null)
                {
                    if (dm.IsDead == false)
                    {
                        return false;
                    }
                }
            }
            return true;

        }
    }
    public enum MoveState
    {
        Moving,
        NotMoving,
    }
    public enum ModelUsage
    {
        Normal,
        Effect,
    }
}
