   M S B A M L   `   `   `  ����F  BJocVaixells, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null)clr-namespace:JocVaixellsJocVaixells  5   	       JocVaixells.MainWindow   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   +localclr-namespace:JocVaixells     5   	     � Title$  
MainWindow��5   	   $	��450��6   $	��800��6)    � 	Resources 5	         ColorChangeStoryboard �� 
TargetName �� TargetProperty	 �� To g� Duration  ColorChangeStoryboard2��%_  &	        &	 �     �� 5
   
   .��5      �� $ displayVisible��5      $2 ,(TextBox.Background).(SolidColorBrush.Color)�5       Blue5      $ 0:0:1D�6   5      �� 5   
   .��5      �� $ displayVisible��5      $2 ,(TextBox.Background).(SolidColorBrush.Color)�5      	 Green5      $ 0:0:1D�6   5      5      .��5      �  � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $��* �6   �� 5      $
��auto �6    � ColumnDefinitions 5   
   �� 5      $��* �6   .+�5!   
   ��  �� Orientation$ 
Horizontal=�6   $��5q�6.   .+�5"      �� $��Filas:��6   $��Center=�6%   �� 5#      -   5        	 ��Name$	 txtRows��5#      $��40��6'   $��5q�62   6=   �� 5$      $��	Columnas:��6   $��Center=�6(   $��5q�6C   �� 5%      -   5        $	 txtCols��5%      $��40��6'   $��5q�62   6=   �� 5&      $	��IP:��6   $��Center=�6"   �� 5'      -   5        $	 txtIP��5'      $	��120��6%   $��5q�61   �� 5(      $��Puerto:��6   $��Center=�6&   $��5q�6A   �� 5)      -   5        $	 txtPort��5)      $��60��6'   $��5q�62   �� 5*      -   5        
 ��Name$
 	btnServer��5*      $��Start Server��6(   $	��100��6?   $��30��6K   $��5q�6W   6b   �� 5+      $��
Animació:��6   $��Center=�6(   �� 5,      -   5        $	 displayVisible��5,      $��60��6.   $��5q�69   5-         JocVaixells.TableControl  5/   
   -   5          Name$ 	mainTable��5/   :      JocVaixells.TableControl   SelectItemClick$ mainTable_SelectItemClick��6`      SelectItemRightClick$$ mainTable_SelectItemRightClick��6�   $��1��6   $��0��6*      Cols$ 3��6N      Rows$ 3��6W   6�   �� 51   
   $��2��6   $��0��6"   .+�52      �� $��Console:��6   �� 53      -   5        $	 
txtConsole��53      $��Bottom=�6*    �� 
IsReadOnly$
 True��6E   $	��100��6W   $��5q�6d   54      55      57      