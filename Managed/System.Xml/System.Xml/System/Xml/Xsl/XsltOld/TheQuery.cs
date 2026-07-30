using System;
using MS.Internal.Xml.XPath;

namespace System.Xml.Xsl.XsltOld
{
	// Token: 0x0200054D RID: 1357
	internal sealed class TheQuery
	{
		// Token: 0x17000B8F RID: 2959
		// (get) Token: 0x060036BE RID: 14014 RVA: 0x00132327 File Offset: 0x00130527
		internal CompiledXpathExpr CompiledQuery
		{
			get
			{
				return this._CompiledQuery;
			}
		}

		// Token: 0x060036BF RID: 14015 RVA: 0x0013232F File Offset: 0x0013052F
		internal TheQuery(CompiledXpathExpr compiledQuery, InputScopeManager manager)
		{
			this._CompiledQuery = compiledQuery;
			this._ScopeManager = manager.Clone();
		}

		// Token: 0x04002313 RID: 8979
		internal InputScopeManager _ScopeManager;

		// Token: 0x04002314 RID: 8980
		private CompiledXpathExpr _CompiledQuery;
	}
}
