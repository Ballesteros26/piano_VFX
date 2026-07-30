using System;
using System.Xml;
using System.Xml.XPath;
using System.Xml.Xsl;

namespace MS.Internal.Xml.XPath
{
	// Token: 0x0200002A RID: 42
	internal sealed class LogicalExpr : ValueQuery
	{
		// Token: 0x06000105 RID: 261 RVA: 0x000046E2 File Offset: 0x000028E2
		public LogicalExpr(Operator.Op op, Query opnd1, Query opnd2)
		{
			this.op = op;
			this.opnd1 = opnd1;
			this.opnd2 = opnd2;
		}

		// Token: 0x06000106 RID: 262 RVA: 0x000046FF File Offset: 0x000028FF
		private LogicalExpr(LogicalExpr other)
			: base(other)
		{
			this.op = other.op;
			this.opnd1 = Query.Clone(other.opnd1);
			this.opnd2 = Query.Clone(other.opnd2);
		}

		// Token: 0x06000107 RID: 263 RVA: 0x00004736 File Offset: 0x00002936
		public override void SetXsltContext(XsltContext context)
		{
			this.opnd1.SetXsltContext(context);
			this.opnd2.SetXsltContext(context);
		}

		// Token: 0x06000108 RID: 264 RVA: 0x00004750 File Offset: 0x00002950
		public override object Evaluate(XPathNodeIterator nodeIterator)
		{
			Operator.Op op = this.op;
			object obj = this.opnd1.Evaluate(nodeIterator);
			object obj2 = this.opnd2.Evaluate(nodeIterator);
			int num = (int)base.GetXPathType(obj);
			int num2 = (int)base.GetXPathType(obj2);
			if (num < num2)
			{
				op = Operator.InvertOperator(op);
				object obj3 = obj;
				obj = obj2;
				obj2 = obj3;
				int num3 = num;
				num = num2;
				num2 = num3;
			}
			if (op == Operator.Op.EQ || op == Operator.Op.NE)
			{
				return LogicalExpr.CompXsltE[num][num2](op, obj, obj2);
			}
			return LogicalExpr.CompXsltO[num][num2](op, obj, obj2);
		}

		// Token: 0x06000109 RID: 265 RVA: 0x000047DC File Offset: 0x000029DC
		private static bool cmpQueryQueryE(Operator.Op op, object val1, object val2)
		{
			bool flag = op == Operator.Op.EQ;
			LogicalExpr.NodeSet nodeSet = new LogicalExpr.NodeSet(val1);
			LogicalExpr.NodeSet nodeSet2 = new LogicalExpr.NodeSet(val2);
			IL_0015:
			while (nodeSet.MoveNext())
			{
				if (!nodeSet2.MoveNext())
				{
					return false;
				}
				string value = nodeSet.Value;
				while (value == nodeSet2.Value != flag)
				{
					if (!nodeSet2.MoveNext())
					{
						nodeSet2.Reset();
						goto IL_0015;
					}
				}
				return true;
			}
			return false;
		}

		// Token: 0x0600010A RID: 266 RVA: 0x00004840 File Offset: 0x00002A40
		private static bool cmpQueryQueryO(Operator.Op op, object val1, object val2)
		{
			LogicalExpr.NodeSet nodeSet = new LogicalExpr.NodeSet(val1);
			LogicalExpr.NodeSet nodeSet2 = new LogicalExpr.NodeSet(val2);
			IL_0010:
			while (nodeSet.MoveNext())
			{
				if (!nodeSet2.MoveNext())
				{
					return false;
				}
				double num = NumberFunctions.Number(nodeSet.Value);
				while (!LogicalExpr.cmpNumberNumber(op, num, NumberFunctions.Number(nodeSet2.Value)))
				{
					if (!nodeSet2.MoveNext())
					{
						nodeSet2.Reset();
						goto IL_0010;
					}
				}
				return true;
			}
			return false;
		}

		// Token: 0x0600010B RID: 267 RVA: 0x000048A8 File Offset: 0x00002AA8
		private static bool cmpQueryNumber(Operator.Op op, object val1, object val2)
		{
			LogicalExpr.NodeSet nodeSet = new LogicalExpr.NodeSet(val1);
			double num = (double)val2;
			while (nodeSet.MoveNext())
			{
				if (LogicalExpr.cmpNumberNumber(op, NumberFunctions.Number(nodeSet.Value), num))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600010C RID: 268 RVA: 0x000048E8 File Offset: 0x00002AE8
		private static bool cmpQueryStringE(Operator.Op op, object val1, object val2)
		{
			LogicalExpr.NodeSet nodeSet = new LogicalExpr.NodeSet(val1);
			string text = (string)val2;
			while (nodeSet.MoveNext())
			{
				if (LogicalExpr.cmpStringStringE(op, nodeSet.Value, text))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600010D RID: 269 RVA: 0x00004924 File Offset: 0x00002B24
		private static bool cmpQueryStringO(Operator.Op op, object val1, object val2)
		{
			LogicalExpr.NodeSet nodeSet = new LogicalExpr.NodeSet(val1);
			double num = NumberFunctions.Number((string)val2);
			while (nodeSet.MoveNext())
			{
				if (LogicalExpr.cmpNumberNumberO(op, NumberFunctions.Number(nodeSet.Value), num))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600010E RID: 270 RVA: 0x00004968 File Offset: 0x00002B68
		private static bool cmpRtfQueryE(Operator.Op op, object val1, object val2)
		{
			string text = LogicalExpr.Rtf(val1);
			LogicalExpr.NodeSet nodeSet = new LogicalExpr.NodeSet(val2);
			while (nodeSet.MoveNext())
			{
				if (LogicalExpr.cmpStringStringE(op, text, nodeSet.Value))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600010F RID: 271 RVA: 0x000049A4 File Offset: 0x00002BA4
		private static bool cmpRtfQueryO(Operator.Op op, object val1, object val2)
		{
			double num = NumberFunctions.Number(LogicalExpr.Rtf(val1));
			LogicalExpr.NodeSet nodeSet = new LogicalExpr.NodeSet(val2);
			while (nodeSet.MoveNext())
			{
				if (LogicalExpr.cmpNumberNumberO(op, num, NumberFunctions.Number(nodeSet.Value)))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000110 RID: 272 RVA: 0x000049E8 File Offset: 0x00002BE8
		private static bool cmpQueryBoolE(Operator.Op op, object val1, object val2)
		{
			LogicalExpr.NodeSet nodeSet = new LogicalExpr.NodeSet(val1);
			bool flag = nodeSet.MoveNext();
			bool flag2 = (bool)val2;
			return LogicalExpr.cmpBoolBoolE(op, flag, flag2);
		}

		// Token: 0x06000111 RID: 273 RVA: 0x00004A14 File Offset: 0x00002C14
		private static bool cmpQueryBoolO(Operator.Op op, object val1, object val2)
		{
			LogicalExpr.NodeSet nodeSet = new LogicalExpr.NodeSet(val1);
			double num = (nodeSet.MoveNext() ? 1.0 : 0.0);
			double num2 = NumberFunctions.Number((bool)val2);
			return LogicalExpr.cmpNumberNumberO(op, num, num2);
		}

		// Token: 0x06000112 RID: 274 RVA: 0x00004A5B File Offset: 0x00002C5B
		private static bool cmpBoolBoolE(Operator.Op op, bool n1, bool n2)
		{
			return op == Operator.Op.EQ == (n1 == n2);
		}

		// Token: 0x06000113 RID: 275 RVA: 0x00004A68 File Offset: 0x00002C68
		private static bool cmpBoolBoolE(Operator.Op op, object val1, object val2)
		{
			bool flag = (bool)val1;
			bool flag2 = (bool)val2;
			return LogicalExpr.cmpBoolBoolE(op, flag, flag2);
		}

		// Token: 0x06000114 RID: 276 RVA: 0x00004A8C File Offset: 0x00002C8C
		private static bool cmpBoolBoolO(Operator.Op op, object val1, object val2)
		{
			double num = NumberFunctions.Number((bool)val1);
			double num2 = NumberFunctions.Number((bool)val2);
			return LogicalExpr.cmpNumberNumberO(op, num, num2);
		}

		// Token: 0x06000115 RID: 277 RVA: 0x00004ABC File Offset: 0x00002CBC
		private static bool cmpBoolNumberE(Operator.Op op, object val1, object val2)
		{
			bool flag = (bool)val1;
			bool flag2 = BooleanFunctions.toBoolean((double)val2);
			return LogicalExpr.cmpBoolBoolE(op, flag, flag2);
		}

		// Token: 0x06000116 RID: 278 RVA: 0x00004AE4 File Offset: 0x00002CE4
		private static bool cmpBoolNumberO(Operator.Op op, object val1, object val2)
		{
			double num = NumberFunctions.Number((bool)val1);
			double num2 = (double)val2;
			return LogicalExpr.cmpNumberNumberO(op, num, num2);
		}

		// Token: 0x06000117 RID: 279 RVA: 0x00004B0C File Offset: 0x00002D0C
		private static bool cmpBoolStringE(Operator.Op op, object val1, object val2)
		{
			bool flag = (bool)val1;
			bool flag2 = BooleanFunctions.toBoolean((string)val2);
			return LogicalExpr.cmpBoolBoolE(op, flag, flag2);
		}

		// Token: 0x06000118 RID: 280 RVA: 0x00004B34 File Offset: 0x00002D34
		private static bool cmpRtfBoolE(Operator.Op op, object val1, object val2)
		{
			bool flag = BooleanFunctions.toBoolean(LogicalExpr.Rtf(val1));
			bool flag2 = (bool)val2;
			return LogicalExpr.cmpBoolBoolE(op, flag, flag2);
		}

		// Token: 0x06000119 RID: 281 RVA: 0x00004B5C File Offset: 0x00002D5C
		private static bool cmpBoolStringO(Operator.Op op, object val1, object val2)
		{
			return LogicalExpr.cmpNumberNumberO(op, NumberFunctions.Number((bool)val1), NumberFunctions.Number((string)val2));
		}

		// Token: 0x0600011A RID: 282 RVA: 0x00004B7A File Offset: 0x00002D7A
		private static bool cmpRtfBoolO(Operator.Op op, object val1, object val2)
		{
			return LogicalExpr.cmpNumberNumberO(op, NumberFunctions.Number(LogicalExpr.Rtf(val1)), NumberFunctions.Number((bool)val2));
		}

		// Token: 0x0600011B RID: 283 RVA: 0x00004B98 File Offset: 0x00002D98
		private static bool cmpNumberNumber(Operator.Op op, double n1, double n2)
		{
			switch (op)
			{
			case Operator.Op.EQ:
				return n1 == n2;
			case Operator.Op.NE:
				return n1 != n2;
			case Operator.Op.LT:
				return n1 < n2;
			case Operator.Op.LE:
				return n1 <= n2;
			case Operator.Op.GT:
				return n1 > n2;
			case Operator.Op.GE:
				return n1 >= n2;
			default:
				return false;
			}
		}

		// Token: 0x0600011C RID: 284 RVA: 0x00004BEF File Offset: 0x00002DEF
		private static bool cmpNumberNumberO(Operator.Op op, double n1, double n2)
		{
			switch (op)
			{
			case Operator.Op.LT:
				return n1 < n2;
			case Operator.Op.LE:
				return n1 <= n2;
			case Operator.Op.GT:
				return n1 > n2;
			case Operator.Op.GE:
				return n1 >= n2;
			default:
				return false;
			}
		}

		// Token: 0x0600011D RID: 285 RVA: 0x00004C28 File Offset: 0x00002E28
		private static bool cmpNumberNumber(Operator.Op op, object val1, object val2)
		{
			double num = (double)val1;
			double num2 = (double)val2;
			return LogicalExpr.cmpNumberNumber(op, num, num2);
		}

		// Token: 0x0600011E RID: 286 RVA: 0x00004C4C File Offset: 0x00002E4C
		private static bool cmpStringNumber(Operator.Op op, object val1, object val2)
		{
			double num = (double)val2;
			double num2 = NumberFunctions.Number((string)val1);
			return LogicalExpr.cmpNumberNumber(op, num2, num);
		}

		// Token: 0x0600011F RID: 287 RVA: 0x00004C74 File Offset: 0x00002E74
		private static bool cmpRtfNumber(Operator.Op op, object val1, object val2)
		{
			double num = (double)val2;
			double num2 = NumberFunctions.Number(LogicalExpr.Rtf(val1));
			return LogicalExpr.cmpNumberNumber(op, num2, num);
		}

		// Token: 0x06000120 RID: 288 RVA: 0x00004C9C File Offset: 0x00002E9C
		private static bool cmpStringStringE(Operator.Op op, string n1, string n2)
		{
			return op == Operator.Op.EQ == (n1 == n2);
		}

		// Token: 0x06000121 RID: 289 RVA: 0x00004CAC File Offset: 0x00002EAC
		private static bool cmpStringStringE(Operator.Op op, object val1, object val2)
		{
			string text = (string)val1;
			string text2 = (string)val2;
			return LogicalExpr.cmpStringStringE(op, text, text2);
		}

		// Token: 0x06000122 RID: 290 RVA: 0x00004CD0 File Offset: 0x00002ED0
		private static bool cmpRtfStringE(Operator.Op op, object val1, object val2)
		{
			string text = LogicalExpr.Rtf(val1);
			string text2 = (string)val2;
			return LogicalExpr.cmpStringStringE(op, text, text2);
		}

		// Token: 0x06000123 RID: 291 RVA: 0x00004CF4 File Offset: 0x00002EF4
		private static bool cmpRtfRtfE(Operator.Op op, object val1, object val2)
		{
			string text = LogicalExpr.Rtf(val1);
			string text2 = LogicalExpr.Rtf(val2);
			return LogicalExpr.cmpStringStringE(op, text, text2);
		}

		// Token: 0x06000124 RID: 292 RVA: 0x00004D18 File Offset: 0x00002F18
		private static bool cmpStringStringO(Operator.Op op, object val1, object val2)
		{
			double num = NumberFunctions.Number((string)val1);
			double num2 = NumberFunctions.Number((string)val2);
			return LogicalExpr.cmpNumberNumberO(op, num, num2);
		}

		// Token: 0x06000125 RID: 293 RVA: 0x00004D48 File Offset: 0x00002F48
		private static bool cmpRtfStringO(Operator.Op op, object val1, object val2)
		{
			double num = NumberFunctions.Number(LogicalExpr.Rtf(val1));
			double num2 = NumberFunctions.Number((string)val2);
			return LogicalExpr.cmpNumberNumberO(op, num, num2);
		}

		// Token: 0x06000126 RID: 294 RVA: 0x00004D78 File Offset: 0x00002F78
		private static bool cmpRtfRtfO(Operator.Op op, object val1, object val2)
		{
			double num = NumberFunctions.Number(LogicalExpr.Rtf(val1));
			double num2 = NumberFunctions.Number(LogicalExpr.Rtf(val2));
			return LogicalExpr.cmpNumberNumberO(op, num, num2);
		}

		// Token: 0x06000127 RID: 295 RVA: 0x00004DA5 File Offset: 0x00002FA5
		public override XPathNodeIterator Clone()
		{
			return new LogicalExpr(this);
		}

		// Token: 0x06000128 RID: 296 RVA: 0x00004DAD File Offset: 0x00002FAD
		private static string Rtf(object o)
		{
			return ((XPathNavigator)o).Value;
		}

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x06000129 RID: 297 RVA: 0x000026AE File Offset: 0x000008AE
		public override XPathResultType StaticType
		{
			get
			{
				return XPathResultType.Boolean;
			}
		}

		// Token: 0x0600012A RID: 298 RVA: 0x00004DBC File Offset: 0x00002FBC
		public override void PrintQuery(XmlWriter w)
		{
			w.WriteStartElement(base.GetType().Name);
			w.WriteAttributeString("op", this.op.ToString());
			this.opnd1.PrintQuery(w);
			this.opnd2.PrintQuery(w);
			w.WriteEndElement();
		}

		// Token: 0x0600012B RID: 299 RVA: 0x00004E14 File Offset: 0x00003014
		// Note: this type is marked as 'beforefieldinit'.
		static LogicalExpr()
		{
			LogicalExpr.cmpXslt[][] array = new LogicalExpr.cmpXslt[5][];
			int num = 0;
			LogicalExpr.cmpXslt[] array2 = new LogicalExpr.cmpXslt[5];
			array2[0] = new LogicalExpr.cmpXslt(LogicalExpr.cmpNumberNumber);
			array[num] = array2;
			int num2 = 1;
			LogicalExpr.cmpXslt[] array3 = new LogicalExpr.cmpXslt[5];
			array3[0] = new LogicalExpr.cmpXslt(LogicalExpr.cmpStringNumber);
			array3[1] = new LogicalExpr.cmpXslt(LogicalExpr.cmpStringStringE);
			array[num2] = array3;
			int num3 = 2;
			LogicalExpr.cmpXslt[] array4 = new LogicalExpr.cmpXslt[5];
			array4[0] = new LogicalExpr.cmpXslt(LogicalExpr.cmpBoolNumberE);
			array4[1] = new LogicalExpr.cmpXslt(LogicalExpr.cmpBoolStringE);
			array4[2] = new LogicalExpr.cmpXslt(LogicalExpr.cmpBoolBoolE);
			array[num3] = array4;
			int num4 = 3;
			LogicalExpr.cmpXslt[] array5 = new LogicalExpr.cmpXslt[5];
			array5[0] = new LogicalExpr.cmpXslt(LogicalExpr.cmpQueryNumber);
			array5[1] = new LogicalExpr.cmpXslt(LogicalExpr.cmpQueryStringE);
			array5[2] = new LogicalExpr.cmpXslt(LogicalExpr.cmpQueryBoolE);
			array5[3] = new LogicalExpr.cmpXslt(LogicalExpr.cmpQueryQueryE);
			array[num4] = array5;
			array[4] = new LogicalExpr.cmpXslt[]
			{
				new LogicalExpr.cmpXslt(LogicalExpr.cmpRtfNumber),
				new LogicalExpr.cmpXslt(LogicalExpr.cmpRtfStringE),
				new LogicalExpr.cmpXslt(LogicalExpr.cmpRtfBoolE),
				new LogicalExpr.cmpXslt(LogicalExpr.cmpRtfQueryE),
				new LogicalExpr.cmpXslt(LogicalExpr.cmpRtfRtfE)
			};
			LogicalExpr.CompXsltE = array;
			LogicalExpr.cmpXslt[][] array6 = new LogicalExpr.cmpXslt[5][];
			int num5 = 0;
			LogicalExpr.cmpXslt[] array7 = new LogicalExpr.cmpXslt[5];
			array7[0] = new LogicalExpr.cmpXslt(LogicalExpr.cmpNumberNumber);
			array6[num5] = array7;
			int num6 = 1;
			LogicalExpr.cmpXslt[] array8 = new LogicalExpr.cmpXslt[5];
			array8[0] = new LogicalExpr.cmpXslt(LogicalExpr.cmpStringNumber);
			array8[1] = new LogicalExpr.cmpXslt(LogicalExpr.cmpStringStringO);
			array6[num6] = array8;
			int num7 = 2;
			LogicalExpr.cmpXslt[] array9 = new LogicalExpr.cmpXslt[5];
			array9[0] = new LogicalExpr.cmpXslt(LogicalExpr.cmpBoolNumberO);
			array9[1] = new LogicalExpr.cmpXslt(LogicalExpr.cmpBoolStringO);
			array9[2] = new LogicalExpr.cmpXslt(LogicalExpr.cmpBoolBoolO);
			array6[num7] = array9;
			int num8 = 3;
			LogicalExpr.cmpXslt[] array10 = new LogicalExpr.cmpXslt[5];
			array10[0] = new LogicalExpr.cmpXslt(LogicalExpr.cmpQueryNumber);
			array10[1] = new LogicalExpr.cmpXslt(LogicalExpr.cmpQueryStringO);
			array10[2] = new LogicalExpr.cmpXslt(LogicalExpr.cmpQueryBoolO);
			array10[3] = new LogicalExpr.cmpXslt(LogicalExpr.cmpQueryQueryO);
			array6[num8] = array10;
			array6[4] = new LogicalExpr.cmpXslt[]
			{
				new LogicalExpr.cmpXslt(LogicalExpr.cmpRtfNumber),
				new LogicalExpr.cmpXslt(LogicalExpr.cmpRtfStringO),
				new LogicalExpr.cmpXslt(LogicalExpr.cmpRtfBoolO),
				new LogicalExpr.cmpXslt(LogicalExpr.cmpRtfQueryO),
				new LogicalExpr.cmpXslt(LogicalExpr.cmpRtfRtfO)
			};
			LogicalExpr.CompXsltO = array6;
		}

		// Token: 0x040000B1 RID: 177
		private Operator.Op op;

		// Token: 0x040000B2 RID: 178
		private Query opnd1;

		// Token: 0x040000B3 RID: 179
		private Query opnd2;

		// Token: 0x040000B4 RID: 180
		private static readonly LogicalExpr.cmpXslt[][] CompXsltE;

		// Token: 0x040000B5 RID: 181
		private static readonly LogicalExpr.cmpXslt[][] CompXsltO;

		// Token: 0x0200002B RID: 43
		// (Invoke) Token: 0x0600012D RID: 301
		private delegate bool cmpXslt(Operator.Op op, object val1, object val2);

		// Token: 0x0200002C RID: 44
		private struct NodeSet
		{
			// Token: 0x06000130 RID: 304 RVA: 0x00005053 File Offset: 0x00003253
			public NodeSet(object opnd)
			{
				this.opnd = (Query)opnd;
				this.current = null;
			}

			// Token: 0x06000131 RID: 305 RVA: 0x00005068 File Offset: 0x00003268
			public bool MoveNext()
			{
				this.current = this.opnd.Advance();
				return this.current != null;
			}

			// Token: 0x06000132 RID: 306 RVA: 0x00005084 File Offset: 0x00003284
			public void Reset()
			{
				this.opnd.Reset();
			}

			// Token: 0x17000049 RID: 73
			// (get) Token: 0x06000133 RID: 307 RVA: 0x00005091 File Offset: 0x00003291
			public string Value
			{
				get
				{
					return this.current.Value;
				}
			}

			// Token: 0x040000B6 RID: 182
			private Query opnd;

			// Token: 0x040000B7 RID: 183
			private XPathNavigator current;
		}
	}
}
