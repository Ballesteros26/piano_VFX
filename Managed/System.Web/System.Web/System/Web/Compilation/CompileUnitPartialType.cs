using System;
using System.CodeDom;

namespace System.Web.Compilation
{
	// Token: 0x02000631 RID: 1585
	internal class CompileUnitPartialType
	{
		// Token: 0x17001559 RID: 5465
		// (get) Token: 0x060043EB RID: 17387 RVA: 0x000B7928 File Offset: 0x000B5B28
		public string TypeName
		{
			get
			{
				if (this.typeName == null)
				{
					if (this.ParentNamespace == null || this.PartialType == null)
					{
						return null;
					}
					this.typeName = this.ParentNamespace.Name;
					if (string.IsNullOrEmpty(this.typeName))
					{
						this.typeName = this.PartialType.Name;
					}
					else
					{
						this.typeName = this.typeName + "." + this.PartialType.Name;
					}
				}
				return this.typeName;
			}
		}

		// Token: 0x060043EC RID: 17388 RVA: 0x000B79A7 File Offset: 0x000B5BA7
		public CompileUnitPartialType(CodeCompileUnit unit, CodeNamespace parentNamespace, CodeTypeDeclaration type)
		{
			this.Unit = unit;
			this.ParentNamespace = parentNamespace;
			this.PartialType = type;
		}

		// Token: 0x0400245D RID: 9309
		public readonly CodeCompileUnit Unit;

		// Token: 0x0400245E RID: 9310
		public readonly CodeNamespace ParentNamespace;

		// Token: 0x0400245F RID: 9311
		public readonly CodeTypeDeclaration PartialType;

		// Token: 0x04002460 RID: 9312
		private string typeName;
	}
}
