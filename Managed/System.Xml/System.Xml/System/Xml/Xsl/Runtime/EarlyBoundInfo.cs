using System;
using System.Reflection;

namespace System.Xml.Xsl.Runtime
{
	// Token: 0x020005D0 RID: 1488
	internal sealed class EarlyBoundInfo
	{
		// Token: 0x06003AE8 RID: 15080 RVA: 0x0014CDA5 File Offset: 0x0014AFA5
		public EarlyBoundInfo(string namespaceUri, Type ebType)
		{
			this.namespaceUri = namespaceUri;
			this.constrInfo = ebType.GetConstructor(Type.EmptyTypes);
		}

		// Token: 0x17000BF5 RID: 3061
		// (get) Token: 0x06003AE9 RID: 15081 RVA: 0x0014CDC5 File Offset: 0x0014AFC5
		public string NamespaceUri
		{
			get
			{
				return this.namespaceUri;
			}
		}

		// Token: 0x17000BF6 RID: 3062
		// (get) Token: 0x06003AEA RID: 15082 RVA: 0x0014CDCD File Offset: 0x0014AFCD
		public Type EarlyBoundType
		{
			get
			{
				return this.constrInfo.DeclaringType;
			}
		}

		// Token: 0x06003AEB RID: 15083 RVA: 0x0014CDDA File Offset: 0x0014AFDA
		public object CreateObject()
		{
			return this.constrInfo.Invoke(new object[0]);
		}

		// Token: 0x06003AEC RID: 15084 RVA: 0x0014CDF0 File Offset: 0x0014AFF0
		public override bool Equals(object obj)
		{
			EarlyBoundInfo earlyBoundInfo = obj as EarlyBoundInfo;
			return earlyBoundInfo != null && this.namespaceUri == earlyBoundInfo.namespaceUri && this.constrInfo == earlyBoundInfo.constrInfo;
		}

		// Token: 0x06003AED RID: 15085 RVA: 0x0014CE2F File Offset: 0x0014B02F
		public override int GetHashCode()
		{
			return this.namespaceUri.GetHashCode();
		}

		// Token: 0x0400267A RID: 9850
		private string namespaceUri;

		// Token: 0x0400267B RID: 9851
		private ConstructorInfo constrInfo;
	}
}
