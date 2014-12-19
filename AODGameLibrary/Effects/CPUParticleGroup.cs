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
using AODGameLibrary.GamePlay;

//废弃 2011/12/15 大地无敌-范若余
namespace AODGameLibrary.Effects
{
    [Serializable]
    /// <summary>
    /// 表示一个粒子群组
    /// </summary>
    public class CPUParticleGroup
    {
        /// <summary>
        /// 绘图设备
        /// </summary>
        private GraphicsDevice device; 
        /// <summary>
        /// 粒子的纹理
        /// </summary>
        public Texture2D texture;
        /// <summary>
        /// 使用的特效档
        /// </summary>
        private Effect effect;

        /// <summary>
        /// 粒子群组当前相对于粒子效果的位置
        /// </summary>
        public Vector3 groupPosition = Vector3.Zero;
        /// <summary>
        /// 该粒子群组属于的粒子效果的位置
        /// </summary>
        public Vector3 effectPosition
        {
            get
            {
                return particleEffect.Position;
            }
        }
        /// <summary>
        /// 粒子组的绝对位置
        /// </summary>
        public Vector3 Position
        {
            get
            {
                if (particleEffect != null)
                {

                    return groupPosition + effectPosition;
                }
                else return groupPosition;
            }
            set
            {
                groupPosition = value - effectPosition;
            }
        }
        /// <summary>
        /// 点的绝对大小
        /// </summary>
        public float PointSize
        {
            get
            {
                if (particleEffect != null)
                {
                    if (particleEffect.parentModel != null)
                    {
                        return pointSize * particleEffect.parentModel.scale;
                    }
                    else return pointSize;
                }
                else return pointSize;
            }
        }
        public bool IsEternal
        {
            get
            {
                return isEternal;
            }
            set
            {

                isEternal = value;
                if (value == true)
                {

                    Rebirth();
                }
            }
        }
        /// <summary>
        /// 上一帧的位置
        /// </summary>
        public Vector3 positionl;
        /// <summary>
        /// 每个粒子存活时间
        /// </summary>
        public float parLifetime = 1.0f;
        /// <summary>
        /// 每个粒子的初始颜色
        /// </summary>
        public Color startColor = new Color(255, 255, 255, 255);
        /// <summary>
        /// 每个粒子的结束颜色
        /// </summary>
        public Color endColor = new Color(255, 255, 255, 0);
        /// <summary>
        ///  每个粒子的可能最小速度
        /// </summary>
        public float minInitialSpeed = 10.0f;
        /// <summary>
        ///  每个粒子的可能最大速度
        /// </summary>
        public float maxInitialSpeed = 100.0f;
        /// <summary>
        /// 每个粒子的可能最小倍数
        /// </summary>
        public float minScale = 0.5f;
        /// <summary>
        /// 每个粒子的可能最大倍数
        /// </summary>
        public float maxScale = 1.5f;
        /// <summary>
        /// 最终缩放大小与初始大小的比
        /// </summary>
        public float scaleRate = 0.3f;
        /// <summary>
        /// 每桢添加的粒子数目,可以不为整数
        /// </summary>
        public float addParticleNum = 0.4f;
        /// <summary>
        /// 点的基本大小
        /// </summary>
        public float pointSize = 32.0f;
        /// <summary>
        /// 是否持续产生新粒子，若为false则开始死亡
        /// </summary>
        public bool isEternal = true ;
        /// <summary>
        /// 死亡时间，若isLasting为false时间到后将不再产生新粒子
        /// </summary>
        public float Deathtime = 1.0f;
        /// <summary>
        /// 在isLasting == false的情况下产生新粒子的时间
        /// </summary>
        public float addParTimeSinceStart = 0.0f;
    
        private bool isActive = true;
        /// <summary>
        /// 该粒子群组是否正在产生新粒子
        /// </summary>
        public bool IsActive
        {
            get { return isActive; }
            set
            {
                isActivel = isActive;
                isActive = value;
            
            }
        }
        /// <summary>
        /// Active为false后的时间
        /// </summary>
        float timeSinceNotActive = 0;

        CPUParticleGroupType parType;
        /// <summary>
        /// 是否执行过Update
        /// </summary>
        bool updated = false;
        /// <summary>
        /// 粒子是否随群组一起运动
        /// </summary>
        public bool particleMoveWithGroup = false;
        /// <summary>
        /// 获取最终从属AODModel的缩放
        /// </summary>
        float ModelScale
        {
            get
            {
                if (particleEffect.parentModel != null)
                {
                    return particleEffect.parentModel.scale;
                }
                else
                {
                    return 1.0f;
                }
            }
        }
        /// <summary>
        /// 该粒子群组是否已终结
        /// </summary>
        public bool isDead = false;
        int spriteArray_Count = 0;
        /// <summary>
        /// 粒子群组所属的粒子效果
        /// </summary>
        ParticleEffect particleEffect;
        /// <summary>
        /// 表示等待添加的粒子树目
        /// </summary>
        private float paradd;
        /// <summary>
        /// 上一桢是否活跃
        /// </summary>
        private bool isActivel;
        /// <summary>
        /// 顶点结构
        /// </summary>
        private struct VertexPointSprite
        {
            private Vector3 position;
            private float pointSize;
            private Color color;

            public VertexPointSprite(Vector3 position, float pointSize, Color color)
            {
                this.position = position;
                this.pointSize = pointSize;
                this.color = color;
            }

            public static readonly VertexElement[] VertexElements =
              {
                  new VertexElement(0,
                                    0,
                                    VertexElementFormat.Vector3,
                                    VertexElementMethod.Default,
                                    VertexElementUsage.Position,
                                    0),
                  new VertexElement(0,
                                    sizeof(float)*3,
                                    VertexElementFormat.Single,
                                    VertexElementMethod.Default,
                                    VertexElementUsage.PointSize,
                                    0),
                  new VertexElement(0,
                                    sizeof(float)*4,
                                    VertexElementFormat.Color,
                                    VertexElementMethod.Default,
                                    VertexElementUsage.Color,
                                    0),
              };
            public static int SizeInBytes = sizeof(float) * (3 + 1 + 1);
        }

        public Particle[] particle = new Particle[250];
        VertexPointSprite[] spriteArray = new VertexPointSprite[250];

        VertexDeclaration pointSpriteVertexDeclaration;
        ///// <summary>
        ///// 初始化一个粒子组
        ///// </summary>
        ///// <param name="game">赋予一个Game</param>
        //public CPUParticleGroup(GameWorld gameWorld)
        //{

        //    this.texture = gameWorld.game.Content.Load<Texture2D>(@"Textures\fire");
        //    this.effect = gameWorld.CPUParEffect;

        //    this.Initialize(gameWorld.game);
        //}
        /// <summary>
        /// 使用一个从资源中读取读取的粒子效果类型来初始化粒子组
        /// </summary>
        /// <param name="game">赋予一个Game</param>
        /// <param name="pType">粒子组类型</param>
        /// <param name="pE">粒子组所属的粒子效果</param>
        public CPUParticleGroup(GameWorld gameWorld, CPUParticleGroupType pType, ParticleEffect pE)
        {

            this.effect = gameWorld.CPUParEffect;
            this.Deathtime = pType.Deathtime;
            this.addParticleNum = pType.addParticleNum;
            this.endColor = new Color(pType.endColor / 255);
            this.groupPosition = pType.groupPosition;
         
            this.maxInitialSpeed = pType.maxInitialSpeed;
            this.maxScale = pType.maxScale;
            this.minInitialSpeed = pType.minInitialSpeed;
            this.minScale = pType.minScale;
            this.parLifetime = pType.parLifetime;
            this.pointSize = pType.pointSize * pE.Scale;
            this.scaleRate = pType.scaleRate;
            this.startColor = new Color(pType.startColor / 255);
            this.particleMoveWithGroup = pType.particleMoveWithGroup;
            if (pType.texture != "")
            {
                this.texture = gameWorld.game.Content.Load<Texture2D>(pType.texture);
            }
            this.particleEffect = pE;
            this.parType = pType;
            this.Initialize(gameWorld.game);
        }
        public void Initialize(Game game)
        {
            this.device = game.GraphicsDevice;
            pointSpriteVertexDeclaration = new VertexDeclaration(device, VertexPointSprite.VertexElements);
            for (int i = 0; i < particle.Length; i++)
            {
                particle[i] = new Particle();
            }
        }
        /// <summary>
        /// 添加粒子
        /// </summary>
        /// <param name="number">添加的粒子数量</param>
        void AddPraticle(int number)
        {
            
            for (int i = 0; i < number; i++)
            {

                foreach (Particle p in particle)
                {
                    if (p.isActive == false)
                    {
                        if (particleMoveWithGroup == false)
                        {
                            Vector3 dv;//为了使粒子效果看起来实在一直运动而不是每一帧更新一次天哪我注释不清
                            if (number != 0 && positionl != null && updated && isActivel)
                            {
                                dv = (positionl - Position) / number;
                            }
                            else
                            {
                                dv = Vector3.Zero;
                            }
                            Vector3 velocity = new Vector3();
                            Random ra = GameHelpers.GameHelper.Random;
                            velocity.X = 0.5f - (float)ra.NextDouble();
                            velocity.Y = 0.5f - (float)ra.NextDouble();
                            velocity.Z = 0.5f - (float)ra.NextDouble();
                            if (velocity != Vector3.Zero)
                            {

                                velocity.Normalize();
                                velocity *= minInitialSpeed + (float)ra.NextDouble() * (maxInitialSpeed - minInitialSpeed);
                            }
                            velocity *= ModelScale;
                            float ss = minScale + (float)ra.NextDouble() * (maxScale - minScale);
                            float ns = ss * scaleRate;
                            Vector3 pst = Position + dv * i;
                            p.Initialize(pst, velocity, parLifetime, ss, ns, startColor, endColor);
                            break;
                        }
                        else
                        {
                            Vector3 velocity = new Vector3();
                            Random ra = GameHelpers.GameHelper.Random;
                            velocity.X = 0.5f - (float)ra.NextDouble();
                            velocity.Y = 0.5f - (float)ra.NextDouble();
                            velocity.Z = 0.5f - (float)ra.NextDouble();
                            if (velocity != Vector3.Zero)
                                velocity.Normalize();
                            else velocity = Vector3.Forward;
                            velocity *= minInitialSpeed + (float)ra.NextDouble() * (maxInitialSpeed - minInitialSpeed);
                            velocity *= ModelScale;
                            float ss = minScale + (float)ra.NextDouble() * (maxScale - minScale);
                            float ns = ss * scaleRate;
                            p.Initialize(this, velocity, parLifetime, ss, ns, startColor, endColor);
                            break;
                        }

                    }
                }
            }
        }
        public void Update(GameTime gameTime)
        {



            float elapsedTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (isActive)
            {
                paradd += addParticleNum;
                if (paradd >= 1.0f)
                {
                    AddPraticle((int)paradd);
                    paradd = paradd - (float)(int)paradd;

                }
                if (isEternal == false)
                {
                    addParTimeSinceStart += elapsedTime;
                    if (addParTimeSinceStart >= Deathtime)
                    {
                        addParTimeSinceStart = Deathtime;

                        isActive = false;
                    }
                }
            }
            else
            {
                if (isDead == false)
                {

                    //如果粒子组不活动的时间大于最后一个粒子的生命期那么粒子组终结
                    timeSinceNotActive += elapsedTime;
                    if (timeSinceNotActive >= parLifetime)
                    {
                        isDead = true;
                    }
                }
            }
            foreach (Particle p in particle)
            {
                if (p.isActive)
                    p.Update(elapsedTime);
            }

            
            positionl = Position;
            
            
            isActivel = isActive;
            updated = true;

        }
        public void Draw(GameTime gameTime, Camera camera)
        {
            float elapsedTime = (float)gameTime.ElapsedGameTime.TotalSeconds;




            effect.Parameters["view"].SetValue(camera.View);
            effect.Parameters["projection"].SetValue(camera.Projection);
            effect.Parameters["viewportHeight"].SetValue(device.Viewport.Height);
            effect.Parameters["modelTexture"].SetValue(texture);

            spriteArray_Count = -1;
            for (int i = 0; i < particle.Length; i++)
            {
                if (particle[i].isActive)
                {

                    spriteArray[++spriteArray_Count] = new VertexPointSprite(particle[i].position,
                                                             particle[i].Scale * PointSize ,
                                                             particle[i].ParColor);
                }
            }

            if (spriteArray_Count <= 0) return;

            //  頂點格式宣告
            device.VertexDeclaration = pointSpriteVertexDeclaration;
            SetParticleRenderStates(device.RenderState);
            /*
            device.RenderState.AlphaBlendEnable = true;
            device.RenderState.SourceBlend = Blend.SourceAlpha;
            device.RenderState.DestinationBlend = Blend.InverseSourceAlpha;
            device.RenderState.AlphaTestEnable = true;
            device.RenderState.AlphaFunction = CompareFunction.Greater;
            device.RenderState.PointSize = 1000.0f;
            device.RenderState.PointSizeMax = 10000.0f;
            device.RenderState.PointSizeMin = 100.0f;
            device.RenderState.DepthBufferEnable = true ;
             */
            effect.CurrentTechnique = effect.Techniques["PointSprites"];

            effect.Begin();
            foreach (EffectPass CurrentPass in effect.CurrentTechnique.Passes)
            {
                CurrentPass.Begin();

                device.DrawUserPrimitives(PrimitiveType.PointList,
                                          spriteArray,
                                          0,
                                          spriteArray_Count);
                CurrentPass.End();
            }
            effect.End();
            device.RenderState.DepthBufferEnable = true;
            device.RenderState.AlphaBlendEnable = false;
            device.RenderState.AlphaTestEnable = false;

            device.RenderState.PointSpriteEnable = false;
            device.RenderState.DepthBufferWriteEnable = true;
        }
        void SetParticleRenderStates(RenderState renderState)
        {
            // Enable point sprites.
            renderState.PointSpriteEnable = true; 

            // Set the alpha blend mode.
            renderState.AlphaBlendEnable = true;
            renderState.AlphaBlendOperation = BlendFunction.Add;
            renderState.SourceBlend = Blend.SourceAlpha;
            renderState.DestinationBlend = Blend.DestinationAlpha;

            // Set the alpha test mode.
            renderState.AlphaTestEnable = true;
            renderState.AlphaFunction = CompareFunction.Greater;
            renderState.ReferenceAlpha = 0;

            // Enable the depth buffer (so particles will not be visible through
            // solid objects like the ground plane), but disable depth writes
            // (so particles will not obscure other particles).
            renderState.DepthBufferEnable = true;
            renderState.DepthBufferWriteEnable = false;
        }
        public void Rebirth()
        {
            isEternal = true;
            addParTimeSinceStart = 0.0f;
            isActive = true;
            isDead = false;
            timeSinceNotActive = 0;
        }

    }
}
