using System;
using System.Web.Compilation;

namespace System.Web.UI
{
	// Token: 0x020001AE RID: 430
	internal abstract class CodeBuilder : ControlBuilder
	{
		// Token: 0x060010B1 RID: 4273 RVA: 0x0002E0B3 File Offset: 0x0002C2B3
		public CodeBuilder(string code, bool isAssign, ILocation location)
		{
			this.code = code;
			this.isAssign = isAssign;
			base.Line = location.BeginLine;
			base.FileName = location.Filename;
			base.Location = location;
		}

		// Token: 0x060010B2 RID: 4274 RVA: 0x00003BEA File Offset: 0x00001DEA
		internal override object CreateInstance()
		{
			return null;
		}

		// Token: 0x1700055C RID: 1372
		// (get) Token: 0x060010B3 RID: 4275 RVA: 0x0002E0E8 File Offset: 0x0002C2E8
		// (set) Token: 0x060010B4 RID: 4276 RVA: 0x0002E0F0 File Offset: 0x0002C2F0
		internal string Code
		{
			get
			{
				return this.code;
			}
			set
			{
				this.code = value;
			}
		}

		// Token: 0x1700055D RID: 1373
		// (get) Token: 0x060010B5 RID: 4277 RVA: 0x0002E0F9 File Offset: 0x0002C2F9
		internal bool IsAssign
		{
			get
			{
				return this.isAssign;
			}
		}

		// Token: 0x0400138F RID: 5007
		private string code;

		// Token: 0x04001390 RID: 5008
		private bool isAssign;
	}
}
