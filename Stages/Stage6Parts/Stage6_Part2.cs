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


namespace Stages.Stage6Parts
{
     
    /// <summary>
    /// 由大地无敌-范若余于2010年1月31日建立
    /// </summary>
    public class Stage6_Part2 : StagePart
    {
        Timer t;
        Timer t1;
        Timer t2;
        bool b;
        bool b1;
        bool b2;
        bool b3;
        bool b4;
        bool b5;
        bool b6;
        Timer mori;
        bool win;
        Unit boss;
        /// <summary>
        /// 初始化
        /// </summary>
        public override void Initialize()
        {

            t = Stage.CreateTimer(0.5f);
            Stage.ScreenEffectManager.KeepColor(Color.White, 1);
            Stage.ScreenEffectManager.Blink(Color.White, 5);
            base.Initialize();
        }
        /// <summary>
        /// 从该片段继续时进行的处理
        /// </summary>
        public override void StartFormThis()
        {

            Stage.PlayMusic(@"Audio\TitanSlayer_-_Dawning_of_Darkness", true, 5);
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
            if (!win)
            {
                if (boss!= null)
                {
                    if (boss.Shield <= 80000 && b1 == false)
                    {
                        /*
                        Stage.AddRealtimeGameMessage(@"Vector: 只有我才有权限开启星际之门！", Color.Red, 2);
                        Stage.AddRealtimeGameMessage(@"Vector: 如果你们不想一起死在秩序之眼的爆炸中，", Color.Red, 2);
                        Stage.AddRealtimeGameMessage(@"Vector: 就把“遗迹”交给我！", Color.Red, 2);
                        
                         */
                        Stage.AddRealtimeGameMessage(@"Vector: Only I can initiate the jump system!", Color.Red, 2);
                        Stage.AddRealtimeGameMessage(@"Vector: If you don't want to die in the explosion of the Eye", Color.Red, 2);
                        Stage.AddRealtimeGameMessage(@"Vector: The give me the Relic!", Color.Red, 2);
                        
                        b1 = true;
                    }
                    else if (boss .Shield<60000&& b2== false)
                    {
                        /*
                        Stage.AddRealtimeGameMessage(@"Vector: 如果你们不想一起死在秩序之眼的爆炸中，", Color.Red, 2);
                        Stage.AddRealtimeGameMessage(@"Vector: 就把“遗迹”交给我！", Color.Red, 2);
                        Stage.AddRealtimeGameMessage(@"Alicia: 没关系的，你会比秩序之眼先爆炸。", Color.Yellow, 4);
                        Stage.AddRealtimeGameMessage(@"Alicia: 这将是秩序之眼最后一次维护真正的秩序。", Color.Yellow, 4);
                        Stage.AddRealtimeGameMessage(@"Vector: 有趣。", Color.Red, 2);
                        
                         */
                        Stage.AddRealtimeGameMessage(@"Vector: If you don't want to die in the explosion of the Eye", Color.Red, 2);
                        Stage.AddRealtimeGameMessage(@"Vector: The give me the Relic!", Color.Red, 2);
                        Stage.AddRealtimeGameMessage(@"Alicia: No matter, you will explode faster.", Color.Yellow, 4);
                        Stage.AddRealtimeGameMessage(@"Alicia: This is the time for cosmos.", Color.Yellow, 4);
                        Stage.AddRealtimeGameMessage(@"Vector: interesting.", Color.Red, 2);
                        Stage.ScreenEffectManager.Blink(Color.Orange, 10);

                        b2 = true;
                    }
                    else if (boss.Shield < 50000 && b3 == false && b6 == false && !Stage.Player.Dead)
                    {

                        ((RegularAI)boss.unitAI).Target = Stage.Player;
                        ((RegularAI)boss.unitAI).TargetChangeAble = false;
                        Stage.ScreenEffectManager.Blink(Color.Orange, 10);
    
                        b3 = true;
                    }
                    else if (!b4 && boss.Shield < 100)
                    {

                        Stage.PlayMusic(@"Audio\Antti_Martikainen_-_Through_Enemy_Lines", true, 5);
                        Stage.AddRealtimeGameMessage(@"Vector: Drones online!", Color.Red, 2);
                        Stage.AddRealtimeGameMessage(@"Vector: You are doomed, traitors!", Color.Red, 2);
                        /*
                        Stage.AddRealtimeGameMessage(@"Vector: 开始投放无人机！", Color.Red, 2);
                        Stage.AddRealtimeGameMessage(@"Vector: 我要毁灭你们，叛徒！", Color.Red, 2);
                         */
                        boss.AddSkill(@"Skills\Summon_Falcon");
                        b4 = true;
                    }
                    else if (!b5 && boss.Armor < 50000)
                    {

                        Stage.PlayMusic(@"Audio\TitanSlayer_-_Dawning_of_Darkness", true, 5);
                        //Stage.AddRealtimeGameMessage(@"Vector: 这是你自找的，可恶！", Color.Red, 2);
                        Stage.AddRealtimeGameMessage(@"Vector: You asked this yourself!", Color.Red, 2);
                        boss.AddSkill(@"Skills\Ghost_AE");
                        b5 = true;
                    }

                }
                
            }
            if (win)
            {
                if (Stage.IsMessageEnd)
                {

                    Stage.Victory("哈哈！");
                    win = false;
                }
            }
            

        }
        public override void Event_TimerRing(Timer timer)
        {
            if (timer == t)
            {
                /*
                Stage.AddRealtimeGameMessage(@"马萨：第三舰队，向盖亚号发起突击！", Color.CornflowerBlue, 4);
                Stage.AddRealtimeGameMessage(@"Alrdis: 第四舰队，锁定盖亚号！", Color.CornflowerBlue, 4);
                Stage.AddRealtimeGameMessage(@"Vector: 你们这些渣滓！", Color.Red, 2);
                */
                Stage.AddRealtimeGameMessage(@"Matha：1st Fleet，let's attack the Gaia!", Color.CornflowerBlue, 4);
                Stage.AddRealtimeGameMessage(@"Alrdis: 4th Fleet, lock on Gaia!", Color.CornflowerBlue, 4);
                Stage.AddRealtimeGameMessage(@"Vector: YOU ALL BETRAYED ME!?", Color.Red, 2);


                Stage.CreateNPCUnit(Content.Load<UnitType>(@"UnitTypes\Phoenix"), 1, new Vector3(3000, 0, 0));
                  Variables.LastCreatedUnit.RiderName = "阿贝拉尔 <3rd fleet>";
                Stage.CreateNPCUnit(Content.Load<UnitType>(@"UnitTypes\Phoenix"), 1, new Vector3(3000, 100, 0));
       
                Variables.LastCreatedUnit.RiderName = "Book <1st Fleet>";
                Stage.CreateNPCUnit(Content.Load<UnitType>(@"UnitTypes\Phoenix"), 1, new Vector3(3000, -100, 0));
                 Variables.LastCreatedUnit.RiderName = "Affton <1st Fleet>";
                Stage.CreateNPCUnit(Content.Load<UnitType>(@"UnitTypes\Phoenix"), 1, new Vector3(3000, 50, 0));
                Variables.LastCreatedUnit.RiderName = "Matha <1st Fleet Commander>";
                Stage.CreateNPCUnit(Content.Load<UnitType>(@"UnitTypes\Falcon3"), 1, new Vector3(-3000, 0, 0));
                Variables.LastCreatedUnit.RiderName = "Affron <4rd Fleet>";
                Stage.CreateNPCUnit(Content.Load<UnitType>(@"UnitTypes\Falcon2"), 1, new Vector3(-3000, 50, 0));
                Variables.LastCreatedUnit.RiderName = "Alrdis <4rd Fleet Commander>";
                Stage.CreateNPCUnit(Content.Load<UnitType>(@"UnitTypes\Falcon3"), 1, new Vector3(-3000, -50, 0));
                Variables.LastCreatedUnit.RiderName = "Orderi <4rd Fleet>";



                t1 = Stage.CreateTimer(20);

                Stage.CreateNPCUnit(Content.Load<UnitType>(@"UnitTypes\Titan_BOSS"), 3, new Vector3(0, 0, 0));
                
                Variables.LastCreatedUnit.RiderName = "Vector";
                boss = Variables.LastCreatedUnit;
                boss.IsInvincible = true;
                boss.MoveTo(Stage.Player.Position);
                b = true;
            }

            if (timer == t1)
            {
                Stage.PlayMusic(@"Audio\Kai_Engel_-_Beneath_The_Stronghold", true, 5);
                /*
                Stage.AddGameMessage(@"Vector: 哈哈哈哈！你们无法撼动盖亚号！", Color.Red, 2);
                Stage.AddGameMessage(@"Alicia: 是吗？", Color.Yellow, 4);
                Stage.AddGameMessage(@"Vector: 怎……怎么会！？", Color.Red, 2);
                Stage.AddGameMessage(@"Alicia: 看来你的护盾好像已经被我重写的“遗迹”发出的信号解除了。", Color.Yellow, 4);
                Stage.AddGameMessage(@"Alicia: 再次瘫痪吧！这次可没有什么能保护盖亚号了！", Color.Yellow, 4);
                 */

                Stage.AddGameMessage(@"Vector: Hahaha, you can't hurt the Gaia!", Color.Red, 2);
                Stage.AddGameMessage(@"Alicia: Is it?", Color.Yellow, 4);
                Stage.AddGameMessage(@"Vector: Wh...what!?", Color.Red, 2);
                Stage.AddGameMessage(@"Alicia: I hacked your shield.", Color.Yellow, 4);
                Stage.AddGameMessage(@"Alicia: Nothing can protect you now, Vector.", Color.Yellow, 4);
                boss.IsInvincible = false;
                boss.Stop();
            }
            if (timer == mori)
            {
                Stage.PlayMusic(@"Audio\Antti_Martikainen_-_The_Chase", true, 5);

                /*
                 Stage.AddGameMessage(@"Vector: 死吧！", Color.Red, 2);
                Stage.AddGameMessage(@"Alicia: 正在激活末日级，钥匙：遗迹。", Color.Yellow, 4);
                Stage.AddGameMessage(@"Vector: 什么？", Color.Red, 2);
                 Stage.AddGameMessage(@"Alicia: 末日，隐形模式关闭，攻击模式开启。", Color.Yellow, 4);
                 Stage.AddGameMessage(@"Alicia: 杰诺，准备好了就按下发射钮吧。", Color.Yellow, 4);
                 Stage.AddGameMessage(@"Vector: 就算是末日，也阻挡不了我！", Color.Red, 2);
                 Stage.AddGameMessage(@"Price Jeffsion: 威克多，你的轨道偏转装置，刚才已经被我破坏了哦！", Color.CornflowerBlue, 4);
                 Stage.AddGameMessage(@"Vector: 不！叛徒，死吧！这个世界属于我！", Color.Red, 2);
                 */

                Stage.AddGameMessage(@"Vector: Die!", Color.Red, 2);
                Stage.AddGameMessage(@"Alicia: Activating the Doom battleship, key: Relic.", Color.Yellow, 4);
                Stage.AddGameMessage(@"Vector: What!?", Color.Red, 2);
                Stage.AddGameMessage(@"Alicia: Doom, cloak off, railgunready.", Color.Yellow, 4);
                Stage.AddGameMessage(@"Alicia: Zero, if you are prepared, then shoot.", Color.Yellow, 4);
                Stage.AddGameMessage(@"Vector: It is your doom!", Color.Red, 2);
                //Stage.AddGameMessage(@"Price Jeffsion: 威克多，你的轨道偏转装置，刚才已经被我破坏了哦！", Color.CornflowerBlue, 4);
                Stage.AddGameMessage(@"Vector: NO! DIE! TRAITORS! THIS WORLD  IS MINE!", Color.Red, 2);


                 Stage.CreatePlayerUnit(Content.Load<UnitType>(@"UnitTypes\Doom_Player"), 1, boss.Position + new Vector3(0, 0, 3000));
                 Variables.LastCreatedUnit.RiderName = @"Zero";
                 Stage.Player = Variables.LastCreatedUnit;
                 ((AODGameLibrary2.AssistAI)Variables.Unit[2].unitAI).AssistUnit = Stage.Player;
                 ((AODGameLibrary2.AssistAI)Variables.Unit[3].unitAI).AssistUnit = Stage.Player;
                 ((AODGameLibrary2.AssistAI)Variables.Unit[4].unitAI).AssistUnit = Stage.Player;
                
                 Stage.Player.IsLootAble = false;
                 Stage.GameWorld.playerAimCamera.cameraObjOffset = new Vector3(0, 100, 0);
                 Stage.GameWorld.playerChaseCamera.cameraObjOffset = new Vector3(0, 130, 100);
                 Stage.Player.SkillControlUnit = true;
                 Stage.FailWhenPlayerDied = true;

                 ((RegularAI)boss.unitAI).Target = Stage.Player;
                 ((RegularAI)boss.unitAI).TargetChangeAble = false;
                 ((RegularAI)boss.unitAI).settings.isCycleAble = true;
                 ((RegularAI)boss.unitAI).settings.minRange = 150;
                 ((RegularAI)boss.unitAI).settings.rangeOfCycle = 1500;
                 ((RegularAI)boss.unitAI).settings.rangeOfAttack = 90000;
                 ((RegularAI)boss.unitAI).settings.rangeOfShot = 5000;
                 boss.angularRate = 90;
                 b6 = true;
             }
            base.Event_TimerRing(timer);
        }
        public override void Event_UnitDied(Unit deadUnit)
        {
            if (deadUnit== boss)
            {
                Stage.ClearMessages();
                Stage.StopMusic();
                Stage.Player.isPlayerControlling = false;
                Stage.Player.IsInvincible = true;
                foreach (Unit u in Stage.AliveUnitsInUnitGroup(3))
                {
                    if (!u.Dead)
                    {
                        u.BeginToDie();
                    }
                }
                Stage.PlayMusic(@"Audio\Moreno_Visintin_-_Mdnel-Inn", true, 5);
                Stage.ScreenEffectManager.Blink(Color.Orange,5);
                Stage.ScreenEffectManager.KeepColor(Color.Black,null);
                Stage.AddFlyingMessage("", Color.White);
                /*
                Stage.AddFlyingMessage(@"...............", Color.White);
                Stage.AddFlyingMessage(@"Bill: ……结束了吗？", Color.CornflowerBlue);
                Stage.AddFlyingMessage(@"Price Jeffsion: 开启星际之门吧。", Color.CornflowerBlue);
                Stage.AddFlyingMessage(@"...............", Color.White);
                
                 */
                Stage.AddFlyingMessage(@"...............", Color.White);
                Stage.AddFlyingMessage(@"Bill: .....is it... over？", Color.CornflowerBlue);
                Stage.AddFlyingMessage(@"Price Jeffsion: It is over, now open the gate.", Color.CornflowerBlue);
                Stage.AddFlyingMessage(@"...............", Color.White);
                
                Stage.AddFlyingMessage("", Color.White);
                Stage.AddFlyingMessage("", Color.White);
                Stage.AddFlyingMessage("", Color.White);
                Stage.AddFlyingMessage("", Color.White);
                Stage.AddFlyingMessage("", Color.White);
                Stage.AddFlyingMessage("", Color.White);
                Stage.AddFlyingMessage(@"...............", Color.White);
                Stage.AddFlyingMessage(@"Price Jeffsion: The Federation needs a new commander.", Color.CornflowerBlue);
                Stage.AddFlyingMessage(@"Bill: It is you. I have to go.", Color.CornflowerBlue);
                Stage.AddFlyingMessage(@"Maria Woodwind: Then, bye, Bill.", Color.CornflowerBlue);
                Stage.AddFlyingMessage(@"...............", Color.White);
                Stage.AddFlyingMessage("", Color.White);
                Stage.AddFlyingMessage("", Color.White);
                Stage.AddFlyingMessage("", Color.White);
                Stage.AddFlyingMessage("", Color.White);
                Stage.AddFlyingMessage("", Color.White);
                Stage.AddFlyingMessage("", Color.White);

                Stage.AddFlyingMessage(@"...............", Color.White);
                /*
                Stage.AddFlyingMessage(@"Zero: 终于完成了。本游侠现在貌似暂时没事可干了。", Color.LightGreen);
                Stage.AddFlyingMessage(@"Alicia: 再也没有秩序之眼了，你也没有战斗的对手了吧。", Color.Yellow);
                Stage.AddFlyingMessage(@"Zero: 对了，你现在想去哪里，阿莉西亚？", Color.LightGreen);
                Stage.AddFlyingMessage(@"Alicia: 由你决定，杰诺。", Color.Yellow);
                
                 */
                Stage.AddFlyingMessage(@"Zero: Finally... Now I don't know what to do...", Color.LightGreen);
                Stage.AddFlyingMessage(@"Alicia: There is no more EoC drones, I suppose.", Color.Yellow);
                Stage.AddFlyingMessage(@"Zero: All right, where do you want to go, Alicia?", Color.LightGreen);
                Stage.AddFlyingMessage(@"Alicia: Surprise me, Zero.", Color.Yellow);
                
                Stage.AddFlyingMessage(@"...............", Color.White);

                Stage.AddFlyingMessage("", Color.White);
                Stage.AddFlyingMessage("", Color.White);
                Stage.AddFlyingMessage("", Color.White);
                Stage.AddFlyingMessage("", Color.White);
                Stage.AddFlyingMessage("", Color.White);
                Stage.AddFlyingMessage("", Color.White);
 

                Stage.AddFlyingMessage(@"命运艺术", Color.Yellow);
                Stage.AddFlyingMessage(@"Art of Destiny", Color.Yellow);
                Stage.AddFlyingMessage(@"制作表", Color.White);
                Stage.AddFlyingMessage(@"", Color.White);
                Stage.AddFlyingMessage(@"", Color.White);
                Stage.AddFlyingMessage(@"", Color.White);
                Stage.AddFlyingMessage(@"", Color.White);
                Stage.AddFlyingMessage(@"", Color.White);
                Stage.AddFlyingMessage(@"游戏总设计师：", Color.White);
                Stage.AddFlyingMessage(@"大地无敌", Color.White);
                Stage.AddFlyingMessage(@"", Color.White);
                Stage.AddFlyingMessage(@"", Color.White);
                Stage.AddFlyingMessage(@"", Color.White);
                Stage.AddFlyingMessage(@"", Color.White);
                Stage.AddFlyingMessage(@"剧情：", Color.White);
                Stage.AddFlyingMessage(@"大地无敌", Color.White);
                Stage.AddFlyingMessage(@"", Color.White);
                Stage.AddFlyingMessage(@"", Color.White);
                Stage.AddFlyingMessage(@"", Color.White);
                Stage.AddFlyingMessage(@"", Color.White);
                Stage.AddFlyingMessage(@"创意：", Color.White);
                Stage.AddFlyingMessage(@"大地无敌", Color.White);
                Stage.AddFlyingMessage(@"", Color.White);
                Stage.AddFlyingMessage(@"", Color.White);
                Stage.AddFlyingMessage(@"", Color.White);
                Stage.AddFlyingMessage(@"", Color.White);
                Stage.AddFlyingMessage(@"平面美术：", Color.White);
                Stage.AddFlyingMessage(@"大地无敌", Color.White);
                Stage.AddFlyingMessage(@"", Color.White);
                Stage.AddFlyingMessage(@"", Color.White);
                Stage.AddFlyingMessage(@"", Color.White);
                Stage.AddFlyingMessage(@"", Color.White);
                Stage.AddFlyingMessage(@"3D美术：", Color.White);
                Stage.AddFlyingMessage(@"大地无敌", Color.White);
                Stage.AddFlyingMessage(@"", Color.White);
                Stage.AddFlyingMessage(@"", Color.White);
                Stage.AddFlyingMessage(@"", Color.White);
                Stage.AddFlyingMessage(@"", Color.White);
                Stage.AddFlyingMessage(@"音效：", Color.White);
                Stage.AddFlyingMessage(@"大地无敌", Color.White);
                Stage.AddFlyingMessage(@"", Color.White);
                Stage.AddFlyingMessage(@"", Color.White);
                Stage.AddFlyingMessage(@"", Color.White);
                Stage.AddFlyingMessage(@"", Color.White);
                Stage.AddFlyingMessage(@"程序架构：", Color.White);
                Stage.AddFlyingMessage(@"大地无敌", Color.White);
                Stage.AddFlyingMessage(@"", Color.White);
                Stage.AddFlyingMessage(@"", Color.White);
                Stage.AddFlyingMessage(@"", Color.White);
                Stage.AddFlyingMessage(@"", Color.White);
                Stage.AddFlyingMessage(@"代码编写：", Color.White);
                Stage.AddFlyingMessage(@"大地无敌", Color.White);
                Stage.AddFlyingMessage(@"", Color.White);
                Stage.AddFlyingMessage(@"", Color.White);
                Stage.AddFlyingMessage(@"", Color.White);
                Stage.AddFlyingMessage(@"", Color.White);
                Stage.AddFlyingMessage(@"测试员：", Color.White);
                Stage.AddFlyingMessage(@"大地无敌", Color.White);
                Stage.AddFlyingMessage(@"", Color.White);
                Stage.AddFlyingMessage(@"", Color.White);
                Stage.AddFlyingMessage(@"", Color.White);
                Stage.AddFlyingMessage(@"", Color.White);
                Stage.AddFlyingMessage(@"感谢XNA游戏世界(http://www.xnaer.com)", Color.White);
                Stage.AddFlyingMessage(@"", Color.White);
                Stage.AddFlyingMessage(@"", Color.White);
                Stage.AddFlyingMessage(@"", Color.White);
                Stage.AddFlyingMessage(@"", Color.White);
                Stage.AddFlyingMessage(@"Copyright 2009-2010 @ 大地无敌@大地天下(http://www.agrp.info) . All rights reserved", Color.White);
                Stage.AddFlyingMessage(@"", Color.White);
                Stage.AddFlyingMessage(@"", Color.White);
                Stage.AddFlyingMessage(@"", Color.White);
                Stage.AddFlyingMessage(@"", Color.White);
                Stage.AddFlyingMessage(@"详细的授权信息请参见license.txt和third-party-licenses.txt，这里保持4年前原样好了。", Color.White);
                Stage.AddFlyingMessage(@"更多信息，请访问http://www.windy.moe", Color.White);
                Stage.AddFlyingMessage(@"See license.txt and third-party-licenses.txt for licensing informations", Color.White);
                Stage.AddFlyingMessage("Created on 2009 by Windy Darian(http://windy.moe)", Color.White);
                Stage.AddFlyingMessage("", Color.White);
                Stage.AddFlyingMessage("", Color.White);
                Stage.AddFlyingMessage("", Color.White);
                Stage.AddFlyingMessage("", Color.White);
                Stage.AddFlyingMessage("", Color.White);

                Stage.AddFlyingMessage(@"Alicia: Go home then, Zero.", Color.Yellow);

                win = true;
            }
            if (deadUnit == Stage.Player)
            {
                if (!b6)
                {
                    Stage.StopMusic();

                    mori = Stage.CreateTimer(10);

                    /*
                    Stage.AddGameMessage(@"Zero: 该死……", Color.LightGreen, 2);
                    Stage.AddGameMessage(@"Bill: 无路可退了。", Color.CornflowerBlue, 2);
                    Stage.AddGameMessage(@"Alicia: 需要帮助吗，游侠？", Color.Yellow, 4);
                    Stage.AddGameMessage(@"Alicia: 这次该我救你了。", Color.Yellow, 4);

                    Stage.AddGameMessage(@"Alicia: 上船吧，杰诺。", Color.Yellow, 4);
                    */


                    Stage.AddGameMessage(@"Zero: Damn...", Color.LightGreen, 2);
                    Stage.AddGameMessage(@"Bill: Zero!", Color.CornflowerBlue, 2);
                    Stage.AddGameMessage(@"Alicia: Need help, hunter?", Color.Yellow, 4);
                    Stage.AddGameMessage(@"Alicia: I'll help you this time.", Color.Yellow, 4);

                    Stage.AddGameMessage(@"Alicia: Come abroad, Zero.", Color.Yellow, 4);
                }
                else
                {
                    Stage.ScreenEffectManager.Blink(Color.Orange, 5);

                    Stage.ScreenEffectManager.KeepColor(Color.Black,null);
                }



            }
            base.Event_UnitDied(deadUnit);
        }
       
    }
}
