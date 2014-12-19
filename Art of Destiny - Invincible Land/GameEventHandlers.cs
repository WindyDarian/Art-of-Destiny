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
using AODGameLibrary;
using AODGameLibrary.Gamehelpers;
using AODGameLibrary.Cameras;
using AODGameLibrary.Units;
using AODGameLibrary.Weapons;
using AODGameLibrary.Effects;
using AODGameLibrary.Models;
using AODGameLibrary.Interface;
using AODGameLibrary.AODObjects;
using AODGameLibrary.GamePlay;
using AODGameLibrary.Menu;
using Stages;

namespace AOD
{
    public delegate void StartGameHandler(Stage stage,int? part);
}
