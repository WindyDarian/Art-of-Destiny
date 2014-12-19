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

namespace Stages.Stage1Parts
{
    /// <summary>
    /// 前往下一个战斗地点,由大地无敌-范若余在2009年11月6日建立
    /// </summary>
    public class Stage1_Part3:StagePart
    {
        bool b1= false;
        Vector3 destination = new Vector3(0, 0, -28000);
        Mark m;
        /// <summary>
        /// 初始化
        /// </summary>
        public override void Initialize()
        {

            Stage.AddGameMessage(@"Zero: 看来一场大的派对在等着我……", Color.LightGreen, 3);
            Stage.AddGameMessage(@"Zero: 在本游侠赶到之前，你们这些“正规军”就先继续撑着吧！", Color.LightGreen, 4);
            
            base.Initialize();
        }
        /// <summary>
        /// 从该片段继续时进行的处理
        /// </summary>
        public override void StartFormThis()
        {
            Stage.Player.Position = new Vector3(0, 0, -5000);
            //Stage.Player.GetWeapon(@"WeaponTypes\Blav", 5);
            base.StartFormThis();
        }
        /// <summary>
        /// 关卡判断
        /// </summary>
        public override void Touch()
        {
            if (Stage.IsMessageEnd && b1 == false)
            {
                Stage.AddTooltipMessage(@"目标地点已经标注，全速前进吧！", Color.LightYellow, 15);
                m = Stage.AddPositionMark(destination);
                Stage.CreateNPCUnit(Content.Load<UnitType>(@"UnitTypes\FlameDestroyer"), 2, new Vector3(0, 100, -20000));
                Stage.PlayMusic(@"Audio\Marcello_Morgese_-_Space_Travel", false, 5);
                b1 = true;

            }
            if (Vector3.Distance(Stage.Player.Position, destination) < 12000 && b1 == true)
            {
                m.End();
                Stage.NextPart();
            }
        }
        /// <summary>
        /// 计时器响应
        /// </summary>
        /// <param name="timer"></param>
        public override void Event_TimerRing(Timer timer)
        {
            base.Event_TimerRing(timer);
        }
        /// <summary>
        /// 单位死亡
        /// </summary>
        /// <param name="deadUnit"></param>
        public override void Event_UnitDied(Unit deadUnit)
        {
            base.Event_UnitDied(deadUnit);
        }
    }
}
