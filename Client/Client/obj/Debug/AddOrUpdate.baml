   M S B A M L   `   `   `  ����A  =Client, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullclr-namespace:ClientClient  5   	       Client.AddOrUpdate   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   &localclr-namespace:Client     5   	     � WindowStartupLocation$  CenterOwner=�5   	   $	��270��6-   $	��350��6:    � 
ResizeMode$ NoResize=�6F   .��5	      �  � ColumnDefinitions 5
   
   �� 5      .+�5   
   �� -   5         ��Name$	 img��5      $	��100��6   $��	20,10,0,0q�6*   $	��Top=�6=   $
��Left=�6U   $	��100��6p   �� 5   
   -   5        $��chooseButton��5      $��Choose image...��6%   $��170,90,50,0q�6?   $	��Top=�6T   6l   �� 5   
   -   5         ��Name$ textBox��5      $��25��6#   $��10,140,10,0q�6/    �� TextAlignment$ Center=�6D   $	��Top=�6[   �� 5   
    �� Orientation$ 
Horizontal=�6   $��39��6.   $
��auto��6:   $��
77,0,77,10q�6G   $��Bottom=�6[   .+�5      �� -   5         ��Name$ okButton��5      $��Ok��6'   $	��Top=�64   $��10q�6L   $��75��6X   6c   �� 5      -   5        $ cancelButton��5      $��Cancel��6+   $	��Top=�6<   $��10q�6T   $��75��6`   6k   5      5      5      