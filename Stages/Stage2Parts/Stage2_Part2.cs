﻿using System;
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

namespace Stages.Stage2Parts
{
    /// <summary>
    /// 由大地无敌-范若余于2010年1月31日建立
    /// </summary>
    public class Stage2_Part2 : StagePart
    {
        /// <summary>
        /// 初始化
        /// </summary>
        public override void Initialize()
        {
            Variables.Unit[2].IsInvincible = true;
            Stage.PlayMusic(@"Audio\Kai_Engel_-_Beneath_The_Stronghold", true, 2);
            /*
            Stage.AddGameMessage(@"Zero: 比尔？", Color.LightGreen, 2);
            Stage.AddGameMessage(@"Bill: 你一个人可应付不了这些敌人。", Color.CornflowerBlue, 4);
            Stage.AddGameMessage(@"Zero: 看来我不能独自拯救世界了。", Color.LightGreen, 3);
            Stage.AddGameMessage(@"Bill: 听我说，虫洞很快就消失了，第三舰队没能跟上来。", Color.CornflowerBlue, 4);
          
            Stage.AddGameMessage(@"Zero: 这是什么该死的地方？", Color.LightGreen, 3);
            Stage.AddGameMessage(@"Bill: 我们现在在外围星域，看来这里完全被教会控制了。", Color.CornflowerBlue, 4);
            Stage.AddGameMessage(@"Zero: “伊瓦”教会，我听说过。", Color.LightGreen, 3);
            Stage.AddGameMessage(@"Bill: 我已经搜索到这附近的一个星门，我们只能通过星门返回休斯星域。", Color.CornflowerBlue, 4);
            Stage.AddGameMessage(@"Bill: 在这之前，我会设法与一个老朋友取得联系，看来他的计划险些泡汤。", Color.CornflowerBlue, 4);
            Stage.AddGameMessage(@"Bill: 我想你听说过他——地球联盟舰队总指挥官威克多.杰克逊。", Color.CornflowerBlue, 4);
            */


            Stage.AddGameMessage(@"Zero: Bill?", Color.LightGreen, 2);
            Stage.AddGameMessage(@"Bill: I assume you cannot do this alone.", Color.CornflowerBlue, 4);
            Stage.AddGameMessage(@"Zero: Okay, I just wanted save this world by myself.", Color.LightGreen, 3);
            Stage.AddGameMessage("Bill: Listen, the wormhole decomposed fast and only you and me\nmade it here.", Color.CornflowerBlue, 4);

            Stage.AddGameMessage(@"Zero: So what is this place", Color.LightGreen, 3);
            Stage.AddGameMessage(@"Bill: A side star system controlled by the Church", Color.CornflowerBlue, 4);
            Stage.AddGameMessage(@"Zero: 'Hands of Ava', I know their name.", Color.LightGreen, 3);
            Stage.AddGameMessage(@"Bill: There is a Star Gate nearby, and we can return to Xius from there", Color.CornflowerBlue, 4);
            Stage.AddGameMessage(@"Bill: Then I'll make contact to an old friend, and you may know him.", Color.CornflowerBlue, 4);
            Stage.AddGameMessage(@"Bill: The general commander of the Federation Fleets, Vector Jackson.", Color.CornflowerBlue, 4);


            base.Initialize();
        }
        /// <summary>
        /// 从该片段继续时进行的处理
        /// </summary>
        public override void StartFormThis()
        {
            Stage.CreateNPCUnit(Content.Load<UnitType>(@"UnitTypes\ShadowMoon"), 4, new Vector3(-20, 1521, -5100));
            Stage.CreateNPCUnit(Content.Load<UnitType>(@"UnitTypes\ShadowMoon"), 4, new Vector3(40, 1500, -4900));
            Stage.Player.Position = new Vector3(20, 1500, -4000);//初始位置
            Variables.Unit[2].Position = new Vector3(100, 1480, -3800);
            base.StartFormThis();
        }
        /// <summary>
        /// 关卡判断
        /// </summary>
        public override void Touch()
        {
            if (Stage.IsMessageEnd && Stage.AliveUnitsInUnitGroup(4).Count <= 0)
            {
                Variables.Unit[2].IsInvincible = false;
                Stage.NextPart();
            }
        }
    }
}
