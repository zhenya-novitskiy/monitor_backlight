using System;
using System.Drawing;
using System.Windows;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Hardcodet.Wpf.TaskbarNotification;
using Lightless;
using Lightless.Components;
using Lightless.CustomEventArgs;

namespace Windowless_Sample
{
    /// <summary>
    /// Provides bindable properties and commands for the NotifyIcon. In this sample, the
    /// view model is assigned to the NotifyIcon in XAML. Alternatively, the startup routing
    /// in App.xaml.cs could have created this view model, and assigned it to the NotifyIcon.
    /// </summary>
    public class NotifyIconViewModel
    {

        public event EventHandler<BoolEventArgs> OnOff;


        private ImageSource _icon;

        /// <summary>
        /// Shows a window, if none is already open.
        /// </summary>
        /// 

        public NotifyIconViewModel()
        {
           // var uri = new Uri("", UriKind.Relative);

        //    var icon = new Icon("Images/on.ico");
        //   Icon = Imaging.CreateBitmapSourceFromHIcon(
        //icon.Handle,
        //new Int32Rect(0, 0, icon.Width, icon.Height),
        //BitmapSizeOptions.FromEmptyOptions());

        }

        public ICommand ShowWindowCommand
        {
            get
            {
                return new DelegateCommand
                {
                    CanExecuteFunc = () => !Application.Current.MainWindow.IsVisible,
                    CommandAction = () =>
                    {
                        Application.Current.MainWindow.Show();
                    },
                    
                };
            }
        }

        /// <summary>
        /// Hides the main window. This command is only enabled if a window is open.
        /// </summary>
        public ICommand HideWindowCommand
        {
            get
            {
                return new DelegateCommand
                {
                    CommandAction = () => Application.Current.MainWindow.Hide(),
                    CanExecuteFunc = () => Application.Current.MainWindow.IsVisible
                };
            }
        }


        /// <summary>
        /// Shuts down the application.
        /// </summary>
        public ICommand ExitApplicationCommand
        {
            get
            {
                return new DelegateCommand { CommandAction = () => Application.Current.Shutdown() };
            }
        }

        public ICommand BlackCommand
        {
            get
            {
                return new DelegateCommand
                {
                    CommandAction = () =>
                    {
                        if (AnimationManager.IsOff)
                        {
                            AnimationManager.Restore();
                            OnOff?.Invoke(this, new BoolEventArgs { Value = true});
                        }
                        else
                        {
                            AnimationManager.TournOff();
                            OnOff?.Invoke(this, new BoolEventArgs { Value = false});
                        }
                    },
                    CanExecuteFunc = () => true
                };
            }
        }

        public ImageSource Icon
        {
            get { return _icon; }
            set
            {
                _icon = value;
            }
        }
    }


    /// <summary>
    /// Simplistic delegate command for the demo.
    /// </summary>
    public class DelegateCommand : ICommand
    {
        public Action CommandAction { get; set; }
        public Func<bool> CanExecuteFunc { get; set; }

        public void Execute(object parameter)
        {
            CommandAction();
        }

        public bool CanExecute(object parameter)
        {
            return CanExecuteFunc == null || CanExecuteFunc();
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }
    }
}