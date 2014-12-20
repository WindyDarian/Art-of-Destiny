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

namespace Stages.Stage1Parts
{
    /// <summary>
    /// BOSS战前的部分,由大地无敌-范若余在2009年11月8日建立
    /// </summary>
    public class Stage1_Part6 : StagePart
    {

        Vector3 destination = new Vector3(0, 0, -50000);
        Unit bill;
        Unit rudolf;
        Unit je;
        Unit boss;
        bool b1 = false;
        bool b2 = false;
        bool b3 = false;
        bool b4 = false;
        Timer t1;
        Timer t2;
        Mark m;
        /// <summary>
        /// 初始化
        /// </summary>
        public override void Initialize()
        {
            if (Variables.Unit[2] != null)
            {

                bill = Variables.Unit[2];
            }
            foreach (Unit u in Stage.AliveUnitsInUnitGroup(2))
            {
                u.BeginToDie();
            }
            Stage.StopMusic();
            Stage.CreateNPCUnit(Content.Load<UnitType>(@"UnitTypes\Falcon2"), 1, destination + new Vector3(100, 100, 100));
            rudolf = Variables.LastCreatedUnit;
            Variables.Unit[3] = Variables.LastCreatedUnit;
            rudolf.IsInvincible = true;
            rudolf.RiderName = "Rudolf Barter";
            Stage.CreateNPCUnit(Content.Load<UnitType>(@"UnitTypes\Falcon"), 1, destination + new Vector3(100, -100, 100));
            je = Variables.LastCreatedUnit;
            Variables.Unit[4] = Variables.LastCreatedUnit;
            je.IsInvincible = true;
            je.RiderName = "Price Jeffsion";
            Stage.CreateNPCUnit(Content.Load<UnitType>(@"UnitTypes\Falcon"), 1, destination + new Vector3(-100, 100, 100));
            Variables.Unit[7] = Variables.LastCreatedUnit;
            Variables.LastCreatedUnit.RiderName = "Maria Woodwind";
            Stage.CreateNPCUnit(Content.Load<UnitType>(@"UnitTypes\Falcon3"), 1, destination + new Vector3(100, -200, 100));
            Variables.Unit[8] = Variables.LastCreatedUnit;
            Variables.LastCreatedUnit.RiderName = "Maks Payne";
            Stage.CreateNPCUnit(Content.Load<UnitType>(@"UnitTypes\Falcon3"), 1, destination + new Vector3(100, 100, -200));
            Variables.Unit[9] = Variables.LastCreatedUnit;
            Variables.LastCreatedUnit.RiderName = "乔治.灰炮";
            Stage.CreateNPCUnit(Content.Load<UnitType>(@"UnitTypes\Falcon"), 1, destination + new Vector3(100, -200, 100));
            Variables.Unit[10] = Variables.LastCreatedUnit;
            Variables.LastCreatedUnit.RiderName = "蒙托尔.雷诺";
            /*
            Stage.AddGameMessage(@"Bill: 鲁道夫，很高兴“再次”见到你。", Color.CornflowerBlue, 4);
            Stage.AddGameMessage(@"Bill: 我的骄傲的第三舰队呢？只剩下这些了么？", Color.CornflowerBlue, 4);
            Stage.AddGameMessage(@"Rudolf the Commander: 比尔？你已经被除名了！", Color.CornflowerBlue, 4);
            Stage.AddGameMessage(@"Zero: 这里没有派对吗？", Color.LightGreen, 4);
            Stage.AddGameMessage(@"Rudolf the Commander: 哼。这些脆弱的家伙……我的舰队刚刚将它们击退。", Color.CornflowerBlue, 4);
            Stage.AddGameMessage(@"Jeffsion the Warrior: 老大，这次是一次绝密的军事行动，我们只带了轻型舰只。", Color.CornflowerBlue, 4);
            Stage.AddGameMessage(@"Jeffsion the Warrior: 然后来了一大批敌人，抢走了运输船上的东西。", Color.CornflowerBlue, 4);
            Stage.AddGameMessage(@"Rudolf the Commander: 够了！我才是你老大！", Color.CornflowerBlue, 4);
            Stage.AddGameMessage(@"Zero: 看来我是来错地方了，如果没有别的事，那我就先离开了。", Color.LightGreen, 3);
            Stage.AddGameMessage(@"Bill: 只是抢走东西，这不像是机器的作风，它们抢走了什么？", Color.CornflowerBlue, 3);
            Stage.AddGameMessage(@"Rudolf the Commander: 这根本不重要。", Color.CornflowerBlue, 4);
            Stage.AddGameMessage(@"Jeffsion the Warrior: 是的，当它受到冲击时，只是发出了很小的波动……", Color.CornflowerBlue, 4);
            Stage.AddGameMessage(@"Jeffsion the Warrior: 然后整个星域的通讯都瘫痪了那么一小下……", Color.CornflowerBlue, 3);
            Stage.AddGameMessage(@"Rudolf the Commander: 傻瓜！我没有让你说！", Color.CornflowerBlue, 4);
            Stage.AddGameMessage(@"Bill: 看来你丢了一件重要的东西。", Color.CornflowerBlue, 3);
             */


            Stage.AddGameMessage(@"Bill: Nice to see you again, Rudolf.", Color.CornflowerBlue, 4);
            Stage.AddGameMessage(@"Bill: Where is my proud 3rd Fleet?", Color.CornflowerBlue, 4);
            Stage.AddGameMessage(@"Rudolf the Commander: Bill!? You are already expelled!", Color.CornflowerBlue, 4);
            Stage.AddGameMessage(@"Zero: Well, no party here?", Color.LightGreen, 4);
            Stage.AddGameMessage(@"Rudolf the Commander: They retreated just now. And for we are strong.", Color.CornflowerBlue, 4);
            Stage.AddGameMessage(@"Jeffsion the Warrior: Boss, this was a secret operation.", Color.CornflowerBlue, 4);
            Stage.AddGameMessage(@"Jeffsion the Warrior: And the object we were escorting is lost!", Color.CornflowerBlue, 4);
            Stage.AddGameMessage(@"Rudolf the Commander: Enough! I am your boss!", Color.CornflowerBlue, 4);
            Stage.AddGameMessage(@"Zero: Well, if there is no party, I am leaving.", Color.LightGreen, 3);
            Stage.AddGameMessage(@"Bill: Retreat... it doesn't seem like what the EoC would do..", Color.CornflowerBlue, 3);
            Stage.AddGameMessage(@"Rudolf the Commander: It is not important.", Color.CornflowerBlue, 4);
            Stage.AddGameMessage("Jeffsion the Warrior: Yeah, when and that object disrupted the communication \nwhole system just now", Color.CornflowerBlue, 4);
            Stage.AddGameMessage(@"Jeffsion the Warrior: And it was not important...", Color.CornflowerBlue, 3);
            Stage.AddGameMessage(@"Rudolf the Commander: Fool! I am not letting you say!", Color.CornflowerBlue, 4);
            Stage.AddGameMessage(@"Bill: Looks like you lost a precious treasure.", Color.CornflowerBlue, 3);

            t1 = Stage.CreateTimer(10);
            base.Initialize();
        }
        /// <summary>
        /// 从该片段继续时进行的处理
        /// </summary>
        public override void StartFormThis()
        {
            Stage.Player.Position = new Vector3(0, 0, -46000);//初始位置
            Stage.CreateNPCUnit(Content.Load<UnitType>(@"UnitTypes\Falcon"), 1, new Vector3(0, -100, -50000));
            Variables.Unit[2] = Variables.LastCreatedUnit;
            bill = Variables.Unit[2];
            bill.IsInvincible = true;

            bill.RiderName = "Bill Warden";
            //Stage.Player.GetWeapon(@"WeaponTypes\Blav", 25);
            base.StartFormThis();
        }
        /// <summary>
        /// 关卡判断
        /// </summary>
        public override void Touch()
        {
            if (b1 == true && b2 == false)
            {
                if (Stage.AliveUnitsInUnitGroup(2).Count <= 0)
                {
                    Stage.StopMusic();
                    /*
                    Stage.AddGameMessage(@"Bill: 第三舰队，重整队形！", Color.CornflowerBlue, 3);
                    Stage.AddGameMessage(@"Bill: 看来形势越来越严峻了。我们必须活着返回附近的避难所！", Color.CornflowerBlue, 4);
                    Stage.AddGameMessage(@"Rudolf the Commander: 比尔，我再警告你一次，这里只有我才是长官。", Color.CornflowerBlue, 4);
                    Stage.AddGameMessage(@"Alicia: 丢掉“遗迹”想掩盖事实的人还敢自称“长官”？", Color.Yellow, 4);
                    Stage.AddGameMessage(@"Bill: 什么？谁在说话？所以你把“遗迹”丢了，鲁道夫？", Color.CornflowerBlue, 3);
                    Stage.AddGameMessage(@"Rudolf the Commander: 是的！“遗迹”丢了！这原本是一次秘密的运送任务！", Color.CornflowerBlue, 4);
                    Stage.AddGameMessage(@"Jeffsion the Warrior: 是“遗迹”？你个老家伙为什么到现在告诉我们？", Color.CornflowerBlue, 3);
                    Stage.AddGameMessage(@"Rudolf the Commander: 管他呢，事实上我们都不知道它的作用不是吗？", Color.CornflowerBlue, 3);
                    Stage.AddGameMessage(@"Rudolf the Commander: 这东西只是联邦拿来鼓舞人心的没用的东西，我们谁也不说就是了。", Color.CornflowerBlue, 4);
                    Stage.AddGameMessage(@"Bill: 鲁道夫，你在拿人类的未来开玩笑。", Color.CornflowerBlue, 3);
                    
                     */

                    Stage.AddGameMessage(@"Bill: Regroup, memebers of the 3rd fleet!", Color.CornflowerBlue, 3);
                    Stage.AddGameMessage(@"Bill: We must return to the Shelter alive!", Color.CornflowerBlue, 4);
                    Stage.AddGameMessage(@"Rudolf the Commander: Bill, I warn you again, I am who is in charge here.", Color.CornflowerBlue, 4);
                    Stage.AddGameMessage(@"Alicia: You lost the Relic and you define yourself a commander?.", Color.Yellow, 4);
                    Stage.AddGameMessage(@"Bill: What? Who is speaking?", Color.CornflowerBlue, 3);
                    Stage.AddGameMessage(@"Rudolf the Commander: Yes, the Relic is lost, that was our secret escort mission.", Color.CornflowerBlue, 4);
                    Stage.AddGameMessage(@"Jeffsion the Warrior: What!? That thing was Relic!? 'Our last hope'?", Color.CornflowerBlue, 3);
                    Stage.AddGameMessage(@"Rudolf the Commander: Whatever, I don't think that thing matters.", Color.CornflowerBlue, 3);
                    Stage.AddGameMessage(@"Rudolf the Commander: It is useless, maybe the Federation are just using it to fool us.", Color.CornflowerBlue, 4);
                    Stage.AddGameMessage(@"Bill: We need to get it back.", Color.CornflowerBlue, 3);
                    


                    m = Stage.AddPositionMark(destination);
                    b2 = true;
                    t2 = Stage.CreateTimer(10);
                    foreach (Unit u in Stage.AliveUnitsInUnitGroup(1))
                    {
                        if (u!= Stage.Player)
                        {
                            u.MoveTo(destination );
                        }
                    }
                    
                }
            }
            if (b2 == true && b3 == false)
            {
                if (Vector3.Distance(Stage.Player.Position,destination)<=1300)
                {
                    if (t2.IsEnd)
                    {
                        m.End();
                        Stage.NextPart();

                    }
                }
            }
        }
        public override void Event_TimerRing(Timer timer)
        {
            if (timer == t1)
            {
                Stage.PlayMusic(@"Audio\Marcello_Morgese_-_Sounds_Of_The_Night", true, 4);
                Stage.CreateNPCUnit(Content.Load<UnitType>(@"UnitTypes\EyesElite"), 2, destination  + new Vector3(0, 0, -6000));
                Stage.CreateNPCUnit(Content.Load<UnitType>(@"UnitTypes\EyesElite"), 2, destination + new Vector3(0, 0, -6200));
                Stage.CreateNPCUnit(Content.Load<UnitType>(@"UnitTypes\EyesElite"), 2, destination + new Vector3(0, 0, -6400));
                Stage.CreateNPCUnit(Content.Load<UnitType>(@"UnitTypes\EyesElite"), 2, destination + new Vector3(0, 0, -5900));
                Stage.CreateNPCUnit(Content.Load<UnitType>(@"UnitTypes\EyesElite"), 2, destination + new Vector3(0, 0, -6100));
                Stage.CreateNPCUnit(Content.Load<UnitType>(@"UnitTypes\ShadowSlain"), 2, destination + new Vector3(0, 0, -6300));
                Stage.CreateNPCUnit(Content.Load<UnitType>(@"UnitTypes\FlameDestroyer"), 2, destination + new Vector3(0, 0, -4200));
                Stage.CreateNPCUnit(Content.Load<UnitType>(@"UnitTypes\GhostMist"), 2, destination + new Vector3(0, 0, -10000));
                b1 = true;
                /*
                Stage.AddGameMessage(@"Rudolf the Commander: 又来了！还有比这更糟的吗？", Color.CornflowerBlue, 3);
                Stage.AddGameMessage(@"Jeffsion the Warrior: 朋友们，放轻松……秩序之眼的另一支小队……", Color.CornflowerBlue, 4);
                Stage.AddGameMessage(@"Bill: 第三舰队，准备战斗！", Color.CornflowerBlue, 4);
                Stage.AddGameMessage(@"鬼雾：凡人，是向秩序之眼效忠，还是接受可悲的死亡命运？", Color.Red, 4);
                Stage.AddGameMessage(@"Zero: 开始摇滚吧！", Color.LightGreen, 3);
                 */
                Stage.AddGameMessage(@"Rudolf the Commander: They come again!", Color.CornflowerBlue, 3);
                Stage.AddGameMessage(@"Jeffsion the Warrior: Another gang of the Eye of Cosmos.", Color.CornflowerBlue, 4);
                Stage.AddGameMessage(@"Bill: Prepare yourselves!", Color.CornflowerBlue, 4);
                Stage.AddGameMessage(@"Ghostmist: Eliminate the mortal.", Color.Red, 4);
                Stage.AddGameMessage(@"Zero: Let's rock.", Color.LightGreen, 3);
            }
            base.Event_TimerRing(timer);
        }
    }
}
