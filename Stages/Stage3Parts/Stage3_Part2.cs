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
using AODGameLibrary.Texts;

namespace Stages.Stage3Parts
{
    /// <summary>
    /// 由大地无敌-范若余于2010年4月27日建立
    /// </summary>
    public class Stage3_Part2 : StagePart
    {
        Timer spawn;
        Timer phase2;
        Timer phase3;
        Timer phase4;
        int p=0;

        AODText time;
        Timer victory;
        Timer victory2;
        bool won;
        /// <summary>
        /// 初始化
        /// </summary>
        public override void Initialize()
        {

            Stage.PlayMusic(@"Audio\Antti_Martikainen_-_The_Chase", true, 10);

            /*
            Stage.AddGameMessage(@"Vector: 该死！盖亚号受到不明来源EMP导弹攻击！", Color.CornflowerBlue, 4);
            Stage.AddGameMessage(@"Vector: 恢复盖亚号正常运作需要一些时间，全体人员，保护盖亚号！", Color.CornflowerBlue, 4);
            Stage.AddGameMessage(@"Vector: 杰诺，同时注意你自己的战机，“遗迹”不能受到损伤！", Color.CornflowerBlue, 4);
            Stage.AddGameMessage(@"Bill: 杰诺交给我和第三舰队来照应，威克多。", Color.CornflowerBlue, 2);
            Stage.AddGameMessage(@"Vector: 很好，盖亚号一旦重新醒来，加上“遗迹”的能量，秩序之眼的统治——", Color.CornflowerBlue, 4);
            Stage.AddGameMessage(@"Vector: ——就可以永远结束！", Color.CornflowerBlue, 4);
            Stage.AddGameMessage(@"Alicia: 杰诺，现在只有你能听见我的话。", Color.Yellow, 4);
            Stage.AddGameMessage(@"Alicia: 我必须告诉你——", Color.Yellow, 4);
            Stage.AddGameMessage(@"Alicia: ……啊——可……恶……", Color.Yellow, 4);
            Stage.AddGameMessage(@"Alicia: ……对不起……", Color.Yellow, 4);
            Stage.AddGameMessage(@"Alicia: ……我……现在……不能和你联系……杰诺……", Color.Yellow, 4);
            Stage.AddGameMessage(@"Alicia: ……只有你……能让一切终结……", Color.Yellow, 4);
            */


            Stage.AddGameMessage(@"Vector: Damn! Gaia is under EMP attack!", Color.CornflowerBlue, 4);
            Stage.AddGameMessage(@"Vector: We need time to bring it back online!", Color.CornflowerBlue, 4);
            Stage.AddGameMessage(@"Vector: Everyone, protect Gaia!", Color.CornflowerBlue, 4);
            Stage.AddGameMessage(@"Bill: I will take care of Zero and the 3rd Fleet.", Color.CornflowerBlue, 2);
            Stage.AddGameMessage(@"Vector: Good, once Gaia ia back, we can push to the heart of the Eye", Color.CornflowerBlue, 4);
            Stage.AddGameMessage(@"Vector: - And use the Relic to end the war, once and for all.", Color.CornflowerBlue, 4);
            Stage.AddGameMessage(@"Alicia: Zero, now only you can hear me.", Color.Yellow, 4);
            Stage.AddGameMessage(@"Alicia: I must tell you -", Color.Yellow, 4);
            Stage.AddGameMessage(@"Alicia: ...Ahhhhhhh...Dam....", Color.Yellow, 4);
            Stage.AddGameMessage(@"Alicia: ...Sorry...", Color.Yellow, 4);
            Stage.AddGameMessage(@"Alicia: ...I cannot... for now...", Color.Yellow, 4);
            Stage.AddGameMessage(@"Alicia: ...Only you... can...", Color.Yellow, 4);

            Stage.CreateNPCUnit(Content.Load<UnitType>(@"UnitTypes\ShadowSlainMK2"), 2, new Vector3(10, 3000, -3000));
            Stage.CreateNPCUnit(Content.Load<UnitType>(@"UnitTypes\ShadowSlainMK2"), 2, new Vector3(10, 3000, -3000));
            Stage.CreateNPCUnit(Content.Load<UnitType>(@"UnitTypes\ShadowSlainMK2"), 2, new Vector3(10, 3000, -3000));
            Stage.CreateNPCUnit(Content.Load<UnitType>(@"UnitTypes\FlameDestroyerMK2"), 2, new Vector3(10, 3000, -3000));
            Stage.CreateNPCUnit(Content.Load<UnitType>(@"UnitTypes\FlameDestroyerMK2"), 2, new Vector3(10, 3000, -3000));
            Stage.CreateNPCUnit(Content.Load<UnitType>(@"UnitTypes\FlameDestroyerMK2"), 2, new Vector3(10, 3000, -3000));
            Stage.CreateNPCUnit(Content.Load<UnitType>(@"UnitTypes\FlameDestroyerMK2"), 2, new Vector3(10, 3000, -3000));
            
            victory = Stage.CreateTimer(600);
            time = new AODText(Stage.GameWorld, "", 0, Color.White, new Vector2(Stage.GameWorld.game.GraphicsDevice.Viewport.Width / 2, 130), FadeOutState.None, Vector2.Zero, true);
            Stage.GameWorld.InstantMessages.Add(time);

            spawn = Stage.CreateTimer(20);
            phase2 = Stage.CreateTimer(120);
            phase3 = Stage.CreateTimer(240);
            phase4 = Stage.CreateTimer(480);

            base.Initialize();
        }
        /// <summary>
        /// 从该片段继续时进行的处理
        /// </summary>
        public override void StartFormThis()
        {

            Stage.Player.Position = new Vector3(400 , 0, 500);//初始位置
            base.StartFormThis();
        }
        /// <summary>
        /// 关卡判断
        /// </summary>
        public override void Touch()
        {
            time.Text = "ETA for repairing: " + victory.GetTimeRemainsText();
            if (won)
            {
                Stage.Victory("哈哈！");
                won = false;
            }
        }
        public override void Event_UnitDied(Unit deadUnit)
        {
            if (deadUnit == Variables.Unit[5])
            {
                Stage.Fail("盖亚号毁灭");
            }
    
            base.Event_UnitDied(deadUnit);
        }
        public override void Event_TimerRing(Timer timer)
        {
            if (timer == spawn)
            {
                spawn.Reset();
                int a = Stage.AliveUnitsInUnitGroup(2).Count;
                int b = Stage.AliveUnitsInUnitGroup(4).Count;
                if (a<6)
                {
                    Stage.CreateNPCUnit(Content.Load<UnitType>(@"UnitTypes\ShadowSlainMK2"), 2, 3500 * AODGameLibrary.Helpers.RandomHelper.RandomDirection());
                    Variables.LastCreatedUnit.Target = Variables.Unit[5];
                    Stage.CreateNPCUnit(Content.Load<UnitType>(@"UnitTypes\FlameDestroyerMK2"), 2, 3500 * AODGameLibrary.Helpers.RandomHelper.RandomDirection());
              
                }
                if (p >= 1)
                {
                    if (a < 6)
                    {
                        Stage.CreateNPCUnit(Content.Load<UnitType>(@"UnitTypes\ShadowSlainMK2"), 2, 3500 * AODGameLibrary.Helpers.RandomHelper.RandomDirection());
                        Variables.LastCreatedUnit.Target = Variables.Unit[5];
                    }

                }
                if (p>=2)
                {
                    if (b < 6)
                        Stage.CreateNPCUnit(Content.Load<UnitType>(@"UnitTypes\Scream"), 4, new Vector3(0, 0, 3500));
               
                }
       
                if (p >= 3)
                {
                    if (a < 6)
                    {

                        Stage.CreateNPCUnit(Content.Load<UnitType>(@"UnitTypes\Lucifer"), 2, 3500 * AODGameLibrary.Helpers.RandomHelper.RandomDirection());
                        Variables.LastCreatedUnit.Target = Variables.Unit[5];
                    }
                }
            }
            if (timer== phase2)
            {
                p = 1;
                Stage.PlayMusic(@"Audio\Kai_Engel_-_Beneath_The_Stronghold", true, 10);
                Stage.CreateNPCUnit(Content.Load<UnitType>(@"UnitTypes\Lucifer"), 2, new Vector3(10, 3000, -3000));
                Variables.LastCreatedUnit.Target = Variables.Unit[5];
                Stage.CreateNPCUnit(Content.Load<UnitType>(@"UnitTypes\Lucifer"), 2, new Vector3(10, 3000, -3000));
                Variables.LastCreatedUnit.Target = Variables.Unit[5];
                Stage.CreateNPCUnit(Content.Load<UnitType>(@"UnitTypes\Lucifer"), 2, new Vector3(10, 3000, -3000));
                Variables.LastCreatedUnit.Target = Variables.Unit[5];
                Stage.CreateNPCUnit(Content.Load<UnitType>(@"UnitTypes\Lucifer"), 2, new Vector3(10, 3300, -3000));
                Variables.LastCreatedUnit.Target = Variables.Unit[5];
                Stage.CreateNPCUnit(Content.Load<UnitType>(@"UnitTypes\Lucifer"), 2, new Vector3(10, 3300, -3000));
                Variables.LastCreatedUnit.Target = Variables.Unit[5];
                Stage.AddGameMessage(@"Jeffsion the Warrior: Enemy missila carriers are approaching!", Color.CornflowerBlue, 4);
                Stage.AddGameMessage(@"Bill: Got it!", Color.CornflowerBlue, 2);
                Stage.AddGameMessage(@"Vector: Don't let them approach!", Color.CornflowerBlue, 4);

                
            }
            if (timer == phase3)
            {
                p = 2;
                Stage.PlayMusic(@"Audio\TitanSlayer_-_Dawning_of_Darkness", true, 10);
                Stage.AddGameMessage(@"Jeffsion the Warrior: The Church! they are behind us!", Color.CornflowerBlue, 4);

                Stage.AddGameMessage(@"Chris the Dark Knight: Long time no see, Vector... or, my dear brother.", Color.Red, 4);
                Stage.AddGameMessage(@"Chris the Dark Knight: Seems you are done for.", Color.Red, 4);
                Stage.AddGameMessage(@"Vector: What are you talking about?", Color.CornflowerBlue, 4);
                Stage.AddGameMessage(@"Chris the Dark Knight: It is us that launched the EMP attack.", Color.Red, 4);
                Stage.AddGameMessage(@"Vector: Zero, deal with the Church", Color.CornflowerBlue, 4);
                Stage.CreateNPCUnit(Content.Load<UnitType>(@"UnitTypes\ShadowMoon"), 4, new Vector3(0, 0, 3500));
                Stage.CreateNPCUnit(Content.Load<UnitType>(@"UnitTypes\Scream"), 4, new Vector3(300, 500, 3500));
                Variables.LastCreatedUnit.Target = Variables.Unit[5];
                Stage.CreateNPCUnit(Content.Load<UnitType>(@"UnitTypes\Scream"), 4, new Vector3(300, 500, 3500));
                Variables.LastCreatedUnit.Target = Variables.Unit[5];
                Stage.CreateNPCUnit(Content.Load<UnitType>(@"UnitTypes\ShadowMoon"), 4, new Vector3(0, 0, 3500));


                Stage.CreateNPCUnit(Content.Load<UnitType>(@"UnitTypes\Falcon3"), 1, new Vector3(0, 5000, 0));
                Variables.LastCreatedUnit.RiderName = "Affron <4rd Fleet>";
                Stage.CreateNPCUnit(Content.Load<UnitType>(@"UnitTypes\Falcon2"), 1, new Vector3(0, 5000, 0));
                Variables.LastCreatedUnit.RiderName = "Alrdis <4rd Fleet Commander>";
                Variables.LastCreatedUnit.SetAI(new AODGameLibrary2.AssistAI(((RegularAI)Variables.LastCreatedUnit.unitAI).settings, Variables.Unit[5]));
                Variables.LastCreatedUnit.IsInvincible = true;
                Stage.CreateNPCUnit(Content.Load<UnitType>(@"UnitTypes\Falcon3"), 1, new Vector3(10, 5000, 0));
                Variables.LastCreatedUnit.RiderName = "Orderi <4rd Fleet>";
                Variables.LastCreatedUnit.IsInvincible = true;
                Stage.AddGameMessage(@"Alrdis: It is 4rd Fleed, looks like we arrived in time!", Color.CornflowerBlue, 4);

            }
            if (timer == phase4)
            {
                p = 3;
                Stage.PlayMusic(@"Audio\Antti_Martikainen_-_Through_Enemy_Lines", true, 10);
                //Stage.AddGameMessage(@"Vector: 快了，就要成功了！", Color.CornflowerBlue, 4);

                Stage.AddGameMessage(@"Vector: We are winning!", Color.CornflowerBlue, 4);
                Stage.CreateNPCUnit(Content.Load<UnitType>(@"UnitTypes\ShadowSlainMK2"), 2, 3500 * AODGameLibrary.Helpers.RandomHelper.RandomDirection());
        
                Stage.CreateNPCUnit(Content.Load<UnitType>(@"UnitTypes\Lucifer"), 2, 3500 * AODGameLibrary.Helpers.RandomHelper.RandomDirection());
                
                Stage.CreateNPCUnit(Content.Load<UnitType>(@"UnitTypes\ShadowSlainMK2"), 2, 3500 * AODGameLibrary.Helpers.RandomHelper.RandomDirection());
              
                Stage.CreateNPCUnit(Content.Load<UnitType>(@"UnitTypes\Lucifer"), 2, 3500 * AODGameLibrary.Helpers.RandomHelper.RandomDirection());
               
         
            }
            
            if (timer == victory)
            {
                Stage.ClearMessages();
                spawn.Pause();
                Stage.Player.IsInvincible = true;
                Variables.Unit[5].IsInvincible = true;
                foreach (Unit u in Stage.AliveUnitsInUnitGroup(2))
                {
                    u.Shield = 0;
                    u.Armor = 1;
                
                }
                foreach (Unit u in Stage.AliveUnitsInUnitGroup(4))
                {
                    u.Shield = 0;
                    u.Armor = 4;

                }
                ((RegularAI)Variables.Unit[5].unitAI).settings.isRotateAble = true;
                ((RegularAI)Variables.Unit[5].unitAI).settings.isMoveAble = true;

                /*
                Stage.AddGameMessage(@"Vector: 终于，完成了。", Color.CornflowerBlue, 4);
                Stage.AddGameMessage(@"Vector: ……让我们向秩序之眼的核心，秩序之环，发起最后的攻击吧！", Color.CornflowerBlue, 4);
                Stage.AddGameMessage(@"Vector: 杰诺，完成你的任务，将“遗迹”带到秩序之眼的核心——秩序之环。", Color.CornflowerBlue, 4);
                Stage.AddGameMessage(@"Vector: 地球联盟的舰队会掩护你。", Color.CornflowerBlue, 4);
                Stage.AddGameMessage(@"Alicia: ……尽管……去……", Color.Yellow, 4);
                */
                Stage.AddGameMessage(@"Vector: Finally it is done.", Color.CornflowerBlue, 4);
                Stage.AddGameMessage("Vector: Let's launch the last strike to the core of EoC\n - The Ring of Cosmos!", Color.CornflowerBlue, 4);
                Stage.AddGameMessage(@"Vector: Zero, you bring the Relic to the Ring.", Color.CornflowerBlue, 4);
                Stage.AddGameMessage(@"Vector: And the whole fleet will cover you.", Color.CornflowerBlue, 4);
                Stage.AddGameMessage(@"Alicia: ...just...go...", Color.Yellow, 4);

                Stage.SavePlayer();
                Stage.EnableStage(4);
                Variables.Unit[5].MoveTo(new Vector3 (0, 0, -10000));
                won = true;
                //Stage.Victory("哈哈！");
            }
            base.Event_TimerRing(timer);
        }
    }
}
