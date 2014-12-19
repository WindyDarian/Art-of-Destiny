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
using AODGameLibrary.AODObjects;
using AODGameLibrary.Units;
using AODGameLibrary.Models;

namespace AODGameLibrary.Ambient
{
    /// <summary>
    /// 装饰物 ，由大地无敌-范若余在2009年12月28日加入。
    /// </summary>
    public class Decoration:VioableUnit
    {
        /// <summary>
        /// 绕Y轴旋转速度(角)
        /// </summary>
        float angularRate = 10;
        public bool Far = false;

        public Decoration():base()
        {
            base.Group = -1;
            
        }
        public Decoration(GameWorld gameWorld,DecorationType dT, Vector3 position):this()
        {
            Position = position;
            LoadType(dT,gameWorld);
            rotation = Matrix.CreateRotationX(AODGameLibrary.Helpers.RandomHelper.RandomNext(0, MathHelper.Pi)) * rotation;
            rotation = Matrix.CreateRotationY(AODGameLibrary.Helpers.RandomHelper.RandomNext(0, MathHelper.Pi)) * rotation;
            rotation = Matrix.CreateRotationZ(AODGameLibrary.Helpers.RandomHelper.RandomNext(0, MathHelper.Pi)) * rotation;
            Model.SetRotation(rotation);
        }
        /// <summary>
        /// 创建一个一定的装饰
        /// </summary>
        /// <param name="gameWorld"></param>
        /// <param name="dT"></param>
        /// <param name="position"></param>
        /// <param name="scale"></param>
        /// <param name="rotation">xyz分别代表绕模型自身xyz轴的旋转，角度</param>
        public Decoration(GameWorld gameWorld, DecorationType dT, Vector3 position, float scale,Vector3 rotation):this()
        {
            Position = position;
            LoadType(dT, gameWorld);
            Scale = scale;
            this.rotation = Matrix.CreateRotationX(MathHelper.ToRadians(rotation.X)) * this.rotation;
            this.rotation = Matrix.CreateRotationY(MathHelper.ToRadians(rotation.Y)) * this.rotation;
            this.rotation = Matrix.CreateRotationZ(MathHelper.ToRadians(rotation.Z)) * this.rotation;
            Model.SetRotation(this.rotation);
        }
        void LoadType(DecorationType dT,GameWorld gameWorld)
        {
            this.Name = dT.Name;
            this.UnitState = UnitState.alive;
            this.Scale = AODGameLibrary.Helpers.RandomHelper.RandomNext(dT.MinScale, dT.MaxScale);
            if (dT.RandomModelRotation)
            {
                this.modelRotation = Matrix.Identity
                                   * Matrix.CreateRotationX(AODGameLibrary.Helpers.RandomHelper.RandomNext(0, MathHelper.Pi))
                                   * Matrix.CreateRotationY(AODGameLibrary.Helpers.RandomHelper.RandomNext(0, MathHelper.Pi))
                                   * Matrix.CreateRotationZ(AODGameLibrary.Helpers.RandomHelper.RandomNext(0, MathHelper.Pi));
                                                            
            }
            else this.modelRotation = dT.ModelRotation;
            if (dT.Modelname != "")
                Model = new AODModel(gameWorld, gameWorld.game.Content.Load<AODModelType>(dT.Modelname), Position, ModelRotation, Scale);
            else Model = null;
            this.Mass = dT.Mass;
            this.FrictionForce = dT.FrictionForce;
            //this.sideThrustForce = unitType.sideThrustForce;
            this.angularRate = dT.AngularRate;
            this.MaxArmor = dT.MaxArmor;
            this.MaxShield = dT.MaxShield;
            this.Armor = this.MaxArmor;
            this.Shield = this.MaxShield;
            this.ShieldRestoreRate = dT.ShieldRestoreRate;
            this.ShieldRestoreTime = dT.ShieldRestoreTime;
            this.ShieldRestoreTimeleft = this.ShieldRestoreTime;
            this.ArmorRestoreRate = dT.ArmorRestoreRate;
            base.Bounding = dT.Bounding;
            base.Heavy = dT.Heavy;
            this.Far = dT.Far;
        }
        public override void Update(GameTime gameTime)
        {
            float elapsedTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            Rotation = Matrix.CreateRotationY(MathHelper.ToRadians(angularRate * elapsedTime))*Rotation;
            base.Update(gameTime);
        }

    }
}
