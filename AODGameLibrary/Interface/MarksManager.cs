using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Net;
using Microsoft.Xna.Framework.Storage;
using AODGameLibrary.Gamehelpers;
using AODGameLibrary.Units;
using AODGameLibrary.Weapons;
using AODGameLibrary.Effects;
using AODGameLibrary;
using System.IO;
using AODGameLibrary.GamePlay;

namespace AODGameLibrary.Interface
{
    /// <summary>
    /// 管理游戏中的标记,由大地无敌-范若余于2009年11月7日建立
    /// </summary>
    public class MarksManager:UI 
    {
        GameWorld gameWorld;
        List<Mark> marks = new List<Mark>(25);
        List<Mark> ScreenUnitMarks = new List<Mark>(20);
        Mark targetMark;
        public MarksManager(GameWorld gameWorld)
        {
            this.gameWorld = gameWorld;
            for (int i = 0; i < 20; i++)
            {
                Mark m = new Mark(gameWorld);
                m.BackOnly = true;
                ScreenUnitMarks.Add(m);
                marks.Add(m);
            }
            targetMark = new Mark(gameWorld);
            targetMark.BackOnly = false;
            targetMark.MarkColor = Color.Red;
            marks.Add(targetMark);
        }
        public override void Update(GameTime gameTime)
        {
            List<Mark> dm = new List<Mark>(10);
            foreach (Mark m in marks)
            {
                m.Update(gameTime);
                if (m.Enabled == false)
                {
                    dm.Add(m);
                }
            }
            foreach (Mark m in dm)
            {
                marks.Remove(m);
            }
        }
        public override void Draw(GameTime gameTime)
        {
            int i = 0;
            foreach (Unit u in gameWorld.units)
            {
                if (i < 20)
                {

                    if (u != gameWorld.Variables.Player && u!= gameWorld.PlayerLockedTarget)
                    {
                        ScreenUnitMarks[i].TargetUnit = u;
                        if (u.Group != gameWorld.Variables.Player.Group)
                        {
                            ScreenUnitMarks[i].MarkColor = Color.Red;
                        }
                        else ScreenUnitMarks[i].MarkColor = Color.Blue;
                        ScreenUnitMarks[i].Visable = true;
                        i++;
                    }
                }
            }
            for (; i < 20; i++)
            {

                ScreenUnitMarks[i].TargetUnit = null;
                ScreenUnitMarks[i].Visable = false;
            }
            if (gameWorld.PlayerLockedTarget != null)
            {
                targetMark.TargetUnit = gameWorld.PlayerLockedTarget;
                targetMark.Visable = true;
            }
            else
            {
                targetMark.TargetUnit = null;
                targetMark.Visable = false;
            }
            foreach (Mark m in marks)
            {
                if (m.Visable)
                {

                    m.Draw(gameTime);
                }
            }
        }
        public Mark AddPositionMark(Vector3 position)
        {
            Mark m = new Mark(gameWorld, this);
            m.TargetPosition = position;
            marks.Add(m);
            return m;
        }
    }

}
