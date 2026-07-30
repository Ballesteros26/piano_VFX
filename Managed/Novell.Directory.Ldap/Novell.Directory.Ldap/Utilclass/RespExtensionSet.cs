using System;
using System.Collections;

namespace Novell.Directory.Ldap.Utilclass
{
	// Token: 0x0200004E RID: 78
	public class RespExtensionSet : SupportClass.AbstractSetSupport
	{
		// Token: 0x170000D7 RID: 215
		// (get) Token: 0x060002EF RID: 751 RVA: 0x0000E7B4 File Offset: 0x0000C9B4
		public override int Count
		{
			get
			{
				return this.map.Count;
			}
		}

		// Token: 0x060002F0 RID: 752 RVA: 0x0000E7C1 File Offset: 0x0000C9C1
		public RespExtensionSet()
		{
			this.map = new Hashtable();
		}

		// Token: 0x060002F1 RID: 753 RVA: 0x0000E7D4 File Offset: 0x0000C9D4
		public void registerResponseExtension(string oid, Type extClass)
		{
			lock (this)
			{
				if (!this.map.ContainsKey(oid))
				{
					this.map.Add(oid, extClass);
				}
			}
		}

		// Token: 0x060002F2 RID: 754 RVA: 0x0000E824 File Offset: 0x0000CA24
		public override IEnumerator GetEnumerator()
		{
			return this.map.Values.GetEnumerator();
		}

		// Token: 0x060002F3 RID: 755 RVA: 0x0000E838 File Offset: 0x0000CA38
		public Type findResponseExtension(string searchOID)
		{
			Type type;
			lock (this)
			{
				if (this.map.ContainsKey(searchOID))
				{
					type = (Type)this.map[searchOID];
				}
				else
				{
					type = null;
				}
			}
			return type;
		}

		// Token: 0x040001F7 RID: 503
		private Hashtable map;
	}
}
