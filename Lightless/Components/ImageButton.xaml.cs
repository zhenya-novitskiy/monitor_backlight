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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Lightless.Components
{
    /// <summary>
    /// Interaction logic for ImageButton.xaml
    /// </summary>
    public partial class ImageButton : UserControl
    {
        private string SelectedPath = "";
        private string notSelectedPath = "";
        private string dirPath = "";

        public ImageButton()
        {
            InitializeComponent();
        }
        public void Init(string selectedPath, string notSelectedPath)
        {
            this.SelectedPath = selectedPath;
            this.notSelectedPath = notSelectedPath;
            this.dirPath = dirPath;
            buttonImage1.Source = new BitmapImage(new Uri(notSelectedPath, UriKind.Relative));
            buttonImage2.Source = new BitmapImage(new Uri(selectedPath, UriKind.Relative));
        }

        private void buttonImage_MouseEnter(object sender, MouseEventArgs e)
        {
            var anim = (Storyboard)FindResource("MouseEnter");

            anim.Begin();
        }

        private void buttonImage_MouseLeave(object sender, MouseEventArgs e)
        {
            var anim = (Storyboard)FindResource("MouseLeave");

            anim.Begin();
        }


    }
}
