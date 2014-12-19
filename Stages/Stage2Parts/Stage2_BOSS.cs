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

namespace Stages.Stage2Parts
{
    /// <summary>
    /// 由大地无敌-范若余于2010年2月14日建立
    /// </summary>
    public class Stage2_BOSS : StagePart
    {
        Unit boss;
        Unit u1;
        Unit u2;
        Unit u3;
        Unit u4;
        Timer t0;
        Timer t1;
        Timer t2;
        Timer t3;
        Timer t4;
        Timer t5;
        Timer tn;
        Timer t6;
        bool b;
        Decoration d;
        Decoration[] cs = new Decoration[4];
        bool win;

        /// <summary>
        /// 初始化
        /// </summary>
        public override void Initialize()
        {
            Stage.StopMusic();
            Stage.Player.Position = new Vector3(0, 0, -26800);//初始位置
            Stage.Variables.Unit[2].Position = new Vector3(50, 50, -26800);
            Stage.Player.SetMoveState(new Vector3(0, 0, 0), Vector3.Zero);
            Variables.Unit[2].SetMoveState(new Vector3(0, 0,0), Vector3.Zero);
            Stage.ScreenEffectManager.Blink(Color.Black, 3);
            Stage.GameWorld.SetCamera(new Camera(Stage.Game, new Vector3(35, 35, -28950), new Vector3(0, 0, -29000), Vector3.Up));
            Stage.Player.isPlayerControlling = false;
            Stage.Player.IsAIControlling = false;
            Stage.Player.IsInvincible = true;
            Variables.Unit[2].IsInvincible = true;
            Stage.Variables.Unit[2].IsAIControlling = false;
            Stage.GameWorld.MovieModelStart();



           // Stage.PlayMusic(@"Audio\CR_TourneyBattle02UniWalk", true, 5);
            Stage.CreateNPCUnit(Content.Load<UnitType>(@"UnitTypes\Scream_BOSS"), 4, new Vector3(0, 0, -29000));
            boss = Variables.LastCreatedUnit;
            boss.RiderName = "黑骑士克雷斯";
            ((RegularAI)boss.unitAI).settings.isMoveAble = false;
            boss.IsInvincible = true;
            boss.IsAIControlling = false;
            Stage.CreateNPCUnit(Content.Load<UnitType>(@"UnitTypes\Scream_add"), 4, new Vector3(300, 300, -29000));
            u1 = Variables.LastCreatedUnit;
            u1.IsInvincible = true;
            u1.IsAIControlling = false;
            Stage.CreateNPCUnit(Content.Load<UnitType>(@"UnitTypes\Scream_add"), 4, new Vector3(300, -300, -29000));
            u2 = Variables.LastCreatedUnit;
            u2.IsInvincible = true;
            u2.IsAIControlling = false;
            Stage.CreateNPCUnit(Content.Load<UnitType>(@"UnitTypes\Scream_add"), 4, new Vector3(-300, 300, -29000));
            u3 = Variables.LastCreatedUnit;
            u3.IsInvincible = true;
            u3.IsAIControlling = false;
            Stage.CreateNPCUnit(Content.Load<UnitType>(@"UnitTypes\Scream_add"), 4, new Vector3(-300, -300, -29000));
            u4 = Variables.LastCreatedUnit;
            u4.IsInvincible = true;
            u4.IsAIControlling = false;
            Stage.AddGameMessage(@"比尔：我们正在接近星际之门。", Color.CornflowerBlue, 3);
            Stage.AddGameMessage(@"杰诺：而且有人想挡我们的路。", Color.LightGreen, 3);
            Stage.AddGameMessage(@"克雷斯：真是白送上虎口的小绵羊……", Color.Red, 4);
            Stage.AddGameMessage(@"阿莉西亚：小心，教会的敌人看起来早有准备。", Color.Yellow, 3);
            t0 = Stage.CreateTimer(2);
            t1 = Stage.CreateTimer(18);
            t2 = Stage.CreateTimer(35);
            t3 = Stage.CreateTimer(50);
            t4 = Stage.CreateTimer(65);
            t5 = Stage.CreateTimer(75);
            tn = Stage.CreateTimer(120);

     

            Stage.CreateDecoration(Content.Load<AODGameLibrary.Ambient.DecorationType>(@"DecorationTypes\BallFilp"), new Vector3(0, 0, -29000), 3000, Vector3.Zero);
            d = Variables.LastCreatedDecoration;
            Stage.CreateDecoration(Content.Load<AODGameLibrary.Ambient.DecorationType>(@"DecorationTypes\BallFilp"), new Vector3(300, 300, -29000), 35, Vector3.Zero);
            cs[0] = Variables.LastCreatedDecoration;
            Stage.CreateDecoration(Content.Load<AODGameLibrary.Ambient.DecorationType>(@"DecorationTypes\BallFilp"), new Vector3(300, -300, -29000), 35, Vector3.Zero);
            cs[1] = Variables.LastCreatedDecoration;
            Stage.CreateDecoration(Content.Load<AODGameLibrary.Ambient.DecorationType>(@"DecorationTypes\BallFilp"), new Vector3(-300, 300, -29000), 35, Vector3.Zero);
            cs[2] = Variables.LastCreatedDecoration;
            Stage.CreateDecoration(Content.Load<AODGameLibrary.Ambient.DecorationType>(@"DecorationTypes\BallFilp"), new Vector3(-300, -300, -29000), 35, Vector3.Zero);
            cs[3] = Variables.LastCreatedDecoration;
            base.Initialize();
        }
        /// <summary>
        /// 从该片段继续时进行的处理
        /// </summary>
        public override void StartFormThis()
        {
            Stage.Player.Position = new Vector3(0, 0, -26800);//初始位置
            Stage.Variables.Unit[2].Position = new Vector3(50, 50, -26800);
            base.StartFormThis();
        }
        /// <summary>
        /// 关卡判断
        /// </summary>
        public override void Touch()
        {
            if (b == false)
            {
                float r = (1 - tn.CurrentTime / tn.EndTime) * 2800;
                d.Scale = MathHelper.Clamp(r, 1, 3000);
                d.Position = boss.Position;
                foreach (VioableUnit u in Stage.GameWorld.GameItemManager.BoundingCollection)
                {
                    if (u.Group != 4)
                    {
                        if (Vector3.Distance(u.Position, boss.Position) > r)
                        {
                            Damage x = new Damage();
                            x.attacker = boss;
                            x.BasicDamage = 150;
                            x.CrossValue = 0.1f;
                            x.Fold = 1;
                            x.FoldArmor = 3;
                            x.FoldShield = 1;

                            u.GetDamage(x);

                            if (u == Stage.Player)
                            {

                                Stage.ScreenEffectManager.Blink(new Color(255,0,0, 55), 0.2f);
                            }
                        }
                    }
                }
            }
            if (win)
            {
                if (Stage.IsMessageEnd)
                {
                    Stage.Victory("胜利！");
                }
            }
        }
        public override void Event_TimerRing(Timer timer)
        {
            if (boss.Dead == false)
            {
                if (timer == t0)
                {
                    
                    Stage.AddGameMessage(@"克雷斯：欢迎来到灵魂的狂欢派对！", Color.Red, 4);
                    Stage.AddGameMessage(@"克雷斯：我看到了“遗迹”，看到神谕，你们拿着它，却手足无措。", Color.Red, 4);
                    Stage.AddGameMessage(@"克雷斯：为了防止那个阴谋破坏主神的秩序，教会将收回这个东西。", Color.Red, 4);
                    Stage.AddGameMessage(@"克雷斯：而“遗迹”的力量将会毁掉你。", Color.Red, 4);
                    Stage.GameWorld.ReleaseCamera();
                    Stage.Player.SetMoveState(new Vector3(0,0,-200), Vector3.Zero);
                    Variables.Unit[2].SetMoveState(new Vector3(0, 0, -300), Vector3.Zero);
                    Stage.Player.MoveTo(new Vector3(0, 0, -28000));
                    Variables.Unit[2].MoveTo(new Vector3(35, 35, -28000));
                    
                }
                if (timer == t1)
                {
                    Stage.PlayMusic(@"Audio\TitanSlayer_-_Dawning_of_Darkness", true, 2);
                    Stage.Player.isPlayerControlling = true;
                    Stage.Variables.Unit[2].IsAIControlling = true;
                    Stage.GameWorld.MovieModelEnd();
                    Stage.Player.IsInvincible = false;
                    Variables.Unit[2].IsInvincible = false;
                    boss.IsInvincible = false;
                    boss.IsAIControlling = true;
                    u1.IsInvincible = false;
                    u1.IsAIControlling = true;
                    
                    Stage.AddGameMessage(@"克雷斯：让祭祀开始吧！", Color.Red, 3);
                    Stage.AddGameMessage(@"比尔：牵制住旁边的敌人！", Color.CornflowerBlue, 2);
                    Stage.AddGameMessage(@"杰诺：该死！“遗迹”开始不稳定了！", Color.LightGreen, 3);
                    Stage.AddGameMessage(@"阿莉西亚：这是一个陷阱！", Color.Yellow, 3);
                    Stage.AddGameMessage(@"阿莉西亚：空间正在坍塌，克雷斯在尝试引发“遗迹”的共振！", Color.Yellow, 4);
                    Stage.AddGameMessage(@"阿莉西亚：在你被压扁之前解决掉他！", Color.Yellow, 3);
                    cs[0].BeginToDie();
                }
                if (timer == t2)
                {
                    u2.IsInvincible = false;
                    u2.IsAIControlling = true;
                    Stage.AddRealtimeGameMessage(@"克雷斯：火焰燃起来了。", Color.Red, 4);
                    Stage.AddRealtimeGameMessage(@"比尔：更多的教徒投入战斗了，小心应付！", Color.CornflowerBlue, 4);
                    cs[1].BeginToDie();
                }
                if (timer == t3)
                {
                    u3.IsInvincible = false;
                    u3.IsAIControlling = true;
                    Stage.AddRealtimeGameMessage(@"克雷斯：让它更加混乱吧！", Color.Red, 4);
                    Stage.AddRealtimeGameMessage(@"阿莉西亚：……撑得住吗？", Color.Yellow, 4);
                    Stage.AddRealtimeGameMessage(@"阿莉西亚：没有了教徒的保护，黑骑士越来越容易受到伤害。", Color.Yellow, 4);
                    cs[2].BeginToDie();
                }
                if (timer == t4)
                {
                    u4.IsInvincible = false;
                    u4.IsAIControlling = true;
                    Stage.AddRealtimeGameMessage(@"克雷斯：混乱风暴！蒙克依，你是最后一个祭品！", Color.Red, 4);
                    Stage.AddRealtimeGameMessage(@"杰诺：又一个，可恶！", Color.LightGreen, 3);
                    cs[3].BeginToDie();
                }
                if (timer == t5)
                {
                    Stage.AddRealtimeGameMessage(@"克雷斯：那么我亲自来终结这风暴吧。", Color.Red, 4);
                    Stage.AddRealtimeGameMessage(@"阿莉西亚：空间坍塌非常严重！", Color.Yellow, 4);
                    ((RegularAI)boss.unitAI).settings.isMoveAble = true;
                    
                
                }

            }
          



            base.Event_TimerRing(timer);
        }
        public override void Event_UnitDied(Unit deadUnit)
        {
            if (deadUnit == boss)
            {

                Stage.SavePlayer();
                Stage.EnableStage(3);
                foreach (Unit  u in Stage.AliveUnitsInUnitGroup(4))
                {
                    if (!u.Dead)
                    {
                        u.BeginToDie();
                    }
                }
                Stage.ClearMessages();
                Stage.ScreenEffectManager.Blink(Color.White, 2);
                Stage.ScreenEffectManager.KeepColor(Color.Black, null);
                Stage.AddGameMessage(@"阿莉西亚：致命的一击！", Color.Yellow, 4);
                Stage.AddGameMessage(@"克雷斯：可恶……咳……咳……", Color.Red, 4);
                Stage.AddGameMessage(@"克雷斯：那么，我们……下次再见吧！", Color.Red, 4);
                Stage.AddGameMessage(@"比尔：我已经接收到坐标，正在校准星际之门参数。", Color.CornflowerBlue, 4);
                Stage.AddGameMessage(@"阿莉西亚：杰诺，“遗迹”依然不稳定！", Color.Yellow, 4);
                Stage.AddGameMessage(@"杰诺：我正在尝试冷却它！", Color.LightGreen, 3);
                Stage.AddGameMessage(@"比尔：空间校准完毕，就位！准备弹射！", Color.CornflowerBlue, 4);
                Stage.AddGameMessage(@"…………", Color.White, 3);
                Stage.Player.isPlayerControlling = false;
                Stage.Player.IsAIControlling = false;
                Stage.Player.IsInvincible = true;
                Variables.Unit[2].IsInvincible = true;
                Stage.Variables.Unit[2].IsAIControlling = false;
                b = true;
              
                win = true;
            }
            base.Event_UnitDied(deadUnit);
        }
    }
}
