   M S B A M L   `   `   `  ����   RconInvolved;    5RconInvolved.OverlayViews.CreateConnectionOverlayView   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     Q MElysium, Version=2.0.1042.4, Culture=neutral, PublicKeyToken=afa220db249e5b15/metro#http://schemas.codeplex.com/elysium  7params*http://schemas.codeplex.com/elysium/params  G CFramework.UI, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null'extrahttp://schemas.extra.com/ui    � Title$T  NThis title only visible if TitleBarVisibility = Visible (Collapsed by default)��.���  � ColumnDefinitions �� $��* ��� $
��Auto ��� $��* � � RowDefinitions �� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto �.+�-  'Framework.UI.Controls.AnimatedWrapPanel  $��1��.+�$  Elysium.Controls.CommandButton  -      TransparentBrush#��[    OkCommandButtonStyle#��[ ����  �� Path$ 
Foreground� �� RelativeSource ��  �� AncestorType)  #Framework.UI.Controls.OverlayWindow"  ���� $ 
Foreground� �� "  ����   DoubleToThicknessConverter �� 	Converter# [  �� ConverterParameter$ Left, Top, Right�� �� *Self+$' !(params:General.BoldPaddingValue)� �� Mode$ OneWay=�  -   #��[    CancelCommandButtonStyle#��[ ���� $ 
Foreground� �� "  ���� $ 
Foreground� �� "  ���� # [ $ Left, Top, Right�� �� *Self+$' !(params:General.BoldPaddingValue)�$ OneWay=��� $��1��$��1��  HeaderTextStyle#��[ $��Title������ $ 
Foreground� �� "  ���� # [ $ Left, Top, Right�� �� *Self+$' !(params:General.BoldPaddingValue)�$ OneWay=��� $��1��$��2��  NormalTextStyle#��[ $V��PContent Content Content Content Content Content Content Content Content Content.������ $ 
Foreground� �� "  ����  �� *Self+$" (params:General.BoldPadding)�$ OneWay=��� $��1��$��3��.+��� #��[ $ ��Standard Window Properties������ $ 
Foreground� �� "  ����  �� *Self+$" (params:General.BoldPadding)�$ OneWay=��  �� $��* ��� $��* � �� $
��Auto ��� $
��Auto �.+��� $ ��HorizontalContentAlignment������ $ 
Foreground� �� "  ����  �� *Self+$" (params:General.BoldPadding)�$ OneWay=�	 �� Target	 �� 
 �� ElementName$(
 "HorizontalContentAlignmentComboBox���� -    ��Name$( "HorizontalContentAlignmentComboBox��$��1������  �� *Self+$" (params:General.BoldPadding)�$ OneWay=� �� SelectedItem �� $  HorizontalContentAlignment� �� $ extra:OverlayWindowL�.@�(  "System.Windows.HorizontalAlignment Left=� 	Right=� 
Center=� Stretch=��� $��1��$��VerticalContentAlignment������ $ 
Foreground� �� "  ����  �� *Self+$" (params:General.BoldPadding)�$ OneWay=�	 �� $&
  VerticalContentAlignmentComboBox���� -   $&  VerticalContentAlignmentComboBox��$��1��$��1������  �� *Self+$" (params:General.BoldPadding)�$ OneWay=� �� $ VerticalContentAlignment� �� $ extra:OverlayWindowL�.@�&   System.Windows.VerticalAlignment Top=� 
Bottom=� 
Center=� Stretch=��� #��[ $��Custom Window Properties������ $ 
Foreground� �� "  ����  �� *Self+$" (params:General.BoldPadding)�$ OneWay=��� #��[ $}��wOverlayWindow has many of the same properties as the Window class (See Window tab). Here are just the most useful ones.������ $ 
Foreground� �� "  ����  �� *Self+$" (params:General.BoldPadding)�$ OneWay=��  �� $��* ��� $��* ��� $��* ��� $��* � �� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto �.+��� $��IsOverlayVisible������ $ 
Foreground� �� "  ����  �� *Self+$" (params:General.BoldPadding)�$ OneWay=�	 �� $"
 IsOverlayVisibleToggleSwitch��#  Elysium.Controls.ToggleSwitch  -     Name$" IsOverlayVisibleToggleSwitch��$��1��$
��Left=�   	IsChecked �� $ IsOverlayVisible� �� $ extra:OverlayWindowL�����  �� *Self+$" (params:General.BoldPadding)�$ OneWay=��� $��1��$��OverlayBackground������ $ 
Foreground� �� "  ����  �� *Self+$" (params:General.BoldPadding)�$ OneWay=�	 �� $
 OverlayBackgroundTextBox���� -    ��Name$ OverlayBackgroundTextBox��$��1��$��1��$��Center=�����  �� *Self+$" (params:General.BoldPadding)�$ OneWay=����� $ OverlayBackground� �� $ extra:OverlayWindowL��� $��2��$��OverlayBorderBrush������ $ 
Foreground� �� "  ����  �� *Self+$" (params:General.BoldPadding)�$ OneWay=�	 �� $
 OverlayBorderBrushTextBox���� -   $ OverlayBorderBrushTextBox��$��1��$��2��$��Center=�����  �� *Self+$" (params:General.BoldPadding)�$ OneWay=����� $ OverlayBorderBrush� �� $ extra:OverlayWindowL��� $��3��$��OverlayBorderThickness������ $ 
Foreground� �� "  ����  �� *Self+$" (params:General.BoldPadding)�$ OneWay=�	 �� $#
 OverlayBorderThicknessTextBox���� -   $# OverlayBorderThicknessTextBox��$��1��$��3��$��Center=�����  �� *Self+$" (params:General.BoldPadding)�$ OneWay=����� $ OverlayBorderThickness� �� $ extra:OverlayWindowL��� $��4��$��OverlayMargin������ $ 
Foreground� �� "  ����  �� *Self+$" (params:General.BoldPadding)�$ OneWay=�	 �� $
 OverlayMarginTextBox���� -	   $ OverlayMarginTextBox��$��1��$��4��$��Center=�����  �� *Self+$" (params:General.BoldPadding)�$ OneWay=����� $ OverlayMargin� �� $ extra:OverlayWindowL��� $��5��$��OverlayOpacity������ $ 
Foreground� �� "  ����  �� *Self+$" (params:General.BoldPadding)�$ OneWay=�	 �� $
 OverlayOpacityTextBox���� -
   $ OverlayOpacityTextBox��$��1��$��5��$��Center=�����  �� *Self+$" (params:General.BoldPadding)�$ OneWay=����� $ OverlayOpacity� �� $ extra:OverlayWindowL��� $��2��$��IsBusy������ $ 
Foreground� �� "  ����  �� *Self+$" (params:General.BoldPadding)�$ OneWay=�	 �� $
 IsBusyToggleSwitch��  -   $ IsBusyToggleSwitch��$��3�� �� $ IsBusy� �� $ extra:OverlayWindowL�����  �� *Self+$" (params:General.BoldPadding)�$ OneWay=��� $��2��$��1��$��Progress������ $ 
Foreground� �� "  ����  �� *Self+$" (params:General.BoldPadding)�$ OneWay=�	 �� $
 ProgressDoubleUpDown��(  "Framework.UI.Controls.DoubleUpDown  -     Name$ ProgressDoubleUpDown��$��3��$��1���  �Framework.UI.Controls.UpDownBase`1[[System.Nullable`1[[System.Double, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]]   	AllowSpin . �	  �Framework.UI.Controls.NumericUpDown`1[[System.Nullable`1[[System.Double, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]] 	  	Increment$ 10X� 	  Maximum$	 100X� 	  Minimum$ 0X�   ShowButtonSpinner . ����  �� *Self+$" (params:General.BoldPadding)�$ OneWay=�   Value �� $ Progress� �� $ extra:OverlayWindowL��� $��2��$��2��$��TitleBarVisibility������  �� *Self+$" (params:General.BoldPadding)�$ OneWay=�	 �� $ 
 TitleBarVisibilityComboBox���� -   $  TitleBarVisibilityComboBox��$��3��$��2������  �� *Self+$" (params:General.BoldPadding)�$ OneWay=� �� $ TitleBarVisibility� �� "  .@�
  System.Windows.Visibility
 Visible=�
 
Hidden=�
 	Collapsed=�