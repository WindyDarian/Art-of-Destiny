using System;
using System.Linq;
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
using System.Collections.Generic;
using AODGameLibrary.Texts;

namespace Stages.Stage4Parts
{
    /// <summary>
    /// 由大地无敌-范若余于2010年5月30日建立
    /// </summary>
    public class Stage4_Part4 : StagePart
    {
        Timer t;
        Unit boss;
        bool b2;
        bool b3;
        bool b4;
        bool won;


        /// <summary>
        /// 初始化
        /// </summary>
        public override void Initialize()
        {
            Stage.GameWorld.MovieModelStart();
            Stage.ScreenEffectManager.Blink(Color.Black, 4);
            Stage.Player.Position = new Vector3(0, 0, 5200);//初始位置
            Stage.Player.SetMoveState(new Vector3(0, 0, 0), new Vector3(0, 180, 0));
            Variables.Unit[2].Position = new Vector3(40, 10, 5200);
            Variables.Unit[2].SetMoveState(new Vector3(0, 0, 0), new Vector3(0, 180, 0));

            Variables.Unit[3].Position = new Vector3(-40, -10, 5200);
            Variables.Unit[3].SetMoveState(new Vector3(0, 0, 0), new Vector3(0, 180, 0));
            Variables.Unit[3].EndlessBullets = false;

            Variables.Unit[4].Position = new Vector3(-40, 10, 5200);
            Variables.Unit[4].SetMoveState(new Vector3(0, 0, 0), new Vector3(0, 180, 0));
            Variables.Unit[4].EndlessBullets = false;

            Stage.Player.Restore();

            t = Stage.CreateTimer(2);
            
            base.Initialize();
        }
        /// <summary>
        /// 从该片段继续时进行的处理
        /// </summary>
        public override void StartFormThis()
        {



            Stage.CreateNPCUnit(Content.Load<UnitType>(@"UnitTypes\Falcon2"), 1, new Vector3(0, 20000, 0));
            Variables.LastCreatedUnit.IsInvincible = true;
            Variables.LastCreatedUnit.RiderName = "普雷斯.杰斐逊 <第三舰队指挥官>";
            Variables.Unit[3] = Variables.LastCreatedUnit;

            Stage.CreateNPCUnit(Content.Load<UnitType>(@"UnitTypes\Falcon3"), 1, new Vector3(0, 20000, 0));
            Variables.LastCreatedUnit.IsInvincible = true;
            Variables.LastCreatedUnit.RiderName = "玛莲娜.林风 <第三舰队>";
            Variables.Unit[4] = Variables.LastCreatedUnit;
            Variables.LastCreatedUnit.SetAI(new AODGameLibrary2.AssistAI(((RegularAI)Variables.LastCreatedUnit.unitAI).settings, Variables.Unit[3]));
           

            

            base.StartFormThis();
        }
        /// <summary>
        /// 关卡判断
        /// </summary>
        public override void Touch()
        {
            if (won)
            {
                Stage.Victory("哈哈！");
                won = false;
            }
            if (boss!= null )
            {
                if (!b4)
                {

                    if (b2)
                    {
                        Skill s = boss.SkillFromName("能量爆发");
                        if (s != null)
                        {
                            if (s.IsSkillUsable && boss.IsUsingSkill == false)
                            {
                                boss.CastSkill(s);
                                Stage.AddRealtimeGameMessage("克雷斯：“遗迹”的力量，为我所用！", Color.Red, 2);

                            }
                        }
                    }
                    if (b3)
                    {
                        Skill s = boss.SkillFromName("墓地震击MK2");
                        if (s != null)
                        {
                            if (s.IsSkillUsable && boss.IsUsingSkill == false)
                            {
                                Stage.AddRealtimeGameMessage("克雷斯：炸裂呀，宇宙！", Color.Red, 2);
                                boss.CastSkill(s);

                            }
                        }
                    }
                    {
                        Skill s = boss.SkillFromName("充能弹");
                        if (s != null)
                        {
                            if (s.IsSkillUsable && boss.IsUsingSkill == false)
                            {
                                boss.CastSkill(s);
                                Stage.AddRealtimeGameMessage("克雷斯：燃烧吧！", Color.Red, 2);


                            }
                        }
                    }
                    {
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
                                        Stage.AddRealtimeGameMessage("克雷斯：上哪儿去？", Color.Red, 2);
                                        break;
                                    }
                                }
                            }
                        }
                    }
                }


                if (!b2 && boss.Shield < 25000)
                {
                    Stage.AddGameMessage(@"………………", Color.White, 4);
                    Stage.AddGameMessage(@"克雷斯：威克多算什么？除了一个野心家，他什么都不是。", Color.Red, 4);
                    Stage.AddGameMessage(@"克雷斯：想知道他的“伟大”计划么？", Color.Red, 4);
                    Stage.AddGameMessage(@"克雷斯：放心吧，我不会把真相告诉即将通向天堂的杂种！", Color.Red, 4);
                    Stage.AddGameMessage(@"Alicia: 天堂……", Color.Yellow, 4);
                    Stage.AddGameMessage(@"克雷斯：把杰诺和“遗迹”交给我，然后去死吧！", Color.Red, 4);
                    Stage.AddGameMessage(@"玛莲娜.林风：看来你需要冷静一下……", Color.CornflowerBlue, 4);
                    b2 = true;
                }
                else if (!b3 && boss.Shield < 10000)
                {
                    Stage.AddGameMessage(@"………………", Color.White, 4);
                    Stage.AddGameMessage(@"克雷斯：知道秩序之眼是如何被制造出来的吗？", Color.Red, 4);
                    Stage.AddGameMessage(@"Alicia: 这是……最高机密……求求你……", Color.Yellow, 4);
                    Stage.AddGameMessage(@"克雷斯：光靠计算机作为核心，是不可能制造出现在的秩序之眼的。", Color.Red, 4);
                    Stage.AddGameMessage(@"克雷斯：所以……需要一项技术，将人类的智慧和计算机的计算能力结合起来……", Color.Red, 4);
                    Stage.AddGameMessage(@"克雷斯：“遗迹”便是关键的钥匙！", Color.Red, 4);
                    Stage.AddGameMessage(@"克雷斯：当然，根据我刚刚入手的档案，还需要一个神圣的牺牲者与秩序之眼结合。", Color.Red, 4);
                    Stage.AddGameMessage(@"Alicia: 停下来！", Color.Yellow, 4);
                    Stage.AddGameMessage(@"克雷斯：这个牺牲者是一个女孩——", Color.Red, 4);
                    Stage.AddGameMessage(@"Zero: 难道说——", Color.LightGreen, 2);
                    Stage.AddGameMessage(@"Alicia: 不！", Color.Yellow, 4);
                    Stage.AddGameMessage(@"克雷斯：她的名字叫——", Color.Red, 4);
                    Stage.AddGameMessage(@"Bill: 阿莉西亚。", Color.CornflowerBlue, 4);
                    Stage.AddGameMessage(@"Zero: ！", Color.LightGreen, 2);
                    Stage.AddGameMessage(@"克雷斯：回答正确，不过你的回答是多余的，比尔。", Color.Red, 4);
                    Stage.AddGameMessage(@"Alicia: ……哈……哈哈……哈哈哈哈……", Color.Yellow, 4);
                    Stage.PlayMusic(@"Audio\Antti_Martikainen_-_Through_Enemy_Lines", true, 2);
                    b3 = true;
                }
                else if (!b4 && boss.Armor < 45000 && boss.Shield < 10000)
                {
                    Stage.PlayMusic(@"Audio\TitanSlayer_-_Dawning_of_Darkness", true, 3);
                    Stage.AddGameMessage(@"………………", Color.White, 4);
                    Stage.AddGameMessage(@"克雷斯：威克多想要统治世界，但是他做不到，因为他的棋子马上就会被干掉。", Color.Red, 4);
                    Stage.AddGameMessage(@"克雷斯：秩序，属于我！", Color.Red, 4);
                    Stage.AddGameMessage(@"Alicia: 你……休想。", Color.Yellow, 4);
                    Stage.AddGameMessage(@"克雷斯：什么？", Color.Red, 4);
                    Stage.AddGameMessage(@"Alicia: 秩序之剑，出击！", Color.Yellow, 4);
                    Stage.AddGameMessage(@"克雷斯：不——不——不！", Color.Red, 4);
                    for (int i = 0; i < 10; i++)
                    {
                        Stage.CreateNPCUnit(Content.Load<UnitType>(@"UnitTypes\EyesElite"), 1, Stage.Player.Position + -2000 * Stage.Player.Face);
                    }
                    b4 = true;
                }
            }
        }
        public override void Event_UnitDied(Unit deadUnit)
        {
            if (deadUnit == boss )
            {
                Stage.SavePlayer();
                Stage.EnableStage(5);
                Stage.StopMusic();
                Stage.AddGameMessage(@"………………", Color.White, 4);
                Stage.AddGameMessage("克雷斯：这怎么……可能……", Color.Red, 3);
                Stage.AddGameMessage("克雷斯：听着……“遗迹”已经被威克多改造过……", Color.Red, 3);
                Stage.AddGameMessage("克雷斯：如果“遗迹”接触秩序之眼……就……会……", Color.Red, 3);
                Stage.AddGameMessage("克雷斯：啊…………", Color.Red, 3);
                Stage.ScreenEffectManager.Blink(Color.White, 2);
                Stage.ScreenEffectManager.KeepColor(Color.Black, null);


                Stage.StopMusic();
                Stage.Player.isPlayerControlling = false;
                Stage.Player.IsInvincible = true;

     

                Stage.AddGameMessage(@"Alicia: 对不起，我就是……", Color.Yellow, 3);
                Stage.AddGameMessage(@"Bill: 秩序之眼，告诉我们你的真正意图吧。", Color.CornflowerBlue, 4);
                Stage.AddGameMessage(@"玛莲娜.林风：你，就是想要毁灭人类的，秩序之眼？", Color.CornflowerBlue, 4);
                Stage.AddGameMessage(@"Alicia: 这并不是我的意愿。", Color.Yellow, 3);
                Stage.AddGameMessage(@"Alicia: 秩序之眼，早在刚刚被制造的时候就被添加了某种隐藏的程序。", Color.Yellow, 3);
                Stage.AddGameMessage(@"Alicia: 这个程序取代了我对大部分秩序之眼军队的控制权，对人类发动了进攻……", Color.Yellow, 3);
                Stage.AddGameMessage(@"Alicia: 我一直在调查秩序之眼被植入这个程序的原因，并试图挽回一切。", Color.Yellow, 3);
                Stage.AddGameMessage(@"Alicia: 经过调查，幕后黑手……是地球联盟指挥官威克多。", Color.Yellow, 3);
                Stage.AddGameMessage(@"Alicia: 威克多还需要一个机会，一个能完全控制秩序之眼的军队的机会。", Color.Yellow, 3);
                Stage.AddGameMessage(@"Alicia: 而这个机会，就在现在……", Color.Yellow, 3);
                Stage.AddGameMessage(@"Alicia: 如果“遗迹”接触到秩序之眼，威克多就能控制秩序之眼的所有军队，", Color.Yellow, 3);
                Stage.AddGameMessage(@"Alicia: 他将统治世界……", Color.Yellow, 3);
                Stage.AddGameMessage(@"Alicia: 我不希望这件事发生……", Color.Yellow, 3);
                Stage.AddGameMessage(@"Alicia: 相信我……", Color.Yellow, 3);
                Stage.AddGameMessage(@"Bill: 恕我不能相信你，", Color.CornflowerBlue, 4);
                Stage.AddGameMessage(@"Bill: 我们还有任务需要完成。", Color.CornflowerBlue, 4);
                won = true;
            }
            base.Event_UnitDied(deadUnit);
        }
        public override void Event_TimerRing(Timer timer)
        {
            if (timer == t)
            {
                Stage.ClearMessages();
                Stage.PlayMusic(@"Audio\Kai_Engel_-_Beneath_The_Stronghold", true, 2);
                Stage.AddGameMessage(@"Bill: 克雷斯，又是你！", Color.CornflowerBlue, 2);
                Stage.AddGameMessage(@"克雷斯：看来……你们还是被蒙在鼓里呀……", Color.Red, 4);
                Stage.AddGameMessage(@"克雷斯：不管是威克多，还是那个阿莉西亚，都想让你们做一个好梦……", Color.Red, 4);
                Stage.AddGameMessage(@"Alicia: 我——", Color.Yellow, 4);
                Stage.AddGameMessage(@"克雷斯：该是醒醒的时候了。", Color.Red, 4);
                Stage.AddGameMessage(@"普雷斯.杰斐逊：教会的纱布！准备好吃子弹吧！", Color.CornflowerBlue, 4);
                Stage.AddGameMessage(@"Bill: 第三舰队，攻击！", Color.CornflowerBlue, 4);
                Stage.GameWorld.MovieModelEnd();
                Stage.CreateNPCUnit(Content.Load<UnitType>(@"UnitTypes\DemonScream"), 4, new Vector3(0, 0, 6500));
                Variables.LastCreatedUnit.RiderName = "黑暗大主教克雷斯";
                boss = Variables.LastCreatedUnit;

               
            }
            base.Event_TimerRing(timer);
        }

    }
}
