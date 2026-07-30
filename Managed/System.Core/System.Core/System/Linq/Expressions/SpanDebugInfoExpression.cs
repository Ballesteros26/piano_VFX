using System;

namespace System.Linq.Expressions
{
	// Token: 0x0200025B RID: 603
	internal sealed class SpanDebugInfoExpression : DebugInfoExpression
	{
		// Token: 0x0600108F RID: 4239 RVA: 0x00035F3F File Offset: 0x0003413F
		internal SpanDebugInfoExpression(SymbolDocumentInfo document, int startLine, int startColumn, int endLine, int endColumn)
			: base(document)
		{
			this._startLine = startLine;
			this._startColumn = startColumn;
			this._endLine = endLine;
			this._endColumn = endColumn;
		}

		// Token: 0x170002DA RID: 730
		// (get) Token: 0x06001090 RID: 4240 RVA: 0x00035F66 File Offset: 0x00034166
		public override int StartLine
		{
			get
			{
				return this._startLine;
			}
		}

		// Token: 0x170002DB RID: 731
		// (get) Token: 0x06001091 RID: 4241 RVA: 0x00035F6E File Offset: 0x0003416E
		public override int StartColumn
		{
			get
			{
				return this._startColumn;
			}
		}

		// Token: 0x170002DC RID: 732
		// (get) Token: 0x06001092 RID: 4242 RVA: 0x00035F76 File Offset: 0x00034176
		public override int EndLine
		{
			get
			{
				return this._endLine;
			}
		}

		// Token: 0x170002DD RID: 733
		// (get) Token: 0x06001093 RID: 4243 RVA: 0x00035F7E File Offset: 0x0003417E
		public override int EndColumn
		{
			get
			{
				return this._endColumn;
			}
		}

		// Token: 0x170002DE RID: 734
		// (get) Token: 0x06001094 RID: 4244 RVA: 0x00002285 File Offset: 0x00000485
		public override bool IsClear
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06001095 RID: 4245 RVA: 0x00035F36 File Offset: 0x00034136
		protected internal override Expression Accept(ExpressionVisitor visitor)
		{
			return visitor.VisitDebugInfo(this);
		}

		// Token: 0x040008D8 RID: 2264
		private readonly int _startLine;

		// Token: 0x040008D9 RID: 2265
		private readonly int _startColumn;

		// Token: 0x040008DA RID: 2266
		private readonly int _endLine;

		// Token: 0x040008DB RID: 2267
		private readonly int _endColumn;
	}
}
