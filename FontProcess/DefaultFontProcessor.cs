using System.IO;
using Microsoft.Xna.Framework.Content.Pipeline;
using Microsoft.Xna.Framework.Content.Pipeline.Graphics;
using Microsoft.Xna.Framework.Content.Pipeline.Processors;


namespace FontProcessors
{
    [ContentProcessor]
    public class DefaultFontProcessor : FontDescriptionProcessor
    {
        public override SpriteFontContent Process(FontDescription input, ContentProcessorContext context)
        {
            //载入文件
            string fullPath = Path.GetFullPath("GameCharacters.txt");
            context.AddDependency(fullPath);
            string letters = File.ReadAllText(fullPath, System.Text.Encoding.UTF8);


            //导入字符
            foreach (char c in letters)
            {
                input.Characters.Add(c);
            }
            return base.Process(input, context);
        }
    }
}