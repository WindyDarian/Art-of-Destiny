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
using Stages.Stage4Parts;
using AODGameLibrary.Ambient;
using Microsoft.Xna.Framework.Media;
using AODGameLibrary.AIs;
using AODGameLibrary.Texts;


namespace Stages
{
    /// <summary>
    /// 第四章-由大地无敌-范若余在2010年5月17日建立
    /// </summary>
    public class Stage4:Stage
    {
        UnitInf bill;
        Timer billRise;

        public Stage4()
            : base()
        {
            
            StageParts.Add(new Stage4_Part1());
            StageParts.Add(new Stage4_Part2());
            StageParts.Add(new Stage4_Part3());
            StageParts.Add(new Stage4_Part4());
        }
        public override void Initialize()
        {
            billRise = GameWorld.CreateTimer(18);
            billRise.Pause();


            LoadPlayer(1, new Vector3(0, 0, 6000));

            //CreatePlayerUnit(Content.Load<UnitType>(@"UnitTypes\Falcon"), 1, new Vector3(0,0,6000));
            Player = Variables.LastCreatedUnit;
            Player.RiderName = "杰诺";
            Player.IsAIControlling = false;
            CreateNPCUnit(Content.Load<UnitType>(@"UnitTypes\Falcon"), 1, new Vector3(30, 30, 5980));
            Variables.Unit[2] = Variables.LastCreatedUnit;
            //Variables.Unit[2].IsInvincible = true;
            Variables.Unit[2].SetAI(new AODGameLibrary2.AssistAI(((RegularAI)Variables.Unit[2].unitAI).settings, Player));
            Variables.LastCreatedUnit.RiderName = "比尔.沃顿";
            bill = new UnitInf(GameWorld);
            GameWorld.AddUI(bill);
            bill.Target = Variables.Unit[2];
            LoadAmbient(@"Ambient\Stage4Ambient");
            ((RegularAI)Player.unitAI).settings.isSkillUsable = false;



            CreateDecoration(Content.Load<AODGameLibrary.Ambient.DecorationType>(@"DecorationTypes\Ball"), new Vector3(0, 0, 0), 4990, Vector3.Zero);
       
        



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
                            AddRealtimeGameMessage(@"比尔：顶住，我需要一点时间恢复！", Color.CornflowerBlue, 2.3f);
                            break;
                        case 2:
                            AddRealtimeGameMessage(@"比尔：我先暂时撤退。", Color.CornflowerBlue, 2);
                            break;
                        case 3:
                            AddRealtimeGameMessage(@"比尔：该死！", Color.CornflowerBlue, 2);
                            break;
                        case 4:
                            AddRealtimeGameMessage(@"杰诺：比尔，你要挂了，赶快闪！", Color.LightGreen, 2);
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
                            AddRealtimeGameMessage(@"比尔：返航！", Color.CornflowerBlue, 2);
                            break;
                        case 2:
                            AddRealtimeGameMessage(@"比尔：恢复战斗，一切正常。", Color.CornflowerBlue, 2);
                            break;
                        case 3:
                            AddRealtimeGameMessage(@"比尔：还好。", Color.CornflowerBlue, 2);
                            break;
                    }
                }
     
                CreateNPCUnit(Content.Load<UnitType>(@"UnitTypes\Falcon"), 1, Player.Position - Player.Face * 200);
                Variables.Unit[2] = Variables.LastCreatedUnit;
                //Variables.Unit[2].IsInvincible = true;
                Variables.Unit[2].SetAI(new AODGameLibrary2.AssistAI(((RegularAI)Variables.Unit[2].unitAI).settings, Player));
                Variables.LastCreatedUnit.RiderName = "比尔.沃顿";
                bill.Target = Variables.Unit[2];
                Variables.Unit[2].Velocity = Player.Velocity;
                Variables.Unit[2].Armor = 400;
                Variables.Unit[2].Shield = 400;

            }
            base.Event_TimerRing(timer);
        }
    }
}
