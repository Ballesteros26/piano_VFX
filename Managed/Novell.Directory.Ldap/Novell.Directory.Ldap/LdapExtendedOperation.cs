using System;

namespace Novell.Directory.Ldap
{
	// Token: 0x0200001F RID: 31
	public class LdapExtendedOperation : ICloneable
	{
		// Token: 0x06000170 RID: 368 RVA: 0x00007B29 File Offset: 0x00005D29
		[CLSCompliant(false)]
		public LdapExtendedOperation(string oid, sbyte[] vals)
		{
			this.oid = oid;
			this.vals = vals;
		}

		// Token: 0x06000171 RID: 369 RVA: 0x00007B40 File Offset: 0x00005D40
		public object Clone()
		{
			object obj2;
			try
			{
				object obj = base.MemberwiseClone();
				Array.Copy(this.vals, 0, ((LdapExtendedOperation)obj).vals, 0, this.vals.Length);
				obj2 = obj;
			}
			catch (Exception)
			{
				throw new SystemException("Internal error, cannot create clone");
			}
			return obj2;
		}

		// Token: 0x06000172 RID: 370 RVA: 0x00007B98 File Offset: 0x00005D98
		public virtual string getID()
		{
			return this.oid;
		}

		// Token: 0x06000173 RID: 371 RVA: 0x00007BA0 File Offset: 0x00005DA0
		[CLSCompliant(false)]
		public virtual sbyte[] getValue()
		{
			return this.vals;
		}

		// Token: 0x06000174 RID: 372 RVA: 0x00007BA8 File Offset: 0x00005DA8
		[CLSCompliant(false)]
		protected internal virtual void setValue(sbyte[] newVals)
		{
			this.vals = newVals;
		}

		// Token: 0x06000175 RID: 373 RVA: 0x00007BB1 File Offset: 0x00005DB1
		protected internal virtual void setID(string newoid)
		{
			this.oid = newoid;
		}

		// Token: 0x040000FB RID: 251
		private string oid;

		// Token: 0x040000FC RID: 252
		private sbyte[] vals;
	}
}
