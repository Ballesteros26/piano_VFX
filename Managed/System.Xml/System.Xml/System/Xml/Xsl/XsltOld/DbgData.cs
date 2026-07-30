using System;
using System.Xml.XPath;

namespace System.Xml.Xsl.XsltOld
{
	// Token: 0x020004FE RID: 1278
	internal class DbgData
	{
		// Token: 0x17000B06 RID: 2822
		// (get) Token: 0x06003422 RID: 13346 RVA: 0x001293D7 File Offset: 0x001275D7
		public XPathNavigator StyleSheet
		{
			get
			{
				return this.styleSheet;
			}
		}

		// Token: 0x17000B07 RID: 2823
		// (get) Token: 0x06003423 RID: 13347 RVA: 0x001293DF File Offset: 0x001275DF
		public VariableAction[] Variables
		{
			get
			{
				return this.variables;
			}
		}

		// Token: 0x06003424 RID: 13348 RVA: 0x001293E8 File Offset: 0x001275E8
		public DbgData(Compiler compiler)
		{
			DbgCompiler dbgCompiler = (DbgCompiler)compiler;
			this.styleSheet = dbgCompiler.Input.Navigator.Clone();
			this.variables = dbgCompiler.LocalVariables;
			dbgCompiler.Debugger.OnInstructionCompile(this.StyleSheet);
		}

		// Token: 0x06003425 RID: 13349 RVA: 0x00129435 File Offset: 0x00127635
		internal void ReplaceVariables(VariableAction[] vars)
		{
			this.variables = vars;
		}

		// Token: 0x06003426 RID: 13350 RVA: 0x0012943E File Offset: 0x0012763E
		private DbgData()
		{
			this.styleSheet = null;
			this.variables = new VariableAction[0];
		}

		// Token: 0x17000B08 RID: 2824
		// (get) Token: 0x06003427 RID: 13351 RVA: 0x00129459 File Offset: 0x00127659
		public static DbgData Empty
		{
			get
			{
				return DbgData.s_nullDbgData;
			}
		}

		// Token: 0x04002185 RID: 8581
		private XPathNavigator styleSheet;

		// Token: 0x04002186 RID: 8582
		private VariableAction[] variables;

		// Token: 0x04002187 RID: 8583
		private static DbgData s_nullDbgData = new DbgData();
	}
}
