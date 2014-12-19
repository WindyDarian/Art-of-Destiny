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
        /// ���ӵ�λ��
        /// </summary>
        public Vector3 position = Vector3.Zero;  
        /// <summary>
        /// ���ӵ�ǰ�ٶ�
        /// </summary>
        public Vector3 velocity = Vector3.Zero;   
        /// <summary>
        /// ���ӵ���������
        /// </summary>
        public float lifetime = 10.0f;  
        /// <summary>
        /// ��ʼ����
        /// </summary>
        private float startScale = 1.0f;
        /// <summary>
        /// ��������
        /// </summary>
        public float endScale = 1.0f;
        /// <summary>
        /// ��ʼ��ɫ
        /// </summary>
        public Color startColor = new Color(255, 255, 255, 255);
        /// <summary>
        /// ������ɫ
        /// </summary>
        public Color endColor = new Color(255, 255, 255, 0);
        ///// <summary>
        ///// ���ڵ�������
        ///// </summary>
        //public CPUParticleGroup pg;
        /// <summary>
        /// �Ƿ��������鱣����Ծ�ֹ
        /// </summary>
        public bool moveWithGroup = false;
        /// <summary>
        /// �õ����ӵ���ɫ
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
        ///// ����׷���������ƶ�������
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
        /// ��������
        /// </summary>
        /// <param name="t">���ʱ��</param>
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