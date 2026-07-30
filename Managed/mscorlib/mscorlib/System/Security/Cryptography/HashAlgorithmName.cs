using System;

namespace System.Security.Cryptography
{
	// Token: 0x0200063D RID: 1597
	public struct HashAlgorithmName : IEquatable<HashAlgorithmName>
	{
		// Token: 0x17000BAE RID: 2990
		// (get) Token: 0x06004566 RID: 17766 RVA: 0x000F479B File Offset: 0x000F299B
		public static HashAlgorithmName MD5
		{
			get
			{
				return new HashAlgorithmName("MD5");
			}
		}

		// Token: 0x17000BAF RID: 2991
		// (get) Token: 0x06004567 RID: 17767 RVA: 0x000F47A7 File Offset: 0x000F29A7
		public static HashAlgorithmName SHA1
		{
			get
			{
				return new HashAlgorithmName("SHA1");
			}
		}

		// Token: 0x17000BB0 RID: 2992
		// (get) Token: 0x06004568 RID: 17768 RVA: 0x000F47B3 File Offset: 0x000F29B3
		public static HashAlgorithmName SHA256
		{
			get
			{
				return new HashAlgorithmName("SHA256");
			}
		}

		// Token: 0x17000BB1 RID: 2993
		// (get) Token: 0x06004569 RID: 17769 RVA: 0x000F47BF File Offset: 0x000F29BF
		public static HashAlgorithmName SHA384
		{
			get
			{
				return new HashAlgorithmName("SHA384");
			}
		}

		// Token: 0x17000BB2 RID: 2994
		// (get) Token: 0x0600456A RID: 17770 RVA: 0x000F47CB File Offset: 0x000F29CB
		public static HashAlgorithmName SHA512
		{
			get
			{
				return new HashAlgorithmName("SHA512");
			}
		}

		// Token: 0x0600456B RID: 17771 RVA: 0x000F47D7 File Offset: 0x000F29D7
		public HashAlgorithmName(string name)
		{
			this._name = name;
		}

		// Token: 0x17000BB3 RID: 2995
		// (get) Token: 0x0600456C RID: 17772 RVA: 0x000F47E0 File Offset: 0x000F29E0
		public string Name
		{
			get
			{
				return this._name;
			}
		}

		// Token: 0x0600456D RID: 17773 RVA: 0x000F47E8 File Offset: 0x000F29E8
		public override string ToString()
		{
			return this._name ?? string.Empty;
		}

		// Token: 0x0600456E RID: 17774 RVA: 0x000F47F9 File Offset: 0x000F29F9
		public override bool Equals(object obj)
		{
			return obj is HashAlgorithmName && this.Equals((HashAlgorithmName)obj);
		}

		// Token: 0x0600456F RID: 17775 RVA: 0x000F4811 File Offset: 0x000F2A11
		public bool Equals(HashAlgorithmName other)
		{
			return this._name == other._name;
		}

		// Token: 0x06004570 RID: 17776 RVA: 0x000F4824 File Offset: 0x000F2A24
		public override int GetHashCode()
		{
			if (this._name != null)
			{
				return this._name.GetHashCode();
			}
			return 0;
		}

		// Token: 0x06004571 RID: 17777 RVA: 0x000F483B File Offset: 0x000F2A3B
		public static bool operator ==(HashAlgorithmName left, HashAlgorithmName right)
		{
			return left.Equals(right);
		}

		// Token: 0x06004572 RID: 17778 RVA: 0x000F4845 File Offset: 0x000F2A45
		public static bool operator !=(HashAlgorithmName left, HashAlgorithmName right)
		{
			return !(left == right);
		}

		// Token: 0x040023BB RID: 9147
		private readonly string _name;
	}
}
