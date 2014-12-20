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
using AODGameLibrary.Ambient;



namespace Stages.Stage5Parts
{
     
    /// <summary>
    /// 由大地无敌-范若余于2010年1月31日建立
    /// </summary>
    public class Stage5_Part2 : StagePart
    {
        Timer t;
        bool b;
        Unit boss;
        Decoration d;
        bool b1;
        bool b2;
        bool b3;
        bool b4;
        bool end;
        Timer t1;
        Timer t2;
        Timer t3;
        Timer t4;
        Timer t5;
        Timer k3;
        /// <summary>
        /// 初始化
        /// </summary>
        public override void Initialize()
        {
            t = Stage.CreateTimer(0.8f);
            Stage.ScreenEffectManager.KeepColor(Color.Black,1);
            Stage.ScreenEffectManager.Blink(Color.Black, 5);

            Stage.CreateDecoration(Content.Load<AODGameLibrary.Ambient.DecorationType>(@"DecorationTypes\BallFilp"), new Vector3(0, 0, 0), 5200, Vector3.Zero);
            d = Variables.LastCreatedDecoration;
            //Stage.CreateDecoration(Content.Load<AODGameLibrary.Ambient.DecorationType>(@"DecorationTypes\BallFilp"), new Vector3(300, 300, -29000), 35, Vector3.Zero);
            //cs[0] = Variables.LastCreatedDecoration;
            //Stage.CreateDecoration(Content.Load<AODGameLibrary.Ambient.DecorationType>(@"DecorationTypes\BallFilp"), new Vector3(300, -300, -29000), 35, Vector3.Zero);
            //cs[1] = Variables.LastCreatedDecoration;
            //Stage.CreateDecoration(Content.Load<AODGameLibrary.Ambient.DecorationType>(@"DecorationTypes\BallFilp"), new Vector3(-300, 300, -29000), 35, Vector3.Zero);
            //cs[2] = Variables.LastCreatedDecoration;
            //Stage.CreateDecoration(Content.Load<AODGameLibrary.Ambient.DecorationType>(@"DecorationTypes\BallFilp"), new Vector3(-300, -300, -29000), 35, Vector3.Zero);
            //cs[3] = Variables.LastCreatedDecoration;
            base.Initialize();
        }
        /// <summary>
        /// 从该片段继续时进行的处理
        /// </summary>
        public override void StartFormThis()
        {
            Stage.Player.Position = new Vector3(0, 0, 3000);//初始位置
            Variables.Unit[4].Position = new Vector3(40, 10, 2950);
            Variables.Unit[3].Position = new Vector3(40, -10, 2950);
            Variables.Unit[2].Position = new Vector3(-40, 10, 2950);
            base.StartFormThis();
        }
        /// <summary>
        /// 关卡判断
        /// </summary>
        public override void Touch()
        {
            if (Vector3.Distance(Stage.Player.Position, Vector3.Zero) > 5000)
            {
                Damage x = new Damage();
                x.attacker = boss;
                x.BasicDamage = 150;
                x.CrossValue = 0.1f;
                x.Fold = 1;
                x.FoldArmor = 3;
                x.FoldShield = 1;
                Stage.ScreenEffectManager.Blink(new Color(255,0,0, 55), 0.2f);

                Stage.Player.GetDamage(x);
            }
            if (!end)
            {
                if (b1)
                {
                    Skill s = boss.SkillFromName("轨道炮");
                    if (s != null)
                    {
                        if (s.IsSkillUsable && boss.IsUsingSkill == false)
                        {
                            boss.CastSkill(s);
                            Stage.AddRealtimeGameMessage("Eye of Cosmos: See in the Eye of Cosmos, mortal.", Color.Red, 2);

                        }
                    }
                }
                if (b3)
                {
                    Skill s = boss.SkillFromName("幽灵导弹_AE");
                    if (s != null)
                    {
                        if (s.IsSkillUsable && boss.IsUsingSkill == false)
                        {
                            boss.CastSkill(s);

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
                            boss.CastSkill(s);
                            Stage.AddRealtimeGameMessage("Eye of Cosmos: Too weak!", Color.Red, 2);

                        }
                    }
                }
                if (b4)
                {
                    if (Stage.AliveUnitsInUnitGroup(2).Count  < 5)
                    {
                        Stage.CreateNPCUnit(Content.Load<UnitType>(@"UnitTypes\ShadowSlain"), 2, new Vector3(0, -914, -4828));
                        Stage.CreateNPCUnit(Content.Load<UnitType>(@"UnitTypes\FlameDestroyer"), 2, new Vector3(0, 4500, 1748));
                    }
                }
            }
            if (end)
            {
                Stage.Victory("YES");
                end = false;
            }

        }
        public override void Event_TimerRing(Timer timer)
        {
            if (timer == t)
            {
                Stage.PlayMusic(@"Audio\Kai_Engel_-_Beneath_The_Stronghold", true, 2);

                /*
                Stage.AddGameMessage(@"Vector: 听着，秩序之眼由一层坚固的外壳保护着。", Color.CornflowerBlue, 4);
                Stage.AddGameMessage(@"Vector: 打破外壳，然后马上把“遗迹”扔进去。", Color.CornflowerBlue, 2);
                Stage.AddGameMessage(@"Vector: 尽快行动，秩序之眼察觉到了你们的存在。", Color.CornflowerBlue, 2);
                Stage.AddGameMessage(@"Zero: 交给我了！", Color.LightGreen, 2);
                Stage.AddGameMessage(@"Vector: 只要“遗迹”接触到秩序之眼，一切就成功了！", Color.CornflowerBlue, 2);
                Stage.AddGameMessage(@"...............", Color.White, 4);
                Stage.AddGameMessage(@"Eye of Cosmos: 秩序之眼启动……消灭——侵略——者——", Color.Red, 4);
                Stage.AddGameMessage(@"Alicia: 小心，它不受我的控制了！", Color.Yellow, 4);
                Stage.AddRealtimeGameMessage(@"Eye of Cosmos: 紧急防御系统初始化……武器准备……", Color.Red, 4);
                */

                Stage.AddGameMessage(@"Vector: Listen, it is protect by a hard shell.", Color.CornflowerBlue, 4);
                Stage.AddGameMessage(@"Vector: Destroy the shell, and shoot the Relic into the core.", Color.CornflowerBlue, 2);
                Stage.AddGameMessage(@"Vector: Do it fast.", Color.CornflowerBlue, 2);
                Stage.AddGameMessage(@"Zero: Fine.", Color.LightGreen, 2);
                Stage.AddGameMessage(@"Vector: We will succeed if the Relic touchs the core.", Color.CornflowerBlue, 2);
                Stage.AddGameMessage(@"...............", Color.White, 4);
                Stage.AddGameMessage(@"Eye of Cosmos: Eys of Cosmos online, destroying the invaders.", Color.Red, 4);
                Stage.AddGameMessage(@"Alicia: Watch out! it is out of my control!", Color.Yellow, 4);
                Stage.AddRealtimeGameMessage(@"Eye of Cosmos: Initializing emergency defence.", Color.Red, 4);
                
                Stage.CreateNPCUnit(Content.Load<UnitType>(@"UnitTypes\EOS"), 2, new Vector3(0, 0, 0));
                Variables.LastCreatedUnit.SetMoveState(Vector3.Zero, new Vector3(0, 180, 0));
                boss = Variables.LastCreatedUnit;
                //Stage.CreateNPCUnit(Content.Load<UnitType>(@"UnitTypes\EOSSh"), 2, new Vector3(0, 800, 0));
                //Variables.LastCreatedUnit.SetMoveState(Vector3.Zero, new Vector3(0, 180, 0));
                //Stage.CreateNPCUnit(Content.Load<UnitType>(@"UnitTypes\EOSMa"), 2, new Vector3(0, -800, 0));
                //Variables.LastCreatedUnit.SetMoveState(Vector3.Zero, new Vector3(0, 180, 0));
                //Stage.CreateNPCUnit(Content.Load<UnitType>(@"UnitTypes\EOSMi"), 2, new Vector3(800, 0, 0));
                //Variables.LastCreatedUnit.SetMoveState(Vector3.Zero, new Vector3(0, 180, 0));
                //Stage.CreateNPCUnit(Content.Load<UnitType>(@"UnitTypes\EOSMa"), 2, new Vector3(-800, 0, 0));
                //Variables.LastCreatedUnit.SetMoveState(Vector3.Zero, new Vector3(0, 180, 0));
                //Stage.CreateNPCUnit(Content.Load<UnitType>(@"UnitTypes\EOSMi"), 2, new Vector3(0, 0, -800));
                //Variables.LastCreatedUnit.SetMoveState(Vector3.Zero, new Vector3(0, 180, 0));
                
                t1 = Stage.CreateTimer(20);
                t3 = Stage.CreateTimer(50);
                t4 = Stage.CreateTimer(75);
                //Stage.CreateNPCUnit(Content.Load<UnitType>(@"UnitTypes\Scream"), 4, new Vector3(0, 0, 0));
                //Stage.CreateNPCUnit(Content.Load<UnitType>(@"UnitTypes\Scream"), 4, new Vector3(0, 0, 0));
                //Stage.CreateNPCUnit(Content.Load<UnitType>(@"UnitTypes\Scream"), 4, new Vector3(0, 0, 0));
                //Stage.CreateNPCUnit(Content.Load<UnitType>(@"UnitTypes\Scream"), 4, new Vector3(0, 0, 0));
                //Stage.CreateNPCUnit(Content.Load<UnitType>(@"UnitTypes\Scream"), 4, new Vector3(0, 0, 0));
                //Stage.CreateNPCUnit(Content.Load<UnitType>(@"UnitTypes\Scream"), 4, new Vector3(0, 0, 0));
                //Stage.CreateNPCUnit(Content.Load<UnitType>(@"UnitTypes\Scream"), 4, new Vector3(0, 0, 0));
                //Stage.CreateNPCUnit(Content.Load<UnitType>(@"UnitTypes\Scream"), 4, new Vector3(0, 0, 0));
                //Stage.CreateNPCUnit(Content.Load<UnitType>(@"UnitTypes\Scream"), 4, new Vector3(0, 0, 0));
                b = true;
            }
            if (!end)
            {
                if (timer == t1)
                {
                    Stage.PlayMusic(@"Audio\Antti_Martikainen_-_The_Chase", true, 2);

                    /*
                    Stage.AddGameMessage(@"Alicia: 糟糕，秩序之眼正在启动内置的战略轨道炮！", Color.Yellow, 4);
                    Stage.AddGameMessage(@"Alicia: 我已经没有权限阻止了，不要正面攻击它！", Color.Yellow, 4);
                    Stage.AddGameMessage(@"Eye of Cosmos: 紧急防御系统初始化完毕……轨道炮激活……", Color.Red, 4);
                    Stage.AddGameMessage(@"Eye of Cosmos: 轨道炮激活完毕……紧急防御系统第二阶段初始化中", Color.Red, 4);
                    Stage.AddGameMessage(@"Vector: 通告！侦测到轨道炮，盖亚号，开启轨道偏转护盾！", Color.CornflowerBlue, 2);
                    Stage.AddGameMessage(@"Vector: 盖亚号安全！各小队自行掩护！", Color.CornflowerBlue, 2);
                     */



                    Stage.AddGameMessage(@"Alicia: It is preparing the Strategic Railgun", Color.Yellow, 4);
                    Stage.AddGameMessage(@"Alicia: Don't engage it from ahead!", Color.Yellow, 4);
                    Stage.AddGameMessage(@"Eye of Cosmos: Railgun, active.", Color.Red, 4);
                    Stage.AddGameMessage(@"Eye of Cosmos: Second phase initializing...", Color.Red, 4);
                    Stage.AddGameMessage(@"Vector: Warning! Railgun threat detected!", Color.CornflowerBlue, 2);
                    Stage.AddGameMessage(@"Vector: Protect yourselves!", Color.CornflowerBlue, 2);

                    t2 = Stage.CreateTimer(10);
                }
                if (timer == t2)
                {
                    b1 = true;
                }
                if (timer == t3)
                {
                    Stage.PlayMusic(@"Audio\Antti_Martikainen_-_Through_Enemy_Lines", true, 2);
                    //Stage.AddRealtimeGameMessage(@"Eye of Cosmos: 紧急防御系统初始化第二阶段……飞弹风暴开启……", Color.Red, 4);
                    Stage.AddRealtimeGameMessage(@"Eye of Cosmos: Second phase completed, Missile Storm on.", Color.Red, 4);
                    //Stage.AddRealtimeGameMessage(@"Vector: 盖亚号安全！各小队自行掩护！", Color.CornflowerBlue, 2);
                    b3 = true;
                }
                if (timer == t4)
                {
                    Stage.PlayMusic(@"Audio\TitanSlayer_-_Dawning_of_Darkness", true, 2);
                    //Stage.AddRealtimeGameMessage(@"Eye of Cosmos: 紧急防御系统初始化第三阶段……快速装配开启……", Color.Red, 4);
                    Stage.AddRealtimeGameMessage(@"Eye of Cosmos: Third phase completed, Mob Module on.", Color.Red, 4);

                    b4 = true;
                }
            }
           
            base.Event_TimerRing(timer);
        }
        public override void Event_UnitDied(Unit deadUnit)
        {
            if (deadUnit == boss)
            {
                Stage.SavePlayer();
                Stage.EnableStage(6);
                Stage.ClearMessages();
                Stage.StopMusic();
                Stage.Player.isPlayerControlling = false;
                Stage.Player.IsInvincible = true;

                Stage.ScreenEffectManager.Blink(Color.White, 2);
                Stage.ScreenEffectManager.KeepColor(Color.Black, null);
                /*
                Stage.AddGameMessage(@"...............", Color.White, 4);
                Stage.AddGameMessage(@"Vector: 就是现在！将“遗迹”掷进去吧！", Color.Red, 2);
                Stage.AddGameMessage(@"Vector: 盖亚号马上就可以赶到！", Color.Red, 2);
                Stage.AddGameMessage(@"...............", Color.White, 4);
                Stage.AddGameMessage(@"Vector: 怎么还没有反应？杰诺！你干了什么？", Color.Red, 2);
                Stage.AddGameMessage(@"Zero: 还没有准备好呢！稍微等一下吧。", Color.LightGreen, 2);
                Stage.AddGameMessage(@"Alicia: 杰诺……谢谢你。", Color.Yellow, 4);
                Stage.AddGameMessage(@"Alicia: 秩序之眼自毁程序，启动。", Color.Yellow, 4);
                Stage.AddGameMessage(@"Vector: 可恶！你们都干了些什么！？", Color.Red, 2);
                Stage.AddGameMessage(@"Bill: 任务完成了，威克多。", Color.CornflowerBlue, 4);
                Stage.AddGameMessage(@"Bill: 秩序之眼已经不再威胁人类了。", Color.CornflowerBlue, 4);
                */

                Stage.AddGameMessage(@"...............", Color.White, 4);
                Stage.AddGameMessage(@"Vector: Now! Shoot the Relic!", Color.Red, 2);
                Stage.AddGameMessage(@"Vector: Gaia is on its way!", Color.Red, 2);
                Stage.AddGameMessage(@"...............", Color.White, 4);
                Stage.AddGameMessage(@"Vector: Why no response? Zero?", Color.Red, 2);
                Stage.AddGameMessage(@"Zero: I am not prepared, just wait another minute!", Color.LightGreen, 2);
                Stage.AddGameMessage(@"Alicia: Zero, thanks.", Color.Yellow, 4);
                Stage.AddGameMessage(@"Alicia: Self destruction system, on.", Color.Yellow, 4);
                Stage.AddGameMessage(@"Vector: Damn! What have you done!?", Color.Red, 2);
                Stage.AddGameMessage(@"Bill: Mission is completed, Vector.", Color.CornflowerBlue, 4);
                Stage.AddGameMessage(@"Bill: The eye will no longer be a threat.", Color.CornflowerBlue, 4);
                
                foreach (Unit u in Stage.AliveUnitsInUnitGroup(2))
                {
                    u.BeginToDie();
                }
                
                end = true;
            }
            base.Event_UnitDied(deadUnit);
        }
    }
}
