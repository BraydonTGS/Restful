using Restful.Global.Constant;
using System.Collections.Generic;

namespace Restful.WPF.Theme
{
    public static class ThemeAccents
    {
        public static string DefaultAccent { get; set; } = Constants.ColorRed;
        public static List<Accent> Accents { get; } = new List<Accent>
        {
               new Accent(Constants.ColorRed),
               new Accent(Constants.ColorGreen),
               new Accent(Constants.ColorBlue),
               new Accent(Constants.ColorPurple),
               new Accent(Constants.ColorOrange),
               new Accent(Constants.ColorLime),
               new Accent(Constants.ColorEmerald),
               new Accent(Constants.ColorTeal),
               new Accent(Constants.ColorCyan),
               new Accent(Constants.ColorCobalt),
               new Accent(Constants.ColorIndigo),
               new Accent(Constants.ColorViolet),
               new Accent(Constants.ColorPink)
        };
    }

    public class Accent
    {
        public string Name { get; set; }
        public Accent(string name)
        {
            Name = name;
        }
    }
}
