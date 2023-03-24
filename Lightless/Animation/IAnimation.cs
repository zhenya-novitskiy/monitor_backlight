using System.Collections.Generic;

namespace Lightless.Animation
{
    public interface IAnimation
    {
        List<Pixel> GetNextStep();
    }
}
