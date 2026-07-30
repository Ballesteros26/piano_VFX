using System;
using System.Collections.Generic;
using System.Reflection;

namespace System.Xml.Xsl.Qil
{
	// Token: 0x0200063C RID: 1596
	internal class QilPatternFactory
	{
		// Token: 0x06003EEB RID: 16107 RVA: 0x00157F82 File Offset: 0x00156182
		public QilPatternFactory(QilFactory f, bool debug)
		{
			this.f = f;
			this.debug = debug;
		}

		// Token: 0x17000CA4 RID: 3236
		// (get) Token: 0x06003EEC RID: 16108 RVA: 0x00157F98 File Offset: 0x00156198
		public QilFactory BaseFactory
		{
			get
			{
				return this.f;
			}
		}

		// Token: 0x17000CA5 RID: 3237
		// (get) Token: 0x06003EED RID: 16109 RVA: 0x00157FA0 File Offset: 0x001561A0
		public bool IsDebug
		{
			get
			{
				return this.debug;
			}
		}

		// Token: 0x06003EEE RID: 16110 RVA: 0x00157FA8 File Offset: 0x001561A8
		public QilLiteral String(string val)
		{
			return this.f.LiteralString(val);
		}

		// Token: 0x06003EEF RID: 16111 RVA: 0x00157FB6 File Offset: 0x001561B6
		public QilLiteral Int32(int val)
		{
			return this.f.LiteralInt32(val);
		}

		// Token: 0x06003EF0 RID: 16112 RVA: 0x00157FC4 File Offset: 0x001561C4
		public QilLiteral Double(double val)
		{
			return this.f.LiteralDouble(val);
		}

		// Token: 0x06003EF1 RID: 16113 RVA: 0x00157FD2 File Offset: 0x001561D2
		public QilName QName(string local, string uri, string prefix)
		{
			return this.f.LiteralQName(local, uri, prefix);
		}

		// Token: 0x06003EF2 RID: 16114 RVA: 0x00157FE2 File Offset: 0x001561E2
		public QilName QName(string local, string uri)
		{
			return this.f.LiteralQName(local, uri, string.Empty);
		}

		// Token: 0x06003EF3 RID: 16115 RVA: 0x00157FF6 File Offset: 0x001561F6
		public QilName QName(string local)
		{
			return this.f.LiteralQName(local, string.Empty, string.Empty);
		}

		// Token: 0x06003EF4 RID: 16116 RVA: 0x0015800E File Offset: 0x0015620E
		public QilNode Unknown(XmlQueryType t)
		{
			return this.f.Unknown(t);
		}

		// Token: 0x06003EF5 RID: 16117 RVA: 0x0015801C File Offset: 0x0015621C
		public QilExpression QilExpression(QilNode root, QilFactory factory)
		{
			return this.f.QilExpression(root, factory);
		}

		// Token: 0x06003EF6 RID: 16118 RVA: 0x0015802B File Offset: 0x0015622B
		public QilList FunctionList()
		{
			return this.f.FunctionList();
		}

		// Token: 0x06003EF7 RID: 16119 RVA: 0x00158038 File Offset: 0x00156238
		public QilList GlobalVariableList()
		{
			return this.f.GlobalVariableList();
		}

		// Token: 0x06003EF8 RID: 16120 RVA: 0x00158045 File Offset: 0x00156245
		public QilList GlobalParameterList()
		{
			return this.f.GlobalParameterList();
		}

		// Token: 0x06003EF9 RID: 16121 RVA: 0x00158052 File Offset: 0x00156252
		public QilList ActualParameterList()
		{
			return this.f.ActualParameterList();
		}

		// Token: 0x06003EFA RID: 16122 RVA: 0x0015805F File Offset: 0x0015625F
		public QilList ActualParameterList(QilNode arg1)
		{
			QilList qilList = this.f.ActualParameterList();
			qilList.Add(arg1);
			return qilList;
		}

		// Token: 0x06003EFB RID: 16123 RVA: 0x00158073 File Offset: 0x00156273
		public QilList ActualParameterList(QilNode arg1, QilNode arg2)
		{
			QilList qilList = this.f.ActualParameterList();
			qilList.Add(arg1);
			qilList.Add(arg2);
			return qilList;
		}

		// Token: 0x06003EFC RID: 16124 RVA: 0x0015808E File Offset: 0x0015628E
		public QilList ActualParameterList(params QilNode[] args)
		{
			return this.f.ActualParameterList(args);
		}

		// Token: 0x06003EFD RID: 16125 RVA: 0x0015809C File Offset: 0x0015629C
		public QilList FormalParameterList()
		{
			return this.f.FormalParameterList();
		}

		// Token: 0x06003EFE RID: 16126 RVA: 0x001580A9 File Offset: 0x001562A9
		public QilList FormalParameterList(QilNode arg1)
		{
			QilList qilList = this.f.FormalParameterList();
			qilList.Add(arg1);
			return qilList;
		}

		// Token: 0x06003EFF RID: 16127 RVA: 0x001580BD File Offset: 0x001562BD
		public QilList FormalParameterList(QilNode arg1, QilNode arg2)
		{
			QilList qilList = this.f.FormalParameterList();
			qilList.Add(arg1);
			qilList.Add(arg2);
			return qilList;
		}

		// Token: 0x06003F00 RID: 16128 RVA: 0x001580D8 File Offset: 0x001562D8
		public QilList FormalParameterList(params QilNode[] args)
		{
			return this.f.FormalParameterList(args);
		}

		// Token: 0x06003F01 RID: 16129 RVA: 0x001580E6 File Offset: 0x001562E6
		public QilList SortKeyList()
		{
			return this.f.SortKeyList();
		}

		// Token: 0x06003F02 RID: 16130 RVA: 0x001580F3 File Offset: 0x001562F3
		public QilList SortKeyList(QilSortKey key)
		{
			QilList qilList = this.f.SortKeyList();
			qilList.Add(key);
			return qilList;
		}

		// Token: 0x06003F03 RID: 16131 RVA: 0x00158107 File Offset: 0x00156307
		public QilList BranchList(params QilNode[] args)
		{
			return this.f.BranchList(args);
		}

		// Token: 0x06003F04 RID: 16132 RVA: 0x00158115 File Offset: 0x00156315
		public QilNode OptimizeBarrier(QilNode child)
		{
			return this.f.OptimizeBarrier(child);
		}

		// Token: 0x06003F05 RID: 16133 RVA: 0x00158123 File Offset: 0x00156323
		public QilNode DataSource(QilNode name, QilNode baseUri)
		{
			return this.f.DataSource(name, baseUri);
		}

		// Token: 0x06003F06 RID: 16134 RVA: 0x00158132 File Offset: 0x00156332
		public QilNode Nop(QilNode child)
		{
			return this.f.Nop(child);
		}

		// Token: 0x06003F07 RID: 16135 RVA: 0x00158140 File Offset: 0x00156340
		public QilNode Error(QilNode text)
		{
			return this.f.Error(text);
		}

		// Token: 0x06003F08 RID: 16136 RVA: 0x0015814E File Offset: 0x0015634E
		public QilNode Warning(QilNode text)
		{
			return this.f.Warning(text);
		}

		// Token: 0x06003F09 RID: 16137 RVA: 0x0015815C File Offset: 0x0015635C
		public QilIterator For(QilNode binding)
		{
			return this.f.For(binding);
		}

		// Token: 0x06003F0A RID: 16138 RVA: 0x0015816A File Offset: 0x0015636A
		public QilIterator Let(QilNode binding)
		{
			return this.f.Let(binding);
		}

		// Token: 0x06003F0B RID: 16139 RVA: 0x00158178 File Offset: 0x00156378
		public QilParameter Parameter(XmlQueryType t)
		{
			return this.f.Parameter(t);
		}

		// Token: 0x06003F0C RID: 16140 RVA: 0x00158186 File Offset: 0x00156386
		public QilParameter Parameter(QilNode defaultValue, QilName name, XmlQueryType t)
		{
			return this.f.Parameter(defaultValue, name, t);
		}

		// Token: 0x06003F0D RID: 16141 RVA: 0x00158196 File Offset: 0x00156396
		public QilNode PositionOf(QilIterator expr)
		{
			return this.f.PositionOf(expr);
		}

		// Token: 0x06003F0E RID: 16142 RVA: 0x001581A4 File Offset: 0x001563A4
		public QilNode True()
		{
			return this.f.True();
		}

		// Token: 0x06003F0F RID: 16143 RVA: 0x001581B1 File Offset: 0x001563B1
		public QilNode False()
		{
			return this.f.False();
		}

		// Token: 0x06003F10 RID: 16144 RVA: 0x001581BE File Offset: 0x001563BE
		public QilNode Boolean(bool b)
		{
			if (!b)
			{
				return this.False();
			}
			return this.True();
		}

		// Token: 0x06003F11 RID: 16145 RVA: 0x00002F50 File Offset: 0x00001150
		private static void CheckLogicArg(QilNode arg)
		{
		}

		// Token: 0x06003F12 RID: 16146 RVA: 0x001581D0 File Offset: 0x001563D0
		public QilNode And(QilNode left, QilNode right)
		{
			QilPatternFactory.CheckLogicArg(left);
			QilPatternFactory.CheckLogicArg(right);
			if (!this.debug)
			{
				if (left.NodeType == QilNodeType.True || right.NodeType == QilNodeType.False)
				{
					return right;
				}
				if (left.NodeType == QilNodeType.False || right.NodeType == QilNodeType.True)
				{
					return left;
				}
			}
			return this.f.And(left, right);
		}

		// Token: 0x06003F13 RID: 16147 RVA: 0x0015822C File Offset: 0x0015642C
		public QilNode Or(QilNode left, QilNode right)
		{
			QilPatternFactory.CheckLogicArg(left);
			QilPatternFactory.CheckLogicArg(right);
			if (!this.debug)
			{
				if (left.NodeType == QilNodeType.True || right.NodeType == QilNodeType.False)
				{
					return left;
				}
				if (left.NodeType == QilNodeType.False || right.NodeType == QilNodeType.True)
				{
					return right;
				}
			}
			return this.f.Or(left, right);
		}

		// Token: 0x06003F14 RID: 16148 RVA: 0x00158288 File Offset: 0x00156488
		public QilNode Not(QilNode child)
		{
			if (!this.debug)
			{
				QilNodeType nodeType = child.NodeType;
				if (nodeType == QilNodeType.True)
				{
					return this.f.False();
				}
				if (nodeType == QilNodeType.False)
				{
					return this.f.True();
				}
				if (nodeType == QilNodeType.Not)
				{
					return ((QilUnary)child).Child;
				}
			}
			return this.f.Not(child);
		}

		// Token: 0x06003F15 RID: 16149 RVA: 0x001582E8 File Offset: 0x001564E8
		public QilNode Conditional(QilNode condition, QilNode trueBranch, QilNode falseBranch)
		{
			if (!this.debug)
			{
				QilNodeType nodeType = condition.NodeType;
				if (nodeType == QilNodeType.True)
				{
					return trueBranch;
				}
				if (nodeType == QilNodeType.False)
				{
					return falseBranch;
				}
				if (nodeType == QilNodeType.Not)
				{
					return this.Conditional(((QilUnary)condition).Child, falseBranch, trueBranch);
				}
			}
			return this.f.Conditional(condition, trueBranch, falseBranch);
		}

		// Token: 0x06003F16 RID: 16150 RVA: 0x0015833C File Offset: 0x0015653C
		public QilNode Choice(QilNode expr, QilList branches)
		{
			if (!this.debug)
			{
				int count = branches.Count;
				if (count == 1)
				{
					return this.f.Loop(this.f.Let(expr), branches[0]);
				}
				if (count == 2)
				{
					return this.f.Conditional(this.f.Eq(expr, this.f.LiteralInt32(0)), branches[0], branches[1]);
				}
			}
			return this.f.Choice(expr, branches);
		}

		// Token: 0x06003F17 RID: 16151 RVA: 0x001583C0 File Offset: 0x001565C0
		public QilNode Length(QilNode child)
		{
			return this.f.Length(child);
		}

		// Token: 0x06003F18 RID: 16152 RVA: 0x001583CE File Offset: 0x001565CE
		public QilNode Sequence()
		{
			return this.f.Sequence();
		}

		// Token: 0x06003F19 RID: 16153 RVA: 0x001583DB File Offset: 0x001565DB
		public QilNode Sequence(QilNode child)
		{
			if (!this.debug)
			{
				return child;
			}
			QilList qilList = this.f.Sequence();
			qilList.Add(child);
			return qilList;
		}

		// Token: 0x06003F1A RID: 16154 RVA: 0x001583F9 File Offset: 0x001565F9
		public QilNode Sequence(QilNode child1, QilNode child2)
		{
			QilList qilList = this.f.Sequence();
			qilList.Add(child1);
			qilList.Add(child2);
			return qilList;
		}

		// Token: 0x06003F1B RID: 16155 RVA: 0x00158414 File Offset: 0x00156614
		public QilNode Sequence(params QilNode[] args)
		{
			if (!this.debug)
			{
				int i = args.Length;
				if (i == 0)
				{
					return this.f.Sequence();
				}
				if (i == 1)
				{
					return args[0];
				}
			}
			QilList qilList = this.f.Sequence();
			foreach (QilNode qilNode in args)
			{
				qilList.Add(qilNode);
			}
			return qilList;
		}

		// Token: 0x06003F1C RID: 16156 RVA: 0x0015846E File Offset: 0x0015666E
		public QilNode Union(QilNode left, QilNode right)
		{
			return this.f.Union(left, right);
		}

		// Token: 0x06003F1D RID: 16157 RVA: 0x0015847D File Offset: 0x0015667D
		public QilNode Sum(QilNode collection)
		{
			return this.f.Sum(collection);
		}

		// Token: 0x06003F1E RID: 16158 RVA: 0x0015848B File Offset: 0x0015668B
		public QilNode Negate(QilNode child)
		{
			return this.f.Negate(child);
		}

		// Token: 0x06003F1F RID: 16159 RVA: 0x00158499 File Offset: 0x00156699
		public QilNode Add(QilNode left, QilNode right)
		{
			return this.f.Add(left, right);
		}

		// Token: 0x06003F20 RID: 16160 RVA: 0x001584A8 File Offset: 0x001566A8
		public QilNode Subtract(QilNode left, QilNode right)
		{
			return this.f.Subtract(left, right);
		}

		// Token: 0x06003F21 RID: 16161 RVA: 0x001584B7 File Offset: 0x001566B7
		public QilNode Multiply(QilNode left, QilNode right)
		{
			return this.f.Multiply(left, right);
		}

		// Token: 0x06003F22 RID: 16162 RVA: 0x001584C6 File Offset: 0x001566C6
		public QilNode Divide(QilNode left, QilNode right)
		{
			return this.f.Divide(left, right);
		}

		// Token: 0x06003F23 RID: 16163 RVA: 0x001584D5 File Offset: 0x001566D5
		public QilNode Modulo(QilNode left, QilNode right)
		{
			return this.f.Modulo(left, right);
		}

		// Token: 0x06003F24 RID: 16164 RVA: 0x001584E4 File Offset: 0x001566E4
		public QilNode StrLength(QilNode str)
		{
			return this.f.StrLength(str);
		}

		// Token: 0x06003F25 RID: 16165 RVA: 0x001584F2 File Offset: 0x001566F2
		public QilNode StrConcat(QilNode values)
		{
			if (!this.debug && values.XmlType.IsSingleton)
			{
				return values;
			}
			return this.f.StrConcat(values);
		}

		// Token: 0x06003F26 RID: 16166 RVA: 0x00158517 File Offset: 0x00156717
		public QilNode StrConcat(params QilNode[] args)
		{
			return this.StrConcat(args);
		}

		// Token: 0x06003F27 RID: 16167 RVA: 0x00158520 File Offset: 0x00156720
		public QilNode StrConcat(IList<QilNode> args)
		{
			if (!this.debug)
			{
				int count = args.Count;
				if (count == 0)
				{
					return this.f.LiteralString(string.Empty);
				}
				if (count == 1)
				{
					return this.StrConcat(args[0]);
				}
			}
			return this.StrConcat(this.f.Sequence(args));
		}

		// Token: 0x06003F28 RID: 16168 RVA: 0x00158576 File Offset: 0x00156776
		public QilNode StrParseQName(QilNode str, QilNode ns)
		{
			return this.f.StrParseQName(str, ns);
		}

		// Token: 0x06003F29 RID: 16169 RVA: 0x00158585 File Offset: 0x00156785
		public QilNode Ne(QilNode left, QilNode right)
		{
			return this.f.Ne(left, right);
		}

		// Token: 0x06003F2A RID: 16170 RVA: 0x00158594 File Offset: 0x00156794
		public QilNode Eq(QilNode left, QilNode right)
		{
			return this.f.Eq(left, right);
		}

		// Token: 0x06003F2B RID: 16171 RVA: 0x001585A3 File Offset: 0x001567A3
		public QilNode Gt(QilNode left, QilNode right)
		{
			return this.f.Gt(left, right);
		}

		// Token: 0x06003F2C RID: 16172 RVA: 0x001585B2 File Offset: 0x001567B2
		public QilNode Ge(QilNode left, QilNode right)
		{
			return this.f.Ge(left, right);
		}

		// Token: 0x06003F2D RID: 16173 RVA: 0x001585C1 File Offset: 0x001567C1
		public QilNode Lt(QilNode left, QilNode right)
		{
			return this.f.Lt(left, right);
		}

		// Token: 0x06003F2E RID: 16174 RVA: 0x001585D0 File Offset: 0x001567D0
		public QilNode Le(QilNode left, QilNode right)
		{
			return this.f.Le(left, right);
		}

		// Token: 0x06003F2F RID: 16175 RVA: 0x001585DF File Offset: 0x001567DF
		public QilNode Is(QilNode left, QilNode right)
		{
			return this.f.Is(left, right);
		}

		// Token: 0x06003F30 RID: 16176 RVA: 0x001585EE File Offset: 0x001567EE
		public QilNode After(QilNode left, QilNode right)
		{
			return this.f.After(left, right);
		}

		// Token: 0x06003F31 RID: 16177 RVA: 0x001585FD File Offset: 0x001567FD
		public QilNode Before(QilNode left, QilNode right)
		{
			return this.f.Before(left, right);
		}

		// Token: 0x06003F32 RID: 16178 RVA: 0x0015860C File Offset: 0x0015680C
		public QilNode Loop(QilIterator variable, QilNode body)
		{
			if (!this.debug && body == variable.Binding)
			{
				return body;
			}
			return this.f.Loop(variable, body);
		}

		// Token: 0x06003F33 RID: 16179 RVA: 0x0015862E File Offset: 0x0015682E
		public QilNode Filter(QilIterator variable, QilNode expr)
		{
			if (!this.debug && expr.NodeType == QilNodeType.True)
			{
				return variable.Binding;
			}
			return this.f.Filter(variable, expr);
		}

		// Token: 0x06003F34 RID: 16180 RVA: 0x00158656 File Offset: 0x00156856
		public QilNode Sort(QilIterator iter, QilNode keys)
		{
			return this.f.Sort(iter, keys);
		}

		// Token: 0x06003F35 RID: 16181 RVA: 0x00158665 File Offset: 0x00156865
		public QilSortKey SortKey(QilNode key, QilNode collation)
		{
			return this.f.SortKey(key, collation);
		}

		// Token: 0x06003F36 RID: 16182 RVA: 0x00158674 File Offset: 0x00156874
		public QilNode DocOrderDistinct(QilNode collection)
		{
			if (collection.NodeType == QilNodeType.DocOrderDistinct)
			{
				return collection;
			}
			return this.f.DocOrderDistinct(collection);
		}

		// Token: 0x06003F37 RID: 16183 RVA: 0x0015868E File Offset: 0x0015688E
		public QilFunction Function(QilList args, QilNode sideEffects, XmlQueryType resultType)
		{
			return this.f.Function(args, sideEffects, resultType);
		}

		// Token: 0x06003F38 RID: 16184 RVA: 0x0015869E File Offset: 0x0015689E
		public QilFunction Function(QilList args, QilNode defn, QilNode sideEffects)
		{
			return this.f.Function(args, defn, sideEffects, defn.XmlType);
		}

		// Token: 0x06003F39 RID: 16185 RVA: 0x001586B4 File Offset: 0x001568B4
		public QilNode Invoke(QilFunction func, QilList args)
		{
			return this.f.Invoke(func, args);
		}

		// Token: 0x06003F3A RID: 16186 RVA: 0x001586C3 File Offset: 0x001568C3
		public QilNode Content(QilNode context)
		{
			return this.f.Content(context);
		}

		// Token: 0x06003F3B RID: 16187 RVA: 0x001586D1 File Offset: 0x001568D1
		public QilNode Parent(QilNode context)
		{
			return this.f.Parent(context);
		}

		// Token: 0x06003F3C RID: 16188 RVA: 0x001586DF File Offset: 0x001568DF
		public QilNode Root(QilNode context)
		{
			return this.f.Root(context);
		}

		// Token: 0x06003F3D RID: 16189 RVA: 0x001586ED File Offset: 0x001568ED
		public QilNode XmlContext()
		{
			return this.f.XmlContext();
		}

		// Token: 0x06003F3E RID: 16190 RVA: 0x001586FA File Offset: 0x001568FA
		public QilNode Descendant(QilNode expr)
		{
			return this.f.Descendant(expr);
		}

		// Token: 0x06003F3F RID: 16191 RVA: 0x00158708 File Offset: 0x00156908
		public QilNode DescendantOrSelf(QilNode context)
		{
			return this.f.DescendantOrSelf(context);
		}

		// Token: 0x06003F40 RID: 16192 RVA: 0x00158716 File Offset: 0x00156916
		public QilNode Ancestor(QilNode expr)
		{
			return this.f.Ancestor(expr);
		}

		// Token: 0x06003F41 RID: 16193 RVA: 0x00158724 File Offset: 0x00156924
		public QilNode AncestorOrSelf(QilNode expr)
		{
			return this.f.AncestorOrSelf(expr);
		}

		// Token: 0x06003F42 RID: 16194 RVA: 0x00158732 File Offset: 0x00156932
		public QilNode Preceding(QilNode expr)
		{
			return this.f.Preceding(expr);
		}

		// Token: 0x06003F43 RID: 16195 RVA: 0x00158740 File Offset: 0x00156940
		public QilNode FollowingSibling(QilNode expr)
		{
			return this.f.FollowingSibling(expr);
		}

		// Token: 0x06003F44 RID: 16196 RVA: 0x0015874E File Offset: 0x0015694E
		public QilNode PrecedingSibling(QilNode expr)
		{
			return this.f.PrecedingSibling(expr);
		}

		// Token: 0x06003F45 RID: 16197 RVA: 0x0015875C File Offset: 0x0015695C
		public QilNode NodeRange(QilNode left, QilNode right)
		{
			return this.f.NodeRange(left, right);
		}

		// Token: 0x06003F46 RID: 16198 RVA: 0x0015876B File Offset: 0x0015696B
		public QilBinary Deref(QilNode context, QilNode id)
		{
			return this.f.Deref(context, id);
		}

		// Token: 0x06003F47 RID: 16199 RVA: 0x0015877A File Offset: 0x0015697A
		public QilNode ElementCtor(QilNode name, QilNode content)
		{
			return this.f.ElementCtor(name, content);
		}

		// Token: 0x06003F48 RID: 16200 RVA: 0x00158789 File Offset: 0x00156989
		public QilNode AttributeCtor(QilNode name, QilNode val)
		{
			return this.f.AttributeCtor(name, val);
		}

		// Token: 0x06003F49 RID: 16201 RVA: 0x00158798 File Offset: 0x00156998
		public QilNode CommentCtor(QilNode content)
		{
			return this.f.CommentCtor(content);
		}

		// Token: 0x06003F4A RID: 16202 RVA: 0x001587A6 File Offset: 0x001569A6
		public QilNode PICtor(QilNode name, QilNode content)
		{
			return this.f.PICtor(name, content);
		}

		// Token: 0x06003F4B RID: 16203 RVA: 0x001587B5 File Offset: 0x001569B5
		public QilNode TextCtor(QilNode content)
		{
			return this.f.TextCtor(content);
		}

		// Token: 0x06003F4C RID: 16204 RVA: 0x001587C3 File Offset: 0x001569C3
		public QilNode RawTextCtor(QilNode content)
		{
			return this.f.RawTextCtor(content);
		}

		// Token: 0x06003F4D RID: 16205 RVA: 0x001587D1 File Offset: 0x001569D1
		public QilNode DocumentCtor(QilNode child)
		{
			return this.f.DocumentCtor(child);
		}

		// Token: 0x06003F4E RID: 16206 RVA: 0x001587DF File Offset: 0x001569DF
		public QilNode NamespaceDecl(QilNode prefix, QilNode uri)
		{
			return this.f.NamespaceDecl(prefix, uri);
		}

		// Token: 0x06003F4F RID: 16207 RVA: 0x001587EE File Offset: 0x001569EE
		public QilNode RtfCtor(QilNode content, QilNode baseUri)
		{
			return this.f.RtfCtor(content, baseUri);
		}

		// Token: 0x06003F50 RID: 16208 RVA: 0x001587FD File Offset: 0x001569FD
		public QilNode NameOf(QilNode expr)
		{
			return this.f.NameOf(expr);
		}

		// Token: 0x06003F51 RID: 16209 RVA: 0x0015880B File Offset: 0x00156A0B
		public QilNode LocalNameOf(QilNode expr)
		{
			return this.f.LocalNameOf(expr);
		}

		// Token: 0x06003F52 RID: 16210 RVA: 0x00158819 File Offset: 0x00156A19
		public QilNode NamespaceUriOf(QilNode expr)
		{
			return this.f.NamespaceUriOf(expr);
		}

		// Token: 0x06003F53 RID: 16211 RVA: 0x00158827 File Offset: 0x00156A27
		public QilNode PrefixOf(QilNode expr)
		{
			return this.f.PrefixOf(expr);
		}

		// Token: 0x06003F54 RID: 16212 RVA: 0x00158835 File Offset: 0x00156A35
		public QilNode TypeAssert(QilNode expr, XmlQueryType t)
		{
			return this.f.TypeAssert(expr, t);
		}

		// Token: 0x06003F55 RID: 16213 RVA: 0x00158844 File Offset: 0x00156A44
		public QilNode IsType(QilNode expr, XmlQueryType t)
		{
			return this.f.IsType(expr, t);
		}

		// Token: 0x06003F56 RID: 16214 RVA: 0x00158853 File Offset: 0x00156A53
		public QilNode IsEmpty(QilNode set)
		{
			return this.f.IsEmpty(set);
		}

		// Token: 0x06003F57 RID: 16215 RVA: 0x00158861 File Offset: 0x00156A61
		public QilNode XPathNodeValue(QilNode expr)
		{
			return this.f.XPathNodeValue(expr);
		}

		// Token: 0x06003F58 RID: 16216 RVA: 0x0015886F File Offset: 0x00156A6F
		public QilNode XPathFollowing(QilNode expr)
		{
			return this.f.XPathFollowing(expr);
		}

		// Token: 0x06003F59 RID: 16217 RVA: 0x0015887D File Offset: 0x00156A7D
		public QilNode XPathNamespace(QilNode expr)
		{
			return this.f.XPathNamespace(expr);
		}

		// Token: 0x06003F5A RID: 16218 RVA: 0x0015888B File Offset: 0x00156A8B
		public QilNode XPathPreceding(QilNode expr)
		{
			return this.f.XPathPreceding(expr);
		}

		// Token: 0x06003F5B RID: 16219 RVA: 0x00158899 File Offset: 0x00156A99
		public QilNode XsltGenerateId(QilNode expr)
		{
			return this.f.XsltGenerateId(expr);
		}

		// Token: 0x06003F5C RID: 16220 RVA: 0x001588A8 File Offset: 0x00156AA8
		public QilNode XsltInvokeEarlyBound(QilNode name, MethodInfo d, XmlQueryType t, IList<QilNode> args)
		{
			QilList qilList = this.f.ActualParameterList();
			qilList.Add(args);
			return this.f.XsltInvokeEarlyBound(name, this.f.LiteralObject(d), qilList, t);
		}

		// Token: 0x06003F5D RID: 16221 RVA: 0x001588E4 File Offset: 0x00156AE4
		public QilNode XsltInvokeLateBound(QilNode name, IList<QilNode> args)
		{
			QilList qilList = this.f.ActualParameterList();
			qilList.Add(args);
			return this.f.XsltInvokeLateBound(name, qilList);
		}

		// Token: 0x06003F5E RID: 16222 RVA: 0x00158911 File Offset: 0x00156B11
		public QilNode XsltCopy(QilNode expr, QilNode content)
		{
			return this.f.XsltCopy(expr, content);
		}

		// Token: 0x06003F5F RID: 16223 RVA: 0x00158920 File Offset: 0x00156B20
		public QilNode XsltCopyOf(QilNode expr)
		{
			return this.f.XsltCopyOf(expr);
		}

		// Token: 0x06003F60 RID: 16224 RVA: 0x0015892E File Offset: 0x00156B2E
		public QilNode XsltConvert(QilNode expr, XmlQueryType t)
		{
			return this.f.XsltConvert(expr, t);
		}

		// Token: 0x040028BE RID: 10430
		private bool debug;

		// Token: 0x040028BF RID: 10431
		private QilFactory f;
	}
}
