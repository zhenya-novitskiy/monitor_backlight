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

namespace Lightless
{
    /// <summary>
    /// Interaction logic for ThemeItem.xaml
    /// </summary>
    public partial class ThemeItem : UserControl
    {
        public ThemeItem()
        {
            InitializeComponent();
        }

        public ThemeItem(int number, string name)
        {
            InitializeComponent();

            ThemeName.Foreground = new SolidColorBrush(Color.FromArgb(255, 255, 255, 255));
            ThemeName.Text = name;

            this.Number.Text = String.Format("{0:00}.", number);


            
            Middle.Visibility = Visibility.Visible;
            
        }

        public enum PositionInGroup
        {

            Top = 1,
            Middle = 2,
            Bottom = 3,
            NotInGroup = 4
        }
    }
}
