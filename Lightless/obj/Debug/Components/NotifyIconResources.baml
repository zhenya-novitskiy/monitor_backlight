   M S B A M L   `   `   `  ����D  @Lightless, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null5clr-namespace:Windowless_SampleWindowless_Sample  5      W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089\ XHardcodet.Wpf.TaskbarNotification, Version=1.0.5.0, Culture=neutral, PublicKeyToken=null   SysTrayMenu  m� Command3   -Hardcodet.Wpf.TaskbarNotification.TaskbarIcon   Name  
NotifyIcon    ToolTipText � ContextMenu    
IconSource    DoubleClickCommand    LeftClickCommand � DataContext+   %Windowless_Sample.NotifyIconViewModel�� 5      D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    :x,http://schemas.microsoft.com/winfx/2006/xaml     5      )tb http://www.hardcodet.net/taskbar  5      )localclr-namespace:Windowless_Sample  5      %n  &	       &	 M    7   �� 5      .@�5   
   m� $��Black��6     6"   �� *BlackCommand+�� 5   
   m� 5   
   $��Show Window��6     6(   �� *ShowWindowCommand+m� 5   
   $��Hide Window��6     6(   �� *HideWindowCommand+�� 5   
   m� 5   
   $
��Exit��6     6!   �� *ExitApplicationCommand+5         5      -   5        $ NotifyTaskbarIcon��5      $3 -Double-click for window, right-click for menu��5      8    5      �� *Icon+ 5      �� *ShowWindowCommand+ 5      �� *BlackCommand+ 5    
     5!      5#      5%      