using System;

namespace System.Xml.Serialization
{
	// Token: 0x020002F6 RID: 758
	internal abstract class TypeModel
	{
		// Token: 0x06001C6B RID: 7275 RVA: 0x0009B67B File Offset: 0x0009987B
		protected TypeModel(Type type, TypeDesc typeDesc, ModelScope scope)
		{
			this.scope = scope;
			this.type = type;
			this.typeDesc = typeDesc;
		}

		// Token: 0x170005A3 RID: 1443
		// (get) Token: 0x06001C6C RID: 7276 RVA: 0x0009B698 File Offset: 0x00099898
		internal Type Type
		{
			get
			{
				return this.type;
			}
		}

		// Token: 0x170005A4 RID: 1444
		// (get) Token: 0x06001C6D RID: 7277 RVA: 0x0009B6A0 File Offset: 0x000998A0
		internal ModelScope ModelScope
		{
			get
			{
				return this.scope;
			}
		}

		// Token: 0x170005A5 RID: 1445
		// (get) Token: 0x06001C6E RID: 7278 RVA: 0x0009B6A8 File Offset: 0x000998A8
		internal TypeDesc TypeDesc
		{
			get
			{
				return this.typeDesc;
			}
		}

		// Token: 0x0400164E RID: 5710
		private TypeDesc typeDesc;

		// Token: 0x0400164F RID: 5711
		private Type type;

		// Token: 0x04001650 RID: 5712
		private ModelScope scope;
	}
}
