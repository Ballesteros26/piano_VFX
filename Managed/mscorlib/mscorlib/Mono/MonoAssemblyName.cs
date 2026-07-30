using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Mono
{
	// Token: 0x02000021 RID: 33
	internal struct MonoAssemblyName
	{
		// Token: 0x040003A5 RID: 933
		private const int MONO_PUBLIC_KEY_TOKEN_LENGTH = 17;

		// Token: 0x040003A6 RID: 934
		internal IntPtr name;

		// Token: 0x040003A7 RID: 935
		internal IntPtr culture;

		// Token: 0x040003A8 RID: 936
		internal IntPtr hash_value;

		// Token: 0x040003A9 RID: 937
		internal IntPtr public_key;

		// Token: 0x040003AA RID: 938
		[FixedBuffer(typeof(byte), 17)]
		internal MonoAssemblyName.<public_key_token>e__FixedBuffer public_key_token;

		// Token: 0x040003AB RID: 939
		internal uint hash_alg;

		// Token: 0x040003AC RID: 940
		internal uint hash_len;

		// Token: 0x040003AD RID: 941
		internal uint flags;

		// Token: 0x040003AE RID: 942
		internal ushort major;

		// Token: 0x040003AF RID: 943
		internal ushort minor;

		// Token: 0x040003B0 RID: 944
		internal ushort build;

		// Token: 0x040003B1 RID: 945
		internal ushort revision;

		// Token: 0x040003B2 RID: 946
		internal ushort arch;

		// Token: 0x02000022 RID: 34
		[CompilerGenerated]
		[UnsafeValueType]
		[StructLayout(LayoutKind.Sequential, Size = 17)]
		public struct <public_key_token>e__FixedBuffer
		{
			// Token: 0x040003B3 RID: 947
			public byte FixedElementField;
		}
	}
}
