using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Net;
using Microsoft.Xna.Framework.Storage;
using AODGameLibrary.Units;
using AODGameLibrary.Gamehelpers;
using AODGameLibrary.AODObjects;
using AODGameLibrary.GamePlay;
using AODGameLibrary.AIs;
using AODGameLibrary;

namespace AODGameLibrary2
{
    /// <summary>
    /// 协助型AI,由大地无敌-范若余于2010年2月20日建立
    /// </summary>
    public class AssistAI:AODGameLibrary.AIs.AI
    {

     
        /// <summary>
        /// 威胁值
        /// </summary>
        float threat = 20.0f;
        /// <summary>
        /// AI参数
        /// </summary>
        public AISettings settings = new AISettings();
        /// <summary>
        /// 得到或设置威胁值，在0到100之间
        /// </summary>
        public float Threat
        {
            get { return threat; }
            set
            {
                threat = MathHelper.Clamp(value, 0.0f, 100.0f);
            }

        }
        
        

        /// <summary>
        /// 战术偏好旋转方向
        /// </summary>
        Vector3 loveDirection = Vector3.Right;
        /// <summary>
        /// 与上次更新的间隔时间
        /// </summary>
        float timeSinceLastUpdate = 0.0f;
        /// <summary>
        /// 与上次发导弹的间隔时间
        /// </summary>
        float timeSinceLastMissile = 0.0f;
        /// <summary>
        /// 在环绕时是否为攻击状态
        /// </summary>
        bool attacking = false;
        bool isTargetChangeAble = true;
        /// <summary>
        /// 是否允许自行改变目标
        /// </summary>
        public bool TargetChangeAble
        {
            get
            {
                return isTargetChangeAble;
            }
            set
            {
                isTargetChangeAble = value;
            }
        }
        public Unit AssistUnit;

        Random r = AODGameLibrary.Helpers.RandomHelper.Random;
        ///// <summary>
        ///// 如果不在协助模式为null,如果在协助模式则为协助目标
        ///// </summary>
        //Unit AssistTarget;
        ///// <summary>
        ///// 协助最大距离
        ///// </summary>
        //float AssistRange;

     

        /// <summary>
        /// 受到伤害更新仇恨
        /// </summary>
        /// <param name="attacker">伤害的制造者</param>
        /// <param name="damagevalue">护盾和护甲伤害的总和除以护盾和护甲最大值</param>
        public override void GetDamage(Unit attacker, float threatvalue)
        {
            if (attacker!= null )
            {
                if (attacker.Group != margedUnit.Group)
                {
                    if (attacker == margedUnit)
                    {
                        Threat += threatvalue * 200;
                    }
                    else
                    {
                        Threat -= threatvalue * 100;
                        if (Threat < 20 && isTargetChangeAble)
                        {
                            Target = attacker;
                        }
                    }
                }
            }


        }
        public AssistAI(AISettings settings,Unit assist):base()
        {
            this.settings = settings;
            AssistUnit = assist;
            r = AODGameLibrary.Helpers.RandomHelper.Random;
            int i = (int)(r.NextDouble() * 3);
            switch (i)
            {
                case 0:
                    loveDirection = Vector3.Right;
                    break;
                case 1:
                    loveDirection = Vector3.Up;
                    break;
                case 2:
                    loveDirection = Vector3.Normalize(Vector3.Right + Vector3.Up);
                    break;
            }
            base.TargetChanged += new EventHandler(RegularAI_TargetChanged);
        }

        void RegularAI_TargetChanged(object sender, EventArgs e)
        {
            threat = 40;
        }
        public override void Update(GameTime gameTime)
        {
            float elapsedTime = (float) gameTime.ElapsedGameTime.TotalSeconds;
            if (margedUnit.UnitState != UnitState.dead && margedUnit.UnitState != UnitState.dying)
            {

                #region 判断是否需要寻找新目标
                if (DoThreatUpdate(gameTime))
                {
                    if (Target != null)
                    {
                        UpdateThreat();
                        if (Threat < 20)
                        {
                            if (TargetChangeAble)
                            {

                                LookForNewTarget(gameWorld);
                            }
                        }
                        else if (margedUnit.Group == Target.Group || Target.UnitState != UnitState.alive)
                        {
                            LookForNewTarget(gameWorld);
                        }


                    }
                    else LookForNewTarget(gameWorld);
                    if (settings.isLoveDirectionChangeAble)
                    {
                        int i = (int)(r.NextDouble() * 3);
                        switch (i)
                        {
                            case 0:
                                loveDirection = Vector3.Right;
                                break;
                            case 1:
                                loveDirection = Vector3.Up;
                                break;
                            case 2:
                                loveDirection = Vector3.Normalize(Vector3.Right + Vector3.Up);
                                break;
                        }
                    }
                }
                #endregion
                #region 进行动作计算
                if (Target != null)
                {
                    if (Target.UnitState == UnitState.alive)
                    {

                        #region 判断是否需要射击
                        if (NeedShot())
                        {
                            margedUnit.ShotFlame();
                        }

                        if (settings.isSkillUsable)
                        {
                            if (settings.isSkillSwitchable)
                            {
                                margedUnit.CurrentSkillNumber += 1;
                                if (margedUnit.CurrentSkill != null)
                                {
                                    
                                    //if (margedUnit.CurrentSkill.IsSkillUsable)
                                    //{
                                        margedUnit.CastSkill();
                                    //}
                                }
                            }
                            if (margedUnit.CurrentSkill != null && margedUnit.IsUsingSkill == false)
                            {
                                //if (margedUnit.CurrentSkill.IsSkillUsable)
                                //{
                                    margedUnit.CastSkill();
                                //}
                            }
                        }
                        
                        if (settings.isMissileLaunchable)
                        {
                            float d = Unit.Distance(margedUnit, Target);
                            timeSinceLastMissile += (float)gameTime.ElapsedGameTime.TotalSeconds;
                            timeSinceLastMissile = MathHelper.Clamp(timeSinceLastMissile, 0, settings.MissileLaunchSpan);
                            if (timeSinceLastMissile >= settings.MissileLaunchSpan && d <= settings.MaxMissileRange && d > settings.MinMissileRange)
                            {

                                margedUnit.ShotMissileFlame();
                                timeSinceLastMissile = 0;
                            }

                        }
                        #endregion

                        if (settings.isMoveAble)
                        {

                            if (Target.UnitState == UnitState.alive)
                            {
                                if (margedUnit.Position != Target.Position)
                                {
                                    float a = Vector3.Dot(Vector3.Normalize(Target.Position - margedUnit.Position), margedUnit.Face);
                                    float d = Unit.Distance(margedUnit, Target);
                                    Vector3 tm;
                                    if (a > 0 && settings.isSmartShot)
                                    {

                                        tm = Unit.SDirection(margedUnit, Target.Position + Target.Velocity*elapsedTime);
                                    }
                                    else tm = Vector3.Normalize(Vector3.Transform(Target.Position+ Target.Velocity*elapsedTime, Matrix.Invert(margedUnit.World)));

                                    if (d > settings.rangeOfCycle)
                                    {
                                        margedUnit.RotateFlame(tm);
                                        //margedUnit.RotateRealWorldFlame(Target.position);//对准目标方向

                                        if (a > 0)
                                        {
                                            margedUnit.EngineOnFlame();

                                        }

                                    }
                                    else if (d <= settings.rangeOfCycle && d >= settings.minRange)
                                    {
                                        if (settings.isCycleAble)
                                        {
                                            if (margedUnit.Speed > margedUnit.MaxSpeed / 2 && attacking == false)
                                            {
                                                margedUnit.RotateFlame(tm);
                                                //margedUnit.RotateRealWorldFlame(Target.position);
                                                attacking = true;
                                            }
                                            else if (margedUnit.Speed < margedUnit.MaxSpeed / 4 && attacking == true)
                                            {

                                                attacking = false;
                                            }
                                            if (attacking == true)
                                            {
                                                margedUnit.RotateFlame(tm);
                                                // margedUnit.RotateRealWorldFlame(Target.position);
                                            }
                                            else
                                            {
                                                if (a > 0.707)
                                                {
                                                    margedUnit.RotateFlame(loveDirection);
                                                }
                                                else if (a < -0.707)
                                                {
                                                    margedUnit.RotateFlame(-loveDirection);
                                                }
                                                else
                                                {
                                                    margedUnit.EngineOnFlame();
                                                }

                                            }
                                        }
                                        else
                                        {
                                            margedUnit.RotateFlame(tm);
                                        }




                                    }
                                    else if (d < settings.minRange)
                                    {
                                        if (a >= 0)
                                        {
                                            margedUnit.RotateFlame(loveDirection);
                                        }
                                        else
                                        {
                                            margedUnit.EngineOnFlame();
                                        }

                                    }

                                }
                                else margedUnit.EngineOnFlame();

                            }
                        }
                        else if(settings.isRotateAble)
                        {
                            if (margedUnit.Position!= Target.Position)
                            {
                                Vector3 tm;
                                tm = Vector3.Normalize(Vector3.Transform(Target.Position + Target.Velocity * elapsedTime, Matrix.Invert(margedUnit.World)));
                                margedUnit.RotateFlame(tm);
                            }

                        }





                    }
                    else
                    {
                        Back();
                    }
                }
                else
                {
                    Back();
                }

                #endregion
            }

        }
        /// <summary>
        /// 判断是否需要射击
        /// </summary>
        /// <returns></returns>
        bool NeedShot()
        {
            bool s = false;

            if (Target != null & settings.isShotAble)
            {

                if (Target.UnitState == UnitState.alive)
                {
                 
                    if (Unit.Distance(margedUnit, Target) <= settings.rangeOfShot )
                    {

                        if (margedUnit.Position != Target.Position)
                        {
                            if (Vector3.Dot(Vector3.Normalize(Target.Position - margedUnit.Position), margedUnit.Face) >= Math.Cos(MathHelper.ToRadians(settings.shotJudgmentAngel)))
                            {
                                s = true;

                            }
                            else
                            {
                                if (margedUnit.IsShotHittable(Target))
                                {
                                    s = true;
                                }
                                //Ray ra;
                                //if (margedUnit.velocity != Vector3.Zero)
                                //{

                                //    ra = new Ray(margedUnit.position, Vector3.Normalize(margedUnit.velocity + margedUnit.CurrentWeapon.basicSpeed * margedUnit.Face));
                                //}
                                //else ra = new Ray(margedUnit.position, margedUnit.Face);

                                //if (Collision.isCollided(Target, ra))
                                //{

                                //    s = true;
                                //}
                            }
                        }
                        else s = true;
                    }
                }

            }
            return s;
        }
        /// <summary>
        /// 判断是否需要移动
        /// </summary>
        /// <returns>是否需要移动</returns>

        void UpdateThreat()
        {
            if (settings.rangeOfView>0)
            {
                Threat -= Unit.Distance(AssistUnit, Target) / settings.rangeOfView * 28;
            }
            //每次仇恨在+10或-10间波动
            Threat += (((float)r.NextDouble()) - 0.25f) * 20.0f;
            if (Unit.Distance(AssistUnit,Target)>=settings.rangeOfView)
            {
                Threat = 0;
            }
        }

        bool DoThreatUpdate(GameTime gameTime)
        {
            timeSinceLastUpdate += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (timeSinceLastUpdate >= settings.updateTimeSpan)
            {
                timeSinceLastUpdate -= settings.updateTimeSpan;
                return true;
            }
            else
            {
                return false;
            }
        }

        void LookForNewTarget(GameWorld gameWorld)
        {
            if (AssistUnit!= null)
            {
                if (AssistUnit.Dead == false)
                {
                    Target = null;
                    List<Unit> AttackAbleUnits = new List<Unit>(0);
                    foreach (Unit u in gameWorld.units)
                    {
                        if (u != null)
                        {
                            if ((Unit.Distance(AssistUnit, u) <= settings.rangeOfView) && (u.Group != margedUnit.Group) && (u.UnitState == UnitState.alive))
                            {
                                AttackAbleUnits.Add(u);
                            }
                        }
                    }

                    if (AttackAbleUnits.Count > 0)
                    {

                        Unit t = AttackAbleUnits[(int)(AttackAbleUnits.Count * r.NextDouble())];
                        if (t != null)
                        {
                            Target = t;
                        }
                    }
                }
        
            }




        }
        void Back() 
        {
            if (AssistUnit!= null )
            {
                if (AssistUnit.Dead== false && Unit.Distance(margedUnit,AssistUnit)>settings.rangeOfCycle)
                {
                    if (settings.isRotateAble)
                    {

                        margedUnit.RotateRealWorldFlame(AssistUnit.Position);
                    }
                    if (settings.isMoveAble)
                    {
                        if (margedUnit.Position != AssistUnit.Position)
                        {
                            if (Vector3.Dot(margedUnit.Face, Vector3.Normalize(AssistUnit.Position - margedUnit.Position)) > 0)
                            {
                                margedUnit.EngineOnFlame();
                            }
                        }
                    }


                    return;
                }
                
            }
            

             if (settings.isSwimable)
            {

                if (settings.isMoveAble && settings.isRotateAble)
                {
                    if (margedUnit.Speed < (margedUnit.MaxSpeed / 4))
                    {

                        margedUnit.EngineOnFlame();
                    }
                    else
                    {
                        margedUnit.RotateFlame(loveDirection);
                        margedUnit.EngineOnFlame();
                    }
                }
            }


        }
        ///// <summary>
        ///// 进入援护模式并设置协助目标
        ///// </summary>
        ///// <param name="AT"></param>
        ///// <returns></returns>
        //public void SetAssist(Unit AT, float range)
        //{
        //    AssistTarget = AT;
        //    AssistRange = range;
        //}
        //public void CancelAssist()
        //{
        //    AssistTarget = null;
        //}
    }
}
