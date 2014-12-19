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

namespace Stages.Stage6Parts
{
     
    /// <summary>
    /// 由大地无敌-范若余于2010年1月31日建立
    /// </summary>
    public class Stage6_Part1 : StagePart
    {
        bool b;

        /// <summary>
        /// 初始化
        /// </summary>
        public override void Initialize()
        {
            Stage.GameWorld.InstantMessages.Add(new AODText(Stage.GameWorld, "第六章-重铸秩序", 3, Color.White, new Vector2(Stage.Game.GraphicsDevice.Viewport.Width / 2,
                  Stage.Game.GraphicsDevice.Viewport.Height / 3), FadeOutState.HalfFade, Vector2.Zero, true, 2));

            Stage.PlayMusic(@"Audio\TitanSlayer_-_Dawning_of_Darkness", true, 2);
            Stage.GameWorld.MovieModelStart();
            Stage.ScreenEffectManager.Blink(Color.Black, 5);
            Stage.ScreenEffectManager.KeepColor(Color.White, null);
            base.Initialize();
            Stage.AddFlyingMessage(@"人类，救了我……", Color.Yellow);
            Stage.AddFlyingMessage(@"欺诈者的野心成为空想……", Color.Yellow);
            Stage.AddFlyingMessage(@"…………", Color.Yellow);
            Stage.AddFlyingMessage(@"一切都结束了，", Color.Yellow);
            Stage.AddFlyingMessage(@"除了一件事——", Color.Yellow);
            Stage.AddFlyingMessage(@"审判。", Color.Yellow);
            Stage.AddFlyingMessage(@"分析完毕。", Color.Yellow);
      
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
            if (Stage.IsMessageEnd && !b)
            {
                Stage.AddGameMessage(@"威克多：这不是……这不在我的计划中……", Color.Red, 2);
                Stage.AddGameMessage(@"威克多：这不是我想要的结果！", Color.Red, 2);
                Stage.AddGameMessage(@"威克多：全军，杀掉这几个叛徒！", Color.Red, 2);

                Stage.AddGameMessage(@"比尔：威克多，你被逮捕了，", Color.CornflowerBlue, 4);
                Stage.AddGameMessage(@"比尔：让我们护送盖亚号回法庭吧。", Color.CornflowerBlue, 4);

                Stage.AddGameMessage(@"威克多：我，才是最高指挥官！", Color.Red, 2);
                Stage.AddGameMessage(@"威克多：我，才是世界的主宰！", Color.Red, 2);
                b = true;
     
            }
            else if (Stage.IsMessageEnd && b)
            {
                Stage.Player.IsInvincible = false;
                Stage.GameWorld.MovieModelEnd();

                Stage.NextPart();
            }
        }
        public override void Event_TimerRing(Timer timer)
        {
            base.Event_TimerRing(timer);
        }
    }
}
