using System;

namespace System.Web
{
	// Token: 0x02000052 RID: 82
	internal class ModuleConfigurationInfo
	{
		// Token: 0x060003D3 RID: 979 RVA: 0x000072CE File Offset: 0x000054CE
		internal ModuleConfigurationInfo(string name, string type, string condition)
		{
			this._type = type;
			this._name = name;
			this._precondition = condition;
		}

		// Token: 0x170001B3 RID: 435
		// (get) Token: 0x060003D4 RID: 980 RVA: 0x000072EB File Offset: 0x000054EB
		internal string Type
		{
			get
			{
				return this._type;
			}
		}

		// Token: 0x170001B4 RID: 436
		// (get) Token: 0x060003D5 RID: 981 RVA: 0x000072F3 File Offset: 0x000054F3
		internal string Name
		{
			get
			{
				return this._name;
			}
		}

		// Token: 0x170001B5 RID: 437
		// (get) Token: 0x060003D6 RID: 982 RVA: 0x000072FB File Offset: 0x000054FB
		internal string Precondition
		{
			get
			{
				return this._precondition;
			}
		}

		// Token: 0x04000E0F RID: 3599
		private string _type;

		// Token: 0x04000E10 RID: 3600
		private string _name;

		// Token: 0x04000E11 RID: 3601
		private string _precondition;
	}
}
