   M S B A M L   `   `   `  ����D  @Lightless, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null%clr-namespace:Lightless	Lightless  5      %clr-namespace:Lightless	Lightless  5      ;"clr-namespace:Lightless.ComponentsLightless.Components  5          Lightless.LEDSurface   5      W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      )localclr-namespace:Lightless     5      (Coreclr-namespace:Lightless     5      9
components"clr-namespace:Lightless.Components     5      $
��1080��5
      $
��1920��6     � 	Resources  5       �� MergedDictionaries �� Source�� 5   
    5      �� 5      $ ../Styles/LedSurface.xaml?�5      5      %    5      .��5      � -   5         �Name$ theGrid��5   	   
������ .+�5   
   �� -   5         ��Name$ Surface��5      .+�5      �� -   5         ��Name$ ItemsContainer��5      $
��1920��6.   $
��1080��6;   
������  �� SelectionMode$ Multiple=�6�      LedSurfaceItemStyle#���   6�   ��6I   ��  �� Path$
 Leds���5      �� 5      .R�5       �� 
������ 6!   5!      ��5#      �� 5$       �� DataType$ components:PixelModel��6#   .R�5%      � .+�5&      A� 	 A�Name$	 EllipseComponent��6=   $��13��6&   $��13��61   $��Center=�6X   $��Center=�6u   ��6�   �� *	PixelData+  ColorToBrushConverter
 �� 	Converter#
 [ �� 5'      $��../Images/led.png��6$   $��Center=�6?   $��15��6\   $��15��6g   $��Center=�6s   6�   A� 5(      $��15��6&   $��15��61   $��Center=�6>   $��Center=�6[   
���   �$��1��6�   y�6�   �� *
Selected+  	BoolToVis#
 [  �� FallbackValue$ Hidden��A� 5)      $��17��6&   $��17��61   $��Center=�6>   $��Center=�6[   
�������$��1��6�   y�6�   �� *
Selected+#
 [ $ Hidden��5*      5+      5-      5.      �� 5/   
   .+�50      �� -   5         ��Name$ selectionBox��51      $y�	Collapsed=�52      ���	#333399FF���#3399FF$��1��55      57      58      59      