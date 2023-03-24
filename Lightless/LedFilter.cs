using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lightless.Components;

namespace Lightless
{
    public static class LedFilter
    {
        public static bool Left1(PixelModel pixel)
        {
            return pixel.Index >= 16 && pixel.Index <29;
        }

        public static bool Left1WithCorners(PixelModel pixel)
        {
            return pixel.Index >= 13 && pixel.Index < 32;
        }

        public static bool Top1(PixelModel pixel)
        {
            return pixel.Index >= 32 && pixel.Index < 57;
        }

        public static bool Right1(PixelModel pixel)
        {
            return pixel.Index >= 60 && pixel.Index < 73;
        }
        public static bool Bottom1(PixelModel pixel)
        {
            return (pixel.Index >= 76 && pixel.Index < 88) || (pixel.Index >= 0 && pixel.Index < 13);
        }

        public static bool Left2(PixelModel pixel)
        {
            return pixel.Index >= 104 && pixel.Index < 117;
        }

        public static bool Top2(PixelModel pixel)
        {
            return pixel.Index >= 120 && pixel.Index < 145;
        }

        public static bool Right2(PixelModel pixel)
        {
            return pixel.Index >= 148 && pixel.Index < 161;
        }

        public static bool Right2WithCorners(PixelModel pixel)
        {
            return pixel.Index >= 145 && pixel.Index < 164;
        }
        public static bool Bottom2(PixelModel pixel)
        {
            return (pixel.Index >= 164 && pixel.Index < 176) || (pixel.Index >= 88 && pixel.Index < 101);
        }
    }
}
