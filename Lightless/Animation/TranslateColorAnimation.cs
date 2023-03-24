using System.Collections.Generic;
using System.Linq;

namespace Lightless.Animation
{
    public class TranslateColorAnimation : IAnimation
    {
        private List<TranslateColorContainer> _translateColorContainer;
        public const double Steps  = 30;
        private double _currentStep = Steps;

        public TranslateColorAnimation(List<Pixel> initialColors, List<Pixel> target)
        {
            _translateColorContainer = new List<TranslateColorContainer>();
            foreach (var initialColor in initialColors)
            {
                var targetColor = target.FirstOrDefault(x => x.Position == initialColor.Position);
                var delta = new TransactionDelta
                {
                    R = (targetColor.R - initialColor.R)/Steps,
                    G = (targetColor.G - initialColor.G)/Steps,
                    B = (targetColor.B - initialColor.B)/Steps
                };
                var container = new TranslateColorContainer {CurrentColor = initialColor, TransactionDelta = delta};
                _translateColorContainer.Add(container);
            }
            
        }
        public List<Pixel> GetNextStep()
        {
            if (_currentStep == 0)
            {
                return null;
            }

            var result = _translateColorContainer.Select(translateColor =>
            {
                var pixel = new Pixel {Position = translateColor.CurrentColor.Position};
                translateColor.SubstructDelta();
                pixel.R = translateColor.CurrentColor.R;
                pixel.G = translateColor.CurrentColor.G;
                pixel.B = translateColor.CurrentColor.B;

                return pixel;
            }).ToList();

            _currentStep--;
            return result;
        }
    }
}