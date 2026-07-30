using System;
using System.Runtime.InteropServices;

namespace Ookii.Dialogs.Interop
{
	// Token: 0x02000032 RID: 50
	[Guid("EBBC7C04-315E-11d2-B62F-006097DF5BD4")]
	[InterfaceType(1)]
	[ComImport]
	internal interface IProgressDialog
	{
		// Token: 0x06000240 RID: 576
		[PreserveSig]
		void StartProgressDialog(IntPtr hwndParent, [MarshalAs(25)] object punkEnableModless, ProgressDialogFlags dwFlags, IntPtr pvResevered);

		// Token: 0x06000241 RID: 577
		[PreserveSig]
		void StopProgressDialog();

		// Token: 0x06000242 RID: 578
		[PreserveSig]
		void SetTitle([MarshalAs(21)] string pwzTitle);

		// Token: 0x06000243 RID: 579
		[PreserveSig]
		void SetAnimation(SafeModuleHandle hInstAnimation, ushort idAnimation);

		// Token: 0x06000244 RID: 580
		[PreserveSig]
		[return: MarshalAs(2)]
		bool HasUserCancelled();

		// Token: 0x06000245 RID: 581
		[PreserveSig]
		void SetProgress(uint dwCompleted, uint dwTotal);

		// Token: 0x06000246 RID: 582
		[PreserveSig]
		void SetProgress64(ulong ullCompleted, ulong ullTotal);

		// Token: 0x06000247 RID: 583
		[PreserveSig]
		void SetLine(uint dwLineNum, [MarshalAs(21)] string pwzString, [MarshalAs(37)] bool fCompactPath, IntPtr pvResevered);

		// Token: 0x06000248 RID: 584
		[PreserveSig]
		void SetCancelMsg([MarshalAs(21)] string pwzCancelMsg, object pvResevered);

		// Token: 0x06000249 RID: 585
		[PreserveSig]
		void Timer(uint dwTimerAction, object pvResevered);
	}
}
