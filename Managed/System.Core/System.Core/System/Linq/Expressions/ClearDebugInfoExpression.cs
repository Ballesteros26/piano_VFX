using System;

namespace System.Linq.Expressions
{
	// Token: 0x0200025C RID: 604
	internal sealed class ClearDebugInfoExpression : DebugInfoExpression
	{
		// Token: 0x06001096 RID: 4246 RVA: 0x00035F86 File Offset: 0x00034186
		internal ClearDebugInfoExpression(SymbolDocumentInfo document)
			: base(document)
		{
		}

		// Token: 0x170002DF RID: 735
		// (get) Token: 0x06001097 RID: 4247 RVA: 0x0000AA13 File Offset: 0x00008C13
		public override bool IsClear
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170002E0 RID: 736
		// (get) Token: 0x06001098 RID: 4248 RVA: 0x00035F8F File Offset: 0x0003418F
		public override int StartLine
		{
			get
			{
				return 16707566;
			}
		}

		// Token: 0x170002E1 RID: 737
		// (get) Token: 0x06001099 RID: 4249 RVA: 0x00002285 File Offset: 0x00000485
		public override int StartColumn
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x170002E2 RID: 738
		// (get) Token: 0x0600109A RID: 4250 RVA: 0x00035F8F File Offset: 0x0003418F
		public override int EndLine
		{
			get
			{
				return 16707566;
			}
		}

		// Token: 0x170002E3 RID: 739
		// (get) Token: 0x0600109B RID: 4251 RVA: 0x00002285 File Offset: 0x00000485
		public override int EndColumn
		{
			get
			{
				return 0;
			}
		}
	}
}
