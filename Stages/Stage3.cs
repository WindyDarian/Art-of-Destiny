﻿using System;
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
            Player.RiderName = "杰诺";
            Player.IsAIControlling = false;
            CreateNPCUnit(Content.Load<UnitType>(@"UnitTypes\Falcon"), 1, Vector3.Zero);
            Variables.Unit[2] = Variables.LastCreatedUnit;
            //Variables.Unit[2].IsInvincible = true;
            Variables.Unit[2].SetAI(new AODGameLibrary2.AssistAI(((RegularAI)Variables.Unit[2].unitAI).settings, Player));
            Variables.LastCreatedUnit.RiderName = "比尔.沃顿";
            bill = new UnitInf(GameWorld);
            GameWorld.AddUI(bill);
            bill.Target = Variables.Unit[2];
            LoadAmbient(@"Ambient\Stage3Ambient");
            ((RegularAI)Player.unitAI).settings.isSkillUsable = false;
            
    


            CreateNPCUnit(Content.Load<UnitType>(@"UnitTypes\Titan"), 1, new Vector3(0, 0, 0));
            Variables.LastCreatedUnit.RiderName = "<地球联盟指挥舰> 威克多";
            ((RegularAI)Variables.LastCreatedUnit.unitAI).settings.isMoveAble = false;
            ((RegularAI)Variables.LastCreatedUnit.unitAI).settings.isRotateAble = false;
            ((RegularAI)Variables.LastCreatedUnit.unitAI).settings.isShotAble = false;
            ((RegularAI)Variables.LastCreatedUnit.unitAI).settings.isSkillUsable = false;
            Variables.Unit[5] = Variables.LastCreatedUnit;
            vector = new UnitInf(GameWorld);
            GameWorld.AddUI(vector);
            vector.Target = Variables.LastCreatedUnit;
            vector.name = "盖亚号";
            vector.position =  new Vector2(5, 410);

            CreateNPCUnit(Content.Load<UnitType>(@"UnitTypes\Phoenix"), 1, new Vector3(10, 0, 0));
            Variables.LastCreatedUnit.SetAI(new AODGameLibrary2.AssistAI(((RegularAI)Variables.LastCreatedUnit.unitAI).settings, Variables.Unit[5]));
            Variables.LastCreatedUnit.IsInvincible = true;
            Variables.LastCreatedUnit.RiderName = "阿贝拉尔 <第一舰队>";
            CreateNPCUnit(Content.Load<UnitType>(@"UnitTypes\Phoenix"), 1, new Vector3(10, 0, 0));
            Variables.LastCreatedUnit.SetAI(new AODGameLibrary2.AssistAI(((RegularAI)Variables.LastCreatedUnit.unitAI).settings, Variables.Unit[5]));
            Variables.LastCreatedUnit.IsInvincible = true;
            Variables.LastCreatedUnit.RiderName = "布克 <第一舰队>";
            CreateNPCUnit(Content.Load<UnitType>(@"UnitTypes\Phoenix"), 1, new Vector3(10, 0, 0));
            Variables.LastCreatedUnit.SetAI(new AODGameLibrary2.AssistAI(((RegularAI)Variables.LastCreatedUnit.unitAI).settings, Variables.Unit[5]));
            Variables.LastCreatedUnit.IsInvincible = true;
            Variables.LastCreatedUnit.RiderName = "阿夫顿 <第一舰队>";
            CreateNPCUnit(Content.Load<UnitType>(@"UnitTypes\Phoenix"), 1, new Vector3(10, 0, 0));
            Variables.LastCreatedUnit.IsInvincible = true;
            Variables.LastCreatedUnit.RiderName = "马萨 <第一舰队指挥官>";
            CreateNPCUnit(Content.Load<UnitType>(@"UnitTypes\Falcon"), 1, new Vector3(10, 0, 0));
            Variables.LastCreatedUnit.SetAI(new AODGameLibrary2.AssistAI(((RegularAI)Variables.LastCreatedUnit.unitAI).settings, Variables.Unit[5]));
            Variables.LastCreatedUnit.IsInvincible = true;
            Variables.LastCreatedUnit.RiderName = "蒙托尔.雷诺 <第三舰队>";
            CreateNPCUnit(Content.Load<UnitType>(@"UnitTypes\Falcon"), 1, new Vector3(10, 0, 0));
            Variables.LastCreatedUnit.SetAI(new AODGameLibrary2.AssistAI(((RegularAI)Variables.LastCreatedUnit.unitAI).settings, Variables.Unit[5]));
            Variables.LastCreatedUnit.IsInvincible = true;
            Variables.LastCreatedUnit.RiderName = "马克斯.派恩 <第三舰队>";
            CreateNPCUnit(Content.Load<UnitType>(@"UnitTypes\Falcon3"), 1, new Vector3(10, 0, 0));
            Variables.LastCreatedUnit.IsInvincible = true;
            Variables.LastCreatedUnit.RiderName = "玛莲娜.林风 <第三舰队>";
            CreateNPCUnit(Content.Load<UnitType>(@"UnitTypes\Falcon2"), 1, new Vector3(10, 0, 0));
            Variables.LastCreatedUnit.IsInvincible = true;
            Variables.LastCreatedUnit.RiderName = "普雷斯.杰斐逊 <第三舰队指挥官>";
            CreateNPCUnit(Content.Load<UnitType>(@"UnitTypes\Falcon3"), 1, new Vector3(10, 0, 0));
            Variables.LastCreatedUnit.SetAI(new AODGameLibrary2.AssistAI(((RegularAI)Variables.LastCreatedUnit.unitAI).settings, Variables.Unit[5]));
            Variables.LastCreatedUnit.IsInvincible = true;
            Variables.LastCreatedUnit.RiderName = "乔治.灰炮 <第三舰队>";
       



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
                            AddRealtimeGameMessage(@"战士杰斐逊：刚才真是危险啊，老大。", Color.CornflowerBlue, 2);
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