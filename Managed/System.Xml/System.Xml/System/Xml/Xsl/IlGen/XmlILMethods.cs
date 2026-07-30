using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Xml.Schema;
using System.Xml.XPath;
using System.Xml.Xsl.Runtime;

namespace System.Xml.Xsl.IlGen
{
	// Token: 0x02000658 RID: 1624
	internal static class XmlILMethods
	{
		// Token: 0x0600413B RID: 16699 RVA: 0x0015C394 File Offset: 0x0015A594
		static XmlILMethods()
		{
			XmlILMethods.StorageMethods[typeof(string)] = new XmlILStorageMethods(typeof(string));
			XmlILMethods.StorageMethods[typeof(bool)] = new XmlILStorageMethods(typeof(bool));
			XmlILMethods.StorageMethods[typeof(int)] = new XmlILStorageMethods(typeof(int));
			XmlILMethods.StorageMethods[typeof(long)] = new XmlILStorageMethods(typeof(long));
			XmlILMethods.StorageMethods[typeof(decimal)] = new XmlILStorageMethods(typeof(decimal));
			XmlILMethods.StorageMethods[typeof(double)] = new XmlILStorageMethods(typeof(double));
			XmlILMethods.StorageMethods[typeof(float)] = new XmlILStorageMethods(typeof(float));
			XmlILMethods.StorageMethods[typeof(DateTime)] = new XmlILStorageMethods(typeof(DateTime));
			XmlILMethods.StorageMethods[typeof(byte[])] = new XmlILStorageMethods(typeof(byte[]));
			XmlILMethods.StorageMethods[typeof(XmlQualifiedName)] = new XmlILStorageMethods(typeof(XmlQualifiedName));
			XmlILMethods.StorageMethods[typeof(TimeSpan)] = new XmlILStorageMethods(typeof(TimeSpan));
			XmlILMethods.StorageMethods[typeof(XPathItem)] = new XmlILStorageMethods(typeof(XPathItem));
			XmlILMethods.StorageMethods[typeof(XPathNavigator)] = new XmlILStorageMethods(typeof(XPathNavigator));
		}

		// Token: 0x0600413C RID: 16700 RVA: 0x001553B8 File Offset: 0x001535B8
		public static MethodInfo GetMethod(Type className, string methName)
		{
			return className.GetMethod(methName);
		}

		// Token: 0x0600413D RID: 16701 RVA: 0x001553C1 File Offset: 0x001535C1
		public static MethodInfo GetMethod(Type className, string methName, params Type[] args)
		{
			return className.GetMethod(methName, args);
		}

		// Token: 0x04002916 RID: 10518
		public static readonly MethodInfo AncCreate = XmlILMethods.GetMethod(typeof(AncestorIterator), "Create");

		// Token: 0x04002917 RID: 10519
		public static readonly MethodInfo AncNext = XmlILMethods.GetMethod(typeof(AncestorIterator), "MoveNext");

		// Token: 0x04002918 RID: 10520
		public static readonly MethodInfo AncDOCreate = XmlILMethods.GetMethod(typeof(AncestorDocOrderIterator), "Create");

		// Token: 0x04002919 RID: 10521
		public static readonly MethodInfo AncDONext = XmlILMethods.GetMethod(typeof(AncestorDocOrderIterator), "MoveNext");

		// Token: 0x0400291A RID: 10522
		public static readonly MethodInfo AttrContentCreate = XmlILMethods.GetMethod(typeof(AttributeContentIterator), "Create");

		// Token: 0x0400291B RID: 10523
		public static readonly MethodInfo AttrContentNext = XmlILMethods.GetMethod(typeof(AttributeContentIterator), "MoveNext");

		// Token: 0x0400291C RID: 10524
		public static readonly MethodInfo AttrCreate = XmlILMethods.GetMethod(typeof(AttributeIterator), "Create");

		// Token: 0x0400291D RID: 10525
		public static readonly MethodInfo AttrNext = XmlILMethods.GetMethod(typeof(AttributeIterator), "MoveNext");

		// Token: 0x0400291E RID: 10526
		public static readonly MethodInfo ContentCreate = XmlILMethods.GetMethod(typeof(ContentIterator), "Create");

		// Token: 0x0400291F RID: 10527
		public static readonly MethodInfo ContentNext = XmlILMethods.GetMethod(typeof(ContentIterator), "MoveNext");

		// Token: 0x04002920 RID: 10528
		public static readonly MethodInfo ContentMergeCreate = XmlILMethods.GetMethod(typeof(ContentMergeIterator), "Create");

		// Token: 0x04002921 RID: 10529
		public static readonly MethodInfo ContentMergeNext = XmlILMethods.GetMethod(typeof(ContentMergeIterator), "MoveNext");

		// Token: 0x04002922 RID: 10530
		public static readonly MethodInfo DescCreate = XmlILMethods.GetMethod(typeof(DescendantIterator), "Create");

		// Token: 0x04002923 RID: 10531
		public static readonly MethodInfo DescNext = XmlILMethods.GetMethod(typeof(DescendantIterator), "MoveNext");

		// Token: 0x04002924 RID: 10532
		public static readonly MethodInfo DescMergeCreate = XmlILMethods.GetMethod(typeof(DescendantMergeIterator), "Create");

		// Token: 0x04002925 RID: 10533
		public static readonly MethodInfo DescMergeNext = XmlILMethods.GetMethod(typeof(DescendantMergeIterator), "MoveNext");

		// Token: 0x04002926 RID: 10534
		public static readonly MethodInfo DiffCreate = XmlILMethods.GetMethod(typeof(DifferenceIterator), "Create");

		// Token: 0x04002927 RID: 10535
		public static readonly MethodInfo DiffNext = XmlILMethods.GetMethod(typeof(DifferenceIterator), "MoveNext");

		// Token: 0x04002928 RID: 10536
		public static readonly MethodInfo DodMergeCreate = XmlILMethods.GetMethod(typeof(DodSequenceMerge), "Create");

		// Token: 0x04002929 RID: 10537
		public static readonly MethodInfo DodMergeAdd = XmlILMethods.GetMethod(typeof(DodSequenceMerge), "AddSequence");

		// Token: 0x0400292A RID: 10538
		public static readonly MethodInfo DodMergeSeq = XmlILMethods.GetMethod(typeof(DodSequenceMerge), "MergeSequences");

		// Token: 0x0400292B RID: 10539
		public static readonly MethodInfo ElemContentCreate = XmlILMethods.GetMethod(typeof(ElementContentIterator), "Create");

		// Token: 0x0400292C RID: 10540
		public static readonly MethodInfo ElemContentNext = XmlILMethods.GetMethod(typeof(ElementContentIterator), "MoveNext");

		// Token: 0x0400292D RID: 10541
		public static readonly MethodInfo FollSibCreate = XmlILMethods.GetMethod(typeof(FollowingSiblingIterator), "Create");

		// Token: 0x0400292E RID: 10542
		public static readonly MethodInfo FollSibNext = XmlILMethods.GetMethod(typeof(FollowingSiblingIterator), "MoveNext");

		// Token: 0x0400292F RID: 10543
		public static readonly MethodInfo FollSibMergeCreate = XmlILMethods.GetMethod(typeof(FollowingSiblingMergeIterator), "Create");

		// Token: 0x04002930 RID: 10544
		public static readonly MethodInfo FollSibMergeNext = XmlILMethods.GetMethod(typeof(FollowingSiblingMergeIterator), "MoveNext");

		// Token: 0x04002931 RID: 10545
		public static readonly MethodInfo IdCreate = XmlILMethods.GetMethod(typeof(IdIterator), "Create");

		// Token: 0x04002932 RID: 10546
		public static readonly MethodInfo IdNext = XmlILMethods.GetMethod(typeof(IdIterator), "MoveNext");

		// Token: 0x04002933 RID: 10547
		public static readonly MethodInfo InterCreate = XmlILMethods.GetMethod(typeof(IntersectIterator), "Create");

		// Token: 0x04002934 RID: 10548
		public static readonly MethodInfo InterNext = XmlILMethods.GetMethod(typeof(IntersectIterator), "MoveNext");

		// Token: 0x04002935 RID: 10549
		public static readonly MethodInfo KindContentCreate = XmlILMethods.GetMethod(typeof(NodeKindContentIterator), "Create");

		// Token: 0x04002936 RID: 10550
		public static readonly MethodInfo KindContentNext = XmlILMethods.GetMethod(typeof(NodeKindContentIterator), "MoveNext");

		// Token: 0x04002937 RID: 10551
		public static readonly MethodInfo NmspCreate = XmlILMethods.GetMethod(typeof(NamespaceIterator), "Create");

		// Token: 0x04002938 RID: 10552
		public static readonly MethodInfo NmspNext = XmlILMethods.GetMethod(typeof(NamespaceIterator), "MoveNext");

		// Token: 0x04002939 RID: 10553
		public static readonly MethodInfo NodeRangeCreate = XmlILMethods.GetMethod(typeof(NodeRangeIterator), "Create");

		// Token: 0x0400293A RID: 10554
		public static readonly MethodInfo NodeRangeNext = XmlILMethods.GetMethod(typeof(NodeRangeIterator), "MoveNext");

		// Token: 0x0400293B RID: 10555
		public static readonly MethodInfo ParentCreate = XmlILMethods.GetMethod(typeof(ParentIterator), "Create");

		// Token: 0x0400293C RID: 10556
		public static readonly MethodInfo ParentNext = XmlILMethods.GetMethod(typeof(ParentIterator), "MoveNext");

		// Token: 0x0400293D RID: 10557
		public static readonly MethodInfo PrecCreate = XmlILMethods.GetMethod(typeof(PrecedingIterator), "Create");

		// Token: 0x0400293E RID: 10558
		public static readonly MethodInfo PrecNext = XmlILMethods.GetMethod(typeof(PrecedingIterator), "MoveNext");

		// Token: 0x0400293F RID: 10559
		public static readonly MethodInfo PreSibCreate = XmlILMethods.GetMethod(typeof(PrecedingSiblingIterator), "Create");

		// Token: 0x04002940 RID: 10560
		public static readonly MethodInfo PreSibNext = XmlILMethods.GetMethod(typeof(PrecedingSiblingIterator), "MoveNext");

		// Token: 0x04002941 RID: 10561
		public static readonly MethodInfo PreSibDOCreate = XmlILMethods.GetMethod(typeof(PrecedingSiblingDocOrderIterator), "Create");

		// Token: 0x04002942 RID: 10562
		public static readonly MethodInfo PreSibDONext = XmlILMethods.GetMethod(typeof(PrecedingSiblingDocOrderIterator), "MoveNext");

		// Token: 0x04002943 RID: 10563
		public static readonly MethodInfo SortKeyCreate = XmlILMethods.GetMethod(typeof(XmlSortKeyAccumulator), "Create");

		// Token: 0x04002944 RID: 10564
		public static readonly MethodInfo SortKeyDateTime = XmlILMethods.GetMethod(typeof(XmlSortKeyAccumulator), "AddDateTimeSortKey");

		// Token: 0x04002945 RID: 10565
		public static readonly MethodInfo SortKeyDecimal = XmlILMethods.GetMethod(typeof(XmlSortKeyAccumulator), "AddDecimalSortKey");

		// Token: 0x04002946 RID: 10566
		public static readonly MethodInfo SortKeyDouble = XmlILMethods.GetMethod(typeof(XmlSortKeyAccumulator), "AddDoubleSortKey");

		// Token: 0x04002947 RID: 10567
		public static readonly MethodInfo SortKeyEmpty = XmlILMethods.GetMethod(typeof(XmlSortKeyAccumulator), "AddEmptySortKey");

		// Token: 0x04002948 RID: 10568
		public static readonly MethodInfo SortKeyFinish = XmlILMethods.GetMethod(typeof(XmlSortKeyAccumulator), "FinishSortKeys");

		// Token: 0x04002949 RID: 10569
		public static readonly MethodInfo SortKeyInt = XmlILMethods.GetMethod(typeof(XmlSortKeyAccumulator), "AddIntSortKey");

		// Token: 0x0400294A RID: 10570
		public static readonly MethodInfo SortKeyInteger = XmlILMethods.GetMethod(typeof(XmlSortKeyAccumulator), "AddIntegerSortKey");

		// Token: 0x0400294B RID: 10571
		public static readonly MethodInfo SortKeyKeys = XmlILMethods.GetMethod(typeof(XmlSortKeyAccumulator), "get_Keys");

		// Token: 0x0400294C RID: 10572
		public static readonly MethodInfo SortKeyString = XmlILMethods.GetMethod(typeof(XmlSortKeyAccumulator), "AddStringSortKey");

		// Token: 0x0400294D RID: 10573
		public static readonly MethodInfo UnionCreate = XmlILMethods.GetMethod(typeof(UnionIterator), "Create");

		// Token: 0x0400294E RID: 10574
		public static readonly MethodInfo UnionNext = XmlILMethods.GetMethod(typeof(UnionIterator), "MoveNext");

		// Token: 0x0400294F RID: 10575
		public static readonly MethodInfo XPFollCreate = XmlILMethods.GetMethod(typeof(XPathFollowingIterator), "Create");

		// Token: 0x04002950 RID: 10576
		public static readonly MethodInfo XPFollNext = XmlILMethods.GetMethod(typeof(XPathFollowingIterator), "MoveNext");

		// Token: 0x04002951 RID: 10577
		public static readonly MethodInfo XPFollMergeCreate = XmlILMethods.GetMethod(typeof(XPathFollowingMergeIterator), "Create");

		// Token: 0x04002952 RID: 10578
		public static readonly MethodInfo XPFollMergeNext = XmlILMethods.GetMethod(typeof(XPathFollowingMergeIterator), "MoveNext");

		// Token: 0x04002953 RID: 10579
		public static readonly MethodInfo XPPrecCreate = XmlILMethods.GetMethod(typeof(XPathPrecedingIterator), "Create");

		// Token: 0x04002954 RID: 10580
		public static readonly MethodInfo XPPrecNext = XmlILMethods.GetMethod(typeof(XPathPrecedingIterator), "MoveNext");

		// Token: 0x04002955 RID: 10581
		public static readonly MethodInfo XPPrecDOCreate = XmlILMethods.GetMethod(typeof(XPathPrecedingDocOrderIterator), "Create");

		// Token: 0x04002956 RID: 10582
		public static readonly MethodInfo XPPrecDONext = XmlILMethods.GetMethod(typeof(XPathPrecedingDocOrderIterator), "MoveNext");

		// Token: 0x04002957 RID: 10583
		public static readonly MethodInfo XPPrecMergeCreate = XmlILMethods.GetMethod(typeof(XPathPrecedingMergeIterator), "Create");

		// Token: 0x04002958 RID: 10584
		public static readonly MethodInfo XPPrecMergeNext = XmlILMethods.GetMethod(typeof(XPathPrecedingMergeIterator), "MoveNext");

		// Token: 0x04002959 RID: 10585
		public static readonly MethodInfo AddNewIndex = XmlILMethods.GetMethod(typeof(XmlQueryRuntime), "AddNewIndex");

		// Token: 0x0400295A RID: 10586
		public static readonly MethodInfo ChangeTypeXsltArg = XmlILMethods.GetMethod(typeof(XmlQueryRuntime), "ChangeTypeXsltArgument", new Type[]
		{
			typeof(int),
			typeof(object),
			typeof(Type)
		});

		// Token: 0x0400295B RID: 10587
		public static readonly MethodInfo ChangeTypeXsltResult = XmlILMethods.GetMethod(typeof(XmlQueryRuntime), "ChangeTypeXsltResult");

		// Token: 0x0400295C RID: 10588
		public static readonly MethodInfo CompPos = XmlILMethods.GetMethod(typeof(XmlQueryRuntime), "ComparePosition");

		// Token: 0x0400295D RID: 10589
		public static readonly MethodInfo Context = XmlILMethods.GetMethod(typeof(XmlQueryRuntime), "get_ExternalContext");

		// Token: 0x0400295E RID: 10590
		public static readonly MethodInfo CreateCollation = XmlILMethods.GetMethod(typeof(XmlQueryRuntime), "CreateCollation");

		// Token: 0x0400295F RID: 10591
		public static readonly MethodInfo DocOrder = XmlILMethods.GetMethod(typeof(XmlQueryRuntime), "DocOrderDistinct");

		// Token: 0x04002960 RID: 10592
		public static readonly MethodInfo EndRtfConstr = XmlILMethods.GetMethod(typeof(XmlQueryRuntime), "EndRtfConstruction");

		// Token: 0x04002961 RID: 10593
		public static readonly MethodInfo EndSeqConstr = XmlILMethods.GetMethod(typeof(XmlQueryRuntime), "EndSequenceConstruction");

		// Token: 0x04002962 RID: 10594
		public static readonly MethodInfo FindIndex = XmlILMethods.GetMethod(typeof(XmlQueryRuntime), "FindIndex");

		// Token: 0x04002963 RID: 10595
		public static readonly MethodInfo GenId = XmlILMethods.GetMethod(typeof(XmlQueryRuntime), "GenerateId");

		// Token: 0x04002964 RID: 10596
		public static readonly MethodInfo GetAtomizedName = XmlILMethods.GetMethod(typeof(XmlQueryRuntime), "GetAtomizedName");

		// Token: 0x04002965 RID: 10597
		public static readonly MethodInfo GetCollation = XmlILMethods.GetMethod(typeof(XmlQueryRuntime), "GetCollation");

		// Token: 0x04002966 RID: 10598
		public static readonly MethodInfo GetEarly = XmlILMethods.GetMethod(typeof(XmlQueryRuntime), "GetEarlyBoundObject");

		// Token: 0x04002967 RID: 10599
		public static readonly MethodInfo GetNameFilter = XmlILMethods.GetMethod(typeof(XmlQueryRuntime), "GetNameFilter");

		// Token: 0x04002968 RID: 10600
		public static readonly MethodInfo GetOutput = XmlILMethods.GetMethod(typeof(XmlQueryRuntime), "get_Output");

		// Token: 0x04002969 RID: 10601
		public static readonly MethodInfo GetGlobalValue = XmlILMethods.GetMethod(typeof(XmlQueryRuntime), "GetGlobalValue");

		// Token: 0x0400296A RID: 10602
		public static readonly MethodInfo GetTypeFilter = XmlILMethods.GetMethod(typeof(XmlQueryRuntime), "GetTypeFilter");

		// Token: 0x0400296B RID: 10603
		public static readonly MethodInfo GlobalComputed = XmlILMethods.GetMethod(typeof(XmlQueryRuntime), "IsGlobalComputed");

		// Token: 0x0400296C RID: 10604
		public static readonly MethodInfo ItemMatchesCode = XmlILMethods.GetMethod(typeof(XmlQueryRuntime), "MatchesXmlType", new Type[]
		{
			typeof(XPathItem),
			typeof(XmlTypeCode)
		});

		// Token: 0x0400296D RID: 10605
		public static readonly MethodInfo ItemMatchesType = XmlILMethods.GetMethod(typeof(XmlQueryRuntime), "MatchesXmlType", new Type[]
		{
			typeof(XPathItem),
			typeof(int)
		});

		// Token: 0x0400296E RID: 10606
		public static readonly MethodInfo QNameEqualLit = XmlILMethods.GetMethod(typeof(XmlQueryRuntime), "IsQNameEqual", new Type[]
		{
			typeof(XPathNavigator),
			typeof(int),
			typeof(int)
		});

		// Token: 0x0400296F RID: 10607
		public static readonly MethodInfo QNameEqualNav = XmlILMethods.GetMethod(typeof(XmlQueryRuntime), "IsQNameEqual", new Type[]
		{
			typeof(XPathNavigator),
			typeof(XPathNavigator)
		});

		// Token: 0x04002970 RID: 10608
		public static readonly MethodInfo RtfConstr = XmlILMethods.GetMethod(typeof(XmlQueryRuntime), "TextRtfConstruction");

		// Token: 0x04002971 RID: 10609
		public static readonly MethodInfo SendMessage = XmlILMethods.GetMethod(typeof(XmlQueryRuntime), "SendMessage");

		// Token: 0x04002972 RID: 10610
		public static readonly MethodInfo SeqMatchesCode = XmlILMethods.GetMethod(typeof(XmlQueryRuntime), "MatchesXmlType", new Type[]
		{
			typeof(IList<XPathItem>),
			typeof(XmlTypeCode)
		});

		// Token: 0x04002973 RID: 10611
		public static readonly MethodInfo SeqMatchesType = XmlILMethods.GetMethod(typeof(XmlQueryRuntime), "MatchesXmlType", new Type[]
		{
			typeof(IList<XPathItem>),
			typeof(int)
		});

		// Token: 0x04002974 RID: 10612
		public static readonly MethodInfo SetGlobalValue = XmlILMethods.GetMethod(typeof(XmlQueryRuntime), "SetGlobalValue");

		// Token: 0x04002975 RID: 10613
		public static readonly MethodInfo StartRtfConstr = XmlILMethods.GetMethod(typeof(XmlQueryRuntime), "StartRtfConstruction");

		// Token: 0x04002976 RID: 10614
		public static readonly MethodInfo StartSeqConstr = XmlILMethods.GetMethod(typeof(XmlQueryRuntime), "StartSequenceConstruction");

		// Token: 0x04002977 RID: 10615
		public static readonly MethodInfo TagAndMappings = XmlILMethods.GetMethod(typeof(XmlQueryRuntime), "ParseTagName", new Type[]
		{
			typeof(string),
			typeof(int)
		});

		// Token: 0x04002978 RID: 10616
		public static readonly MethodInfo TagAndNamespace = XmlILMethods.GetMethod(typeof(XmlQueryRuntime), "ParseTagName", new Type[]
		{
			typeof(string),
			typeof(string)
		});

		// Token: 0x04002979 RID: 10617
		public static readonly MethodInfo ThrowException = XmlILMethods.GetMethod(typeof(XmlQueryRuntime), "ThrowException");

		// Token: 0x0400297A RID: 10618
		public static readonly MethodInfo XsltLib = XmlILMethods.GetMethod(typeof(XmlQueryRuntime), "get_XsltFunctions");

		// Token: 0x0400297B RID: 10619
		public static readonly MethodInfo GetDataSource = XmlILMethods.GetMethod(typeof(XmlQueryContext), "GetDataSource");

		// Token: 0x0400297C RID: 10620
		public static readonly MethodInfo GetDefaultDataSource = XmlILMethods.GetMethod(typeof(XmlQueryContext), "get_DefaultDataSource");

		// Token: 0x0400297D RID: 10621
		public static readonly MethodInfo GetParam = XmlILMethods.GetMethod(typeof(XmlQueryContext), "GetParameter");

		// Token: 0x0400297E RID: 10622
		public static readonly MethodInfo InvokeXsltLate = XmlILMethods.GetMethod(typeof(XmlQueryContext), "InvokeXsltLateBoundFunction");

		// Token: 0x0400297F RID: 10623
		public static readonly MethodInfo IndexAdd = XmlILMethods.GetMethod(typeof(XmlILIndex), "Add");

		// Token: 0x04002980 RID: 10624
		public static readonly MethodInfo IndexLookup = XmlILMethods.GetMethod(typeof(XmlILIndex), "Lookup");

		// Token: 0x04002981 RID: 10625
		public static readonly MethodInfo ItemIsNode = XmlILMethods.GetMethod(typeof(XPathItem), "get_IsNode");

		// Token: 0x04002982 RID: 10626
		public static readonly MethodInfo Value = XmlILMethods.GetMethod(typeof(XPathItem), "get_Value");

		// Token: 0x04002983 RID: 10627
		public static readonly MethodInfo ValueAsAny = XmlILMethods.GetMethod(typeof(XPathItem), "ValueAs", new Type[]
		{
			typeof(Type),
			typeof(IXmlNamespaceResolver)
		});

		// Token: 0x04002984 RID: 10628
		public static readonly MethodInfo NavClone = XmlILMethods.GetMethod(typeof(XPathNavigator), "Clone");

		// Token: 0x04002985 RID: 10629
		public static readonly MethodInfo NavLocalName = XmlILMethods.GetMethod(typeof(XPathNavigator), "get_LocalName");

		// Token: 0x04002986 RID: 10630
		public static readonly MethodInfo NavMoveAttr = XmlILMethods.GetMethod(typeof(XPathNavigator), "MoveToAttribute", new Type[]
		{
			typeof(string),
			typeof(string)
		});

		// Token: 0x04002987 RID: 10631
		public static readonly MethodInfo NavMoveId = XmlILMethods.GetMethod(typeof(XPathNavigator), "MoveToId");

		// Token: 0x04002988 RID: 10632
		public static readonly MethodInfo NavMoveParent = XmlILMethods.GetMethod(typeof(XPathNavigator), "MoveToParent");

		// Token: 0x04002989 RID: 10633
		public static readonly MethodInfo NavMoveRoot = XmlILMethods.GetMethod(typeof(XPathNavigator), "MoveToRoot");

		// Token: 0x0400298A RID: 10634
		public static readonly MethodInfo NavMoveTo = XmlILMethods.GetMethod(typeof(XPathNavigator), "MoveTo");

		// Token: 0x0400298B RID: 10635
		public static readonly MethodInfo NavNmsp = XmlILMethods.GetMethod(typeof(XPathNavigator), "get_NamespaceURI");

		// Token: 0x0400298C RID: 10636
		public static readonly MethodInfo NavPrefix = XmlILMethods.GetMethod(typeof(XPathNavigator), "get_Prefix");

		// Token: 0x0400298D RID: 10637
		public static readonly MethodInfo NavSamePos = XmlILMethods.GetMethod(typeof(XPathNavigator), "IsSamePosition");

		// Token: 0x0400298E RID: 10638
		public static readonly MethodInfo NavType = XmlILMethods.GetMethod(typeof(XPathNavigator), "get_NodeType");

		// Token: 0x0400298F RID: 10639
		public static readonly MethodInfo StartElemLitName = XmlILMethods.GetMethod(typeof(XmlQueryOutput), "WriteStartElement", new Type[]
		{
			typeof(string),
			typeof(string),
			typeof(string)
		});

		// Token: 0x04002990 RID: 10640
		public static readonly MethodInfo StartElemLocName = XmlILMethods.GetMethod(typeof(XmlQueryOutput), "WriteStartElementLocalName", new Type[] { typeof(string) });

		// Token: 0x04002991 RID: 10641
		public static readonly MethodInfo EndElemStackName = XmlILMethods.GetMethod(typeof(XmlQueryOutput), "WriteEndElement");

		// Token: 0x04002992 RID: 10642
		public static readonly MethodInfo StartAttrLitName = XmlILMethods.GetMethod(typeof(XmlQueryOutput), "WriteStartAttribute", new Type[]
		{
			typeof(string),
			typeof(string),
			typeof(string)
		});

		// Token: 0x04002993 RID: 10643
		public static readonly MethodInfo StartAttrLocName = XmlILMethods.GetMethod(typeof(XmlQueryOutput), "WriteStartAttributeLocalName", new Type[] { typeof(string) });

		// Token: 0x04002994 RID: 10644
		public static readonly MethodInfo EndAttr = XmlILMethods.GetMethod(typeof(XmlQueryOutput), "WriteEndAttribute");

		// Token: 0x04002995 RID: 10645
		public static readonly MethodInfo Text = XmlILMethods.GetMethod(typeof(XmlQueryOutput), "WriteString");

		// Token: 0x04002996 RID: 10646
		public static readonly MethodInfo NoEntText = XmlILMethods.GetMethod(typeof(XmlQueryOutput), "WriteRaw", new Type[] { typeof(string) });

		// Token: 0x04002997 RID: 10647
		public static readonly MethodInfo StartTree = XmlILMethods.GetMethod(typeof(XmlQueryOutput), "StartTree");

		// Token: 0x04002998 RID: 10648
		public static readonly MethodInfo EndTree = XmlILMethods.GetMethod(typeof(XmlQueryOutput), "EndTree");

		// Token: 0x04002999 RID: 10649
		public static readonly MethodInfo StartElemLitNameUn = XmlILMethods.GetMethod(typeof(XmlQueryOutput), "WriteStartElementUnchecked", new Type[]
		{
			typeof(string),
			typeof(string),
			typeof(string)
		});

		// Token: 0x0400299A RID: 10650
		public static readonly MethodInfo StartElemLocNameUn = XmlILMethods.GetMethod(typeof(XmlQueryOutput), "WriteStartElementUnchecked", new Type[] { typeof(string) });

		// Token: 0x0400299B RID: 10651
		public static readonly MethodInfo StartContentUn = XmlILMethods.GetMethod(typeof(XmlQueryOutput), "StartElementContentUnchecked");

		// Token: 0x0400299C RID: 10652
		public static readonly MethodInfo EndElemLitNameUn = XmlILMethods.GetMethod(typeof(XmlQueryOutput), "WriteEndElementUnchecked", new Type[]
		{
			typeof(string),
			typeof(string),
			typeof(string)
		});

		// Token: 0x0400299D RID: 10653
		public static readonly MethodInfo EndElemLocNameUn = XmlILMethods.GetMethod(typeof(XmlQueryOutput), "WriteEndElementUnchecked", new Type[] { typeof(string) });

		// Token: 0x0400299E RID: 10654
		public static readonly MethodInfo StartAttrLitNameUn = XmlILMethods.GetMethod(typeof(XmlQueryOutput), "WriteStartAttributeUnchecked", new Type[]
		{
			typeof(string),
			typeof(string),
			typeof(string)
		});

		// Token: 0x0400299F RID: 10655
		public static readonly MethodInfo StartAttrLocNameUn = XmlILMethods.GetMethod(typeof(XmlQueryOutput), "WriteStartAttributeUnchecked", new Type[] { typeof(string) });

		// Token: 0x040029A0 RID: 10656
		public static readonly MethodInfo EndAttrUn = XmlILMethods.GetMethod(typeof(XmlQueryOutput), "WriteEndAttributeUnchecked");

		// Token: 0x040029A1 RID: 10657
		public static readonly MethodInfo NamespaceDeclUn = XmlILMethods.GetMethod(typeof(XmlQueryOutput), "WriteNamespaceDeclarationUnchecked");

		// Token: 0x040029A2 RID: 10658
		public static readonly MethodInfo TextUn = XmlILMethods.GetMethod(typeof(XmlQueryOutput), "WriteStringUnchecked");

		// Token: 0x040029A3 RID: 10659
		public static readonly MethodInfo NoEntTextUn = XmlILMethods.GetMethod(typeof(XmlQueryOutput), "WriteRawUnchecked");

		// Token: 0x040029A4 RID: 10660
		public static readonly MethodInfo StartRoot = XmlILMethods.GetMethod(typeof(XmlQueryOutput), "WriteStartRoot");

		// Token: 0x040029A5 RID: 10661
		public static readonly MethodInfo EndRoot = XmlILMethods.GetMethod(typeof(XmlQueryOutput), "WriteEndRoot");

		// Token: 0x040029A6 RID: 10662
		public static readonly MethodInfo StartElemCopyName = XmlILMethods.GetMethod(typeof(XmlQueryOutput), "WriteStartElementComputed", new Type[] { typeof(XPathNavigator) });

		// Token: 0x040029A7 RID: 10663
		public static readonly MethodInfo StartElemMapName = XmlILMethods.GetMethod(typeof(XmlQueryOutput), "WriteStartElementComputed", new Type[]
		{
			typeof(string),
			typeof(int)
		});

		// Token: 0x040029A8 RID: 10664
		public static readonly MethodInfo StartElemNmspName = XmlILMethods.GetMethod(typeof(XmlQueryOutput), "WriteStartElementComputed", new Type[]
		{
			typeof(string),
			typeof(string)
		});

		// Token: 0x040029A9 RID: 10665
		public static readonly MethodInfo StartElemQName = XmlILMethods.GetMethod(typeof(XmlQueryOutput), "WriteStartElementComputed", new Type[] { typeof(XmlQualifiedName) });

		// Token: 0x040029AA RID: 10666
		public static readonly MethodInfo StartAttrCopyName = XmlILMethods.GetMethod(typeof(XmlQueryOutput), "WriteStartAttributeComputed", new Type[] { typeof(XPathNavigator) });

		// Token: 0x040029AB RID: 10667
		public static readonly MethodInfo StartAttrMapName = XmlILMethods.GetMethod(typeof(XmlQueryOutput), "WriteStartAttributeComputed", new Type[]
		{
			typeof(string),
			typeof(int)
		});

		// Token: 0x040029AC RID: 10668
		public static readonly MethodInfo StartAttrNmspName = XmlILMethods.GetMethod(typeof(XmlQueryOutput), "WriteStartAttributeComputed", new Type[]
		{
			typeof(string),
			typeof(string)
		});

		// Token: 0x040029AD RID: 10669
		public static readonly MethodInfo StartAttrQName = XmlILMethods.GetMethod(typeof(XmlQueryOutput), "WriteStartAttributeComputed", new Type[] { typeof(XmlQualifiedName) });

		// Token: 0x040029AE RID: 10670
		public static readonly MethodInfo NamespaceDecl = XmlILMethods.GetMethod(typeof(XmlQueryOutput), "WriteNamespaceDeclaration");

		// Token: 0x040029AF RID: 10671
		public static readonly MethodInfo StartComment = XmlILMethods.GetMethod(typeof(XmlQueryOutput), "WriteStartComment");

		// Token: 0x040029B0 RID: 10672
		public static readonly MethodInfo CommentText = XmlILMethods.GetMethod(typeof(XmlQueryOutput), "WriteCommentString");

		// Token: 0x040029B1 RID: 10673
		public static readonly MethodInfo EndComment = XmlILMethods.GetMethod(typeof(XmlQueryOutput), "WriteEndComment");

		// Token: 0x040029B2 RID: 10674
		public static readonly MethodInfo StartPI = XmlILMethods.GetMethod(typeof(XmlQueryOutput), "WriteStartProcessingInstruction");

		// Token: 0x040029B3 RID: 10675
		public static readonly MethodInfo PIText = XmlILMethods.GetMethod(typeof(XmlQueryOutput), "WriteProcessingInstructionString");

		// Token: 0x040029B4 RID: 10676
		public static readonly MethodInfo EndPI = XmlILMethods.GetMethod(typeof(XmlQueryOutput), "WriteEndProcessingInstruction");

		// Token: 0x040029B5 RID: 10677
		public static readonly MethodInfo WriteItem = XmlILMethods.GetMethod(typeof(XmlQueryOutput), "WriteItem");

		// Token: 0x040029B6 RID: 10678
		public static readonly MethodInfo CopyOf = XmlILMethods.GetMethod(typeof(XmlQueryOutput), "XsltCopyOf");

		// Token: 0x040029B7 RID: 10679
		public static readonly MethodInfo StartCopy = XmlILMethods.GetMethod(typeof(XmlQueryOutput), "StartCopy");

		// Token: 0x040029B8 RID: 10680
		public static readonly MethodInfo EndCopy = XmlILMethods.GetMethod(typeof(XmlQueryOutput), "EndCopy");

		// Token: 0x040029B9 RID: 10681
		public static readonly MethodInfo DecAdd = XmlILMethods.GetMethod(typeof(decimal), "Add");

		// Token: 0x040029BA RID: 10682
		public static readonly MethodInfo DecCmp = XmlILMethods.GetMethod(typeof(decimal), "Compare", new Type[]
		{
			typeof(decimal),
			typeof(decimal)
		});

		// Token: 0x040029BB RID: 10683
		public static readonly MethodInfo DecEq = XmlILMethods.GetMethod(typeof(decimal), "Equals", new Type[]
		{
			typeof(decimal),
			typeof(decimal)
		});

		// Token: 0x040029BC RID: 10684
		public static readonly MethodInfo DecSub = XmlILMethods.GetMethod(typeof(decimal), "Subtract");

		// Token: 0x040029BD RID: 10685
		public static readonly MethodInfo DecMul = XmlILMethods.GetMethod(typeof(decimal), "Multiply");

		// Token: 0x040029BE RID: 10686
		public static readonly MethodInfo DecDiv = XmlILMethods.GetMethod(typeof(decimal), "Divide");

		// Token: 0x040029BF RID: 10687
		public static readonly MethodInfo DecRem = XmlILMethods.GetMethod(typeof(decimal), "Remainder");

		// Token: 0x040029C0 RID: 10688
		public static readonly MethodInfo DecNeg = XmlILMethods.GetMethod(typeof(decimal), "Negate");

		// Token: 0x040029C1 RID: 10689
		public static readonly MethodInfo QNameEq = XmlILMethods.GetMethod(typeof(XmlQualifiedName), "Equals");

		// Token: 0x040029C2 RID: 10690
		public static readonly MethodInfo StrEq = XmlILMethods.GetMethod(typeof(string), "Equals", new Type[]
		{
			typeof(string),
			typeof(string)
		});

		// Token: 0x040029C3 RID: 10691
		public static readonly MethodInfo StrCat2 = XmlILMethods.GetMethod(typeof(string), "Concat", new Type[]
		{
			typeof(string),
			typeof(string)
		});

		// Token: 0x040029C4 RID: 10692
		public static readonly MethodInfo StrCat3 = XmlILMethods.GetMethod(typeof(string), "Concat", new Type[]
		{
			typeof(string),
			typeof(string),
			typeof(string)
		});

		// Token: 0x040029C5 RID: 10693
		public static readonly MethodInfo StrCat4 = XmlILMethods.GetMethod(typeof(string), "Concat", new Type[]
		{
			typeof(string),
			typeof(string),
			typeof(string),
			typeof(string)
		});

		// Token: 0x040029C6 RID: 10694
		public static readonly MethodInfo StrCmp = XmlILMethods.GetMethod(typeof(string), "CompareOrdinal", new Type[]
		{
			typeof(string),
			typeof(string)
		});

		// Token: 0x040029C7 RID: 10695
		public static readonly MethodInfo StrLen = XmlILMethods.GetMethod(typeof(string), "get_Length");

		// Token: 0x040029C8 RID: 10696
		public static readonly MethodInfo DblToDec = XmlILMethods.GetMethod(typeof(XsltConvert), "ToDecimal", new Type[] { typeof(double) });

		// Token: 0x040029C9 RID: 10697
		public static readonly MethodInfo DblToInt = XmlILMethods.GetMethod(typeof(XsltConvert), "ToInt", new Type[] { typeof(double) });

		// Token: 0x040029CA RID: 10698
		public static readonly MethodInfo DblToLng = XmlILMethods.GetMethod(typeof(XsltConvert), "ToLong", new Type[] { typeof(double) });

		// Token: 0x040029CB RID: 10699
		public static readonly MethodInfo DblToStr = XmlILMethods.GetMethod(typeof(XsltConvert), "ToString", new Type[] { typeof(double) });

		// Token: 0x040029CC RID: 10700
		public static readonly MethodInfo DecToDbl = XmlILMethods.GetMethod(typeof(XsltConvert), "ToDouble", new Type[] { typeof(decimal) });

		// Token: 0x040029CD RID: 10701
		public static readonly MethodInfo DTToStr = XmlILMethods.GetMethod(typeof(XsltConvert), "ToString", new Type[] { typeof(DateTime) });

		// Token: 0x040029CE RID: 10702
		public static readonly MethodInfo IntToDbl = XmlILMethods.GetMethod(typeof(XsltConvert), "ToDouble", new Type[] { typeof(int) });

		// Token: 0x040029CF RID: 10703
		public static readonly MethodInfo LngToDbl = XmlILMethods.GetMethod(typeof(XsltConvert), "ToDouble", new Type[] { typeof(long) });

		// Token: 0x040029D0 RID: 10704
		public static readonly MethodInfo StrToDbl = XmlILMethods.GetMethod(typeof(XsltConvert), "ToDouble", new Type[] { typeof(string) });

		// Token: 0x040029D1 RID: 10705
		public static readonly MethodInfo StrToDT = XmlILMethods.GetMethod(typeof(XsltConvert), "ToDateTime", new Type[] { typeof(string) });

		// Token: 0x040029D2 RID: 10706
		public static readonly MethodInfo ItemToBool = XmlILMethods.GetMethod(typeof(XsltConvert), "ToBoolean", new Type[] { typeof(XPathItem) });

		// Token: 0x040029D3 RID: 10707
		public static readonly MethodInfo ItemToDbl = XmlILMethods.GetMethod(typeof(XsltConvert), "ToDouble", new Type[] { typeof(XPathItem) });

		// Token: 0x040029D4 RID: 10708
		public static readonly MethodInfo ItemToStr = XmlILMethods.GetMethod(typeof(XsltConvert), "ToString", new Type[] { typeof(XPathItem) });

		// Token: 0x040029D5 RID: 10709
		public static readonly MethodInfo ItemToNode = XmlILMethods.GetMethod(typeof(XsltConvert), "ToNode", new Type[] { typeof(XPathItem) });

		// Token: 0x040029D6 RID: 10710
		public static readonly MethodInfo ItemToNodes = XmlILMethods.GetMethod(typeof(XsltConvert), "ToNodeSet", new Type[] { typeof(XPathItem) });

		// Token: 0x040029D7 RID: 10711
		public static readonly MethodInfo ItemsToBool = XmlILMethods.GetMethod(typeof(XsltConvert), "ToBoolean", new Type[] { typeof(IList<XPathItem>) });

		// Token: 0x040029D8 RID: 10712
		public static readonly MethodInfo ItemsToDbl = XmlILMethods.GetMethod(typeof(XsltConvert), "ToDouble", new Type[] { typeof(IList<XPathItem>) });

		// Token: 0x040029D9 RID: 10713
		public static readonly MethodInfo ItemsToNode = XmlILMethods.GetMethod(typeof(XsltConvert), "ToNode", new Type[] { typeof(IList<XPathItem>) });

		// Token: 0x040029DA RID: 10714
		public static readonly MethodInfo ItemsToNodes = XmlILMethods.GetMethod(typeof(XsltConvert), "ToNodeSet", new Type[] { typeof(IList<XPathItem>) });

		// Token: 0x040029DB RID: 10715
		public static readonly MethodInfo ItemsToStr = XmlILMethods.GetMethod(typeof(XsltConvert), "ToString", new Type[] { typeof(IList<XPathItem>) });

		// Token: 0x040029DC RID: 10716
		public static readonly MethodInfo StrCatCat = XmlILMethods.GetMethod(typeof(StringConcat), "Concat");

		// Token: 0x040029DD RID: 10717
		public static readonly MethodInfo StrCatClear = XmlILMethods.GetMethod(typeof(StringConcat), "Clear");

		// Token: 0x040029DE RID: 10718
		public static readonly MethodInfo StrCatResult = XmlILMethods.GetMethod(typeof(StringConcat), "GetResult");

		// Token: 0x040029DF RID: 10719
		public static readonly MethodInfo StrCatDelim = XmlILMethods.GetMethod(typeof(StringConcat), "set_Delimiter");

		// Token: 0x040029E0 RID: 10720
		public static readonly MethodInfo NavsToItems = XmlILMethods.GetMethod(typeof(XmlILStorageConverter), "NavigatorsToItems");

		// Token: 0x040029E1 RID: 10721
		public static readonly MethodInfo ItemsToNavs = XmlILMethods.GetMethod(typeof(XmlILStorageConverter), "ItemsToNavigators");

		// Token: 0x040029E2 RID: 10722
		public static readonly MethodInfo SetDod = XmlILMethods.GetMethod(typeof(XmlQueryNodeSequence), "set_IsDocOrderDistinct");

		// Token: 0x040029E3 RID: 10723
		public static readonly MethodInfo GetTypeFromHandle = XmlILMethods.GetMethod(typeof(Type), "GetTypeFromHandle");

		// Token: 0x040029E4 RID: 10724
		public static readonly MethodInfo InitializeArray = XmlILMethods.GetMethod(typeof(RuntimeHelpers), "InitializeArray");

		// Token: 0x040029E5 RID: 10725
		public static readonly Dictionary<Type, XmlILStorageMethods> StorageMethods = new Dictionary<Type, XmlILStorageMethods>();
	}
}
