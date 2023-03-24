using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Lightless.Arduino;
using Lightless.Components;
using Lightless.Infrastructure;

namespace Lightless
{
    /// <summary>
    /// Interaction logic for LEDSurface.xaml
    /// </summary>
    public partial class LEDSurface 
    {
        public LedCollection<PixelModel> Leds { get; set; }

        public LEDSurface()
        {
            InitializeComponent();
            Leds = new LedCollection<PixelModel>();
            

            var x = 420;
            var y = 500;
            var distance = 20;
            int index = -1;


            index = DrawLedsForMonitor(index, x, y, distance);
            x += 750;
            DrawLedsForMonitor(index, x, y, distance);

            ItemsContainer.ItemsSource = Leds;
            Leds.CollectionUpdated += Leds_CollectionUpdated;
        }

        private void Leds_CollectionUpdated(object sender, EventArgs e)
        {
            ArduinoCommunication.SendData(new ArduinoColorData(1, Leds.Select(x => x.PixelData), SendPriority.Higth));
        }

        bool mouseDown = false; // Set to 'true' when mouse is held down.
        Point mouseDownPos; // The point where the mouse button was clicked down.

        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            // Capture and track the mouse.
            mouseDown = true;
            mouseDownPos = e.GetPosition(theGrid);
            theGrid.CaptureMouse();

            // Initial placement of the drag selection box.         
            Canvas.SetLeft(selectionBox, mouseDownPos.X);
            Canvas.SetTop(selectionBox, mouseDownPos.Y);
            selectionBox.Width = 0;
            selectionBox.Height = 0;

            // Make the drag selection box visible.
            selectionBox.Visibility = Visibility.Visible;
        }

        private void Grid_MouseUp(object sender, MouseButtonEventArgs e)
        {
            // Release the mouse capture and stop tracking it.
            mouseDown = false;
            theGrid.ReleaseMouseCapture();

            // Hide the drag selection box.
            selectionBox.Visibility = Visibility.Collapsed;

            Point mouseUpPos = e.GetPosition(theGrid);

            // TODO: 
            //
            // The mouse has been released, check to see if any of the items 
            // in the other canvas are contained within mouseDownPos and 
            // mouseUpPos, for any that are, select them!
            //
        }

        private void Grid_MouseMove(object sender, MouseEventArgs e)
        {
            if (mouseDown)
            {
                // When the mouse is held down, reposition the drag selection box.

                Point mousePos = e.GetPosition(theGrid);

                if (mouseDownPos.X < mousePos.X)
                {
                    Canvas.SetLeft(selectionBox, mouseDownPos.X);
                    selectionBox.Width = mousePos.X - mouseDownPos.X;
                }
                else
                {
                    Canvas.SetLeft(selectionBox, mousePos.X);
                    selectionBox.Width = mouseDownPos.X - mousePos.X;
                }

                if (mouseDownPos.Y < mousePos.Y)
                {
                    Canvas.SetTop(selectionBox, mouseDownPos.Y);
                    selectionBox.Height = mousePos.Y - mouseDownPos.Y;
                }
                else
                {
                    Canvas.SetTop(selectionBox, mousePos.Y);
                    selectionBox.Height = mouseDownPos.Y - mousePos.Y;
                }

                var selectionBoxX1 = Canvas.GetLeft(selectionBox);
                var selectionBoxX2 = selectionBoxX1 + selectionBox.Width;
                var selectionBoxY1 = Canvas.GetTop(selectionBox);
                var selectionBoxY2 = selectionBoxY1 + selectionBox.Height;
                var x1 = Math.Min(selectionBoxX1, selectionBoxX2);
                var x2 = Math.Max(selectionBoxX1, selectionBoxX2);
                var y1 = Math.Min(selectionBoxY1, selectionBoxY2);
                var y2 = Math.Max(selectionBoxY1, selectionBoxY2);

                foreach (var led in Leds)
                {
                    if (x1 <= led.XPos + 10 && x2 >= led.XPos + 10
                        && y1 <= led.YPos + 20 && y2 >= led.YPos + 20 )
                    {
                        led.Selected = true;
                    }
                    else
                    {
                        led.Selected = false;
                    }

                }
            }
        }
        private int DrawLedsForMonitor(int index, int offsetX, int offsetY, int distance)
        {

            for (var i = 0; i < 13; i++)
            {
                index ++;
                var led = new PixelModel
                {
                    Orientation = 0,
                    Index = index,
                    XPos = offsetX - i*distance,
                    YPos = offsetY + distance,
                    PixelData = new Pixel {Position = index}
                };
                Leds.Add(led);
            }

            for (int i = 0; i < 3; i++)
            {
                index++;
                var led = new PixelModel
                {
                    Orientation = 45,
                    Index = index,
                    XPos = offsetX - distance * 13 - i * distance,
                    YPos = offsetY - i * distance,
                    PixelData = new Pixel { Position = index }
                };
                Leds.Add(led);
            }

            for (int i = 0; i < 13; i++)
            {
                index++;
                var led = new PixelModel
                {
                    Orientation = 90,
                    Index = index,
                    XPos = offsetX - distance * 16,
                    YPos = offsetY - distance * 3 - i * distance,
                    PixelData = new Pixel { Position = index }
                };
                Leds.Add(led);
            }

            for (int i = 0; i < 3; i++)
            {
                index++;
                var led = new PixelModel
                {
                    Orientation = 135,
                    Index = index,
                    XPos = offsetX - distance * 15 + i * distance,
                    YPos = offsetY - distance * 16 - distance * i,
                    PixelData = new Pixel { Position = index }
                };
                Leds.Add(led);
            }

            for (int i = 0; i < 25; i++)
            {
                index++;
                var led = new PixelModel
                {
                    Orientation = 180,
                    Index = index,
                    XPos = offsetX - distance * 12 + distance * i,
                    YPos = offsetY - distance * 19,
                    PixelData = new Pixel { Position = index }
                };
                Leds.Add(led);
            }

            for (int i = 0; i < 3; i++)
            {
                index++;
                var led = new PixelModel
                {
                    Orientation = 225,
                    Index = index,
                    XPos = offsetX + distance * 13 + distance * i,
                    YPos = offsetY - distance * 18 + distance * i,
                    PixelData = new Pixel { Position = index }
                };
                Leds.Add(led);
            }

            for (int i = 0; i < 13; i++)
            {
                index++;
                var led = new PixelModel
                {
                    Orientation = 270,
                    Index = index,
                    XPos = offsetX + distance * 16,
                    YPos = offsetY - distance * 15 + distance * i,
                    PixelData = new Pixel { Position = index }
                };
                Leds.Add(led);
            }

            for (int i = 0; i < 3; i++)
            {
                index++;
                var led = new PixelModel
                {
                    Orientation = 315,
                    Index = index,
                    XPos = offsetX + distance * 15 - distance * i,
                    YPos = offsetY - distance * 2 + distance * i,
                    PixelData = new Pixel { Position = index }
                };
                Leds.Add(led);
            }

            for (int i = 0; i < 12; i++)
            {
                index++;
                var led = new PixelModel
                {
                    Orientation = 0,
                    Index = index,
                    XPos = offsetX + distance * 12 - distance * i,
                    YPos = offsetY + distance,
                    PixelData = new Pixel { Position = index }
                };
                Leds.Add(led);
            }

            return index;
        }
    }
}
