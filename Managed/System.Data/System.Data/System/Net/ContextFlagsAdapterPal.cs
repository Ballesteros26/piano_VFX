using System;

namespace System.Net
{
	// Token: 0x0200003A RID: 58
	internal static class ContextFlagsAdapterPal
	{
		// Token: 0x0600022E RID: 558 RVA: 0x0000D180 File Offset: 0x0000B380
		internal static ContextFlagsPal GetContextFlagsPalFromInterop(Interop.NetSecurityNative.GssFlags gssFlags, bool isServer)
		{
			ContextFlagsPal contextFlagsPal = ContextFlagsPal.None;
			if ((gssFlags & Interop.NetSecurityNative.GssFlags.GSS_C_INTEG_FLAG) != (Interop.NetSecurityNative.GssFlags)0U)
			{
				contextFlagsPal |= (isServer ? ContextFlagsPal.AcceptIntegrity : ContextFlagsPal.AcceptStream);
			}
			foreach (ContextFlagsAdapterPal.ContextFlagMapping contextFlagMapping in ContextFlagsAdapterPal.s_contextFlagMapping)
			{
				if ((gssFlags & contextFlagMapping.GssFlags) == contextFlagMapping.GssFlags)
				{
					contextFlagsPal |= contextFlagMapping.ContextFlag;
				}
			}
			return contextFlagsPal;
		}

		// Token: 0x0600022F RID: 559 RVA: 0x0000D1E0 File Offset: 0x0000B3E0
		internal static Interop.NetSecurityNative.GssFlags GetInteropFromContextFlagsPal(ContextFlagsPal flags, bool isServer)
		{
			Interop.NetSecurityNative.GssFlags gssFlags = (Interop.NetSecurityNative.GssFlags)0U;
			if (isServer)
			{
				if ((flags & ContextFlagsPal.AcceptIntegrity) != ContextFlagsPal.None)
				{
					gssFlags |= Interop.NetSecurityNative.GssFlags.GSS_C_INTEG_FLAG;
				}
			}
			else if ((flags & ContextFlagsPal.AcceptStream) != ContextFlagsPal.None)
			{
				gssFlags |= Interop.NetSecurityNative.GssFlags.GSS_C_INTEG_FLAG;
			}
			foreach (ContextFlagsAdapterPal.ContextFlagMapping contextFlagMapping in ContextFlagsAdapterPal.s_contextFlagMapping)
			{
				if ((flags & contextFlagMapping.ContextFlag) == contextFlagMapping.ContextFlag)
				{
					gssFlags |= contextFlagMapping.GssFlags;
				}
			}
			return gssFlags;
		}

		// Token: 0x0400044C RID: 1100
		private static readonly ContextFlagsAdapterPal.ContextFlagMapping[] s_contextFlagMapping = new ContextFlagsAdapterPal.ContextFlagMapping[]
		{
			new ContextFlagsAdapterPal.ContextFlagMapping(Interop.NetSecurityNative.GssFlags.GSS_C_CONF_FLAG, ContextFlagsPal.Confidentiality),
			new ContextFlagsAdapterPal.ContextFlagMapping(Interop.NetSecurityNative.GssFlags.GSS_C_IDENTIFY_FLAG, ContextFlagsPal.AcceptIntegrity),
			new ContextFlagsAdapterPal.ContextFlagMapping(Interop.NetSecurityNative.GssFlags.GSS_C_MUTUAL_FLAG, ContextFlagsPal.MutualAuth),
			new ContextFlagsAdapterPal.ContextFlagMapping(Interop.NetSecurityNative.GssFlags.GSS_C_REPLAY_FLAG, ContextFlagsPal.ReplayDetect),
			new ContextFlagsAdapterPal.ContextFlagMapping(Interop.NetSecurityNative.GssFlags.GSS_C_SEQUENCE_FLAG, ContextFlagsPal.SequenceDetect)
		};

		// Token: 0x0200003B RID: 59
		private struct ContextFlagMapping
		{
			// Token: 0x06000231 RID: 561 RVA: 0x0000D2B0 File Offset: 0x0000B4B0
			public ContextFlagMapping(Interop.NetSecurityNative.GssFlags gssFlag, ContextFlagsPal contextFlag)
			{
				this.GssFlags = gssFlag;
				this.ContextFlag = contextFlag;
			}

			// Token: 0x0400044D RID: 1101
			public readonly Interop.NetSecurityNative.GssFlags GssFlags;

			// Token: 0x0400044E RID: 1102
			public readonly ContextFlagsPal ContextFlag;
		}
	}
}
