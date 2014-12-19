using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Net;
using Microsoft.Xna.Framework.Storage;
namespace 关卡生成器
{
    public partial class DecorationProducer : Form
    {
       
        public DecorationProducer()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Vector3 cen = Vector3.Zero;
            int num = 0;
            string name = "";
            float rad = 0;
            bool ok = false;
            List<Vector3> positions = new List<Vector3>();
            try
            {
                cen = new Vector3(Convert.ToSingle(centerX.Text), Convert.ToSingle(centerY.Text), Convert.ToSingle(centerZ.Text));
                num = Convert.ToInt32(number.Value);
                name = assetName.Text;
                rad = Convert.ToSingle(radius.Text);
                ok = true;
                for (int i = 0; i < num; i++)
                {
                    positions.Add(GameHelpers.GameHelper.RandomPointInBall(cen, rad));
                }
            }
            catch 
            {
                throw;
                
            }

            if (ok)
            {
               FileStream fs = new FileStream("export.txt",FileMode.Create);
               StreamWriter sw = new StreamWriter(fs, Encoding.UTF8);
               
              
                    foreach (Vector3 v in positions)
                    {
                        sw.WriteLine(@"<Item>");
                        sw.WriteLine(@"    <DecorationType>" + name + "</DecorationType>");
                        sw.WriteLine(@"    <Position>" + v.X.ToString() + " " + v.Y.ToString() + " " + v.Z.ToString() + "</Position>");
                        sw.WriteLine(@"    <RandomScaleAndRotation>" + "true" + "</RandomScaleAndRotation>");
                        sw.WriteLine(@"    <Scale>" + "1" + "</Scale>");
                        sw.WriteLine(@"    <Rotation>" + "0 0 0" + "</Rotation>");
                        sw.WriteLine(@"</Item>");

                      
                    }
                    sw.Flush();

                    if (fs!= null)
                    {
                        
                        fs.Close();
                    }

            }

        }
    }
}
