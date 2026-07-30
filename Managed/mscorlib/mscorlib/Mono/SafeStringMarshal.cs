using System;
using System.Runtime.CompilerServices;

namespace Mono
{
	// Token: 0x02000024 RID: 36
	internal struct SafeStringMarshal : IDisposable
	{
		// Token: 0x060000A6 RID: 166
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern IntPtr StringToUtf8(string str);

		// Token: 0x060000A7 RID: 167
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void GFree(IntPtr ptr);

		// Token: 0x060000A8 RID: 168 RVA: 0x0000403F File Offset: 0x0000223F
		public SafeStringMarshal(string str)
		{
			this.str = str;
			this.marshaled_string = IntPtr.Zero;
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x060000A9 RID: 169 RVA: 0x00004053 File Offset: 0x00002253
		public IntPtr Value
		{
			get
			{
				if (this.marshaled_string == IntPtr.Zero && this.str != null)
				{
					this.marshaled_string = SafeStringMarshal.StringToUtf8(this.str);
				}
				return this.marshaled_string;
			}
		}

		// Token: 0x060000AA RID: 170 RVA: 0x00004086 File Offset: 0x00002286
		public void Dispose()
		{
			if (this.marshaled_string != IntPtr.Zero)
			{
				SafeStringMarshal.GFree(this.marshaled_string);
				this.marshaled_string = IntPtr.Zero;
			}
		}

		// Token: 0x040003B5 RID: 949
		private readonly string str;

		// Token: 0x040003B6 RID: 950
		private IntPtr marshaled_string;
	}
}
