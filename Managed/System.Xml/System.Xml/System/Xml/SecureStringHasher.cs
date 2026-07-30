using System;
using System.Collections.Generic;
using System.Reflection;
using System.Security;
using System.Security.Permissions;

namespace System.Xml
{
	// Token: 0x020000BB RID: 187
	internal class SecureStringHasher : IEqualityComparer<string>
	{
		// Token: 0x060005F2 RID: 1522 RVA: 0x0001AF37 File Offset: 0x00019137
		public SecureStringHasher()
		{
			this.hashCodeRandomizer = Environment.TickCount;
		}

		// Token: 0x060005F3 RID: 1523 RVA: 0x0001AF4A File Offset: 0x0001914A
		public bool Equals(string x, string y)
		{
			return string.Equals(x, y, StringComparison.Ordinal);
		}

		// Token: 0x060005F4 RID: 1524 RVA: 0x0001AF54 File Offset: 0x00019154
		[SecuritySafeCritical]
		public int GetHashCode(string key)
		{
			if (SecureStringHasher.hashCodeDelegate == null)
			{
				SecureStringHasher.hashCodeDelegate = SecureStringHasher.GetHashCodeDelegate();
			}
			return SecureStringHasher.hashCodeDelegate(key, key.Length, (long)this.hashCodeRandomizer);
		}

		// Token: 0x060005F5 RID: 1525 RVA: 0x0001AF80 File Offset: 0x00019180
		[SecurityCritical]
		private static int GetHashCodeOfString(string key, int sLen, long additionalEntropy)
		{
			int num = (int)additionalEntropy;
			for (int i = 0; i < key.Length; i++)
			{
				num += (num << 7) ^ (int)key[i];
			}
			num -= num >> 17;
			num -= num >> 11;
			return num - (num >> 5);
		}

		// Token: 0x060005F6 RID: 1526 RVA: 0x0001AFC4 File Offset: 0x000191C4
		[SecuritySafeCritical]
		[ReflectionPermission(SecurityAction.Assert, Unrestricted = true)]
		private static SecureStringHasher.HashCodeOfStringDelegate GetHashCodeDelegate()
		{
			MethodInfo method = typeof(string).GetMethod("InternalMarvin32HashString", BindingFlags.Static | BindingFlags.NonPublic);
			if (method != null)
			{
				return (SecureStringHasher.HashCodeOfStringDelegate)Delegate.CreateDelegate(typeof(SecureStringHasher.HashCodeOfStringDelegate), method);
			}
			return new SecureStringHasher.HashCodeOfStringDelegate(SecureStringHasher.GetHashCodeOfString);
		}

		// Token: 0x040003C4 RID: 964
		[SecurityCritical]
		private static SecureStringHasher.HashCodeOfStringDelegate hashCodeDelegate;

		// Token: 0x040003C5 RID: 965
		private int hashCodeRandomizer;

		// Token: 0x020000BC RID: 188
		// (Invoke) Token: 0x060005F8 RID: 1528
		[SecurityCritical]
		private delegate int HashCodeOfStringDelegate(string s, int sLen, long additionalEntropy);
	}
}
