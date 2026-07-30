using System;
using System.Xml.Schema;

namespace System.Xml.Serialization
{
	// Token: 0x020002E5 RID: 741
	internal class AttributeAccessor : Accessor
	{
		// Token: 0x17000553 RID: 1363
		// (get) Token: 0x06001BBB RID: 7099 RVA: 0x00099D61 File Offset: 0x00097F61
		internal bool IsSpecialXmlNamespace
		{
			get
			{
				return this.isSpecial;
			}
		}

		// Token: 0x17000554 RID: 1364
		// (get) Token: 0x06001BBC RID: 7100 RVA: 0x00099D69 File Offset: 0x00097F69
		// (set) Token: 0x06001BBD RID: 7101 RVA: 0x00099D71 File Offset: 0x00097F71
		internal bool IsList
		{
			get
			{
				return this.isList;
			}
			set
			{
				this.isList = value;
			}
		}

		// Token: 0x06001BBE RID: 7102 RVA: 0x00099D7C File Offset: 0x00097F7C
		internal void CheckSpecial()
		{
			if (this.Name.LastIndexOf(':') >= 0)
			{
				if (!this.Name.StartsWith("xml:", StringComparison.Ordinal))
				{
					throw new InvalidOperationException(Res.GetString("Invalid name character in '{0}'.", new object[] { this.Name }));
				}
				this.Name = this.Name.Substring("xml:".Length);
				base.Namespace = "http://www.w3.org/XML/1998/namespace";
				this.isSpecial = true;
			}
			else if (base.Namespace == "http://www.w3.org/XML/1998/namespace")
			{
				this.isSpecial = true;
			}
			else
			{
				this.isSpecial = false;
			}
			if (this.isSpecial)
			{
				base.Form = XmlSchemaForm.Qualified;
			}
		}

		// Token: 0x04001607 RID: 5639
		private bool isSpecial;

		// Token: 0x04001608 RID: 5640
		private bool isList;
	}
}
