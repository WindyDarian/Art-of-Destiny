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
using AODGameLibrary.Ambient;
using AODGameLibrary.CollisionChecking;
namespace AODGameLibrary.GameManagers
{
    /// <summary>
    /// 游戏物件管理器（单位、导弹、弹药、环境物等）
    /// </summary>
    public class GameItemManager:GameManager
    {

        public List<Unit> units = new List<Unit>(0);
        public List<Decoration> decorations = new List<Decoration>(50);
        public List<Bullet> bullets = new List<Bullet>(2500);
        public List<Missile> missiles = new List<Missile>(0);
        public List<LootItem> lootItems = new List<LootItem>(0);
         List<Unit> removingUnits = new List<Unit>(5);
         List<Missile> removingMissiles = new List<Missile>(5);
         List<Decoration> removingDecorations = new List<Decoration>(5);
         List<LootItem> removingLoots = new List<LootItem>(5);
         List<Bullet> removingBullets = new List<Bullet>(50);
        /// <summary>
        /// 碰撞检测单位集合
        /// </summary>
         List<VioableUnit> boundingCollection = new List<VioableUnit>(100);

         public List<VioableUnit> BoundingCollection
         {
             get { return boundingCollection; }
         }
        public GameItemManager(GameWorld gw):base(gw)
        {
           
        }
        public override void Update(GameTime gameTime)
        {
            float elapsedTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

            #region 更新单位
           
            removingUnits.Clear();

            for (int i = 0; i < units.Count; i++)
            {
                Unit u = units[i];

                if (u != null)
                {

                    u.Update(gameTime);
                    if (u.UnitState == UnitState.dead)
                    {
                        removingUnits.Add(u);//收集要被移除的单位
                    }
                }

            }
			

            #endregion

            #region 回收多余的单位
            foreach (Unit u in removingUnits)
            {
                if (u != null)
                {
                    units.Remove(u);
                }
            }
            #endregion

            #region 环境物

            removingDecorations.Clear();//晕，终于发现这里居然错误地写成了removingMissiles.Clear();
            foreach (Decoration d in decorations)
            {
                if (AODGameLibrary.Helpers.RandomHelper.WithinRange(d.Position, GameWorld.CurrentStage.Player.Position, GameConsts.GameViewDistance)|| d.Far)
                {

                    d.Update(gameTime);
                    if (d.UnitState == UnitState.dead)
                    {
                        removingDecorations.Add(d);//收集要被移除的单位
                    }
                }
            }

            #endregion

            #region 回收多余的单位
            foreach (Decoration d in removingDecorations)
            {
                if (d != null)
                {
                    decorations.Remove(d);
                }
            }
            #endregion
            
            

            #region 更新导弹
            removingMissiles.Clear();
            foreach (Missile m in missiles)
            {
                if (m != null)
                {
                    m.Update(gameTime);
                    if (m.UnitState == UnitState.dead)
                    {
                        removingMissiles.Add(m);
                    }
                }
            }
            foreach (Missile m in removingMissiles)
            {
                if (m != null)
                {
                    missiles.Remove(m);
                }
            }
            #endregion

            #region 更新LOOT
            {
                removingLoots.Clear();
                foreach (LootItem loot in lootItems)
                {
                    if (AODGameLibrary.Helpers.RandomHelper.WithinRange(loot.Position, GameWorld.CurrentStage.Player.Position, GameConsts.GameViewDistance))
                    {
                        loot.Update(gameTime);
                        if (loot.UnitState == UnitState.dead)
                        {
                            removingLoots.Add(loot);
                        }
                    }
                }
                foreach (LootItem loot in removingLoots)
                {
                    if (loot != null)
                    {
                        lootItems.Remove(loot);
                    }
                }
                if (lootItems.Count>GameConsts.MaxLootNum)
                {
                    lootItems.Remove(lootItems[0]);
                }
            }
            #endregion

            #region 更新弹药
            removingBullets.Clear(); //终于。。不再慢了。。。
            foreach (Bullet bullet in bullets)
            {

                if (bullet != null)
                {
                    bullet.Update(gameTime);


                    if (bullet.IsDead)
                    {
                        removingBullets.Add(bullet);
                    }
                }

            }
            foreach (Bullet bullet in removingBullets)
            {
                if (bullet != null)
                {
                    bullets.Remove(bullet);

                }
            }
            #endregion

            #region 单位间碰撞检测
 
            VioableUnit a;
            VioableUnit b;
            for (int m = 0; m < boundingCollection.Count; m++)
            {
                a = boundingCollection[m];
                if (a.Dead)
                {
                    continue;
                }
                for (int n = m + 1; n < boundingCollection.Count; n++)
                {
                    b = boundingCollection[n];
                    if (b.Dead)
                    {
                        continue;
                    }
                    if (b != null && a != b)
                    {
                        bool ts = b.Heavy || a.Heavy;
                        if (a.Bounding && b.Bounding && (AODGameLibrary.Helpers.RandomHelper.WithinRange(a.Position, b.Position, GameConsts.BoundingDistance) || ts))
                        {

                            if (Collision.IsCollided(a, b))
                            {
                                if ((!a.Heavy && !b.Heavy) || (a.Heavy&& b.Heavy&&a is Unit && b is Unit))
                                {
                                    //float t = Vector3.Dot(a.Thrust, Vector3.Normalize(b.position - a.position));//推力在两单位中心线的投影
                                    //if (t > 0)
                                    //    //a.GetThrust(-1 * t * Vector3.Normalize(b.position - a.position));
                                    //    a.Thrust = Vector3.Zero;
                                    //float x = Vector3.Dot(a.velocity, Vector3.Normalize(b.position - a.position));//速度在两单位中心线的投影
                                    //if (x > 0)
                                    //    a.GetThrust(-2f * x * Vector3.Normalize(b.position - a.position) * a.mass);
                                    if (a.collided == false && b.collided == false)
                                    {
                                        if (a.Position != b.Position)
                                        {
                                            {
                                                float s1 = Vector3.Dot(a.Velocity, Vector3.Normalize(b.Position - a.Position));
                                                float s2 = Vector3.Dot(b.Velocity, Vector3.Normalize(a.Position - b.Position));
                                                if (s1 > 0)
                                                {
                                                    a.GetImpulse(-1 * Vector3.Normalize(b.Position - a.Position) * s1 * a.Mass);
                                                    a.GetImpulse(Vector3.Normalize(a.Position - b.Position) * s2 * b.Mass);
                                                }
                                            }
                                            {
                                                float s1 = Vector3.Dot(b.Velocity, Vector3.Normalize(a.Position - b.Position));
                                                float s2 = Vector3.Dot(a.Velocity, Vector3.Normalize(b.Position - a.Position));
                                                if (s1 > 0)
                                                {
                                                    b.GetImpulse(-1 * Vector3.Normalize(a.Position - b.Position) * s1 * b.Mass);
                                                    b.GetImpulse(Vector3.Normalize(b.Position - a.Position) * s2 * a.Mass);
                                                }
                                            }

                                        }


                                    }
                                    if (a.Position != b.Position)
                                    {

                                        a.GetImpulse(-1 * Vector3.Normalize(b.Position - a.Position) * a.FrictionForce * elapsedTime);
                                        b.GetImpulse(-1 * Vector3.Normalize(a.Position - b.Position) * b.FrictionForce * elapsedTime);
                                    }
                                    else
                                    {
                                        a.GetImpulse(Vector3.Left * a.FrictionForce * elapsedTime);
                                        b.GetImpulse(Vector3.Right * b.FrictionForce * elapsedTime);
                                    }
                                    a.collided = true;
                                    b.collided = true;



                                }
                                else
                                {
                                    
                                    VioableUnit l = null;
                                    VioableUnit h = null;
                                    if (a.Heavy != b.Heavy)
                                    {

                                        if (b.Heavy)
                                        {
                                            h = b;
                                            l = a;
                                        }
                                        else
                                        {
                                            h = a;
                                            l = b;
                                        }
                                    }
                                    else
                                    {
                                        if (b is Decoration && !(a is Decoration))
                                        {
                                            h = b;
                                            l = a;
                                        }
                                        if (a is Decoration && !(b is Decoration))
                                        {
                                            h = a;
                                            l = b;
                                        }
                                    }
                                    if (l!= null && h != null)
                                    {
                                        if (l.Position != h.Position)
                                        {
                                            float t = Vector3.Dot(l.Thrust, Vector3.Normalize(h.Position - l.Position));//推力在两单位中心线的投影
                                            if (t > 0)
                                                // a.GetThrust(-1 * t * Vector3.Normalize(b.position - a.position));
                                                l.Thrust = Vector3.Zero;
                                            float x = Vector3.Dot(l.Velocity, Vector3.Normalize(h.Position - l.Position));//速度在两单位中心线的投影
                                            if (x > 0)
                                                l.GetImpulse(-1 * x * Vector3.Normalize(h.Position - l.Position) * l.Mass);
                                            l.GetImpulse(-1 * Vector3.Normalize(h.Position - l.Position) * l.FrictionForce * elapsedTime);
                                            //if (a.Thrust != Vector3.Zero)
                                            //    if (Collision.IsCollided(b, new Ray(a.position, Vector3.Normalize(a.Thrust))))
                                            //        a.Thrust = Vector3.Zero;
                                            //if (a.velocity != Vector3.Zero)
                                            //    if (Collision.IsCollided(b, new Ray(a.position, Vector3.Normalize(a.velocity))))
                                            //        a.velocity *= -1.1f;
                                        }
                                    }
                                    
                                    else
                                    {
                                        l.GetThrust(-1 * l.Velocity * Vector3.Forward * l.Mass / elapsedTime);
                                    }


                                }


                            }
                        }
                    }
                }
            }
            

            #endregion


            #region 更新位置
            foreach (Unit u in units)
            {
                if (u != null)
                {
                    u.UpdateLocation(gameTime);
                }
            }
            foreach (Decoration d in decorations)
            {
                if (AODGameLibrary.Helpers.RandomHelper.WithinRange(d.Position, GameWorld.CurrentStage.Player.Position, GameConsts.GameViewDistance)||d.Far)
                {
                    d.UpdateLocation(gameTime);
                }
            }
            foreach (Missile m in missiles)
            {
                if (m != null)
                {
                    m.UpdateLocation(gameTime);
                }
            }
            foreach (LootItem loot in lootItems)
            {
                if (AODGameLibrary.Helpers.RandomHelper.WithinRange(loot.Position, GameWorld.CurrentStage.Player.Position, GameConsts.GameViewDistance))
                {
                    loot.UpdateLocation(gameTime);
                }
            }
            for (int i = 0; i < units.Count; i++)
			{
                Unit u = units[i];
                if (u != null)
                {
                    u.UpdateWeapons(gameTime);
                }
			}
            #endregion

            base.Update(gameTime);
        }
        public override void SUpdate()
        {
            boundingCollection.Clear();
            foreach (Unit u in units)
            {
                boundingCollection.Add(u);
            }
            foreach (Decoration d in decorations)
            {
                if (d.Bounding)
                {
                    if (AODGameLibrary.Helpers.RandomHelper.WithinRange(d.Position, GameWorld.CurrentStage.Player.Position, GameConsts.GameViewDistance)||d.Far)
                    {

                        boundingCollection.Add(d);
                    }
                }
              
            }

            foreach (Unit u in units)
            {
                if (u != null)
                {
                    u.SUpdate();
                }
            }
            foreach (Decoration u in decorations)
            {
                if (AODGameLibrary.Helpers.RandomHelper.WithinRange(u.Position, GameWorld.CurrentStage.Player.Position, GameConsts.GameViewDistance)||u.Far)
                {
                    u.SUpdate();
                }
            }
            foreach (Missile m in missiles)
            {


                if (m != null)
                {
                    m.SUpdate();
                }
            }
            foreach (LootItem loot in lootItems)
            {
                if (loot != null)
                {
                    loot.SUpdate();
                }
            }
            #region 弹药碰撞检测
            foreach (Bullet bullet in bullets)
            {

                
                if (bullet.bulletOver == false)
                {

                    if (bullet.weapon.isInstant == false)
                    {



                        bullet.CheckCollision(boundingCollection);


                    }
                }

                bullet.SUpdate();
            }

            #endregion

            base.SUpdate();
        }
        public override void DrawGameScene(GameTime gameTime,Camera camera)
        {
            #region 绘出单位和导弹和loot
            foreach (Unit aunit in units)
            {
                if (aunit != null && aunit.Model != null)
                {
                    aunit.DrawModels(gameTime, camera);
                }
            }
            foreach (Decoration u in decorations)
            {
                if (u != null&& u.Model!= null)
                {
                    u.DrawModels(gameTime,camera);
                }
            }
            foreach (Missile m in missiles)
            {
                if (m != null && m.Model != null)
                {
                    m.DrawModels(gameTime, camera);
                }
            }
            foreach (LootItem loot in lootItems)
            {
                if (AODGameLibrary.Helpers.RandomHelper.WithinRange(loot.Position, GameWorld.CurrentStage.Player.Position, GameConsts.GameViewDistance) && loot.Model != null)
                {
                    loot.DrawModels(gameTime, camera);
                }
            }
            foreach (Unit aunit in units)
            {
                if (aunit != null && aunit.Model != null)
                {
                    aunit.DrawEffects(gameTime, camera);
                }
            }
            foreach (Decoration u in decorations)
            {
                if (u != null && u.Model != null)
                {
                    u.DrawEffects(gameTime, camera);
                }
            }
            foreach (Missile m in missiles)
            {
                if (m != null && m.Model != null)
                {
                    m.DrawEffects(gameTime, camera);
                }
            }
            foreach (LootItem loot in lootItems)
            {
                if (AODGameLibrary.Helpers.RandomHelper.WithinRange(loot.Position, GameWorld.CurrentStage.Player.Position, GameConsts.GameViewDistance) && loot.Model != null)
                {
                    loot.DrawEffects(gameTime, camera);
                }
            }

            #endregion


            #region 绘出弹药
            foreach (Bullet bullet in bullets)
            {
                if (bullet != null)
                {

                    bullet.Draw(gameTime, camera);
                }
            }
            #endregion
            base.DrawGameScene(gameTime,camera);
        }
        /// <summary>
        /// 返回距离一个点一定距离范围内的所有碰撞单位
        /// </summary>
        /// <param name="o">中点</param>
        /// <param name="range">最大距离</param>
        /// <returns></returns>
        public List<VioableUnit> ItemInRange(Vector3 o, float range)
        {
            List<VioableUnit> lu = new List<VioableUnit>(20);
            foreach (VioableUnit u in boundingCollection)
            {
                if (AODGameLibrary.Helpers.RandomHelper.WithinRange(o, u.Position, range))
                {
                    lu.Add(u);
                }
            }
            return lu;
        }

    }
}
