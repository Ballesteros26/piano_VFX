using System;
using System.Collections;
using System.Xml.XPath;

namespace MS.Internal.Xml.XPath
{
	// Token: 0x02000023 RID: 35
	internal class Function : AstNode
	{
		// Token: 0x060000DB RID: 219 RVA: 0x0000408C File Offset: 0x0000228C
		public Function(Function.FunctionType ftype, ArrayList argumentList)
		{
			this.functionType = ftype;
			this.argumentList = new ArrayList(argumentList);
		}

		// Token: 0x060000DC RID: 220 RVA: 0x000040A7 File Offset: 0x000022A7
		public Function(string prefix, string name, ArrayList argumentList)
		{
			this.functionType = Function.FunctionType.FuncUserDefined;
			this.prefix = prefix;
			this.name = name;
			this.argumentList = new ArrayList(argumentList);
		}

		// Token: 0x060000DD RID: 221 RVA: 0x000040D1 File Offset: 0x000022D1
		public Function(Function.FunctionType ftype)
		{
			this.functionType = ftype;
		}

		// Token: 0x060000DE RID: 222 RVA: 0x000040E0 File Offset: 0x000022E0
		public Function(Function.FunctionType ftype, AstNode arg)
		{
			this.functionType = ftype;
			this.argumentList = new ArrayList();
			this.argumentList.Add(arg);
		}

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x060000DF RID: 223 RVA: 0x00004107 File Offset: 0x00002307
		public override AstNode.AstType Type
		{
			get
			{
				return AstNode.AstType.Function;
			}
		}

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x060000E0 RID: 224 RVA: 0x0000410A File Offset: 0x0000230A
		public override XPathResultType ReturnType
		{
			get
			{
				return Function.ReturnTypes[(int)this.functionType];
			}
		}

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x060000E1 RID: 225 RVA: 0x00004118 File Offset: 0x00002318
		public Function.FunctionType TypeOfFunction
		{
			get
			{
				return this.functionType;
			}
		}

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x060000E2 RID: 226 RVA: 0x00004120 File Offset: 0x00002320
		public ArrayList ArgumentList
		{
			get
			{
				return this.argumentList;
			}
		}

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x060000E3 RID: 227 RVA: 0x00004128 File Offset: 0x00002328
		public string Prefix
		{
			get
			{
				return this.prefix;
			}
		}

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x060000E4 RID: 228 RVA: 0x00004130 File Offset: 0x00002330
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x04000089 RID: 137
		private Function.FunctionType functionType;

		// Token: 0x0400008A RID: 138
		private ArrayList argumentList;

		// Token: 0x0400008B RID: 139
		private string name;

		// Token: 0x0400008C RID: 140
		private string prefix;

		// Token: 0x0400008D RID: 141
		internal static XPathResultType[] ReturnTypes = new XPathResultType[]
		{
			XPathResultType.Number,
			XPathResultType.Number,
			XPathResultType.Number,
			XPathResultType.NodeSet,
			XPathResultType.String,
			XPathResultType.String,
			XPathResultType.String,
			XPathResultType.String,
			XPathResultType.Boolean,
			XPathResultType.Number,
			XPathResultType.Boolean,
			XPathResultType.Boolean,
			XPathResultType.Boolean,
			XPathResultType.String,
			XPathResultType.Boolean,
			XPathResultType.Boolean,
			XPathResultType.String,
			XPathResultType.String,
			XPathResultType.String,
			XPathResultType.Number,
			XPathResultType.String,
			XPathResultType.String,
			XPathResultType.Boolean,
			XPathResultType.Number,
			XPathResultType.Number,
			XPathResultType.Number,
			XPathResultType.Number,
			XPathResultType.Any
		};

		// Token: 0x02000024 RID: 36
		public enum FunctionType
		{
			// Token: 0x0400008F RID: 143
			FuncLast,
			// Token: 0x04000090 RID: 144
			FuncPosition,
			// Token: 0x04000091 RID: 145
			FuncCount,
			// Token: 0x04000092 RID: 146
			FuncID,
			// Token: 0x04000093 RID: 147
			FuncLocalName,
			// Token: 0x04000094 RID: 148
			FuncNameSpaceUri,
			// Token: 0x04000095 RID: 149
			FuncName,
			// Token: 0x04000096 RID: 150
			FuncString,
			// Token: 0x04000097 RID: 151
			FuncBoolean,
			// Token: 0x04000098 RID: 152
			FuncNumber,
			// Token: 0x04000099 RID: 153
			FuncTrue,
			// Token: 0x0400009A RID: 154
			FuncFalse,
			// Token: 0x0400009B RID: 155
			FuncNot,
			// Token: 0x0400009C RID: 156
			FuncConcat,
			// Token: 0x0400009D RID: 157
			FuncStartsWith,
			// Token: 0x0400009E RID: 158
			FuncContains,
			// Token: 0x0400009F RID: 159
			FuncSubstringBefore,
			// Token: 0x040000A0 RID: 160
			FuncSubstringAfter,
			// Token: 0x040000A1 RID: 161
			FuncSubstring,
			// Token: 0x040000A2 RID: 162
			FuncStringLength,
			// Token: 0x040000A3 RID: 163
			FuncNormalize,
			// Token: 0x040000A4 RID: 164
			FuncTranslate,
			// Token: 0x040000A5 RID: 165
			FuncLang,
			// Token: 0x040000A6 RID: 166
			FuncSum,
			// Token: 0x040000A7 RID: 167
			FuncFloor,
			// Token: 0x040000A8 RID: 168
			FuncCeiling,
			// Token: 0x040000A9 RID: 169
			FuncRound,
			// Token: 0x040000AA RID: 170
			FuncUserDefined
		}
	}
}
