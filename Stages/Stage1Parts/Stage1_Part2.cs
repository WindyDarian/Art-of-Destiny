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
                Stage.AddTooltipMessage(@"Tap W(LS) to turn off/on engine.", Color.LightYellow, 8);
                Stage.AddTooltipMessage(@"Press right mouse (LT) button to aim", Color.LightYellow, 8);
                Stage.AddTooltipMessage(@"Press left mouse (RT) button to shoot", Color.LightYellow, 8);
                Stage.AddTooltipMessage(@"Press F(RB) to launch missile (less effective for energy shield) ", Color.LightYellow, 8);
                Stage.CreateNPCUnit(Content.Load<UnitType>(@"UnitTypes\FlameDestroyer"), 2, new Vector3(0, 2000, -7250));
                Stage.AddTooltipMessage(@"Hold S(B) to brake.", Color.LightYellow, 8);
                Stage.AddTooltipMessage(@"Hold Space(A) to auto-aim. Press Q(Y) to lock on a target.", Color.LightYellow, 8);
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
                //Stage.AddTooltipMessage(@"靠近敌人战机残留的发光物可以回收一些可利用的东西。", Color.LightYellow, 8);
                Stage.AddTooltipMessage(@"Approach glowing remains to loot.", Color.LightYellow, 8);
              
            }
            base.Event_UnitDied(deadUnit);
        }
       
    }
}
