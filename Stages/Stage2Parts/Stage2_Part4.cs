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

namespace Stages.Stage2Parts
{
    /// <summary>
    /// 由大地无敌-范若余于2010年1月31日建立
    /// </summary>
    public class Stage2_Part4 : StagePart
    {
        Timer t1;
        /// <summary>
        /// 初始化
        /// </summary>
        public override void Initialize()
        {
            Stage.PlayMusic(@"Audio\Kai_Engel_-_Beneath_The_Stronghold", true, 2);
            /*
            Stage.AddGameMessage(@"Zero: 上膛吧。", Color.LightGreen, 3);
            Stage.AddGameMessage(@"Chris: 异教徒们，别太嚣张了，你们的一举一动完全在教会的掌握之中！", Color.Red, 4);
            Stage.AddGameMessage(@"Chris: 没有任何仁慈，神将赐予你们同等的毁灭。", Color.Red, 4);
            Stage.AddGameMessage(@"Chris: 我，黑骑士，就在星际的门口恭候你们的光临，而且……你们被包围了。", Color.Red, 8);
            t1 = Stage.CreateTimer(2);
            Stage.AddGameMessage(@"Bill: 克雷斯？他是个危险的家伙，一切必须小心。", Color.CornflowerBlue, 4);
            Stage.AddGameMessage(@"Alicia: 小心了，敌人正源源不断地从那个星际之门涌出来。", Color.Yellow, 3);
            */

            Stage.AddGameMessage(@"Zero: Go.", Color.LightGreen, 3);
            Stage.AddGameMessage(@"Chris: Heretics, the 'Hands of Ava' foresees all!", Color.Red, 4);
            Stage.AddGameMessage(@"Chris: And there would be mercy for you.", Color.Red, 4);
            Stage.AddGameMessage(@"Chris: We are waiting for you.", Color.Red, 8);
            t1 = Stage.CreateTimer(2);
            Stage.AddGameMessage(@"Bill: Chris!? He is a dangerous guy.", Color.CornflowerBlue, 4);
            Stage.AddGameMessage(@"Alicia: Watch out, enemies are coming.", Color.Yellow, 3);

            Stage.AddPositionMark(new Vector3(0, 0, -28000));
            base.Initialize();
        }
        /// <summary>
        /// 从该片段继续时进行的处理
        /// </summary>
        public override void StartFormThis()
        {
            Stage.Player.Position = new Vector3(20, 1540, -4500);//初始位置
            base.StartFormThis();
        }
        /// <summary>
        /// 关卡判断
        /// </summary>
        public override void Touch()
        {
            if (Vector3.Distance(Stage.Player.Position, new Vector3(0, 0, -30000)) < 5000)
            {
                Stage.NextPart();
            }
        }
        public override void Event_TimerRing(Timer timer)
        {
            if (timer == t1)
            {
                Stage.CreateNPCUnit(Content.Load<UnitType>(@"UnitTypes\Scream"), 4, Stage.Player.Position + new Vector3(0, 0, -3000));
                Variables.LastCreatedUnit.Target = Stage.Player;
                Stage.CreateNPCUnit(Content.Load<UnitType>(@"UnitTypes\ShadowMoon"), 4, Stage.Player.Position + new Vector3(0, 0, 3000));
                Variables.LastCreatedUnit.Target = Stage.Player;
                Stage.CreateNPCUnit(Content.Load<UnitType>(@"UnitTypes\ShadowMoon"), 4,  new Vector3(0, 3000, -30000));
                Variables.LastCreatedUnit.Target = Stage.Player;
                Stage.CreateNPCUnit(Content.Load<UnitType>(@"UnitTypes\ShadowMoon"), 4,  new Vector3(0, 0, -30000));
                Variables.LastCreatedUnit.Target = Stage.Player;
                Stage.CreateNPCUnit(Content.Load<UnitType>(@"UnitTypes\ShadowMoon"), 4, Stage.Player.Position + new Vector3(0, 0, 2555));
                Variables.LastCreatedUnit.Target = Stage.Player;
            }
            base.Event_TimerRing(timer);
        }
    }
}
