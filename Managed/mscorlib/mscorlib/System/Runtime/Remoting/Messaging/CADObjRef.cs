using System;

namespace System.Runtime.Remoting.Messaging
{
	// Token: 0x020007FB RID: 2043
	internal class CADObjRef
	{
		// Token: 0x060051ED RID: 20973 RVA: 0x00121A0C File Offset: 0x0011FC0C
		public CADObjRef(ObjRef o, int sourceDomain)
		{
			this.objref = o;
			this.TypeInfo = o.SerializeType();
			this.SourceDomain = sourceDomain;
		}

		// Token: 0x17000DF8 RID: 3576
		// (get) Token: 0x060051EE RID: 20974 RVA: 0x00121A2E File Offset: 0x0011FC2E
		public string TypeName
		{
			get
			{
				return this.objref.TypeInfo.TypeName;
			}
		}

		// Token: 0x17000DF9 RID: 3577
		// (get) Token: 0x060051EF RID: 20975 RVA: 0x00121A40 File Offset: 0x0011FC40
		public string URI
		{
			get
			{
				return this.objref.URI;
			}
		}

		// Token: 0x04002AE7 RID: 10983
		internal ObjRef objref;

		// Token: 0x04002AE8 RID: 10984
		internal int SourceDomain;

		// Token: 0x04002AE9 RID: 10985
		internal byte[] TypeInfo;
	}
}
