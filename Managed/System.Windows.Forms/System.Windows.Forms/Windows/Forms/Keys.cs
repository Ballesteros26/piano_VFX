using System;
using System.ComponentModel;
using System.Runtime.InteropServices;

namespace System.Windows.Forms
{
	/// <summary>Specifies key codes and modifiers.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020001FA RID: 506
	[ComVisible(true)]
	[Editor("System.Windows.Forms.Design.ShortcutKeysEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[TypeConverter(typeof(KeysConverter))]
	[Flags]
	public enum Keys
	{
		/// <summary>No key pressed.</summary>
		// Token: 0x0400106A RID: 4202
		None = 0,
		/// <summary>The left mouse button.</summary>
		// Token: 0x0400106B RID: 4203
		LButton = 1,
		/// <summary>The right mouse button.</summary>
		// Token: 0x0400106C RID: 4204
		RButton = 2,
		/// <summary>The CANCEL key.</summary>
		// Token: 0x0400106D RID: 4205
		Cancel = 3,
		/// <summary>The middle mouse button (three-button mouse).</summary>
		// Token: 0x0400106E RID: 4206
		MButton = 4,
		/// <summary>The first x mouse button (five-button mouse).</summary>
		// Token: 0x0400106F RID: 4207
		XButton1 = 5,
		/// <summary>The second x mouse button (five-button mouse).</summary>
		// Token: 0x04001070 RID: 4208
		XButton2 = 6,
		/// <summary>The BACKSPACE key.</summary>
		// Token: 0x04001071 RID: 4209
		Back = 8,
		/// <summary>The TAB key.</summary>
		// Token: 0x04001072 RID: 4210
		Tab = 9,
		/// <summary>The LINEFEED key.</summary>
		// Token: 0x04001073 RID: 4211
		LineFeed = 10,
		/// <summary>The CLEAR key.</summary>
		// Token: 0x04001074 RID: 4212
		Clear = 12,
		/// <summary>The RETURN key.</summary>
		// Token: 0x04001075 RID: 4213
		Return = 13,
		/// <summary>The ENTER key.</summary>
		// Token: 0x04001076 RID: 4214
		Enter = 13,
		/// <summary>The SHIFT key.</summary>
		// Token: 0x04001077 RID: 4215
		ShiftKey = 16,
		/// <summary>The CTRL key.</summary>
		// Token: 0x04001078 RID: 4216
		ControlKey = 17,
		/// <summary>The ALT key.</summary>
		// Token: 0x04001079 RID: 4217
		Menu = 18,
		/// <summary>The PAUSE key.</summary>
		// Token: 0x0400107A RID: 4218
		Pause = 19,
		/// <summary>The CAPS LOCK key.</summary>
		// Token: 0x0400107B RID: 4219
		CapsLock = 20,
		/// <summary>The CAPS LOCK key.</summary>
		// Token: 0x0400107C RID: 4220
		Capital = 20,
		/// <summary>The IME Kana mode key.</summary>
		// Token: 0x0400107D RID: 4221
		KanaMode = 21,
		/// <summary>The IME Hanguel mode key. (maintained for compatibility; use HangulMode) </summary>
		// Token: 0x0400107E RID: 4222
		HanguelMode = 21,
		/// <summary>The IME Hangul mode key.</summary>
		// Token: 0x0400107F RID: 4223
		HangulMode = 21,
		/// <summary>The IME Junja mode key.</summary>
		// Token: 0x04001080 RID: 4224
		JunjaMode = 23,
		/// <summary>The IME final mode key.</summary>
		// Token: 0x04001081 RID: 4225
		FinalMode = 24,
		/// <summary>The IME Kanji mode key.</summary>
		// Token: 0x04001082 RID: 4226
		KanjiMode = 25,
		/// <summary>The IME Hanja mode key.</summary>
		// Token: 0x04001083 RID: 4227
		HanjaMode = 25,
		/// <summary>The ESC key.</summary>
		// Token: 0x04001084 RID: 4228
		Escape = 27,
		/// <summary>The IME convert key.</summary>
		// Token: 0x04001085 RID: 4229
		IMEConvert = 28,
		/// <summary>The IME nonconvert key.</summary>
		// Token: 0x04001086 RID: 4230
		IMENonconvert = 29,
		/// <summary>The IME accept key. Obsolete, use <see cref="F:System.Windows.Forms.Keys.IMEAccept" /> instead.</summary>
		// Token: 0x04001087 RID: 4231
		IMEAceept = 30,
		/// <summary>The IME mode change key.</summary>
		// Token: 0x04001088 RID: 4232
		IMEModeChange = 31,
		/// <summary>The SPACEBAR key.</summary>
		// Token: 0x04001089 RID: 4233
		Space = 32,
		/// <summary>The PAGE UP key.</summary>
		// Token: 0x0400108A RID: 4234
		PageUp = 33,
		/// <summary>The PAGE UP key.</summary>
		// Token: 0x0400108B RID: 4235
		Prior = 33,
		/// <summary>The PAGE DOWN key.</summary>
		// Token: 0x0400108C RID: 4236
		PageDown = 34,
		/// <summary>The PAGE DOWN key.</summary>
		// Token: 0x0400108D RID: 4237
		Next = 34,
		/// <summary>The END key.</summary>
		// Token: 0x0400108E RID: 4238
		End = 35,
		/// <summary>The HOME key.</summary>
		// Token: 0x0400108F RID: 4239
		Home = 36,
		/// <summary>The LEFT ARROW key.</summary>
		// Token: 0x04001090 RID: 4240
		Left = 37,
		/// <summary>The UP ARROW key.</summary>
		// Token: 0x04001091 RID: 4241
		Up = 38,
		/// <summary>The RIGHT ARROW key.</summary>
		// Token: 0x04001092 RID: 4242
		Right = 39,
		/// <summary>The DOWN ARROW key.</summary>
		// Token: 0x04001093 RID: 4243
		Down = 40,
		/// <summary>The SELECT key.</summary>
		// Token: 0x04001094 RID: 4244
		Select = 41,
		/// <summary>The PRINT key.</summary>
		// Token: 0x04001095 RID: 4245
		Print = 42,
		/// <summary>The EXECUTE key.</summary>
		// Token: 0x04001096 RID: 4246
		Execute = 43,
		/// <summary>The PRINT SCREEN key.</summary>
		// Token: 0x04001097 RID: 4247
		PrintScreen = 44,
		/// <summary>The PRINT SCREEN key.</summary>
		// Token: 0x04001098 RID: 4248
		Snapshot = 44,
		/// <summary>The INS key.</summary>
		// Token: 0x04001099 RID: 4249
		Insert = 45,
		/// <summary>The DEL key.</summary>
		// Token: 0x0400109A RID: 4250
		Delete = 46,
		/// <summary>The HELP key.</summary>
		// Token: 0x0400109B RID: 4251
		Help = 47,
		/// <summary>The 0 key.</summary>
		// Token: 0x0400109C RID: 4252
		D0 = 48,
		/// <summary>The 1 key.</summary>
		// Token: 0x0400109D RID: 4253
		D1 = 49,
		/// <summary>The 2 key.</summary>
		// Token: 0x0400109E RID: 4254
		D2 = 50,
		/// <summary>The 3 key.</summary>
		// Token: 0x0400109F RID: 4255
		D3 = 51,
		/// <summary>The 4 key.</summary>
		// Token: 0x040010A0 RID: 4256
		D4 = 52,
		/// <summary>The 5 key.</summary>
		// Token: 0x040010A1 RID: 4257
		D5 = 53,
		/// <summary>The 6 key.</summary>
		// Token: 0x040010A2 RID: 4258
		D6 = 54,
		/// <summary>The 7 key.</summary>
		// Token: 0x040010A3 RID: 4259
		D7 = 55,
		/// <summary>The 8 key.</summary>
		// Token: 0x040010A4 RID: 4260
		D8 = 56,
		/// <summary>The 9 key.</summary>
		// Token: 0x040010A5 RID: 4261
		D9 = 57,
		/// <summary>The A key.</summary>
		// Token: 0x040010A6 RID: 4262
		A = 65,
		/// <summary>The B key.</summary>
		// Token: 0x040010A7 RID: 4263
		B = 66,
		/// <summary>The C key.</summary>
		// Token: 0x040010A8 RID: 4264
		C = 67,
		/// <summary>The D key.</summary>
		// Token: 0x040010A9 RID: 4265
		D = 68,
		/// <summary>The E key.</summary>
		// Token: 0x040010AA RID: 4266
		E = 69,
		/// <summary>The F key.</summary>
		// Token: 0x040010AB RID: 4267
		F = 70,
		/// <summary>The G key.</summary>
		// Token: 0x040010AC RID: 4268
		G = 71,
		/// <summary>The H key.</summary>
		// Token: 0x040010AD RID: 4269
		H = 72,
		/// <summary>The I key.</summary>
		// Token: 0x040010AE RID: 4270
		I = 73,
		/// <summary>The J key.</summary>
		// Token: 0x040010AF RID: 4271
		J = 74,
		/// <summary>The K key.</summary>
		// Token: 0x040010B0 RID: 4272
		K = 75,
		/// <summary>The L key.</summary>
		// Token: 0x040010B1 RID: 4273
		L = 76,
		/// <summary>The M key.</summary>
		// Token: 0x040010B2 RID: 4274
		M = 77,
		/// <summary>The N key.</summary>
		// Token: 0x040010B3 RID: 4275
		N = 78,
		/// <summary>The O key.</summary>
		// Token: 0x040010B4 RID: 4276
		O = 79,
		/// <summary>The P key.</summary>
		// Token: 0x040010B5 RID: 4277
		P = 80,
		/// <summary>The Q key.</summary>
		// Token: 0x040010B6 RID: 4278
		Q = 81,
		/// <summary>The R key.</summary>
		// Token: 0x040010B7 RID: 4279
		R = 82,
		/// <summary>The S key.</summary>
		// Token: 0x040010B8 RID: 4280
		S = 83,
		/// <summary>The T key.</summary>
		// Token: 0x040010B9 RID: 4281
		T = 84,
		/// <summary>The U key.</summary>
		// Token: 0x040010BA RID: 4282
		U = 85,
		/// <summary>The V key.</summary>
		// Token: 0x040010BB RID: 4283
		V = 86,
		/// <summary>The W key.</summary>
		// Token: 0x040010BC RID: 4284
		W = 87,
		/// <summary>The X key.</summary>
		// Token: 0x040010BD RID: 4285
		X = 88,
		/// <summary>The Y key.</summary>
		// Token: 0x040010BE RID: 4286
		Y = 89,
		/// <summary>The Z key.</summary>
		// Token: 0x040010BF RID: 4287
		Z = 90,
		/// <summary>The left Windows logo key (Microsoft Natural Keyboard).</summary>
		// Token: 0x040010C0 RID: 4288
		LWin = 91,
		/// <summary>The right Windows logo key (Microsoft Natural Keyboard).</summary>
		// Token: 0x040010C1 RID: 4289
		RWin = 92,
		/// <summary>The application key (Microsoft Natural Keyboard).</summary>
		// Token: 0x040010C2 RID: 4290
		Apps = 93,
		/// <summary>The 0 key on the numeric keypad.</summary>
		// Token: 0x040010C3 RID: 4291
		NumPad0 = 96,
		/// <summary>The 1 key on the numeric keypad.</summary>
		// Token: 0x040010C4 RID: 4292
		NumPad1 = 97,
		/// <summary>The 2 key on the numeric keypad.</summary>
		// Token: 0x040010C5 RID: 4293
		NumPad2 = 98,
		/// <summary>The 3 key on the numeric keypad.</summary>
		// Token: 0x040010C6 RID: 4294
		NumPad3 = 99,
		/// <summary>The 4 key on the numeric keypad.</summary>
		// Token: 0x040010C7 RID: 4295
		NumPad4 = 100,
		/// <summary>The 5 key on the numeric keypad.</summary>
		// Token: 0x040010C8 RID: 4296
		NumPad5 = 101,
		/// <summary>The 6 key on the numeric keypad.</summary>
		// Token: 0x040010C9 RID: 4297
		NumPad6 = 102,
		/// <summary>The 7 key on the numeric keypad.</summary>
		// Token: 0x040010CA RID: 4298
		NumPad7 = 103,
		/// <summary>The 8 key on the numeric keypad.</summary>
		// Token: 0x040010CB RID: 4299
		NumPad8 = 104,
		/// <summary>The 9 key on the numeric keypad.</summary>
		// Token: 0x040010CC RID: 4300
		NumPad9 = 105,
		/// <summary>The multiply key.</summary>
		// Token: 0x040010CD RID: 4301
		Multiply = 106,
		/// <summary>The add key.</summary>
		// Token: 0x040010CE RID: 4302
		Add = 107,
		/// <summary>The separator key.</summary>
		// Token: 0x040010CF RID: 4303
		Separator = 108,
		/// <summary>The subtract key.</summary>
		// Token: 0x040010D0 RID: 4304
		Subtract = 109,
		/// <summary>The decimal key.</summary>
		// Token: 0x040010D1 RID: 4305
		Decimal = 110,
		/// <summary>The divide key.</summary>
		// Token: 0x040010D2 RID: 4306
		Divide = 111,
		/// <summary>The F1 key.</summary>
		// Token: 0x040010D3 RID: 4307
		F1 = 112,
		/// <summary>The F2 key.</summary>
		// Token: 0x040010D4 RID: 4308
		F2 = 113,
		/// <summary>The F3 key.</summary>
		// Token: 0x040010D5 RID: 4309
		F3 = 114,
		/// <summary>The F4 key.</summary>
		// Token: 0x040010D6 RID: 4310
		F4 = 115,
		/// <summary>The F5 key.</summary>
		// Token: 0x040010D7 RID: 4311
		F5 = 116,
		/// <summary>The F6 key.</summary>
		// Token: 0x040010D8 RID: 4312
		F6 = 117,
		/// <summary>The F7 key.</summary>
		// Token: 0x040010D9 RID: 4313
		F7 = 118,
		/// <summary>The F8 key.</summary>
		// Token: 0x040010DA RID: 4314
		F8 = 119,
		/// <summary>The F9 key.</summary>
		// Token: 0x040010DB RID: 4315
		F9 = 120,
		/// <summary>The F10 key.</summary>
		// Token: 0x040010DC RID: 4316
		F10 = 121,
		/// <summary>The F11 key.</summary>
		// Token: 0x040010DD RID: 4317
		F11 = 122,
		/// <summary>The F12 key.</summary>
		// Token: 0x040010DE RID: 4318
		F12 = 123,
		/// <summary>The F13 key.</summary>
		// Token: 0x040010DF RID: 4319
		F13 = 124,
		/// <summary>The F14 key.</summary>
		// Token: 0x040010E0 RID: 4320
		F14 = 125,
		/// <summary>The F15 key.</summary>
		// Token: 0x040010E1 RID: 4321
		F15 = 126,
		/// <summary>The F16 key.</summary>
		// Token: 0x040010E2 RID: 4322
		F16 = 127,
		/// <summary>The F17 key.</summary>
		// Token: 0x040010E3 RID: 4323
		F17 = 128,
		/// <summary>The F18 key.</summary>
		// Token: 0x040010E4 RID: 4324
		F18 = 129,
		/// <summary>The F19 key.</summary>
		// Token: 0x040010E5 RID: 4325
		F19 = 130,
		/// <summary>The F20 key.</summary>
		// Token: 0x040010E6 RID: 4326
		F20 = 131,
		/// <summary>The F21 key.</summary>
		// Token: 0x040010E7 RID: 4327
		F21 = 132,
		/// <summary>The F22 key.</summary>
		// Token: 0x040010E8 RID: 4328
		F22 = 133,
		/// <summary>The F23 key.</summary>
		// Token: 0x040010E9 RID: 4329
		F23 = 134,
		/// <summary>The F24 key.</summary>
		// Token: 0x040010EA RID: 4330
		F24 = 135,
		/// <summary>The NUM LOCK key.</summary>
		// Token: 0x040010EB RID: 4331
		NumLock = 144,
		/// <summary>The SCROLL LOCK key.</summary>
		// Token: 0x040010EC RID: 4332
		Scroll = 145,
		/// <summary>The left SHIFT key.</summary>
		// Token: 0x040010ED RID: 4333
		LShiftKey = 160,
		/// <summary>The right SHIFT key.</summary>
		// Token: 0x040010EE RID: 4334
		RShiftKey = 161,
		/// <summary>The left CTRL key.</summary>
		// Token: 0x040010EF RID: 4335
		LControlKey = 162,
		/// <summary>The right CTRL key.</summary>
		// Token: 0x040010F0 RID: 4336
		RControlKey = 163,
		/// <summary>The left ALT key.</summary>
		// Token: 0x040010F1 RID: 4337
		LMenu = 164,
		/// <summary>The right ALT key.</summary>
		// Token: 0x040010F2 RID: 4338
		RMenu = 165,
		/// <summary>The browser back key (Windows 2000 or later).</summary>
		// Token: 0x040010F3 RID: 4339
		BrowserBack = 166,
		/// <summary>The browser forward key (Windows 2000 or later).</summary>
		// Token: 0x040010F4 RID: 4340
		BrowserForward = 167,
		/// <summary>The browser refresh key (Windows 2000 or later).</summary>
		// Token: 0x040010F5 RID: 4341
		BrowserRefresh = 168,
		/// <summary>The browser stop key (Windows 2000 or later).</summary>
		// Token: 0x040010F6 RID: 4342
		BrowserStop = 169,
		/// <summary>The browser search key (Windows 2000 or later).</summary>
		// Token: 0x040010F7 RID: 4343
		BrowserSearch = 170,
		/// <summary>The browser favorites key (Windows 2000 or later).</summary>
		// Token: 0x040010F8 RID: 4344
		BrowserFavorites = 171,
		/// <summary>The browser home key (Windows 2000 or later).</summary>
		// Token: 0x040010F9 RID: 4345
		BrowserHome = 172,
		/// <summary>The volume mute key (Windows 2000 or later).</summary>
		// Token: 0x040010FA RID: 4346
		VolumeMute = 173,
		/// <summary>The volume down key (Windows 2000 or later).</summary>
		// Token: 0x040010FB RID: 4347
		VolumeDown = 174,
		/// <summary>The volume up key (Windows 2000 or later).</summary>
		// Token: 0x040010FC RID: 4348
		VolumeUp = 175,
		/// <summary>The media next track key (Windows 2000 or later).</summary>
		// Token: 0x040010FD RID: 4349
		MediaNextTrack = 176,
		/// <summary>The media previous track key (Windows 2000 or later).</summary>
		// Token: 0x040010FE RID: 4350
		MediaPreviousTrack = 177,
		/// <summary>The media Stop key (Windows 2000 or later).</summary>
		// Token: 0x040010FF RID: 4351
		MediaStop = 178,
		/// <summary>The media play pause key (Windows 2000 or later).</summary>
		// Token: 0x04001100 RID: 4352
		MediaPlayPause = 179,
		/// <summary>The launch mail key (Windows 2000 or later).</summary>
		// Token: 0x04001101 RID: 4353
		LaunchMail = 180,
		/// <summary>The select media key (Windows 2000 or later).</summary>
		// Token: 0x04001102 RID: 4354
		SelectMedia = 181,
		/// <summary>The start application one key (Windows 2000 or later).</summary>
		// Token: 0x04001103 RID: 4355
		LaunchApplication1 = 182,
		/// <summary>The start application two key (Windows 2000 or later).</summary>
		// Token: 0x04001104 RID: 4356
		LaunchApplication2 = 183,
		/// <summary>The OEM Semicolon key on a US standard keyboard (Windows 2000 or later).</summary>
		// Token: 0x04001105 RID: 4357
		OemSemicolon = 186,
		/// <summary>The OEM plus key on any country/region keyboard (Windows 2000 or later).</summary>
		// Token: 0x04001106 RID: 4358
		Oemplus = 187,
		/// <summary>The OEM comma key on any country/region keyboard (Windows 2000 or later).</summary>
		// Token: 0x04001107 RID: 4359
		Oemcomma = 188,
		/// <summary>The OEM minus key on any country/region keyboard (Windows 2000 or later).</summary>
		// Token: 0x04001108 RID: 4360
		OemMinus = 189,
		/// <summary>The OEM period key on any country/region keyboard (Windows 2000 or later).</summary>
		// Token: 0x04001109 RID: 4361
		OemPeriod = 190,
		/// <summary>The OEM question mark key on a US standard keyboard (Windows 2000 or later).</summary>
		// Token: 0x0400110A RID: 4362
		OemQuestion = 191,
		/// <summary>The OEM tilde key on a US standard keyboard (Windows 2000 or later).</summary>
		// Token: 0x0400110B RID: 4363
		Oemtilde = 192,
		/// <summary>The OEM open bracket key on a US standard keyboard (Windows 2000 or later).</summary>
		// Token: 0x0400110C RID: 4364
		OemOpenBrackets = 219,
		/// <summary>The OEM pipe key on a US standard keyboard (Windows 2000 or later).</summary>
		// Token: 0x0400110D RID: 4365
		OemPipe = 220,
		/// <summary>The OEM close bracket key on a US standard keyboard (Windows 2000 or later).</summary>
		// Token: 0x0400110E RID: 4366
		OemCloseBrackets = 221,
		/// <summary>The OEM singled/double quote key on a US standard keyboard (Windows 2000 or later).</summary>
		// Token: 0x0400110F RID: 4367
		OemQuotes = 222,
		/// <summary>The OEM 8 key.</summary>
		// Token: 0x04001110 RID: 4368
		Oem8 = 223,
		/// <summary>The OEM angle bracket or backslash key on the RT 102 key keyboard (Windows 2000 or later).</summary>
		// Token: 0x04001111 RID: 4369
		OemBackslash = 226,
		/// <summary>The PROCESS KEY key.</summary>
		// Token: 0x04001112 RID: 4370
		ProcessKey = 229,
		/// <summary>The ATTN key.</summary>
		// Token: 0x04001113 RID: 4371
		Attn = 246,
		/// <summary>The CRSEL key.</summary>
		// Token: 0x04001114 RID: 4372
		Crsel = 247,
		/// <summary>The EXSEL key.</summary>
		// Token: 0x04001115 RID: 4373
		Exsel = 248,
		/// <summary>The ERASE EOF key.</summary>
		// Token: 0x04001116 RID: 4374
		EraseEof = 249,
		/// <summary>The PLAY key.</summary>
		// Token: 0x04001117 RID: 4375
		Play = 250,
		/// <summary>The ZOOM key.</summary>
		// Token: 0x04001118 RID: 4376
		Zoom = 251,
		/// <summary>A constant reserved for future use.</summary>
		// Token: 0x04001119 RID: 4377
		NoName = 252,
		/// <summary>The PA1 key.</summary>
		// Token: 0x0400111A RID: 4378
		Pa1 = 253,
		/// <summary>The CLEAR key.</summary>
		// Token: 0x0400111B RID: 4379
		OemClear = 254,
		/// <summary>The bitmask to extract a key code from a key value.</summary>
		// Token: 0x0400111C RID: 4380
		KeyCode = 65535,
		/// <summary>The SHIFT modifier key.</summary>
		// Token: 0x0400111D RID: 4381
		Shift = 65536,
		/// <summary>The CTRL modifier key.</summary>
		// Token: 0x0400111E RID: 4382
		Control = 131072,
		/// <summary>The ALT modifier key.</summary>
		// Token: 0x0400111F RID: 4383
		Alt = 262144,
		/// <summary>The bitmask to extract modifiers from a key value.</summary>
		// Token: 0x04001120 RID: 4384
		Modifiers = -65536,
		/// <summary>The IME accept key, replaces <see cref="F:System.Windows.Forms.Keys.IMEAceept" />.</summary>
		// Token: 0x04001121 RID: 4385
		IMEAccept = 30,
		/// <summary>The OEM 1 key.</summary>
		// Token: 0x04001122 RID: 4386
		Oem1 = 186,
		/// <summary>The OEM 102 key.</summary>
		// Token: 0x04001123 RID: 4387
		Oem102 = 226,
		/// <summary>The OEM 2 key.</summary>
		// Token: 0x04001124 RID: 4388
		Oem2 = 191,
		/// <summary>The OEM 3 key.</summary>
		// Token: 0x04001125 RID: 4389
		Oem3 = 192,
		/// <summary>The OEM 4 key.</summary>
		// Token: 0x04001126 RID: 4390
		Oem4 = 219,
		/// <summary>The OEM 5 key.</summary>
		// Token: 0x04001127 RID: 4391
		Oem5 = 220,
		/// <summary>The OEM 6 key.</summary>
		// Token: 0x04001128 RID: 4392
		Oem6 = 221,
		/// <summary>The OEM 7 key.</summary>
		// Token: 0x04001129 RID: 4393
		Oem7 = 222,
		/// <summary>Used to pass Unicode characters as if they were keystrokes. The Packet key value is the low word of a 32-bit virtual-key value used for non-keyboard input methods.</summary>
		// Token: 0x0400112A RID: 4394
		Packet = 231,
		/// <summary>The computer sleep key.</summary>
		// Token: 0x0400112B RID: 4395
		Sleep = 95
	}
}
