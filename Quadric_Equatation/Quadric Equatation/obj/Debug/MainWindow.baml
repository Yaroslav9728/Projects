   M S B A M L   `   `   `  ����M  IQuadric Equatation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null7 clr-namespace:Quadric_EquatationQuadric_Equatation  5   	   #    Quadric_Equatation.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   2local clr-namespace:Quadric_Equatation     5   	     � Title$  
MainWindow��5   	   $	��394��6   $��532.5��6)   .��5	      � $��	0,0,-8,-1q�6    � ColumnDefinitions 5
   
   �� 5      �� 5      $��0* �6   ��5   
   �� 5       �� ImageSource$, &Resources/math-background-17535970.jpg��6   .+�5   
   �� -   5         ��Name$
 ABox��5      $
��Left=�6    $��38��6;   $��	90,88,0,0q�6G    �� TextWrapping$
 Wrap=�6Z   $�� ��6n   $	��Top=�6v   $��53��6�   $��24(�6�   �� 5   
   -   5         ��Name$	 ax2��5      $	��x^2��6   $
��Left=�6+   $��
143,86,0,0q�6F   $	��Top=�6Z   $��40��6r   $��55��6~   $��24(�6�   
�������6�   �� 5   
   -   5        $
 BBox��5      $
��Left=�6    $��38��6;   $��
203,88,0,0q�6G   $
 Wrap=�6[   $�� ��6o   $	��Top=�6w   $��44��6�    J� RenderTransformOrigin$ -0.091,0.521&�6�   $��24(�6�   �� 5   
   -   5        $ x��5      $��x��6   $
��Left=�6'   $��
252,88,0,0q�6B   $	��Top=�6V   $��38��6n   $��32��6z   $��24(�6�   
�������6�   �� 5   
   -   5        $
 CBox��5      $
��Left=�6    $��38��6;   $��
289,88,0,0q�6G   $
 Wrap=�6[   $�� ��6o   $	��Top=�6w   $��44��6�   $ -0.091,0.521&�6�   $��24(�6�   �� 5   
   -   5        $ label��5      $	��= 0��6   $
��Left=�6-   $��
338,88,0,0q�6H   $	��Top=�6\   $��39��6t   $��24(�6�   $��46��6�   $ -0.239,0.641&�6�   
�������6�   �� 5   
   -   5         ��Name$ Input_BButton��5      $��Input values��6(   $
��Left=�6?   $��
75,321,0,0q�6Z   $	��Top=�6n   $	��108��6�   $��33��6�   6�   �� 5   
   -   5        $ Calculate_Button��5      $��	Calculate��6+   $
��Left=�6?   $��332,321,0,0q�6Z   $	��Top=�6o   $	��124��6�   $��33��6�   6�   5      5      