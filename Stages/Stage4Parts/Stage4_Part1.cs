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
namespace Stages.Stage4Parts
{
    /// <summary>
    /// 由大地无敌-范若余于2010年5月17日建立
    /// </summary>
    public class Stage4_Part1 : StagePart
    {
        Timer t;
        bool b;
        /// <summary>
        /// 初始化
        /// </summary>
        public override void Initialize()
        {
            Stage.GameWorld.InstantMessages.Add(new AODText(Stage.GameWorld, "第四章-午夜之环", 3, Color.CornflowerBlue, new Vector2(Stage.Game.GraphicsDevice.Viewport.Width / 2,
               Stage.Game.GraphicsDevice.Viewport.Height / 3), FadeOutState.HalfFade, Vector2.Zero, true, 2));
            //Stage.CreateNPCUnit(Content.Load<UnitType>(@"UnitTypes\EOSMa"), 2, new Vector3(200, 5200, 0));
            //Stage.CreateNPCUnit(Content.Load<UnitType>(@"UnitTypes\EOSMa"), 2, new Vector3(-200, 5200, 0));
            //Stage.CreateNPCUnit(Content.Load<UnitType>(@"UnitTypes\EOSMa"), 2, new Vector3(200, 2600, 4498));
            //Stage.CreateNPCUnit(Content.Load<UnitType>(@"UnitTypes\EOSMa"), 2, new Vector3(-200, 2600, 4498));
            //Stage.CreateNPCUnit(Content.Load<UnitType>(@"UnitTypes\EOSSh"), 2, new Vector3(200, 2600, -4498));
            //Stage.CreateNPCUnit(Content.Load<UnitType>(@"UnitTypes\EOSMa"), 2, new Vector3(-200, 2600, -4498));
            //Stage.CreateNPCUnit(Content.Load<UnitType>(@"UnitTypes\EOSMi"), 2, new Vector3(200, -2600, 4498));
            //Stage.CreateNPCUnit(Content.Load<UnitType>(@"UnitTypes\EOSMa"), 2, new Vector3(-200, -2600, 4498));
            //Stage.CreateNPCUnit(Content.Load<UnitType>(@"UnitTypes\EOSSh"), 2, new Vector3(200, -2600, -4498));
            //Stage.CreateNPCUnit(Content.Load<UnitType>(@"UnitTypes\EOSMi"), 2, new Vector3(-200, -2600, -4498));
            //Stage.CreateNPCUnit(Content.Load<UnitType>(@"UnitTypes\EOSMi"), 2, new Vector3(200, -5200, 0));
            //Stage.CreateNPCUnit(Content.Load<UnitType>(@"UnitTypes\EOSMi"), 2, new Vector3(-200, -5200, 0));
            //Stage.CreateNPCUnit(Content.Load<UnitType>(@"UnitTypes\EOSMa"), 2, new Vector3(200, 0, 5000));
            //Stage.CreateNPCUnit(Content.Load<UnitType>(@"UnitTypes\EOSMa"), 2, new Vector3(-200, 0, 5000));
            //Stage.CreateNPCUnit(Content.Load<UnitType>(@"UnitTypes\EOSSh"), 2, new Vector3(200, 0, -5000));
            //Stage.CreateNPCUnit(Content.Load<UnitType>(@"UnitTypes\EOSSh"), 2, new Vector3(-200, 0, -5000));
            //Stage.CreateNPCUnit(Content.Load<UnitType>(@"UnitTypes\PowerShield"), 2, new Vector3(0, 5200, 0));
            //Stage.CreateNPCUnit(Content.Load<UnitType>(@"UnitTypes\PowerShield"), 2, new Vector3(0, -5200, 0));
            //Stage.CreateNPCUnit(Content.Load<UnitType>(@"UnitTypes\PowerShield"), 2, new Vector3(0, 2600, 4498));
            //Stage.CreateNPCUnit(Content.Load<UnitType>(@"UnitTypes\PowerShield"), 2, new Vector3(0, 2600, -4498));
            //Stage.CreateNPCUnit(Content.Load<UnitType>(@"UnitTypes\PowerShield"), 2, new Vector3(0, -2600, 4498));
            //Stage.CreateNPCUnit(Content.Load<UnitType>(@"UnitTypes\PowerShield"), 2, new Vector3(0, -2600, -4498));
            t = Stage.CreateTimer(4);
            Stage.GameWorld.MovieModelStart();
            Stage.ScreenEffectManager.Blink(Color.White, 5);

         
                 base.Initialize();
        }
        /// <summary>
        /// 从该片段继续时进行的处理
        /// </summary>
        public override void StartFormThis()
        {
            
       
       


            Stage.Player.Position = new Vector3(-3757, 0, 6500);//初始位置
            Stage.Player.SetMoveState(new Vector3(150, 0, -256), new Vector3(0, -30, 0));
            Variables.Unit[2].Position = new Vector3(-3800, 0, 6300);
            Variables.Unit[2].SetMoveState(new Vector3(150, 0, -256), new Vector3(0, -30, 0));
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
                Stage.AddGameMessage(@"Zero: 这就是秩序之环……", Color.LightGreen, 2);
                Stage.AddGameMessage(@"Bill: 秩序之眼的核心。", Color.CornflowerBlue, 4);
                Stage.AddGameMessage(@"Bill: 盖亚号的轨道弹射系统已经成功将我们弹射到了秩序之环的外围。", Color.CornflowerBlue, 4);
                Stage.AddGameMessage(@"Zero: 只要将“遗迹”向秩序之环中心的能量源抛出去就能解决一切了吧。", Color.LightGreen, 2);
                Stage.AddGameMessage(@"威克多：杰诺、比尔，秩序之环现在由一个能量护盾保护着。", Color.CornflowerBlue, 4);
                Stage.AddGameMessage(@"威克多：必须毁掉六个力场发生器才能进入秩序之环的内部。", Color.CornflowerBlue, 4);
                Stage.AddGameMessage(@"威克多：盖亚号遇到了另一波敌人，稍后才能抵达。", Color.CornflowerBlue, 4);
                Stage.AddGameMessage(@"威克多：一切交给你们了，在秩序之眼注意到之前渗入秩序之环的中心。", Color.CornflowerBlue, 4);
                b = true;
            }
            base.Event_TimerRing(timer);
        }
        
    }
}
