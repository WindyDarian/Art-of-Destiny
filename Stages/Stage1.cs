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
using Stages.Stage1Parts;
using AODGameLibrary.Ambient;
using Microsoft.Xna.Framework.Media;

namespace Stages
{

    /// <summary>
    /// 第一章:诸神的黄昏,由大地无敌-范若余在2009年10月29日重建
    /// </summary>
    public class Stage1:Stage
    {
        
        public Stage1():base()
        {
            StageParts.Add(new Stage1_Part1());
            StageParts.Add(new Stage1_Part2());
            StageParts.Add(new Stage1_Part3());
            StageParts.Add(new Stage1_Part4());
            StageParts.Add(new Stage1_Part5());
            StageParts.Add(new Stage1_Part6());
            StageParts.Add(new Stage1_Part7());

        }
        public override void Initialize()
        {
           // CreatePlayerUnit(Content.Load<UnitType>(@"UnitTypes\Falcon"), 1, Vector3.Zero);
           LoadPlayer(1, Vector3.Zero);
            Player = Variables.LastCreatedUnit;
            
            Player.RiderName = "Zero";
            LoadAmbient(@"Ambient\Stage1Ambient");

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
        public override void StartNew()
        {
            StartFromStagePart(0);
            base.StartNew();
        }
        public override void Touch()
        {
            base.Touch();
        }
    }
}
