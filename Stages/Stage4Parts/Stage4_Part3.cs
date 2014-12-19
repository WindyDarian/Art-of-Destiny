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
using System.Collections.Generic;
using AODGameLibrary.Texts;

namespace Stages.Stage4Parts
{
    /// <summary>
    /// 由大地无敌-范若余于2010年5月30日建立
    /// </summary>
    public class Stage4_Part3 : StagePart
    {
        Timer t;
        bool b;
        bool m;
        /// <summary>
        /// 初始化
        /// </summary>
        public override void Initialize()
        {
            Stage.GameWorld.MovieModelStart();
            Stage.ScreenEffectManager.Blink(Color.Black, 4);
            Stage.Player.Position = new Vector3(0, 0, 5200);//初始位置
            Stage.Player.SetMoveState(new Vector3(0, 0, 0), new Vector3(0, 0, 0));
            Stage.Player.Restore();

            Variables.Unit[2].Position = new Vector3(40, 10, 5150);
            Variables.Unit[2].SetMoveState(new Vector3(0, 0, 0), new Vector3(0, 0, 0));

            Variables.Unit[3].Position = new Vector3(-40, -10, 5150);
            Variables.Unit[3].SetMoveState(new Vector3(0, 0, 0), new Vector3(0, 0, 0));

            Variables.Unit[4].Position = new Vector3(-40, 10, 5150);
            Variables.Unit[4].SetMoveState(new Vector3(0, 0, 0), new Vector3(0, 0, 0));
            Stage.StopMusic();
            t = Stage.CreateTimer(2);

            base.Initialize();
        }
        /// <summary>
        /// 从该片段继续时进行的处理
        /// </summary>
        public override void StartFormThis()
        {



            Stage.CreateNPCUnit(Content.Load<UnitType>(@"UnitTypes\Falcon2"), 1, new Vector3(0, 20000, 0));
            Variables.LastCreatedUnit.IsInvincible = true;
            Variables.LastCreatedUnit.RiderName = "普雷斯.杰斐逊 <第三舰队指挥官>";
            Variables.Unit[3] = Variables.LastCreatedUnit;

            Stage.CreateNPCUnit(Content.Load<UnitType>(@"UnitTypes\Falcon3"), 1, new Vector3(0, 20000, 0));
            Variables.LastCreatedUnit.IsInvincible = true;
            Variables.LastCreatedUnit.RiderName = "玛莲娜.林风 <第三舰队>";
            Variables.Unit[4] = Variables.LastCreatedUnit;
            Variables.LastCreatedUnit.SetAI(new AODGameLibrary2.AssistAI(((RegularAI)Variables.LastCreatedUnit.unitAI).settings, Variables.Unit[3]));
           

            

            base.StartFormThis();
        }
        /// <summary>
        /// 关卡判断
        /// </summary>
        public override void Touch()
        {
            if (b)
            {
                if (!m)
                {
                    if (Stage.IsMessageEnd)
                    {
                        Stage.PlayMusic(@"Audio\Kai_Engel_-_Beneath_The_Stronghold", true, 2);
                        Stage.GameWorld.MovieModelEnd();
                        m = true;
                        Stage.CreateNPCUnit(Content.Load<UnitType>(@"UnitTypes\ShadowMoon"), 4, new Vector3(0, 0, 6500));
                        Stage.CreateNPCUnit(Content.Load<UnitType>(@"UnitTypes\ShadowMoon"), 4, new Vector3(0, 0, 6500));
                        Stage.CreateNPCUnit(Content.Load<UnitType>(@"UnitTypes\ShadowMoon"), 4, new Vector3(0, 1500, 5000));
                        Stage.CreateNPCUnit(Content.Load<UnitType>(@"UnitTypes\ShadowMoon"), 4, new Vector3(0, 1500, 5000));
                        Stage.CreateNPCUnit(Content.Load<UnitType>(@"UnitTypes\ShadowMoon"), 4, new Vector3(0, -1500, 5000));
                        Stage.CreateNPCUnit(Content.Load<UnitType>(@"UnitTypes\ShadowMoon"), 4, new Vector3(0, -1500, 5000));
                        Stage.CreateNPCUnit(Content.Load<UnitType>(@"UnitTypes\ShadowMoon"), 4, new Vector3(0, 0, 6500));
                        Stage.CreateNPCUnit(Content.Load<UnitType>(@"UnitTypes\ShadowMoon"), 4, new Vector3(0, 0, 6500));
                        Stage.CreateNPCUnit(Content.Load<UnitType>(@"UnitTypes\Scream"), 4, new Vector3(0, -1500, 5000));
                        Stage.CreateNPCUnit(Content.Load<UnitType>(@"UnitTypes\Scream"), 4, new Vector3(0, 1500, 5000));
                        Stage.CreateNPCUnit(Content.Load<UnitType>(@"UnitTypes\Scream"), 4, new Vector3(0, 0, 6500));
                        Stage.CreateNPCUnit(Content.Load<UnitType>(@"UnitTypes\Scream"), 4, new Vector3(0, 0, 6500));
                    }
                }
            
                if (Stage.AliveUnitsInUnitGroup(4).Count <= 0)
                {
                    Stage.StopMusic();
                    Stage.NextPart();
                }
            }
      
        }
        public override void Event_UnitDied(Unit deadUnit)
        {
        
            base.Event_UnitDied(deadUnit);
        }
        public override void Event_TimerRing(Timer timer)
        {
            if (timer == t)
            {
                Stage.AddGameMessage(@"Zero: 全部破坏掉了！", Color.LightGreen, 2);
                Stage.AddGameMessage(@"Bill: 秩序之环的能量正在消失……", Color.CornflowerBlue, 4);
                Stage.AddGameMessage(@"Bill: 很快，我们将面对秩序之眼的本体。", Color.CornflowerBlue, 4);
                Stage.AddGameMessage(@"玛莲娜.林风：抱歉，我们和盖亚号失去了联系。", Color.CornflowerBlue, 4);
                Stage.AddGameMessage(@"普雷斯.杰斐逊：秩序之眼和“遗迹”产生了某些感应，干扰了周围的磁场。", Color.CornflowerBlue, 4);
                Stage.AddGameMessage(@"Bill: 奇怪的是……我们的通话并没有受到干扰。", Color.CornflowerBlue, 4);
                Stage.AddGameMessage(@"Zero: 到达秩序之环的中心后，把“遗迹”扔到能量堆中就可以了吧。", Color.LightGreen, 2);
                Stage.AddGameMessage(@"Bill: 希望如此——", Color.CornflowerBlue, 4);
                Stage.AddGameMessage(@"Alicia: 不要这样做……", Color.Yellow, 4);
                Stage.AddGameMessage(@"Zero: 阿莉西亚，是你吗？", Color.LightGreen, 2);
                Stage.AddGameMessage(@"Alicia: 不要……", Color.Yellow, 4);
                Stage.AddGameMessage(@"Alicia: 杰诺……我发现……这是一个圈套……", Color.Yellow, 4);
                Stage.AddGameMessage(@"Bill: 这是什么意思——", Color.CornflowerBlue, 4);
                Stage.AddGameMessage(@"玛莲娜.林风：是教会！散开！", Color.CornflowerBlue, 4);
                
                b = true;

            }
            base.Event_TimerRing(timer);
        }

    }
}
