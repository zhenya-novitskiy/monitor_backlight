using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Media;
using Lightless.Annotations;

namespace Lightless.Components
{
    public class PixelModel : INotifyPropertyChanged
    {
        private int _index;
        private Pixel _pixelData;
        private int _xPos;
        private int _yPos;
        private int _orientation;
        private bool _selected;

        public void PixelDataChanged()
        {
            OnPropertyChanged(nameof(PixelData));
        }

        public int Index
        {
            get { return _index; }
            set
            {
                _index = value;
                OnPropertyChanged(nameof(Index));
            }
        }

        public Pixel PixelData
        {
            get { return _pixelData; }
            set
            {
                _pixelData = value;
                OnPropertyChanged(nameof(PixelData));
            }
        }

        public int XPos
        {
            get { return _xPos; }
            set
            {
                _xPos = value;
                OnPropertyChanged(nameof(XPos));
            }
        }

        public int YPos
        {
            get { return _yPos; }
            set
            {
                _yPos = value;
                OnPropertyChanged(nameof(YPos));
            }
        }

        public int Orientation
        {
            get { return _orientation; }
            set
            {
                _orientation = value;
                OnPropertyChanged(nameof(Orientation));
            }
        }

        public bool Selected
        {
            get { return _selected; }
            set
            {
                _selected = value;
                OnPropertyChanged(nameof(Selected));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
