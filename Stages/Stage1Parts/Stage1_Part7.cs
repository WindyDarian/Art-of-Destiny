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
using Microsoft.Xna.Framework.Content;
using AODGameLibrary.AIs;

namespace Stages.Stage1Parts
{
    /// <summary>
    /// 最后的BOSS战,由大地无敌-范若余在2009年11月9日建立
    /// </summary>
    public class Stage1_Part7 : StagePart
    {
        Vector3 destination = new Vector3(0, 0, -50000);
        Unit bill;
        Unit rudolf;
        Unit je;
        Unit boss;
        bool b1 = false;
        bool b2 = false;
        bool b3 = false;
        bool b4 = false;
        Timer t1;
        Timer t2;
        Timer t3;
        Mark m;
        bool battleBegin = false;
        bool phase1On = false;
        bool phase2On = false;
        bool phase3On = false;
        List<Unit> adds = new List<Unit>(6);
        Timer ti1;
        Timer bossStart;
        Timer railgun;
        Timer x;
        bool won = false;
        /// <summary>
        /// 初始化
        /// </summary>
        public override void Initialize()
        {
            Stage.PlayMusic(@"Audio\Marcello_Morgese_-_Sounds_Of_The_Night", true, 4);
            if (Variables.Unit[2] != null)
            {

                bill = Variables.Unit[2];
            }
            if (Variables.Unit[3]!= null)
            {
                rudolf = Variables.Unit[3];
            }
            if (Variables.Unit[4] != null)
            {
                je = Variables.Unit[4];
            }
            Stage.AddGameMessage(@"指挥官鲁道夫：又来了！它们始终就没有放弃过进攻！", Color.CornflowerBlue, 3);
            Stage.AddGameMessage(@"比尔：我们被包围了，小心四周！", Color.CornflowerBlue, 3);

            Stage.CreateNPCUnit(Stage.Content.Load<UnitType>(@"UnitTypes\ShadowSlain"), 2, Stage.Player.Position + new Vector3(0, 0, -6000));
            Stage.CreateNPCUnit(Stage.Content.Load<UnitType>(@"UnitTypes\ShadowSlain"), 2, Stage.Player.Position + new Vector3(0, 0, 6200));
            Stage.CreateNPCUnit(Stage.Content.Load<UnitType>(@"UnitTypes\ShadowSlain"), 2, Stage.Player.Position + new Vector3(0, -6400, 0));
            Stage.CreateNPCUnit(Stage.Content.Load<UnitType>(@"UnitTypes\FlameDestroyer"), 2, Stage.Player.Position + new Vector3(0, 6500, 0));
            Stage.CreateNPCUnit(Stage.Content.Load<UnitType>(@"UnitTypes\EyesElite"), 2, Stage.Player.Position + new Vector3(6900, 0, 0));
            Stage.CreateNPCUnit(Stage.Content.Load<UnitType>(@"UnitTypes\FlameDestroyer"), 2, Stage.Player.Position + new Vector3(4000, 2000, 0));
            Stage.CreateNPCUnit(Stage.Content.Load<UnitType>(@"UnitTypes\EyesElite"), 2, Stage.Player.Position + new Vector3(4999, 2111, 0));
            t1 = Stage.CreateTimer(10);
            base.Initialize();
        }
        /// <summary>
        /// 从该片段继续时进行的处理
        /// </summary>
        public override void StartFormThis()
        {
            Stage.Player.Position = new Vector3(0, 0, -50000);//初始位置
            Stage.CreateNPCUnit(Content.Load<UnitType>(@"UnitTypes\Falcon"), 1, new Vector3(0, -100, -50000));
            Variables.Unit[2] = Variables.LastCreatedUnit;
            bill = Variables.Unit[2];
            bill.IsInvincible = true;
            bill.RiderName = "比尔.沃顿";
            Stage.CreateNPCUnit(Content.Load<UnitType>(@"UnitTypes\Falcon2"), 1, destination + new Vector3(100, 100, 100));
            rudolf = Variables.LastCreatedUnit;
            Variables.Unit[3] = Variables.LastCreatedUnit;
            rudolf.IsInvincible = true;
            rudolf.RiderName = "鲁道夫.巴特尔";
            Stage.CreateNPCUnit(Content.Load<UnitType>(@"UnitTypes\Falcon"), 1, destination + new Vector3(100, -100, 100));
            je = Variables.LastCreatedUnit;
            Variables.Unit[4] = Variables.LastCreatedUnit;
            je.RiderName = "普雷斯.杰斐逊";
            je.IsInvincible = true;
            Stage.CreateNPCUnit(Content.Load<UnitType>(@"UnitTypes\Falcon"), 1, destination + new Vector3(-100, 100, 100));
            Variables.Unit[7] = Variables.LastCreatedUnit;
            Variables.LastCreatedUnit.RiderName = "玛莲娜.林风";
            Stage.CreateNPCUnit(Content.Load<UnitType>(@"UnitTypes\Falcon3"), 1, destination + new Vector3(100, -200, 100));
            Variables.Unit[8] = Variables.LastCreatedUnit;
            Variables.LastCreatedUnit.RiderName = "马克斯.派恩";

            //Stage.Player.GetWeapon(@"WeaponTypes\Blav", 25);
            Stage.Player.AddSkill(@"Skills\Thruster");
            base.StartFormThis();
        }
        /// <summary>
        /// 关卡判断
        /// </summary>
        public override void Touch()
        {
            if (b1 == false)
            {
                if (t1.IsEnd)
                {
                    Stage.PlayMusic(@"Audio\Antti_Martikainen_-_The_Chase", true, 5);
                    if (Variables.Unit[9]!= null)
                    {
                        
                    Variables.Unit[9].BeginToDie();
                    }
                    if (Variables.Unit[10] != null)
                    {

                        Variables.Unit[10].BeginToDie();
                    }
                    Variables.Switch[13] = true;
                    Stage.CreateNPCUnit(Stage.Content.Load<UnitType>(@"UnitTypes\ShadowSlain"), 2, Stage.Player.Position + new Vector3(0, -3000, -4200));
                    Stage.CreateNPCUnit(Stage.Content.Load<UnitType>(@"UnitTypes\FlameDestroyer"), 2, Stage.Player.Position + new Vector3(4000, 2000, 0));
                    Stage.CreateNPCUnit(Stage.Content.Load<UnitType>(@"UnitTypes\ShadowSlain"), 2, Stage.Player.Position + new Vector3(0, -3000, -4200));
                    Stage.CreateNPCUnit(Stage.Content.Load<UnitType>(@"UnitTypes\EyesElite"), 2, Stage.Player.Position + new Vector3(4000, 2000, 0));
                    Stage.ScreenEffectManager.Blink(Color.Black, 3);
        
                    t3 = Stage.CreateTimer(4);
                    Stage.CreateNPCUnit(Stage.Content.Load<UnitType>(@"UnitTypes\Doom"), 3, destination + new Vector3(4000, 0, 0));
                    boss = Variables.LastCreatedUnit;
                    boss.IsInvincible = true;
                    foreach (Unit u in Stage.AliveUnitsInUnitGroup(2))
                    {
                        u.unitAI.Target = Variables.LastCreatedUnit;
                        u.EndlessBullets = false;
                        u.CurrentWeapon.AmmoNum = 100;
                        ((RegularAI)u.unitAI).TargetChangeAble = false;
                    }
                    foreach (Unit u in Stage.AliveUnitsInUnitGroup(1))
                    {
                        if (u != Stage.Player)
                        {

                            u.unitAI.Target = Variables.LastCreatedUnit;
                        }
                    }
                    t2 = Stage.CreateTimer(32);

                    boss.IsAIControlling = false;
                    boss.MoveTo(destination);
                    b1 = true;
                }
            }
            if (boss != null)
            {


                if (boss.UnitState == UnitState.alive)
                {

                    if (battleBegin)
                    {
                        if (railgun.IsEnd)
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

                            bill.CurrentWeapon.AmmoNum += 2000;
                            Stage.PlayMusic(@"Audio\Kai_Engel_-_Beneath_The_Stronghold", true, 5);
                            boss.Shield = 12000;
                            boss.IsInvincible = true;
                            ((RegularAI)boss.unitAI).settings.isMoveAble = false;
                            Stage.ClearMessages();
                            Stage.AddRealtimeGameMessage("末日：检测到目标对本系统构成威胁。", Color.Red, 3);
                            Stage.AddRealtimeGameMessage("末日：释放防御力场产生器，防御模式启动中……", Color.Red, 3);
                            Stage.AddRealtimeGameMessage(@"阿莉西亚：这些东西构成了一个坚固的护盾……", Color.Yellow, 3);
                            Stage.AddRealtimeGameMessage(@"阿莉西亚：最简单的办法是击垮它们。", Color.Yellow, 3);
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
                            Stage.AddRealtimeGameMessage(@"阿莉西亚：那么，现在就火力全开吧。", Color.Yellow, 3);
                            phase2On = false;
                            phase3On = true;
                            ((RegularAI)boss.unitAI).settings.isMoveAble = true;
                            boss.IsInvincible = false;
                            boss.angularRate = 20.0f;
                            bill.CurrentWeapon.AmmoNum += 1500;

                        }
                    }
                    if (phase3On)
                    {

                    }
                }
            }
            if (won )
            {
                if (Stage.IsMessageEnd)
                {
                    Stage.Victory("胜利！！");
                }
                
            }
        }
        public override void Event_TimerRing(Timer timer)
        {
            if (timer == t2)
            {
                boss.CastSkill("末日之雷");
                foreach (Unit u in Stage.AliveUnitsInUnitGroup(2))
                {
                    u.Shield = 0;
                    u.ShieldRestoreRate = 0;
                    u.Armor = 1;
                }
                if (Variables.Unit[7] != null)
                {

                    ((RegularAI)Variables.Unit[7].unitAI).settings.isShotAble = false;
                   ((RegularAI)Variables.Unit[7].unitAI).settings.isSkillUsable = false;
                }
                if (Variables.Unit[8] != null)
                {

                    Variables.Unit[8].BeginToDie();
                }
                rudolf.IsInvincible = false;
                    boss.IsAIControlling = true;
                bossStart = Stage.CreateTimer(16);
                x = Stage.CreateTimer(5);
            }
            if (timer==x)
            {
                Stage.AddGameMessage(@"阿莉西亚：傻瓜，你们根本不知道怎样对付这个家伙，现在没人能伤到它。", Color.Yellow, 4);
                Stage.AddGameMessage(@"杰诺：难道你知道怎么对付它？小女孩？", Color.LightGreen, 3);
                Stage.AddGameMessage(@"阿莉西亚：我不是什么小女孩，我叫阿莉西亚，附近星域的黑客。", Color.Yellow, 4);
                Stage.AddGameMessage(@"阿莉西亚：“遗迹”就在那个大家伙里面，它现在干扰了几乎所有的正常通信。", Color.Yellow, 4);
                Stage.AddGameMessage(@"阿莉西亚：我正在使用一个特殊的频道和你联系。只有你能听见我说话。", Color.Yellow, 4);
                Stage.AddGameMessage(@"阿莉西亚：靠近它，不要犹豫。照我说的做，然后打败它。", Color.Yellow, 4);
                Stage.AddGameMessage(@"阿莉西亚：它的防御系统基本上已经被我破解，再过几秒就可以被普通武器损伤了。", Color.Yellow, 4);
           
            }
            if (timer == bossStart)
            {
                boss.IsInvincible = false;
                boss.Stop();
                boss.IsAIControlling = true;
                ((RegularAI)boss.unitAI).settings.isShotAble = false;
                boss.unitAI.Target = Stage.Player;
                ((RegularAI)boss.unitAI).TargetChangeAble = false;
                //boss.CastSkill("末日之雷");
                ti1 = Stage.CreateTimer(12.5f);
                Stage.AddRealtimeGameMessage("末日：末日降临！", Color.Red, 3);
                foreach (Unit u in Stage.AliveUnitsInUnitGroup(1))
                {
                    if (u!=Stage.Player)
                    {
                        u.EndlessBullets = false;
                    }
                }

                battleBegin = true;
                phase1On = true;
                Stage.AddRealtimeGameMessage(@"阿莉西亚：它注意到你了，避开它的头部。", Color.Yellow, 4);
                railgun = Stage.CreateTimer(9);

            }
            if (timer == ti1 && boss.UnitState != UnitState.dead && phase3On)
            {
                Skill b = boss.SkillFromName("末日之雷");
                if (b.IsSkillUsable && boss.IsUsingSkill == false)
                {
                    boss.CastSkill(b);
                    Stage.AddRealtimeGameMessage("末日：末-日-之-雷！", Color.Red, 2);


                }
            }
            if (timer == t3)
            {
                Stage.AddGameMessage(@"指挥官鲁道夫：这是怎么回事？", Color.CornflowerBlue, 2);
                Stage.AddGameMessage(@"战士杰斐逊：看来抢走你所运送的“遗迹”的那个大家伙想你了……", Color.CornflowerBlue, 3);
                Stage.AddGameMessage(@"比尔：一艘战列舰，载有轨道炮，我们几乎不可能在它面前活过一分钟。", Color.CornflowerBlue, 4);
                Stage.AddGameMessage(@"杰诺：奇怪——它们好像在自相残杀？", Color.LightGreen, 2);
                Stage.AddGameMessage(@"末日：接受命令——毁灭一切——", Color.Red, 4);
                Stage.AddGameMessage(@"指挥官鲁道夫：不！我们没有希望了！", Color.CornflowerBlue, 3);
                Stage.AddGameMessage(@"……信号受到强烈干扰，无法通信……", Color.White, 4);
            }
            base.Event_TimerRing(timer);
        }
        public override void Event_UnitDied(Unit deadUnit)
        {
            if (phase2On && adds.Contains(deadUnit))
            {
                adds.Remove(deadUnit);
            }
            if (deadUnit == boss)
            {
                Stage.ClearMessages();
                Stage.AddGameMessage("末日：计算……偏差……系统……崩溃", Color.Red, 3);
                
                Stage.ScreenEffectManager.Blink(Color.White, 2);
                Stage.ScreenEffectManager.KeepColor(Color.Black, null);


                Stage.StopMusic();
                Stage.Player.isPlayerControlling = false;
                Stage.Player.IsInvincible = true;

                if (rudolf.Dead == false)
                {
                    rudolf.BeginToDie();
                }

                Stage.AddGameMessage(@"阿莉西亚：你做到了！", Color.Yellow, 3);
                Stage.AddGameMessage(@"比尔：通讯暂时恢复了，杰诺，你带领我们击败了它。", Color.CornflowerBlue, 4);
                Stage.AddGameMessage(@"战士杰斐逊：呼叫第三舰队指挥部，首席指挥官鲁道夫阵亡，已确认。", Color.CornflowerBlue, 4);
                Stage.AddGameMessage(@"杰诺：等等，这光芒，看来遗迹的能量越来越不稳定了", Color.LightGreen, 3);
                Stage.AddGameMessage(@"阿莉西亚：你说对了，杰诺，赶快回收它。在“遗迹”变得无法控制之前。", Color.Yellow, 4);
                Stage.AddGameMessage(@"……信号异常，空间异常……", Color.White, 4);
                Stage.AddGameMessage(@"杰诺：可恶，这是什么东西，虫洞？", Color.LightGreen, 3);
                Stage.AddGameMessage(@"阿莉西亚：“遗迹”的不稳定能量开启了虫洞……", Color.Yellow, 4);
                Stage.AddGameMessage(@"阿莉西亚：糟糕！“遗迹”已经被吸入虫洞里了！", Color.Yellow, 4);
                Stage.AddGameMessage(@"比尔：该死！必须把那个东西拿回来！", Color.CornflowerBlue, 4);
                Stage.AddGameMessage(@"阿莉西亚：否则人类就玩完了。", Color.Yellow, 4);
                Stage.AddGameMessage(@"杰诺：交给我了，在虫洞还没有关闭之前……", Color.LightGreen, 3);
                Stage.AddGameMessage(@"阿莉西亚：你是对的，杰诺，回收“遗迹”，将它装进你的战机……", Color.Yellow, 4);
                Stage.AddGameMessage(@"阿莉西亚：我知道地球联盟现在有一个关于“遗迹”的大计划……", Color.Yellow, 4);
                Stage.AddGameMessage(@"阿莉西亚：你和别人不同，杰诺……", Color.Yellow, 4);
                Stage.AddGameMessage(@"阿莉西亚：到那时，你的命运将会真正揭开……", Color.Yellow, 4);

                won = true;

                Stage.SavePlayer();
                Stage.EnableStage(2);
            }

        }

    }
}
