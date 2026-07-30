using System;
using System.Xml.Schema;

namespace System.Xml.Serialization
{
	// Token: 0x020002E0 RID: 736
	internal abstract class Accessor
	{
		// Token: 0x06001B90 RID: 7056 RVA: 0x000020FD File Offset: 0x000002FD
		internal Accessor()
		{
		}

		// Token: 0x17000542 RID: 1346
		// (get) Token: 0x06001B91 RID: 7057 RVA: 0x00099A98 File Offset: 0x00097C98
		// (set) Token: 0x06001B92 RID: 7058 RVA: 0x00099AA0 File Offset: 0x00097CA0
		internal TypeMapping Mapping
		{
			get
			{
				return this.mapping;
			}
			set
			{
				this.mapping = value;
			}
		}

		// Token: 0x17000543 RID: 1347
		// (get) Token: 0x06001B93 RID: 7059 RVA: 0x00099AA9 File Offset: 0x00097CA9
		// (set) Token: 0x06001B94 RID: 7060 RVA: 0x00099AB1 File Offset: 0x00097CB1
		internal object Default
		{
			get
			{
				return this.defaultValue;
			}
			set
			{
				this.defaultValue = value;
			}
		}

		// Token: 0x17000544 RID: 1348
		// (get) Token: 0x06001B95 RID: 7061 RVA: 0x00099ABA File Offset: 0x00097CBA
		internal bool HasDefault
		{
			get
			{
				return this.defaultValue != null && this.defaultValue != DBNull.Value;
			}
		}

		// Token: 0x17000545 RID: 1349
		// (get) Token: 0x06001B96 RID: 7062 RVA: 0x00099AD6 File Offset: 0x00097CD6
		// (set) Token: 0x06001B97 RID: 7063 RVA: 0x00099AEC File Offset: 0x00097CEC
		internal virtual string Name
		{
			get
			{
				if (this.name != null)
				{
					return this.name;
				}
				return string.Empty;
			}
			set
			{
				this.name = value;
			}
		}

		// Token: 0x17000546 RID: 1350
		// (get) Token: 0x06001B98 RID: 7064 RVA: 0x00099AF5 File Offset: 0x00097CF5
		// (set) Token: 0x06001B99 RID: 7065 RVA: 0x00099AFD File Offset: 0x00097CFD
		internal bool Any
		{
			get
			{
				return this.any;
			}
			set
			{
				this.any = value;
			}
		}

		// Token: 0x17000547 RID: 1351
		// (get) Token: 0x06001B9A RID: 7066 RVA: 0x00099B06 File Offset: 0x00097D06
		// (set) Token: 0x06001B9B RID: 7067 RVA: 0x00099B0E File Offset: 0x00097D0E
		internal string AnyNamespaces
		{
			get
			{
				return this.anyNs;
			}
			set
			{
				this.anyNs = value;
			}
		}

		// Token: 0x17000548 RID: 1352
		// (get) Token: 0x06001B9C RID: 7068 RVA: 0x00099B17 File Offset: 0x00097D17
		// (set) Token: 0x06001B9D RID: 7069 RVA: 0x00099B1F File Offset: 0x00097D1F
		internal string Namespace
		{
			get
			{
				return this.ns;
			}
			set
			{
				this.ns = value;
			}
		}

		// Token: 0x17000549 RID: 1353
		// (get) Token: 0x06001B9E RID: 7070 RVA: 0x00099B28 File Offset: 0x00097D28
		// (set) Token: 0x06001B9F RID: 7071 RVA: 0x00099B30 File Offset: 0x00097D30
		internal XmlSchemaForm Form
		{
			get
			{
				return this.form;
			}
			set
			{
				this.form = value;
			}
		}

		// Token: 0x1700054A RID: 1354
		// (get) Token: 0x06001BA0 RID: 7072 RVA: 0x00099B39 File Offset: 0x00097D39
		// (set) Token: 0x06001BA1 RID: 7073 RVA: 0x00099B41 File Offset: 0x00097D41
		internal bool IsFixed
		{
			get
			{
				return this.isFixed;
			}
			set
			{
				this.isFixed = value;
			}
		}

		// Token: 0x1700054B RID: 1355
		// (get) Token: 0x06001BA2 RID: 7074 RVA: 0x00099B4A File Offset: 0x00097D4A
		// (set) Token: 0x06001BA3 RID: 7075 RVA: 0x00099B52 File Offset: 0x00097D52
		internal bool IsOptional
		{
			get
			{
				return this.isOptional;
			}
			set
			{
				this.isOptional = value;
			}
		}

		// Token: 0x1700054C RID: 1356
		// (get) Token: 0x06001BA4 RID: 7076 RVA: 0x00099B5B File Offset: 0x00097D5B
		// (set) Token: 0x06001BA5 RID: 7077 RVA: 0x00099B63 File Offset: 0x00097D63
		internal bool IsTopLevelInSchema
		{
			get
			{
				return this.topLevelInSchema;
			}
			set
			{
				this.topLevelInSchema = value;
			}
		}

		// Token: 0x06001BA6 RID: 7078 RVA: 0x00099B6C File Offset: 0x00097D6C
		internal static string EscapeName(string name)
		{
			if (name == null || name.Length == 0)
			{
				return name;
			}
			return XmlConvert.EncodeLocalName(name);
		}

		// Token: 0x06001BA7 RID: 7079 RVA: 0x00099B84 File Offset: 0x00097D84
		internal static string EscapeQName(string name)
		{
			if (name == null || name.Length == 0)
			{
				return name;
			}
			int num = name.LastIndexOf(':');
			if (num < 0)
			{
				return XmlConvert.EncodeLocalName(name);
			}
			if (num == 0 || num == name.Length - 1)
			{
				throw new ArgumentException(Res.GetString("Invalid name character in '{0}'.", new object[] { name }), "name");
			}
			return new XmlQualifiedName(XmlConvert.EncodeLocalName(name.Substring(num + 1)), XmlConvert.EncodeLocalName(name.Substring(0, num))).ToString();
		}

		// Token: 0x06001BA8 RID: 7080 RVA: 0x00099C04 File Offset: 0x00097E04
		internal static string UnescapeName(string name)
		{
			return XmlConvert.DecodeName(name);
		}

		// Token: 0x06001BA9 RID: 7081 RVA: 0x00099C0C File Offset: 0x00097E0C
		internal string ToString(string defaultNs)
		{
			if (this.Any)
			{
				return ((this.Namespace == null) ? "##any" : this.Namespace) + ":" + this.Name;
			}
			if (!(this.Namespace == defaultNs))
			{
				return this.Namespace + ":" + this.Name;
			}
			return this.Name;
		}

		// Token: 0x040015F7 RID: 5623
		private string name;

		// Token: 0x040015F8 RID: 5624
		private object defaultValue;

		// Token: 0x040015F9 RID: 5625
		private string ns;

		// Token: 0x040015FA RID: 5626
		private TypeMapping mapping;

		// Token: 0x040015FB RID: 5627
		private bool any;

		// Token: 0x040015FC RID: 5628
		private string anyNs;

		// Token: 0x040015FD RID: 5629
		private bool topLevelInSchema;

		// Token: 0x040015FE RID: 5630
		private bool isFixed;

		// Token: 0x040015FF RID: 5631
		private bool isOptional;

		// Token: 0x04001600 RID: 5632
		private XmlSchemaForm form;
	}
}
