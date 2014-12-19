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

namespace AODGameLibrary.Effects.ParticleShapes
{
    /// <summary>
    /// 粒子线
    /// </summary>
    public class ParticleLine:ParticleShape
    {
        Vector3 origin = Vector3.Zero;
        [ContentSerializerIgnore]
        /// <summary>
        /// 中心
        /// </summary>
        public Vector3 Origin
        {
            get { return origin; }
            set { origin = value; }
        }
    
        float length;
        /// <summary>
        /// 长度
        /// </summary>
        public float Length
        {
            get { return length; }
            set { length = value; }
        }
        int particleEffectsNum;
        /// <summary>
        /// 粒子效果数
        /// </summary>
        public int ParticleEffectsNum
        {
            get { return particleEffectsNum; }
            set { particleEffectsNum = value; }
        }
        Vector3 direction = Vector3.Forward;
        [ContentSerializerIgnore]
        /// <summary>
        /// 方向向量（不包含在XML文件中）
        /// </summary>
        public Vector3 Direction
        {
            get { return direction; }
            set {
                if (value != Vector3.Zero)
                {


                    direction = Vector3.Normalize(value);
                }
                else throw new ApplicationException("这个方向向量不能为零向量！");
            }
        }

        public override void Initialize(GameWorld gameWorld)
        {
            for (int i = 0; i < particleEffectsNum; i++)
            {
                ChildParticleEffects.Add(new ParticleEffect(gameWorld, ParticleEffectType));
            }
            base.Initialize(gameWorld);
        }
        
        public override void Update(GameTime gameTime)
        {
            Vector3 s = length * direction / particleEffectsNum;
            for (int i = 0; i < particleEffectsNum; i++)
            {
                ChildParticleEffects[i].position = s * i + origin;
            }
            base.Update(gameTime);
        }
    }
}
