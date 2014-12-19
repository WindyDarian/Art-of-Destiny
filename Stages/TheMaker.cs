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

namespace Stages
{
    /// <summary>
    /// 制作者，由大地无敌-范若余在2009年11月16日创建
    /// </summary>
    public class TheMaker : Stage
    {

        public TheMaker()
            : base()
        {
            StageParts.Add(new TheMakerPart());
        }
        public override void Initialize()
        {
            
            CreatePlayerUnit(Content.Load<UnitType>(@"UnitTypes\Falcon"), 1, Vector3.Zero + new Vector3(0, 0, -2000));
            //Variables.LastCreatedUnit.IsInvincible = true;
            Player = Variables.LastCreatedUnit;
            Player.EndlessBullets = true;
            LoadAmbient(@"Ambient\testStageAmbient2");
            GameWorld.PlayMusic(@"Audio\Moreno_Visintin_-_Mdnel-Inn",true,5);
            Player.IsInvincible = true;
            Player.RiderName = "杰诺";
            
            Player.GetWeapon(@"WeaponTypes\Blav", 25);
            this.HideUI = true;
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
        
       
    }
}
