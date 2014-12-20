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
    /// 由大地无敌-范若余于2010年5月26日建立
    /// </summary>
    public class Stage4_Part2 : StagePart
    {
        List<Unit> shields = new List<Unit>(6);
        int n = 6;
        AODText te;
        bool m;

        /// <summary>
        /// 初始化
        /// </summary>
        public override void Initialize()
        {

            Stage.CreateNPCUnit(Content.Load<UnitType>(@"UnitTypes\EOSMa"), 2, new Vector3(200, 5200, 0));
            Stage.CreateNPCUnit(Content.Load<UnitType>(@"UnitTypes\EOSMa"), 2, new Vector3(-200, 5200, 0));
            Stage.CreateNPCUnit(Content.Load<UnitType>(@"UnitTypes\EOSMa"), 2, new Vector3(200, 2600, 4498));
            Stage.CreateNPCUnit(Content.Load<UnitType>(@"UnitTypes\EOSMa"), 2, new Vector3(-200, 2600, 4498));
            Stage.CreateNPCUnit(Content.Load<UnitType>(@"UnitTypes\EOSSh"), 2, new Vector3(200, 2600, -4498));
            Stage.CreateNPCUnit(Content.Load<UnitType>(@"UnitTypes\EOSMa"), 2, new Vector3(-200, 2600, -4498));
            Stage.CreateNPCUnit(Content.Load<UnitType>(@"UnitTypes\EOSMi"), 2, new Vector3(200, -2600, 4498));
            Stage.CreateNPCUnit(Content.Load<UnitType>(@"UnitTypes\EOSMa"), 2, new Vector3(-200, -2600, 4498));
            Stage.CreateNPCUnit(Content.Load<UnitType>(@"UnitTypes\EOSSh"), 2, new Vector3(200, -2600, -4498));
            Stage.CreateNPCUnit(Content.Load<UnitType>(@"UnitTypes\EOSMi"), 2, new Vector3(-200, -2600, -4498));
            Stage.CreateNPCUnit(Content.Load<UnitType>(@"UnitTypes\EOSMi"), 2, new Vector3(200, -5200, 0));
            Stage.CreateNPCUnit(Content.Load<UnitType>(@"UnitTypes\EOSMi"), 2, new Vector3(-200, -5200, 0));
            Stage.CreateNPCUnit(Content.Load<UnitType>(@"UnitTypes\EOSMa"), 2, new Vector3(200, 0, 5000));
            Stage.CreateNPCUnit(Content.Load<UnitType>(@"UnitTypes\EOSMa"), 2, new Vector3(-200, 0, 5000));
            Stage.CreateNPCUnit(Content.Load<UnitType>(@"UnitTypes\EOSSh"), 2, new Vector3(200, 0, -5000));
            Stage.CreateNPCUnit(Content.Load<UnitType>(@"UnitTypes\EOSSh"), 2, new Vector3(-200, 0, -5000));
            Stage.CreateNPCUnit(Content.Load<UnitType>(@"UnitTypes\PowerShield"), 2, new Vector3(0, 5200, 0));
            shields.Add(Variables.LastCreatedUnit);
            Stage.CreateNPCUnit(Content.Load<UnitType>(@"UnitTypes\PowerShield"), 2, new Vector3(0, -5200, 0));
            shields.Add(Variables.LastCreatedUnit);
            Stage.CreateNPCUnit(Content.Load<UnitType>(@"UnitTypes\PowerShield"), 2, new Vector3(0, 2600, 4498));
            shields.Add(Variables.LastCreatedUnit);
            Stage.CreateNPCUnit(Content.Load<UnitType>(@"UnitTypes\PowerShield"), 2, new Vector3(0, 2600, -4498));
            shields.Add(Variables.LastCreatedUnit);
            Stage.CreateNPCUnit(Content.Load<UnitType>(@"UnitTypes\PowerShield"), 2, new Vector3(0, -2600, 4498));
            shields.Add(Variables.LastCreatedUnit);
            Stage.CreateNPCUnit(Content.Load<UnitType>(@"UnitTypes\PowerShield"), 2, new Vector3(0, -2600, -4498));
            shields.Add(Variables.LastCreatedUnit);


         
            Stage.CreateNPCUnit(Content.Load<UnitType>(@"UnitTypes\Falcon2"), 1, new Vector3(0, 20000, 0));
            Variables.LastCreatedUnit.IsInvincible = true;
            Variables.LastCreatedUnit.RiderName = "Price Jeffsion <3rd Fleet Commander>";
            Variables.Unit[3] = Variables.LastCreatedUnit;
            Variables.LastCreatedUnit.MoveTo(new Vector3(0, 6000, 0));

            Stage.CreateNPCUnit(Content.Load<UnitType>(@"UnitTypes\Falcon3"), 1, new Vector3(0, 20000, 0));
            Variables.LastCreatedUnit.IsInvincible = true;
            Variables.LastCreatedUnit.RiderName = "Maria Woodwind <3rd Fleet>";
            Variables.Unit[4] = Variables.LastCreatedUnit;
            Variables.LastCreatedUnit.SetAI(new AODGameLibrary2.AssistAI(((RegularAI)Variables.LastCreatedUnit.unitAI).settings, Variables.Unit[3]));
           

            te = new AODText(Stage.GameWorld, "", 0, Color.White, new Vector2(Stage.GameWorld.game.GraphicsDevice.Viewport.Width / 2, 130), FadeOutState.None, Vector2.Zero, true);
            Stage.GameWorld.InstantMessages.Add(te);
            te.Text = "Field Generator Destroyed: " + (6 - n) + "/" + "6";
            Stage.PlayMusic(@"Audio\Marcello_Morgese_-_Sounds_Of_The_Night", true, 4);
            base.Initialize();
        }
        /// <summary>
        /// 从该片段继续时进行的处理
        /// </summary>
        public override void StartFormThis()
        {
            
       
       


            Stage.Player.Position = new Vector3(-3757, 0, 6500);//初始位置
            Stage.Player.SetMoveState(new Vector3(150, 0, -256), new Vector3(0, -30, 0));

            base.StartFormThis();
        }
        /// <summary>
        /// 关卡判断
        /// </summary>
        public override void Touch()
        {
            if (!m)
            {
                if (Unit.Distance(Stage.Player, Variables.Unit[3]) < 1000)
                {

                    Stage.AddGameMessage(@"Bill: Jeffsion, nice to see you all.", Color.CornflowerBlue, 4);
                    Stage.AddGameMessage(@"Maria Woodwind: Support team of 3rd Fleet is here.", Color.CornflowerBlue, 4);
                    Stage.AddGameMessage(@"Price Jeffsion: Boss! Zero! Let's see who is better in this field.", Color.CornflowerBlue, 4);
                    Stage.AddGameMessage(@"Zero: Yeah, just roll it.", Color.LightGreen, 2);
                      m = true;
                }
            }
        }
        public override void Event_UnitDied(Unit deadUnit)
        {
            if (shields.Contains(deadUnit))
            {
                n -= 1;
                te.Text = "Field Generator Destroyed: " + (6 - n) + "/" + "6";
                if (n >= 2)
                {
                    if (Stage.Player.Position != Vector3.Zero)
                    {
                        Stage.CreateNPCUnit(Content.Load<UnitType>(@"UnitTypes\FlameDestroyerMK2"), 2, Vector3.Normalize(Stage.Player.Position) * -5000);
                    }
                }
             
                if (n== 0)
                {
                    foreach (Unit u in Stage.AliveUnitsInUnitGroup(2))
                    {
                        u.BeginToDie();
                    }
                    Stage.GameWorld.InstantMessages.Remove(te);
                    Stage.NextPart();
                 
                }
            }
            base.Event_UnitDied(deadUnit);
        }

    }
}
