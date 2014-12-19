using System;

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

namespace Stages.Stage1Parts
{
    /// <summary>
    /// 一些战斗,外加比尔来支援,由大地无敌-范若余在2009年11月8日建立
    /// </summary>
    public class Stage1_Part4:StagePart
    {
        Vector3 destination = new Vector3(0, 0, -28000);
        bool b1 = false;
        bool b2 = false;
        bool b3 = false;
        bool b4 = false;
        Timer t1;
        Timer t2;
        Unit bill;
        /// <summary>
        /// 初始化
        /// </summary>
        public override void Initialize()
        {
            Stage.PlayMusic(@"Audio\Marcello_Morgese_-_Sounds_Of_The_Night", true, 4);
            Stage.AddGameMessage("秩序之眼爪牙：发现猎物。", Color.Red, 4);

            Stage.AddGameMessage(@"Zero: 看来它们已经控制了这个地方，在支援舰队之前，必须先陪它们玩一下。", Color.LightGreen, 3);

            Stage.CreateNPCUnit(Content.Load<UnitType>(@"UnitTypes\FlameDestroyer"), 2, Stage.Player.Position + new Vector3(0, 0, -3000));

            Stage.CreateNPCUnit(Content.Load<UnitType>(@"UnitTypes\FlameDestroyer"), 2, Stage.Player.Position + new Vector3(0, 200, -2500));

            b1 = true;

            base.Initialize();

        }
        /// <summary>
        /// 从该片段继续时进行的处理
        /// </summary>
        public override void StartFormThis()
        {
            Stage.Player.Position = new Vector3(0, 0, -12000);//初始位置

            //Stage.Player.GetWeapon(@"WeaponTypes\Blav", 5);
            base.StartFormThis();
        }
        /// <summary>
        /// 关卡判断
        /// </summary>
        public override void Touch()
        {
            if (b1 == true && b2 == false )
            {
                if (Stage.AliveUnitsInUnitGroup(2).Count <= 0)
                {
                    Vector3 v = new Vector3(2000, 0, 0);
                    Stage.CreateNPCUnit(Content.Load<UnitType>(@"UnitTypes\Falcon"), 1, Stage.Player.Position - v);
                    Variables.Unit[2] = Variables.LastCreatedUnit;
                    bill = Variables.Unit[2];
                    bill.IsInvincible = true;
                    bill.RiderName = "Bill Warden";
                    Stage.CreateNPCUnit(Content.Load<UnitType>(@"UnitTypes\FlameDestroyer"), 2, Stage.Player.Face * 3000 + Stage.Player.Position);
                    Variables.LastCreatedUnit.unitAI.Target = Stage.Player;
                    bill.unitAI.Target = Variables.LastCreatedUnit;
                    ((RegularAI)bill.unitAI).Threat = 100;
                    
                    Stage.CreateNPCUnit(Content.Load<UnitType>(@"UnitTypes\ShadowSlain"), 2, Stage.Player.Face * -3000 + Stage.Player.Position);
                    Variables.LastCreatedUnit.unitAI.Target = Stage.Player;
                    Stage.CreateNPCUnit(Content.Load<UnitType>(@"UnitTypes\FlameDestroyer"), 2, Stage.Player.Position + v);
                    Variables.LastCreatedUnit.unitAI.Target = bill;
                    ((RegularAI)Variables.LastCreatedUnit.unitAI).Threat = 100;


                    Stage.AddGameMessage(@"Bill: 小心你的背后！", Color.CornflowerBlue, 4);
                    Stage.AddGameMessage(@"Bill: 看来你需要帮忙，朋友，我是Bill Warden，第三舰队的前成员。", Color.CornflowerBlue, 4);
                    Stage.AddGameMessage(@"Bill: 你正在使用的行星突击炮，是很好的武器，", Color.CornflowerBlue, 4);
                    Stage.AddGameMessage(@"Bill: 它可以轻松刺穿敌人的能量护盾，但是对护甲的伤害并不理想。", Color.CornflowerBlue, 4);
                    Stage.AddGameMessage(@"Bill: 如果你让它有足够长的时间冷却，再次射击时就会有短时间的急速状态。", Color.CornflowerBlue, 4);
                    Stage.AddGameMessage(@"Bill: 秩序之眼的能量护盾都是为防御导弹的攻击而安装的，", Color.CornflowerBlue, 4);
                    Stage.AddGameMessage(@"Bill: 如果你在用行星突击炮破掉了对手的护盾后再发射导弹的话……", Color.CornflowerBlue, 4);
                    Stage.AddGameMessage(@"Zero: 够了，你以为我是新手吗？ ", Color.LightGreen, 2);
                    Stage.AddGameMessage(@"Bill: 随便。但是我认为我们现在必须把这里清理一下。", Color.CornflowerBlue, 2);
                    Stage.AddGameMessage(@"Zero: 总之，我是老鸟，这就够了。", Color.LightGreen, 2);
                    t1 = Stage.CreateTimer(22);
                    b2 = true;
                }
            }
            if (b4 == true && Stage.AliveUnitsInUnitGroup(2).Count <= 0)
            {
                Stage.NextPart();

            }
            if (t2!= null)
            {
                if ((t2.IsEnd || Stage.AliveUnitsInUnitGroup(2).Count <= 0) && (b4 == false && b3== true))
                {
                    Stage.CreateNPCUnit(Content.Load<UnitType>(@"UnitTypes\EyesElite"), 2, Stage.Player.Position + new Vector3(0, 0, -3000));

                    Stage.CreateNPCUnit(Content.Load<UnitType>(@"UnitTypes\EyesElite"), 2, Stage.Player.Position + new Vector3(0, 0, -3000));

                    Stage.CreateNPCUnit(Content.Load<UnitType>(@"UnitTypes\EyesElite"), 2, Stage.Player.Position + new Vector3(0, 0, 3000));

                    Stage.AddGameMessage(@"Bill: 这些家伙是秩序之眼的精英！", Color.CornflowerBlue, 4);
                    Stage.AddGameMessage(@"Zero: 比废铁有用一点罢了。", Color.LightGreen, 2);
                    b4 = true;
                }
            }
 
        }
        public override void Event_TimerRing(Timer timer)
        {
            if (timer == t1 && b3 == false)
            {

                Stage.CreateNPCUnit(Content.Load<UnitType>(@"UnitTypes\ShadowSlain"), 2, Stage.Player.Position + new Vector3(0, 0, -3000));

                Stage.CreateNPCUnit(Content.Load<UnitType>(@"UnitTypes\FlameDestroyer"), 2, Stage.Player.Position + new Vector3(0, 0, 3000));

                Stage.CreateNPCUnit(Content.Load<UnitType>(@"UnitTypes\FlameDestroyer"), 2, Stage.Player.Position + new Vector3(0, 3000, 0));

                //Stage.CreateNPCUnit(Content.Load<UnitType>(@"UnitTypes\ShadowSlain"), 2, Stage.Player.Position + new Vector3(0, -3000, 0));

                //Stage.CreateNPCUnit(Content.Load<UnitType>(@"UnitTypes\ShadowSlain"), 2, Stage.Player.Position + new Vector3(3000, 0, 0));

                Stage.CreateNPCUnit(Content.Load<UnitType>(@"UnitTypes\FlameDestroyer"), 2, Stage.Player.Position + new Vector3(-3000, 0, 0));

                Stage.CreateNPCUnit(Content.Load<UnitType>(@"UnitTypes\ShadowSlain"), 2, Stage.Player.Position + new Vector3(-2555, 0, 2555));

                //Stage.CreateNPCUnit(Content.Load<UnitType>(@"UnitTypes\EyesElite"), 2, Stage.Player.Position + new Vector3(100, 0, 8000));

                Stage.AddGameMessage(@"Bill: 朋友，看来我们被包围了。", Color.CornflowerBlue, 4);
                Stage.AddGameMessage(@"Bill: 这些家伙带有导弹，时刻注意自己的能量护盾。", Color.CornflowerBlue, 4);
                Stage.AddGameMessage(@"Zero: 这就是本游侠大显身手的机会么？ ", Color.LightGreen, 3);
                b3 = true;
                t2 = Stage.CreateTimer(95);
            }

            base.Event_TimerRing(timer);
        }
    }
}
