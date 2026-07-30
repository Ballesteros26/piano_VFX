using System;
using System.Xml.Schema;

namespace System.Xml
{
	// Token: 0x0200022C RID: 556
	internal class XmlName : IXmlSchemaInfo
	{
		// Token: 0x0600150D RID: 5389 RVA: 0x00077B40 File Offset: 0x00075D40
		public static XmlName Create(string prefix, string localName, string ns, int hashCode, XmlDocument ownerDoc, XmlName next, IXmlSchemaInfo schemaInfo)
		{
			if (schemaInfo == null)
			{
				return new XmlName(prefix, localName, ns, hashCode, ownerDoc, next);
			}
			return new XmlNameEx(prefix, localName, ns, hashCode, ownerDoc, next, schemaInfo);
		}

		// Token: 0x0600150E RID: 5390 RVA: 0x00077B63 File Offset: 0x00075D63
		internal XmlName(string prefix, string localName, string ns, int hashCode, XmlDocument ownerDoc, XmlName next)
		{
			this.prefix = prefix;
			this.localName = localName;
			this.ns = ns;
			this.name = null;
			this.hashCode = hashCode;
			this.ownerDoc = ownerDoc;
			this.next = next;
		}

		// Token: 0x170003EB RID: 1003
		// (get) Token: 0x0600150F RID: 5391 RVA: 0x00077B9F File Offset: 0x00075D9F
		public string LocalName
		{
			get
			{
				return this.localName;
			}
		}

		// Token: 0x170003EC RID: 1004
		// (get) Token: 0x06001510 RID: 5392 RVA: 0x00077BA7 File Offset: 0x00075DA7
		public string NamespaceURI
		{
			get
			{
				return this.ns;
			}
		}

		// Token: 0x170003ED RID: 1005
		// (get) Token: 0x06001511 RID: 5393 RVA: 0x00077BAF File Offset: 0x00075DAF
		public string Prefix
		{
			get
			{
				return this.prefix;
			}
		}

		// Token: 0x170003EE RID: 1006
		// (get) Token: 0x06001512 RID: 5394 RVA: 0x00077BB7 File Offset: 0x00075DB7
		public int HashCode
		{
			get
			{
				return this.hashCode;
			}
		}

		// Token: 0x170003EF RID: 1007
		// (get) Token: 0x06001513 RID: 5395 RVA: 0x00077BBF File Offset: 0x00075DBF
		public XmlDocument OwnerDocument
		{
			get
			{
				return this.ownerDoc;
			}
		}

		// Token: 0x170003F0 RID: 1008
		// (get) Token: 0x06001514 RID: 5396 RVA: 0x00077BC8 File Offset: 0x00075DC8
		public string Name
		{
			get
			{
				if (this.name == null)
				{
					if (this.prefix.Length > 0)
					{
						if (this.localName.Length > 0)
						{
							string text = this.prefix + ":" + this.localName;
							XmlNameTable nameTable = this.ownerDoc.NameTable;
							lock (nameTable)
							{
								if (this.name == null)
								{
									this.name = this.ownerDoc.NameTable.Add(text);
								}
								goto IL_0099;
							}
						}
						this.name = this.prefix;
					}
					else
					{
						this.name = this.localName;
					}
				}
				IL_0099:
				return this.name;
			}
		}

		// Token: 0x170003F1 RID: 1009
		// (get) Token: 0x06001515 RID: 5397 RVA: 0x0000226C File Offset: 0x0000046C
		public virtual XmlSchemaValidity Validity
		{
			get
			{
				return XmlSchemaValidity.NotKnown;
			}
		}

		// Token: 0x170003F2 RID: 1010
		// (get) Token: 0x06001516 RID: 5398 RVA: 0x0000226C File Offset: 0x0000046C
		public virtual bool IsDefault
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170003F3 RID: 1011
		// (get) Token: 0x06001517 RID: 5399 RVA: 0x0000226C File Offset: 0x0000046C
		public virtual bool IsNil
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170003F4 RID: 1012
		// (get) Token: 0x06001518 RID: 5400 RVA: 0x0000365F File Offset: 0x0000185F
		public virtual XmlSchemaSimpleType MemberType
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170003F5 RID: 1013
		// (get) Token: 0x06001519 RID: 5401 RVA: 0x0000365F File Offset: 0x0000185F
		public virtual XmlSchemaType SchemaType
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170003F6 RID: 1014
		// (get) Token: 0x0600151A RID: 5402 RVA: 0x0000365F File Offset: 0x0000185F
		public virtual XmlSchemaElement SchemaElement
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170003F7 RID: 1015
		// (get) Token: 0x0600151B RID: 5403 RVA: 0x0000365F File Offset: 0x0000185F
		public virtual XmlSchemaAttribute SchemaAttribute
		{
			get
			{
				return null;
			}
		}

		// Token: 0x0600151C RID: 5404 RVA: 0x00077C84 File Offset: 0x00075E84
		public virtual bool Equals(IXmlSchemaInfo schemaInfo)
		{
			return schemaInfo == null;
		}

		// Token: 0x0600151D RID: 5405 RVA: 0x00077C8C File Offset: 0x00075E8C
		public static int GetHashCode(string name)
		{
			int num = 0;
			if (name != null)
			{
				for (int i = name.Length - 1; i >= 0; i--)
				{
					char c = name[i];
					if (c == ':')
					{
						break;
					}
					num += (num << 7) ^ (int)c;
				}
				num -= num >> 17;
				num -= num >> 11;
				num -= num >> 5;
			}
			return num;
		}

		// Token: 0x04000DE9 RID: 3561
		private string prefix;

		// Token: 0x04000DEA RID: 3562
		private string localName;

		// Token: 0x04000DEB RID: 3563
		private string ns;

		// Token: 0x04000DEC RID: 3564
		private string name;

		// Token: 0x04000DED RID: 3565
		private int hashCode;

		// Token: 0x04000DEE RID: 3566
		internal XmlDocument ownerDoc;

		// Token: 0x04000DEF RID: 3567
		internal XmlName next;
	}
}
