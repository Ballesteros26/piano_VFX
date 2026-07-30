using System;

namespace System.Xml.Serialization
{
	// Token: 0x020002E7 RID: 743
	internal abstract class TypeMapping : Mapping
	{
		// Token: 0x17000556 RID: 1366
		// (get) Token: 0x06001BC4 RID: 7108 RVA: 0x00099E50 File Offset: 0x00098050
		// (set) Token: 0x06001BC5 RID: 7109 RVA: 0x00099E58 File Offset: 0x00098058
		internal bool ReferencedByTopLevelElement
		{
			get
			{
				return this.referencedByTopLevelElement;
			}
			set
			{
				this.referencedByTopLevelElement = value;
			}
		}

		// Token: 0x17000557 RID: 1367
		// (get) Token: 0x06001BC6 RID: 7110 RVA: 0x00099E61 File Offset: 0x00098061
		// (set) Token: 0x06001BC7 RID: 7111 RVA: 0x00099E73 File Offset: 0x00098073
		internal bool ReferencedByElement
		{
			get
			{
				return this.referencedByElement || this.referencedByTopLevelElement;
			}
			set
			{
				this.referencedByElement = value;
			}
		}

		// Token: 0x17000558 RID: 1368
		// (get) Token: 0x06001BC8 RID: 7112 RVA: 0x00099E7C File Offset: 0x0009807C
		// (set) Token: 0x06001BC9 RID: 7113 RVA: 0x00099E84 File Offset: 0x00098084
		internal string Namespace
		{
			get
			{
				return this.typeNs;
			}
			set
			{
				this.typeNs = value;
			}
		}

		// Token: 0x17000559 RID: 1369
		// (get) Token: 0x06001BCA RID: 7114 RVA: 0x00099E8D File Offset: 0x0009808D
		// (set) Token: 0x06001BCB RID: 7115 RVA: 0x00099E95 File Offset: 0x00098095
		internal string TypeName
		{
			get
			{
				return this.typeName;
			}
			set
			{
				this.typeName = value;
			}
		}

		// Token: 0x1700055A RID: 1370
		// (get) Token: 0x06001BCC RID: 7116 RVA: 0x00099E9E File Offset: 0x0009809E
		// (set) Token: 0x06001BCD RID: 7117 RVA: 0x00099EA6 File Offset: 0x000980A6
		internal TypeDesc TypeDesc
		{
			get
			{
				return this.typeDesc;
			}
			set
			{
				this.typeDesc = value;
			}
		}

		// Token: 0x1700055B RID: 1371
		// (get) Token: 0x06001BCE RID: 7118 RVA: 0x00099EAF File Offset: 0x000980AF
		// (set) Token: 0x06001BCF RID: 7119 RVA: 0x00099EB7 File Offset: 0x000980B7
		internal bool IncludeInSchema
		{
			get
			{
				return this.includeInSchema;
			}
			set
			{
				this.includeInSchema = value;
			}
		}

		// Token: 0x1700055C RID: 1372
		// (get) Token: 0x06001BD0 RID: 7120 RVA: 0x0000226C File Offset: 0x0000046C
		// (set) Token: 0x06001BD1 RID: 7121 RVA: 0x00002F50 File Offset: 0x00001150
		internal virtual bool IsList
		{
			get
			{
				return false;
			}
			set
			{
			}
		}

		// Token: 0x1700055D RID: 1373
		// (get) Token: 0x06001BD2 RID: 7122 RVA: 0x00099EC0 File Offset: 0x000980C0
		// (set) Token: 0x06001BD3 RID: 7123 RVA: 0x00099EC8 File Offset: 0x000980C8
		internal bool IsReference
		{
			get
			{
				return this.reference;
			}
			set
			{
				this.reference = value;
			}
		}

		// Token: 0x1700055E RID: 1374
		// (get) Token: 0x06001BD4 RID: 7124 RVA: 0x00099ED1 File Offset: 0x000980D1
		internal bool IsAnonymousType
		{
			get
			{
				return this.typeName == null || this.typeName.Length == 0;
			}
		}

		// Token: 0x1700055F RID: 1375
		// (get) Token: 0x06001BD5 RID: 7125 RVA: 0x00099EEB File Offset: 0x000980EB
		internal virtual string DefaultElementName
		{
			get
			{
				if (!this.IsAnonymousType)
				{
					return this.typeName;
				}
				return XmlConvert.EncodeLocalName(this.typeDesc.Name);
			}
		}

		// Token: 0x0400160A RID: 5642
		private TypeDesc typeDesc;

		// Token: 0x0400160B RID: 5643
		private string typeNs;

		// Token: 0x0400160C RID: 5644
		private string typeName;

		// Token: 0x0400160D RID: 5645
		private bool referencedByElement;

		// Token: 0x0400160E RID: 5646
		private bool referencedByTopLevelElement;

		// Token: 0x0400160F RID: 5647
		private bool includeInSchema = true;

		// Token: 0x04001610 RID: 5648
		private bool reference;
	}
}
