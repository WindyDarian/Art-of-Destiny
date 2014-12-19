using System;
using System.Collections.Generic;

using System.Text;
using AODGameLibrary.Units;
using Microsoft.Xna.Framework;
using AODGameLibrary.Cameras;
using AODGameLibrary.Weapons;
using AODGameLibrary.Gamehelpers;
using AODGameLibrary.Effects;
using Microsoft.Xna.Framework.Graphics;
using AODGameLibrary.Interface;
using AODGameLibrary.AODObjects;
using AODGameLibrary.GamePlay;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Audio;
using AODGameLibrary.Effects.ParticleShapes;
using AODGameLibrary.Ambient;
using AODGameLibrary.AIs;
namespace Stages.OtherParts
{
    /// <summary>
    /// BOSS战-末日的StagePart,由大地无敌-范若余在2009年10月27日建立
    /// </summary>
    class Doom:StagePart
    {
        bool battleBegin = false;
        bool phase1On = false;
        bool phase2On = false;
        bool phase3On = false;
        List<Unit> adds = new List<Unit>(6);
        Unit boss;
        Timer ti1;
        public override void Initialize()
        {
            
            Stage.CreateNPCUnit(Content.Load<UnitType>(@"UnitTypes\Falcon"), 1, Vector3.Zero + new Vector3(0, 0, -100));
            Variables.LastCreatedUnit.RiderName = "比尔.沃顿";
            Variables.LastCreatedUnit.IsInvincible = true;
            Variables.LastCreatedUnit.EndlessBullets = false;
            //Stage.Player.IsInvincible = true;

            Stage.CreateNPCUnit(Content.Load<UnitType>(@"UnitTypes\Falcon3"), 1, Vector3.Zero + new Vector3(0, 0, -200));
            Variables.LastCreatedUnit.RiderName = "玛莲娜.林风";
            Variables.LastCreatedUnit.EndlessBullets = false;
            Stage.CreateNPCUnit(Content.Load<UnitType>(@"UnitTypes\Falcon"), 1, Vector3.Zero + new Vector3(0, 0, -300));
            Variables.LastCreatedUnit.EndlessBullets = false;
            Variables.LastCreatedUnit.RiderName = "马克斯.派恩";

            Stage.CreateNPCUnit(Content.Load<UnitType>(@"UnitTypes\Doom"), 3, new Vector3(0, 0, -2000));
            boss = Variables.LastCreatedUnit;
            ((RegularAI)boss.unitAI).settings.isShotAble = false;
            ((RegularAI)boss.unitAI).Target = Variables.Player;
            ((RegularAI)boss.unitAI).TargetChangeAble = false;
            ti1 = Stage.CreateTimer(12.5f);
            Stage.AddGameMessage("末日：末日降临！", Color.Red, 3);
            Stage.PlayMusic(@"Audio\Antti_Martikainen_-_The_Chase", true, 5);
            battleBegin = true;
            phase1On = true;
        
            Stage.CreateLoot(Content.Load<LootSettings>(@"Loots\PAGAmmoSmall"), new Vector3(600, 0, 0) + boss.Position);
            Stage.CreateLoot(Content.Load<LootSettings>(@"Loots\PAGAmmoSmall"), new Vector3(-600, 0, 0) + boss.Position);
            Stage.CreateLoot(Content.Load<LootSettings>(@"Loots\PAGAmmoSmall"), new Vector3(0, 600, 0) + boss.Position);
            Stage.CreateLoot(Content.Load<LootSettings>(@"Loots\PAGAmmoSmall"), new Vector3(0, -600, 0) + boss.Position);
            Stage.CreateLoot(Content.Load<LootSettings>(@"Loots\PAGAmmoSmall"), new Vector3(0, 0, -600) + boss.Position);
            Stage.CreateLoot(Content.Load<LootSettings>(@"Loots\PAGAmmoSmall"), new Vector3(0, 0, -600) + boss.Position);

            base.Initialize();
        }
        /// <summary>
        /// 从这里中途读入
        /// </summary>
        public override void StartFormThis()
        {
            Stage.Player.Position = new Vector3(0, 0, 2000);


            base.StartFormThis();
        }
        public override void Touch()
        {
            if (boss.UnitState == UnitState.alive)
            {

                if (battleBegin)
                {
                    Skill s = boss.SkillFromName("轨道炮");
                    if (s != null)
                    {
                        if (s.IsSkillUsable && boss.IsUsingSkill == false)
                        {
                            boss.CastSkill(s);
                            Stage.AddRealtimeGameMessage("末日：轨道炮，充能完毕。", Color.Red, 2);
                            if (phase3On)
                            {

                                ti1.Reset();
                            }

                        }
                    }
                    Skill g = boss.SkillFromName("空间吸引");
                    if (g != null)
                    {
                        if (g.IsCooldowned && boss.IsUsingSkill == false)
                        {
                            foreach (Unit u in Stage.Units)
                            {
                                if (Unit.Distance(boss, u) > 2300 && u.Group != boss.Group)
                                {
                                    boss.CastSkill(g, u);
                                    Stage.AddRealtimeGameMessage("末日：直面末日吧！", Color.Red, 2);
                                    break;
                                }
                            }
                        }
                    }

                }
                if (phase1On && battleBegin)
                {
                    if (boss.Armor <= 12000)
                    {
                        Stage.PlayMusic(@"Audio\Kai_Engel_-_Beneath_The_Stronghold", true, 5);
                        boss.Shield = 12000;
                        boss.IsInvincible = true;
                        ((RegularAI)boss.unitAI).settings.isMoveAble = false;
                        Stage.ClearMessages();
                        Stage.AddRealtimeGameMessage("末日：检测到目标对本系统构成威胁。", Color.Red, 3);
                        Stage.AddRealtimeGameMessage("末日：释放防御力场产生器，防御模式启动中……", Color.Red, 3);
                        phase1On = false;
                        phase2On = true;
                        Stage.CreateNPCUnit(Content.Load<UnitType>(@"UnitTypes\DoomShield"), 3, new Vector3(500, 0, 0) + boss.Position);
                        adds.Add(Variables.LastCreatedUnit);
                        Stage.CreateNPCUnit(Content.Load<UnitType>(@"UnitTypes\DoomShield"), 3, new Vector3(-500, 0, 0) + boss.Position);
                        adds.Add(Variables.LastCreatedUnit);
                        Stage.CreateNPCUnit(Content.Load<UnitType>(@"UnitTypes\DoomShield"), 3, new Vector3(0, 500, 0) + boss.Position);
                        adds.Add(Variables.LastCreatedUnit);
                        Stage.CreateNPCUnit(Content.Load<UnitType>(@"UnitTypes\DoomShield"), 3, new Vector3(0, -500, 0) + boss.Position);
                        adds.Add(Variables.LastCreatedUnit);
                        Stage.CreateNPCUnit(Content.Load<UnitType>(@"UnitTypes\DoomShield"), 3, new Vector3(0, 0, 500) + boss.Position);
                        adds.Add(Variables.LastCreatedUnit);
                        Stage.CreateNPCUnit(Content.Load<UnitType>(@"UnitTypes\DoomShield"), 3, new Vector3(0, 0, -500) + boss.Position);
                        adds.Add(Variables.LastCreatedUnit);
                    }
                }
                if (phase2On)
                {
                    if (adds.Count == 0)
                    {
                        Stage.PlayMusic(@"Audio\Antti_Martikainen_-_Through_Enemy_Lines", true, 5);
                        Stage.AddRealtimeGameMessage("末日：防御力场被破坏，防御模式关闭，狂暴模式启动中……", Color.Red, 3);
                        phase2On = false;
                        phase3On = true;
                        ((RegularAI)boss.unitAI).settings.isMoveAble = true;
                        boss.IsInvincible = false;
                        boss.angularRate = 20.0f;

                    }
                }
                if (phase3On)
                {

                }
            }
        }
        public override void Event_TimerRing(Timer timer)
        {
            if (timer == ti1 && boss.UnitState != UnitState.dead && phase3On)
            {
                Skill b = boss.SkillFromName("末日之雷");
                if (b.IsSkillUsable && boss.IsUsingSkill == false)
                {
                    boss.CastSkill(b);
                    Stage.AddRealtimeGameMessage("末日：末-日-之-雷！", Color.Red, 2);


                }
            }
        }
        public override void Event_UnitDied(Unit deadUnit)
        {
            if (phase2On && adds.Contains(deadUnit))
            {
                adds.Remove(deadUnit);
            }
            if (deadUnit == boss)
            {
                Stage.AddRealtimeGameMessage("末日：计算……偏差……系统……崩溃", Color.Red, 3);
                Stage.ScreenEffectManager.Blink(Color.Orange, 2);
                Stage.Victory("胜利！！");
                Stage.SavePlayer();
            }
            
        }
    }
}
