using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Lightless.CustomEventArgs;

namespace Lightless
{
    /// <summary>
    /// Interaction logic for ColorPicker.xaml
    /// </summary>
    public partial class ColorPicker : UserControl
    {
        public event EventHandler<ColorChangeEventArgs> ColorChanged;

        public ColorPicker()
        {
            InitializeComponent();
        }

        private void Overlay_MouseMove(object sender, MouseEventArgs e)
        {
            if (Mouse.LeftButton == MouseButtonState.Pressed)
            {
                ChangeColor();
            }
        }

        private void ChangeColor()
        {
            var imageX = (int)Mouse.GetPosition(Overlay).X;
            var imageY = (int)Mouse.GetPosition(Overlay).Y;
            if ((imageX < 0) || (imageY < 0) || (imageX > ColorPickerBackground.Width - 1) || (imageY > ColorPickerBackground.Height - 1)) return;

            CroppedBitmap cb = new CroppedBitmap(ColorPickerBackground.Source as BitmapSource, new Int32Rect(imageX, imageY, 1, 1));
            byte[] pixels = new byte[4];
            cb.CopyPixels(pixels, 4, 0);

            ellipsePixel.SetValue(Canvas.LeftProperty, (double)(Mouse.GetPosition(Overlay).X - (ellipsePixel.Width / 2.0)));
            ellipsePixel.SetValue(Canvas.TopProperty, (double)(Mouse.GetPosition(Overlay).Y - (ellipsePixel.Width / 2.0)));
            Overlay.InvalidateVisual();

            var color = Color.FromArgb((byte)255, pixels[2], pixels[1], pixels[0]);

            ColorChanged?.Invoke(this, new ColorChangeEventArgs { Color = color });
        }

        private void Overlay_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ChangeColor();
        }
    }
}
