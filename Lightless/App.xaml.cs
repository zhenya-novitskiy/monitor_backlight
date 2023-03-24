using System.Drawing;
using System.Windows;
using Hardcodet.Wpf.TaskbarNotification;
using Lightless.Components;
using Lightless.CustomEventArgs;
using Windowless_Sample;

namespace Lightless
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private TaskbarIcon notifyIcon;

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            notifyIcon = (TaskbarIcon)FindResource("NotifyIcon");
            notifyIcon.Icon = new Icon("Images/on.ico");
            var aaa = (NotifyIconViewModel) notifyIcon.DataContext;
            aaa.OnOff += Aaa_OnOff;
        }

        private void Aaa_OnOff(object sender, BoolEventArgs e)
        {
            if (e.Value)
            {
                notifyIcon.Icon = new Icon("Images/on.ico");
            }
            else
            {
                notifyIcon.Icon = new Icon("Images/off.ico");
            }
            
        }

        protected override void OnExit(ExitEventArgs e)
        {
            notifyIcon.Dispose(); 
            AnimationManager.TournOff();
            base.OnExit(e);
        }
    }
}
