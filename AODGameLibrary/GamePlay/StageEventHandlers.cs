using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AODGameLibrary.GamePlay
{
    /// <summary>
    /// 2010/7/9
    /// </summary>
    public static class StageEventHandlers
    {
        public delegate void StagePartChangeHandler(int targetPart, int formerPart);
    }
}
