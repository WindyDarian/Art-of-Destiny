using System;
using System.Collections.Generic;
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
using Stages.Stage3Parts;
using AODGameLibrary.Ambient;
using Microsoft.Xna.Framework.Media;
using AODGameLibrary.AIs;
using AODGameLibrary.Texts;


namespace Stages
{
    /// <summary>
    /// 第三章-由大地无敌-范若余在2010年4月10日建立
    /// </summary>
    public class Stage3:Stage
    {
        UnitInf bill;
        Timer billRise;
        UnitInf vector;

        public Stage3()
            : base()
        {
            
            StageParts.Add(new Stage3_Part1());
            StageParts.Add(new Stage3_Part2());
         
        }
        public override void Initialize()
        {
            billRise = GameWorld.CreateTimer(18);
            billRise.Pause();


            LoadPlayer(1, Vector3.Zero);

            Player = Variables.LastCreatedUnit;
            Player.RiderName = "Zero";
            Player.IsAIControlling = false;
            CreateNPCUnit(Content.Load<UnitType>(@"UnitTypes\Falcon"), 1, Vector3.Zero);
            Variables.Unit[2] = Variables.LastCreatedUnit;
            //Variables.Unit[2].IsInvincible = true;
            Variables.Unit[2].SetAI(new AODGameLibrary2.AssistAI(((RegularAI)Variables.Unit[2].unitAI).settings, Player));
            Variables.LastCreatedUnit.RiderName = "Bill Warden";
            bill = new UnitInf(GameWorld);
            GameWorld.AddUI(bill);
            bill.Target = Variables.Unit[2];
            LoadAmbient(@"Ambient\Stage3Ambient");
            ((RegularAI)Player.unitAI).settings.isSkillUsable = false;
            
    


            CreateNPCUnit(Content.Load<UnitType>(@"UnitTypes\Titan"), 1, new Vector3(0, 0, 0));
            Variables.LastCreatedUnit.RiderName = "<Mothership of the Federation> Vector";
            ((RegularAI)Variables.LastCreatedUnit.unitAI).settings.isMoveAble = false;
            ((RegularAI)Variables.LastCreatedUnit.unitAI).settings.isRotateAble = false;
            ((RegularAI)Variables.LastCreatedUnit.unitAI).settings.isShotAble = false;
            ((RegularAI)Variables.LastCreatedUnit.unitAI).settings.isSkillUsable = false;
            Variables.Unit[5] = Variables.LastCreatedUnit;
            vector = new UnitInf(GameWorld);
            GameWorld.AddUI(vector);
            vector.Target = Variables.LastCreatedUnit;
            vector.name = "Gaia";
            vector.position =  new Vector2(5, 410);

            CreateNPCUnit(Content.Load<UnitType>(@"UnitTypes\Phoenix"), 1, new Vector3(10, 0, 0));
            Variables.LastCreatedUnit.SetAI(new AODGameLibrary2.AssistAI(((RegularAI)Variables.LastCreatedUnit.unitAI).settings, Variables.Unit[5]));
            Variables.LastCreatedUnit.IsInvincible = true;
            Variables.LastCreatedUnit.RiderName = "Abanar <1st Fleet>";
            CreateNPCUnit(Content.Load<UnitType>(@"UnitTypes\Phoenix"), 1, new Vector3(10, 0, 0));
            Variables.LastCreatedUnit.SetAI(new AODGameLibrary2.AssistAI(((RegularAI)Variables.LastCreatedUnit.unitAI).settings, Variables.Unit[5]));
            Variables.LastCreatedUnit.IsInvincible = true;
            Variables.LastCreatedUnit.RiderName = "Book <1st Fleet>";
            CreateNPCUnit(Content.Load<UnitType>(@"UnitTypes\Phoenix"), 1, new Vector3(10, 0, 0));
            Variables.LastCreatedUnit.SetAI(new AODGameLibrary2.AssistAI(((RegularAI)Variables.LastCreatedUnit.unitAI).settings, Variables.Unit[5]));
            Variables.LastCreatedUnit.IsInvincible = true;
            Variables.LastCreatedUnit.RiderName = "Affton <1st Fleet>";
            CreateNPCUnit(Content.Load<UnitType>(@"UnitTypes\Phoenix"), 1, new Vector3(10, 0, 0));
            Variables.LastCreatedUnit.IsInvincible = true;
            Variables.LastCreatedUnit.RiderName = "Matha <1st Fleet Commander>";
            CreateNPCUnit(Content.Load<UnitType>(@"UnitTypes\Falcon"), 1, new Vector3(10, 0, 0));
            Variables.LastCreatedUnit.SetAI(new AODGameLibrary2.AssistAI(((RegularAI)Variables.LastCreatedUnit.unitAI).settings, Variables.Unit[5]));
            Variables.LastCreatedUnit.IsInvincible = true;
            Variables.LastCreatedUnit.RiderName = "Montor Raynor <3rd fleet>";
            CreateNPCUnit(Content.Load<UnitType>(@"UnitTypes\Falcon"), 1, new Vector3(10, 0, 0));
            Variables.LastCreatedUnit.SetAI(new AODGameLibrary2.AssistAI(((RegularAI)Variables.LastCreatedUnit.unitAI).settings, Variables.Unit[5]));
            Variables.LastCreatedUnit.IsInvincible = true;
            Variables.LastCreatedUnit.RiderName = "Maks Payne <3rd Fleet>";
            CreateNPCUnit(Content.Load<UnitType>(@"UnitTypes\Falcon3"), 1, new Vector3(10, 0, 0));
            Variables.LastCreatedUnit.IsInvincible = true;
            Variables.LastCreatedUnit.RiderName = "Maria Woodwind <3rd Fleet>";
            CreateNPCUnit(Content.Load<UnitType>(@"UnitTypes\Falcon2"), 1, new Vector3(10, 0, 0));
            Variables.LastCreatedUnit.IsInvincible = true;
            Variables.LastCreatedUnit.RiderName = "Price Jeffsion <3rd Fleet Commander>";
            CreateNPCUnit(Content.Load<UnitType>(@"UnitTypes\Falcon3"), 1, new Vector3(10, 0, 0));
            Variables.LastCreatedUnit.SetAI(new AODGameLibrary2.AssistAI(((RegularAI)Variables.LastCreatedUnit.unitAI).settings, Variables.Unit[5]));
            Variables.LastCreatedUnit.IsInvincible = true;
            Variables.LastCreatedUnit.RiderName = "Jodge Graycannon <3rd Fleet>";
       



            base.Initialize();
        }
       
        public override void LoadContent()
        {

            Content.Load<Song>(@"Audio\Kai_Engel_-_Beneath_The_Stronghold");
            Content.Load<Song>(@"Audio\Antti_Martikainen_-_The_Chase");
            Content.Load<Song>(@"Audio\Kai_Engel_-_Beneath_The_Stronghold");
            Content.Load<Song>(@"Audio\Antti_Martikainen_-_Through_Enemy_Lines");
            Content.Load<Song>(@"Audio\TitanSlayer_-_Dawning_of_Darkness");
            base.LoadContent();
        }
        public override void StartFromStagePart(int part)
        {
            
            base.StartFromStagePart(part);
        }
        public override void StartNew()
        {
            StartFromStagePart(0);
            base.StartNew();
        }
        public override void Touch()
        {
            
            base.Touch();
        }
        public override void Event_UnitDied(Unit deadUnit)
        {
            if (deadUnit == Variables.Unit[2])
            {
                billRise.Play();
                if (Player.isPlayerControlling)
                {
                    switch (AODGameLibrary.Helpers.RandomHelper.RandomInt(1, 4))
                    {
                        case 1:
                            AddRealtimeGameMessage(@"Bill: Hold on, and buy some time for me!", Color.CornflowerBlue, 2.3f);
                            break;
                        case 2:
                            AddRealtimeGameMessage(@"Bill: I need to retreat at the moment.", Color.CornflowerBlue, 2);
                            break;
                        case 3:
                            AddRealtimeGameMessage(@"Bill: Damn!", Color.CornflowerBlue, 2);
                            break;
                        case 4:
                            AddRealtimeGameMessage(@"Zero: Bill, you are under fire, get out.", Color.LightGreen, 2);
                            break;
                    }
                }
          
                
            }
            base.Event_UnitDied(deadUnit);
        }
        public override void Event_TimerRing(Timer timer)
        {
            if (timer == billRise)
            {
                billRise.Reset();
                billRise.Pause();
                if (Player.isPlayerControlling)
                {
                    switch (AODGameLibrary.Helpers.RandomHelper.RandomInt(1, 3))
                    {
                        case 1:
                            AddRealtimeGameMessage(@"Bill: I am good.", Color.CornflowerBlue, 2);
                            break;
                        case 2:
                            AddRealtimeGameMessage(@"Bill: Returning to battle.", Color.CornflowerBlue, 2);
                            break;
                        case 3:
                            AddRealtimeGameMessage(@"Jeffsion the Warrior: That was close, boss.", Color.CornflowerBlue, 2);
                            break;
                    }
                }
     
                CreateNPCUnit(Content.Load<UnitType>(@"UnitTypes\Falcon"), 1, Player.Position - Player.Face * 200);
                Variables.Unit[2] = Variables.LastCreatedUnit;
                //Variables.Unit[2].IsInvincible = true;
                Variables.Unit[2].SetAI(new AODGameLibrary2.AssistAI(((RegularAI)Variables.Unit[2].unitAI).settings, Player));
                Variables.LastCreatedUnit.RiderName = "Bill Warden";
                bill.Target = Variables.Unit[2];
                Variables.Unit[2].Velocity = Player.Velocity;
                Variables.Unit[2].Armor = 400;
                Variables.Unit[2].Shield = 400;

            }
            base.Event_TimerRing(timer);
        }
    }
}
