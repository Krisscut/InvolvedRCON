   M S B A M L   `   `   `  ����  RconInvolvedB    <RconInvolved.OverlayViews.CreateProfileOverlayViews.HelpView   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      Q MElysium, Version=2.0.1042.4, Culture=neutral, PublicKeyToken=afa220db249e5b15/metro#http://schemas.codeplex.com/elysium  5      7params*http://schemas.codeplex.com/elysium/params  5      G CFramework.UI, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null'extrahttp://schemas.extra.com/ui  5      $	��350��5	      $��813.05��5
      .��5      � $��0,0,0,0q�6     � ColumnDefinitions  5   
   �� 5      $��* �6   �� 5      $
��Auto �6   �� 5      $��* �6    � RowDefinitions 5   
   �� 5      $��* �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $��* �6   .+�5   
   �� $��1��6   $��1��5         HeaderTextStyle#��[  5       ��5      �� *
Foreground+ �� RelativeSource ��  �� AncestorType)  #Framework.UI.Controls.OverlayWindow"  ��5      �� *#!(params:General.BoldPaddingValue)+ �� ConverterParameter$ Left, Top, Right��  DoubleToThicknessConverter �� 	Converter# [  �� Mode$ OneWay=� �� *Self+�� 5!       � Language$ fr-fr�6   $1�+Menu de création et modification de profil��6+   6`   �� 5#   
   $��1��6   $��2��5$        NormalTextStyle#��[ 5'      $
��Wrap=�5(      $	��600��6(   ��5%      �� *
Foreground+ �� "  ��5&      �� *(params:General.BoldPadding)+$ OneWay=� �� *Self+�� 5)      $ fr-fr�6   $���        Ce menu vous permet de creer un nouveau profil de serveur pour une connexion plus rapide lors de la prochaine utilisation.��6+   6�   �� 5+   
   $��1��6   $��3��5,      $	��Top=�6#   .+�5-      �� #��[  5/      $��BattleEye section��50      ��5-      ��  �� Path$ 
Foreground� �� "  ��5.      ��  �� *Self+$" (params:General.BoldPadding)�$ OneWay=�50      �� 52      #��[ 54      $
��Wrap=�55      $	��600��6,   ��52      �� *
Foreground+ �� "  ��53      �� *(params:General.BoldPadding)+$ OneWay=� �� *Self+�� 56      $ fr-fr�6   $k�e        Les principales informations nécéssaires pour se connecter et s'interfacer avec le serveur.��6/   6�   57      �� 58   
   $��1��6   $��5��59      $	��Top=�6#   .+�5:      �� #��[  5<      $��Notification��5=      ��5:      �� $ 
Foreground� �� "  ��5;      ��  �� *Self+$" (params:General.BoldPadding)�$ OneWay=�5=      �� 5?      #��[ 5A      $	��600��6A   $	��600��6P   $����       Le système de notification fonctionne au moyen de Websockets. Il permet de recevoir des notifications en temps réel sur les connexions des autres utilisateurs connectés, ainsi que d'autres fonctionnalités rendant l'application dynamique.��6\   $
��Wrap=�5B      ��5?      �� *
Foreground+ �� "  ��5@      �� *(params:General.BoldPadding)+$ OneWay=� �� *Self+5B      5C      -  'Framework.UI.Controls.AnimatedWrapPanel  5E   
   $��1��6"   $��6��5F   "   $��Center=�6/   .+�5G      $  Elysium.Controls.CommandButton  -   5          TransparentBrush#��[ 5H   "     OkCommandButtonStyle#��[ 5L   "   ��5I   "   �� $ 
Foreground� �� "  ��5J   "   �� $ 
Foreground� �� "  ��5K   "   �� # [ $ Left, Top, Right�� �� *Self+$' !(params:General.BoldPaddingValue)�$ OneWay=�5L   "     5M      -   5        #��[ 5N   "     CancelCommandButtonStyle#��[ 5R   "   ��5O   "   �� $ 
Foreground� �� "  ��5P   "   �� $ 
Foreground� �� "  ��5Q   "   �� # [ $ Left, Top, Right�� �� *Self+$' !(params:General.BoldPaddingValue)�$ OneWay=�5R   "   5S      5U      5V      