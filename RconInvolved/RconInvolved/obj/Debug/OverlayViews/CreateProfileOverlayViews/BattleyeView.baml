   M S B A M L   `   `   `  ����  RconInvolvedF    @RconInvolved.OverlayViews.CreateProfileOverlayViews.BattleyeView   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      Q MElysium, Version=2.0.1042.4, Culture=neutral, PublicKeyToken=afa220db249e5b15/metro#http://schemas.codeplex.com/elysium  5      7params*http://schemas.codeplex.com/elysium/params  5      G CFramework.UI, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null'extrahttp://schemas.extra.com/ui  5      $	��350��5	      $��813.05��5
      .��5      � $��0,0,0,0q�6     � ColumnDefinitions  5   
   �� 5      $��* �6   �� 5      $
��Auto �6   �� 5      $��* �6    � RowDefinitions 5   
   �� 5      $��* �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $��* �6   .+�5   
   �� $��1��6   $��1��5         HeaderTextStyle#��[  5      ��5      �� *
Foreground+ �� RelativeSource ��  �� AncestorType)  #Framework.UI.Controls.OverlayWindow"  ��5      �� *#!(params:General.BoldPaddingValue)+ �� ConverterParameter$ Left, Top, Right��  DoubleToThicknessConverter �� 	Converter# [  �� Mode$ OneWay=� �� *Self+�� 5       � Language$ fr-fr�6   $"�Creation d'un profil serveur��6+   6R   �� 5!   
   $��1��6   $��2��5"        NormalTextStyle#��[ 5%      $
��Wrap=�5&      ��5#      �� *
Foreground+ �� "  ��5$      �� *(params:General.BoldPadding)+$ OneWay=� �� *Self+�� 5'      $ fr-fr�6   $���        Ce menu vous permet de creer un nouveau profil de serveur pour une connexion plus rapide lors de la prochaine utilisation.��6+   6�   �� 5*   
   $��1��6   $��3��5+      $	��Top=�6#   .+�5,      �� #��[  5.      $��Informations��5/      ��5,      ��  �� Path$ 
Foreground� �� "  ��5-      ��  �� *Self+$" (params:General.BoldPadding)�$ OneWay=�5/      � 50        51      �� 52      $��* �6'   �� 53      $��* �6'   �� 54      $��* �6'   �� 55      $��* �6'    57      �� 58      $
��Auto �6$   �� 59      $
��Auto �6$   �� 5:      $
��Auto �6$   .+�5=      �� $��Nom du profil��6   $
��Left=�5@   Z   ��5>      �� $ 
Foreground� �� "  ��5?      ��  �� *Self+$" (params:General.BoldPadding)�$ OneWay=�	 �� Target	 5@      �� 
 �� ElementName$(
 "HorizontalContentAlignmentComboBox��6Z   �� 5B      -   5         ��Name$ ProfilNameValue��5B      $��1��5C      $��3��5D      $�� ��5F      $��Center=�5G      ��5E      ��  �� *Self+$" (params:General.BoldPadding)�$ OneWay=�5G      �� 5I      $��1��6   $��Nom d'hôte ou IP��5J      $
��Left=�5L   �   ��5K      �� $ 
Foreground� �� "  ��5L      ��  �� *Self+$" (params:General.BoldPadding)�$ OneWay=�6�   �� 5N      -   5        $ HostnameValue��5N      $��1��5O      $��1��5P      $�� ��5R      $��Center=�5S      ��5Q      ��  �� *Self+$" (params:General.BoldPadding)�$ OneWay=�5S      �� 5T      $��1��6   $��2��5U      $
��Port��5V      $��Center=�5X   �   ��5W      �� $ 
Foreground� �� "  ��5X      ��  �� *Self+$" (params:General.BoldPadding)�$ OneWay=�6�   �� 5Z      -   5        $ 	PortValue��5Z      $��1��5[      $��3��5\      $�� ��5^      $��Center=�5_      ��5]      ��  �� *Self+$" (params:General.BoldPadding)�$ OneWay=�5_      �� 5`      $��2��6   $��2��5a      $��Mot de passe��5b      $
��Left=�5d   �   ��5c      �� $ 
Foreground� �� "  ��5d      ��  �� *Self+$" (params:General.BoldPadding)�$ OneWay=�6�   G� 5f      -   5         G�Name$ PasswordValue��5f      $��2��5g      $��1��5h      $��3��5i      $��Center=�5k      ��5j      ��  �� *Self+$" (params:General.BoldPadding)�$ OneWay=�5k      5l      �� 5n      #��[  5p      ��5n      �� *
Foreground+ �� "  ��5o      �� *(params:General.BoldPadding)+$ OneWay=� �� *Self+�� 5q      $ fr-fr�6   $�Autre propriétés��6/   6J   �� 5r      #��[ 5t      ��5r      �� *
Foreground+ �� "  ��5s      �� *(params:General.BoldPadding)+$ OneWay=� �� *Self+�� 5u      $ fr-fr�6   $M�G        Les autres informations nécessaires au fonctionnement du RCON.��6/   6�   � 5v        5w      �� 5x      $	��43* �6'   �� 5y      $	��68* �6'   �� 5z      $
��111* �6'    5|      �� 5}      $
��Auto �6$   .+�5�      �� $��Auto-connexion��5�      $��119,6q�5�       � ToolTip$8 2Auto-connexion lorsque ce profil est selectionné?��6'   $��Center=�6c   $��2��6�   ��5�      �� $ 
Foreground� �� "  5�   �   #  Elysium.Controls.ToggleSwitch  5�      -   5          Name$ AutoReconnect��5�   %   $��2��5�   %   $��Center=�5�   %   $��143,11,142,11q�5�   %   $8 2Auto-connexion lorsque ce profil est selectionné?��6<   5�      5�      5�      5�      