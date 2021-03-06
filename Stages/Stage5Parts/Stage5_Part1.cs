﻿using System;
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

namespace Stages.Stage5Parts
{
     
    /// <summary>
    /// 由大地无敌-范若余于2010年1月31日建立
    /// </summary>
    public class Stage5_Part1 : StagePart
    {
        Timer t;
        bool b;

        /// <summary>
        /// 初始化
        /// </summary>
        public override void Initialize()
        {
            Stage.GameWorld.InstantMessages.Add(new AODText(Stage.GameWorld, "Chapter 5 - Art of Destiny", 3, Color.CornflowerBlue, new Vector2(Stage.Game.GraphicsDevice.Viewport.Width / 2,
                  Stage.Game.GraphicsDevice.Viewport.Height / 3), FadeOutState.HalfFade, Vector2.Zero, true, 2));
          
            t = Stage.CreateTimer(4);
            Stage.GameWorld.MovieModelStart();
            Stage.ScreenEffectManager.Blink(Color.White, 5);
            Stage.ScreenEffectManager.KeepColor(Color.Black, null);
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
            
            if (Stage.IsMessageEnd && b)
            {
                Stage.Player.IsInvincible = false;
                Stage.GameWorld.MovieModelEnd();

                Stage.NextPart();
            }
        }
        public override void Event_TimerRing(Timer timer)
        {
            if (timer == t)
            {
                /*
                Stage.AddGameMessage(@"Alicia: ……好吧，如果你们执意要这样的话。", Color.Yellow, 4);
                Stage.AddGameMessage(@"Alicia: 因为程序的限制，我不能和自己的另一部分交战。", Color.Yellow, 4);
                
                Stage.AddGameMessage(@"Alicia: 秩序之眼现在不受我的控制，", Color.Yellow, 4);
                Stage.AddGameMessage(@"Alicia: 所以，都靠你们了。", Color.Yellow, 4);
                Stage.AddGameMessage(@"Zero: 大干一场。", Color.LightGreen, 2);
                Stage.AddGameMessage(@"…………", Color.White, 4);
                Stage.AddGameMessage(@"Vector: 杰诺、比尔，刚才教会干扰了通信，舰队联系不上你们！", Color.CornflowerBlue, 4);

                Stage.AddGameMessage(@"Vector: 盖亚号现在被一支行动迅速的秩序之眼小队纠缠着，", Color.CornflowerBlue, 2);
                Stage.AddGameMessage(@"Vector: 需要更长的时间才能会合。", Color.CornflowerBlue, 2);
                Stage.AddGameMessage(@"Zero: 一切正常，威克多。我们已经接近秩序之眼的本体了。", Color.LightGreen, 2);
                Stage.AddGameMessage(@"Maria Woodwind: 非常接近。", Color.CornflowerBlue, 4);
                Stage.AddGameMessage(@"Bill: 威克多，告诉我们该干什么。", Color.CornflowerBlue, 4);
                Stage.AddGameMessage(@"Price Jeffsion: 上膛吧！", Color.CornflowerBlue, 4);
                
                 */
                Stage.AddGameMessage(@"Alicia: ...right, if you insist.", Color.Yellow, 4);
                Stage.AddGameMessage("Alicia: I can't fight with the other part of myself due to\nthe hard limitation.", Color.Yellow, 4);

                Stage.AddGameMessage(@"Alicia: The Cosmos is out of my control.", Color.Yellow, 4);
                Stage.AddGameMessage(@"Alicia: So, wish you the best.", Color.Yellow, 4);
                Stage.AddGameMessage(@"Zero: We will do our best.", Color.LightGreen, 2);
                Stage.AddGameMessage(@"...........", Color.White, 4);
                Stage.AddGameMessage(@"Vector: Zero, Bill, are you okay? The communication was disrupted.", Color.CornflowerBlue, 4);

                Stage.AddGameMessage(@"Vector: Some fast EoC forces are attacking Gaia.", Color.CornflowerBlue, 2);
                Stage.AddGameMessage(@"Vector: We need more time to arrive. What's your status?", Color.CornflowerBlue, 2);
                Stage.AddGameMessage(@"Zero: All green, Vector. we are finishing the job.", Color.LightGreen, 2);
                Stage.AddGameMessage(@"Maria Woodwind: And it would be quick.", Color.CornflowerBlue, 4);
                Stage.AddGameMessage(@"Bill: Good luck, Vector.", Color.CornflowerBlue, 4);
                Stage.AddGameMessage(@"Price Jeffsion: Let's finsih this!", Color.CornflowerBlue, 4);
                
                b = true;
            }
            base.Event_TimerRing(timer);
        }
    }
}
