using System;

namespace System
{
	/// <summary>Specifies the standard keys on a console.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x0200013D RID: 317
	[Serializable]
	public enum ConsoleKey
	{
		/// <summary>The BACKSPACE key.</summary>
		// Token: 0x040007E9 RID: 2025
		Backspace = 8,
		/// <summary>The TAB key.</summary>
		// Token: 0x040007EA RID: 2026
		Tab,
		/// <summary>The CLEAR key.</summary>
		// Token: 0x040007EB RID: 2027
		Clear = 12,
		/// <summary>The ENTER key.</summary>
		// Token: 0x040007EC RID: 2028
		Enter,
		/// <summary>The PAUSE key.</summary>
		// Token: 0x040007ED RID: 2029
		Pause = 19,
		/// <summary>The ESC (ESCAPE) key.</summary>
		// Token: 0x040007EE RID: 2030
		Escape = 27,
		/// <summary>The SPACEBAR key.</summary>
		// Token: 0x040007EF RID: 2031
		Spacebar = 32,
		/// <summary>The PAGE UP key.</summary>
		// Token: 0x040007F0 RID: 2032
		PageUp,
		/// <summary>The PAGE DOWN key.</summary>
		// Token: 0x040007F1 RID: 2033
		PageDown,
		/// <summary>The END key.</summary>
		// Token: 0x040007F2 RID: 2034
		End,
		/// <summary>The HOME key.</summary>
		// Token: 0x040007F3 RID: 2035
		Home,
		/// <summary>The LEFT ARROW key.</summary>
		// Token: 0x040007F4 RID: 2036
		LeftArrow,
		/// <summary>The UP ARROW key.</summary>
		// Token: 0x040007F5 RID: 2037
		UpArrow,
		/// <summary>The RIGHT ARROW key.</summary>
		// Token: 0x040007F6 RID: 2038
		RightArrow,
		/// <summary>The DOWN ARROW key.</summary>
		// Token: 0x040007F7 RID: 2039
		DownArrow,
		/// <summary>The SELECT key.</summary>
		// Token: 0x040007F8 RID: 2040
		Select,
		/// <summary>The PRINT key.</summary>
		// Token: 0x040007F9 RID: 2041
		Print,
		/// <summary>The EXECUTE key.</summary>
		// Token: 0x040007FA RID: 2042
		Execute,
		/// <summary>The PRINT SCREEN key.</summary>
		// Token: 0x040007FB RID: 2043
		PrintScreen,
		/// <summary>The INS (INSERT) key.</summary>
		// Token: 0x040007FC RID: 2044
		Insert,
		/// <summary>The DEL (DELETE) key.</summary>
		// Token: 0x040007FD RID: 2045
		Delete,
		/// <summary>The HELP key.</summary>
		// Token: 0x040007FE RID: 2046
		Help,
		/// <summary>The 0 key.</summary>
		// Token: 0x040007FF RID: 2047
		D0,
		/// <summary>The 1 key.</summary>
		// Token: 0x04000800 RID: 2048
		D1,
		/// <summary>The 2 key.</summary>
		// Token: 0x04000801 RID: 2049
		D2,
		/// <summary>The 3 key.</summary>
		// Token: 0x04000802 RID: 2050
		D3,
		/// <summary>The 4 key.</summary>
		// Token: 0x04000803 RID: 2051
		D4,
		/// <summary>The 5 key.</summary>
		// Token: 0x04000804 RID: 2052
		D5,
		/// <summary>The 6 key.</summary>
		// Token: 0x04000805 RID: 2053
		D6,
		/// <summary>The 7 key.</summary>
		// Token: 0x04000806 RID: 2054
		D7,
		/// <summary>The 8 key.</summary>
		// Token: 0x04000807 RID: 2055
		D8,
		/// <summary>The 9 key.</summary>
		// Token: 0x04000808 RID: 2056
		D9,
		/// <summary>The A key.</summary>
		// Token: 0x04000809 RID: 2057
		A = 65,
		/// <summary>The B key.</summary>
		// Token: 0x0400080A RID: 2058
		B,
		/// <summary>The C key.</summary>
		// Token: 0x0400080B RID: 2059
		C,
		/// <summary>The D key.</summary>
		// Token: 0x0400080C RID: 2060
		D,
		/// <summary>The E key.</summary>
		// Token: 0x0400080D RID: 2061
		E,
		/// <summary>The F key.</summary>
		// Token: 0x0400080E RID: 2062
		F,
		/// <summary>The G key.</summary>
		// Token: 0x0400080F RID: 2063
		G,
		/// <summary>The H key.</summary>
		// Token: 0x04000810 RID: 2064
		H,
		/// <summary>The I key.</summary>
		// Token: 0x04000811 RID: 2065
		I,
		/// <summary>The J key.</summary>
		// Token: 0x04000812 RID: 2066
		J,
		/// <summary>The K key.</summary>
		// Token: 0x04000813 RID: 2067
		K,
		/// <summary>The L key.</summary>
		// Token: 0x04000814 RID: 2068
		L,
		/// <summary>The M key.</summary>
		// Token: 0x04000815 RID: 2069
		M,
		/// <summary>The N key.</summary>
		// Token: 0x04000816 RID: 2070
		N,
		/// <summary>The O key.</summary>
		// Token: 0x04000817 RID: 2071
		O,
		/// <summary>The P key.</summary>
		// Token: 0x04000818 RID: 2072
		P,
		/// <summary>The Q key.</summary>
		// Token: 0x04000819 RID: 2073
		Q,
		/// <summary>The R key.</summary>
		// Token: 0x0400081A RID: 2074
		R,
		/// <summary>The S key.</summary>
		// Token: 0x0400081B RID: 2075
		S,
		/// <summary>The T key.</summary>
		// Token: 0x0400081C RID: 2076
		T,
		/// <summary>The U key.</summary>
		// Token: 0x0400081D RID: 2077
		U,
		/// <summary>The V key.</summary>
		// Token: 0x0400081E RID: 2078
		V,
		/// <summary>The W key.</summary>
		// Token: 0x0400081F RID: 2079
		W,
		/// <summary>The X key.</summary>
		// Token: 0x04000820 RID: 2080
		X,
		/// <summary>The Y key.</summary>
		// Token: 0x04000821 RID: 2081
		Y,
		/// <summary>The Z key.</summary>
		// Token: 0x04000822 RID: 2082
		Z,
		/// <summary>The left Windows logo key (Microsoft Natural Keyboard).</summary>
		// Token: 0x04000823 RID: 2083
		LeftWindows,
		/// <summary>The right Windows logo key (Microsoft Natural Keyboard).</summary>
		// Token: 0x04000824 RID: 2084
		RightWindows,
		/// <summary>The Application key (Microsoft Natural Keyboard).</summary>
		// Token: 0x04000825 RID: 2085
		Applications,
		/// <summary>The Computer Sleep key.</summary>
		// Token: 0x04000826 RID: 2086
		Sleep = 95,
		/// <summary>The 0 key on the numeric keypad.</summary>
		// Token: 0x04000827 RID: 2087
		NumPad0,
		/// <summary>The 1 key on the numeric keypad.</summary>
		// Token: 0x04000828 RID: 2088
		NumPad1,
		/// <summary>The 2 key on the numeric keypad.</summary>
		// Token: 0x04000829 RID: 2089
		NumPad2,
		/// <summary>The 3 key on the numeric keypad.</summary>
		// Token: 0x0400082A RID: 2090
		NumPad3,
		/// <summary>The 4 key on the numeric keypad.</summary>
		// Token: 0x0400082B RID: 2091
		NumPad4,
		/// <summary>The 5 key on the numeric keypad.</summary>
		// Token: 0x0400082C RID: 2092
		NumPad5,
		/// <summary>The 6 key on the numeric keypad.</summary>
		// Token: 0x0400082D RID: 2093
		NumPad6,
		/// <summary>The 7 key on the numeric keypad.</summary>
		// Token: 0x0400082E RID: 2094
		NumPad7,
		/// <summary>The 8 key on the numeric keypad.</summary>
		// Token: 0x0400082F RID: 2095
		NumPad8,
		/// <summary>The 9 key on the numeric keypad.</summary>
		// Token: 0x04000830 RID: 2096
		NumPad9,
		/// <summary>The Multiply key (the multiplication key on the numeric keypad).</summary>
		// Token: 0x04000831 RID: 2097
		Multiply,
		/// <summary>The Add key (the addition key on the numeric keypad).</summary>
		// Token: 0x04000832 RID: 2098
		Add,
		/// <summary>The Separator key.</summary>
		// Token: 0x04000833 RID: 2099
		Separator,
		/// <summary>The Subtract key (the subtraction key on the numeric keypad).</summary>
		// Token: 0x04000834 RID: 2100
		Subtract,
		/// <summary>The Decimal key (the decimal key on the numeric keypad). </summary>
		// Token: 0x04000835 RID: 2101
		Decimal,
		/// <summary>The Divide key (the division key on the numeric keypad). </summary>
		// Token: 0x04000836 RID: 2102
		Divide,
		/// <summary>The F1 key.</summary>
		// Token: 0x04000837 RID: 2103
		F1,
		/// <summary>The F2 key.</summary>
		// Token: 0x04000838 RID: 2104
		F2,
		/// <summary>The F3 key.</summary>
		// Token: 0x04000839 RID: 2105
		F3,
		/// <summary>The F4 key.</summary>
		// Token: 0x0400083A RID: 2106
		F4,
		/// <summary>The F5 key.</summary>
		// Token: 0x0400083B RID: 2107
		F5,
		/// <summary>The F6 key.</summary>
		// Token: 0x0400083C RID: 2108
		F6,
		/// <summary>The F7 key.</summary>
		// Token: 0x0400083D RID: 2109
		F7,
		/// <summary>The F8 key.</summary>
		// Token: 0x0400083E RID: 2110
		F8,
		/// <summary>The F9 key.</summary>
		// Token: 0x0400083F RID: 2111
		F9,
		/// <summary>The F10 key.</summary>
		// Token: 0x04000840 RID: 2112
		F10,
		/// <summary>The F11 key.</summary>
		// Token: 0x04000841 RID: 2113
		F11,
		/// <summary>The F12 key.</summary>
		// Token: 0x04000842 RID: 2114
		F12,
		/// <summary>The F13 key.</summary>
		// Token: 0x04000843 RID: 2115
		F13,
		/// <summary>The F14 key.</summary>
		// Token: 0x04000844 RID: 2116
		F14,
		/// <summary>The F15 key.</summary>
		// Token: 0x04000845 RID: 2117
		F15,
		/// <summary>The F16 key.</summary>
		// Token: 0x04000846 RID: 2118
		F16,
		/// <summary>The F17 key.</summary>
		// Token: 0x04000847 RID: 2119
		F17,
		/// <summary>The F18 key.</summary>
		// Token: 0x04000848 RID: 2120
		F18,
		/// <summary>The F19 key.</summary>
		// Token: 0x04000849 RID: 2121
		F19,
		/// <summary>The F20 key.</summary>
		// Token: 0x0400084A RID: 2122
		F20,
		/// <summary>The F21 key.</summary>
		// Token: 0x0400084B RID: 2123
		F21,
		/// <summary>The F22 key.</summary>
		// Token: 0x0400084C RID: 2124
		F22,
		/// <summary>The F23 key.</summary>
		// Token: 0x0400084D RID: 2125
		F23,
		/// <summary>The F24 key.</summary>
		// Token: 0x0400084E RID: 2126
		F24,
		/// <summary>The Browser Back key (Windows 2000 or later).</summary>
		// Token: 0x0400084F RID: 2127
		BrowserBack = 166,
		/// <summary>The Browser Forward key (Windows 2000 or later).</summary>
		// Token: 0x04000850 RID: 2128
		BrowserForward,
		/// <summary>The Browser Refresh key (Windows 2000 or later).</summary>
		// Token: 0x04000851 RID: 2129
		BrowserRefresh,
		/// <summary>The Browser Stop key (Windows 2000 or later).</summary>
		// Token: 0x04000852 RID: 2130
		BrowserStop,
		/// <summary>The Browser Search key (Windows 2000 or later).</summary>
		// Token: 0x04000853 RID: 2131
		BrowserSearch,
		/// <summary>The Browser Favorites key (Windows 2000 or later).</summary>
		// Token: 0x04000854 RID: 2132
		BrowserFavorites,
		/// <summary>The Browser Home key (Windows 2000 or later).</summary>
		// Token: 0x04000855 RID: 2133
		BrowserHome,
		/// <summary>The Volume Mute key (Microsoft Natural Keyboard, Windows 2000 or later).</summary>
		// Token: 0x04000856 RID: 2134
		VolumeMute,
		/// <summary>The Volume Down key (Microsoft Natural Keyboard, Windows 2000 or later).</summary>
		// Token: 0x04000857 RID: 2135
		VolumeDown,
		/// <summary>The Volume Up key (Microsoft Natural Keyboard, Windows 2000 or later).</summary>
		// Token: 0x04000858 RID: 2136
		VolumeUp,
		/// <summary>The Media Next Track key (Windows 2000 or later).</summary>
		// Token: 0x04000859 RID: 2137
		MediaNext,
		/// <summary>The Media Previous Track key (Windows 2000 or later).</summary>
		// Token: 0x0400085A RID: 2138
		MediaPrevious,
		/// <summary>The Media Stop key (Windows 2000 or later).</summary>
		// Token: 0x0400085B RID: 2139
		MediaStop,
		/// <summary>The Media Play/Pause key (Windows 2000 or later).</summary>
		// Token: 0x0400085C RID: 2140
		MediaPlay,
		/// <summary>The Start Mail key (Microsoft Natural Keyboard, Windows 2000 or later).</summary>
		// Token: 0x0400085D RID: 2141
		LaunchMail,
		/// <summary>The Select Media key (Microsoft Natural Keyboard, Windows 2000 or later).</summary>
		// Token: 0x0400085E RID: 2142
		LaunchMediaSelect,
		/// <summary>The Start Application 1 key (Microsoft Natural Keyboard, Windows 2000 or later).</summary>
		// Token: 0x0400085F RID: 2143
		LaunchApp1,
		/// <summary>The Start Application 2 key (Microsoft Natural Keyboard, Windows 2000 or later).</summary>
		// Token: 0x04000860 RID: 2144
		LaunchApp2,
		/// <summary>The OEM 1 key (OEM specific).</summary>
		// Token: 0x04000861 RID: 2145
		Oem1 = 186,
		/// <summary>The OEM Plus key on any country/region keyboard (Windows 2000 or later).</summary>
		// Token: 0x04000862 RID: 2146
		OemPlus,
		/// <summary>The OEM Comma key on any country/region keyboard (Windows 2000 or later).</summary>
		// Token: 0x04000863 RID: 2147
		OemComma,
		/// <summary>The OEM Minus key on any country/region keyboard (Windows 2000 or later).</summary>
		// Token: 0x04000864 RID: 2148
		OemMinus,
		/// <summary>The OEM Period key on any country/region keyboard (Windows 2000 or later).</summary>
		// Token: 0x04000865 RID: 2149
		OemPeriod,
		/// <summary>The OEM 2 key (OEM specific).</summary>
		// Token: 0x04000866 RID: 2150
		Oem2,
		/// <summary>The OEM 3 key (OEM specific).</summary>
		// Token: 0x04000867 RID: 2151
		Oem3,
		/// <summary>The OEM 4 key (OEM specific).</summary>
		// Token: 0x04000868 RID: 2152
		Oem4 = 219,
		/// <summary>The OEM 5 (OEM specific).</summary>
		// Token: 0x04000869 RID: 2153
		Oem5,
		/// <summary>The OEM 6 key (OEM specific).</summary>
		// Token: 0x0400086A RID: 2154
		Oem6,
		/// <summary>The OEM 7 key (OEM specific).</summary>
		// Token: 0x0400086B RID: 2155
		Oem7,
		/// <summary>The OEM 8 key (OEM specific).</summary>
		// Token: 0x0400086C RID: 2156
		Oem8,
		/// <summary>The OEM 102 key (OEM specific).</summary>
		// Token: 0x0400086D RID: 2157
		Oem102 = 226,
		/// <summary>The IME PROCESS key.</summary>
		// Token: 0x0400086E RID: 2158
		Process = 229,
		/// <summary>The PACKET key (used to pass Unicode characters with keystrokes).</summary>
		// Token: 0x0400086F RID: 2159
		Packet = 231,
		/// <summary>The ATTN key.</summary>
		// Token: 0x04000870 RID: 2160
		Attention = 246,
		/// <summary>The CRSEL (CURSOR SELECT) key.</summary>
		// Token: 0x04000871 RID: 2161
		CrSel,
		/// <summary>The EXSEL (EXTEND SELECTION) key.</summary>
		// Token: 0x04000872 RID: 2162
		ExSel,
		/// <summary>The ERASE EOF key.</summary>
		// Token: 0x04000873 RID: 2163
		EraseEndOfFile,
		/// <summary>The PLAY key.</summary>
		// Token: 0x04000874 RID: 2164
		Play,
		/// <summary>The ZOOM key.</summary>
		// Token: 0x04000875 RID: 2165
		Zoom,
		/// <summary>A constant reserved for future use.</summary>
		// Token: 0x04000876 RID: 2166
		NoName,
		/// <summary>The PA1 key.</summary>
		// Token: 0x04000877 RID: 2167
		Pa1,
		/// <summary>The CLEAR key (OEM specific).</summary>
		// Token: 0x04000878 RID: 2168
		OemClear
	}
}
