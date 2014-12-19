using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using AODGameLibrary.GamePlay;

namespace AODGameLibrary.Effects
{
    public class Particle
    {
        /// <summary>
        /// 粒子的位置
        /// </summary>
        public Vector3 position = Vector3.Zero;  
        /// <summary>
        /// 粒子当前速度
        /// </summary>
        public Vector3 velocity = Vector3.Zero;   
        /// <summary>
        /// 粒子的生命周期
        /// </summary>
        public float lifetime = 10.0f;  
        /// <summary>
        /// 初始缩放
        /// </summary>
        private float startScale = 1.0f;
        /// <summary>
        /// 最终缩放
        /// </summary>
        public float endScale = 1.0f;
        /// <summary>
        /// 初始颜色
        /// </summary>
        public Color startColor = new Color(255, 255, 255, 255);
        /// <summary>
        /// 结束颜色
        /// </summary>
        public Color endColor = new Color(255, 255, 255, 0);
        ///// <summary>
        ///// 属于的粒子组
        ///// </summary>
        //public CPUParticleGroup pg;
        /// <summary>
        /// 是否与粒子组保持相对静止
        /// </summary>
        public bool moveWithGroup = false;
        /// <summary>
        /// 得到粒子的颜色
        /// </summary>
        public Color ParColor
        {
            get
            {
                Vector4 sC = startColor.ToVector4();
                Vector4 eC = endColor.ToVector4();
                return new Color(sC + (eC - sC) * timeSinceStart / lifetime);
            }
        }
        public float timeSinceStart = 0;
        public float Scale
        {
            get
            {
                return startScale + (endScale - startScale) * timeSinceStart / lifetime;
            }
        }

        public bool isActive = false ;
        
            
        

        public void Initialize(Vector3 Position, 
                               Vector3 Velocity,
                               float Lifetime, 
                               float startScale,
                               float endScale,
                               Color startColor,
                               Color endColor)
        {
            this.moveWithGroup = false;
            this.position = Position;
            this.velocity = Velocity;
            this.lifetime = Lifetime;
            this.startColor = startColor;
            this.endColor = endColor;
            this.startScale = startScale;
            this.endScale = endScale;
            isActive = true;
            this.timeSinceStart = 0.0f; 
        }
        ///// <summary>
        ///// 创建追随粒子组移动的粒子
        ///// </summary>
        ///// <param name="pg"></param>
        ///// <param name="Velocity"></param>
        ///// <param name="Lifetime"></param>
        ///// <param name="startScale"></param>
        ///// <param name="endScale"></param>
        ///// <param name="startColor"></param>
        ///// <param name="endColor"></param>
        //public void Initialize(CPUParticleGroup pg,
        //                       Vector3 velocity,
        //                       float Lifetime,
        //                       float startScale,
        //                       float endScale,
        //                       Color startColor,
        //                       Color endColor)
        //{
        //    this.pg = pg;
        //    this.moveWithGroup = true;
        //    this.velocity = velocity;
        //    this.lifetime = Lifetime;
        //    this.startColor = startColor;
        //    this.endColor = endColor;
        //    this.startScale = startScale;
        //    this.endScale = endScale;
        //    isActive = true;
        //    this.timeSinceStart = 0.0f;
        //}

        /// <summary>
        /// 更新粒子
        /// </summary>
        /// <param name="t">间隔时间</param>
        public void Update(float t)
        {
            if (isActive == true )
            {
                timeSinceStart += t;

                if (timeSinceStart <= lifetime)
                {
                    //if (moveWithGroup == false)
                    //{

                        position += velocity * t;
                    //}
                    //else
                    //{
                    //    position = pg.Position + velocity * timeSinceStart;
                    //}
                }

                else isActive = false;
            }


        }
    }
}