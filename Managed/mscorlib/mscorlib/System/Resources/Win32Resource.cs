using System;
using System.IO;

namespace System.Resources
{
	// Token: 0x020002B6 RID: 694
	internal abstract class Win32Resource
	{
		// Token: 0x06001FB3 RID: 8115 RVA: 0x0007CB48 File Offset: 0x0007AD48
		internal Win32Resource(NameOrId type, NameOrId name, int language)
		{
			this.type = type;
			this.name = name;
			this.language = language;
		}

		// Token: 0x06001FB4 RID: 8116 RVA: 0x0007CB65 File Offset: 0x0007AD65
		internal Win32Resource(Win32ResourceType type, int name, int language)
		{
			this.type = new NameOrId((int)type);
			this.name = new NameOrId(name);
			this.language = language;
		}

		// Token: 0x17000459 RID: 1113
		// (get) Token: 0x06001FB5 RID: 8117 RVA: 0x0007CB8C File Offset: 0x0007AD8C
		public Win32ResourceType ResourceType
		{
			get
			{
				if (this.type.IsName)
				{
					return (Win32ResourceType)(-1);
				}
				return (Win32ResourceType)this.type.Id;
			}
		}

		// Token: 0x1700045A RID: 1114
		// (get) Token: 0x06001FB6 RID: 8118 RVA: 0x0007CBA8 File Offset: 0x0007ADA8
		public NameOrId Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x1700045B RID: 1115
		// (get) Token: 0x06001FB7 RID: 8119 RVA: 0x0007CBB0 File Offset: 0x0007ADB0
		public NameOrId Type
		{
			get
			{
				return this.type;
			}
		}

		// Token: 0x1700045C RID: 1116
		// (get) Token: 0x06001FB8 RID: 8120 RVA: 0x0007CBB8 File Offset: 0x0007ADB8
		public int Language
		{
			get
			{
				return this.language;
			}
		}

		// Token: 0x06001FB9 RID: 8121
		public abstract void WriteTo(Stream s);

		// Token: 0x06001FBA RID: 8122 RVA: 0x0007CBC0 File Offset: 0x0007ADC0
		public override string ToString()
		{
			return string.Concat(new object[] { "Win32Resource (Kind=", this.ResourceType, ", Name=", this.name, ")" });
		}

		// Token: 0x04001141 RID: 4417
		private NameOrId type;

		// Token: 0x04001142 RID: 4418
		private NameOrId name;

		// Token: 0x04001143 RID: 4419
		private int language;
	}
}
