   M S B A M L   `   `   `  ����L  HCreateReactFromDB, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null5clr-namespace:CreateReactFromDBCreateReactFromDB  5   	   "    CreateReactFromDB.MainWindow   5   	   W SPresentationCore, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   1localclr-namespace:CreateReactFromDB     5   	   -   5          � Title$   Create React js components��5   	   $	��550��6,   $	��800��69   .��5
      �  � RowDefinitions 5   
   �� 5      $��5 �6   �� 5      �� 5      $��5 �6    � ColumnDefinitions 5   
   �� 5      $��5 �6   �� 5      $��* �6   �� 5      $��5 �6   .+�5   
   �� $��1��6   $��1��6%    �� Orientation$ Vertical=�62   .+�5      �� $ Vertical=�6   .+�5      �� $��Connection string:��6   �� 5      -   5         ��Name$ txtConnection��5      $Q��KServer=SURFACE-MARTYN1;database=GatherStockCommon;Integrated Security=true;��61   $	��500��6�   $
��Left=�6�   5      �� 5      $ 
Horizontal=�6   $��0,10,0,0q�62   .+�5      �� -   5         ��Name$ 
butConnect��5      $��Connect to database��6-   $	��200��6K      btn-warning#��[  6p   �� 5      -   5         ��Name$ 
lblConnect��5      $��2q�6,   $�� ��67   6D   5      �� 5      $ Vertical=�6   $��0,10,0,0q�60   .+�5      �� $1��+Select table to create React Component for:��6   �� 5       -   5         ��Name$ 	cboTables��5       $
��Left=�6.   $	��500��6I   6U   5!      &   System.Windows.Controls.DataGrid  5"      -   5          Name$ 	grdFields��5"      $	��200��6*   $��0,10,0,0q�67   	   AutoGenerateColumns$	 False��6I   ��6e   �� 
   Columns
 5#      0  *System.Windows.Controls.DataGridTextColumn  5$      ,  &System.Windows.Controls.DataGridColumn   Header$ Order��6)   1  +System.Windows.Controls.DataGridBoundColumn   Binding 68   �� *ORDINAL_POSITION+4  .System.Windows.Controls.DataGridCheckBoxColumn  5%      $ In List��6-    6>   �� *
IsInList+  5&      $ Visible��6-    6>   �� *	IsVisible+  5'      $ Caption��6)      Width5  /System.Windows.Controls.DataGridLengthConverter$	 150 6:    6F   �� *Readable_Caption+4  .System.Windows.Controls.DataGridTemplateColumn  5(      $
 Type��6-   $	 100 6;      CellTemplate 5)      �� 5*      .R�5+   "   �� ��6,   �� *	DATA_TYPE+5,      5.        5/      $ Default��6)   $	 150 6:    6F   �� *COLUMN_DEFAULT+  50      $ 
Max Length��6)   $	 100 6=    6I   �� *CHARACTER_MAXIMUM_LENGTH+  51      $ 
Validation��6-    6A   �� *IsValidated+  52      $ Category��6-    53      �� 54      .R�55   "   �� ��6,   �� *
Category+56      58      5:      �� 5;      $ Vertical=�6   .+�5<      �� $��Output folder:��6    �� Left$ 0��61   
 �� Top$	 130��6A   �� 5=      -   5        $ 	txtOutput��5=      $ 5��6-   $	 156��6=   $��C:\temp��6N   $	��495��6]   $
��Left=�6i   5>      �� 5?      $��26��6   $	��790��6%   $ 
Horizontal=�61   $��0,10,0,0q�6J   $
��Left=�6\   .+�5@      �� -   5         ��Name$ chkList��5@      $
��List��6,   $��4q�6;    d� 	IsChecked$
 TrueY�6F   �� 5A      -	   5        $ 	chkCreate��5A      $��Create��6.   $��4q�6?   $
 TrueY�6J   �� 5B      -
   5        $ chkRead��5B      $
��Read��6,   $��4q�6;   $
 TrueY�6F   �� 5C      -   5        $ 	chkUpdate��5C      $��Update��6.   $��4q�6?   $
 TrueY�6J   �� 5D      -   5        $ 	chkDelete��5D      $��Delete��6.   $��4q�6?   $
 TrueY�6J   5E      �� 5F      $ 
Horizontal=�6   $��0,10,0,0q�62   .+�5G      �� -   5        $ 	butCreate��5G      $��Create React js��6,   $	��200��6F     btn-primary#��[ 6R   6w   �� 5H      -   5        $ 	lblCreate��5H      $��2q�6+   $�� ��66   6C   5I      5J      5M      5N      