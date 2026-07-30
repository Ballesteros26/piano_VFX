using System;
using Unity;

namespace System.Security.Cryptography
{
	// Token: 0x02000640 RID: 1600
	public sealed class RSASignaturePadding : IEquatable<RSASignaturePadding>
	{
		// Token: 0x06004585 RID: 17797 RVA: 0x000F49C9 File Offset: 0x000F2BC9
		private RSASignaturePadding(RSASignaturePaddingMode mode)
		{
			this._mode = mode;
		}

		// Token: 0x17000BBB RID: 3003
		// (get) Token: 0x06004586 RID: 17798 RVA: 0x000F49D8 File Offset: 0x000F2BD8
		public static RSASignaturePadding Pkcs1
		{
			get
			{
				return RSASignaturePadding.s_pkcs1;
			}
		}

		// Token: 0x17000BBC RID: 3004
		// (get) Token: 0x06004587 RID: 17799 RVA: 0x000F49DF File Offset: 0x000F2BDF
		public static RSASignaturePadding Pss
		{
			get
			{
				return RSASignaturePadding.s_pss;
			}
		}

		// Token: 0x17000BBD RID: 3005
		// (get) Token: 0x06004588 RID: 17800 RVA: 0x000F49E6 File Offset: 0x000F2BE6
		public RSASignaturePaddingMode Mode
		{
			get
			{
				return this._mode;
			}
		}

		// Token: 0x06004589 RID: 17801 RVA: 0x000F49F0 File Offset: 0x000F2BF0
		public override int GetHashCode()
		{
			return this._mode.GetHashCode();
		}

		// Token: 0x0600458A RID: 17802 RVA: 0x000F4A11 File Offset: 0x000F2C11
		public override bool Equals(object obj)
		{
			return this.Equals(obj as RSASignaturePadding);
		}

		// Token: 0x0600458B RID: 17803 RVA: 0x000F4A1F File Offset: 0x000F2C1F
		public bool Equals(RSASignaturePadding other)
		{
			return other != null && this._mode == other._mode;
		}

		// Token: 0x0600458C RID: 17804 RVA: 0x000F4A3A File Offset: 0x000F2C3A
		public static bool operator ==(RSASignaturePadding left, RSASignaturePadding right)
		{
			if (left == null)
			{
				return right == null;
			}
			return left.Equals(right);
		}

		// Token: 0x0600458D RID: 17805 RVA: 0x000F4A4B File Offset: 0x000F2C4B
		public static bool operator !=(RSASignaturePadding left, RSASignaturePadding right)
		{
			return !(left == right);
		}

		// Token: 0x0600458E RID: 17806 RVA: 0x000F4A58 File Offset: 0x000F2C58
		public override string ToString()
		{
			return this._mode.ToString();
		}

		// Token: 0x06004590 RID: 17808 RVA: 0x0001FB35 File Offset: 0x0001DD35
		internal RSASignaturePadding()
		{
			ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x040023C6 RID: 9158
		private static readonly RSASignaturePadding s_pkcs1 = new RSASignaturePadding(RSASignaturePaddingMode.Pkcs1);

		// Token: 0x040023C7 RID: 9159
		private static readonly RSASignaturePadding s_pss = new RSASignaturePadding(RSASignaturePaddingMode.Pss);

		// Token: 0x040023C8 RID: 9160
		private readonly RSASignaturePaddingMode _mode;
	}
}
