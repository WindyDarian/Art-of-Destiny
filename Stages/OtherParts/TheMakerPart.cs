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

namespace Stages.OtherParts
{
    /// <summary>
    /// 制作者，由大地无敌-范若余于2009年11月16日创建
    /// </summary>
    public class TheMakerPart : StagePart
    {
        List<Unit> f = new List<Unit>(4);
        List<Unit> e = new List<Unit>(10);
        /// <summary>
        /// 初始化
        /// </summary>
        public override void Initialize()
        {

            Stage.AddFlyingMessage(@"命运艺术", Color.Yellow);
            Stage.AddFlyingMessage(@"Art of Destiny", Color.Yellow);
            Stage.AddFlyingMessage(@"制作表", Color.White);
            Stage.AddFlyingMessage(@"", Color.White);
            Stage.AddFlyingMessage(@"", Color.White);
            Stage.AddFlyingMessage(@"", Color.White);
            Stage.AddFlyingMessage(@"", Color.White);
            Stage.AddFlyingMessage(@"", Color.White);
            Stage.AddFlyingMessage(@"游戏总设计师：", Color.White);
            Stage.AddFlyingMessage(@"大地无敌", Color.White);
            Stage.AddFlyingMessage(@"", Color.White);
            Stage.AddFlyingMessage(@"", Color.White);
            Stage.AddFlyingMessage(@"", Color.White);
            Stage.AddFlyingMessage(@"", Color.White);
            Stage.AddFlyingMessage(@"剧情：", Color.White);
            Stage.AddFlyingMessage(@"大地无敌", Color.White);
            Stage.AddFlyingMessage(@"", Color.White);
            Stage.AddFlyingMessage(@"", Color.White);
            Stage.AddFlyingMessage(@"", Color.White);
            Stage.AddFlyingMessage(@"", Color.White);
            Stage.AddFlyingMessage(@"创意：", Color.White);
            Stage.AddFlyingMessage(@"大地无敌", Color.White);
            Stage.AddFlyingMessage(@"", Color.White);
            Stage.AddFlyingMessage(@"", Color.White);
            Stage.AddFlyingMessage(@"", Color.White);
            Stage.AddFlyingMessage(@"", Color.White);
            Stage.AddFlyingMessage(@"平面美术：", Color.White);
            Stage.AddFlyingMessage(@"大地无敌", Color.White);
            Stage.AddFlyingMessage(@"", Color.White);
            Stage.AddFlyingMessage(@"", Color.White);
            Stage.AddFlyingMessage(@"", Color.White);
            Stage.AddFlyingMessage(@"", Color.White);
            Stage.AddFlyingMessage(@"3D美术：", Color.White);
            Stage.AddFlyingMessage(@"大地无敌", Color.White);
            Stage.AddFlyingMessage(@"", Color.White);
            Stage.AddFlyingMessage(@"", Color.White);
            Stage.AddFlyingMessage(@"", Color.White);
            Stage.AddFlyingMessage(@"", Color.White);
            Stage.AddFlyingMessage(@"音效：", Color.White);
            Stage.AddFlyingMessage(@"大地无敌", Color.White);
            Stage.AddFlyingMessage(@"", Color.White); 
            Stage.AddFlyingMessage(@"", Color.White);
            Stage.AddFlyingMessage(@"", Color.White);
            Stage.AddFlyingMessage(@"", Color.White);
            Stage.AddFlyingMessage(@"程序架构：", Color.White);
            Stage.AddFlyingMessage(@"大地无敌", Color.White);
            Stage.AddFlyingMessage(@"", Color.White);
            Stage.AddFlyingMessage(@"", Color.White);
            Stage.AddFlyingMessage(@"", Color.White);
            Stage.AddFlyingMessage(@"", Color.White);
            Stage.AddFlyingMessage(@"代码编写：", Color.White);
            Stage.AddFlyingMessage(@"大地无敌", Color.White);
            Stage.AddFlyingMessage(@"", Color.White);
            Stage.AddFlyingMessage(@"", Color.White);
            Stage.AddFlyingMessage(@"", Color.White);
            Stage.AddFlyingMessage(@"", Color.White);
            Stage.AddFlyingMessage(@"测试员：", Color.White);
            Stage.AddFlyingMessage(@"大地无敌", Color.White);
            Stage.AddFlyingMessage(@"", Color.White);
            Stage.AddFlyingMessage(@"", Color.White);
            Stage.AddFlyingMessage(@"", Color.White);
            Stage.AddFlyingMessage(@"", Color.White);
            Stage.AddFlyingMessage(@"感谢XNA游戏世界(http://www.xnaer.com)", Color.White);
            Stage.AddFlyingMessage(@"", Color.White);
            Stage.AddFlyingMessage(@"", Color.White);
            Stage.AddFlyingMessage(@"", Color.White);
            Stage.AddFlyingMessage(@"", Color.White);
            Stage.AddFlyingMessage(@"", Color.White);
            Stage.AddFlyingMessage(@"Copyright @ 大地无敌@大地天下(http://www.agrp.info) . All rights reserved", Color.White);
            Stage.AddFlyingMessage(@"", Color.White);
            Stage.AddFlyingMessage(@"", Color.White);
            Stage.AddFlyingMessage(@"", Color.White);
            Stage.AddFlyingMessage(@"", Color.White);
            Stage.AddFlyingMessage(@"", Color.White);
            Stage.AddFlyingMessage(@"更多信息，请访问http://www.agrp.info", Color.White);
            Stage.AddFlyingMessage(@"", Color.White);
            Stage.AddFlyingMessage(@"制作表已显示完毕，可在任何时候按Esc退出", Color.White);
            base.Initialize();
        }
        /// <summary>
        /// 从该片段继续时进行的处理
        /// </summary>
        public override void StartFormThis()
        {

            Stage.Player.Position = new Vector3(0, 0, 0);//初始位置
            Stage.CreateNPCUnit(Content.Load<UnitType>(@"UnitTypes\Falcon"), 1, new Vector3(0, -100, 0));
            Stage.CreateNPCUnit(Content.Load<UnitType>(@"UnitTypes\Falcon2"), 1, new Vector3(0, 0, -100));
            Stage.CreateNPCUnit(Content.Load<UnitType>(@"UnitTypes\Falcon3"), 1, new Vector3(-100, 0, 0));
            Stage.CreateNPCUnit(Stage.Content.Load<UnitType>(@"UnitTypes\ShadowSlain"), 2, Stage.Player.Position + new Vector3(0, 0, -6000));
            Stage.CreateNPCUnit(Stage.Content.Load<UnitType>(@"UnitTypes\ShadowSlain"), 2, Stage.Player.Position + new Vector3(0, 0, 6200));
            Stage.CreateNPCUnit(Stage.Content.Load<UnitType>(@"UnitTypes\ShadowSlain"), 2, Stage.Player.Position + new Vector3(0, -6400, 0));
            Stage.CreateNPCUnit(Stage.Content.Load<UnitType>(@"UnitTypes\FlameDestroyer"), 2, Stage.Player.Position + new Vector3(0, 6500, 0));
            Stage.CreateNPCUnit(Stage.Content.Load<UnitType>(@"UnitTypes\EyesElite"), 2, Stage.Player.Position + new Vector3(6900, 0, 0));
            Stage.CreateNPCUnit(Stage.Content.Load<UnitType>(@"UnitTypes\FlameDestroyer"), 2, Stage.Player.Position + new Vector3(4000, 2000, 0));
            Stage.CreateNPCUnit(Stage.Content.Load<UnitType>(@"UnitTypes\EyesElite"), 2, Stage.Player.Position + new Vector3(4999, 2111, 0));
            Stage.CreateNPCUnit(Stage.Content.Load<UnitType>(@"UnitTypes\EyesElite"), 2, Stage.Player.Position + new Vector3(4999, 2111, 0));
  
            Stage.ScreenEffectManager.Blink(Color.Black, 20);
            base.StartFormThis();
        }
        /// <summary>
        /// 关卡判断
        /// </summary>
        public override void Touch()
        {
            if (Stage.AliveUnitsInUnitGroup(1).Count<4)
            {

                Stage.CreateNPCUnit(Content.Load<UnitType>(@"UnitTypes\Falcon"), 1, Stage.Player.Position + Stage.Player.Face * 2000);
            }
            else if (Stage.AliveUnitsInUnitGroup(2).Count < 4)
            {
                Stage.CreateNPCUnit(Content.Load<UnitType>(@"UnitTypes\EyesElite"), 2, Stage.Player.Position + Stage.Player.Face * -2000);

            }
            Stage.ClearLoots();
        }
        public override void Event_PlayerDied(Unit player)
        {


            base.Event_PlayerDied(player);
        }
        public override void Event_UnitDied(Unit deadUnit)
        {

            base.Event_UnitDied(deadUnit);
        }
    }
}
