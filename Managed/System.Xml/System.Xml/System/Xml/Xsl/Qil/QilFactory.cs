using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace System.Xml.Xsl.Qil
{
	// Token: 0x0200062F RID: 1583
	internal sealed class QilFactory
	{
		// Token: 0x06003E06 RID: 15878 RVA: 0x001563FB File Offset: 0x001545FB
		public QilFactory()
		{
			this.typeCheck = new QilTypeChecker();
		}

		// Token: 0x17000C7F RID: 3199
		// (get) Token: 0x06003E07 RID: 15879 RVA: 0x0015640E File Offset: 0x0015460E
		public QilTypeChecker TypeChecker
		{
			get
			{
				return this.typeCheck;
			}
		}

		// Token: 0x06003E08 RID: 15880 RVA: 0x00156418 File Offset: 0x00154618
		public QilExpression QilExpression(QilNode root, QilFactory factory)
		{
			QilExpression qilExpression = new QilExpression(QilNodeType.QilExpression, root, factory);
			qilExpression.XmlType = this.typeCheck.CheckQilExpression(qilExpression);
			return qilExpression;
		}

		// Token: 0x06003E09 RID: 15881 RVA: 0x00156441 File Offset: 0x00154641
		public QilList FunctionList(IList<QilNode> values)
		{
			QilList qilList = this.FunctionList();
			qilList.Add(values);
			return qilList;
		}

		// Token: 0x06003E0A RID: 15882 RVA: 0x00156450 File Offset: 0x00154650
		public QilList GlobalVariableList(IList<QilNode> values)
		{
			QilList qilList = this.GlobalVariableList();
			qilList.Add(values);
			return qilList;
		}

		// Token: 0x06003E0B RID: 15883 RVA: 0x0015645F File Offset: 0x0015465F
		public QilList GlobalParameterList(IList<QilNode> values)
		{
			QilList qilList = this.GlobalParameterList();
			qilList.Add(values);
			return qilList;
		}

		// Token: 0x06003E0C RID: 15884 RVA: 0x0015646E File Offset: 0x0015466E
		public QilList ActualParameterList(IList<QilNode> values)
		{
			QilList qilList = this.ActualParameterList();
			qilList.Add(values);
			return qilList;
		}

		// Token: 0x06003E0D RID: 15885 RVA: 0x0015647D File Offset: 0x0015467D
		public QilList FormalParameterList(IList<QilNode> values)
		{
			QilList qilList = this.FormalParameterList();
			qilList.Add(values);
			return qilList;
		}

		// Token: 0x06003E0E RID: 15886 RVA: 0x0015648C File Offset: 0x0015468C
		public QilList SortKeyList(IList<QilNode> values)
		{
			QilList qilList = this.SortKeyList();
			qilList.Add(values);
			return qilList;
		}

		// Token: 0x06003E0F RID: 15887 RVA: 0x0015649B File Offset: 0x0015469B
		public QilList BranchList(IList<QilNode> values)
		{
			QilList qilList = this.BranchList();
			qilList.Add(values);
			return qilList;
		}

		// Token: 0x06003E10 RID: 15888 RVA: 0x001564AA File Offset: 0x001546AA
		public QilList Sequence(IList<QilNode> values)
		{
			QilList qilList = this.Sequence();
			qilList.Add(values);
			return qilList;
		}

		// Token: 0x06003E11 RID: 15889 RVA: 0x001564B9 File Offset: 0x001546B9
		public QilParameter Parameter(XmlQueryType xmlType)
		{
			return this.Parameter(null, null, xmlType);
		}

		// Token: 0x06003E12 RID: 15890 RVA: 0x001564C4 File Offset: 0x001546C4
		public QilStrConcat StrConcat(QilNode values)
		{
			return this.StrConcat(this.LiteralString(""), values);
		}

		// Token: 0x06003E13 RID: 15891 RVA: 0x001564D8 File Offset: 0x001546D8
		public QilName LiteralQName(string local)
		{
			return this.LiteralQName(local, string.Empty, string.Empty);
		}

		// Token: 0x06003E14 RID: 15892 RVA: 0x001564EB File Offset: 0x001546EB
		public QilTargetType TypeAssert(QilNode expr, XmlQueryType xmlType)
		{
			return this.TypeAssert(expr, this.LiteralType(xmlType));
		}

		// Token: 0x06003E15 RID: 15893 RVA: 0x001564FB File Offset: 0x001546FB
		public QilTargetType IsType(QilNode expr, XmlQueryType xmlType)
		{
			return this.IsType(expr, this.LiteralType(xmlType));
		}

		// Token: 0x06003E16 RID: 15894 RVA: 0x0015650B File Offset: 0x0015470B
		public QilTargetType XsltConvert(QilNode expr, XmlQueryType xmlType)
		{
			return this.XsltConvert(expr, this.LiteralType(xmlType));
		}

		// Token: 0x06003E17 RID: 15895 RVA: 0x0015651B File Offset: 0x0015471B
		public QilFunction Function(QilNode arguments, QilNode sideEffects, XmlQueryType xmlType)
		{
			return this.Function(arguments, this.Unknown(xmlType), sideEffects, xmlType);
		}

		// Token: 0x06003E18 RID: 15896 RVA: 0x00156530 File Offset: 0x00154730
		public QilExpression QilExpression(QilNode root)
		{
			QilExpression qilExpression = new QilExpression(QilNodeType.QilExpression, root);
			qilExpression.XmlType = this.typeCheck.CheckQilExpression(qilExpression);
			return qilExpression;
		}

		// Token: 0x06003E19 RID: 15897 RVA: 0x00156558 File Offset: 0x00154758
		public QilList FunctionList()
		{
			QilList qilList = new QilList(QilNodeType.FunctionList);
			qilList.XmlType = this.typeCheck.CheckFunctionList(qilList);
			return qilList;
		}

		// Token: 0x06003E1A RID: 15898 RVA: 0x00156580 File Offset: 0x00154780
		public QilList GlobalVariableList()
		{
			QilList qilList = new QilList(QilNodeType.GlobalVariableList);
			qilList.XmlType = this.typeCheck.CheckGlobalVariableList(qilList);
			return qilList;
		}

		// Token: 0x06003E1B RID: 15899 RVA: 0x001565A8 File Offset: 0x001547A8
		public QilList GlobalParameterList()
		{
			QilList qilList = new QilList(QilNodeType.GlobalParameterList);
			qilList.XmlType = this.typeCheck.CheckGlobalParameterList(qilList);
			return qilList;
		}

		// Token: 0x06003E1C RID: 15900 RVA: 0x001565D0 File Offset: 0x001547D0
		public QilList ActualParameterList()
		{
			QilList qilList = new QilList(QilNodeType.ActualParameterList);
			qilList.XmlType = this.typeCheck.CheckActualParameterList(qilList);
			return qilList;
		}

		// Token: 0x06003E1D RID: 15901 RVA: 0x001565F8 File Offset: 0x001547F8
		public QilList FormalParameterList()
		{
			QilList qilList = new QilList(QilNodeType.FormalParameterList);
			qilList.XmlType = this.typeCheck.CheckFormalParameterList(qilList);
			return qilList;
		}

		// Token: 0x06003E1E RID: 15902 RVA: 0x00156620 File Offset: 0x00154820
		public QilList SortKeyList()
		{
			QilList qilList = new QilList(QilNodeType.SortKeyList);
			qilList.XmlType = this.typeCheck.CheckSortKeyList(qilList);
			return qilList;
		}

		// Token: 0x06003E1F RID: 15903 RVA: 0x00156648 File Offset: 0x00154848
		public QilList BranchList()
		{
			QilList qilList = new QilList(QilNodeType.BranchList);
			qilList.XmlType = this.typeCheck.CheckBranchList(qilList);
			return qilList;
		}

		// Token: 0x06003E20 RID: 15904 RVA: 0x00156670 File Offset: 0x00154870
		public QilUnary OptimizeBarrier(QilNode child)
		{
			QilUnary qilUnary = new QilUnary(QilNodeType.OptimizeBarrier, child);
			qilUnary.XmlType = this.typeCheck.CheckOptimizeBarrier(qilUnary);
			return qilUnary;
		}

		// Token: 0x06003E21 RID: 15905 RVA: 0x00156698 File Offset: 0x00154898
		public QilNode Unknown(XmlQueryType xmlType)
		{
			QilNode qilNode = new QilNode(QilNodeType.Unknown, xmlType);
			qilNode.XmlType = this.typeCheck.CheckUnknown(qilNode);
			return qilNode;
		}

		// Token: 0x06003E22 RID: 15906 RVA: 0x001566C4 File Offset: 0x001548C4
		public QilDataSource DataSource(QilNode name, QilNode baseUri)
		{
			QilDataSource qilDataSource = new QilDataSource(QilNodeType.DataSource, name, baseUri);
			qilDataSource.XmlType = this.typeCheck.CheckDataSource(qilDataSource);
			return qilDataSource;
		}

		// Token: 0x06003E23 RID: 15907 RVA: 0x001566F0 File Offset: 0x001548F0
		public QilUnary Nop(QilNode child)
		{
			QilUnary qilUnary = new QilUnary(QilNodeType.Nop, child);
			qilUnary.XmlType = this.typeCheck.CheckNop(qilUnary);
			return qilUnary;
		}

		// Token: 0x06003E24 RID: 15908 RVA: 0x0015671C File Offset: 0x0015491C
		public QilUnary Error(QilNode child)
		{
			QilUnary qilUnary = new QilUnary(QilNodeType.Error, child);
			qilUnary.XmlType = this.typeCheck.CheckError(qilUnary);
			return qilUnary;
		}

		// Token: 0x06003E25 RID: 15909 RVA: 0x00156748 File Offset: 0x00154948
		public QilUnary Warning(QilNode child)
		{
			QilUnary qilUnary = new QilUnary(QilNodeType.Warning, child);
			qilUnary.XmlType = this.typeCheck.CheckWarning(qilUnary);
			return qilUnary;
		}

		// Token: 0x06003E26 RID: 15910 RVA: 0x00156774 File Offset: 0x00154974
		public QilIterator For(QilNode binding)
		{
			QilIterator qilIterator = new QilIterator(QilNodeType.For, binding);
			qilIterator.XmlType = this.typeCheck.CheckFor(qilIterator);
			return qilIterator;
		}

		// Token: 0x06003E27 RID: 15911 RVA: 0x001567A0 File Offset: 0x001549A0
		public QilIterator Let(QilNode binding)
		{
			QilIterator qilIterator = new QilIterator(QilNodeType.Let, binding);
			qilIterator.XmlType = this.typeCheck.CheckLet(qilIterator);
			return qilIterator;
		}

		// Token: 0x06003E28 RID: 15912 RVA: 0x001567CC File Offset: 0x001549CC
		public QilParameter Parameter(QilNode defaultValue, QilNode name, XmlQueryType xmlType)
		{
			QilParameter qilParameter = new QilParameter(QilNodeType.Parameter, defaultValue, name, xmlType);
			qilParameter.XmlType = this.typeCheck.CheckParameter(qilParameter);
			return qilParameter;
		}

		// Token: 0x06003E29 RID: 15913 RVA: 0x001567F8 File Offset: 0x001549F8
		public QilUnary PositionOf(QilNode child)
		{
			QilUnary qilUnary = new QilUnary(QilNodeType.PositionOf, child);
			qilUnary.XmlType = this.typeCheck.CheckPositionOf(qilUnary);
			return qilUnary;
		}

		// Token: 0x06003E2A RID: 15914 RVA: 0x00156824 File Offset: 0x00154A24
		public QilNode True()
		{
			QilNode qilNode = new QilNode(QilNodeType.True);
			qilNode.XmlType = this.typeCheck.CheckTrue(qilNode);
			return qilNode;
		}

		// Token: 0x06003E2B RID: 15915 RVA: 0x0015684C File Offset: 0x00154A4C
		public QilNode False()
		{
			QilNode qilNode = new QilNode(QilNodeType.False);
			qilNode.XmlType = this.typeCheck.CheckFalse(qilNode);
			return qilNode;
		}

		// Token: 0x06003E2C RID: 15916 RVA: 0x00156874 File Offset: 0x00154A74
		public QilLiteral LiteralString(string value)
		{
			QilLiteral qilLiteral = new QilLiteral(QilNodeType.LiteralString, value);
			qilLiteral.XmlType = this.typeCheck.CheckLiteralString(qilLiteral);
			return qilLiteral;
		}

		// Token: 0x06003E2D RID: 15917 RVA: 0x001568A0 File Offset: 0x00154AA0
		public QilLiteral LiteralInt32(int value)
		{
			QilLiteral qilLiteral = new QilLiteral(QilNodeType.LiteralInt32, value);
			qilLiteral.XmlType = this.typeCheck.CheckLiteralInt32(qilLiteral);
			return qilLiteral;
		}

		// Token: 0x06003E2E RID: 15918 RVA: 0x001568D0 File Offset: 0x00154AD0
		public QilLiteral LiteralInt64(long value)
		{
			QilLiteral qilLiteral = new QilLiteral(QilNodeType.LiteralInt64, value);
			qilLiteral.XmlType = this.typeCheck.CheckLiteralInt64(qilLiteral);
			return qilLiteral;
		}

		// Token: 0x06003E2F RID: 15919 RVA: 0x00156900 File Offset: 0x00154B00
		public QilLiteral LiteralDouble(double value)
		{
			QilLiteral qilLiteral = new QilLiteral(QilNodeType.LiteralDouble, value);
			qilLiteral.XmlType = this.typeCheck.CheckLiteralDouble(qilLiteral);
			return qilLiteral;
		}

		// Token: 0x06003E30 RID: 15920 RVA: 0x00156930 File Offset: 0x00154B30
		public QilLiteral LiteralDecimal(decimal value)
		{
			QilLiteral qilLiteral = new QilLiteral(QilNodeType.LiteralDecimal, value);
			qilLiteral.XmlType = this.typeCheck.CheckLiteralDecimal(qilLiteral);
			return qilLiteral;
		}

		// Token: 0x06003E31 RID: 15921 RVA: 0x00156960 File Offset: 0x00154B60
		public QilName LiteralQName(string localName, string namespaceUri, string prefix)
		{
			QilName qilName = new QilName(QilNodeType.LiteralQName, localName, namespaceUri, prefix);
			qilName.XmlType = this.typeCheck.CheckLiteralQName(qilName);
			return qilName;
		}

		// Token: 0x06003E32 RID: 15922 RVA: 0x0015698C File Offset: 0x00154B8C
		public QilLiteral LiteralType(XmlQueryType value)
		{
			QilLiteral qilLiteral = new QilLiteral(QilNodeType.LiteralType, value);
			qilLiteral.XmlType = this.typeCheck.CheckLiteralType(qilLiteral);
			return qilLiteral;
		}

		// Token: 0x06003E33 RID: 15923 RVA: 0x001569B8 File Offset: 0x00154BB8
		public QilLiteral LiteralObject(object value)
		{
			QilLiteral qilLiteral = new QilLiteral(QilNodeType.LiteralObject, value);
			qilLiteral.XmlType = this.typeCheck.CheckLiteralObject(qilLiteral);
			return qilLiteral;
		}

		// Token: 0x06003E34 RID: 15924 RVA: 0x001569E4 File Offset: 0x00154BE4
		public QilBinary And(QilNode left, QilNode right)
		{
			QilBinary qilBinary = new QilBinary(QilNodeType.And, left, right);
			qilBinary.XmlType = this.typeCheck.CheckAnd(qilBinary);
			return qilBinary;
		}

		// Token: 0x06003E35 RID: 15925 RVA: 0x00156A10 File Offset: 0x00154C10
		public QilBinary Or(QilNode left, QilNode right)
		{
			QilBinary qilBinary = new QilBinary(QilNodeType.Or, left, right);
			qilBinary.XmlType = this.typeCheck.CheckOr(qilBinary);
			return qilBinary;
		}

		// Token: 0x06003E36 RID: 15926 RVA: 0x00156A3C File Offset: 0x00154C3C
		public QilUnary Not(QilNode child)
		{
			QilUnary qilUnary = new QilUnary(QilNodeType.Not, child);
			qilUnary.XmlType = this.typeCheck.CheckNot(qilUnary);
			return qilUnary;
		}

		// Token: 0x06003E37 RID: 15927 RVA: 0x00156A68 File Offset: 0x00154C68
		public QilTernary Conditional(QilNode left, QilNode center, QilNode right)
		{
			QilTernary qilTernary = new QilTernary(QilNodeType.Conditional, left, center, right);
			qilTernary.XmlType = this.typeCheck.CheckConditional(qilTernary);
			return qilTernary;
		}

		// Token: 0x06003E38 RID: 15928 RVA: 0x00156A94 File Offset: 0x00154C94
		public QilChoice Choice(QilNode expression, QilNode branches)
		{
			QilChoice qilChoice = new QilChoice(QilNodeType.Choice, expression, branches);
			qilChoice.XmlType = this.typeCheck.CheckChoice(qilChoice);
			return qilChoice;
		}

		// Token: 0x06003E39 RID: 15929 RVA: 0x00156AC0 File Offset: 0x00154CC0
		public QilUnary Length(QilNode child)
		{
			QilUnary qilUnary = new QilUnary(QilNodeType.Length, child);
			qilUnary.XmlType = this.typeCheck.CheckLength(qilUnary);
			return qilUnary;
		}

		// Token: 0x06003E3A RID: 15930 RVA: 0x00156AEC File Offset: 0x00154CEC
		public QilList Sequence()
		{
			QilList qilList = new QilList(QilNodeType.Sequence);
			qilList.XmlType = this.typeCheck.CheckSequence(qilList);
			return qilList;
		}

		// Token: 0x06003E3B RID: 15931 RVA: 0x00156B14 File Offset: 0x00154D14
		public QilBinary Union(QilNode left, QilNode right)
		{
			QilBinary qilBinary = new QilBinary(QilNodeType.Union, left, right);
			qilBinary.XmlType = this.typeCheck.CheckUnion(qilBinary);
			return qilBinary;
		}

		// Token: 0x06003E3C RID: 15932 RVA: 0x00156B40 File Offset: 0x00154D40
		public QilBinary Intersection(QilNode left, QilNode right)
		{
			QilBinary qilBinary = new QilBinary(QilNodeType.Intersection, left, right);
			qilBinary.XmlType = this.typeCheck.CheckIntersection(qilBinary);
			return qilBinary;
		}

		// Token: 0x06003E3D RID: 15933 RVA: 0x00156B6C File Offset: 0x00154D6C
		public QilBinary Difference(QilNode left, QilNode right)
		{
			QilBinary qilBinary = new QilBinary(QilNodeType.Difference, left, right);
			qilBinary.XmlType = this.typeCheck.CheckDifference(qilBinary);
			return qilBinary;
		}

		// Token: 0x06003E3E RID: 15934 RVA: 0x00156B98 File Offset: 0x00154D98
		public QilUnary Average(QilNode child)
		{
			QilUnary qilUnary = new QilUnary(QilNodeType.Average, child);
			qilUnary.XmlType = this.typeCheck.CheckAverage(qilUnary);
			return qilUnary;
		}

		// Token: 0x06003E3F RID: 15935 RVA: 0x00156BC4 File Offset: 0x00154DC4
		public QilUnary Sum(QilNode child)
		{
			QilUnary qilUnary = new QilUnary(QilNodeType.Sum, child);
			qilUnary.XmlType = this.typeCheck.CheckSum(qilUnary);
			return qilUnary;
		}

		// Token: 0x06003E40 RID: 15936 RVA: 0x00156BF0 File Offset: 0x00154DF0
		public QilUnary Minimum(QilNode child)
		{
			QilUnary qilUnary = new QilUnary(QilNodeType.Minimum, child);
			qilUnary.XmlType = this.typeCheck.CheckMinimum(qilUnary);
			return qilUnary;
		}

		// Token: 0x06003E41 RID: 15937 RVA: 0x00156C1C File Offset: 0x00154E1C
		public QilUnary Maximum(QilNode child)
		{
			QilUnary qilUnary = new QilUnary(QilNodeType.Maximum, child);
			qilUnary.XmlType = this.typeCheck.CheckMaximum(qilUnary);
			return qilUnary;
		}

		// Token: 0x06003E42 RID: 15938 RVA: 0x00156C48 File Offset: 0x00154E48
		public QilUnary Negate(QilNode child)
		{
			QilUnary qilUnary = new QilUnary(QilNodeType.Negate, child);
			qilUnary.XmlType = this.typeCheck.CheckNegate(qilUnary);
			return qilUnary;
		}

		// Token: 0x06003E43 RID: 15939 RVA: 0x00156C74 File Offset: 0x00154E74
		public QilBinary Add(QilNode left, QilNode right)
		{
			QilBinary qilBinary = new QilBinary(QilNodeType.Add, left, right);
			qilBinary.XmlType = this.typeCheck.CheckAdd(qilBinary);
			return qilBinary;
		}

		// Token: 0x06003E44 RID: 15940 RVA: 0x00156CA0 File Offset: 0x00154EA0
		public QilBinary Subtract(QilNode left, QilNode right)
		{
			QilBinary qilBinary = new QilBinary(QilNodeType.Subtract, left, right);
			qilBinary.XmlType = this.typeCheck.CheckSubtract(qilBinary);
			return qilBinary;
		}

		// Token: 0x06003E45 RID: 15941 RVA: 0x00156CCC File Offset: 0x00154ECC
		public QilBinary Multiply(QilNode left, QilNode right)
		{
			QilBinary qilBinary = new QilBinary(QilNodeType.Multiply, left, right);
			qilBinary.XmlType = this.typeCheck.CheckMultiply(qilBinary);
			return qilBinary;
		}

		// Token: 0x06003E46 RID: 15942 RVA: 0x00156CF8 File Offset: 0x00154EF8
		public QilBinary Divide(QilNode left, QilNode right)
		{
			QilBinary qilBinary = new QilBinary(QilNodeType.Divide, left, right);
			qilBinary.XmlType = this.typeCheck.CheckDivide(qilBinary);
			return qilBinary;
		}

		// Token: 0x06003E47 RID: 15943 RVA: 0x00156D24 File Offset: 0x00154F24
		public QilBinary Modulo(QilNode left, QilNode right)
		{
			QilBinary qilBinary = new QilBinary(QilNodeType.Modulo, left, right);
			qilBinary.XmlType = this.typeCheck.CheckModulo(qilBinary);
			return qilBinary;
		}

		// Token: 0x06003E48 RID: 15944 RVA: 0x00156D50 File Offset: 0x00154F50
		public QilUnary StrLength(QilNode child)
		{
			QilUnary qilUnary = new QilUnary(QilNodeType.StrLength, child);
			qilUnary.XmlType = this.typeCheck.CheckStrLength(qilUnary);
			return qilUnary;
		}

		// Token: 0x06003E49 RID: 15945 RVA: 0x00156D7C File Offset: 0x00154F7C
		public QilStrConcat StrConcat(QilNode delimiter, QilNode values)
		{
			QilStrConcat qilStrConcat = new QilStrConcat(QilNodeType.StrConcat, delimiter, values);
			qilStrConcat.XmlType = this.typeCheck.CheckStrConcat(qilStrConcat);
			return qilStrConcat;
		}

		// Token: 0x06003E4A RID: 15946 RVA: 0x00156DA8 File Offset: 0x00154FA8
		public QilBinary StrParseQName(QilNode left, QilNode right)
		{
			QilBinary qilBinary = new QilBinary(QilNodeType.StrParseQName, left, right);
			qilBinary.XmlType = this.typeCheck.CheckStrParseQName(qilBinary);
			return qilBinary;
		}

		// Token: 0x06003E4B RID: 15947 RVA: 0x00156DD4 File Offset: 0x00154FD4
		public QilBinary Ne(QilNode left, QilNode right)
		{
			QilBinary qilBinary = new QilBinary(QilNodeType.Ne, left, right);
			qilBinary.XmlType = this.typeCheck.CheckNe(qilBinary);
			return qilBinary;
		}

		// Token: 0x06003E4C RID: 15948 RVA: 0x00156E00 File Offset: 0x00155000
		public QilBinary Eq(QilNode left, QilNode right)
		{
			QilBinary qilBinary = new QilBinary(QilNodeType.Eq, left, right);
			qilBinary.XmlType = this.typeCheck.CheckEq(qilBinary);
			return qilBinary;
		}

		// Token: 0x06003E4D RID: 15949 RVA: 0x00156E2C File Offset: 0x0015502C
		public QilBinary Gt(QilNode left, QilNode right)
		{
			QilBinary qilBinary = new QilBinary(QilNodeType.Gt, left, right);
			qilBinary.XmlType = this.typeCheck.CheckGt(qilBinary);
			return qilBinary;
		}

		// Token: 0x06003E4E RID: 15950 RVA: 0x00156E58 File Offset: 0x00155058
		public QilBinary Ge(QilNode left, QilNode right)
		{
			QilBinary qilBinary = new QilBinary(QilNodeType.Ge, left, right);
			qilBinary.XmlType = this.typeCheck.CheckGe(qilBinary);
			return qilBinary;
		}

		// Token: 0x06003E4F RID: 15951 RVA: 0x00156E84 File Offset: 0x00155084
		public QilBinary Lt(QilNode left, QilNode right)
		{
			QilBinary qilBinary = new QilBinary(QilNodeType.Lt, left, right);
			qilBinary.XmlType = this.typeCheck.CheckLt(qilBinary);
			return qilBinary;
		}

		// Token: 0x06003E50 RID: 15952 RVA: 0x00156EB0 File Offset: 0x001550B0
		public QilBinary Le(QilNode left, QilNode right)
		{
			QilBinary qilBinary = new QilBinary(QilNodeType.Le, left, right);
			qilBinary.XmlType = this.typeCheck.CheckLe(qilBinary);
			return qilBinary;
		}

		// Token: 0x06003E51 RID: 15953 RVA: 0x00156EDC File Offset: 0x001550DC
		public QilBinary Is(QilNode left, QilNode right)
		{
			QilBinary qilBinary = new QilBinary(QilNodeType.Is, left, right);
			qilBinary.XmlType = this.typeCheck.CheckIs(qilBinary);
			return qilBinary;
		}

		// Token: 0x06003E52 RID: 15954 RVA: 0x00156F08 File Offset: 0x00155108
		public QilBinary After(QilNode left, QilNode right)
		{
			QilBinary qilBinary = new QilBinary(QilNodeType.After, left, right);
			qilBinary.XmlType = this.typeCheck.CheckAfter(qilBinary);
			return qilBinary;
		}

		// Token: 0x06003E53 RID: 15955 RVA: 0x00156F34 File Offset: 0x00155134
		public QilBinary Before(QilNode left, QilNode right)
		{
			QilBinary qilBinary = new QilBinary(QilNodeType.Before, left, right);
			qilBinary.XmlType = this.typeCheck.CheckBefore(qilBinary);
			return qilBinary;
		}

		// Token: 0x06003E54 RID: 15956 RVA: 0x00156F60 File Offset: 0x00155160
		public QilLoop Loop(QilNode variable, QilNode body)
		{
			QilLoop qilLoop = new QilLoop(QilNodeType.Loop, variable, body);
			qilLoop.XmlType = this.typeCheck.CheckLoop(qilLoop);
			return qilLoop;
		}

		// Token: 0x06003E55 RID: 15957 RVA: 0x00156F8C File Offset: 0x0015518C
		public QilLoop Filter(QilNode variable, QilNode body)
		{
			QilLoop qilLoop = new QilLoop(QilNodeType.Filter, variable, body);
			qilLoop.XmlType = this.typeCheck.CheckFilter(qilLoop);
			return qilLoop;
		}

		// Token: 0x06003E56 RID: 15958 RVA: 0x00156FB8 File Offset: 0x001551B8
		public QilLoop Sort(QilNode variable, QilNode body)
		{
			QilLoop qilLoop = new QilLoop(QilNodeType.Sort, variable, body);
			qilLoop.XmlType = this.typeCheck.CheckSort(qilLoop);
			return qilLoop;
		}

		// Token: 0x06003E57 RID: 15959 RVA: 0x00156FE4 File Offset: 0x001551E4
		public QilSortKey SortKey(QilNode key, QilNode collation)
		{
			QilSortKey qilSortKey = new QilSortKey(QilNodeType.SortKey, key, collation);
			qilSortKey.XmlType = this.typeCheck.CheckSortKey(qilSortKey);
			return qilSortKey;
		}

		// Token: 0x06003E58 RID: 15960 RVA: 0x00157010 File Offset: 0x00155210
		public QilUnary DocOrderDistinct(QilNode child)
		{
			QilUnary qilUnary = new QilUnary(QilNodeType.DocOrderDistinct, child);
			qilUnary.XmlType = this.typeCheck.CheckDocOrderDistinct(qilUnary);
			return qilUnary;
		}

		// Token: 0x06003E59 RID: 15961 RVA: 0x0015703C File Offset: 0x0015523C
		public QilFunction Function(QilNode arguments, QilNode definition, QilNode sideEffects, XmlQueryType xmlType)
		{
			QilFunction qilFunction = new QilFunction(QilNodeType.Function, arguments, definition, sideEffects, xmlType);
			qilFunction.XmlType = this.typeCheck.CheckFunction(qilFunction);
			return qilFunction;
		}

		// Token: 0x06003E5A RID: 15962 RVA: 0x0015706C File Offset: 0x0015526C
		public QilInvoke Invoke(QilNode function, QilNode arguments)
		{
			QilInvoke qilInvoke = new QilInvoke(QilNodeType.Invoke, function, arguments);
			qilInvoke.XmlType = this.typeCheck.CheckInvoke(qilInvoke);
			return qilInvoke;
		}

		// Token: 0x06003E5B RID: 15963 RVA: 0x00157098 File Offset: 0x00155298
		public QilUnary Content(QilNode child)
		{
			QilUnary qilUnary = new QilUnary(QilNodeType.Content, child);
			qilUnary.XmlType = this.typeCheck.CheckContent(qilUnary);
			return qilUnary;
		}

		// Token: 0x06003E5C RID: 15964 RVA: 0x001570C4 File Offset: 0x001552C4
		public QilBinary Attribute(QilNode left, QilNode right)
		{
			QilBinary qilBinary = new QilBinary(QilNodeType.Attribute, left, right);
			qilBinary.XmlType = this.typeCheck.CheckAttribute(qilBinary);
			return qilBinary;
		}

		// Token: 0x06003E5D RID: 15965 RVA: 0x001570F0 File Offset: 0x001552F0
		public QilUnary Parent(QilNode child)
		{
			QilUnary qilUnary = new QilUnary(QilNodeType.Parent, child);
			qilUnary.XmlType = this.typeCheck.CheckParent(qilUnary);
			return qilUnary;
		}

		// Token: 0x06003E5E RID: 15966 RVA: 0x0015711C File Offset: 0x0015531C
		public QilUnary Root(QilNode child)
		{
			QilUnary qilUnary = new QilUnary(QilNodeType.Root, child);
			qilUnary.XmlType = this.typeCheck.CheckRoot(qilUnary);
			return qilUnary;
		}

		// Token: 0x06003E5F RID: 15967 RVA: 0x00157148 File Offset: 0x00155348
		public QilNode XmlContext()
		{
			QilNode qilNode = new QilNode(QilNodeType.XmlContext);
			qilNode.XmlType = this.typeCheck.CheckXmlContext(qilNode);
			return qilNode;
		}

		// Token: 0x06003E60 RID: 15968 RVA: 0x00157170 File Offset: 0x00155370
		public QilUnary Descendant(QilNode child)
		{
			QilUnary qilUnary = new QilUnary(QilNodeType.Descendant, child);
			qilUnary.XmlType = this.typeCheck.CheckDescendant(qilUnary);
			return qilUnary;
		}

		// Token: 0x06003E61 RID: 15969 RVA: 0x0015719C File Offset: 0x0015539C
		public QilUnary DescendantOrSelf(QilNode child)
		{
			QilUnary qilUnary = new QilUnary(QilNodeType.DescendantOrSelf, child);
			qilUnary.XmlType = this.typeCheck.CheckDescendantOrSelf(qilUnary);
			return qilUnary;
		}

		// Token: 0x06003E62 RID: 15970 RVA: 0x001571C8 File Offset: 0x001553C8
		public QilUnary Ancestor(QilNode child)
		{
			QilUnary qilUnary = new QilUnary(QilNodeType.Ancestor, child);
			qilUnary.XmlType = this.typeCheck.CheckAncestor(qilUnary);
			return qilUnary;
		}

		// Token: 0x06003E63 RID: 15971 RVA: 0x001571F4 File Offset: 0x001553F4
		public QilUnary AncestorOrSelf(QilNode child)
		{
			QilUnary qilUnary = new QilUnary(QilNodeType.AncestorOrSelf, child);
			qilUnary.XmlType = this.typeCheck.CheckAncestorOrSelf(qilUnary);
			return qilUnary;
		}

		// Token: 0x06003E64 RID: 15972 RVA: 0x00157220 File Offset: 0x00155420
		public QilUnary Preceding(QilNode child)
		{
			QilUnary qilUnary = new QilUnary(QilNodeType.Preceding, child);
			qilUnary.XmlType = this.typeCheck.CheckPreceding(qilUnary);
			return qilUnary;
		}

		// Token: 0x06003E65 RID: 15973 RVA: 0x0015724C File Offset: 0x0015544C
		public QilUnary FollowingSibling(QilNode child)
		{
			QilUnary qilUnary = new QilUnary(QilNodeType.FollowingSibling, child);
			qilUnary.XmlType = this.typeCheck.CheckFollowingSibling(qilUnary);
			return qilUnary;
		}

		// Token: 0x06003E66 RID: 15974 RVA: 0x00157278 File Offset: 0x00155478
		public QilUnary PrecedingSibling(QilNode child)
		{
			QilUnary qilUnary = new QilUnary(QilNodeType.PrecedingSibling, child);
			qilUnary.XmlType = this.typeCheck.CheckPrecedingSibling(qilUnary);
			return qilUnary;
		}

		// Token: 0x06003E67 RID: 15975 RVA: 0x001572A4 File Offset: 0x001554A4
		public QilBinary NodeRange(QilNode left, QilNode right)
		{
			QilBinary qilBinary = new QilBinary(QilNodeType.NodeRange, left, right);
			qilBinary.XmlType = this.typeCheck.CheckNodeRange(qilBinary);
			return qilBinary;
		}

		// Token: 0x06003E68 RID: 15976 RVA: 0x001572D0 File Offset: 0x001554D0
		public QilBinary Deref(QilNode left, QilNode right)
		{
			QilBinary qilBinary = new QilBinary(QilNodeType.Deref, left, right);
			qilBinary.XmlType = this.typeCheck.CheckDeref(qilBinary);
			return qilBinary;
		}

		// Token: 0x06003E69 RID: 15977 RVA: 0x001572FC File Offset: 0x001554FC
		public QilBinary ElementCtor(QilNode left, QilNode right)
		{
			QilBinary qilBinary = new QilBinary(QilNodeType.ElementCtor, left, right);
			qilBinary.XmlType = this.typeCheck.CheckElementCtor(qilBinary);
			return qilBinary;
		}

		// Token: 0x06003E6A RID: 15978 RVA: 0x00157328 File Offset: 0x00155528
		public QilBinary AttributeCtor(QilNode left, QilNode right)
		{
			QilBinary qilBinary = new QilBinary(QilNodeType.AttributeCtor, left, right);
			qilBinary.XmlType = this.typeCheck.CheckAttributeCtor(qilBinary);
			return qilBinary;
		}

		// Token: 0x06003E6B RID: 15979 RVA: 0x00157354 File Offset: 0x00155554
		public QilUnary CommentCtor(QilNode child)
		{
			QilUnary qilUnary = new QilUnary(QilNodeType.CommentCtor, child);
			qilUnary.XmlType = this.typeCheck.CheckCommentCtor(qilUnary);
			return qilUnary;
		}

		// Token: 0x06003E6C RID: 15980 RVA: 0x00157380 File Offset: 0x00155580
		public QilBinary PICtor(QilNode left, QilNode right)
		{
			QilBinary qilBinary = new QilBinary(QilNodeType.PICtor, left, right);
			qilBinary.XmlType = this.typeCheck.CheckPICtor(qilBinary);
			return qilBinary;
		}

		// Token: 0x06003E6D RID: 15981 RVA: 0x001573AC File Offset: 0x001555AC
		public QilUnary TextCtor(QilNode child)
		{
			QilUnary qilUnary = new QilUnary(QilNodeType.TextCtor, child);
			qilUnary.XmlType = this.typeCheck.CheckTextCtor(qilUnary);
			return qilUnary;
		}

		// Token: 0x06003E6E RID: 15982 RVA: 0x001573D8 File Offset: 0x001555D8
		public QilUnary RawTextCtor(QilNode child)
		{
			QilUnary qilUnary = new QilUnary(QilNodeType.RawTextCtor, child);
			qilUnary.XmlType = this.typeCheck.CheckRawTextCtor(qilUnary);
			return qilUnary;
		}

		// Token: 0x06003E6F RID: 15983 RVA: 0x00157404 File Offset: 0x00155604
		public QilUnary DocumentCtor(QilNode child)
		{
			QilUnary qilUnary = new QilUnary(QilNodeType.DocumentCtor, child);
			qilUnary.XmlType = this.typeCheck.CheckDocumentCtor(qilUnary);
			return qilUnary;
		}

		// Token: 0x06003E70 RID: 15984 RVA: 0x00157430 File Offset: 0x00155630
		public QilBinary NamespaceDecl(QilNode left, QilNode right)
		{
			QilBinary qilBinary = new QilBinary(QilNodeType.NamespaceDecl, left, right);
			qilBinary.XmlType = this.typeCheck.CheckNamespaceDecl(qilBinary);
			return qilBinary;
		}

		// Token: 0x06003E71 RID: 15985 RVA: 0x0015745C File Offset: 0x0015565C
		public QilBinary RtfCtor(QilNode left, QilNode right)
		{
			QilBinary qilBinary = new QilBinary(QilNodeType.RtfCtor, left, right);
			qilBinary.XmlType = this.typeCheck.CheckRtfCtor(qilBinary);
			return qilBinary;
		}

		// Token: 0x06003E72 RID: 15986 RVA: 0x00157488 File Offset: 0x00155688
		public QilUnary NameOf(QilNode child)
		{
			QilUnary qilUnary = new QilUnary(QilNodeType.NameOf, child);
			qilUnary.XmlType = this.typeCheck.CheckNameOf(qilUnary);
			return qilUnary;
		}

		// Token: 0x06003E73 RID: 15987 RVA: 0x001574B4 File Offset: 0x001556B4
		public QilUnary LocalNameOf(QilNode child)
		{
			QilUnary qilUnary = new QilUnary(QilNodeType.LocalNameOf, child);
			qilUnary.XmlType = this.typeCheck.CheckLocalNameOf(qilUnary);
			return qilUnary;
		}

		// Token: 0x06003E74 RID: 15988 RVA: 0x001574E0 File Offset: 0x001556E0
		public QilUnary NamespaceUriOf(QilNode child)
		{
			QilUnary qilUnary = new QilUnary(QilNodeType.NamespaceUriOf, child);
			qilUnary.XmlType = this.typeCheck.CheckNamespaceUriOf(qilUnary);
			return qilUnary;
		}

		// Token: 0x06003E75 RID: 15989 RVA: 0x0015750C File Offset: 0x0015570C
		public QilUnary PrefixOf(QilNode child)
		{
			QilUnary qilUnary = new QilUnary(QilNodeType.PrefixOf, child);
			qilUnary.XmlType = this.typeCheck.CheckPrefixOf(qilUnary);
			return qilUnary;
		}

		// Token: 0x06003E76 RID: 15990 RVA: 0x00157538 File Offset: 0x00155738
		public QilTargetType TypeAssert(QilNode source, QilNode targetType)
		{
			QilTargetType qilTargetType = new QilTargetType(QilNodeType.TypeAssert, source, targetType);
			qilTargetType.XmlType = this.typeCheck.CheckTypeAssert(qilTargetType);
			return qilTargetType;
		}

		// Token: 0x06003E77 RID: 15991 RVA: 0x00157564 File Offset: 0x00155764
		public QilTargetType IsType(QilNode source, QilNode targetType)
		{
			QilTargetType qilTargetType = new QilTargetType(QilNodeType.IsType, source, targetType);
			qilTargetType.XmlType = this.typeCheck.CheckIsType(qilTargetType);
			return qilTargetType;
		}

		// Token: 0x06003E78 RID: 15992 RVA: 0x00157590 File Offset: 0x00155790
		public QilUnary IsEmpty(QilNode child)
		{
			QilUnary qilUnary = new QilUnary(QilNodeType.IsEmpty, child);
			qilUnary.XmlType = this.typeCheck.CheckIsEmpty(qilUnary);
			return qilUnary;
		}

		// Token: 0x06003E79 RID: 15993 RVA: 0x001575BC File Offset: 0x001557BC
		public QilUnary XPathNodeValue(QilNode child)
		{
			QilUnary qilUnary = new QilUnary(QilNodeType.XPathNodeValue, child);
			qilUnary.XmlType = this.typeCheck.CheckXPathNodeValue(qilUnary);
			return qilUnary;
		}

		// Token: 0x06003E7A RID: 15994 RVA: 0x001575E8 File Offset: 0x001557E8
		public QilUnary XPathFollowing(QilNode child)
		{
			QilUnary qilUnary = new QilUnary(QilNodeType.XPathFollowing, child);
			qilUnary.XmlType = this.typeCheck.CheckXPathFollowing(qilUnary);
			return qilUnary;
		}

		// Token: 0x06003E7B RID: 15995 RVA: 0x00157614 File Offset: 0x00155814
		public QilUnary XPathPreceding(QilNode child)
		{
			QilUnary qilUnary = new QilUnary(QilNodeType.XPathPreceding, child);
			qilUnary.XmlType = this.typeCheck.CheckXPathPreceding(qilUnary);
			return qilUnary;
		}

		// Token: 0x06003E7C RID: 15996 RVA: 0x00157640 File Offset: 0x00155840
		public QilUnary XPathNamespace(QilNode child)
		{
			QilUnary qilUnary = new QilUnary(QilNodeType.XPathNamespace, child);
			qilUnary.XmlType = this.typeCheck.CheckXPathNamespace(qilUnary);
			return qilUnary;
		}

		// Token: 0x06003E7D RID: 15997 RVA: 0x0015766C File Offset: 0x0015586C
		public QilUnary XsltGenerateId(QilNode child)
		{
			QilUnary qilUnary = new QilUnary(QilNodeType.XsltGenerateId, child);
			qilUnary.XmlType = this.typeCheck.CheckXsltGenerateId(qilUnary);
			return qilUnary;
		}

		// Token: 0x06003E7E RID: 15998 RVA: 0x00157698 File Offset: 0x00155898
		public QilInvokeLateBound XsltInvokeLateBound(QilNode name, QilNode arguments)
		{
			QilInvokeLateBound qilInvokeLateBound = new QilInvokeLateBound(QilNodeType.XsltInvokeLateBound, name, arguments);
			qilInvokeLateBound.XmlType = this.typeCheck.CheckXsltInvokeLateBound(qilInvokeLateBound);
			return qilInvokeLateBound;
		}

		// Token: 0x06003E7F RID: 15999 RVA: 0x001576C4 File Offset: 0x001558C4
		public QilInvokeEarlyBound XsltInvokeEarlyBound(QilNode name, QilNode clrMethod, QilNode arguments, XmlQueryType xmlType)
		{
			QilInvokeEarlyBound qilInvokeEarlyBound = new QilInvokeEarlyBound(QilNodeType.XsltInvokeEarlyBound, name, clrMethod, arguments, xmlType);
			qilInvokeEarlyBound.XmlType = this.typeCheck.CheckXsltInvokeEarlyBound(qilInvokeEarlyBound);
			return qilInvokeEarlyBound;
		}

		// Token: 0x06003E80 RID: 16000 RVA: 0x001576F4 File Offset: 0x001558F4
		public QilBinary XsltCopy(QilNode left, QilNode right)
		{
			QilBinary qilBinary = new QilBinary(QilNodeType.XsltCopy, left, right);
			qilBinary.XmlType = this.typeCheck.CheckXsltCopy(qilBinary);
			return qilBinary;
		}

		// Token: 0x06003E81 RID: 16001 RVA: 0x00157720 File Offset: 0x00155920
		public QilUnary XsltCopyOf(QilNode child)
		{
			QilUnary qilUnary = new QilUnary(QilNodeType.XsltCopyOf, child);
			qilUnary.XmlType = this.typeCheck.CheckXsltCopyOf(qilUnary);
			return qilUnary;
		}

		// Token: 0x06003E82 RID: 16002 RVA: 0x0015774C File Offset: 0x0015594C
		public QilTargetType XsltConvert(QilNode source, QilNode targetType)
		{
			QilTargetType qilTargetType = new QilTargetType(QilNodeType.XsltConvert, source, targetType);
			qilTargetType.XmlType = this.typeCheck.CheckXsltConvert(qilTargetType);
			return qilTargetType;
		}

		// Token: 0x06003E83 RID: 16003 RVA: 0x00002F50 File Offset: 0x00001150
		[Conditional("QIL_TRACE_NODE_CREATION")]
		public void TraceNode(QilNode n)
		{
		}

		// Token: 0x04002842 RID: 10306
		private QilTypeChecker typeCheck;
	}
}
