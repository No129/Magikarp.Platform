   M S B A M L   `   `   `  ����U  QMagikarp.Platform.UI.Entry, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullG(clr-namespace:Magikarp.Platform.UI.EntryMagikarp.Platform.UI.Entry  5   	   )    #Magikarp.Platform.UI.Entry.MainView   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   :local(clr-namespace:Magikarp.Platform.UI.Entry     5   	     � WindowState$  	Maximized=�5   
    � Title$ MainView��5	   	   $	��600��6   $	��800��6'   .��5
      � ���#e5e5e5 � ColumnDefinitions 5   
   �� 5      $
��50px �6   �� 5      $	��15* �6   $��500px��6+   $��125px��6<   �� 5      $	��85* �6   �� 5      $	��5px �6    � RowDefinitions 5   
   �� 5      $
��25px �6   �� 5      $��* �6   �� 5      $	��5px �6   .+�5   
   �� $��0��6   $��0��6"   $��4��62   ���#3a3a3a.+�5      �� .��6   file6   �� 5      .��6   About6   5      �� 5   
   $��1��6   $��0��6"   $��2��62   ���#7575756C   �� 5   
   -   5        $��FunctionEntryStackPanel��5      $��1��64   $��1��6A   $��2��6Q   ���#545454.+�5      �� ��6   �� *FunctionEntryModels+��5      �� 5      .R�5      3   -Magikarp.Platform.UI.Entry.FunctionEntryPanel  5       5"      5#      �� 5$   
   $��1��6   $��1��6%   $��2��65   $	��3px��6F   �� 5%   
   $��1��6   $��2��6   $��10q�6/    J� SnapsToDevicePixels . $��2q�6V    �� CornerRadius$ 5��6j   ._�5&      � ���#ffffff.+�5'      �� -   5        $��MainTabControl��5'      5(       J� Effect 5)      3  -System.Windows.Media.Effects.DropShadowEffect  5*         Color$ #bab8b2��6#      
BlurRadius$ 16Q�63   	   ShadowDepth$	 0Q�6C   
   Opacity$
 1Q�6S   5,      5-      5.      