   M S B A M L   `   `   `  ����D  @Lightless, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null;"clr-namespace:Lightless.ComponentsLightless.Components  5          Lightless.ThemeItem   5      W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      4local"clr-namespace:Lightless.Components     5      $	��280��5   "     � 	Resources  5	         	PlayTrack �� 
TargetName �� TargetProperty	 �� To �� From g� Duration  	StopTrack��%"  &	        &	 �    �� 5   
   .��5      �� $ 	ThemeName��6   $ Foreground.Color�6A   $ #0f9d9cX�6o   $ WhiteX�6|   $ 0:0:0.5D�6�   6�   �� 5      $ Number��6   $ Foreground.Color�6>   $ #0f9d9cX�6l   $ #a8a8a8X�6y   $ 0:0:0.5D�6�   6�   �� 5      $
 Time��6   $ Foreground.Color�6<   $ #0f9d9cX�6j   $ #a8a8a8X�6w   $ 0:0:0.5D�6�   6�   5      �� 5   
   .��5      �� $ 	ThemeName��6   $ Foreground.Color�6@   $ WhiteX�6m   $ #0f9d9cX�6x   $ 0:0:0.5D�6�   6�   �� 5      $ Number��6   $ Foreground.Color�6=   $ #a8a8a8X�6j   $ #0f9d9cX�6w   $ 0:0:0.5D�6�   6�   �� 5      $
 Time��6   $ Foreground.Color�6;   $ #a8a8a8X�6h   $ #0f9d9cX�6u   $ 0:0:0.5D�6�   6�   5      5      .��5      � $��23��6   
������ .+�5   
   � -   5         �Name$	 Top��5      $��	-10,0,0,0q�6   $	��800��6/   
������ $y�Hidden=�6T   $	��Top=�6h   $��23��6�   $
��Left=�6�   .+�5      �  J� OpacityMask 5      �� 5       �� 
StartPoint$	 0,0&�6*   	 �� EndPoint$		 0,1&�6;   ��5      � 
 � Offset$
 0Q�6+    � Color$ Black��67   6G   � 5       $
 1Q�6+   $ Transparent��66   6M   5#      .+�5%      �� $��1q�6   
�������$	��800��6B   $��30��6N    �� CornerRadius$ 8��6[    5&      �� 5'      $	 0,0&�6.   $		 1,0&�6?   ��5)   "   � $
 0Q�6/   $ Black��6;   6K   � 5*   "   $	
 0.2Q�6/   $ Transparent��6<   6S   5-       J� Effect 5/      3  -System.Windows.Media.Effects.DropShadowEffect  50         
BlurRadius$ 1Q�6+      	Direction$	 -90Q�6:      ShadowDepth$	 0.5Q�6J   6^   52      54      56      � 57   
   -   5        $ Middle��57      $��	-10,0,0,0q�6   $��27��62   
������ $y�	Collapsed=�6V   $	��Top=�6m   $��23��6�   $
��Left=�6�   59      � 5:   
   -   5        $ Bottom��5:      $��	-10,0,0,0q�6    $	��800��63   
������ $y�	Collapsed=�6Y   $	��Top=�6p   $��23��6�   $
��Left=�6�    5;      �� 5<      $	 0,1&�6&   $		 0,0&�67   ��5>      � $
 0Q�6'   $ Black��63   6C   � 5?      $
 1Q�6'   $ Transparent��62   6I   5A      .+�5C      �� $��1q�6   
�������$	��800��6>   $��Bottom=�6J   $��30��6e   $ 8��6r    5D      �� 5E      $	 0,0&�6*   $		 1,0&�6;   ��5G      � $
 0Q�6+   $ Black��67   6G   � 5H      $	
 0.2Q�6+   $ Transparent��68   6O   5K      5M      5N      �� 5Q   
    �� Orientation$ 
Horizontal=�6   
������ .+�5R      �� -   5         ��Name$ Number��5R   {   $��Center=�6   $��Century Gothic)�63   ���#a8a8a8$��Regular"�6f   $��00��6�   $��14(�6�   $��CharacterEllipsis=�6�   6�   �� 5S      -   5        $ 	ThemeName��5S   �   $��Center=�6    �� Padding$ 5,0,0,0q�63   $��Century Gothic)�6G   
�������$��5,0,0,0q�6w   $��Regular"�6�   $��
Theme name��6�   $��14(�6�   $	��209��6�   $��CharacterEllipsis=�6�   6�   �� 5T      -   5        $
 Time��5T   �   $��Center=�6   $ 5,0,0,0q�63   $��Century Gothic)�6E    �� TextAlignment$ Right=�6a   $��00:00��6w   ���#a8a8a8$��10,0,0,0q�6�   $��Regular"�6�   $��14(�6�   $��40��6�   6�   5U      5V      5W      