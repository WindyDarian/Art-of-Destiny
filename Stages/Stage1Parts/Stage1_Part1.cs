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
using AODGameLibrary.Texts;
namespace Stages.Stage1Parts
{

    /// <summary>
    /// 第一章的第一部分,由大地无敌-范若余在2009年11月3日建立
    /// </summary>
    class Stage1_Part1:AODGameLibrary.GamePlay.StagePart
    {
        Timer t1;
        bool b1;
        public override void Initialize()
        {
            Stage.Player.skills.Clear();
            //Stage.GameWorld.InstantMessages.Add(new AODText(Stage.GameWorld, "第一章-诸神的黄昏", 3, Color.White, new Vector2(Stage.Game.GraphicsDevice.Viewport.Width / 2,
            Stage.GameWorld.InstantMessages.Add(new AODText(Stage.GameWorld, "Chapter 1 - Twilight of Gods", 3, Color.White, new Vector2(Stage.Game.GraphicsDevice.Viewport.Width / 2,
            
            Stage.Game.GraphicsDevice.Viewport.Height / 3 ), FadeOutState.HalfFade, Vector2.Zero, true, 2));
            Stage.Player.isPlayerControlling = false;
            Stage.Player.IsAIControlling = false;
            Stage.ScreenEffectManager.Blink(Color.Black, 40);
            Stage.Player.MoveTo(new Vector3(0, 0, -5000));

            /*
            Stage.AddFlyingMessage(@"光阴似箭……", Color.White);
            Stage.AddFlyingMessage(@"我们的文明不断发展，壮大，然后毁灭……", Color.White);
            Stage.AddFlyingMessage(@"秩序之眼，这个超级计算机接管了帝国的防卫系统。", Color.White);
            Stage.AddFlyingMessage(@"后果，可想而知……", Color.White);
            Stage.AddFlyingMessage(@"", Color.White);
            Stage.AddFlyingMessage(@"黑暗来临……", Color.White);
            Stage.AddFlyingMessage(@"它已经发动了进攻，人类开始了最后的抵抗。", Color.White);
            Stage.AddFlyingMessage(@"幸运的是，只要“遗迹”还在人类手上，它们就无法侵扰地球。", Color.White);
            Stage.AddFlyingMessage(@"", Color.White);
            Stage.AddFlyingMessage(@"现在，", Color.White);
            Stage.AddFlyingMessage(@"星际猎人杰诺，正在赶往一条求救信息的发出地点。", Color.White);
            Stage.AddFlyingMessage(@"而我们的故事，就从这里开始。", Color.White);
            */
            Stage.AddFlyingMessage(@"Time goes fast...", Color.White);
            Stage.AddFlyingMessage(@"We develop, we prevail, and we vanish……", Color.White);
            Stage.AddFlyingMessage(@"Eye of Order - the super computer controlled the defense system of the Empire.", Color.White);
            Stage.AddFlyingMessage(@"And we all know what it had cost.", Color.White);
            Stage.AddFlyingMessage(@"", Color.White);
            Stage.AddFlyingMessage(@"Darkness has come.", Color.White);
            Stage.AddFlyingMessage(@"The robots are invading, we are defending our last hope.", Color.White);
            Stage.AddFlyingMessage("As long as the Relic still belongs to us mankind,\nthey are unable to destroy our homeland.", Color.White);
            Stage.AddFlyingMessage(@"", Color.White);
            Stage.AddFlyingMessage(@"Now", Color.White);
            Stage.AddFlyingMessage(@"Zero the spacehunter, are on his way to the source of a SOS message.", Color.White);
            Stage.AddFlyingMessage(@"And this is the beginning of our story.", Color.White);


            t1 = Stage.CreateTimer(25);
            
            base.Initialize();
        }
        public override void StartFormThis()
        {
            Stage.Player.Position = new Vector3(0, 0, 12000) ;
            base.StartFormThis();
        }
        public override void Touch()
        {
            if (t1 != null)
            {

                if (t1.IsEnd && b1 == false)
                {
                    Stage.AddRealtimeGameMessage(@"Rudolf the Commander: 呼叫附近星域的各单位，我们正在受到攻击！", Color.CornflowerBlue, 4);
                    Stage.AddRealtimeGameMessage(@"Rudolf the Commander: 我是第三舰队指挥官鲁道夫.巴特尔，", Color.CornflowerBlue, 3);
                    Stage.AddRealtimeGameMessage(@"Rudolf the Commander: 我们正在受到秩序之眼的攻击！", Color.CornflowerBlue, 3);
                    Stage.AddRealtimeGameMessage(@"Rudolf the Commander: 重复，我们正在受到秩序之眼的攻击！", Color.CornflowerBlue, 4);
                    Stage.AddRealtimeGameMessage(@"Rudolf the Commander: 损失惨重，请求支援！", Color.CornflowerBlue, 2);
                    Stage.AddRealtimeGameMessage(@"Rudolf the Commander: 该死，它们在攻击运输船！", Color.CornflowerBlue, 3);
                    Stage.AddRealtimeGameMessage(@"Rudolf the Commander: 重整队形，保护运输船！第三舰队重整队形！", Color.CornflowerBlue, 3);
                    Stage.AddRealtimeGameMessage(@"Rudolf the Commander: 它们摧毁了运输机！我们被包围了", Color.CornflowerBlue, 3);
                    Stage.AddRealtimeGameMessage(@"Rudolf the Commander: 那是什么？不！", Color.CornflowerBlue, 3);
                    Stage.AddRealtimeGameMessage(@"…… …… ……", Color.White, 2);
                    Stage.AddRealtimeGameMessage(@"苍隼系统信息：与目标失去联系。", Color.White, 4);
                    b1 = true;
                }
            }
            if (Stage.IsMessageEnd && b1 == true)
            {
                Stage.Player.Stop();
                Stage.Player.isPlayerControlling = true;
                Stage.Player.IsAIControlling = true;
                Stage.NextPart();
            }
        }
    }
}
