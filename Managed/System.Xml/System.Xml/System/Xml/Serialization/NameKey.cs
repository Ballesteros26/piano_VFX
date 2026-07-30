using System;

namespace System.Xml.Serialization
{
	// Token: 0x020002FF RID: 767
	internal class NameKey
	{
		// Token: 0x06001C8E RID: 7310 RVA: 0x0009BCD8 File Offset: 0x00099ED8
		internal NameKey(string name, string ns)
		{
			this.name = name;
			this.ns = ns;
		}

		// Token: 0x06001C8F RID: 7311 RVA: 0x0009BCF0 File Offset: 0x00099EF0
		public override bool Equals(object other)
		{
			if (!(other is NameKey))
			{
				return false;
			}
			NameKey nameKey = (NameKey)other;
			return this.name == nameKey.name && this.ns == nameKey.ns;
		}

		// Token: 0x06001C90 RID: 7312 RVA: 0x0009BD34 File Offset: 0x00099F34
		public override int GetHashCode()
		{
			return ((this.ns == null) ? "<null>".GetHashCode() : this.ns.GetHashCode()) ^ ((this.name == null) ? 0 : this.name.GetHashCode());
		}

		// Token: 0x04001662 RID: 5730
		private string ns;

		// Token: 0x04001663 RID: 5731
		private string name;
	}
}
