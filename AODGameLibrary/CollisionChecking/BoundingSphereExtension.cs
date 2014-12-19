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

namespace AODGameLibrary.CollisionChecking
{
    /// <summary>
    /// BoundingSphere的扩展，由大地无敌-范若余在2009年12月21日编写
    /// </summary>
    public static class BoundingSphereExtension
    {
        /// <summary>
        /// 得到射线与碰撞球体的第一个交点，如果没有返回null
        /// </summary>
        /// <param name="bS"></param>
        /// <param name="r"></param>
        public static Vector3? GetCollisionPoint(this BoundingSphere bS , Ray r)
        {
            if (bS.Intersects(r) != null)
            {

                //用向量点乘得到射线起点到圆心的向量在射线上的投影长度
                Vector3 v1 = bS.Center - r.Position;
                float l = Vector3.Dot(v1, r.Direction);

                //得到投影向量
                Vector3 v2 = r.Direction * l;

                //勾股定理算出碰撞点离射线起点的距离l
                float d2 = (bS.Radius * bS.Radius) - (v1 - v2).LengthSquared();
                if (d2 >= 0)
                {
                    if (bS.Contains(r.Position)==ContainmentType.Contains)//如果射线起点在球内就取射出点
                    {
                        l += (float)Math.Sqrt(d2);
                    }
                    else
                    {

                        l -= (float)Math.Sqrt(d2);
                    }
                }
                else return null;

                return r.Direction * l + r.Position;


            }
            else return null;
        }
    }
}
