using System;

namespace System.IO
{
	// Token: 0x020003DD RID: 989
	[Flags]
	internal enum FilterFlags : uint
	{
		// Token: 0x04001A78 RID: 6776
		ReadPoll = 4096U,
		// Token: 0x04001A79 RID: 6777
		ReadOutOfBand = 8192U,
		// Token: 0x04001A7A RID: 6778
		ReadLowWaterMark = 1U,
		// Token: 0x04001A7B RID: 6779
		WriteLowWaterMark = 1U,
		// Token: 0x04001A7C RID: 6780
		NoteTrigger = 16777216U,
		// Token: 0x04001A7D RID: 6781
		NoteFFNop = 0U,
		// Token: 0x04001A7E RID: 6782
		NoteFFAnd = 1073741824U,
		// Token: 0x04001A7F RID: 6783
		NoteFFOr = 2147483648U,
		// Token: 0x04001A80 RID: 6784
		NoteFFCopy = 3221225472U,
		// Token: 0x04001A81 RID: 6785
		NoteFFCtrlMask = 3221225472U,
		// Token: 0x04001A82 RID: 6786
		NoteFFlagsMask = 16777215U,
		// Token: 0x04001A83 RID: 6787
		VNodeDelete = 1U,
		// Token: 0x04001A84 RID: 6788
		VNodeWrite = 2U,
		// Token: 0x04001A85 RID: 6789
		VNodeExtend = 4U,
		// Token: 0x04001A86 RID: 6790
		VNodeAttrib = 8U,
		// Token: 0x04001A87 RID: 6791
		VNodeLink = 16U,
		// Token: 0x04001A88 RID: 6792
		VNodeRename = 32U,
		// Token: 0x04001A89 RID: 6793
		VNodeRevoke = 64U,
		// Token: 0x04001A8A RID: 6794
		VNodeNone = 128U,
		// Token: 0x04001A8B RID: 6795
		ProcExit = 2147483648U,
		// Token: 0x04001A8C RID: 6796
		ProcFork = 1073741824U,
		// Token: 0x04001A8D RID: 6797
		ProcExec = 536870912U,
		// Token: 0x04001A8E RID: 6798
		ProcReap = 268435456U,
		// Token: 0x04001A8F RID: 6799
		ProcSignal = 134217728U,
		// Token: 0x04001A90 RID: 6800
		ProcExitStatus = 67108864U,
		// Token: 0x04001A91 RID: 6801
		ProcResourceEnd = 33554432U,
		// Token: 0x04001A92 RID: 6802
		ProcAppactive = 8388608U,
		// Token: 0x04001A93 RID: 6803
		ProcAppBackground = 4194304U,
		// Token: 0x04001A94 RID: 6804
		ProcAppNonUI = 2097152U,
		// Token: 0x04001A95 RID: 6805
		ProcAppInactive = 1048576U,
		// Token: 0x04001A96 RID: 6806
		ProcAppAllStates = 15728640U,
		// Token: 0x04001A97 RID: 6807
		ProcPDataMask = 1048575U,
		// Token: 0x04001A98 RID: 6808
		ProcControlMask = 4293918720U,
		// Token: 0x04001A99 RID: 6809
		VMPressure = 2147483648U,
		// Token: 0x04001A9A RID: 6810
		VMPressureTerminate = 1073741824U,
		// Token: 0x04001A9B RID: 6811
		VMPressureSuddenTerminate = 536870912U,
		// Token: 0x04001A9C RID: 6812
		VMError = 268435456U,
		// Token: 0x04001A9D RID: 6813
		TimerSeconds = 1U,
		// Token: 0x04001A9E RID: 6814
		TimerMicroSeconds = 2U,
		// Token: 0x04001A9F RID: 6815
		TimerNanoSeconds = 4U,
		// Token: 0x04001AA0 RID: 6816
		TimerAbsolute = 8U
	}
}
