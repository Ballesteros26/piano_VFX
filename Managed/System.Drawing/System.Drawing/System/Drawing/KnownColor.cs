using System;

namespace System.Drawing
{
	/// <summary>Specifies the known system colors.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000078 RID: 120
	public enum KnownColor
	{
		/// <summary>The system-defined color of the active window's border.</summary>
		// Token: 0x040003FB RID: 1019
		ActiveBorder = 1,
		/// <summary>The system-defined color of the background of the active window's title bar.</summary>
		// Token: 0x040003FC RID: 1020
		ActiveCaption,
		/// <summary>The system-defined color of the text in the active window's title bar.</summary>
		// Token: 0x040003FD RID: 1021
		ActiveCaptionText,
		/// <summary>The system-defined color of the application workspace. The application workspace is the area in a multiple-document view that is not being occupied by documents.</summary>
		// Token: 0x040003FE RID: 1022
		AppWorkspace,
		/// <summary>The system-defined face color of a 3-D element.</summary>
		// Token: 0x040003FF RID: 1023
		Control,
		/// <summary>The system-defined shadow color of a 3-D element. The shadow color is applied to parts of a 3-D element that face away from the light source.</summary>
		// Token: 0x04000400 RID: 1024
		ControlDark,
		/// <summary>The system-defined color that is the dark shadow color of a 3-D element. The dark shadow color is applied to the parts of a 3-D element that are the darkest color.</summary>
		// Token: 0x04000401 RID: 1025
		ControlDarkDark,
		/// <summary>The system-defined color that is the light color of a 3-D element. The light color is applied to parts of a 3-D element that face the light source.</summary>
		// Token: 0x04000402 RID: 1026
		ControlLight,
		/// <summary>The system-defined highlight color of a 3-D element. The highlight color is applied to the parts of a 3-D element that are the lightest color.</summary>
		// Token: 0x04000403 RID: 1027
		ControlLightLight,
		/// <summary>The system-defined color of text in a 3-D element.</summary>
		// Token: 0x04000404 RID: 1028
		ControlText,
		/// <summary>The system-defined color of the desktop.</summary>
		// Token: 0x04000405 RID: 1029
		Desktop,
		/// <summary>The system-defined color of dimmed text. Items in a list that are disabled are displayed in dimmed text.</summary>
		// Token: 0x04000406 RID: 1030
		GrayText,
		/// <summary>The system-defined color of the background of selected items. This includes selected menu items as well as selected text. </summary>
		// Token: 0x04000407 RID: 1031
		Highlight,
		/// <summary>The system-defined color of the text of selected items.</summary>
		// Token: 0x04000408 RID: 1032
		HighlightText,
		/// <summary>The system-defined color used to designate a hot-tracked item. Single-clicking a hot-tracked item executes the item.</summary>
		// Token: 0x04000409 RID: 1033
		HotTrack,
		/// <summary>The system-defined color of an inactive window's border.</summary>
		// Token: 0x0400040A RID: 1034
		InactiveBorder,
		/// <summary>The system-defined color of the background of an inactive window's title bar.</summary>
		// Token: 0x0400040B RID: 1035
		InactiveCaption,
		/// <summary>The system-defined color of the text in an inactive window's title bar.</summary>
		// Token: 0x0400040C RID: 1036
		InactiveCaptionText,
		/// <summary>The system-defined color of the background of a ToolTip.</summary>
		// Token: 0x0400040D RID: 1037
		Info,
		/// <summary>The system-defined color of the text of a ToolTip.</summary>
		// Token: 0x0400040E RID: 1038
		InfoText,
		/// <summary>The system-defined color of a menu's background.</summary>
		// Token: 0x0400040F RID: 1039
		Menu,
		/// <summary>The system-defined color of a menu's text.</summary>
		// Token: 0x04000410 RID: 1040
		MenuText,
		/// <summary>The system-defined color of the background of a scroll bar.</summary>
		// Token: 0x04000411 RID: 1041
		ScrollBar,
		/// <summary>The system-defined color of the background in the client area of a window.</summary>
		// Token: 0x04000412 RID: 1042
		Window,
		/// <summary>The system-defined color of a window frame.</summary>
		// Token: 0x04000413 RID: 1043
		WindowFrame,
		/// <summary>The system-defined color of the text in the client area of a window.</summary>
		// Token: 0x04000414 RID: 1044
		WindowText,
		/// <summary>A system-defined color.</summary>
		// Token: 0x04000415 RID: 1045
		Transparent,
		/// <summary>A system-defined color.</summary>
		// Token: 0x04000416 RID: 1046
		AliceBlue,
		/// <summary>A system-defined color.</summary>
		// Token: 0x04000417 RID: 1047
		AntiqueWhite,
		/// <summary>A system-defined color.</summary>
		// Token: 0x04000418 RID: 1048
		Aqua,
		/// <summary>A system-defined color.</summary>
		// Token: 0x04000419 RID: 1049
		Aquamarine,
		/// <summary>A system-defined color.</summary>
		// Token: 0x0400041A RID: 1050
		Azure,
		/// <summary>A system-defined color.</summary>
		// Token: 0x0400041B RID: 1051
		Beige,
		/// <summary>A system-defined color.</summary>
		// Token: 0x0400041C RID: 1052
		Bisque,
		/// <summary>A system-defined color.</summary>
		// Token: 0x0400041D RID: 1053
		Black,
		/// <summary>A system-defined color.</summary>
		// Token: 0x0400041E RID: 1054
		BlanchedAlmond,
		/// <summary>A system-defined color.</summary>
		// Token: 0x0400041F RID: 1055
		Blue,
		/// <summary>A system-defined color.</summary>
		// Token: 0x04000420 RID: 1056
		BlueViolet,
		/// <summary>A system-defined color.</summary>
		// Token: 0x04000421 RID: 1057
		Brown,
		/// <summary>A system-defined color.</summary>
		// Token: 0x04000422 RID: 1058
		BurlyWood,
		/// <summary>A system-defined color.</summary>
		// Token: 0x04000423 RID: 1059
		CadetBlue,
		/// <summary>A system-defined color.</summary>
		// Token: 0x04000424 RID: 1060
		Chartreuse,
		/// <summary>A system-defined color.</summary>
		// Token: 0x04000425 RID: 1061
		Chocolate,
		/// <summary>A system-defined color.</summary>
		// Token: 0x04000426 RID: 1062
		Coral,
		/// <summary>A system-defined color.</summary>
		// Token: 0x04000427 RID: 1063
		CornflowerBlue,
		/// <summary>A system-defined color.</summary>
		// Token: 0x04000428 RID: 1064
		Cornsilk,
		/// <summary>A system-defined color.</summary>
		// Token: 0x04000429 RID: 1065
		Crimson,
		/// <summary>A system-defined color.</summary>
		// Token: 0x0400042A RID: 1066
		Cyan,
		/// <summary>A system-defined color.</summary>
		// Token: 0x0400042B RID: 1067
		DarkBlue,
		/// <summary>A system-defined color.</summary>
		// Token: 0x0400042C RID: 1068
		DarkCyan,
		/// <summary>A system-defined color.</summary>
		// Token: 0x0400042D RID: 1069
		DarkGoldenrod,
		/// <summary>A system-defined color.</summary>
		// Token: 0x0400042E RID: 1070
		DarkGray,
		/// <summary>A system-defined color.</summary>
		// Token: 0x0400042F RID: 1071
		DarkGreen,
		/// <summary>A system-defined color.</summary>
		// Token: 0x04000430 RID: 1072
		DarkKhaki,
		/// <summary>A system-defined color.</summary>
		// Token: 0x04000431 RID: 1073
		DarkMagenta,
		/// <summary>A system-defined color.</summary>
		// Token: 0x04000432 RID: 1074
		DarkOliveGreen,
		/// <summary>A system-defined color.</summary>
		// Token: 0x04000433 RID: 1075
		DarkOrange,
		/// <summary>A system-defined color.</summary>
		// Token: 0x04000434 RID: 1076
		DarkOrchid,
		/// <summary>A system-defined color.</summary>
		// Token: 0x04000435 RID: 1077
		DarkRed,
		/// <summary>A system-defined color.</summary>
		// Token: 0x04000436 RID: 1078
		DarkSalmon,
		/// <summary>A system-defined color.</summary>
		// Token: 0x04000437 RID: 1079
		DarkSeaGreen,
		/// <summary>A system-defined color.</summary>
		// Token: 0x04000438 RID: 1080
		DarkSlateBlue,
		/// <summary>A system-defined color.</summary>
		// Token: 0x04000439 RID: 1081
		DarkSlateGray,
		/// <summary>A system-defined color.</summary>
		// Token: 0x0400043A RID: 1082
		DarkTurquoise,
		/// <summary>A system-defined color.</summary>
		// Token: 0x0400043B RID: 1083
		DarkViolet,
		/// <summary>A system-defined color.</summary>
		// Token: 0x0400043C RID: 1084
		DeepPink,
		/// <summary>A system-defined color.</summary>
		// Token: 0x0400043D RID: 1085
		DeepSkyBlue,
		/// <summary>A system-defined color.</summary>
		// Token: 0x0400043E RID: 1086
		DimGray,
		/// <summary>A system-defined color.</summary>
		// Token: 0x0400043F RID: 1087
		DodgerBlue,
		/// <summary>A system-defined color.</summary>
		// Token: 0x04000440 RID: 1088
		Firebrick,
		/// <summary>A system-defined color.</summary>
		// Token: 0x04000441 RID: 1089
		FloralWhite,
		/// <summary>A system-defined color.</summary>
		// Token: 0x04000442 RID: 1090
		ForestGreen,
		/// <summary>A system-defined color.</summary>
		// Token: 0x04000443 RID: 1091
		Fuchsia,
		/// <summary>A system-defined color.</summary>
		// Token: 0x04000444 RID: 1092
		Gainsboro,
		/// <summary>A system-defined color.</summary>
		// Token: 0x04000445 RID: 1093
		GhostWhite,
		/// <summary>A system-defined color.</summary>
		// Token: 0x04000446 RID: 1094
		Gold,
		/// <summary>A system-defined color.</summary>
		// Token: 0x04000447 RID: 1095
		Goldenrod,
		/// <summary>A system-defined color.</summary>
		// Token: 0x04000448 RID: 1096
		Gray,
		/// <summary>A system-defined color.</summary>
		// Token: 0x04000449 RID: 1097
		Green,
		/// <summary>A system-defined color.</summary>
		// Token: 0x0400044A RID: 1098
		GreenYellow,
		/// <summary>A system-defined color.</summary>
		// Token: 0x0400044B RID: 1099
		Honeydew,
		/// <summary>A system-defined color.</summary>
		// Token: 0x0400044C RID: 1100
		HotPink,
		/// <summary>A system-defined color.</summary>
		// Token: 0x0400044D RID: 1101
		IndianRed,
		/// <summary>A system-defined color.</summary>
		// Token: 0x0400044E RID: 1102
		Indigo,
		/// <summary>A system-defined color.</summary>
		// Token: 0x0400044F RID: 1103
		Ivory,
		/// <summary>A system-defined color.</summary>
		// Token: 0x04000450 RID: 1104
		Khaki,
		/// <summary>A system-defined color.</summary>
		// Token: 0x04000451 RID: 1105
		Lavender,
		/// <summary>A system-defined color.</summary>
		// Token: 0x04000452 RID: 1106
		LavenderBlush,
		/// <summary>A system-defined color.</summary>
		// Token: 0x04000453 RID: 1107
		LawnGreen,
		/// <summary>A system-defined color.</summary>
		// Token: 0x04000454 RID: 1108
		LemonChiffon,
		/// <summary>A system-defined color.</summary>
		// Token: 0x04000455 RID: 1109
		LightBlue,
		/// <summary>A system-defined color.</summary>
		// Token: 0x04000456 RID: 1110
		LightCoral,
		/// <summary>A system-defined color.</summary>
		// Token: 0x04000457 RID: 1111
		LightCyan,
		/// <summary>A system-defined color.</summary>
		// Token: 0x04000458 RID: 1112
		LightGoldenrodYellow,
		/// <summary>A system-defined color.</summary>
		// Token: 0x04000459 RID: 1113
		LightGray,
		/// <summary>A system-defined color.</summary>
		// Token: 0x0400045A RID: 1114
		LightGreen,
		/// <summary>A system-defined color.</summary>
		// Token: 0x0400045B RID: 1115
		LightPink,
		/// <summary>A system-defined color.</summary>
		// Token: 0x0400045C RID: 1116
		LightSalmon,
		/// <summary>A system-defined color.</summary>
		// Token: 0x0400045D RID: 1117
		LightSeaGreen,
		/// <summary>A system-defined color.</summary>
		// Token: 0x0400045E RID: 1118
		LightSkyBlue,
		/// <summary>A system-defined color.</summary>
		// Token: 0x0400045F RID: 1119
		LightSlateGray,
		/// <summary>A system-defined color.</summary>
		// Token: 0x04000460 RID: 1120
		LightSteelBlue,
		/// <summary>A system-defined color.</summary>
		// Token: 0x04000461 RID: 1121
		LightYellow,
		/// <summary>A system-defined color.</summary>
		// Token: 0x04000462 RID: 1122
		Lime,
		/// <summary>A system-defined color.</summary>
		// Token: 0x04000463 RID: 1123
		LimeGreen,
		/// <summary>A system-defined color.</summary>
		// Token: 0x04000464 RID: 1124
		Linen,
		/// <summary>A system-defined color.</summary>
		// Token: 0x04000465 RID: 1125
		Magenta,
		/// <summary>A system-defined color.</summary>
		// Token: 0x04000466 RID: 1126
		Maroon,
		/// <summary>A system-defined color.</summary>
		// Token: 0x04000467 RID: 1127
		MediumAquamarine,
		/// <summary>A system-defined color.</summary>
		// Token: 0x04000468 RID: 1128
		MediumBlue,
		/// <summary>A system-defined color.</summary>
		// Token: 0x04000469 RID: 1129
		MediumOrchid,
		/// <summary>A system-defined color.</summary>
		// Token: 0x0400046A RID: 1130
		MediumPurple,
		/// <summary>A system-defined color.</summary>
		// Token: 0x0400046B RID: 1131
		MediumSeaGreen,
		/// <summary>A system-defined color.</summary>
		// Token: 0x0400046C RID: 1132
		MediumSlateBlue,
		/// <summary>A system-defined color.</summary>
		// Token: 0x0400046D RID: 1133
		MediumSpringGreen,
		/// <summary>A system-defined color.</summary>
		// Token: 0x0400046E RID: 1134
		MediumTurquoise,
		/// <summary>A system-defined color.</summary>
		// Token: 0x0400046F RID: 1135
		MediumVioletRed,
		/// <summary>A system-defined color.</summary>
		// Token: 0x04000470 RID: 1136
		MidnightBlue,
		/// <summary>A system-defined color.</summary>
		// Token: 0x04000471 RID: 1137
		MintCream,
		/// <summary>A system-defined color.</summary>
		// Token: 0x04000472 RID: 1138
		MistyRose,
		/// <summary>A system-defined color.</summary>
		// Token: 0x04000473 RID: 1139
		Moccasin,
		/// <summary>A system-defined color.</summary>
		// Token: 0x04000474 RID: 1140
		NavajoWhite,
		/// <summary>A system-defined color.</summary>
		// Token: 0x04000475 RID: 1141
		Navy,
		/// <summary>A system-defined color.</summary>
		// Token: 0x04000476 RID: 1142
		OldLace,
		/// <summary>A system-defined color.</summary>
		// Token: 0x04000477 RID: 1143
		Olive,
		/// <summary>A system-defined color.</summary>
		// Token: 0x04000478 RID: 1144
		OliveDrab,
		/// <summary>A system-defined color.</summary>
		// Token: 0x04000479 RID: 1145
		Orange,
		/// <summary>A system-defined color.</summary>
		// Token: 0x0400047A RID: 1146
		OrangeRed,
		/// <summary>A system-defined color.</summary>
		// Token: 0x0400047B RID: 1147
		Orchid,
		/// <summary>A system-defined color.</summary>
		// Token: 0x0400047C RID: 1148
		PaleGoldenrod,
		/// <summary>A system-defined color.</summary>
		// Token: 0x0400047D RID: 1149
		PaleGreen,
		/// <summary>A system-defined color.</summary>
		// Token: 0x0400047E RID: 1150
		PaleTurquoise,
		/// <summary>A system-defined color.</summary>
		// Token: 0x0400047F RID: 1151
		PaleVioletRed,
		/// <summary>A system-defined color.</summary>
		// Token: 0x04000480 RID: 1152
		PapayaWhip,
		/// <summary>A system-defined color.</summary>
		// Token: 0x04000481 RID: 1153
		PeachPuff,
		/// <summary>A system-defined color.</summary>
		// Token: 0x04000482 RID: 1154
		Peru,
		/// <summary>A system-defined color.</summary>
		// Token: 0x04000483 RID: 1155
		Pink,
		/// <summary>A system-defined color.</summary>
		// Token: 0x04000484 RID: 1156
		Plum,
		/// <summary>A system-defined color.</summary>
		// Token: 0x04000485 RID: 1157
		PowderBlue,
		/// <summary>A system-defined color.</summary>
		// Token: 0x04000486 RID: 1158
		Purple,
		/// <summary>A system-defined color.</summary>
		// Token: 0x04000487 RID: 1159
		Red,
		/// <summary>A system-defined color.</summary>
		// Token: 0x04000488 RID: 1160
		RosyBrown,
		/// <summary>A system-defined color.</summary>
		// Token: 0x04000489 RID: 1161
		RoyalBlue,
		/// <summary>A system-defined color.</summary>
		// Token: 0x0400048A RID: 1162
		SaddleBrown,
		/// <summary>A system-defined color.</summary>
		// Token: 0x0400048B RID: 1163
		Salmon,
		/// <summary>A system-defined color.</summary>
		// Token: 0x0400048C RID: 1164
		SandyBrown,
		/// <summary>A system-defined color.</summary>
		// Token: 0x0400048D RID: 1165
		SeaGreen,
		/// <summary>A system-defined color.</summary>
		// Token: 0x0400048E RID: 1166
		SeaShell,
		/// <summary>A system-defined color.</summary>
		// Token: 0x0400048F RID: 1167
		Sienna,
		/// <summary>A system-defined color.</summary>
		// Token: 0x04000490 RID: 1168
		Silver,
		/// <summary>A system-defined color.</summary>
		// Token: 0x04000491 RID: 1169
		SkyBlue,
		/// <summary>A system-defined color.</summary>
		// Token: 0x04000492 RID: 1170
		SlateBlue,
		/// <summary>A system-defined color.</summary>
		// Token: 0x04000493 RID: 1171
		SlateGray,
		/// <summary>A system-defined color.</summary>
		// Token: 0x04000494 RID: 1172
		Snow,
		/// <summary>A system-defined color.</summary>
		// Token: 0x04000495 RID: 1173
		SpringGreen,
		/// <summary>A system-defined color.</summary>
		// Token: 0x04000496 RID: 1174
		SteelBlue,
		/// <summary>A system-defined color.</summary>
		// Token: 0x04000497 RID: 1175
		Tan,
		/// <summary>A system-defined color.</summary>
		// Token: 0x04000498 RID: 1176
		Teal,
		/// <summary>A system-defined color.</summary>
		// Token: 0x04000499 RID: 1177
		Thistle,
		/// <summary>A system-defined color.</summary>
		// Token: 0x0400049A RID: 1178
		Tomato,
		/// <summary>A system-defined color.</summary>
		// Token: 0x0400049B RID: 1179
		Turquoise,
		/// <summary>A system-defined color.</summary>
		// Token: 0x0400049C RID: 1180
		Violet,
		/// <summary>A system-defined color.</summary>
		// Token: 0x0400049D RID: 1181
		Wheat,
		/// <summary>A system-defined color.</summary>
		// Token: 0x0400049E RID: 1182
		White,
		/// <summary>A system-defined color.</summary>
		// Token: 0x0400049F RID: 1183
		WhiteSmoke,
		/// <summary>A system-defined color.</summary>
		// Token: 0x040004A0 RID: 1184
		Yellow,
		/// <summary>A system-defined color.</summary>
		// Token: 0x040004A1 RID: 1185
		YellowGreen,
		/// <summary>The system-defined face color of a 3-D element.</summary>
		// Token: 0x040004A2 RID: 1186
		ButtonFace,
		/// <summary>The system-defined color that is the highlight color of a 3-D element. This color is applied to parts of a 3-D element that face the light source.</summary>
		// Token: 0x040004A3 RID: 1187
		ButtonHighlight,
		/// <summary>The system-defined color that is the shadow color of a 3-D element. This color is applied to parts of a 3-D element that face away from the light source.</summary>
		// Token: 0x040004A4 RID: 1188
		ButtonShadow,
		/// <summary>The system-defined color of the lightest color in the color gradient of an active window's title bar.</summary>
		// Token: 0x040004A5 RID: 1189
		GradientActiveCaption,
		/// <summary>The system-defined color of the lightest color in the color gradient of an inactive window's title bar. </summary>
		// Token: 0x040004A6 RID: 1190
		GradientInactiveCaption,
		/// <summary>The system-defined color of the background of a menu bar.</summary>
		// Token: 0x040004A7 RID: 1191
		MenuBar,
		/// <summary>The system-defined color used to highlight menu items when the menu appears as a flat menu.</summary>
		// Token: 0x040004A8 RID: 1192
		MenuHighlight
	}
}
