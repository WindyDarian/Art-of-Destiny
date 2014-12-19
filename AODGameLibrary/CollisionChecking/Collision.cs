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
using AODGameLibrary.Gamehelpers;
using AODGameLibrary.Cameras;
using AODGameLibrary.Units;
using AODGameLibrary.Weapons;
using AODGameLibrary.Models;
using AODGameLibrary.GamePlay;
using AODGameLibrary.AODObjects;

namespace AODGameLibrary.CollisionChecking
{
    /// <summary>
    /// 处理单位的碰撞检测，最近一次是由大地无敌-范若余在2009年12月20日进行改良和优化
    /// </summary>
    public class Collision
    {

        /// <summary>
        /// 处理弹药和单位的碰撞，返回碰撞点
        /// </summary>
        /// <param name="unit"></param>
        /// <param name="bullet"></param>
        /// <returns></returns>
        public static Vector3? IsCollided(VioableUnit unit, Bullet bullet)
        {
            if (Vector3.Distance(unit.Position, bullet.position) <= GameConsts.BoundingDistance)
            {

                if (bullet.position != bullet.positionl)
                {

                    Ray ray1;
                    Ray ray2;
                    if (bullet.weapon.isInstant == false)
                    {
                        ray1 = new Ray(bullet.positionl, Vector3.Normalize(bullet.position - bullet.positionl));
                        ray2 = new Ray(bullet.position, Vector3.Normalize(bullet.positionl - bullet.position));

                    }
                    else
                    {
                        Vector3 v1 = bullet.position;
                        Vector3 v2 = bullet.position + Vector3.Normalize(bullet.velocity) * bullet.Range;

                        ray1 = new Ray(v1, Vector3.Normalize(v2 - v1));
                        ray2 = new Ray(v2, Vector3.Normalize(v1 - v2));
                    }


                    if (unit.Model.TransformedMajorSphere.Intersects(ray1) != null && unit.Model.TransformedMajorSphere.Intersects(ray2) != null)
                    {
                        foreach (BoundingSphere k in unit.Model.TransformedBoundingSpheres)
                        {
                            if (k.Intersects(ray1) != null && k.Intersects(ray2) != null)
                            {

                                return k.GetCollisionPoint(ray1);
                            }
                        }
                    }


                }
                return null;
            }
            return null;
        }
        /// <summary>
        /// 处理单位和射线的碰撞检测
        /// </summary>
        /// <param name="unit"></param>
        /// <param name="ray"></param>
        /// <returns></returns>
        public static bool  IsCollided(ObjectUnit unit, Ray ray)
        {




            if (unit.Model.TransformedMajorSphere.Intersects(ray) != null)
            {
                foreach (BoundingSphere k in unit.Model.TransformedBoundingSpheres)
                {
                    if (k != null)
                    {

                        if (k.Intersects(ray) != null)
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
            

        }
        /// <summary>
        /// 通过球体的碰撞检测检查出目标是否在导弹爆炸范围内
        /// </summary>
        /// <param name="unit">目标</param>
        /// <param name="missile">导弹</param>
        /// <param name="explosionRadiusFold">半径缩放倍数,在是否爆炸判断时可比1稍小</param>
        /// <returns></returns>
        public static bool IsCollided(VioableUnit unit, Missile missile, float explosionRadiusFold)
        {
            if (Vector3.Distance(unit.Position, missile.Position) <= GameConsts.BoundingDistance)
            {
                BoundingSphere b = new BoundingSphere(missile.Position, missile.missileType.explosionRadius * explosionRadiusFold);

                ContainmentType ct = b.Contains(unit.Model.TransformedMajorSphere);
                ContainmentType ct2 = unit.Model.TransformedMajorSphere.Contains(b);
                if (ct != ContainmentType.Disjoint || ct2 != ContainmentType.Disjoint)
                {
                    foreach (BoundingSphere k in unit.Model.TransformedBoundingSpheres)
                    {
                        ContainmentType ct3 = b.Contains(k);
                        ContainmentType ct4 = k.Contains(b);
                        if (ct3 != ContainmentType.Disjoint || ct4 != ContainmentType.Disjoint)
                            return true;
                    }

                }
            }

            return false;
        }
        public static bool IsCollided(Vector3 point, Barrel b)
        {
            if (b.Intersects(new BoundingSphere(point,0)))
            {
                return true;
            }
            return false;
        }
        public static bool IsCollided(VioableUnit unit, Barrel b)
        {
            bool ins = false;
            
            if (b.Intersects(unit.Model.TransformedMajorSphere))
            {
                foreach (BoundingSphere k in unit.Model.TransformedBoundingSpheres)
                {
                    if (k != null)
                    {
                        if (b.Intersects(k))
                        {
                            ins = true;
                            return ins;
                        }

                    }
                }

            }
            
            return ins;
        }
        public static bool IsCollided(ObjectUnit unit, ObjectUnit unit2)
        {
            //if (Vector3.Distance(unit.Position, unit2.Position) <= GameConsts.BoundingDistance)
            {


                if (isCollided(unit.Model.TransformedMajorSphere, unit2.Model.TransformedMajorSphere))
                {
                    foreach (BoundingSphere b1 in unit.Model.TransformedBoundingSpheres)
                    {
                        foreach (BoundingSphere b2 in unit2.Model.TransformedBoundingSpheres)
                        {
                            if (isCollided(b1, b2))
                            {
                                return true;
                            }
                        }
                    }
                }
            }
            return false;
        }
        public static bool isCollided(BoundingSphere a, BoundingSphere b)
        {
            
            ContainmentType ct = a.Contains(b);
            ContainmentType ct2 = b.Contains(a);
            if (ct != ContainmentType.Disjoint || ct2 != ContainmentType.Disjoint)
            {
                return true;
            }
            return false;

        }
        public static bool IsCollided(ObjectUnit unit, Vector3 point)
        {
            if (Vector3.Distance(unit.Position, point) <= GameConsts.BoundingDistance)
            {

                if (unit.Model.TransformedMajorSphere.Contains(point) == ContainmentType.Contains)
                {
                    foreach (BoundingSphere k in unit.Model.TransformedBoundingSpheres)
                    {
                        if (k != null)
                        {

                            if (k.Contains(point) == ContainmentType.Contains)
                            {
                                return true;
                            }
                        }
                    }
                }
                
            }
            return false;
        }
        public static bool IsCollided(VioableUnit unit, BoundingSphere boundingSphere)
        {
            if (Vector3.Distance(unit.Position, boundingSphere.Center) <= GameConsts.BoundingDistance)
            {


                if (isCollided(unit.Model.TransformedMajorSphere, boundingSphere))
                {
                    foreach (BoundingSphere k in unit.Model.TransformedBoundingSpheres)
                    {
                        if (k != null)
                        {

                            if (isCollided(k, boundingSphere))
                            {
                                return true;
                            }
                        }
                    }
                }

            }
            return false;
        }
       

    }
}
