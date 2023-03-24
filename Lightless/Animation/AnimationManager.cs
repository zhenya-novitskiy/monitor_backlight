using System.Collections.Generic;
using System.Linq;
using System.Timers;
using Lightless.Animation;
using Lightless.Arduino;
using Lightless.Infrastructure;

namespace Lightless.Components
{
    public static class AnimationManager
    {
        private static Timer _timer;

        public static LedCollection<PixelModel> PixelModelCollection;

        public static List<PixelModel> TempPixelModelCollection;

        private static IAnimation _currentAnimation;

        public static bool IsOff;
        static AnimationManager()
        {
            //_pixelModelCollection = pixelModel;
            _timer = new Timer { Interval = 20, AutoReset = true };
            _timer.Elapsed += _timer_Elapsed;
            _timer.Start();
        }

        static int i = 1;
        private static void _timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            var resultCollection = _currentAnimation?.GetNextStep();
            if (resultCollection != null)
            {

                PixelModelCollection.Update(() =>
                {
                    foreach (var pixelModel in PixelModelCollection)
                    {
                        var updatedPixel = resultCollection.FirstOrDefault(x => x.Position == pixelModel.Index);
                        pixelModel.PixelData.R = updatedPixel.R;
                        pixelModel.PixelData.G = updatedPixel.G;
                        pixelModel.PixelData.B = updatedPixel.B;
                        pixelModel.PixelDataChanged();
                    }
                });
                i++;
            }
            else
            {
                _currentAnimation = null;
            }
        }

        public static void TranslateToColor(List<Pixel> translateToColors)
        {
            IsOff = false;
            _currentAnimation = new TranslateColorAnimation(PixelModelCollection.Select(x => x.PixelData).ToList(), translateToColors);
        }

        public static void TranslateNow(List<Pixel> translateToColors)
        {
            IsOff = false;

            PixelModelCollection.Update(() =>
            {
                foreach (var pixelModel in PixelModelCollection)
                {
                    var updatedPixel = translateToColors.FirstOrDefault(x => x.Position == pixelModel.Index);
                    pixelModel.PixelData.R = updatedPixel.R;
                    pixelModel.PixelData.G = updatedPixel.G;
                    pixelModel.PixelData.B = updatedPixel.B;
                    pixelModel.PixelDataChanged();
                }
            });
        }

        public static void TournOff()
        {
            IsOff = true;
            var data = new List<Pixel>();
            for (int j = 0; j < 176; j++)
            {
                data.Add(new Pixel(j, 0, 0, 0));
            }

            ArduinoCommunication.SendData(new ArduinoColorData(1, data, SendPriority.Higth));
        }

        public static void Restore()
        {
            IsOff = false;
            
            PixelModelCollection.Update(() =>
            {
                foreach (var pixelModel in PixelModelCollection)
                {
                    var updatedPixel = PixelModelCollection.FirstOrDefault(x => x.Index == pixelModel.Index);
                    pixelModel.PixelData.R = updatedPixel.PixelData.R;
                    pixelModel.PixelData.G = updatedPixel.PixelData.G;
                    pixelModel.PixelData.B = updatedPixel.PixelData.B;
                    pixelModel.PixelDataChanged();
                }
            });
        }
    }
}
