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
using AODGameLibrary.Cameras;
using AODGameLibrary.Models;
using AODGameLibrary.AODObjects;
using AODGameLibrary.GamePlay;


namespace AODGameLibrary.Effects
{
    [Serializable]
    /// <summary>
    /// 表现一种粒子效果,所有粒子效果的基类
    /// </summary>
    public class ParticleEffect
    {
        ///// <summary>
        ///// 表示该效果由CPU负责更新的粒子组群集
        ///// </summary>
        //public List<CPUParticleGroup> cpuParticleGroups = new List<CPUParticleGroup> (2);
        /// <summary>
        /// 表示该效果由GPU负责更新的粒子组群集
        /// </summary>
        public List<ParticleGroup> particleGroups = new List<ParticleGroup> (4);
        /// <summary>
        /// 粒子效果位置，如果从属于一个AODModel则是相对于模型中心点的位置
        /// </summary>
        public Vector3 position;
        /// <summary>
        /// 父模型
        /// </summary>
        public AODModel parentModel;
        /// <summary>
        /// 用作绘出粒子时参考的移动速度，注意不影响自身的移动
        /// </summary>
        public Vector3 velocity;
        private float scale = 1;
        /// <summary>
        /// 缩放
        /// </summary>
        public float Scale
        {
            get { return scale; }
            set { scale = value; }
        }
        GameWorld gameWorld;
        public Vector3 Position
        {
            get
            {
                if (parentModel != null)
                {
                    return Vector3.Transform(position, parentModel.World);
                }
                else return position;
            }
        }
        public Vector3 ScaledPosition
        {
            get
            {
                if (parentModel != null)
                {

                    return Vector3.TransformNormal(position, parentModel.ScaleMatrix);
                }
                else return position;
            }
        }
        public bool IsDead
        {
            get
            {
                //foreach (CPUParticleGroup pg in cpuParticleGroups)
                //{
                //    if (pg!= null)
                //    {
                //        if (pg.isDead == false)
                //        {
                //            return false;
                //        }
                //    }

                //}
                foreach (ParticleGroup pg in particleGroups)
                {
                    if (pg!= null )
                    {
                        if (pg.IsAlive)
                        {
                            return false;
                        }
                    }
                }
                return true;
            }
        }
        public void BeginToDie()
        {

            //foreach (CPUParticleGroup pg in cpuParticleGroups)
            //{
            //    if (pg != null)
            //    {
            //        pg.IsEternal = false;
            //    }
            //}
            foreach (ParticleGroup pg in particleGroups)
            {
                if (pg != null)
                {
                    pg.IsEternal = false;
                }
            }

        }
        public void ReBirth()
        {
            //foreach (CPUParticleGroup pg in cpuParticleGroups)
            //{
            //    if (pg != null)
            //    {
            //        pg.Rebirth();
            //    }

            //}
            foreach (ParticleGroup pg in particleGroups)
            {
                if (pg != null)
                {
                    pg.ReBirth();
                }
            }
        }


        /// <summary>
        /// 产生粒子,然后开始死亡
        /// </summary>
        public void Flash()
        {
            ReBirth();
            BeginToDie();
        }
        public ParticleEffect(GameWorld gameWorld, ParticleEffectType particleEffectType)
        {
            LoadType(gameWorld, particleEffectType);
        }
        public ParticleEffect(GameWorld gameWorld, ParticleEffectType particleEffectType, AODModel model)
        {
            parentModel = model;
            LoadType(gameWorld, particleEffectType);
        }
        public ParticleEffect(GameWorld gameWorld, ParticleEffectType particleEffectType, AODModel model, Vector3 position)
        {
            parentModel = model;
      
            this.position = position;
            
            LoadType(gameWorld, particleEffectType);
        }
        public ParticleEffect(GameWorld gameWorld, ParticleEffectType particleEffectType, AODModel model, Vector3 position,float scale)
        {
            parentModel = model;
            this.position = position;
            this.scale *= scale;
            LoadType(gameWorld, particleEffectType);
        
        }
        public void Update(GameTime gameTime)
        {
            velocity = Vector3.Zero;
            UpdateGroups(gameTime);
        }
        public void Update(GameTime gameTime,Vector3 velocity)
        {

            this.velocity = velocity;
            
            UpdateGroups(gameTime);
        }
        void UpdateGroups(GameTime gameTime)
        {
            //foreach (CPUParticleGroup pg in cpuParticleGroups)
            //{
            //    if (pg != null)
            //    {


            //        pg.Update(gameTime);
            //    }
            //}
            foreach (ParticleGroup pg in particleGroups)
            {
                if (pg != null)
                {
                    pg.Update(gameTime);
                }
            }
        }
        public void Draw(GameTime gameTime ,Camera camera)
        {
            //foreach (CPUParticleGroup  pg in cpuParticleGroups )
            //{
            //    if (pg != null )
            //    {
            //        pg.Draw(gameTime, camera);
            //    }
            //}
            foreach (ParticleGroup pg in particleGroups)
            {
                if (pg != null)
                {
                    if (parentModel != null && pg.IsSticking)
                    {

                        pg.SetWorld(parentModel.RotationTranslation);//相对矩阵
                    }
                    pg.SetCamera(camera.View, camera.Projection);
                    pg.Draw(gameTime);
                }
            }
        }
        void LoadType(GameWorld gameWorld, ParticleEffectType particleEffectType)
        {
            this.gameWorld = gameWorld;
    
            if (particleEffectType != null)
            {
                foreach (string s in particleEffectType.ParticleGroups)
                {
                    if (s!= "")
                    {
                         particleGroups.Add(new ParticleGroup(gameWorld.game, gameWorld.game.Content, s,this ));
     
                    }
                }
                //foreach (string s in particleEffectType.CPUParticleGroups)
                //{
                //    if (s != "")
                //    {
                //        cpuParticleGroups.Add(new CPUParticleGroup(gameWorld,
                //                                     gameWorld.game.Content.Load<CPUParticleGroupType>(s),
                //                                     this));
                //    }
                //}
                scale *= particleEffectType.Scale;
  
            }
        }

    }

 
}
