#region File Description
//-----------------------------------------------------------------------------
// ParticleSettings.cs
//
// Microsoft XNA Community Game Platform
// Copyright (C) Microsoft Corporation. All rights reserved.
//-----------------------------------------------------------------------------


//由范若余-大地无敌于2009年8月4日移植入命运艺术(Art of Destiny)中
#endregion

#region Using Statements
using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
#endregion

namespace AODGameLibrary.Effects
{
    /// <summary>
    /// Settings class describes all the tweakable options used
    /// to control the appearance of a particle system.
    /// </summary>
    public class ParticleSettings
    {
        // Name of the texture used by this particle system.
        public string TextureName = null;


        // Maximum number of particles that can be displayed at one time.
        public int MaxParticles = 200;


        // How long these particles will last.
        public TimeSpan Duration = TimeSpan.FromSeconds(1);
        


        // If greater than zero, some particles will last a shorter time than others.
        public float DurationRandomness = 0;


        // Controls how much particles are influenced by the velocity of the object
        // which created them. You can see this in action with the explosion effect,
        // where the flames continue to move in the same direction as the source
        // projectile. The projectile trail particles, on the other hand, set this
        // value very low so they are less affected by the velocity of the projectile.
        public float EmitterVelocitySensitivity = 1;


        // Range of values controlling how much X and Z axis velocity to give each
        // particle. Values for individual particles are randomly chosen from somewhere
        // between these limits.
        public float MinHorizontalVelocity = 0;
        public float MaxHorizontalVelocity = 0;


        // Range of values controlling how much Y axis velocity to give each particle.
        // Values for individual particles are randomly chosen from somewhere between
        // these limits.
        public float MinVerticalVelocity = 0;
        public float MaxVerticalVelocity = 0;

        #region 由范若余-大地无敌于2009年8月5日添加，用于弥补该类在无重力条件下速度计算的不足
        //和HorizontalVelocity、VerticalVelocity叠加，在完全随机的方向上的速度
        public float MinInitialVelocity = 0;
        public float MaxInitialVelocity = 0;
        #endregion


        // Direction and strength of the gravity effect. Note that this can point in any
        // direction, not just down! The fire effect points it upward to make the flames
        // rise, and the smoke plume points it sideways to simulate wind.
        public Vector3 Gravity = Vector3.Zero;


        // Controls how the particle velocity will change over their lifetime. If set
        // to 1, particles will keep going at the same speed as when they were created.
        // If set to 0, particles will come to a complete stop right before they die.
        // Values greater than 1 make the particles speed up over time.
        public float EndVelocity = 1;


        // Range of values controlling the particle color and alpha. Values for
        // individual particles are randomly chosen from somewhere between these limits.
        //转化为Vector4更方便使用XML表示
        public Vector4 MinColor = new Vector4 (255,255,255,255);
        public Vector4 MaxColor = new Vector4 (255,255,255,255);


        // Range of values controlling how fast the particles rotate. Values for
        // individual particles are randomly chosen from somewhere between these
        // limits. If both these values are set to 0, the particle system will
        // automatically switch to an alternative shader technique that does not
        // support rotation, and thus requires significantly less GPU power. This
        // means if you don't need the rotation effect, you may get a performance
        // boost from leaving these values at 0.
        public float MinRotateSpeed = 0;
        public float MaxRotateSpeed = 0;


        // Range of values controlling how big the particles are when first created.
        // Values for individual particles are randomly chosen from somewhere between
        // these limits.
        public float MinStartSize = 100;
        public float MaxStartSize = 100;


        // Range of values controlling how big particles become at the end of their
        // life. Values for individual particles are randomly chosen from somewhere
        // between these limits.
        public float MinEndSize = 100;
        public float MaxEndSize = 100;



        // Alpha blending settings.//准备废弃。
        public Blend SourceBlend = Blend.SourceAlpha;
        public Blend DestinationBlend = Blend.InverseSourceAlpha;

        /// <summary>
        /// 小初始速度，仅在相对静止粒子组中有效
        /// </summary>
        public Vector3 MinStartVelocity;
        /// <summary>
        /// 大初始速度，仅在相对静止粒子组中有效
        /// </summary>
        public Vector3 MaxStartVelocity;

        /// <summary>
        /// 添加粒子的时间间隔
        /// </summary>
        public float AddParSpan;
        /// <summary>
        /// 结束永久持续添加粒子后继续添加粒子的时间
        /// </summary>
        public float AddParTime;
        /// <summary>
        /// 是否和父效果相对静止
        /// </summary>
        public bool IsSticking = false;
        
    }
}
