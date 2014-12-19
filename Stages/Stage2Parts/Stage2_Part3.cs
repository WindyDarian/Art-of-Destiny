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

namespace Stages.Stage2Parts
{
    /// <summary>
    /// 由大地无敌-范若余于2010年2月3日建立
    /// </summary>
    public class Stage2_Part3 : StagePart
    {
       
        bool c1;
        bool c2;

        /// <summary>
        /// 初始化
        /// </summary>
        public override void Initialize()
        {
            Stage.StopMusic();
            Stage.ScreenEffectManager.Blink(Color.Black, 2);
            Stage.Player.isPlayerControlling = false;
            Stage.Player.IsAIControlling = false;
            Stage.Player.Position = new Vector3(20, 1540, -4000);
            Variables.Unit[2].Position = new Vector3(20, 1500, -3200);
            Stage.Player.SetMoveState(Vector3.Zero, Vector3.Zero);
            Variables.Unit[2].SetMoveState(Vector3.Zero, Vector3.Zero);
            Stage.GameWorld.SetCamera(new Camera(Stage.Game, new Vector3(10, 1550, -3980), Stage.Player, Vector3.Up));
            Stage.ScreenEffectManager.Blink(Color.Black, 5);
            Stage.GameWorld.MovieModelStart();

            Stage.GameWorld.GameMessageBox.StartToCount(39);

            Stage.AddGameMessage(@"Zero: 我讨厌这鬼地方。", Color.LightGreen, 2);
            Stage.AddGameMessage(@"Alicia: ……杰诺，还有比尔，你们能听到吗？一切正常？", Color.Yellow, 3);
            Stage.AddGameMessage(@"Zero: 在本游侠的处理下，一切太正常不过了。", Color.LightGreen, 3);
            Stage.AddGameMessage(@"Alicia: 太好了！", Color.Yellow, 3);
            Stage.AddGameMessage(@"Bill: 女士，我不知道你是如何做到在这么远的距离使声音如此清楚。", Color.CornflowerBlue, 4);
            Stage.AddGameMessage(@"Bill: 我必须怀疑……", Color.CornflowerBlue, 4);
            Stage.AddGameMessage(@"Alicia: 怀疑吗……你们当然有权利怀疑我……", Color.Yellow, 3);
            Stage.AddGameMessage(@"Zero: 但不得不说如果没有你，我们已经死光了。", Color.LightGreen, 3);
            Stage.AddGameMessage(@"Zero: 所以，你有本游侠的……感谢。", Color.LightGreen, 3);
            Stage.AddGameMessage(@"Zero: 比尔，她是阿莉西亚，刚才那个对付那个大家伙也有一部分她的功劳。", Color.LightGreen, 3);
            Stage.AddGameMessage(@"Bill: 好吧，我现在暂时相信你，女士。", Color.CornflowerBlue, 4);
            Stage.AddGameMessage(@"Bill: 盖亚号，比尔正在呼叫，请作出回应。", Color.CornflowerBlue, 4);
            Stage.AddGameMessage(@"…………", Color.White, 3);
            Stage.AddGameMessage(@"威克多：比尔，第三舰队已经向我报告了你现在的情况。", Color.Yellow, 4);
            Stage.AddGameMessage(@"威克多：你们正处在外围星域，那里布满了伊瓦之手的兵力。", Color.Yellow, 4);
            Stage.AddGameMessage(@"威克多：根据我们的定位追踪，遗迹在你朋友的手上。", Color.Yellow, 4);
            Stage.AddGameMessage(@"Zero: 我是他朋友？", Color.LightGreen, 3);
            Stage.AddGameMessage(@"Bill: 威克多，我们正准备突破最近的星门，我们需要一个回到休斯星域的坐标。", Color.CornflowerBlue, 4);
            Stage.AddGameMessage(@"威克多：不，我们会给你提供另一个坐标。", Color.Yellow, 4);
            Stage.AddGameMessage(@"Zero: 什么？", Color.LightGreen, 2);
            Stage.AddGameMessage(@"威克多：我们打算直接进攻秩序之眼的核心，“遗迹”将是这个计划必不可少的东西。", Color.Yellow, 4);
            Stage.AddGameMessage(@"Zero: 好吧，那么“遗迹”有什么用？", Color.LightGreen, 2);
            Stage.AddGameMessage("威克多：十五年前，在228号行星“安纳希尔”的底层深处的废墟中，我们发现了一块\r\n神秘的远古水晶……", Color.Yellow, 4);
            Stage.AddGameMessage(@"威克多：我们把这块水晶叫做——“遗迹”……", Color.Yellow, 4);
            Stage.AddGameMessage("威克多：“遗迹”中蕴藏着的，是无尽的知识与财富……于是我们使用这无法被理解的\r\n知识，制造了最强大的控制中心——秩序之眼，并且将它和人类的意志相结合。", Color.Yellow, 4);
            Stage.AddGameMessage(@"威克多：十年来，秩序之眼一直保卫着人类的安全……", Color.Yellow, 4);
            Stage.AddGameMessage(@"Zero: 然后程序出了BUG，对吧？", Color.LightGreen, 2);
            Stage.AddGameMessage(@"威克多：审判来临……整个宇宙陷入了无尽的战争……", Color.Yellow, 4);
            Stage.AddGameMessage(@"威克多：中止战争的方法只有一个，就是重新格式化秩序之眼的系统……", Color.Yellow, 4);
            Stage.AddGameMessage("威克多：当秩序之眼的核心和“遗迹”接触时，格式化便会启动，秩序之眼便会回到最\r\n初的状态……", Color.Yellow, 4);
            Stage.AddGameMessage(@"威克多：战争的轮回，便会终结……", Color.Yellow, 4);
            Stage.AddGameMessage(@"Zero: 听起来很美。", Color.LightGreen, 2);
            Stage.AddGameMessage(@"威克多：这个计划很简单……该死，教会正在监听我们的通信。", Color.Yellow, 4);
            Stage.AddGameMessage(@"威克多：不能让他们知道太多，总之，你们必须赶到星门！", Color.Yellow, 4);
            Stage.AddGameMessage(@"威克多：比尔，我以这次行动的总指挥官的身份，恢复你第三舰队指挥官的职务。", Color.Yellow, 4);
            Stage.AddGameMessage(@"威克多：我会在你到达星门后提供目标坐标，第三舰队会在那边接应。", Color.Yellow, 4);
            Stage.AddGameMessage(@"威克多：不要让我再次失望，Bill Warden。", Color.Yellow, 4);
            Stage.AddGameMessage(@"…………", Color.White, 3);
            Stage.AddGameMessage(@"Alicia: 嘿，这个指挥官疯了吗？", Color.Yellow, 4);
            Stage.AddGameMessage(@"Bill: 虽然我不知道细节，但这个行动简直是以卵击石。", Color.CornflowerBlue, 4);
            Stage.AddGameMessage(@"Bill: 杰诺，我们能够做到，至少能够突破星门。", Color.CornflowerBlue, 4);
            Stage.AddGameMessage(@"Bill: 突破星门之后，你有两个选择。", Color.CornflowerBlue, 4);
            Stage.AddGameMessage(@"Bill: 其一是，如果你想活下去，你可以在星门那边把“遗迹”交给我。", Color.CornflowerBlue, 4);
            Stage.AddGameMessage(@"Bill: 然后我会派人护送你安全返回。而第二个选择……", Color.CornflowerBlue, 4);
            Stage.AddGameMessage(@"Zero: 那么，我选第二个。", Color.LightGreen, 2);
            Stage.AddGameMessage(@"Alicia: 杰诺，真的吗？你会为了与自己毫不相干的东西而战斗下去？", Color.Yellow, 4);
            Stage.AddGameMessage(@"Alicia: 或许只是因为好奇着，只是因为期待着吧。", Color.Yellow, 4);
            Stage.AddGameMessage(@"Alicia: 但你还是选择了自己的路。", Color.Yellow, 3);
            Stage.AddGameMessage(@"Bill: 看来你已经决定好了，杰诺。那么，我们前进吧。", Color.CornflowerBlue, 4);

           
            base.Initialize();
        }
        /// <summary>
        /// 从该片段继续时进行的处理
        /// </summary>
        public override void StartFormThis()
        {
            Stage.Player.Position = new Vector3(20 , 1540, -4000);//初始位置
            base.StartFormThis();
        }
        /// <summary>
        /// 关卡判断
        /// </summary>
        public override void Touch()
        {
            if (Stage.IsMessageEnd)
            {
                 Stage.GameWorld.ReleaseCamera();
                 Stage.GameWorld.MovieModelEnd();
                 Stage.Player.isPlayerControlling = true;
                 Stage.ScreenEffectManager.Blink(Color.Black, 5);
                 Stage.NextPart();
            }
            if (c1== false&&Stage.GameWorld.GameMessageBox.CountNumber==21)
            {
                Stage.ScreenEffectManager.Blink(Color.Black, 5);
                Stage.GameWorld.SetCamera(new Camera(Stage.Game, new Vector3(0, 100, -28500), new Vector3(0, 0, -30000), Vector3.Up));
              
                c1 = true;
            }
            else if (c2 == false &&Stage.GameWorld.GameMessageBox.CountNumber==12)
            {
                Stage.ScreenEffectManager.Blink(Color.Black, 5);
                Stage.GameWorld.SetCamera(new Camera(Stage.Game, Stage.Player.Position + new Vector3(12, 12, -12), Stage.Player, Vector3.Up));
       
            }
        }
    }
}
