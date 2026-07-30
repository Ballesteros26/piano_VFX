using System;

namespace Mono
{
	// Token: 0x0200001A RID: 26
	internal static class RuntimeStructs
	{
		// Token: 0x0200001B RID: 27
		internal struct RemoteClass
		{
			// Token: 0x04000384 RID: 900
			internal IntPtr default_vtable;

			// Token: 0x04000385 RID: 901
			internal IntPtr xdomain_vtable;

			// Token: 0x04000386 RID: 902
			internal unsafe RuntimeStructs.MonoClass* proxy_class;

			// Token: 0x04000387 RID: 903
			internal IntPtr proxy_class_name;

			// Token: 0x04000388 RID: 904
			internal uint interface_count;
		}

		// Token: 0x0200001C RID: 28
		internal struct MonoClass
		{
		}

		// Token: 0x0200001D RID: 29
		internal struct GenericParamInfo
		{
			// Token: 0x04000389 RID: 905
			internal unsafe RuntimeStructs.MonoClass* pklass;

			// Token: 0x0400038A RID: 906
			internal IntPtr name;

			// Token: 0x0400038B RID: 907
			internal ushort flags;

			// Token: 0x0400038C RID: 908
			internal uint token;

			// Token: 0x0400038D RID: 909
			internal unsafe RuntimeStructs.MonoClass** constraints;
		}

		// Token: 0x0200001E RID: 30
		internal struct GPtrArray
		{
			// Token: 0x0400038E RID: 910
			internal unsafe IntPtr* data;

			// Token: 0x0400038F RID: 911
			internal int len;
		}

		// Token: 0x0200001F RID: 31
		private struct HandleStackMark
		{
			// Token: 0x04000390 RID: 912
			private int size;

			// Token: 0x04000391 RID: 913
			private int interior_size;

			// Token: 0x04000392 RID: 914
			private IntPtr chunk;
		}

		// Token: 0x02000020 RID: 32
		private struct MonoError
		{
			// Token: 0x04000393 RID: 915
			private ushort error_code;

			// Token: 0x04000394 RID: 916
			private ushort hidden_0;

			// Token: 0x04000395 RID: 917
			private IntPtr hidden_1;

			// Token: 0x04000396 RID: 918
			private IntPtr hidden_2;

			// Token: 0x04000397 RID: 919
			private IntPtr hidden_3;

			// Token: 0x04000398 RID: 920
			private IntPtr hidden_4;

			// Token: 0x04000399 RID: 921
			private IntPtr hidden_5;

			// Token: 0x0400039A RID: 922
			private IntPtr hidden_6;

			// Token: 0x0400039B RID: 923
			private IntPtr hidden_7;

			// Token: 0x0400039C RID: 924
			private IntPtr hidden_8;

			// Token: 0x0400039D RID: 925
			private IntPtr hidden_11;

			// Token: 0x0400039E RID: 926
			private IntPtr hidden_12;

			// Token: 0x0400039F RID: 927
			private IntPtr hidden_13;

			// Token: 0x040003A0 RID: 928
			private IntPtr hidden_14;

			// Token: 0x040003A1 RID: 929
			private IntPtr hidden_15;

			// Token: 0x040003A2 RID: 930
			private IntPtr hidden_16;

			// Token: 0x040003A3 RID: 931
			private IntPtr hidden_17;

			// Token: 0x040003A4 RID: 932
			private IntPtr hidden_18;
		}
	}
}
