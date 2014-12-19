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

namespace Stages.Stage1Parts
{
    /// <summary>
    /// 第一章的第二部分，面对一个敌人,由大地无敌-范若余在2009年11月4日建立
    /// </summary>
    public class Stage1_Part2:AODGameLibrary.GamePlay.StagePart
    {
        Timer t;
        Timer t2;
        Unit firstEnemy;
        public override void Initialize()
        {
            



            t = Stage.CreateTimer(3);
            base.Initialize();
        }
        public override void StartFormThis()
        {

            Stage.Player.Position = new Vector3(0, 0, -5000);
            base.StartFormThis();
        }
        public override void Touch()
        {
        }
        public override void Event_TimerRing(Timer timer)
        {
            if (timer == t)
            {
                Stage.AddTooltipMessage(@"转动鼠标移动视角,按W打开/关闭引擎", Color.LightYellow, 8);
                Stage.AddTooltipMessage(@"按住鼠标右键进行瞄准", Color.LightYellow, 8);
                Stage.AddTooltipMessage(@"按住鼠标左键射击", Color.LightYellow, 8);
                Stage.AddTooltipMessage(@"按下F键发射导弹", Color.LightYellow, 8);
                Stage.CreateNPCUnit(Content.Load<UnitType>(@"UnitTypes\FlameDestroyer"), 2, new Vector3(0, 2000, -7250));
                Stage.PlayMusic(@"Audio\Marcello_Morgese_-_Sounds_Of_The_Night", true, 2);
                firstEnemy = Variables.LastCreatedUnit;
            }
            else if (timer ==t2 )
            {
                Stage.NextPart();
            }
            base.Event_TimerRing(timer);
        }
        public override void Event_UnitDied(Unit deadUnit)
        {
            if (deadUnit== firstEnemy)
            {
                t2 = Stage.CreateTimer(4);
                Stage.AddTooltipMessage(@"靠近敌人战机残留的发光物可以回收一些可利用的东西。", Color.LightYellow, 8);
              
            }
            base.Event_UnitDied(deadUnit);
        }
       
    }
}
