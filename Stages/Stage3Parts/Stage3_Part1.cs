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
    /// 由大地无敌-范若余于2010年4月10日建立
    /// </summary>
    public class Stage3_Part1 : StagePart
    {
        /// <summary>
        /// 初始化
        /// </summary>
        public override void Initialize()
        {
            Stage.GameWorld.InstantMessages.Add(new AODText(Stage.GameWorld, "Chapter 3 - Flowing Light", 3, Color.CornflowerBlue, new Vector2(Stage.Game.GraphicsDevice.Viewport.Width / 2,
               Stage.Game.GraphicsDevice.Viewport.Height / 3), FadeOutState.HalfFade, Vector2.Zero, true, 2));
           
            
            Stage.GameWorld.MovieModelStart();
            Stage.ScreenEffectManager.Blink(Color.White, 20);
            /*
            Stage.AddFlyingMessage(@"杰诺通过星门，和地球联盟的军队会合", Color.Yellow);
            Stage.AddFlyingMessage(@"共同向秩序之眼的发起袭击。", Color.Yellow);
            Stage.AddFlyingMessage(@"然而威克多指挥的第三舰队指挥舰——盖亚号，", Color.Yellow);
            Stage.AddFlyingMessage(@"受到了不明EMP导弹的攻击陷入瘫痪", Color.Yellow);
            Stage.AddFlyingMessage(@"时间紧迫……", Color.Yellow);
            Stage.AddFlyingMessage(@"……", Color.Yellow);
            Stage.AddFlyingMessage(@"分析完毕。", Color.Yellow);

             */

            Stage.AddFlyingMessage(@"Zero made through the Star Gate, and regrouped with the 3rd Fleet.", Color.Yellow);
            Stage.AddFlyingMessage(@"The legion began to advance to the heart of Eye of Cosmos.", Color.Yellow);
            Stage.AddFlyingMessage(@"But Gaia - the mothership of Vector's Fleet", Color.Yellow);
            Stage.AddFlyingMessage("Are attacked by a wave of EMP strike,\nand It takes time for the mothership function again.", Color.Yellow);
            Stage.AddFlyingMessage(@"Time is short...", Color.Yellow);
            Stage.AddFlyingMessage(@"......", Color.Yellow);
            Stage.AddFlyingMessage(@"Analysis complete.", Color.Yellow);
            base.Initialize();
        }
        /// <summary>
        /// 从该片段继续时进行的处理
        /// </summary>
        public override void StartFormThis()
        {
            
       
       


            Stage.Player.Position = new Vector3(400, 0, 500);//初始位置
            base.StartFormThis();
        }
        /// <summary>
        /// 关卡判断
        /// </summary>
        public override void Touch()
        {
            if (Stage.IsMessageEnd)
            {
                Stage.Player.IsInvincible = false;
                Stage.GameWorld.MovieModelEnd();

                Stage.NextPart();
            }
        }
    }
}
