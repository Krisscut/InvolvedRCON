   M S B A M L   `   `   `  ����  RconInvolved;    5RconInvolved.OverlayViews.CreateConnectionOverlayView   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      Q MElysium, Version=2.0.1042.4, Culture=neutral, PublicKeyToken=afa220db249e5b15/metro#http://schemas.codeplex.com/elysium  5      7params*http://schemas.codeplex.com/elysium/params  5      G CFramework.UI, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null'extrahttp://schemas.extra.com/ui  5        � Title$T  NThis title only visible if TitleBarVisibility = Visible (Collapsed by default)��5
      $��411.05��5      $
��1035��5      .��5      � $��0,0,0,0q�6    � ColumnDefinitions 5   
   �� 5      $��* �6   �� 5      $
��Auto �6   �� 5      $��* �6    � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   .+�5   
   -  'Framework.UI.Controls.AnimatedWrapPanel  $��1��6"   .+�5      $  Elysium.Controls.CommandButton  -   5           TransparentBrush#��[  5   "     OkCommandButtonStyle#��[ 5    "   ��5   "   ��  �� Path$ 
Foreground� �� RelativeSource ��  �� AncestorType)  #Framework.UI.Controls.OverlayWindow"  ��5   "   �� $ 
Foreground� �� "  ��5   "   ��   DoubleToThicknessConverter �� 	Converter# [  �� ConverterParameter$ Left, Top, Right�� �� *Self+$' !(params:General.BoldPaddingValue)� �� Mode$ OneWay=�5    "     5!      -   5        #��[  5"   "     CancelCommandButtonStyle#��[ 5&   "   ��5#   "   �� $ 
Foreground� �� "  ��5$   "   �� $ 
Foreground� �� "  ��5%   "   �� # [ $ Left, Top, Right�� �� *Self+$' !(params:General.BoldPaddingValue)�$ OneWay=�5&   "   5'      �� 5)   
   $��1��6   $��1��5*        HeaderTextStyle#��[ 5-      ��5+      �� *
Foreground+ �� "  ��5,      �� *#!(params:General.BoldPaddingValue)+$ Left, Top, Right��# [ $ OneWay=� �� *Self+�� 5.      	 � Language$	 fr-fr�6   $"�Creation d'un profil serveur��6+   6R   �� 50   
   $��1��6   $��2��51        NormalTextStyle#��[ 54      $
��Wrap=�55      ��52      �� *
Foreground+ �� "  ��53      �� *(params:General.BoldPadding)+$ OneWay=� �� *Self+�� 56      $	 fr-fr�6   $��zCe menu vous permet de creer un nouveau profil de serveur pour une connexion plus rapide lors de la prochaine utilisation.��6+   6�   �� 59   
   $��1��6   $��3��5:      $	��Top=�6#   .+�5;      �� #��[ 5=      $��Informations��5>      ��5;      �� $ 
Foreground� �� "  ��5<      ��  �� *Self+$" (params:General.BoldPadding)�$ OneWay=�5>      � 5?       5@      �� 5A      $��* �6'   �� 5B      $��* �6'   �� 5C      $��* �6'   �� 5D      $��* �6'    5F      �� 5G      $
��Auto �6$   �� 5H      $
��Auto �6$   �� 5I      $
��Auto �6$   .+�5L      �� $��Nom du profil��6   $
��Left=�5O   Z   ��5M      �� $ 
Foreground� �� "  ��5N      ��  �� *Self+$" (params:General.BoldPadding)�$ OneWay=�
 �� Target
 5O      ��  �� ElementName$( "HorizontalContentAlignmentComboBox��6Z   �� 5Q      -   5         ��Name$ ProfilNameValue��5Q      $��1��5R      $��3��5S      $�� ��5U      $��Center=�5V      ��5T      ��  �� *Self+$" (params:General.BoldPadding)�$ OneWay=�5V      �� 5X      $��1��6   $��Nom d'hôte ou IP��5Y      $
��Left=�5[   �   ��5Z      �� $ 
Foreground� �� "  ��5[      ��  �� *Self+$" (params:General.BoldPadding)�$ OneWay=�6�   �� 5]      -   5        $ HostnameValue��5]      $��1��5^      $��1��5_      $�� ��5a      $��Center=�5b      ��5`      ��  �� *Self+$" (params:General.BoldPadding)�$ OneWay=�5b      �� 5c      $��1��6   $��2��5d      $
��Port��5e      $��Center=�5g   �   ��5f      �� $ 
Foreground� �� "  ��5g      ��  �� *Self+$" (params:General.BoldPadding)�$ OneWay=�6�   �� 5i      -   5        $ 	PortValue��5i      $��1��5j      $��3��5k      $�� ��5m      $��Center=�5n      ��5l      ��  �� *Self+$" (params:General.BoldPadding)�$ OneWay=�5n      �� 5o      $��2��6   $��2��5p      $��Mot de passe��5q      $
��Left=�5s   �   ��5r      �� $ 
Foreground� �� "  ��5s      ��  �� *Self+$" (params:General.BoldPadding)�$ OneWay=�6�   G� 5u      -   5         G�Name$ PasswordValue��5u      $��2��5v      $��1��5w      $��3��5x      $��Center=�5z      ��5y      ��  �� *Self+$" (params:General.BoldPadding)�$ OneWay=�5z      5{      �� 5}      #��[ 5      ��5}      �� *
Foreground+ �� "  ��5~      �� *(params:General.BoldPadding)+$ OneWay=� �� *Self+�� 5�      $	 fr-fr�6   $�Autre propriétés��6/   6J   �� 5�      #��[ 5�      ��5�      �� *
Foreground+ �� "  ��5�      �� *(params:General.BoldPadding)+$ OneWay=� �� *Self+�� 5�      $	 fr-fr�6   $D�>Les autre informations nécessaires au fonctionnement du RCON.��6/   6w   � 5�       5�      �� 5�      $	��43* �6'   �� 5�      $	��68* �6'   �� 5�      $
��111* �6'    5�      �� 5�      $
��Auto �6$   .+�5�      �� $��Auto-connexion��5�      $��119,6q�5�       � ToolTip$8 2Auto-connexion lorsque ce profil est selectionné?��6'   $��Center=�6c   $��2��6�   ��5�      �� $ 
Foreground� �� "  5�   �   #  Elysium.Controls.ToggleSwitch  5�      -   5          Name$ AutoReconnect��5�   %   $��2��5�   %   $��Center=�5�   %   $��143,11,142,11q�5�   %   $8 2Auto-connexion lorsque ce profil est selectionné?��6<   5�      5�      5�      5�      