   M S B A M L   `   `   `  ����A  =sysUcz, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullclr-namespace:ID3ID3  5   	       ID3.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   #localclr-namespace:ID3     5   	     � Title$  ID3 - Bartosz Babczyński��5   	   $��338.396��6*   $	��759��6;   .��5	      � ���	#FF93856E.+�5
   
   �� -   5         ��Name$ btn_FindDataFile��5
      $��Szukaj��6+   $
��Left=�6<   $��26��6W   $��
138,10,0,0q�6c   $	��Top=�6w   $��85��6�   6�   �� 5   
   -   5         ��Name$ 	lbl_info1��5      $��Wskaż plik z danymi: ��6#   $
��Left=�6C   $��26��6^   $��	10,10,0,0q�6j   $	��Top=�6}   $	��128��6�   �� 5   
   -   5        $ lbl_Path��5      $
��Left=�6"   $��26��6=   $��
228,10,0,0q�6I   $	��Top=�6]   $	��279��6u   5      &   System.Windows.Controls.DataGrid  5   
   -   5          Name$ dataGrid��5         CanUserAddRows .     CanUserDeleteRows .     CanUserResizeColumns .     CanUserResizeRows .     CanUserSortColumns .  $
��Left=�6�   $	��221��6�   $��	10,72,0,0q�6�   $	��Top=�6�   $	��497��6�   �� 5   
   -   5        $ lbl_entropyInfo��5      $y�Hidden=�6)   $%��Entropia dla kolejnych kolumn: ��6=   $
��Left=�6g   $��27��6�   $��
10,298,0,0q�6�   $	��Top=�6�   $	��174��6�   �� 5   
   -   5        	 ��Name$	 txtBl_entropyVals��5      $y�Hidden=�6/   $
��Left=�6C   $	��109��6^   $��189,298,0,0q�6k   $
��Wrap=�6�   $	��Top=�6�   $	��307��6�   �� 5   
   -   5        
 ��Name$
 cB_CatAttColNumb��5      $
��Left=�6-   $��26��6H   $��
240,41,0,0q�6T   $	��Top=�6h   $��56��6�   {�.  6�   �� 5   
   -   5        $ lbl_CatAttColNum��5      $+��%Nr kolumny z atrybutem kategorycznym:��6*   $
��Left=�6Z   $��26��6u   $��	10,41,0,0q�6�   $	��Top=�6�   $	��225��6�    J� RenderTransformOrigin$ 0.23,-0.818&�6�   �� 5   
   -	   5        $ btn_ID3��5      $	��ID3��6"   $
��Left=�60   $��26��6K   $��
422,41,0,0q�6W   $	��Top=�6k   $��85��6�   6�   �� 5   
   -
   5         ��Name$ tB_Tree��5      $
��Auto=�6#   $
��Left=�6S   $	��221��6n   $��
512,72,0,0q�6{   $	��Top=�6�   $	��220��6�   5      5      