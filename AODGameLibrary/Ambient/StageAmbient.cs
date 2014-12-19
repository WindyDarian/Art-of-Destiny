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
    /// 关卡环境，由大地无敌-范若余于2010年1月17日加入
    /// </summary>
    public struct StageAmbient
    {
        public SkySphereEntry SkySphere;
        public List<DecorationEntry> Decorations;
    
    }
    public struct DecorationEntry
    {
        /// <summary>
        /// 装饰种类
        /// </summary>
        public string DecorationType;
        /// <summary>
        /// 地点
        /// </summary>
        public Vector3 Position;
        /// <summary>
        /// 是否随机缩放和旋转，如果为true则Scale和Rotation值无效
        /// </summary>
        public bool RandomScaleAndRotation;
        /// <summary>
        /// 缩放
        /// </summary>
        public float Scale;
        /// <summary>
        /// xyz分别代表绕模型自身xyz轴的旋转，角度
        /// </summary>
        public Vector3 Rotation;

    }
    public struct SkySphereEntry
    {
        public string SkySphereModel;
        public string Texture;
        public Vector3 Rotation;
    }
}
