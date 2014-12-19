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
using Stages.OtherParts;
using Microsoft.Xna.Framework.Media;

namespace Stages
{
    /// <summary>
    /// BOSS战测试关卡,由大地无敌-范若余在2009年8月11日创建
    /// </summary>
    public class TestStage : Stage
    {

        public TestStage():base()
        {
            StageParts.Add(new Doom());
           // StageParts.Add(new am());
        }
        public override void Initialize()
        {
            LoadPlayer(1, new Vector3(0, 0, -2000));
            //CreatePlayerUnit(Content.Load<UnitType>(@"UnitTypes\Falcon"), 1, Vector3.Zero +);
            //Variables.LastCreatedUnit.IsInvincible = true;
            Player = Variables.LastCreatedUnit;
            Player.RiderName = "Zero";
            LoadAmbient(@"Ambient\testStageAmbient2");
            //LoadAmbient(@"Ambient\Stage2Ambient");
            base.Initialize();
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
            base.Event_UnitDied(deadUnit);
        }
        public override void Event_TimerRing(Timer timer)
        {

            base.Event_TimerRing(timer);
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
        
       
    }
}
