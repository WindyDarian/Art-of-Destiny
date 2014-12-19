using System;
using System.Collections.Generic;

using System.Text;
using AODGameLibrary.Units;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;
using AODGameLibrary.Cameras;
using AODGameLibrary.Weapons;
using AODGameLibrary.Gamehelpers;
using AODGameLibrary.Effects;
using AODGameLibrary.Effects.ParticleShapes;
using Microsoft.Xna.Framework.Graphics;
using AODGameLibrary.Interface;
using AODGameLibrary.AODObjects;
using AODGameLibrary.GamePlay;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
using AODGameLibrary.Texts;

namespace AODGameLibrary.Ambient
{
    /// <summary>
    /// 表示一个天空包,由大地无敌-范若余于2009年11月15日制作
    /// </summary>
    public class SkySphere
    {
        private Model model;

        public Model Model
        {
            get { return model; }
            set { model = value; }
        }
        private Texture2D texture;

        public Texture2D Texture
        {
            get { return texture; }
            set { texture = value; }
        }
        
        public Vector3 Rotation{
            get { return rotation; }
            set
            {
                rotation = new Vector3(value.X % 360, value.Y % 360, value.Z % 360);
                rotationMatrix = Matrix.CreateRotationX(MathHelper.ToRadians(rotation.X));
                rotationMatrix = Matrix.CreateRotationY(MathHelper.ToRadians(rotation.Y)) * rotationMatrix;
                rotationMatrix = Matrix.CreateRotationZ(MathHelper.ToRadians(rotation.Z)) * rotationMatrix;
            }
        }
        Vector3 rotation = Vector3.Zero;
        Matrix rotationMatrix;
        public SkySphere(string model,GameWorld gameWorld)
        {
           this.model = gameWorld.Content.Load<Model>(model);
           Rotation = Vector3.Zero;
        }
        public SkySphere(string model, GameWorld gameWorld,Vector3 rotation)
            : this(model ,gameWorld)
        {
            Rotation = rotation;
        }
        public SkySphere(string model, string texture, GameWorld gameWorld, Vector3 rotation)
            : this(model, gameWorld, rotation)
        {
            this.texture = gameWorld.Content.Load<Texture2D>(texture);
        }
        public void Draw(Camera camera)
        {
            if (model!= null)
            {
                Matrix[] skytransforms = new Matrix[model.Bones.Count];
                model.CopyAbsoluteBoneTransformsTo(skytransforms);

                foreach (ModelMesh mesh in model.Meshes)
                {
                    // This is where the mesh orientation is set, as well as our camera and projection.
                    foreach (BasicEffect effect in mesh.Effects)
                    {
                        effect.TextureEnabled = true;
                        if (texture != null)
                        {
                            effect.Texture = texture;
                        }
                        effect.AmbientLightColor = new Vector3(1, 1, 1);
                        effect.World = skytransforms[mesh.ParentBone.Index]
                            * Matrix.CreateScale(2000.0f)
                            * rotationMatrix
                            * Matrix.CreateTranslation(camera.Position);
                        effect.View = camera.View;
                        effect.Projection = camera.skyprojection;
                    }
                    // Draw the mesh, using the effects set above.
                    mesh.Draw();
                }
            }

        }
    }
}
