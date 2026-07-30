using System;

namespace UnityEngine
{
	// Token: 0x02000092 RID: 146
	public enum RuntimePlatform
	{
		// Token: 0x04000150 RID: 336
		OSXEditor,
		// Token: 0x04000151 RID: 337
		OSXPlayer,
		// Token: 0x04000152 RID: 338
		WindowsPlayer,
		// Token: 0x04000153 RID: 339
		[Obsolete("WebPlayer export is no longer supported in Unity 5.4+.", true)]
		OSXWebPlayer,
		// Token: 0x04000154 RID: 340
		[Obsolete("Dashboard widget on Mac OS X export is no longer supported in Unity 5.4+.", true)]
		OSXDashboardPlayer,
		// Token: 0x04000155 RID: 341
		[Obsolete("WebPlayer export is no longer supported in Unity 5.4+.", true)]
		WindowsWebPlayer,
		// Token: 0x04000156 RID: 342
		WindowsEditor = 7,
		// Token: 0x04000157 RID: 343
		IPhonePlayer,
		// Token: 0x04000158 RID: 344
		[Obsolete("Xbox360 export is no longer supported in Unity 5.5+.")]
		XBOX360 = 10,
		// Token: 0x04000159 RID: 345
		[Obsolete("PS3 export is no longer supported in Unity >=5.5.")]
		PS3 = 9,
		// Token: 0x0400015A RID: 346
		Android = 11,
		// Token: 0x0400015B RID: 347
		[Obsolete("NaCl export is no longer supported in Unity 5.0+.")]
		NaCl,
		// Token: 0x0400015C RID: 348
		[Obsolete("FlashPlayer export is no longer supported in Unity 5.0+.")]
		FlashPlayer = 15,
		// Token: 0x0400015D RID: 349
		LinuxPlayer = 13,
		// Token: 0x0400015E RID: 350
		LinuxEditor = 16,
		// Token: 0x0400015F RID: 351
		WebGLPlayer,
		// Token: 0x04000160 RID: 352
		[Obsolete("Use WSAPlayerX86 instead")]
		MetroPlayerX86,
		// Token: 0x04000161 RID: 353
		WSAPlayerX86 = 18,
		// Token: 0x04000162 RID: 354
		[Obsolete("Use WSAPlayerX64 instead")]
		MetroPlayerX64,
		// Token: 0x04000163 RID: 355
		WSAPlayerX64 = 19,
		// Token: 0x04000164 RID: 356
		[Obsolete("Use WSAPlayerARM instead")]
		MetroPlayerARM,
		// Token: 0x04000165 RID: 357
		WSAPlayerARM = 20,
		// Token: 0x04000166 RID: 358
		[Obsolete("Windows Phone 8 was removed in 5.3")]
		WP8Player,
		// Token: 0x04000167 RID: 359
		[Obsolete("BlackBerryPlayer export is no longer supported in Unity 5.4+.")]
		BlackBerryPlayer,
		// Token: 0x04000168 RID: 360
		[Obsolete("TizenPlayer export is no longer supported in Unity 2017.3+.")]
		TizenPlayer,
		// Token: 0x04000169 RID: 361
		[Obsolete("PSP2 is no longer supported as of Unity 2018.3")]
		PSP2,
		// Token: 0x0400016A RID: 362
		PS4,
		// Token: 0x0400016B RID: 363
		[Obsolete("PSM export is no longer supported in Unity >= 5.3")]
		PSM,
		// Token: 0x0400016C RID: 364
		XboxOne,
		// Token: 0x0400016D RID: 365
		[Obsolete("SamsungTVPlayer export is no longer supported in Unity 2017.3+.")]
		SamsungTVPlayer,
		// Token: 0x0400016E RID: 366
		[Obsolete("Wii U is no longer supported in Unity 2018.1+.")]
		WiiU = 30,
		// Token: 0x0400016F RID: 367
		tvOS,
		// Token: 0x04000170 RID: 368
		Switch,
		// Token: 0x04000171 RID: 369
		Lumin,
		// Token: 0x04000172 RID: 370
		Stadia
	}
}
