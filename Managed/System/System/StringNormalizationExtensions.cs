using System;
using System.ComponentModel;
using System.Security;
using System.Text;
using Unity;

namespace System
{
	// Token: 0x020007D4 RID: 2004
	[EditorBrowsable(EditorBrowsableState.Never)]
	public static class StringNormalizationExtensions
	{
		// Token: 0x0600401D RID: 16413 RVA: 0x000E0D9C File Offset: 0x000DEF9C
		[EditorBrowsable(EditorBrowsableState.Never)]
		public static bool IsNormalized(this string value)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return default(bool);
		}

		// Token: 0x0600401E RID: 16414 RVA: 0x000E0DB8 File Offset: 0x000DEFB8
		[EditorBrowsable(EditorBrowsableState.Never)]
		[SecurityCritical]
		public static bool IsNormalized(this string value, NormalizationForm normalizationForm)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return default(bool);
		}

		// Token: 0x0600401F RID: 16415 RVA: 0x0003D2D0 File Offset: 0x0003B4D0
		[EditorBrowsable(EditorBrowsableState.Never)]
		public static string Normalize(this string value)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}

		// Token: 0x06004020 RID: 16416 RVA: 0x0003D2D0 File Offset: 0x0003B4D0
		[SecurityCritical]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public static string Normalize(this string value, NormalizationForm normalizationForm)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}
	}
}
