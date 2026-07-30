using System;

namespace System
{
	// Token: 0x020001FC RID: 508
	internal struct RuntimeMethodHandleInternal
	{
		// Token: 0x170002EF RID: 751
		// (get) Token: 0x060017C0 RID: 6080 RVA: 0x0005CDEC File Offset: 0x0005AFEC
		internal static RuntimeMethodHandleInternal EmptyHandle
		{
			get
			{
				return default(RuntimeMethodHandleInternal);
			}
		}

		// Token: 0x060017C1 RID: 6081 RVA: 0x0005CE02 File Offset: 0x0005B002
		internal bool IsNullHandle()
		{
			return this.m_handle.IsNull();
		}

		// Token: 0x170002F0 RID: 752
		// (get) Token: 0x060017C2 RID: 6082 RVA: 0x0005CE0F File Offset: 0x0005B00F
		internal IntPtr Value
		{
			get
			{
				return this.m_handle;
			}
		}

		// Token: 0x060017C3 RID: 6083 RVA: 0x0005CE17 File Offset: 0x0005B017
		internal RuntimeMethodHandleInternal(IntPtr value)
		{
			this.m_handle = value;
		}

		// Token: 0x04000C56 RID: 3158
		internal IntPtr m_handle;
	}
}
