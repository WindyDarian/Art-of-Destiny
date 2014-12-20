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
using AODGameLibrary.Texts;
namespace Stages.Stage2Parts
{
    /// <summary>
    /// 由大地无敌-范若余在2010年1月21日建立
    /// </summary>
    public class Stage2_Part1 : StagePart
    {
        Unit t;
        Unit bill;
        /// <summary>
        /// 初始化
        /// </summary>
        public override void Initialize()
        {
            Stage.GameWorld.InstantMessages.Add(new AODText(Stage.GameWorld, "Chapter 2 - The Moon", 3, Color.CornflowerBlue, new Vector2(Stage.Game.GraphicsDevice.Viewport.Width / 2,
               Stage.Game.GraphicsDevice.Viewport.Height / 3), FadeOutState.HalfFade, Vector2.Zero, true, 2));
           
            bill = Variables.Unit[2];
            bill.IsInvincible = true;


            Stage.PlayMusic(@"Audio\Antti_Martikainen_-_The_Chase", true, 2);
            Stage.CreateNPCUnit(Content.Load<UnitType>(@"UnitTypes\ShadowMoon"), 4, new Vector3(10, 3000, -8000));
            Variables.LastCreatedUnit.Target = bill;
            Stage.CreateNPCUnit(Content.Load<UnitType>(@"UnitTypes\ShadowMoon"), 4, new Vector3(20, 3000, -8000));
            Variables.LastCreatedUnit.Target = bill;
            Stage.CreateNPCUnit(Content.Load<UnitType>(@"UnitTypes\ShadowMoon"), 4, new Vector3(30, 3000, -8000));
            Variables.LastCreatedUnit.Target = bill;
            Stage.CreateNPCUnit(Content.Load<UnitType>(@"UnitTypes\ShadowMoon"), 4, new Vector3(40, 3000, -8000));
            Variables.LastCreatedUnit.Target = bill;
            t = Variables.LastCreatedUnit;
            Stage.GameWorld.MovieModelStart();

            Stage.Player.IsInvincible = true;
            Stage.Player.Target = t;
            
            Stage.GameWorld.SetCamera(new Camera(Stage.Game, new Vector3(20, 2080, -4000),t, Vector3.Up));
            Stage.ScreenEffectManager.Blink(Color.White, 20);

            /*
            Stage.AddFlyingMessage(@"杰诺和第三舰队共同战斗，", Color.Yellow);
            Stage.AddFlyingMessage(@"从秩序之眼的手中夺回了“遗迹”，", Color.Yellow);
            Stage.AddFlyingMessage(@"但因为“遗迹”的不稳定力量，被困于一个遥远星域。", Color.Yellow);
            Stage.AddFlyingMessage(@"他的战斗，远未停止……", Color.Yellow);
            Stage.AddFlyingMessage(@"谜底，尚待揭晓……", Color.Yellow);
            Stage.AddFlyingMessage(@"……", Color.Yellow);
            Stage.AddFlyingMessage(@"分析完毕。", Color.Yellow);
            */
            Stage.AddFlyingMessage(@"Zero fighted together with the 3rd Fleet,", Color.Yellow);
            Stage.AddFlyingMessage(@"And retrived the Relic,", Color.Yellow);
            Stage.AddFlyingMessage(@"But because of its unstable power, they are stuck in a barren space", Color.Yellow);
            Stage.AddFlyingMessage(@"His fate will go on.", Color.Yellow);
            Stage.AddFlyingMessage(@"All is going to be unveiled", Color.Yellow);
            Stage.AddFlyingMessage(@"......", Color.Yellow);
            Stage.AddFlyingMessage(@"Analysis complete.", Color.Yellow);
            base.Initialize();
        }
        /// <summary>
        /// 从该片段继续时进行的处理
        /// </summary>
        public override void StartFormThis()
        {
            Stage.Player.Position = new Vector3(0, 0, 0);//初始位置
            Variables.Unit[2].Position = new Vector3(0, 20, 0);
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
                Stage.GameWorld.ReleaseCamera();
                Stage.GameWorld.MovieModelEnd();
                bill.IsInvincible = false;

                Stage.NextPart();
            }
        }
    }
}
