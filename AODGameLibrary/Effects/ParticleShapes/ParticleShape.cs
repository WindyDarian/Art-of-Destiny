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
    /// 由同种粒子效果构成的粒子形状，由大地无敌-范若余于2009年11月11日建立
    /// </summary>
    public class ParticleShape
    {
        private ParticleEffectType particleEffectType;
        /// <summary>
        /// 粒子种类
        /// </summary>
        public ParticleEffectType ParticleEffectType
        {
            get { return particleEffectType; }
            set { particleEffectType = value; }
        }
        

        private List<ParticleEffect> childarticleEffects = new List<ParticleEffect>(50);
        [ContentSerializerIgnore]
        /// <summary>
        /// 子模型
        /// </summary>
        public List<ParticleEffect> ChildParticleEffects
        {
            get { return childarticleEffects; }
            set { childarticleEffects = value; }
        }
        [ContentSerializerIgnore]
        public bool Cloned = false;
        [ContentSerializerIgnore]
        /// <summary>
        /// 是否死掉
        /// </summary>
        public bool IsDead
        {
            get
            {
                foreach (ParticleEffect pe in childarticleEffects)
                {
                    if (pe.IsDead == false)
                    {
                        return false;
                    }
                }
                return true;
            }
        }
        public virtual void Initialize(GameWorld gameWorld)
        {
            if (Cloned == false)
            {
                throw new ApplicationException("粒子形状未克隆！");
            }
        }
        public virtual void Update(GameTime gameTime)
        {
            foreach (ParticleEffect cE in childarticleEffects)
            {
                cE.Update(gameTime);
            }
        }
        public virtual void Draw(GameTime gameTime,Camera camera)
        {
            foreach (ParticleEffect cE in childarticleEffects)
            {
                cE.Draw(gameTime,camera);
            }
        }
        public virtual ParticleShape Clone()
        {
            ParticleShape s = (ParticleShape) this.MemberwiseClone();
            s.childarticleEffects = new List<ParticleEffect>(childarticleEffects);
            s.Cloned = true;
            return s;
        }
        public void BeginToDie()
        {
            foreach (ParticleEffect pe in childarticleEffects)
            {
                pe.BeginToDie();
            }
        }
        public void Flash()
        {
            foreach (ParticleEffect pe in childarticleEffects)
            {
                pe.Flash();
            }
        }
    }

}
