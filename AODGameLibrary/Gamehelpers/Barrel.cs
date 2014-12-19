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


namespace AODGameLibrary.Gamehelpers
{
    /// <summary>
    /// 表示桶形\柱状\锥状轨迹的结构,由大地无敌-范若余于2009年8月17日建立
    /// </summary>
    public struct Barrel
    {
        /// <summary>
        /// 起始点
        /// </summary>
        Vector3 origin;
        /// <summary>
        /// 起始点
        /// </summary>
        public Vector3 Origin
        {
            get
            {
                return origin;
            }
            set
            {
                origin = value;
            }

        }

        /// <summary>
        /// 终点
        /// </summary>
        Vector3 endpoint;
        /// <summary>
        /// 终点
        /// </summary>
        public Vector3 Endpoint
        {
            get
            {
                return endpoint;
            }
            set
            {
                endpoint = value;
            }
        }
        /// <summary>
        /// 从起点到终点的长度
        /// </summary>
        public float Length
        {
            get
            {
                return Vector3.Distance(endpoint ,origin);
            }
            set
            {
                if (endpoint != origin)
                    endpoint = Vector3.Normalize(endpoint - origin) * value + origin;
            }
        }

        /// <summary>
        /// 初始半径
        /// </summary>
        public float startRadios;
        /// <summary>
        /// 初始半径
        /// </summary>
        public float StartRadios
        {
            get
            {
                return startRadios;
            }
            set
            {
                startRadios = value;
            }
        }

        /// <summary>
        /// 结束半径
        /// </summary>
        public float endRadios;
        /// <summary>
        /// 结束半径
        /// </summary>
        public float EndRadios
        {
            get
            {
                return endRadios;
            }
            set
            {
                endRadios = value;
            }
        }
        /// <summary>
        /// 通过起始点.终点.起始半径.终止半径建立桶形
        /// </summary>
        /// <param name="origin"></param>
        /// <param name="endPoint"></param>
        /// <param name="radios"></param>
        public Barrel(Vector3 origin, Vector3 endpoint, float startRadios,float endRadios)
        {
            this.endpoint = endpoint;
            this.endRadios = endRadios;
            this.origin = origin;
            this.startRadios = startRadios;
        }
        /// <summary>
        /// 通过起始点.方向.长度.起始半径.终止半径建立桶形
        /// </summary>
        /// <param name="origin"></param>
        /// <param name="direction"></param>
        /// <param name="length"></param>
        /// <param name="startRadios"></param>
        /// <param name="endRadios"></param>
        public Barrel(Vector3 origin, Vector3 direction, float length, float startRadios, float endRadios)
        {
            this.endRadios = endRadios;
            this.origin = origin;
            this.startRadios = startRadios;
            if (length != 0 && direction != Vector3.Zero)
            {
                this.endpoint = origin + Vector3.Normalize(direction) * length;
            }
            else
            {
                this.endpoint = origin;
            }

        }
        public bool Intersects(BoundingSphere bs)
        {
            
            if (origin != endpoint)
            {
                float a = 0;
                float k = 0;
                if (origin != bs.Center)
                {
                    a = (float)(Vector3.Dot(Vector3.Normalize(bs.Center - origin), Vector3.Normalize(endpoint - origin)));
                    k = MathHelper.Clamp((bs.Center - origin).Length() * a / (endpoint - origin).Length(), 0, 1);
                   
                    Vector3 b = Vector3.Lerp(origin, endpoint, k);
                    float br = k * (endRadios - startRadios) + startRadios;
                    if (Vector3.Distance(b, bs.Center) <= br + bs.Radius)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                }
                else
                {
                    return true;
                }
            }
            else
            {
                BoundingSphere k = new BoundingSphere(origin, MathHelper.Max(startRadios, endRadios));
                if (k.Intersects(bs))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }


        
        
    }
}
