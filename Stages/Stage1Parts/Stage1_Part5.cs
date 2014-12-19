using System;

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

namespace Stages.Stage1Parts
{
    /// <summary>
    /// 和第三舰队会合,由大地无敌-范若余在2009年11月8日建立
    /// </summary>
    public class Stage1_Part5 : StagePart
    {
        Unit bill;
        bool b1 = false;
        bool b2 = false;
        bool b3 = false;
        bool b4 = false;
        Timer t1;
        Timer t2;
        Vector3 destination = new Vector3(0, 0, -50000);
        Mark m;
        /// <summary>
        /// 初始化
        /// </summary>
        public override void Initialize()
        {
            Stage.AddGameMessage(@"比尔：看来暂时是清理干净了。", Color.CornflowerBlue, 3);
            Stage.AddGameMessage(@"杰诺：还有更大的派对在后面。", Color.LightGreen, 3);
            Stage.AddGameMessage(@"比尔：看来你也收到了求救信息，这里离信号发出的地点还有一段距离。", Color.CornflowerBlue, 4);
            Stage.AddGameMessage(@"比尔：我先赶去，你准备好了就跟上。", Color.CornflowerBlue, 3);
            Stage.PlayMusic(@"Audio\Marcello_Morgese_-_Space_Travel", true, 9);
            if (Variables.Unit[2] != null)
            {

                bill = Variables.Unit[2];
            }
            t1 = Stage.CreateTimer(9);
            base.Initialize();
        }
        /// <summary>
        /// 从该片段继续时进行的处理
        /// </summary>
        public override void StartFormThis()
        {
            Stage.Player.Position = new Vector3(0, 100, -12000);//初始位置
            Stage.CreateNPCUnit(Content.Load<UnitType>(@"UnitTypes\Falcon"), 1, new Vector3(0, -100, -12000));
            Variables.Unit[2] = Variables.LastCreatedUnit;
            bill = Variables.Unit[2];
            bill.IsInvincible = true;
            bill.RiderName = "比尔.沃顿";
            //Stage.Player.GetWeapon(@"WeaponTypes\Blav", 20);
            base.StartFormThis();
        }
        /// <summary>
        /// 关卡判断
        /// </summary>
        public override void Touch()
        {
            if (b1 == false)
            {
                if (Vector3.Distance(Stage.Player.Position, destination) < 30000)
                {
                    Stage.CreateNPCUnit(Content.Load<UnitType>(@"UnitTypes\ShadowSlain"), 2, Stage.Player.Position + new Vector3(0, 100, -3000));
                    Variables.LastCreatedUnit.Target = Stage.Player;
                    ((RegularAI)Variables.LastCreatedUnit.unitAI).TargetChangeAble = false;
                    Stage.CreateNPCUnit(Content.Load<UnitType>(@"UnitTypes\ShadowSlain"), 2, Stage.Player.Position + new Vector3(0, -100, -3000));
                    Variables.LastCreatedUnit.Target = Stage.Player;
                    ((RegularAI)Variables.LastCreatedUnit.unitAI).TargetChangeAble = false;
                    b1 = true;
                }
            }
            if (b2 == false)
            {
                if (Vector3.Distance(Stage.Player.Position, destination) < 22000)
                {
                    Stage.CreateNPCUnit(Content.Load<UnitType>(@"UnitTypes\EyesElite"), 2, Stage.Player.Position + new Vector3(-100, 300, -3000));
                    Variables.LastCreatedUnit.Target = Stage.Player;
                    ((RegularAI)Variables.LastCreatedUnit.unitAI).TargetChangeAble = false;
                    b2 = true;
                }
            }
            if (b3 == false)
            {
                if (Vector3.Distance(Stage.Player.Position, destination) < 12000)
                {
                    Stage.CreateNPCUnit(Content.Load<UnitType>(@"UnitTypes\EyesElite"), 2, Stage.Player.Position + new Vector3(0, 0, -3000));
                    Variables.LastCreatedUnit.Target = Stage.Player;
                    ((RegularAI)Variables.LastCreatedUnit.unitAI).TargetChangeAble = false;
                    Stage.CreateNPCUnit(Content.Load<UnitType>(@"UnitTypes\FlameDestroyer"), 2, Stage.Player.Position + new Vector3(-100, 300, -3000));
                    Variables.LastCreatedUnit.Target = Stage.Player;
                    ((RegularAI)Variables.LastCreatedUnit.unitAI).TargetChangeAble = false;
                    b3 = true;
                }
            }
            if (Vector3.Distance(Stage.Player.Position, destination) < 4000)
            {
                bill.IsAIControlling = true;
                m.End();
                Stage.NextPart();
            }

        }
        public override void Event_TimerRing(Timer timer)
        {
            if (timer == t1)
            {
                bill.MoveTo(destination);
                bill.IsAIControlling = false;
                m = Stage.AddPositionMark(destination);
                Stage.AddGameMessage(@"比尔：那么，让我开始前进吧。再见！", Color.CornflowerBlue, 4);
                Stage.AddGameMessage(@"比尔：事实上，我很喜欢看到鲁道夫这个老顽固被打得屁滚尿流的样子。", Color.CornflowerBlue, 4);

            }
            base.Event_TimerRing(timer);
        }
    }
}
