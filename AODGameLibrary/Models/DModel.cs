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
namespace AODGameLibrary.Models
{
    /// <summary>
    /// 管理模型的类，最后注释2009年7月17日BY大地无敌-范若余
    /// </summary>
    public class DModel
    {
        /// <summary>
        /// 模型
        /// </summary>
        public Model model;
        /// <summary>
        /// 模型名称
        /// </summary>
        public string name;
        /// <summary>
        /// 相对于父模型的位置
        /// </summary>
        public Vector3 position;
        /// <summary>
        /// 相对于父模型的旋转矩阵
        /// </summary>
        public Matrix rotation;
        /// <summary>
        /// 相对于父模型的缩放
        /// </summary>
        public float scale;
        /// <summary>
        /// 父模型
        /// </summary>
        public AODModel parentModel;
        /// <summary>
        /// 死亡后模型消失所用时间
        /// </summary>
        float dieTime = 0.5f;
        float dieTimePast = 0.0f;
        public float alpha = 1;
        bool isDying = false;
        bool isDead = false;
        public bool IsDead
        {
            get
            {
                return isDead;
            }
        }
        /// <summary>
        /// 取得相对于父模型的世界矩阵
        /// </summary>
        public Matrix RelativeWorld
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
        /// 取得绝对的世界矩阵
        /// </summary>
        public Matrix World
        {
            get
            {
                if (parentModel != null)
                {
                    return RelativeWorld * parentModel.World;
                }
                else return RelativeWorld;
            }
        }
        /// <summary>
        /// 属于的Game
        /// </summary>
        Game game;
        /// <summary>
        /// 新建一个模型
        /// </summary>
        /// <param name="aModel">父模型</param>
        /// <param name="game"></param>
        /// <param name="dModelType">读取的DModelType</param>
        public DModel(AODModel aModel,Game game ,DModelType dModelType)
        {
            this.game = game;
            LoadType(dModelType);
            
            parentModel = aModel;
        }
        /// <summary>
        /// 新建一个模型并指定相对于父模型的参数
        /// </summary>
        /// <param name="aModel">父模型</param>
        /// <param name="game"></param>
        /// <param name="dModelType">读取的DModelType</param>
        /// <param name="scale">相对缩放</param>
        /// <param name="rotation">相对旋转矩阵</param>
        /// <param name="position">相对位置</param>
        public DModel(AODModel aModel, Game game, DModelType dModelType,float scale,Matrix rotation,Vector3 position,float alpha)
        {
            this.game = game;
            LoadType(dModelType);
            this.scale = scale;
            this.rotation = rotation;
            this.position = position;
            parentModel = aModel;
            this.alpha = alpha;
        }
        /// <summary>
        /// 新建一个无父模型的模型
        /// </summary>
        /// <param name="game"></param>
        /// <param name="dModelType">读取的DModelType</param>
        public DModel(Game game, DModelType dModelType)
        {
            this.game = game;
            LoadType(dModelType);
        }
        /// <summary>
        /// 从一个DModelType中读取参数新建模型
        /// </summary>
        /// <param name="dModelType">需要读取的DModelType</param>
        void LoadType(DModelType dModelType)
        {
            try
            {
                model = game.Content.Load<Model>(dModelType.model);
            }
            catch
            {
                Console.WriteLine("无法读取模型");
            }
            name = dModelType.name;
            this.dieTime = dModelType.dieTime;
        }
        /// <summary>
        /// 更新模型
        /// </summary>
        /// <param name="gameTime"></param>
        public void Update(GameTime gameTime)
        {
            if (isDying)
            {
                float elapsedTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
                dieTimePast += elapsedTime;
                if (dieTimePast>= dieTime)
                {
                    dieTimePast = dieTime;
                    isDying = false;
                    isDead = true;
                }
            }
        }
        /// <summary>
        /// 绘出模型
        /// </summary>
        /// <param name="gameTime"></param>
        /// <param name="camera">指定的相机</param>
        public void Draw(GameTime gameTime , Camera camera)
        {
            if (IsDead == false)
            {

                if (parentModel != null)
                {
                    Matrix[] transforms = new Matrix[model.Bones.Count];
                    model.CopyAbsoluteBoneTransformsTo(transforms);


                    
                    //game.GraphicsDevice.RenderState.AlphaBlendEnable = true;
                    //game.GraphicsDevice.RenderState.AlphaTestEnable = true;
                    game.GraphicsDevice.BlendState = BlendState.AlphaBlend;
                   game.GraphicsDevice.DepthStencilState = DepthStencilState.Default;

                    foreach (ModelMesh mesh in model.Meshes)
                    {

                        foreach (BasicEffect effect in mesh.Effects)
                        {

                            effect.EnableDefaultLighting();

                            effect.World = transforms[mesh.ParentBone.Index]
                                    * World;
                            effect.View = camera.View;
                            effect.FogEnabled = true;
                            effect.FogEnd = 12000;
                            effect.FogStart = 2000;
                            effect.Alpha = alpha;
                            effect.Projection = camera.Projection;

                        }
                        mesh.Draw();
                    }

                }
            }
        }
        public void BeginToDie()
        {
            isDying = true;
        }
    }
}
