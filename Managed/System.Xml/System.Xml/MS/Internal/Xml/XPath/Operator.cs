using System;
using System.Xml.XPath;

namespace MS.Internal.Xml.XPath
{
	// Token: 0x02000034 RID: 52
	internal class Operator : AstNode
	{
		// Token: 0x06000169 RID: 361 RVA: 0x00005949 File Offset: 0x00003B49
		public static Operator.Op InvertOperator(Operator.Op op)
		{
			return Operator.invertOp[(int)op];
		}

		// Token: 0x0600016A RID: 362 RVA: 0x00005952 File Offset: 0x00003B52
		public Operator(Operator.Op op, AstNode opnd1, AstNode opnd2)
		{
			this.opType = op;
			this.opnd1 = opnd1;
			this.opnd2 = opnd2;
		}

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x0600016B RID: 363 RVA: 0x00003242 File Offset: 0x00001442
		public override AstNode.AstType Type
		{
			get
			{
				return AstNode.AstType.Operator;
			}
		}

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x0600016C RID: 364 RVA: 0x0000596F File Offset: 0x00003B6F
		public override XPathResultType ReturnType
		{
			get
			{
				if (this.opType <= Operator.Op.GE)
				{
					return XPathResultType.Boolean;
				}
				if (this.opType <= Operator.Op.MOD)
				{
					return XPathResultType.Number;
				}
				return XPathResultType.NodeSet;
			}
		}

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x0600016D RID: 365 RVA: 0x00005989 File Offset: 0x00003B89
		public Operator.Op OperatorType
		{
			get
			{
				return this.opType;
			}
		}

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x0600016E RID: 366 RVA: 0x00005991 File Offset: 0x00003B91
		public AstNode Operand1
		{
			get
			{
				return this.opnd1;
			}
		}

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x0600016F RID: 367 RVA: 0x00005999 File Offset: 0x00003B99
		public AstNode Operand2
		{
			get
			{
				return this.opnd2;
			}
		}

		// Token: 0x040000C5 RID: 197
		private static Operator.Op[] invertOp = new Operator.Op[]
		{
			Operator.Op.INVALID,
			Operator.Op.INVALID,
			Operator.Op.INVALID,
			Operator.Op.EQ,
			Operator.Op.NE,
			Operator.Op.GT,
			Operator.Op.GE,
			Operator.Op.LT,
			Operator.Op.LE
		};

		// Token: 0x040000C6 RID: 198
		private Operator.Op opType;

		// Token: 0x040000C7 RID: 199
		private AstNode opnd1;

		// Token: 0x040000C8 RID: 200
		private AstNode opnd2;

		// Token: 0x02000035 RID: 53
		public enum Op
		{
			// Token: 0x040000CA RID: 202
			INVALID,
			// Token: 0x040000CB RID: 203
			OR,
			// Token: 0x040000CC RID: 204
			AND,
			// Token: 0x040000CD RID: 205
			EQ,
			// Token: 0x040000CE RID: 206
			NE,
			// Token: 0x040000CF RID: 207
			LT,
			// Token: 0x040000D0 RID: 208
			LE,
			// Token: 0x040000D1 RID: 209
			GT,
			// Token: 0x040000D2 RID: 210
			GE,
			// Token: 0x040000D3 RID: 211
			PLUS,
			// Token: 0x040000D4 RID: 212
			MINUS,
			// Token: 0x040000D5 RID: 213
			MUL,
			// Token: 0x040000D6 RID: 214
			DIV,
			// Token: 0x040000D7 RID: 215
			MOD,
			// Token: 0x040000D8 RID: 216
			UNION
		}
	}
}
