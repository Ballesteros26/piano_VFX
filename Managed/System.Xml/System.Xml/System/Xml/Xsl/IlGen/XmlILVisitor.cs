using System;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;
using System.Xml.Schema;
using System.Xml.Utils;
using System.Xml.XPath;
using System.Xml.Xsl.Qil;
using System.Xml.Xsl.Runtime;

namespace System.Xml.Xsl.IlGen
{
	// Token: 0x02000675 RID: 1653
	internal class XmlILVisitor : QilVisitor
	{
		// Token: 0x060042BE RID: 17086 RVA: 0x00169980 File Offset: 0x00167B80
		public void Visit(QilExpression qil, GenerateHelper helper, MethodInfo methRoot)
		{
			this.qil = qil;
			this.helper = helper;
			this.iterNested = null;
			this.indexId = 0;
			this.PrepareGlobalValues(qil.GlobalParameterList);
			this.PrepareGlobalValues(qil.GlobalVariableList);
			this.VisitGlobalValues(qil.GlobalParameterList);
			this.VisitGlobalValues(qil.GlobalVariableList);
			foreach (QilNode qilNode in qil.FunctionList)
			{
				QilFunction qilFunction = (QilFunction)qilNode;
				this.Function(qilFunction);
			}
			this.helper.MethodBegin(methRoot, null, true);
			this.StartNestedIterator(qil.Root);
			this.Visit(qil.Root);
			this.EndNestedIterator(qil.Root);
			this.helper.MethodEnd();
		}

		// Token: 0x060042BF RID: 17087 RVA: 0x00169A5C File Offset: 0x00167C5C
		private void PrepareGlobalValues(QilList globalIterators)
		{
			foreach (QilNode qilNode in globalIterators)
			{
				QilIterator qilIterator = (QilIterator)qilNode;
				MethodInfo functionBinding = XmlILAnnotation.Write(qilIterator).FunctionBinding;
				IteratorDescriptor iteratorDescriptor = new IteratorDescriptor(this.helper);
				iteratorDescriptor.Storage = StorageDescriptor.Global(functionBinding, this.GetItemStorageType(qilIterator), !qilIterator.XmlType.IsSingleton);
				XmlILAnnotation.Write(qilIterator).CachedIteratorDescriptor = iteratorDescriptor;
			}
		}

		// Token: 0x060042C0 RID: 17088 RVA: 0x00169AE8 File Offset: 0x00167CE8
		private void VisitGlobalValues(QilList globalIterators)
		{
			foreach (QilNode qilNode in globalIterators)
			{
				QilIterator qilIterator = (QilIterator)qilNode;
				QilParameter qilParameter = qilIterator as QilParameter;
				MethodInfo globalLocation = XmlILAnnotation.Write(qilIterator).CachedIteratorDescriptor.Storage.GlobalLocation;
				bool flag = !qilIterator.XmlType.IsSingleton;
				int num = this.helper.StaticData.DeclareGlobalValue(qilIterator.DebugName);
				this.helper.MethodBegin(globalLocation, qilIterator.SourceLine, false);
				Label label = this.helper.DefineLabel();
				Label label2 = this.helper.DefineLabel();
				this.helper.LoadQueryRuntime();
				this.helper.LoadInteger(num);
				this.helper.Call(XmlILMethods.GlobalComputed);
				this.helper.Emit(OpCodes.Brtrue, label);
				this.StartNestedIterator(qilIterator);
				if (qilParameter != null)
				{
					LocalBuilder localBuilder = this.helper.DeclareLocal("$$$param", typeof(object));
					this.helper.CallGetParameter(qilParameter.Name.LocalName, qilParameter.Name.NamespaceUri);
					this.helper.Emit(OpCodes.Stloc, localBuilder);
					this.helper.Emit(OpCodes.Ldloc, localBuilder);
					this.helper.Emit(OpCodes.Brfalse, label2);
					this.helper.LoadQueryRuntime();
					this.helper.LoadInteger(num);
					this.helper.LoadQueryRuntime();
					this.helper.LoadInteger(this.helper.StaticData.DeclareXmlType(XmlQueryTypeFactory.ItemS));
					this.helper.Emit(OpCodes.Ldloc, localBuilder);
					this.helper.Call(XmlILMethods.ChangeTypeXsltResult);
					this.helper.CallSetGlobalValue(typeof(object));
					this.helper.EmitUnconditionalBranch(OpCodes.Br, label);
				}
				this.helper.MarkLabel(label2);
				if (qilIterator.Binding != null)
				{
					this.helper.LoadQueryRuntime();
					this.helper.LoadInteger(num);
					this.NestedVisitEnsureStack(qilIterator.Binding, this.GetItemStorageType(qilIterator), flag);
					this.helper.CallSetGlobalValue(this.GetStorageType(qilIterator));
				}
				else
				{
					this.helper.LoadQueryRuntime();
					this.helper.Emit(OpCodes.Ldstr, Res.GetString("Supplied XsltArgumentList does not contain a parameter with local name '{0}' and namespace '{1}'.", new string[]
					{
						qilParameter.Name.LocalName,
						qilParameter.Name.NamespaceUri
					}));
					this.helper.Call(XmlILMethods.ThrowException);
				}
				this.EndNestedIterator(qilIterator);
				this.helper.MarkLabel(label);
				this.helper.CallGetGlobalValue(num, this.GetStorageType(qilIterator));
				this.helper.MethodEnd();
			}
		}

		// Token: 0x060042C1 RID: 17089 RVA: 0x00169DEC File Offset: 0x00167FEC
		private void Function(QilFunction ndFunc)
		{
			foreach (QilNode qilNode in ndFunc.Arguments)
			{
				QilIterator qilIterator = (QilIterator)qilNode;
				IteratorDescriptor iteratorDescriptor = new IteratorDescriptor(this.helper);
				int num = XmlILAnnotation.Write(qilIterator).ArgumentPosition + 1;
				iteratorDescriptor.Storage = StorageDescriptor.Parameter(num, this.GetItemStorageType(qilIterator), !qilIterator.XmlType.IsSingleton);
				XmlILAnnotation.Write(qilIterator).CachedIteratorDescriptor = iteratorDescriptor;
			}
			MethodInfo functionBinding = XmlILAnnotation.Write(ndFunc).FunctionBinding;
			bool flag = XmlILConstructInfo.Read(ndFunc).ConstructMethod == XmlILConstructMethod.Writer;
			this.helper.MethodBegin(functionBinding, ndFunc.SourceLine, flag);
			foreach (QilNode qilNode2 in ndFunc.Arguments)
			{
				QilIterator qilIterator2 = (QilIterator)qilNode2;
				if (this.qil.IsDebug && qilIterator2.SourceLine != null)
				{
					this.helper.DebugSequencePoint(qilIterator2.SourceLine);
				}
				if (qilIterator2.Binding != null)
				{
					int num = (qilIterator2.Annotation as XmlILAnnotation).ArgumentPosition + 1;
					Label label = this.helper.DefineLabel();
					this.helper.LoadQueryRuntime();
					this.helper.LoadParameter(num);
					this.helper.LoadInteger(29);
					this.helper.Call(XmlILMethods.SeqMatchesCode);
					this.helper.Emit(OpCodes.Brfalse, label);
					this.StartNestedIterator(qilIterator2);
					this.NestedVisitEnsureStack(qilIterator2.Binding, this.GetItemStorageType(qilIterator2), !qilIterator2.XmlType.IsSingleton);
					this.EndNestedIterator(qilIterator2);
					this.helper.SetParameter(num);
					this.helper.MarkLabel(label);
				}
			}
			this.StartNestedIterator(ndFunc);
			if (flag)
			{
				this.NestedVisit(ndFunc.Definition);
			}
			else
			{
				this.NestedVisitEnsureStack(ndFunc.Definition, this.GetItemStorageType(ndFunc), !ndFunc.XmlType.IsSingleton);
			}
			this.EndNestedIterator(ndFunc);
			this.helper.MethodEnd();
		}

		// Token: 0x060042C2 RID: 17090 RVA: 0x0016A04C File Offset: 0x0016824C
		protected override QilNode Visit(QilNode nd)
		{
			if (nd == null)
			{
				return null;
			}
			if (this.qil.IsDebug && nd.SourceLine != null && !(nd is QilIterator))
			{
				this.helper.DebugSequencePoint(nd.SourceLine);
			}
			switch (XmlILConstructInfo.Read(nd).ConstructMethod)
			{
			case XmlILConstructMethod.WriterThenIterator:
				this.NestedConstruction(nd);
				return nd;
			case XmlILConstructMethod.IteratorThenWriter:
				this.CopySequence(nd);
				return nd;
			}
			base.Visit(nd);
			return nd;
		}

		// Token: 0x060042C3 RID: 17091 RVA: 0x0000206B File Offset: 0x0000026B
		protected override QilNode VisitChildren(QilNode parent)
		{
			return parent;
		}

		// Token: 0x060042C4 RID: 17092 RVA: 0x0016A0CB File Offset: 0x001682CB
		private void NestedConstruction(QilNode nd)
		{
			this.helper.CallStartSequenceConstruction();
			base.Visit(nd);
			this.helper.CallEndSequenceConstruction();
			this.iterCurr.Storage = StorageDescriptor.Stack(typeof(XPathItem), true);
		}

		// Token: 0x060042C5 RID: 17093 RVA: 0x0016A108 File Offset: 0x00168308
		private void CopySequence(QilNode nd)
		{
			XmlQueryType xmlType = nd.XmlType;
			bool flag;
			Label label;
			this.StartWriterLoop(nd, out flag, out label);
			if (xmlType.IsSingleton)
			{
				this.helper.LoadQueryOutput();
				base.Visit(nd);
				this.iterCurr.EnsureItemStorageType(nd.XmlType, typeof(XPathItem));
			}
			else
			{
				base.Visit(nd);
				this.iterCurr.EnsureItemStorageType(nd.XmlType, typeof(XPathItem));
				this.iterCurr.EnsureNoStackNoCache("$$$copyTemp");
				this.helper.LoadQueryOutput();
			}
			this.iterCurr.EnsureStackNoCache();
			this.helper.Call(XmlILMethods.WriteItem);
			this.EndWriterLoop(nd, flag, label);
		}

		// Token: 0x060042C6 RID: 17094 RVA: 0x0016A1C0 File Offset: 0x001683C0
		protected override QilNode VisitDataSource(QilDataSource ndSrc)
		{
			this.helper.LoadQueryContext();
			this.NestedVisitEnsureStack(ndSrc.Name);
			this.NestedVisitEnsureStack(ndSrc.BaseUri);
			this.helper.Call(XmlILMethods.GetDataSource);
			LocalBuilder localBuilder = this.helper.DeclareLocal("$$$navDoc", typeof(XPathNavigator));
			this.helper.Emit(OpCodes.Stloc, localBuilder);
			this.helper.Emit(OpCodes.Ldloc, localBuilder);
			this.helper.Emit(OpCodes.Brfalse, this.iterCurr.GetLabelNext());
			this.iterCurr.Storage = StorageDescriptor.Local(localBuilder, typeof(XPathNavigator), false);
			return ndSrc;
		}

		// Token: 0x060042C7 RID: 17095 RVA: 0x0016A275 File Offset: 0x00168475
		protected override QilNode VisitNop(QilUnary ndNop)
		{
			return this.Visit(ndNop.Child);
		}

		// Token: 0x060042C8 RID: 17096 RVA: 0x0016A275 File Offset: 0x00168475
		protected override QilNode VisitOptimizeBarrier(QilUnary ndBarrier)
		{
			return this.Visit(ndBarrier.Child);
		}

		// Token: 0x060042C9 RID: 17097 RVA: 0x0016A284 File Offset: 0x00168484
		protected override QilNode VisitError(QilUnary ndErr)
		{
			this.helper.LoadQueryRuntime();
			this.NestedVisitEnsureStack(ndErr.Child);
			this.helper.Call(XmlILMethods.ThrowException);
			if (XmlILConstructInfo.Read(ndErr).ConstructMethod == XmlILConstructMethod.Writer)
			{
				this.iterCurr.Storage = StorageDescriptor.None();
			}
			else
			{
				this.helper.Emit(OpCodes.Ldnull);
				this.iterCurr.Storage = StorageDescriptor.Stack(typeof(XPathItem), false);
			}
			return ndErr;
		}

		// Token: 0x060042CA RID: 17098 RVA: 0x0016A304 File Offset: 0x00168504
		protected override QilNode VisitWarning(QilUnary ndWarning)
		{
			this.helper.LoadQueryRuntime();
			this.NestedVisitEnsureStack(ndWarning.Child);
			this.helper.Call(XmlILMethods.SendMessage);
			if (XmlILConstructInfo.Read(ndWarning).ConstructMethod == XmlILConstructMethod.Writer)
			{
				this.iterCurr.Storage = StorageDescriptor.None();
			}
			else
			{
				this.VisitEmpty(ndWarning);
			}
			return ndWarning;
		}

		// Token: 0x060042CB RID: 17099 RVA: 0x0016A360 File Offset: 0x00168560
		protected override QilNode VisitTrue(QilNode ndTrue)
		{
			if (this.iterCurr.CurrentBranchingContext != BranchingContext.None)
			{
				this.helper.EmitUnconditionalBranch((this.iterCurr.CurrentBranchingContext == BranchingContext.OnTrue) ? OpCodes.Brtrue : OpCodes.Brfalse, this.iterCurr.LabelBranch);
				this.iterCurr.Storage = StorageDescriptor.None();
			}
			else
			{
				this.helper.LoadBoolean(true);
				this.iterCurr.Storage = StorageDescriptor.Stack(typeof(bool), false);
			}
			return ndTrue;
		}

		// Token: 0x060042CC RID: 17100 RVA: 0x0016A3E4 File Offset: 0x001685E4
		protected override QilNode VisitFalse(QilNode ndFalse)
		{
			if (this.iterCurr.CurrentBranchingContext != BranchingContext.None)
			{
				this.helper.EmitUnconditionalBranch((this.iterCurr.CurrentBranchingContext == BranchingContext.OnFalse) ? OpCodes.Brtrue : OpCodes.Brfalse, this.iterCurr.LabelBranch);
				this.iterCurr.Storage = StorageDescriptor.None();
			}
			else
			{
				this.helper.LoadBoolean(false);
				this.iterCurr.Storage = StorageDescriptor.Stack(typeof(bool), false);
			}
			return ndFalse;
		}

		// Token: 0x060042CD RID: 17101 RVA: 0x0016A468 File Offset: 0x00168668
		protected override QilNode VisitLiteralString(QilLiteral ndStr)
		{
			this.helper.Emit(OpCodes.Ldstr, ndStr);
			this.iterCurr.Storage = StorageDescriptor.Stack(typeof(string), false);
			return ndStr;
		}

		// Token: 0x060042CE RID: 17102 RVA: 0x0016A49C File Offset: 0x0016869C
		protected override QilNode VisitLiteralInt32(QilLiteral ndInt)
		{
			this.helper.LoadInteger(ndInt);
			this.iterCurr.Storage = StorageDescriptor.Stack(typeof(int), false);
			return ndInt;
		}

		// Token: 0x060042CF RID: 17103 RVA: 0x0016A4CB File Offset: 0x001686CB
		protected override QilNode VisitLiteralInt64(QilLiteral ndLong)
		{
			this.helper.Emit(OpCodes.Ldc_I8, ndLong);
			this.iterCurr.Storage = StorageDescriptor.Stack(typeof(long), false);
			return ndLong;
		}

		// Token: 0x060042D0 RID: 17104 RVA: 0x0016A4FF File Offset: 0x001686FF
		protected override QilNode VisitLiteralDouble(QilLiteral ndDbl)
		{
			this.helper.Emit(OpCodes.Ldc_R8, ndDbl);
			this.iterCurr.Storage = StorageDescriptor.Stack(typeof(double), false);
			return ndDbl;
		}

		// Token: 0x060042D1 RID: 17105 RVA: 0x0016A534 File Offset: 0x00168734
		protected override QilNode VisitLiteralDecimal(QilLiteral ndDec)
		{
			this.helper.ConstructLiteralDecimal(ndDec);
			this.iterCurr.Storage = StorageDescriptor.Stack(typeof(decimal), false);
			return ndDec;
		}

		// Token: 0x060042D2 RID: 17106 RVA: 0x0016A563 File Offset: 0x00168763
		protected override QilNode VisitLiteralQName(QilName ndQName)
		{
			this.helper.ConstructLiteralQName(ndQName.LocalName, ndQName.NamespaceUri);
			this.iterCurr.Storage = StorageDescriptor.Stack(typeof(XmlQualifiedName), false);
			return ndQName;
		}

		// Token: 0x060042D3 RID: 17107 RVA: 0x0016A598 File Offset: 0x00168798
		protected override QilNode VisitAnd(QilBinary ndAnd)
		{
			IteratorDescriptor iteratorDescriptor = this.iterCurr;
			this.StartNestedIterator(ndAnd.Left);
			Label label = this.StartConjunctiveTests(iteratorDescriptor.CurrentBranchingContext, iteratorDescriptor.LabelBranch);
			this.Visit(ndAnd.Left);
			this.EndNestedIterator(ndAnd.Left);
			this.StartNestedIterator(ndAnd.Right);
			this.StartLastConjunctiveTest(iteratorDescriptor.CurrentBranchingContext, iteratorDescriptor.LabelBranch, label);
			this.Visit(ndAnd.Right);
			this.EndNestedIterator(ndAnd.Right);
			this.EndConjunctiveTests(iteratorDescriptor.CurrentBranchingContext, iteratorDescriptor.LabelBranch, label);
			return ndAnd;
		}

		// Token: 0x060042D4 RID: 17108 RVA: 0x0016A630 File Offset: 0x00168830
		private Label StartConjunctiveTests(BranchingContext brctxt, Label lblBranch)
		{
			if (brctxt == BranchingContext.OnFalse)
			{
				this.iterCurr.SetBranching(BranchingContext.OnFalse, lblBranch);
				return lblBranch;
			}
			Label label = this.helper.DefineLabel();
			this.iterCurr.SetBranching(BranchingContext.OnFalse, label);
			return label;
		}

		// Token: 0x060042D5 RID: 17109 RVA: 0x0016A66A File Offset: 0x0016886A
		private void StartLastConjunctiveTest(BranchingContext brctxt, Label lblBranch, Label lblOnFalse)
		{
			if (brctxt == BranchingContext.OnTrue)
			{
				this.iterCurr.SetBranching(BranchingContext.OnTrue, lblBranch);
				return;
			}
			this.iterCurr.SetBranching(BranchingContext.OnFalse, lblOnFalse);
		}

		// Token: 0x060042D6 RID: 17110 RVA: 0x0016A68C File Offset: 0x0016888C
		private void EndConjunctiveTests(BranchingContext brctxt, Label lblBranch, Label lblOnFalse)
		{
			switch (brctxt)
			{
			case BranchingContext.None:
				this.helper.ConvBranchToBool(lblOnFalse, false);
				this.iterCurr.Storage = StorageDescriptor.Stack(typeof(bool), false);
				return;
			case BranchingContext.OnTrue:
				this.helper.MarkLabel(lblOnFalse);
				break;
			case BranchingContext.OnFalse:
				break;
			default:
				return;
			}
			this.iterCurr.Storage = StorageDescriptor.None();
		}

		// Token: 0x060042D7 RID: 17111 RVA: 0x0016A6F4 File Offset: 0x001688F4
		protected override QilNode VisitOr(QilBinary ndOr)
		{
			Label label = default(Label);
			BranchingContext branchingContext = this.iterCurr.CurrentBranchingContext;
			if (branchingContext != BranchingContext.OnTrue)
			{
				if (branchingContext == BranchingContext.OnFalse)
				{
					label = this.helper.DefineLabel();
					this.NestedVisitWithBranch(ndOr.Left, BranchingContext.OnTrue, label);
				}
				else
				{
					label = this.helper.DefineLabel();
					this.NestedVisitWithBranch(ndOr.Left, BranchingContext.OnTrue, label);
				}
			}
			else
			{
				this.NestedVisitWithBranch(ndOr.Left, BranchingContext.OnTrue, this.iterCurr.LabelBranch);
			}
			branchingContext = this.iterCurr.CurrentBranchingContext;
			if (branchingContext != BranchingContext.OnTrue)
			{
				if (branchingContext == BranchingContext.OnFalse)
				{
					this.NestedVisitWithBranch(ndOr.Right, BranchingContext.OnFalse, this.iterCurr.LabelBranch);
				}
				else
				{
					this.NestedVisitWithBranch(ndOr.Right, BranchingContext.OnTrue, label);
				}
			}
			else
			{
				this.NestedVisitWithBranch(ndOr.Right, BranchingContext.OnTrue, this.iterCurr.LabelBranch);
			}
			switch (this.iterCurr.CurrentBranchingContext)
			{
			case BranchingContext.None:
				this.helper.ConvBranchToBool(label, true);
				this.iterCurr.Storage = StorageDescriptor.Stack(typeof(bool), false);
				return ndOr;
			case BranchingContext.OnTrue:
				break;
			case BranchingContext.OnFalse:
				this.helper.MarkLabel(label);
				break;
			default:
				return ndOr;
			}
			this.iterCurr.Storage = StorageDescriptor.None();
			return ndOr;
		}

		// Token: 0x060042D8 RID: 17112 RVA: 0x0016A82C File Offset: 0x00168A2C
		protected override QilNode VisitNot(QilUnary ndNot)
		{
			Label label = default(Label);
			BranchingContext currentBranchingContext = this.iterCurr.CurrentBranchingContext;
			if (currentBranchingContext != BranchingContext.OnTrue)
			{
				if (currentBranchingContext == BranchingContext.OnFalse)
				{
					this.NestedVisitWithBranch(ndNot.Child, BranchingContext.OnTrue, this.iterCurr.LabelBranch);
				}
				else
				{
					label = this.helper.DefineLabel();
					this.NestedVisitWithBranch(ndNot.Child, BranchingContext.OnTrue, label);
				}
			}
			else
			{
				this.NestedVisitWithBranch(ndNot.Child, BranchingContext.OnFalse, this.iterCurr.LabelBranch);
			}
			if (this.iterCurr.CurrentBranchingContext == BranchingContext.None)
			{
				this.helper.ConvBranchToBool(label, false);
				this.iterCurr.Storage = StorageDescriptor.Stack(typeof(bool), false);
			}
			else
			{
				this.iterCurr.Storage = StorageDescriptor.None();
			}
			return ndNot;
		}

		// Token: 0x060042D9 RID: 17113 RVA: 0x0016A8EC File Offset: 0x00168AEC
		protected override QilNode VisitConditional(QilTernary ndCond)
		{
			if (XmlILConstructInfo.Read(ndCond).ConstructMethod == XmlILConstructMethod.Writer)
			{
				Label label = this.helper.DefineLabel();
				this.NestedVisitWithBranch(ndCond.Left, BranchingContext.OnFalse, label);
				this.NestedVisit(ndCond.Center);
				if (ndCond.Right.NodeType == QilNodeType.Sequence && ndCond.Right.Count == 0)
				{
					this.helper.MarkLabel(label);
					this.NestedVisit(ndCond.Right);
				}
				else
				{
					Label label2 = this.helper.DefineLabel();
					this.helper.EmitUnconditionalBranch(OpCodes.Br, label2);
					this.helper.MarkLabel(label);
					this.NestedVisit(ndCond.Right);
					this.helper.MarkLabel(label2);
				}
				this.iterCurr.Storage = StorageDescriptor.None();
			}
			else
			{
				LocalBuilder localBuilder = null;
				LocalBuilder localBuilder2 = null;
				Type itemStorageType = this.GetItemStorageType(ndCond);
				Label label3 = this.helper.DefineLabel();
				if (ndCond.XmlType.IsSingleton)
				{
					this.NestedVisitWithBranch(ndCond.Left, BranchingContext.OnFalse, label3);
				}
				else
				{
					localBuilder2 = this.helper.DeclareLocal("$$$cond", itemStorageType);
					localBuilder = this.helper.DeclareLocal("$$$boolResult", typeof(bool));
					this.NestedVisitEnsureLocal(ndCond.Left, localBuilder);
					this.helper.Emit(OpCodes.Ldloc, localBuilder);
					this.helper.Emit(OpCodes.Brfalse, label3);
				}
				this.ConditionalBranch(ndCond.Center, itemStorageType, localBuilder2);
				IteratorDescriptor iteratorDescriptor = this.iterNested;
				Label label4 = this.helper.DefineLabel();
				this.helper.EmitUnconditionalBranch(OpCodes.Br, label4);
				this.helper.MarkLabel(label3);
				this.ConditionalBranch(ndCond.Right, itemStorageType, localBuilder2);
				if (!ndCond.XmlType.IsSingleton)
				{
					this.helper.EmitUnconditionalBranch(OpCodes.Brtrue, label4);
					Label label5 = this.helper.DefineLabel();
					this.helper.MarkLabel(label5);
					this.helper.Emit(OpCodes.Ldloc, localBuilder);
					this.helper.Emit(OpCodes.Brtrue, iteratorDescriptor.GetLabelNext());
					this.helper.EmitUnconditionalBranch(OpCodes.Br, this.iterNested.GetLabelNext());
					this.iterCurr.SetIterator(label5, StorageDescriptor.Local(localBuilder2, itemStorageType, false));
				}
				this.helper.MarkLabel(label4);
			}
			return ndCond;
		}

		// Token: 0x060042DA RID: 17114 RVA: 0x0016AB4C File Offset: 0x00168D4C
		private void ConditionalBranch(QilNode ndBranch, Type itemStorageType, LocalBuilder locResult)
		{
			if (locResult != null)
			{
				this.NestedVisit(ndBranch, this.iterCurr.GetLabelNext());
				this.iterCurr.EnsureItemStorageType(ndBranch.XmlType, itemStorageType);
				this.iterCurr.EnsureLocalNoCache(locResult);
				return;
			}
			if (this.iterCurr.IsBranching)
			{
				this.NestedVisitWithBranch(ndBranch, this.iterCurr.CurrentBranchingContext, this.iterCurr.LabelBranch);
				return;
			}
			this.NestedVisitEnsureStack(ndBranch, itemStorageType, false);
		}

		// Token: 0x060042DB RID: 17115 RVA: 0x0016ABC4 File Offset: 0x00168DC4
		protected override QilNode VisitChoice(QilChoice ndChoice)
		{
			this.NestedVisit(ndChoice.Expression);
			QilNode branches = ndChoice.Branches;
			int num = branches.Count - 1;
			Label[] array = new Label[num];
			int i;
			for (i = 0; i < num; i++)
			{
				array[i] = this.helper.DefineLabel();
			}
			Label label = this.helper.DefineLabel();
			Label label2 = this.helper.DefineLabel();
			this.helper.Emit(OpCodes.Switch, array);
			this.helper.EmitUnconditionalBranch(OpCodes.Br, label);
			for (i = 0; i < num; i++)
			{
				this.helper.MarkLabel(array[i]);
				this.NestedVisit(branches[i]);
				this.helper.EmitUnconditionalBranch(OpCodes.Br, label2);
			}
			this.helper.MarkLabel(label);
			this.NestedVisit(branches[i]);
			this.helper.MarkLabel(label2);
			this.iterCurr.Storage = StorageDescriptor.None();
			return ndChoice;
		}

		// Token: 0x060042DC RID: 17116 RVA: 0x0016ACD0 File Offset: 0x00168ED0
		protected override QilNode VisitLength(QilUnary ndSetLen)
		{
			Label label = this.helper.DefineLabel();
			OptimizerPatterns optimizerPatterns = OptimizerPatterns.Read(ndSetLen);
			if (this.CachesResult(ndSetLen.Child))
			{
				this.NestedVisitEnsureStack(ndSetLen.Child);
				this.helper.CallCacheCount(this.iterNested.Storage.ItemStorageType);
			}
			else
			{
				this.helper.Emit(OpCodes.Ldc_I4_0);
				this.StartNestedIterator(ndSetLen.Child, label);
				this.Visit(ndSetLen.Child);
				this.iterCurr.EnsureNoCache();
				this.iterCurr.DiscardStack();
				this.helper.Emit(OpCodes.Ldc_I4_1);
				this.helper.Emit(OpCodes.Add);
				if (optimizerPatterns.MatchesPattern(OptimizerPatternName.MaxPosition))
				{
					this.helper.Emit(OpCodes.Dup);
					this.helper.LoadInteger((int)optimizerPatterns.GetArgument(OptimizerPatternArgument.ElementQName));
					this.helper.Emit(OpCodes.Bgt, label);
				}
				this.iterCurr.LoopToEnd(label);
				this.EndNestedIterator(ndSetLen.Child);
			}
			this.iterCurr.Storage = StorageDescriptor.Stack(typeof(int), false);
			return ndSetLen;
		}

		// Token: 0x060042DD RID: 17117 RVA: 0x0016AE04 File Offset: 0x00169004
		protected override QilNode VisitSequence(QilList ndSeq)
		{
			if (XmlILConstructInfo.Read(ndSeq).ConstructMethod == XmlILConstructMethod.Writer)
			{
				using (IEnumerator<QilNode> enumerator = ndSeq.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						QilNode qilNode = enumerator.Current;
						this.NestedVisit(qilNode);
					}
					return ndSeq;
				}
			}
			if (ndSeq.Count == 0)
			{
				this.VisitEmpty(ndSeq);
			}
			else
			{
				this.Sequence(ndSeq);
			}
			return ndSeq;
		}

		// Token: 0x060042DE RID: 17118 RVA: 0x0016AE74 File Offset: 0x00169074
		private void VisitEmpty(QilNode nd)
		{
			this.helper.EmitUnconditionalBranch(OpCodes.Brtrue, this.iterCurr.GetLabelNext());
			this.helper.Emit(OpCodes.Ldnull);
			this.iterCurr.Storage = StorageDescriptor.Stack(typeof(XPathItem), false);
		}

		// Token: 0x060042DF RID: 17119 RVA: 0x0016AEC8 File Offset: 0x001690C8
		private void Sequence(QilList ndSeq)
		{
			Label label = default(Label);
			Type itemStorageType = this.GetItemStorageType(ndSeq);
			if (ndSeq.XmlType.IsSingleton)
			{
				foreach (QilNode qilNode in ndSeq)
				{
					if (qilNode.XmlType.IsSingleton)
					{
						this.NestedVisitEnsureStack(qilNode);
					}
					else
					{
						label = this.helper.DefineLabel();
						this.NestedVisit(qilNode, label);
						this.iterCurr.DiscardStack();
						this.helper.MarkLabel(label);
					}
				}
				this.iterCurr.Storage = StorageDescriptor.Stack(itemStorageType, false);
				return;
			}
			LocalBuilder localBuilder = this.helper.DeclareLocal("$$$itemList", itemStorageType);
			LocalBuilder localBuilder2 = this.helper.DeclareLocal("$$$idxList", typeof(int));
			Label[] array = new Label[ndSeq.Count];
			Label label2 = this.helper.DefineLabel();
			for (int i = 0; i < ndSeq.Count; i++)
			{
				if (i != 0)
				{
					this.helper.MarkLabel(label);
				}
				if (i == ndSeq.Count - 1)
				{
					label = this.iterCurr.GetLabelNext();
				}
				else
				{
					label = this.helper.DefineLabel();
				}
				this.helper.LoadInteger(i);
				this.helper.Emit(OpCodes.Stloc, localBuilder2);
				this.NestedVisit(ndSeq[i], label);
				this.iterCurr.EnsureItemStorageType(ndSeq[i].XmlType, itemStorageType);
				this.iterCurr.EnsureLocalNoCache(localBuilder);
				array[i] = this.iterNested.GetLabelNext();
				this.helper.EmitUnconditionalBranch(OpCodes.Brtrue, label2);
			}
			Label label3 = this.helper.DefineLabel();
			this.helper.MarkLabel(label3);
			this.helper.Emit(OpCodes.Ldloc, localBuilder2);
			this.helper.Emit(OpCodes.Switch, array);
			this.helper.MarkLabel(label2);
			this.iterCurr.SetIterator(label3, StorageDescriptor.Local(localBuilder, itemStorageType, false));
		}

		// Token: 0x060042E0 RID: 17120 RVA: 0x0016B0FC File Offset: 0x001692FC
		protected override QilNode VisitUnion(QilBinary ndUnion)
		{
			return this.CreateSetIterator(ndUnion, "$$$iterUnion", typeof(UnionIterator), XmlILMethods.UnionCreate, XmlILMethods.UnionNext);
		}

		// Token: 0x060042E1 RID: 17121 RVA: 0x0016B11E File Offset: 0x0016931E
		protected override QilNode VisitIntersection(QilBinary ndInter)
		{
			return this.CreateSetIterator(ndInter, "$$$iterInter", typeof(IntersectIterator), XmlILMethods.InterCreate, XmlILMethods.InterNext);
		}

		// Token: 0x060042E2 RID: 17122 RVA: 0x0016B140 File Offset: 0x00169340
		protected override QilNode VisitDifference(QilBinary ndDiff)
		{
			return this.CreateSetIterator(ndDiff, "$$$iterDiff", typeof(DifferenceIterator), XmlILMethods.DiffCreate, XmlILMethods.DiffNext);
		}

		// Token: 0x060042E3 RID: 17123 RVA: 0x0016B164 File Offset: 0x00169364
		private QilNode CreateSetIterator(QilBinary ndSet, string iterName, Type iterType, MethodInfo methCreate, MethodInfo methNext)
		{
			LocalBuilder localBuilder = this.helper.DeclareLocal(iterName, iterType);
			LocalBuilder localBuilder2 = this.helper.DeclareLocal("$$$navSet", typeof(XPathNavigator));
			this.helper.Emit(OpCodes.Ldloca, localBuilder);
			this.helper.LoadQueryRuntime();
			this.helper.Call(methCreate);
			Label label = this.helper.DefineLabel();
			Label label2 = this.helper.DefineLabel();
			Label label3 = this.helper.DefineLabel();
			this.NestedVisit(ndSet.Left, label);
			Label labelNext = this.iterNested.GetLabelNext();
			this.iterCurr.EnsureLocal(localBuilder2);
			this.helper.EmitUnconditionalBranch(OpCodes.Brtrue, label2);
			this.helper.MarkLabel(label3);
			this.NestedVisit(ndSet.Right, label);
			Label labelNext2 = this.iterNested.GetLabelNext();
			this.iterCurr.EnsureLocal(localBuilder2);
			this.helper.EmitUnconditionalBranch(OpCodes.Brtrue, label2);
			this.helper.MarkLabel(label);
			this.helper.Emit(OpCodes.Ldnull);
			this.helper.Emit(OpCodes.Stloc, localBuilder2);
			this.helper.MarkLabel(label2);
			this.helper.Emit(OpCodes.Ldloca, localBuilder);
			this.helper.Emit(OpCodes.Ldloc, localBuilder2);
			this.helper.Call(methNext);
			if (ndSet.XmlType.IsSingleton)
			{
				this.helper.Emit(OpCodes.Switch, new Label[] { label3, labelNext, labelNext2 });
				this.iterCurr.Storage = StorageDescriptor.Current(localBuilder, typeof(XPathNavigator));
			}
			else
			{
				this.helper.Emit(OpCodes.Switch, new Label[]
				{
					this.iterCurr.GetLabelNext(),
					label3,
					labelNext,
					labelNext2
				});
				this.iterCurr.SetIterator(label, StorageDescriptor.Current(localBuilder, typeof(XPathNavigator)));
			}
			return ndSet;
		}

		// Token: 0x060042E4 RID: 17124 RVA: 0x0016B388 File Offset: 0x00169588
		protected override QilNode VisitAverage(QilUnary ndAvg)
		{
			XmlILStorageMethods xmlILStorageMethods = XmlILMethods.StorageMethods[this.GetItemStorageType(ndAvg)];
			return this.CreateAggregator(ndAvg, "$$$aggAvg", xmlILStorageMethods, xmlILStorageMethods.AggAvg, xmlILStorageMethods.AggAvgResult);
		}

		// Token: 0x060042E5 RID: 17125 RVA: 0x0016B3C0 File Offset: 0x001695C0
		protected override QilNode VisitSum(QilUnary ndSum)
		{
			XmlILStorageMethods xmlILStorageMethods = XmlILMethods.StorageMethods[this.GetItemStorageType(ndSum)];
			return this.CreateAggregator(ndSum, "$$$aggSum", xmlILStorageMethods, xmlILStorageMethods.AggSum, xmlILStorageMethods.AggSumResult);
		}

		// Token: 0x060042E6 RID: 17126 RVA: 0x0016B3F8 File Offset: 0x001695F8
		protected override QilNode VisitMinimum(QilUnary ndMin)
		{
			XmlILStorageMethods xmlILStorageMethods = XmlILMethods.StorageMethods[this.GetItemStorageType(ndMin)];
			return this.CreateAggregator(ndMin, "$$$aggMin", xmlILStorageMethods, xmlILStorageMethods.AggMin, xmlILStorageMethods.AggMinResult);
		}

		// Token: 0x060042E7 RID: 17127 RVA: 0x0016B430 File Offset: 0x00169630
		protected override QilNode VisitMaximum(QilUnary ndMax)
		{
			XmlILStorageMethods xmlILStorageMethods = XmlILMethods.StorageMethods[this.GetItemStorageType(ndMax)];
			return this.CreateAggregator(ndMax, "$$$aggMax", xmlILStorageMethods, xmlILStorageMethods.AggMax, xmlILStorageMethods.AggMaxResult);
		}

		// Token: 0x060042E8 RID: 17128 RVA: 0x0016B468 File Offset: 0x00169668
		private QilNode CreateAggregator(QilUnary ndAgg, string aggName, XmlILStorageMethods methods, MethodInfo methAgg, MethodInfo methResult)
		{
			Label label = this.helper.DefineLabel();
			Type declaringType = methAgg.DeclaringType;
			LocalBuilder localBuilder = this.helper.DeclareLocal(aggName, declaringType);
			this.helper.Emit(OpCodes.Ldloca, localBuilder);
			this.helper.Call(methods.AggCreate);
			this.StartNestedIterator(ndAgg.Child, label);
			this.helper.Emit(OpCodes.Ldloca, localBuilder);
			this.Visit(ndAgg.Child);
			this.iterCurr.EnsureStackNoCache();
			this.iterCurr.EnsureItemStorageType(ndAgg.XmlType, this.GetItemStorageType(ndAgg));
			this.helper.Call(methAgg);
			this.helper.Emit(OpCodes.Ldloca, localBuilder);
			this.iterCurr.LoopToEnd(label);
			this.EndNestedIterator(ndAgg.Child);
			if (ndAgg.XmlType.MaybeEmpty)
			{
				this.helper.Call(methods.AggIsEmpty);
				this.helper.Emit(OpCodes.Brtrue, this.iterCurr.GetLabelNext());
				this.helper.Emit(OpCodes.Ldloca, localBuilder);
			}
			this.helper.Call(methResult);
			this.iterCurr.Storage = StorageDescriptor.Stack(this.GetItemStorageType(ndAgg), false);
			return ndAgg;
		}

		// Token: 0x060042E9 RID: 17129 RVA: 0x0016B5AD File Offset: 0x001697AD
		protected override QilNode VisitNegate(QilUnary ndNeg)
		{
			this.NestedVisitEnsureStack(ndNeg.Child);
			this.helper.CallArithmeticOp(QilNodeType.Negate, ndNeg.XmlType.TypeCode);
			this.iterCurr.Storage = StorageDescriptor.Stack(this.GetItemStorageType(ndNeg), false);
			return ndNeg;
		}

		// Token: 0x060042EA RID: 17130 RVA: 0x0016B5EC File Offset: 0x001697EC
		protected override QilNode VisitAdd(QilBinary ndPlus)
		{
			return this.ArithmeticOp(ndPlus);
		}

		// Token: 0x060042EB RID: 17131 RVA: 0x0016B5EC File Offset: 0x001697EC
		protected override QilNode VisitSubtract(QilBinary ndMinus)
		{
			return this.ArithmeticOp(ndMinus);
		}

		// Token: 0x060042EC RID: 17132 RVA: 0x0016B5EC File Offset: 0x001697EC
		protected override QilNode VisitMultiply(QilBinary ndMul)
		{
			return this.ArithmeticOp(ndMul);
		}

		// Token: 0x060042ED RID: 17133 RVA: 0x0016B5EC File Offset: 0x001697EC
		protected override QilNode VisitDivide(QilBinary ndDiv)
		{
			return this.ArithmeticOp(ndDiv);
		}

		// Token: 0x060042EE RID: 17134 RVA: 0x0016B5EC File Offset: 0x001697EC
		protected override QilNode VisitModulo(QilBinary ndMod)
		{
			return this.ArithmeticOp(ndMod);
		}

		// Token: 0x060042EF RID: 17135 RVA: 0x0016B5F8 File Offset: 0x001697F8
		private QilNode ArithmeticOp(QilBinary ndOp)
		{
			this.NestedVisitEnsureStack(ndOp.Left, ndOp.Right);
			this.helper.CallArithmeticOp(ndOp.NodeType, ndOp.XmlType.TypeCode);
			this.iterCurr.Storage = StorageDescriptor.Stack(this.GetItemStorageType(ndOp), false);
			return ndOp;
		}

		// Token: 0x060042F0 RID: 17136 RVA: 0x0016B64C File Offset: 0x0016984C
		protected override QilNode VisitStrLength(QilUnary ndLen)
		{
			this.NestedVisitEnsureStack(ndLen.Child);
			this.helper.Call(XmlILMethods.StrLen);
			this.iterCurr.Storage = StorageDescriptor.Stack(typeof(int), false);
			return ndLen;
		}

		// Token: 0x060042F1 RID: 17137 RVA: 0x0016B688 File Offset: 0x00169888
		protected override QilNode VisitStrConcat(QilStrConcat ndStrConcat)
		{
			QilNode qilNode = ndStrConcat.Delimiter;
			if (qilNode.NodeType == QilNodeType.LiteralString && ((QilLiteral)qilNode).Length == 0)
			{
				qilNode = null;
			}
			QilNode values = ndStrConcat.Values;
			bool flag;
			if (values.NodeType == QilNodeType.Sequence && values.Count < 5)
			{
				flag = true;
				using (IEnumerator<QilNode> enumerator = values.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						if (!enumerator.Current.XmlType.IsSingleton)
						{
							flag = false;
						}
					}
					goto IL_0079;
				}
			}
			flag = false;
			IL_0079:
			if (flag)
			{
				foreach (QilNode qilNode2 in values)
				{
					this.NestedVisitEnsureStack(qilNode2);
				}
				this.helper.CallConcatStrings(values.Count);
			}
			else
			{
				LocalBuilder localBuilder = this.helper.DeclareLocal("$$$strcat", typeof(StringConcat));
				this.helper.Emit(OpCodes.Ldloca, localBuilder);
				this.helper.Call(XmlILMethods.StrCatClear);
				if (qilNode != null)
				{
					this.helper.Emit(OpCodes.Ldloca, localBuilder);
					this.NestedVisitEnsureStack(qilNode);
					this.helper.Call(XmlILMethods.StrCatDelim);
				}
				this.helper.Emit(OpCodes.Ldloca, localBuilder);
				if (values.NodeType == QilNodeType.Sequence)
				{
					using (IEnumerator<QilNode> enumerator = values.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							QilNode qilNode3 = enumerator.Current;
							this.GenerateConcat(qilNode3, localBuilder);
						}
						goto IL_0181;
					}
				}
				this.GenerateConcat(values, localBuilder);
				IL_0181:
				this.helper.Call(XmlILMethods.StrCatResult);
			}
			this.iterCurr.Storage = StorageDescriptor.Stack(typeof(string), false);
			return ndStrConcat;
		}

		// Token: 0x060042F2 RID: 17138 RVA: 0x0016B86C File Offset: 0x00169A6C
		private void GenerateConcat(QilNode ndStr, LocalBuilder locStringConcat)
		{
			Label label = this.helper.DefineLabel();
			this.StartNestedIterator(ndStr, label);
			this.Visit(ndStr);
			this.iterCurr.EnsureStackNoCache();
			this.iterCurr.EnsureItemStorageType(ndStr.XmlType, typeof(string));
			this.helper.Call(XmlILMethods.StrCatCat);
			this.helper.Emit(OpCodes.Ldloca, locStringConcat);
			this.iterCurr.LoopToEnd(label);
			this.EndNestedIterator(ndStr);
		}

		// Token: 0x060042F3 RID: 17139 RVA: 0x0016B8EF File Offset: 0x00169AEF
		protected override QilNode VisitStrParseQName(QilBinary ndParsedTagName)
		{
			this.VisitStrParseQName(ndParsedTagName, false);
			return ndParsedTagName;
		}

		// Token: 0x060042F4 RID: 17140 RVA: 0x0016B8FC File Offset: 0x00169AFC
		private void VisitStrParseQName(QilBinary ndParsedTagName, bool preservePrefix)
		{
			if (!preservePrefix)
			{
				this.helper.LoadQueryRuntime();
			}
			this.NestedVisitEnsureStack(ndParsedTagName.Left);
			if (ndParsedTagName.Right.XmlType.TypeCode == XmlTypeCode.String)
			{
				this.NestedVisitEnsureStack(ndParsedTagName.Right);
				if (!preservePrefix)
				{
					this.helper.CallParseTagName(GenerateNameType.TagNameAndNamespace);
				}
			}
			else
			{
				if (ndParsedTagName.Right.NodeType == QilNodeType.Sequence)
				{
					this.helper.LoadInteger(this.helper.StaticData.DeclarePrefixMappings(ndParsedTagName.Right));
				}
				else
				{
					this.helper.LoadInteger(this.helper.StaticData.DeclarePrefixMappings(new QilNode[] { ndParsedTagName.Right }));
				}
				if (!preservePrefix)
				{
					this.helper.CallParseTagName(GenerateNameType.TagNameAndMappings);
				}
			}
			this.iterCurr.Storage = StorageDescriptor.Stack(typeof(XmlQualifiedName), false);
		}

		// Token: 0x060042F5 RID: 17141 RVA: 0x0016B9DA File Offset: 0x00169BDA
		protected override QilNode VisitNe(QilBinary ndNe)
		{
			this.Compare(ndNe);
			return ndNe;
		}

		// Token: 0x060042F6 RID: 17142 RVA: 0x0016B9DA File Offset: 0x00169BDA
		protected override QilNode VisitEq(QilBinary ndEq)
		{
			this.Compare(ndEq);
			return ndEq;
		}

		// Token: 0x060042F7 RID: 17143 RVA: 0x0016B9DA File Offset: 0x00169BDA
		protected override QilNode VisitGt(QilBinary ndGt)
		{
			this.Compare(ndGt);
			return ndGt;
		}

		// Token: 0x060042F8 RID: 17144 RVA: 0x0016B9DA File Offset: 0x00169BDA
		protected override QilNode VisitGe(QilBinary ndGe)
		{
			this.Compare(ndGe);
			return ndGe;
		}

		// Token: 0x060042F9 RID: 17145 RVA: 0x0016B9DA File Offset: 0x00169BDA
		protected override QilNode VisitLt(QilBinary ndLt)
		{
			this.Compare(ndLt);
			return ndLt;
		}

		// Token: 0x060042FA RID: 17146 RVA: 0x0016B9DA File Offset: 0x00169BDA
		protected override QilNode VisitLe(QilBinary ndLe)
		{
			this.Compare(ndLe);
			return ndLe;
		}

		// Token: 0x060042FB RID: 17147 RVA: 0x0016B9E4 File Offset: 0x00169BE4
		private void Compare(QilBinary ndComp)
		{
			QilNodeType nodeType = ndComp.NodeType;
			if (nodeType == QilNodeType.Eq || nodeType == QilNodeType.Ne)
			{
				if (this.TryZeroCompare(nodeType, ndComp.Left, ndComp.Right))
				{
					return;
				}
				if (this.TryZeroCompare(nodeType, ndComp.Right, ndComp.Left))
				{
					return;
				}
				if (this.TryNameCompare(nodeType, ndComp.Left, ndComp.Right))
				{
					return;
				}
				if (this.TryNameCompare(nodeType, ndComp.Right, ndComp.Left))
				{
					return;
				}
			}
			this.NestedVisitEnsureStack(ndComp.Left, ndComp.Right);
			XmlTypeCode typeCode = ndComp.Left.XmlType.TypeCode;
			if (typeCode <= XmlTypeCode.QName)
			{
				switch (typeCode)
				{
				case XmlTypeCode.String:
				case XmlTypeCode.Decimal:
					break;
				case XmlTypeCode.Boolean:
				case XmlTypeCode.Double:
					goto IL_010D;
				case XmlTypeCode.Float:
					return;
				default:
					if (typeCode != XmlTypeCode.QName)
					{
						return;
					}
					break;
				}
				if (nodeType == QilNodeType.Eq || nodeType == QilNodeType.Ne)
				{
					this.helper.CallCompareEquals(typeCode);
					this.ZeroCompare((nodeType == QilNodeType.Eq) ? QilNodeType.Ne : QilNodeType.Eq, true);
					return;
				}
				this.helper.CallCompare(typeCode);
				this.helper.Emit(OpCodes.Ldc_I4_0);
				this.ClrCompare(nodeType, typeCode);
				return;
			}
			else if (typeCode != XmlTypeCode.Integer && typeCode != XmlTypeCode.Int)
			{
				return;
			}
			IL_010D:
			this.ClrCompare(nodeType, typeCode);
		}

		// Token: 0x060042FC RID: 17148 RVA: 0x0016BB06 File Offset: 0x00169D06
		protected override QilNode VisitIs(QilBinary ndIs)
		{
			this.NestedVisitEnsureStack(ndIs.Left, ndIs.Right);
			this.helper.Call(XmlILMethods.NavSamePos);
			this.ZeroCompare(QilNodeType.Ne, true);
			return ndIs;
		}

		// Token: 0x060042FD RID: 17149 RVA: 0x0016BB34 File Offset: 0x00169D34
		protected override QilNode VisitBefore(QilBinary ndBefore)
		{
			this.ComparePosition(ndBefore);
			return ndBefore;
		}

		// Token: 0x060042FE RID: 17150 RVA: 0x0016BB34 File Offset: 0x00169D34
		protected override QilNode VisitAfter(QilBinary ndAfter)
		{
			this.ComparePosition(ndAfter);
			return ndAfter;
		}

		// Token: 0x060042FF RID: 17151 RVA: 0x0016BB40 File Offset: 0x00169D40
		private void ComparePosition(QilBinary ndComp)
		{
			this.helper.LoadQueryRuntime();
			this.NestedVisitEnsureStack(ndComp.Left, ndComp.Right);
			this.helper.Call(XmlILMethods.CompPos);
			this.helper.LoadInteger(0);
			this.ClrCompare((ndComp.NodeType == QilNodeType.Before) ? QilNodeType.Lt : QilNodeType.Gt, XmlTypeCode.String);
		}

		// Token: 0x06004300 RID: 17152 RVA: 0x0016BBA0 File Offset: 0x00169DA0
		protected override QilNode VisitFor(QilIterator ndFor)
		{
			IteratorDescriptor cachedIteratorDescriptor = XmlILAnnotation.Write(ndFor).CachedIteratorDescriptor;
			this.iterCurr.Storage = cachedIteratorDescriptor.Storage;
			if (this.iterCurr.Storage.Location == ItemLocation.Global)
			{
				this.iterCurr.EnsureStack();
			}
			return ndFor;
		}

		// Token: 0x06004301 RID: 17153 RVA: 0x0016BBEC File Offset: 0x00169DEC
		protected override QilNode VisitLet(QilIterator ndLet)
		{
			return this.VisitFor(ndLet);
		}

		// Token: 0x06004302 RID: 17154 RVA: 0x0016BBEC File Offset: 0x00169DEC
		protected override QilNode VisitParameter(QilParameter ndParameter)
		{
			return this.VisitFor(ndParameter);
		}

		// Token: 0x06004303 RID: 17155 RVA: 0x0016BBF8 File Offset: 0x00169DF8
		protected override QilNode VisitLoop(QilLoop ndLoop)
		{
			bool flag;
			Label label;
			this.StartWriterLoop(ndLoop, out flag, out label);
			this.StartBinding(ndLoop.Variable);
			this.Visit(ndLoop.Body);
			this.EndBinding(ndLoop.Variable);
			this.EndWriterLoop(ndLoop, flag, label);
			return ndLoop;
		}

		// Token: 0x06004304 RID: 17156 RVA: 0x0016BC40 File Offset: 0x00169E40
		protected override QilNode VisitFilter(QilLoop ndFilter)
		{
			if (this.HandleFilterPatterns(ndFilter))
			{
				return ndFilter;
			}
			this.StartBinding(ndFilter.Variable);
			this.iterCurr.SetIterator(this.iterNested);
			this.StartNestedIterator(ndFilter.Body);
			this.iterCurr.SetBranching(BranchingContext.OnFalse, this.iterCurr.ParentIterator.GetLabelNext());
			this.Visit(ndFilter.Body);
			this.EndNestedIterator(ndFilter.Body);
			this.EndBinding(ndFilter.Variable);
			return ndFilter;
		}

		// Token: 0x06004305 RID: 17157 RVA: 0x0016BCC4 File Offset: 0x00169EC4
		private bool HandleFilterPatterns(QilLoop ndFilter)
		{
			OptimizerPatterns optimizerPatterns = OptimizerPatterns.Read(ndFilter);
			bool flag = optimizerPatterns.MatchesPattern(OptimizerPatternName.FilterElements);
			if (flag || optimizerPatterns.MatchesPattern(OptimizerPatternName.FilterContentKind))
			{
				XmlNodeKindFlags xmlNodeKindFlags;
				QilName qilName;
				if (flag)
				{
					xmlNodeKindFlags = XmlNodeKindFlags.Element;
					qilName = (QilName)optimizerPatterns.GetArgument(OptimizerPatternArgument.ElementQName);
				}
				else
				{
					xmlNodeKindFlags = ((XmlQueryType)optimizerPatterns.GetArgument(OptimizerPatternArgument.ElementQName)).NodeKinds;
					qilName = null;
				}
				QilNode qilNode = (QilNode)optimizerPatterns.GetArgument(OptimizerPatternArgument.StepNode);
				QilNode qilNode2 = (QilNode)optimizerPatterns.GetArgument(OptimizerPatternArgument.StepInput);
				QilNodeType nodeType = qilNode.NodeType;
				switch (nodeType)
				{
				case QilNodeType.Content:
					if (flag)
					{
						LocalBuilder localBuilder = this.helper.DeclareLocal("$$$iterElemContent", typeof(ElementContentIterator));
						this.helper.Emit(OpCodes.Ldloca, localBuilder);
						this.NestedVisitEnsureStack(qilNode2);
						this.helper.CallGetAtomizedName(this.helper.StaticData.DeclareName(qilName.LocalName));
						this.helper.CallGetAtomizedName(this.helper.StaticData.DeclareName(qilName.NamespaceUri));
						this.helper.Call(XmlILMethods.ElemContentCreate);
						this.GenerateSimpleIterator(typeof(XPathNavigator), localBuilder, XmlILMethods.ElemContentNext);
					}
					else if (xmlNodeKindFlags == XmlNodeKindFlags.Content)
					{
						this.CreateSimpleIterator(qilNode2, "$$$iterContent", typeof(ContentIterator), XmlILMethods.ContentCreate, XmlILMethods.ContentNext);
					}
					else
					{
						LocalBuilder localBuilder = this.helper.DeclareLocal("$$$iterContent", typeof(NodeKindContentIterator));
						this.helper.Emit(OpCodes.Ldloca, localBuilder);
						this.NestedVisitEnsureStack(qilNode2);
						this.helper.LoadInteger((int)this.QilXmlToXPathNodeType(xmlNodeKindFlags));
						this.helper.Call(XmlILMethods.KindContentCreate);
						this.GenerateSimpleIterator(typeof(XPathNavigator), localBuilder, XmlILMethods.KindContentNext);
					}
					return true;
				case QilNodeType.Attribute:
				case QilNodeType.Root:
				case QilNodeType.XmlContext:
					break;
				case QilNodeType.Parent:
					this.CreateFilteredIterator(qilNode2, "$$$iterPar", typeof(ParentIterator), XmlILMethods.ParentCreate, XmlILMethods.ParentNext, xmlNodeKindFlags, qilName, TriState.Unknown, null);
					return true;
				case QilNodeType.Descendant:
				case QilNodeType.DescendantOrSelf:
					this.CreateFilteredIterator(qilNode2, "$$$iterDesc", typeof(DescendantIterator), XmlILMethods.DescCreate, XmlILMethods.DescNext, xmlNodeKindFlags, qilName, (qilNode.NodeType == QilNodeType.Descendant) ? TriState.False : TriState.True, null);
					return true;
				case QilNodeType.Ancestor:
				case QilNodeType.AncestorOrSelf:
					this.CreateFilteredIterator(qilNode2, "$$$iterAnc", typeof(AncestorIterator), XmlILMethods.AncCreate, XmlILMethods.AncNext, xmlNodeKindFlags, qilName, (qilNode.NodeType == QilNodeType.Ancestor) ? TriState.False : TriState.True, null);
					return true;
				case QilNodeType.Preceding:
					this.CreateFilteredIterator(qilNode2, "$$$iterPrec", typeof(PrecedingIterator), XmlILMethods.PrecCreate, XmlILMethods.PrecNext, xmlNodeKindFlags, qilName, TriState.Unknown, null);
					return true;
				case QilNodeType.FollowingSibling:
					this.CreateFilteredIterator(qilNode2, "$$$iterFollSib", typeof(FollowingSiblingIterator), XmlILMethods.FollSibCreate, XmlILMethods.FollSibNext, xmlNodeKindFlags, qilName, TriState.Unknown, null);
					return true;
				case QilNodeType.PrecedingSibling:
					this.CreateFilteredIterator(qilNode2, "$$$iterPreSib", typeof(PrecedingSiblingIterator), XmlILMethods.PreSibCreate, XmlILMethods.PreSibNext, xmlNodeKindFlags, qilName, TriState.Unknown, null);
					return true;
				case QilNodeType.NodeRange:
					this.CreateFilteredIterator(qilNode2, "$$$iterRange", typeof(NodeRangeIterator), XmlILMethods.NodeRangeCreate, XmlILMethods.NodeRangeNext, xmlNodeKindFlags, qilName, TriState.Unknown, ((QilBinary)qilNode).Right);
					return true;
				default:
					if (nodeType == QilNodeType.XPathFollowing)
					{
						this.CreateFilteredIterator(qilNode2, "$$$iterFoll", typeof(XPathFollowingIterator), XmlILMethods.XPFollCreate, XmlILMethods.XPFollNext, xmlNodeKindFlags, qilName, TriState.Unknown, null);
						return true;
					}
					if (nodeType == QilNodeType.XPathPreceding)
					{
						this.CreateFilteredIterator(qilNode2, "$$$iterPrec", typeof(XPathPrecedingIterator), XmlILMethods.XPPrecCreate, XmlILMethods.XPPrecNext, xmlNodeKindFlags, qilName, TriState.Unknown, null);
						return true;
					}
					break;
				}
			}
			else
			{
				if (optimizerPatterns.MatchesPattern(OptimizerPatternName.FilterAttributeKind))
				{
					QilNode qilNode2 = (QilNode)optimizerPatterns.GetArgument(OptimizerPatternArgument.StepInput);
					this.CreateSimpleIterator(qilNode2, "$$$iterAttr", typeof(AttributeIterator), XmlILMethods.AttrCreate, XmlILMethods.AttrNext);
					return true;
				}
				if (optimizerPatterns.MatchesPattern(OptimizerPatternName.EqualityIndex))
				{
					Label label = this.helper.DefineLabel();
					Label label2 = this.helper.DefineLabel();
					QilIterator qilIterator = (QilIterator)optimizerPatterns.GetArgument(OptimizerPatternArgument.StepNode);
					QilNode qilNode3 = (QilNode)optimizerPatterns.GetArgument(OptimizerPatternArgument.StepInput);
					LocalBuilder localBuilder2 = this.helper.DeclareLocal("$$$index", typeof(XmlILIndex));
					this.helper.LoadQueryRuntime();
					this.helper.Emit(OpCodes.Ldarg_1);
					this.helper.LoadInteger(this.indexId);
					this.helper.Emit(OpCodes.Ldloca, localBuilder2);
					this.helper.Call(XmlILMethods.FindIndex);
					this.helper.Emit(OpCodes.Brtrue, label2);
					this.helper.LoadQueryRuntime();
					this.helper.Emit(OpCodes.Ldarg_1);
					this.helper.LoadInteger(this.indexId);
					this.helper.Emit(OpCodes.Ldloc, localBuilder2);
					this.StartNestedIterator(qilIterator, label);
					this.StartBinding(qilIterator);
					this.Visit(qilNode3);
					this.iterCurr.EnsureStackNoCache();
					this.VisitFor(qilIterator);
					this.iterCurr.EnsureStackNoCache();
					this.iterCurr.EnsureItemStorageType(qilIterator.XmlType, typeof(XPathNavigator));
					this.helper.Call(XmlILMethods.IndexAdd);
					this.helper.Emit(OpCodes.Ldloc, localBuilder2);
					this.iterCurr.LoopToEnd(label);
					this.EndBinding(qilIterator);
					this.EndNestedIterator(qilIterator);
					this.helper.Call(XmlILMethods.AddNewIndex);
					this.helper.MarkLabel(label2);
					this.helper.Emit(OpCodes.Ldloc, localBuilder2);
					this.helper.Emit(OpCodes.Ldarg_2);
					this.helper.Call(XmlILMethods.IndexLookup);
					this.iterCurr.Storage = StorageDescriptor.Stack(typeof(XPathNavigator), true);
					this.indexId++;
					return true;
				}
			}
			return false;
		}

		// Token: 0x06004306 RID: 17158 RVA: 0x0016C2A0 File Offset: 0x0016A4A0
		private void StartBinding(QilIterator ndIter)
		{
			OptimizerPatterns optimizerPatterns = OptimizerPatterns.Read(ndIter);
			if (this.qil.IsDebug && ndIter.SourceLine != null)
			{
				this.helper.DebugSequencePoint(ndIter.SourceLine);
			}
			if (ndIter.NodeType == QilNodeType.For || ndIter.XmlType.IsSingleton)
			{
				this.StartForBinding(ndIter, optimizerPatterns);
			}
			else
			{
				this.StartLetBinding(ndIter);
			}
			XmlILAnnotation.Write(ndIter).CachedIteratorDescriptor = this.iterNested;
		}

		// Token: 0x06004307 RID: 17159 RVA: 0x0016C314 File Offset: 0x0016A514
		private void StartForBinding(QilIterator ndFor, OptimizerPatterns patt)
		{
			LocalBuilder localBuilder = null;
			if (this.iterCurr.HasLabelNext)
			{
				this.StartNestedIterator(ndFor.Binding, this.iterCurr.GetLabelNext());
			}
			else
			{
				this.StartNestedIterator(ndFor.Binding);
			}
			if (patt.MatchesPattern(OptimizerPatternName.IsPositional))
			{
				localBuilder = this.helper.DeclareLocal("$$$pos", typeof(int));
				this.helper.Emit(OpCodes.Ldc_I4_0);
				this.helper.Emit(OpCodes.Stloc, localBuilder);
			}
			this.Visit(ndFor.Binding);
			if (this.qil.IsDebug && ndFor.DebugName != null)
			{
				this.helper.DebugStartScope();
				this.iterCurr.EnsureLocalNoCache("$$$for");
				this.iterCurr.Storage.LocalLocation.SetLocalSymInfo(ndFor.DebugName);
			}
			else
			{
				this.iterCurr.EnsureNoStackNoCache("$$$for");
			}
			if (patt.MatchesPattern(OptimizerPatternName.IsPositional))
			{
				this.helper.Emit(OpCodes.Ldloc, localBuilder);
				this.helper.Emit(OpCodes.Ldc_I4_1);
				this.helper.Emit(OpCodes.Add);
				this.helper.Emit(OpCodes.Stloc, localBuilder);
				if (patt.MatchesPattern(OptimizerPatternName.MaxPosition))
				{
					this.helper.Emit(OpCodes.Ldloc, localBuilder);
					this.helper.LoadInteger((int)patt.GetArgument(OptimizerPatternArgument.ElementQName));
					this.helper.Emit(OpCodes.Bgt, this.iterCurr.ParentIterator.GetLabelNext());
				}
				this.iterCurr.LocalPosition = localBuilder;
			}
			this.EndNestedIterator(ndFor.Binding);
			this.iterCurr.SetIterator(this.iterNested);
		}

		// Token: 0x06004308 RID: 17160 RVA: 0x0016C4D0 File Offset: 0x0016A6D0
		public void StartLetBinding(QilIterator ndLet)
		{
			this.StartNestedIterator(ndLet);
			this.NestedVisit(ndLet.Binding, this.GetItemStorageType(ndLet), !ndLet.XmlType.IsSingleton);
			if (this.qil.IsDebug && ndLet.DebugName != null)
			{
				this.helper.DebugStartScope();
				this.iterCurr.EnsureLocal("$$$cache");
				this.iterCurr.Storage.LocalLocation.SetLocalSymInfo(ndLet.DebugName);
			}
			else
			{
				this.iterCurr.EnsureNoStack("$$$cache");
			}
			this.EndNestedIterator(ndLet);
		}

		// Token: 0x06004309 RID: 17161 RVA: 0x0016C56C File Offset: 0x0016A76C
		private void EndBinding(QilIterator ndIter)
		{
			if (this.qil.IsDebug && ndIter.DebugName != null)
			{
				this.helper.DebugEndScope();
			}
		}

		// Token: 0x0600430A RID: 17162 RVA: 0x0016C590 File Offset: 0x0016A790
		protected override QilNode VisitPositionOf(QilUnary ndPos)
		{
			LocalBuilder localPosition = XmlILAnnotation.Write(ndPos.Child as QilIterator).CachedIteratorDescriptor.LocalPosition;
			this.iterCurr.Storage = StorageDescriptor.Local(localPosition, typeof(int), false);
			return ndPos;
		}

		// Token: 0x0600430B RID: 17163 RVA: 0x0016C5D8 File Offset: 0x0016A7D8
		protected override QilNode VisitSort(QilLoop ndSort)
		{
			Type itemStorageType = this.GetItemStorageType(ndSort);
			Label label = this.helper.DefineLabel();
			XmlILStorageMethods xmlILStorageMethods = XmlILMethods.StorageMethods[itemStorageType];
			LocalBuilder localBuilder = this.helper.DeclareLocal("$$$cache", xmlILStorageMethods.SeqType);
			this.helper.Emit(OpCodes.Ldloc, localBuilder);
			this.helper.CallToken(xmlILStorageMethods.SeqReuse);
			this.helper.Emit(OpCodes.Stloc, localBuilder);
			this.helper.Emit(OpCodes.Ldloc, localBuilder);
			LocalBuilder localBuilder2 = this.helper.DeclareLocal("$$$keys", typeof(XmlSortKeyAccumulator));
			this.helper.Emit(OpCodes.Ldloca, localBuilder2);
			this.helper.Call(XmlILMethods.SortKeyCreate);
			this.StartNestedIterator(ndSort.Variable, label);
			this.StartBinding(ndSort.Variable);
			this.iterCurr.EnsureStackNoCache();
			this.iterCurr.EnsureItemStorageType(ndSort.Variable.XmlType, this.GetItemStorageType(ndSort.Variable));
			this.helper.Call(xmlILStorageMethods.SeqAdd);
			this.helper.Emit(OpCodes.Ldloca, localBuilder2);
			foreach (QilNode qilNode in ndSort.Body)
			{
				QilSortKey qilSortKey = (QilSortKey)qilNode;
				this.VisitSortKey(qilSortKey, localBuilder2);
			}
			this.helper.Call(XmlILMethods.SortKeyFinish);
			this.helper.Emit(OpCodes.Ldloc, localBuilder);
			this.iterCurr.LoopToEnd(label);
			this.helper.Emit(OpCodes.Pop);
			this.helper.Emit(OpCodes.Ldloc, localBuilder);
			this.helper.Emit(OpCodes.Ldloca, localBuilder2);
			this.helper.Call(XmlILMethods.SortKeyKeys);
			this.helper.Call(xmlILStorageMethods.SeqSortByKeys);
			this.iterCurr.Storage = StorageDescriptor.Local(localBuilder, itemStorageType, true);
			this.EndBinding(ndSort.Variable);
			this.EndNestedIterator(ndSort.Variable);
			this.iterCurr.SetIterator(this.iterNested);
			return ndSort;
		}

		// Token: 0x0600430C RID: 17164 RVA: 0x0016C814 File Offset: 0x0016AA14
		private void VisitSortKey(QilSortKey ndKey, LocalBuilder locKeys)
		{
			this.helper.Emit(OpCodes.Ldloca, locKeys);
			if (ndKey.Collation.NodeType == QilNodeType.LiteralString)
			{
				this.helper.CallGetCollation(this.helper.StaticData.DeclareCollation((QilLiteral)ndKey.Collation));
			}
			else
			{
				this.helper.LoadQueryRuntime();
				this.NestedVisitEnsureStack(ndKey.Collation);
				this.helper.Call(XmlILMethods.CreateCollation);
			}
			if (ndKey.XmlType.IsSingleton)
			{
				this.NestedVisitEnsureStack(ndKey.Key);
				this.helper.AddSortKey(ndKey.Key.XmlType);
				return;
			}
			Label label = this.helper.DefineLabel();
			this.StartNestedIterator(ndKey.Key, label);
			this.Visit(ndKey.Key);
			this.iterCurr.EnsureStackNoCache();
			this.iterCurr.EnsureItemStorageType(ndKey.Key.XmlType, this.GetItemStorageType(ndKey.Key));
			this.helper.AddSortKey(ndKey.Key.XmlType);
			Label label2 = this.helper.DefineLabel();
			this.helper.EmitUnconditionalBranch(OpCodes.Br_S, label2);
			this.helper.MarkLabel(label);
			this.helper.AddSortKey(null);
			this.helper.MarkLabel(label2);
			this.EndNestedIterator(ndKey.Key);
		}

		// Token: 0x0600430D RID: 17165 RVA: 0x0016C97C File Offset: 0x0016AB7C
		protected override QilNode VisitDocOrderDistinct(QilUnary ndDod)
		{
			if (ndDod.XmlType.IsSingleton)
			{
				return this.Visit(ndDod.Child);
			}
			if (this.HandleDodPatterns(ndDod))
			{
				return ndDod;
			}
			this.helper.LoadQueryRuntime();
			this.NestedVisitEnsureCache(ndDod.Child, typeof(XPathNavigator));
			this.iterCurr.EnsureStack();
			this.helper.Call(XmlILMethods.DocOrder);
			return ndDod;
		}

		// Token: 0x0600430E RID: 17166 RVA: 0x0016C9EC File Offset: 0x0016ABEC
		private bool HandleDodPatterns(QilUnary ndDod)
		{
			OptimizerPatterns optimizerPatterns = OptimizerPatterns.Read(ndDod);
			bool flag = optimizerPatterns.MatchesPattern(OptimizerPatternName.JoinAndDod);
			if (flag || optimizerPatterns.MatchesPattern(OptimizerPatternName.DodReverse))
			{
				OptimizerPatterns optimizerPatterns2 = OptimizerPatterns.Read((QilNode)optimizerPatterns.GetArgument(OptimizerPatternArgument.ElementQName));
				XmlNodeKindFlags xmlNodeKindFlags;
				QilName qilName;
				if (optimizerPatterns2.MatchesPattern(OptimizerPatternName.FilterElements))
				{
					xmlNodeKindFlags = XmlNodeKindFlags.Element;
					qilName = (QilName)optimizerPatterns2.GetArgument(OptimizerPatternArgument.ElementQName);
				}
				else if (optimizerPatterns2.MatchesPattern(OptimizerPatternName.FilterContentKind))
				{
					xmlNodeKindFlags = ((XmlQueryType)optimizerPatterns2.GetArgument(OptimizerPatternArgument.ElementQName)).NodeKinds;
					qilName = null;
				}
				else
				{
					xmlNodeKindFlags = (((ndDod.XmlType.NodeKinds & XmlNodeKindFlags.Attribute) != XmlNodeKindFlags.None) ? XmlNodeKindFlags.Any : XmlNodeKindFlags.Content);
					qilName = null;
				}
				QilNode qilNode = (QilNode)optimizerPatterns2.GetArgument(OptimizerPatternArgument.StepNode);
				if (flag)
				{
					QilNodeType qilNodeType = qilNode.NodeType;
					if (qilNodeType <= QilNodeType.DescendantOrSelf)
					{
						if (qilNodeType == QilNodeType.Content)
						{
							this.CreateContainerIterator(ndDod, "$$$iterContent", typeof(ContentMergeIterator), XmlILMethods.ContentMergeCreate, XmlILMethods.ContentMergeNext, xmlNodeKindFlags, qilName, TriState.Unknown);
							return true;
						}
						if (qilNodeType - QilNodeType.Descendant <= 1)
						{
							this.CreateContainerIterator(ndDod, "$$$iterDesc", typeof(DescendantMergeIterator), XmlILMethods.DescMergeCreate, XmlILMethods.DescMergeNext, xmlNodeKindFlags, qilName, (qilNode.NodeType == QilNodeType.Descendant) ? TriState.False : TriState.True);
							return true;
						}
					}
					else
					{
						if (qilNodeType == QilNodeType.FollowingSibling)
						{
							this.CreateContainerIterator(ndDod, "$$$iterFollSib", typeof(FollowingSiblingMergeIterator), XmlILMethods.FollSibMergeCreate, XmlILMethods.FollSibMergeNext, xmlNodeKindFlags, qilName, TriState.Unknown);
							return true;
						}
						if (qilNodeType == QilNodeType.XPathFollowing)
						{
							this.CreateContainerIterator(ndDod, "$$$iterFoll", typeof(XPathFollowingMergeIterator), XmlILMethods.XPFollMergeCreate, XmlILMethods.XPFollMergeNext, xmlNodeKindFlags, qilName, TriState.Unknown);
							return true;
						}
						if (qilNodeType == QilNodeType.XPathPreceding)
						{
							this.CreateContainerIterator(ndDod, "$$$iterPrec", typeof(XPathPrecedingMergeIterator), XmlILMethods.XPPrecMergeCreate, XmlILMethods.XPPrecMergeNext, xmlNodeKindFlags, qilName, TriState.Unknown);
							return true;
						}
					}
				}
				else
				{
					QilNode qilNode2 = (QilNode)optimizerPatterns2.GetArgument(OptimizerPatternArgument.StepInput);
					QilNodeType qilNodeType = qilNode.NodeType;
					if (qilNodeType - QilNodeType.Ancestor <= 1)
					{
						this.CreateFilteredIterator(qilNode2, "$$$iterAnc", typeof(AncestorDocOrderIterator), XmlILMethods.AncDOCreate, XmlILMethods.AncDONext, xmlNodeKindFlags, qilName, (qilNode.NodeType == QilNodeType.Ancestor) ? TriState.False : TriState.True, null);
						return true;
					}
					if (qilNodeType == QilNodeType.PrecedingSibling)
					{
						this.CreateFilteredIterator(qilNode2, "$$$iterPreSib", typeof(PrecedingSiblingDocOrderIterator), XmlILMethods.PreSibDOCreate, XmlILMethods.PreSibDONext, xmlNodeKindFlags, qilName, TriState.Unknown, null);
						return true;
					}
					if (qilNodeType == QilNodeType.XPathPreceding)
					{
						this.CreateFilteredIterator(qilNode2, "$$$iterPrec", typeof(XPathPrecedingDocOrderIterator), XmlILMethods.XPPrecDOCreate, XmlILMethods.XPPrecDONext, xmlNodeKindFlags, qilName, TriState.Unknown, null);
						return true;
					}
				}
			}
			else if (optimizerPatterns.MatchesPattern(OptimizerPatternName.DodMerge))
			{
				LocalBuilder localBuilder = this.helper.DeclareLocal("$$$dodMerge", typeof(DodSequenceMerge));
				Label label = this.helper.DefineLabel();
				this.helper.Emit(OpCodes.Ldloca, localBuilder);
				this.helper.LoadQueryRuntime();
				this.helper.Call(XmlILMethods.DodMergeCreate);
				this.helper.Emit(OpCodes.Ldloca, localBuilder);
				this.StartNestedIterator(ndDod.Child, label);
				this.Visit(ndDod.Child);
				this.iterCurr.EnsureStack();
				this.helper.Call(XmlILMethods.DodMergeAdd);
				this.helper.Emit(OpCodes.Ldloca, localBuilder);
				this.iterCurr.LoopToEnd(label);
				this.EndNestedIterator(ndDod.Child);
				this.helper.Call(XmlILMethods.DodMergeSeq);
				this.iterCurr.Storage = StorageDescriptor.Stack(typeof(XPathNavigator), true);
				return true;
			}
			return false;
		}

		// Token: 0x0600430F RID: 17167 RVA: 0x0016CD54 File Offset: 0x0016AF54
		protected override QilNode VisitInvoke(QilInvoke ndInvoke)
		{
			QilFunction function = ndInvoke.Function;
			MethodInfo functionBinding = XmlILAnnotation.Write(function).FunctionBinding;
			bool flag = XmlILConstructInfo.Read(function).ConstructMethod == XmlILConstructMethod.Writer;
			this.helper.LoadQueryRuntime();
			for (int i = 0; i < ndInvoke.Arguments.Count; i++)
			{
				QilNode qilNode = ndInvoke.Arguments[i];
				QilNode qilNode2 = ndInvoke.Function.Arguments[i];
				this.NestedVisitEnsureStack(qilNode, this.GetItemStorageType(qilNode2), !qilNode2.XmlType.IsSingleton);
			}
			if (OptimizerPatterns.Read(ndInvoke).MatchesPattern(OptimizerPatternName.TailCall))
			{
				this.helper.TailCall(functionBinding);
			}
			else
			{
				this.helper.Call(functionBinding);
			}
			if (!flag)
			{
				this.iterCurr.Storage = StorageDescriptor.Stack(this.GetItemStorageType(ndInvoke), !ndInvoke.XmlType.IsSingleton);
			}
			else
			{
				this.iterCurr.Storage = StorageDescriptor.None();
			}
			return ndInvoke;
		}

		// Token: 0x06004310 RID: 17168 RVA: 0x0016CE44 File Offset: 0x0016B044
		protected override QilNode VisitContent(QilUnary ndContent)
		{
			this.CreateSimpleIterator(ndContent.Child, "$$$iterAttrContent", typeof(AttributeContentIterator), XmlILMethods.AttrContentCreate, XmlILMethods.AttrContentNext);
			return ndContent;
		}

		// Token: 0x06004311 RID: 17169 RVA: 0x0016CE6C File Offset: 0x0016B06C
		protected override QilNode VisitAttribute(QilBinary ndAttr)
		{
			QilName qilName = ndAttr.Right as QilName;
			LocalBuilder localBuilder = this.helper.DeclareLocal("$$$navAttr", typeof(XPathNavigator));
			this.SyncToNavigator(localBuilder, ndAttr.Left);
			this.helper.Emit(OpCodes.Ldloc, localBuilder);
			this.helper.CallGetAtomizedName(this.helper.StaticData.DeclareName(qilName.LocalName));
			this.helper.CallGetAtomizedName(this.helper.StaticData.DeclareName(qilName.NamespaceUri));
			this.helper.Call(XmlILMethods.NavMoveAttr);
			this.helper.Emit(OpCodes.Brfalse, this.iterCurr.GetLabelNext());
			this.iterCurr.Storage = StorageDescriptor.Local(localBuilder, typeof(XPathNavigator), false);
			return ndAttr;
		}

		// Token: 0x06004312 RID: 17170 RVA: 0x0016CF48 File Offset: 0x0016B148
		protected override QilNode VisitParent(QilUnary ndParent)
		{
			LocalBuilder localBuilder = this.helper.DeclareLocal("$$$navParent", typeof(XPathNavigator));
			this.SyncToNavigator(localBuilder, ndParent.Child);
			this.helper.Emit(OpCodes.Ldloc, localBuilder);
			this.helper.Call(XmlILMethods.NavMoveParent);
			this.helper.Emit(OpCodes.Brfalse, this.iterCurr.GetLabelNext());
			this.iterCurr.Storage = StorageDescriptor.Local(localBuilder, typeof(XPathNavigator), false);
			return ndParent;
		}

		// Token: 0x06004313 RID: 17171 RVA: 0x0016CFD8 File Offset: 0x0016B1D8
		protected override QilNode VisitRoot(QilUnary ndRoot)
		{
			LocalBuilder localBuilder = this.helper.DeclareLocal("$$$navRoot", typeof(XPathNavigator));
			this.SyncToNavigator(localBuilder, ndRoot.Child);
			this.helper.Emit(OpCodes.Ldloc, localBuilder);
			this.helper.Call(XmlILMethods.NavMoveRoot);
			this.iterCurr.Storage = StorageDescriptor.Local(localBuilder, typeof(XPathNavigator), false);
			return ndRoot;
		}

		// Token: 0x06004314 RID: 17172 RVA: 0x0016D04B File Offset: 0x0016B24B
		protected override QilNode VisitXmlContext(QilNode ndCtxt)
		{
			this.helper.LoadQueryContext();
			this.helper.Call(XmlILMethods.GetDefaultDataSource);
			this.iterCurr.Storage = StorageDescriptor.Stack(typeof(XPathNavigator), false);
			return ndCtxt;
		}

		// Token: 0x06004315 RID: 17173 RVA: 0x0016D084 File Offset: 0x0016B284
		protected override QilNode VisitDescendant(QilUnary ndDesc)
		{
			this.CreateFilteredIterator(ndDesc.Child, "$$$iterDesc", typeof(DescendantIterator), XmlILMethods.DescCreate, XmlILMethods.DescNext, XmlNodeKindFlags.Any, null, TriState.False, null);
			return ndDesc;
		}

		// Token: 0x06004316 RID: 17174 RVA: 0x0016D0BC File Offset: 0x0016B2BC
		protected override QilNode VisitDescendantOrSelf(QilUnary ndDesc)
		{
			this.CreateFilteredIterator(ndDesc.Child, "$$$iterDesc", typeof(DescendantIterator), XmlILMethods.DescCreate, XmlILMethods.DescNext, XmlNodeKindFlags.Any, null, TriState.True, null);
			return ndDesc;
		}

		// Token: 0x06004317 RID: 17175 RVA: 0x0016D0F4 File Offset: 0x0016B2F4
		protected override QilNode VisitAncestor(QilUnary ndAnc)
		{
			this.CreateFilteredIterator(ndAnc.Child, "$$$iterAnc", typeof(AncestorIterator), XmlILMethods.AncCreate, XmlILMethods.AncNext, XmlNodeKindFlags.Any, null, TriState.False, null);
			return ndAnc;
		}

		// Token: 0x06004318 RID: 17176 RVA: 0x0016D12C File Offset: 0x0016B32C
		protected override QilNode VisitAncestorOrSelf(QilUnary ndAnc)
		{
			this.CreateFilteredIterator(ndAnc.Child, "$$$iterAnc", typeof(AncestorIterator), XmlILMethods.AncCreate, XmlILMethods.AncNext, XmlNodeKindFlags.Any, null, TriState.True, null);
			return ndAnc;
		}

		// Token: 0x06004319 RID: 17177 RVA: 0x0016D164 File Offset: 0x0016B364
		protected override QilNode VisitPreceding(QilUnary ndPrec)
		{
			this.CreateFilteredIterator(ndPrec.Child, "$$$iterPrec", typeof(PrecedingIterator), XmlILMethods.PrecCreate, XmlILMethods.PrecNext, XmlNodeKindFlags.Any, null, TriState.Unknown, null);
			return ndPrec;
		}

		// Token: 0x0600431A RID: 17178 RVA: 0x0016D19C File Offset: 0x0016B39C
		protected override QilNode VisitFollowingSibling(QilUnary ndFollSib)
		{
			this.CreateFilteredIterator(ndFollSib.Child, "$$$iterFollSib", typeof(FollowingSiblingIterator), XmlILMethods.FollSibCreate, XmlILMethods.FollSibNext, XmlNodeKindFlags.Any, null, TriState.Unknown, null);
			return ndFollSib;
		}

		// Token: 0x0600431B RID: 17179 RVA: 0x0016D1D4 File Offset: 0x0016B3D4
		protected override QilNode VisitPrecedingSibling(QilUnary ndPreSib)
		{
			this.CreateFilteredIterator(ndPreSib.Child, "$$$iterPreSib", typeof(PrecedingSiblingIterator), XmlILMethods.PreSibCreate, XmlILMethods.PreSibNext, XmlNodeKindFlags.Any, null, TriState.Unknown, null);
			return ndPreSib;
		}

		// Token: 0x0600431C RID: 17180 RVA: 0x0016D20C File Offset: 0x0016B40C
		protected override QilNode VisitNodeRange(QilBinary ndRange)
		{
			this.CreateFilteredIterator(ndRange.Left, "$$$iterRange", typeof(NodeRangeIterator), XmlILMethods.NodeRangeCreate, XmlILMethods.NodeRangeNext, XmlNodeKindFlags.Any, null, TriState.Unknown, ndRange.Right);
			return ndRange;
		}

		// Token: 0x0600431D RID: 17181 RVA: 0x0016D24C File Offset: 0x0016B44C
		protected override QilNode VisitDeref(QilBinary ndDeref)
		{
			LocalBuilder localBuilder = this.helper.DeclareLocal("$$$iterId", typeof(IdIterator));
			this.helper.Emit(OpCodes.Ldloca, localBuilder);
			this.NestedVisitEnsureStack(ndDeref.Left);
			this.NestedVisitEnsureStack(ndDeref.Right);
			this.helper.Call(XmlILMethods.IdCreate);
			this.GenerateSimpleIterator(typeof(XPathNavigator), localBuilder, XmlILMethods.IdNext);
			return ndDeref;
		}

		// Token: 0x0600431E RID: 17182 RVA: 0x0016D2C4 File Offset: 0x0016B4C4
		protected override QilNode VisitElementCtor(QilBinary ndElem)
		{
			XmlILConstructInfo xmlILConstructInfo = XmlILConstructInfo.Read(ndElem);
			bool flag = this.CheckWithinContent(xmlILConstructInfo) || !xmlILConstructInfo.IsNamespaceInScope || this.ElementCachesAttributes(xmlILConstructInfo);
			if (XmlILConstructInfo.Read(ndElem.Right).FinalStates == PossibleXmlStates.Any)
			{
				flag = true;
			}
			if (xmlILConstructInfo.FinalStates == PossibleXmlStates.Any)
			{
				flag = true;
			}
			if (!flag)
			{
				this.BeforeStartChecks(ndElem);
			}
			GenerateNameType generateNameType = this.LoadNameAndType(XPathNodeType.Element, ndElem.Left, true, flag);
			this.helper.CallWriteStartElement(generateNameType, flag);
			this.NestedVisit(ndElem.Right);
			if (XmlILConstructInfo.Read(ndElem.Right).FinalStates == PossibleXmlStates.EnumAttrs && !flag)
			{
				this.helper.CallStartElementContent();
			}
			generateNameType = this.LoadNameAndType(XPathNodeType.Element, ndElem.Left, false, flag);
			this.helper.CallWriteEndElement(generateNameType, flag);
			if (!flag)
			{
				this.AfterEndChecks(ndElem);
			}
			this.iterCurr.Storage = StorageDescriptor.None();
			return ndElem;
		}

		// Token: 0x0600431F RID: 17183 RVA: 0x0016D3A0 File Offset: 0x0016B5A0
		protected override QilNode VisitAttributeCtor(QilBinary ndAttr)
		{
			XmlILConstructInfo xmlILConstructInfo = XmlILConstructInfo.Read(ndAttr);
			bool flag = this.CheckEnumAttrs(xmlILConstructInfo) || !xmlILConstructInfo.IsNamespaceInScope;
			if (!flag)
			{
				this.BeforeStartChecks(ndAttr);
			}
			GenerateNameType generateNameType = this.LoadNameAndType(XPathNodeType.Attribute, ndAttr.Left, true, flag);
			this.helper.CallWriteStartAttribute(generateNameType, flag);
			this.NestedVisit(ndAttr.Right);
			this.helper.CallWriteEndAttribute(flag);
			if (!flag)
			{
				this.AfterEndChecks(ndAttr);
			}
			this.iterCurr.Storage = StorageDescriptor.None();
			return ndAttr;
		}

		// Token: 0x06004320 RID: 17184 RVA: 0x0016D424 File Offset: 0x0016B624
		protected override QilNode VisitCommentCtor(QilUnary ndComment)
		{
			this.helper.CallWriteStartComment();
			this.NestedVisit(ndComment.Child);
			this.helper.CallWriteEndComment();
			this.iterCurr.Storage = StorageDescriptor.None();
			return ndComment;
		}

		// Token: 0x06004321 RID: 17185 RVA: 0x0016D45C File Offset: 0x0016B65C
		protected override QilNode VisitPICtor(QilBinary ndPI)
		{
			this.helper.LoadQueryOutput();
			this.NestedVisitEnsureStack(ndPI.Left);
			this.helper.CallWriteStartPI();
			this.NestedVisit(ndPI.Right);
			this.helper.CallWriteEndPI();
			this.iterCurr.Storage = StorageDescriptor.None();
			return ndPI;
		}

		// Token: 0x06004322 RID: 17186 RVA: 0x0016D4B3 File Offset: 0x0016B6B3
		protected override QilNode VisitTextCtor(QilUnary ndText)
		{
			return this.VisitTextCtor(ndText, false);
		}

		// Token: 0x06004323 RID: 17187 RVA: 0x0016D4BD File Offset: 0x0016B6BD
		protected override QilNode VisitRawTextCtor(QilUnary ndText)
		{
			return this.VisitTextCtor(ndText, true);
		}

		// Token: 0x06004324 RID: 17188 RVA: 0x0016D4C8 File Offset: 0x0016B6C8
		private QilNode VisitTextCtor(QilUnary ndText, bool disableOutputEscaping)
		{
			XmlILConstructInfo xmlILConstructInfo = XmlILConstructInfo.Read(ndText);
			PossibleXmlStates initialStates = xmlILConstructInfo.InitialStates;
			bool flag = initialStates - PossibleXmlStates.WithinAttr > 2 && this.CheckWithinContent(xmlILConstructInfo);
			if (!flag)
			{
				this.BeforeStartChecks(ndText);
			}
			this.helper.LoadQueryOutput();
			this.NestedVisitEnsureStack(ndText.Child);
			switch (xmlILConstructInfo.InitialStates)
			{
			case PossibleXmlStates.WithinAttr:
				this.helper.CallWriteString(false, flag);
				break;
			case PossibleXmlStates.WithinComment:
				this.helper.Call(XmlILMethods.CommentText);
				break;
			case PossibleXmlStates.WithinPI:
				this.helper.Call(XmlILMethods.PIText);
				break;
			default:
				this.helper.CallWriteString(disableOutputEscaping, flag);
				break;
			}
			if (!flag)
			{
				this.AfterEndChecks(ndText);
			}
			this.iterCurr.Storage = StorageDescriptor.None();
			return ndText;
		}

		// Token: 0x06004325 RID: 17189 RVA: 0x0016D58E File Offset: 0x0016B78E
		protected override QilNode VisitDocumentCtor(QilUnary ndDoc)
		{
			this.helper.CallWriteStartRoot();
			this.NestedVisit(ndDoc.Child);
			this.helper.CallWriteEndRoot();
			this.iterCurr.Storage = StorageDescriptor.None();
			return ndDoc;
		}

		// Token: 0x06004326 RID: 17190 RVA: 0x0016D5C4 File Offset: 0x0016B7C4
		protected override QilNode VisitNamespaceDecl(QilBinary ndNmsp)
		{
			XmlILConstructInfo xmlILConstructInfo = XmlILConstructInfo.Read(ndNmsp);
			bool flag = this.CheckEnumAttrs(xmlILConstructInfo) || this.MightHaveNamespacesAfterAttributes(xmlILConstructInfo);
			if (!flag)
			{
				this.BeforeStartChecks(ndNmsp);
			}
			this.helper.LoadQueryOutput();
			this.NestedVisitEnsureStack(ndNmsp.Left);
			this.NestedVisitEnsureStack(ndNmsp.Right);
			this.helper.CallWriteNamespaceDecl(flag);
			if (!flag)
			{
				this.AfterEndChecks(ndNmsp);
			}
			this.iterCurr.Storage = StorageDescriptor.None();
			return ndNmsp;
		}

		// Token: 0x06004327 RID: 17191 RVA: 0x0016D640 File Offset: 0x0016B840
		protected override QilNode VisitRtfCtor(QilBinary ndRtf)
		{
			OptimizerPatterns optimizerPatterns = OptimizerPatterns.Read(ndRtf);
			string text = (QilLiteral)ndRtf.Right;
			if (optimizerPatterns.MatchesPattern(OptimizerPatternName.SingleTextRtf))
			{
				this.helper.LoadQueryRuntime();
				this.NestedVisitEnsureStack((QilNode)optimizerPatterns.GetArgument(OptimizerPatternArgument.ElementQName));
				this.helper.Emit(OpCodes.Ldstr, text);
				this.helper.Call(XmlILMethods.RtfConstr);
			}
			else
			{
				this.helper.CallStartRtfConstruction(text);
				this.NestedVisit(ndRtf.Left);
				this.helper.CallEndRtfConstruction();
			}
			this.iterCurr.Storage = StorageDescriptor.Stack(typeof(XPathNavigator), false);
			return ndRtf;
		}

		// Token: 0x06004328 RID: 17192 RVA: 0x0016D6EE File Offset: 0x0016B8EE
		protected override QilNode VisitNameOf(QilUnary ndName)
		{
			return this.VisitNodeProperty(ndName);
		}

		// Token: 0x06004329 RID: 17193 RVA: 0x0016D6EE File Offset: 0x0016B8EE
		protected override QilNode VisitLocalNameOf(QilUnary ndName)
		{
			return this.VisitNodeProperty(ndName);
		}

		// Token: 0x0600432A RID: 17194 RVA: 0x0016D6EE File Offset: 0x0016B8EE
		protected override QilNode VisitNamespaceUriOf(QilUnary ndName)
		{
			return this.VisitNodeProperty(ndName);
		}

		// Token: 0x0600432B RID: 17195 RVA: 0x0016D6EE File Offset: 0x0016B8EE
		protected override QilNode VisitPrefixOf(QilUnary ndName)
		{
			return this.VisitNodeProperty(ndName);
		}

		// Token: 0x0600432C RID: 17196 RVA: 0x0016D6F8 File Offset: 0x0016B8F8
		private QilNode VisitNodeProperty(QilUnary ndProp)
		{
			this.NestedVisitEnsureStack(ndProp.Child);
			switch (ndProp.NodeType)
			{
			case QilNodeType.NameOf:
				this.helper.Emit(OpCodes.Dup);
				this.helper.Call(XmlILMethods.NavLocalName);
				this.helper.Call(XmlILMethods.NavNmsp);
				this.helper.Construct(XmlILConstructors.QName);
				this.iterCurr.Storage = StorageDescriptor.Stack(typeof(XmlQualifiedName), false);
				break;
			case QilNodeType.LocalNameOf:
				this.helper.Call(XmlILMethods.NavLocalName);
				this.iterCurr.Storage = StorageDescriptor.Stack(typeof(string), false);
				break;
			case QilNodeType.NamespaceUriOf:
				this.helper.Call(XmlILMethods.NavNmsp);
				this.iterCurr.Storage = StorageDescriptor.Stack(typeof(string), false);
				break;
			case QilNodeType.PrefixOf:
				this.helper.Call(XmlILMethods.NavPrefix);
				this.iterCurr.Storage = StorageDescriptor.Stack(typeof(string), false);
				break;
			}
			return ndProp;
		}

		// Token: 0x0600432D RID: 17197 RVA: 0x0016D81C File Offset: 0x0016BA1C
		protected override QilNode VisitTypeAssert(QilTargetType ndTypeAssert)
		{
			if (!ndTypeAssert.Source.XmlType.IsSingleton && ndTypeAssert.XmlType.IsSingleton && !this.iterCurr.HasLabelNext)
			{
				Label label = this.helper.DefineLabel();
				this.helper.MarkLabel(label);
				this.NestedVisit(ndTypeAssert.Source, label);
			}
			else
			{
				this.Visit(ndTypeAssert.Source);
			}
			this.iterCurr.EnsureItemStorageType(ndTypeAssert.Source.XmlType, this.GetItemStorageType(ndTypeAssert));
			return ndTypeAssert;
		}

		// Token: 0x0600432E RID: 17198 RVA: 0x0016D8A8 File Offset: 0x0016BAA8
		protected override QilNode VisitIsType(QilTargetType ndIsType)
		{
			XmlQueryType xmlType = ndIsType.Source.XmlType;
			XmlQueryType targetType = ndIsType.TargetType;
			if (xmlType.IsSingleton && targetType == XmlQueryTypeFactory.Node)
			{
				this.NestedVisitEnsureStack(ndIsType.Source);
				this.helper.Call(XmlILMethods.ItemIsNode);
				this.ZeroCompare(QilNodeType.Ne, true);
				return ndIsType;
			}
			if (this.MatchesNodeKinds(ndIsType, xmlType, targetType))
			{
				return ndIsType;
			}
			XmlTypeCode xmlTypeCode;
			if (targetType == XmlQueryTypeFactory.Double)
			{
				xmlTypeCode = XmlTypeCode.Double;
			}
			else if (targetType == XmlQueryTypeFactory.String)
			{
				xmlTypeCode = XmlTypeCode.String;
			}
			else if (targetType == XmlQueryTypeFactory.Boolean)
			{
				xmlTypeCode = XmlTypeCode.Boolean;
			}
			else if (targetType == XmlQueryTypeFactory.Node)
			{
				xmlTypeCode = XmlTypeCode.Node;
			}
			else
			{
				xmlTypeCode = XmlTypeCode.None;
			}
			if (xmlTypeCode != XmlTypeCode.None)
			{
				this.helper.LoadQueryRuntime();
				this.NestedVisitEnsureStack(ndIsType.Source, typeof(XPathItem), !xmlType.IsSingleton);
				this.helper.LoadInteger((int)xmlTypeCode);
				this.helper.Call(xmlType.IsSingleton ? XmlILMethods.ItemMatchesCode : XmlILMethods.SeqMatchesCode);
				this.ZeroCompare(QilNodeType.Ne, true);
				return ndIsType;
			}
			this.helper.LoadQueryRuntime();
			this.NestedVisitEnsureStack(ndIsType.Source, typeof(XPathItem), !xmlType.IsSingleton);
			this.helper.LoadInteger(this.helper.StaticData.DeclareXmlType(targetType));
			this.helper.Call(xmlType.IsSingleton ? XmlILMethods.ItemMatchesType : XmlILMethods.SeqMatchesType);
			this.ZeroCompare(QilNodeType.Ne, true);
			return ndIsType;
		}

		// Token: 0x0600432F RID: 17199 RVA: 0x0016DA14 File Offset: 0x0016BC14
		private bool MatchesNodeKinds(QilTargetType ndIsType, XmlQueryType typDerived, XmlQueryType typBase)
		{
			bool flag = true;
			if (!typBase.IsNode || !typBase.IsSingleton)
			{
				return false;
			}
			if (!typDerived.IsNode || !typDerived.IsSingleton || !typDerived.IsNotRtf)
			{
				return false;
			}
			XmlNodeKindFlags xmlNodeKindFlags = XmlNodeKindFlags.None;
			foreach (XmlQueryType xmlQueryType in typBase)
			{
				if (xmlQueryType == XmlQueryTypeFactory.Element)
				{
					xmlNodeKindFlags |= XmlNodeKindFlags.Element;
				}
				else if (xmlQueryType == XmlQueryTypeFactory.Attribute)
				{
					xmlNodeKindFlags |= XmlNodeKindFlags.Attribute;
				}
				else if (xmlQueryType == XmlQueryTypeFactory.Text)
				{
					xmlNodeKindFlags |= XmlNodeKindFlags.Text;
				}
				else if (xmlQueryType == XmlQueryTypeFactory.Document)
				{
					xmlNodeKindFlags |= XmlNodeKindFlags.Document;
				}
				else if (xmlQueryType == XmlQueryTypeFactory.Comment)
				{
					xmlNodeKindFlags |= XmlNodeKindFlags.Comment;
				}
				else if (xmlQueryType == XmlQueryTypeFactory.PI)
				{
					xmlNodeKindFlags |= XmlNodeKindFlags.PI;
				}
				else
				{
					if (xmlQueryType != XmlQueryTypeFactory.Namespace)
					{
						return false;
					}
					xmlNodeKindFlags |= XmlNodeKindFlags.Namespace;
				}
			}
			xmlNodeKindFlags = typDerived.NodeKinds & xmlNodeKindFlags;
			if (!Bits.ExactlyOne((uint)xmlNodeKindFlags))
			{
				xmlNodeKindFlags = ~xmlNodeKindFlags & XmlNodeKindFlags.Any;
				flag = !flag;
			}
			XPathNodeType xpathNodeType;
			if (xmlNodeKindFlags <= XmlNodeKindFlags.Comment)
			{
				switch (xmlNodeKindFlags)
				{
				case XmlNodeKindFlags.Document:
					xpathNodeType = XPathNodeType.Root;
					goto IL_014A;
				case XmlNodeKindFlags.Element:
					xpathNodeType = XPathNodeType.Element;
					goto IL_014A;
				case XmlNodeKindFlags.Document | XmlNodeKindFlags.Element:
					break;
				case XmlNodeKindFlags.Attribute:
					xpathNodeType = XPathNodeType.Attribute;
					goto IL_014A;
				default:
					if (xmlNodeKindFlags == XmlNodeKindFlags.Comment)
					{
						xpathNodeType = XPathNodeType.Comment;
						goto IL_014A;
					}
					break;
				}
			}
			else
			{
				if (xmlNodeKindFlags == XmlNodeKindFlags.PI)
				{
					xpathNodeType = XPathNodeType.ProcessingInstruction;
					goto IL_014A;
				}
				if (xmlNodeKindFlags == XmlNodeKindFlags.Namespace)
				{
					xpathNodeType = XPathNodeType.Namespace;
					goto IL_014A;
				}
			}
			this.helper.Emit(OpCodes.Ldc_I4_1);
			xpathNodeType = XPathNodeType.All;
			IL_014A:
			this.NestedVisitEnsureStack(ndIsType.Source);
			this.helper.Call(XmlILMethods.NavType);
			if (xpathNodeType == XPathNodeType.All)
			{
				this.helper.Emit(OpCodes.Shl);
				int num = 0;
				if ((xmlNodeKindFlags & XmlNodeKindFlags.Document) != XmlNodeKindFlags.None)
				{
					num |= 1;
				}
				if ((xmlNodeKindFlags & XmlNodeKindFlags.Element) != XmlNodeKindFlags.None)
				{
					num |= 2;
				}
				if ((xmlNodeKindFlags & XmlNodeKindFlags.Attribute) != XmlNodeKindFlags.None)
				{
					num |= 4;
				}
				if ((xmlNodeKindFlags & XmlNodeKindFlags.Text) != XmlNodeKindFlags.None)
				{
					num |= 112;
				}
				if ((xmlNodeKindFlags & XmlNodeKindFlags.Comment) != XmlNodeKindFlags.None)
				{
					num |= 256;
				}
				if ((xmlNodeKindFlags & XmlNodeKindFlags.PI) != XmlNodeKindFlags.None)
				{
					num |= 128;
				}
				if ((xmlNodeKindFlags & XmlNodeKindFlags.Namespace) != XmlNodeKindFlags.None)
				{
					num |= 8;
				}
				this.helper.LoadInteger(num);
				this.helper.Emit(OpCodes.And);
				this.ZeroCompare(flag ? QilNodeType.Ne : QilNodeType.Eq, false);
			}
			else
			{
				this.helper.LoadInteger((int)xpathNodeType);
				this.ClrCompare(flag ? QilNodeType.Eq : QilNodeType.Ne, XmlTypeCode.Int);
			}
			return true;
		}

		// Token: 0x06004330 RID: 17200 RVA: 0x0016DC4C File Offset: 0x0016BE4C
		protected override QilNode VisitIsEmpty(QilUnary ndIsEmpty)
		{
			if (this.CachesResult(ndIsEmpty.Child))
			{
				this.NestedVisitEnsureStack(ndIsEmpty.Child);
				this.helper.CallCacheCount(this.iterNested.Storage.ItemStorageType);
				BranchingContext currentBranchingContext = this.iterCurr.CurrentBranchingContext;
				if (currentBranchingContext != BranchingContext.OnTrue)
				{
					if (currentBranchingContext == BranchingContext.OnFalse)
					{
						this.helper.TestAndBranch(0, this.iterCurr.LabelBranch, OpCodes.Bne_Un);
					}
					else
					{
						Label label = this.helper.DefineLabel();
						this.helper.Emit(OpCodes.Brfalse_S, label);
						this.helper.ConvBranchToBool(label, true);
					}
				}
				else
				{
					this.helper.TestAndBranch(0, this.iterCurr.LabelBranch, OpCodes.Beq);
				}
			}
			else
			{
				Label label2 = this.helper.DefineLabel();
				IteratorDescriptor iteratorDescriptor = this.iterCurr;
				if (iteratorDescriptor.CurrentBranchingContext == BranchingContext.OnTrue)
				{
					this.StartNestedIterator(ndIsEmpty.Child, this.iterCurr.LabelBranch);
				}
				else
				{
					this.StartNestedIterator(ndIsEmpty.Child, label2);
				}
				this.Visit(ndIsEmpty.Child);
				this.iterCurr.EnsureNoCache();
				this.iterCurr.DiscardStack();
				switch (iteratorDescriptor.CurrentBranchingContext)
				{
				case BranchingContext.None:
					this.helper.ConvBranchToBool(label2, true);
					break;
				case BranchingContext.OnFalse:
					this.helper.EmitUnconditionalBranch(OpCodes.Br, iteratorDescriptor.LabelBranch);
					this.helper.MarkLabel(label2);
					break;
				}
				this.EndNestedIterator(ndIsEmpty.Child);
			}
			if (this.iterCurr.IsBranching)
			{
				this.iterCurr.Storage = StorageDescriptor.None();
			}
			else
			{
				this.iterCurr.Storage = StorageDescriptor.Stack(typeof(bool), false);
			}
			return ndIsEmpty;
		}

		// Token: 0x06004331 RID: 17201 RVA: 0x0016DE18 File Offset: 0x0016C018
		protected override QilNode VisitXPathNodeValue(QilUnary ndVal)
		{
			if (ndVal.Child.XmlType.IsSingleton)
			{
				this.NestedVisitEnsureStack(ndVal.Child, typeof(XPathNavigator), false);
				this.helper.Call(XmlILMethods.Value);
			}
			else
			{
				Label label = this.helper.DefineLabel();
				this.StartNestedIterator(ndVal.Child, label);
				this.Visit(ndVal.Child);
				this.iterCurr.EnsureStackNoCache();
				this.helper.Call(XmlILMethods.Value);
				Label label2 = this.helper.DefineLabel();
				this.helper.EmitUnconditionalBranch(OpCodes.Br, label2);
				this.helper.MarkLabel(label);
				this.helper.Emit(OpCodes.Ldstr, "");
				this.helper.MarkLabel(label2);
				this.EndNestedIterator(ndVal.Child);
			}
			this.iterCurr.Storage = StorageDescriptor.Stack(typeof(string), false);
			return ndVal;
		}

		// Token: 0x06004332 RID: 17202 RVA: 0x0016DF18 File Offset: 0x0016C118
		protected override QilNode VisitXPathFollowing(QilUnary ndFoll)
		{
			this.CreateFilteredIterator(ndFoll.Child, "$$$iterFoll", typeof(XPathFollowingIterator), XmlILMethods.XPFollCreate, XmlILMethods.XPFollNext, XmlNodeKindFlags.Any, null, TriState.Unknown, null);
			return ndFoll;
		}

		// Token: 0x06004333 RID: 17203 RVA: 0x0016DF50 File Offset: 0x0016C150
		protected override QilNode VisitXPathPreceding(QilUnary ndPrec)
		{
			this.CreateFilteredIterator(ndPrec.Child, "$$$iterPrec", typeof(XPathPrecedingIterator), XmlILMethods.XPPrecCreate, XmlILMethods.XPPrecNext, XmlNodeKindFlags.Any, null, TriState.Unknown, null);
			return ndPrec;
		}

		// Token: 0x06004334 RID: 17204 RVA: 0x0016DF88 File Offset: 0x0016C188
		protected override QilNode VisitXPathNamespace(QilUnary ndNmsp)
		{
			this.CreateSimpleIterator(ndNmsp.Child, "$$$iterNmsp", typeof(NamespaceIterator), XmlILMethods.NmspCreate, XmlILMethods.NmspNext);
			return ndNmsp;
		}

		// Token: 0x06004335 RID: 17205 RVA: 0x0016DFB0 File Offset: 0x0016C1B0
		protected override QilNode VisitXsltGenerateId(QilUnary ndGenId)
		{
			this.helper.LoadQueryRuntime();
			if (ndGenId.Child.XmlType.IsSingleton)
			{
				this.NestedVisitEnsureStack(ndGenId.Child, typeof(XPathNavigator), false);
				this.helper.Call(XmlILMethods.GenId);
			}
			else
			{
				Label label = this.helper.DefineLabel();
				this.StartNestedIterator(ndGenId.Child, label);
				this.Visit(ndGenId.Child);
				this.iterCurr.EnsureStackNoCache();
				this.iterCurr.EnsureItemStorageType(ndGenId.Child.XmlType, typeof(XPathNavigator));
				this.helper.Call(XmlILMethods.GenId);
				Label label2 = this.helper.DefineLabel();
				this.helper.EmitUnconditionalBranch(OpCodes.Br, label2);
				this.helper.MarkLabel(label);
				this.helper.Emit(OpCodes.Pop);
				this.helper.Emit(OpCodes.Ldstr, "");
				this.helper.MarkLabel(label2);
				this.EndNestedIterator(ndGenId.Child);
			}
			this.iterCurr.Storage = StorageDescriptor.Stack(typeof(string), false);
			return ndGenId;
		}

		// Token: 0x06004336 RID: 17206 RVA: 0x0016E0EC File Offset: 0x0016C2EC
		protected override QilNode VisitXsltInvokeLateBound(QilInvokeLateBound ndInvoke)
		{
			LocalBuilder localBuilder = this.helper.DeclareLocal("$$$args", typeof(IList<XPathItem>[]));
			QilName name = ndInvoke.Name;
			this.helper.LoadQueryContext();
			this.helper.Emit(OpCodes.Ldstr, name.LocalName);
			this.helper.Emit(OpCodes.Ldstr, name.NamespaceUri);
			this.helper.LoadInteger(ndInvoke.Arguments.Count);
			this.helper.Emit(OpCodes.Newarr, typeof(IList<XPathItem>));
			this.helper.Emit(OpCodes.Stloc, localBuilder);
			for (int i = 0; i < ndInvoke.Arguments.Count; i++)
			{
				QilNode qilNode = ndInvoke.Arguments[i];
				this.helper.Emit(OpCodes.Ldloc, localBuilder);
				this.helper.LoadInteger(i);
				this.helper.Emit(OpCodes.Ldelema, typeof(IList<XPathItem>));
				this.NestedVisitEnsureCache(qilNode, typeof(XPathItem));
				this.iterCurr.EnsureStack();
				this.helper.Emit(OpCodes.Stobj, typeof(IList<XPathItem>));
			}
			this.helper.Emit(OpCodes.Ldloc, localBuilder);
			this.helper.Call(XmlILMethods.InvokeXsltLate);
			this.iterCurr.Storage = StorageDescriptor.Stack(typeof(XPathItem), true);
			return ndInvoke;
		}

		// Token: 0x06004337 RID: 17207 RVA: 0x0016E264 File Offset: 0x0016C464
		protected override QilNode VisitXsltInvokeEarlyBound(QilInvokeEarlyBound ndInvoke)
		{
			QilName name = ndInvoke.Name;
			XmlExtensionFunction xmlExtensionFunction = new XmlExtensionFunction(name.LocalName, name.NamespaceUri, ndInvoke.ClrMethod);
			Type clrReturnType = xmlExtensionFunction.ClrReturnType;
			Type storageType = this.GetStorageType(ndInvoke);
			if (clrReturnType != storageType && !ndInvoke.XmlType.IsEmpty)
			{
				this.helper.LoadQueryRuntime();
				this.helper.LoadInteger(this.helper.StaticData.DeclareXmlType(ndInvoke.XmlType));
			}
			if (!xmlExtensionFunction.Method.IsStatic)
			{
				if (name.NamespaceUri.Length == 0)
				{
					this.helper.LoadXsltLibrary();
				}
				else
				{
					this.helper.CallGetEarlyBoundObject(this.helper.StaticData.DeclareEarlyBound(name.NamespaceUri, xmlExtensionFunction.Method.DeclaringType), xmlExtensionFunction.Method.DeclaringType);
				}
			}
			for (int i = 0; i < ndInvoke.Arguments.Count; i++)
			{
				QilNode qilNode = ndInvoke.Arguments[i];
				XmlQueryType xmlArgumentType = xmlExtensionFunction.GetXmlArgumentType(i);
				Type clrArgumentType = xmlExtensionFunction.GetClrArgumentType(i);
				if (name.NamespaceUri.Length == 0)
				{
					Type itemStorageType = this.GetItemStorageType(qilNode);
					if (clrArgumentType == XmlILMethods.StorageMethods[itemStorageType].IListType)
					{
						this.NestedVisitEnsureStack(qilNode, itemStorageType, true);
					}
					else if (clrArgumentType == XmlILMethods.StorageMethods[typeof(XPathItem)].IListType)
					{
						this.NestedVisitEnsureStack(qilNode, typeof(XPathItem), true);
					}
					else if ((qilNode.XmlType.IsSingleton && clrArgumentType == itemStorageType) || qilNode.XmlType.TypeCode == XmlTypeCode.None)
					{
						this.NestedVisitEnsureStack(qilNode, clrArgumentType, false);
					}
					else if (qilNode.XmlType.IsSingleton && clrArgumentType == typeof(XPathItem))
					{
						this.NestedVisitEnsureStack(qilNode, typeof(XPathItem), false);
					}
				}
				else
				{
					Type storageType2 = this.GetStorageType(xmlArgumentType);
					if (xmlArgumentType.TypeCode == XmlTypeCode.Item || !clrArgumentType.IsAssignableFrom(storageType2))
					{
						this.helper.LoadQueryRuntime();
						this.helper.LoadInteger(this.helper.StaticData.DeclareXmlType(xmlArgumentType));
						this.NestedVisitEnsureStack(qilNode, this.GetItemStorageType(xmlArgumentType), !xmlArgumentType.IsSingleton);
						this.helper.TreatAs(storageType2, typeof(object));
						this.helper.LoadType(clrArgumentType);
						this.helper.Call(XmlILMethods.ChangeTypeXsltArg);
						this.helper.TreatAs(typeof(object), clrArgumentType);
					}
					else
					{
						this.NestedVisitEnsureStack(qilNode, this.GetItemStorageType(xmlArgumentType), !xmlArgumentType.IsSingleton);
					}
				}
			}
			this.helper.Call(xmlExtensionFunction.Method);
			if (ndInvoke.XmlType.IsEmpty)
			{
				this.helper.Emit(OpCodes.Ldsfld, XmlILMethods.StorageMethods[typeof(XPathItem)].SeqEmpty);
			}
			else if (clrReturnType != storageType)
			{
				this.helper.TreatAs(clrReturnType, typeof(object));
				this.helper.Call(XmlILMethods.ChangeTypeXsltResult);
				this.helper.TreatAs(typeof(object), storageType);
			}
			else if (name.NamespaceUri.Length != 0 && !clrReturnType.IsValueType)
			{
				Label label = this.helper.DefineLabel();
				this.helper.Emit(OpCodes.Dup);
				this.helper.Emit(OpCodes.Brtrue, label);
				this.helper.LoadQueryRuntime();
				this.helper.Emit(OpCodes.Ldstr, Res.GetString("Extension functions cannot return null values."));
				this.helper.Call(XmlILMethods.ThrowException);
				this.helper.MarkLabel(label);
			}
			this.iterCurr.Storage = StorageDescriptor.Stack(this.GetItemStorageType(ndInvoke), !ndInvoke.XmlType.IsSingleton);
			return ndInvoke;
		}

		// Token: 0x06004338 RID: 17208 RVA: 0x0016E684 File Offset: 0x0016C884
		protected override QilNode VisitXsltCopy(QilBinary ndCopy)
		{
			Label label = this.helper.DefineLabel();
			this.helper.LoadQueryOutput();
			this.NestedVisitEnsureStack(ndCopy.Left);
			this.helper.Call(XmlILMethods.StartCopy);
			this.helper.Emit(OpCodes.Brfalse, label);
			this.NestedVisit(ndCopy.Right);
			this.helper.LoadQueryOutput();
			this.NestedVisitEnsureStack(ndCopy.Left);
			this.helper.Call(XmlILMethods.EndCopy);
			this.helper.MarkLabel(label);
			this.iterCurr.Storage = StorageDescriptor.None();
			return ndCopy;
		}

		// Token: 0x06004339 RID: 17209 RVA: 0x0016E725 File Offset: 0x0016C925
		protected override QilNode VisitXsltCopyOf(QilUnary ndCopyOf)
		{
			this.helper.LoadQueryOutput();
			this.NestedVisitEnsureStack(ndCopyOf.Child);
			this.helper.Call(XmlILMethods.CopyOf);
			this.iterCurr.Storage = StorageDescriptor.None();
			return ndCopyOf;
		}

		// Token: 0x0600433A RID: 17210 RVA: 0x0016E760 File Offset: 0x0016C960
		protected override QilNode VisitXsltConvert(QilTargetType ndConv)
		{
			XmlQueryType xmlType = ndConv.Source.XmlType;
			XmlQueryType targetType = ndConv.TargetType;
			MethodInfo methodInfo;
			if (this.GetXsltConvertMethod(xmlType, targetType, out methodInfo))
			{
				this.NestedVisitEnsureStack(ndConv.Source);
			}
			else
			{
				this.NestedVisitEnsureStack(ndConv.Source, typeof(XPathItem), !xmlType.IsSingleton);
				this.GetXsltConvertMethod(xmlType.IsSingleton ? XmlQueryTypeFactory.Item : XmlQueryTypeFactory.ItemS, targetType, out methodInfo);
			}
			if (methodInfo != null)
			{
				this.helper.Call(methodInfo);
			}
			this.iterCurr.Storage = StorageDescriptor.Stack(this.GetItemStorageType(targetType), !targetType.IsSingleton);
			return ndConv;
		}

		// Token: 0x0600433B RID: 17211 RVA: 0x0016E810 File Offset: 0x0016CA10
		private bool GetXsltConvertMethod(XmlQueryType typSrc, XmlQueryType typDst, out MethodInfo meth)
		{
			meth = null;
			if (typDst == XmlQueryTypeFactory.BooleanX)
			{
				if (typSrc == XmlQueryTypeFactory.Item)
				{
					meth = XmlILMethods.ItemToBool;
				}
				else if (typSrc == XmlQueryTypeFactory.ItemS)
				{
					meth = XmlILMethods.ItemsToBool;
				}
			}
			else if (typDst == XmlQueryTypeFactory.DateTimeX)
			{
				if (typSrc == XmlQueryTypeFactory.StringX)
				{
					meth = XmlILMethods.StrToDT;
				}
			}
			else if (typDst == XmlQueryTypeFactory.DecimalX)
			{
				if (typSrc == XmlQueryTypeFactory.DoubleX)
				{
					meth = XmlILMethods.DblToDec;
				}
			}
			else if (typDst == XmlQueryTypeFactory.DoubleX)
			{
				if (typSrc == XmlQueryTypeFactory.DecimalX)
				{
					meth = XmlILMethods.DecToDbl;
				}
				else if (typSrc == XmlQueryTypeFactory.IntX)
				{
					meth = XmlILMethods.IntToDbl;
				}
				else if (typSrc == XmlQueryTypeFactory.Item)
				{
					meth = XmlILMethods.ItemToDbl;
				}
				else if (typSrc == XmlQueryTypeFactory.ItemS)
				{
					meth = XmlILMethods.ItemsToDbl;
				}
				else if (typSrc == XmlQueryTypeFactory.LongX)
				{
					meth = XmlILMethods.LngToDbl;
				}
				else if (typSrc == XmlQueryTypeFactory.StringX)
				{
					meth = XmlILMethods.StrToDbl;
				}
			}
			else if (typDst == XmlQueryTypeFactory.IntX)
			{
				if (typSrc == XmlQueryTypeFactory.DoubleX)
				{
					meth = XmlILMethods.DblToInt;
				}
			}
			else if (typDst == XmlQueryTypeFactory.LongX)
			{
				if (typSrc == XmlQueryTypeFactory.DoubleX)
				{
					meth = XmlILMethods.DblToLng;
				}
			}
			else if (typDst == XmlQueryTypeFactory.NodeNotRtf)
			{
				if (typSrc == XmlQueryTypeFactory.Item)
				{
					meth = XmlILMethods.ItemToNode;
				}
				else if (typSrc == XmlQueryTypeFactory.ItemS)
				{
					meth = XmlILMethods.ItemsToNode;
				}
			}
			else if (typDst == XmlQueryTypeFactory.NodeSDod || typDst == XmlQueryTypeFactory.NodeNotRtfS)
			{
				if (typSrc == XmlQueryTypeFactory.Item)
				{
					meth = XmlILMethods.ItemToNodes;
				}
				else if (typSrc == XmlQueryTypeFactory.ItemS)
				{
					meth = XmlILMethods.ItemsToNodes;
				}
			}
			else if (typDst == XmlQueryTypeFactory.StringX)
			{
				if (typSrc == XmlQueryTypeFactory.DateTimeX)
				{
					meth = XmlILMethods.DTToStr;
				}
				else if (typSrc == XmlQueryTypeFactory.DoubleX)
				{
					meth = XmlILMethods.DblToStr;
				}
				else if (typSrc == XmlQueryTypeFactory.Item)
				{
					meth = XmlILMethods.ItemToStr;
				}
				else if (typSrc == XmlQueryTypeFactory.ItemS)
				{
					meth = XmlILMethods.ItemsToStr;
				}
			}
			return meth != null;
		}

		// Token: 0x0600433C RID: 17212 RVA: 0x0016EA06 File Offset: 0x0016CC06
		private void SyncToNavigator(LocalBuilder locNav, QilNode ndCtxt)
		{
			this.helper.Emit(OpCodes.Ldloc, locNav);
			this.NestedVisitEnsureStack(ndCtxt);
			this.helper.CallSyncToNavigator();
			this.helper.Emit(OpCodes.Stloc, locNav);
		}

		// Token: 0x0600433D RID: 17213 RVA: 0x0016EA3C File Offset: 0x0016CC3C
		private void CreateSimpleIterator(QilNode ndCtxt, string iterName, Type iterType, MethodInfo methCreate, MethodInfo methNext)
		{
			LocalBuilder localBuilder = this.helper.DeclareLocal(iterName, iterType);
			this.helper.Emit(OpCodes.Ldloca, localBuilder);
			this.NestedVisitEnsureStack(ndCtxt);
			this.helper.Call(methCreate);
			this.GenerateSimpleIterator(typeof(XPathNavigator), localBuilder, methNext);
		}

		// Token: 0x0600433E RID: 17214 RVA: 0x0016EA90 File Offset: 0x0016CC90
		private void CreateFilteredIterator(QilNode ndCtxt, string iterName, Type iterType, MethodInfo methCreate, MethodInfo methNext, XmlNodeKindFlags kinds, QilName ndName, TriState orSelf, QilNode ndEnd)
		{
			LocalBuilder localBuilder = this.helper.DeclareLocal(iterName, iterType);
			this.helper.Emit(OpCodes.Ldloca, localBuilder);
			this.NestedVisitEnsureStack(ndCtxt);
			this.LoadSelectFilter(kinds, ndName);
			if (orSelf != TriState.Unknown)
			{
				this.helper.LoadBoolean(orSelf == TriState.True);
			}
			if (ndEnd != null)
			{
				this.NestedVisitEnsureStack(ndEnd);
			}
			this.helper.Call(methCreate);
			this.GenerateSimpleIterator(typeof(XPathNavigator), localBuilder, methNext);
		}

		// Token: 0x0600433F RID: 17215 RVA: 0x0016EB10 File Offset: 0x0016CD10
		private void CreateContainerIterator(QilUnary ndDod, string iterName, Type iterType, MethodInfo methCreate, MethodInfo methNext, XmlNodeKindFlags kinds, QilName ndName, TriState orSelf)
		{
			LocalBuilder localBuilder = this.helper.DeclareLocal(iterName, iterType);
			QilLoop qilLoop = (QilLoop)ndDod.Child;
			this.helper.Emit(OpCodes.Ldloca, localBuilder);
			this.LoadSelectFilter(kinds, ndName);
			if (orSelf != TriState.Unknown)
			{
				this.helper.LoadBoolean(orSelf == TriState.True);
			}
			this.helper.Call(methCreate);
			Label label = this.helper.DefineLabel();
			this.StartNestedIterator(qilLoop, label);
			this.StartBinding(qilLoop.Variable);
			this.EndBinding(qilLoop.Variable);
			this.EndNestedIterator(qilLoop.Variable);
			this.iterCurr.Storage = this.iterNested.Storage;
			this.GenerateContainerIterator(ndDod, localBuilder, label, methNext, typeof(XPathNavigator));
		}

		// Token: 0x06004340 RID: 17216 RVA: 0x0016EBD8 File Offset: 0x0016CDD8
		private void GenerateSimpleIterator(Type itemStorageType, LocalBuilder locIter, MethodInfo methNext)
		{
			Label label = this.helper.DefineLabel();
			this.helper.MarkLabel(label);
			this.helper.Emit(OpCodes.Ldloca, locIter);
			this.helper.Call(methNext);
			this.helper.Emit(OpCodes.Brfalse, this.iterCurr.GetLabelNext());
			this.iterCurr.SetIterator(label, StorageDescriptor.Current(locIter, itemStorageType));
		}

		// Token: 0x06004341 RID: 17217 RVA: 0x0016EC48 File Offset: 0x0016CE48
		private void GenerateContainerIterator(QilNode nd, LocalBuilder locIter, Label lblOnEndNested, MethodInfo methNext, Type itemStorageType)
		{
			Label label = this.helper.DefineLabel();
			this.iterCurr.EnsureNoStackNoCache(nd.XmlType.IsNode ? "$$$navInput" : "$$$itemInput");
			this.helper.Emit(OpCodes.Ldloca, locIter);
			this.iterCurr.PushValue();
			this.helper.EmitUnconditionalBranch(OpCodes.Br, label);
			this.helper.MarkLabel(lblOnEndNested);
			this.helper.Emit(OpCodes.Ldloca, locIter);
			this.helper.Emit(OpCodes.Ldnull);
			this.helper.MarkLabel(label);
			this.helper.Call(methNext);
			if (nd.XmlType.IsSingleton)
			{
				this.helper.LoadInteger(1);
				this.helper.Emit(OpCodes.Beq, this.iterNested.GetLabelNext());
				this.iterCurr.Storage = StorageDescriptor.Current(locIter, itemStorageType);
				return;
			}
			this.helper.Emit(OpCodes.Switch, new Label[]
			{
				this.iterCurr.GetLabelNext(),
				this.iterNested.GetLabelNext()
			});
			this.iterCurr.SetIterator(lblOnEndNested, StorageDescriptor.Current(locIter, itemStorageType));
		}

		// Token: 0x06004342 RID: 17218 RVA: 0x0016ED90 File Offset: 0x0016CF90
		private GenerateNameType LoadNameAndType(XPathNodeType nodeType, QilNode ndName, bool isStart, bool callChk)
		{
			this.helper.LoadQueryOutput();
			GenerateNameType generateNameType = GenerateNameType.StackName;
			if (ndName.NodeType == QilNodeType.LiteralQName)
			{
				if (isStart || !callChk)
				{
					QilName qilName = ndName as QilName;
					string prefix = qilName.Prefix;
					string localName = qilName.LocalName;
					string namespaceUri = qilName.NamespaceUri;
					if (qilName.NamespaceUri.Length == 0)
					{
						this.helper.Emit(OpCodes.Ldstr, qilName.LocalName);
						return GenerateNameType.LiteralLocalName;
					}
					if (!ValidateNames.ValidateName(prefix, localName, namespaceUri, nodeType, ValidateNames.Flags.CheckPrefixMapping))
					{
						if (isStart)
						{
							this.helper.Emit(OpCodes.Ldstr, localName);
							this.helper.Emit(OpCodes.Ldstr, namespaceUri);
							this.helper.Construct(XmlILConstructors.QName);
							generateNameType = GenerateNameType.QName;
						}
					}
					else
					{
						this.helper.Emit(OpCodes.Ldstr, prefix);
						this.helper.Emit(OpCodes.Ldstr, localName);
						this.helper.Emit(OpCodes.Ldstr, namespaceUri);
						generateNameType = GenerateNameType.LiteralName;
					}
				}
			}
			else if (isStart)
			{
				if (ndName.NodeType == QilNodeType.NameOf)
				{
					this.NestedVisitEnsureStack((ndName as QilUnary).Child);
					generateNameType = GenerateNameType.CopiedName;
				}
				else if (ndName.NodeType == QilNodeType.StrParseQName)
				{
					this.VisitStrParseQName(ndName as QilBinary, true);
					if ((ndName as QilBinary).Right.XmlType.TypeCode == XmlTypeCode.String)
					{
						generateNameType = GenerateNameType.TagNameAndNamespace;
					}
					else
					{
						generateNameType = GenerateNameType.TagNameAndMappings;
					}
				}
				else
				{
					this.NestedVisitEnsureStack(ndName);
					generateNameType = GenerateNameType.QName;
				}
			}
			return generateNameType;
		}

		// Token: 0x06004343 RID: 17219 RVA: 0x0016EEF0 File Offset: 0x0016D0F0
		private bool TryZeroCompare(QilNodeType relOp, QilNode ndFirst, QilNode ndSecond)
		{
			switch (ndFirst.NodeType)
			{
			case QilNodeType.True:
				relOp = ((relOp == QilNodeType.Eq) ? QilNodeType.Ne : QilNodeType.Eq);
				goto IL_0055;
			case QilNodeType.False:
				goto IL_0055;
			case QilNodeType.LiteralInt32:
				if ((QilLiteral)ndFirst != 0)
				{
					return false;
				}
				goto IL_0055;
			case QilNodeType.LiteralInt64:
				if ((QilLiteral)ndFirst != 0)
				{
					return false;
				}
				goto IL_0055;
			}
			return false;
			IL_0055:
			this.NestedVisitEnsureStack(ndSecond);
			this.ZeroCompare(relOp, ndSecond.XmlType.TypeCode == XmlTypeCode.Boolean);
			return true;
		}

		// Token: 0x06004344 RID: 17220 RVA: 0x0016EF70 File Offset: 0x0016D170
		private bool TryNameCompare(QilNodeType relOp, QilNode ndFirst, QilNode ndSecond)
		{
			if (ndFirst.NodeType == QilNodeType.NameOf)
			{
				QilNodeType nodeType = ndSecond.NodeType;
				if (nodeType == QilNodeType.LiteralQName || nodeType == QilNodeType.NameOf)
				{
					this.helper.LoadQueryRuntime();
					this.NestedVisitEnsureStack((ndFirst as QilUnary).Child);
					if (ndSecond.NodeType == QilNodeType.LiteralQName)
					{
						QilName qilName = ndSecond as QilName;
						this.helper.LoadInteger(this.helper.StaticData.DeclareName(qilName.LocalName));
						this.helper.LoadInteger(this.helper.StaticData.DeclareName(qilName.NamespaceUri));
						this.helper.Call(XmlILMethods.QNameEqualLit);
					}
					else
					{
						this.NestedVisitEnsureStack(ndSecond);
						this.helper.Call(XmlILMethods.QNameEqualNav);
					}
					this.ZeroCompare((relOp == QilNodeType.Eq) ? QilNodeType.Ne : QilNodeType.Eq, true);
					return true;
				}
			}
			return false;
		}

		// Token: 0x06004345 RID: 17221 RVA: 0x0016F04C File Offset: 0x0016D24C
		private void ClrCompare(QilNodeType relOp, XmlTypeCode code)
		{
			BranchingContext currentBranchingContext = this.iterCurr.CurrentBranchingContext;
			OpCode opCode;
			if (currentBranchingContext == BranchingContext.OnTrue)
			{
				switch (relOp)
				{
				case QilNodeType.Ne:
					opCode = OpCodes.Bne_Un;
					break;
				case QilNodeType.Eq:
					opCode = OpCodes.Beq;
					break;
				case QilNodeType.Gt:
					opCode = OpCodes.Bgt;
					break;
				case QilNodeType.Ge:
					opCode = OpCodes.Bge;
					break;
				case QilNodeType.Lt:
					opCode = OpCodes.Blt;
					break;
				case QilNodeType.Le:
					opCode = OpCodes.Ble;
					break;
				default:
					opCode = OpCodes.Nop;
					break;
				}
				this.helper.Emit(opCode, this.iterCurr.LabelBranch);
				this.iterCurr.Storage = StorageDescriptor.None();
				return;
			}
			if (currentBranchingContext == BranchingContext.OnFalse)
			{
				if (code == XmlTypeCode.Double || code == XmlTypeCode.Float)
				{
					switch (relOp)
					{
					case QilNodeType.Ne:
						opCode = OpCodes.Beq;
						break;
					case QilNodeType.Eq:
						opCode = OpCodes.Bne_Un;
						break;
					case QilNodeType.Gt:
						opCode = OpCodes.Ble_Un;
						break;
					case QilNodeType.Ge:
						opCode = OpCodes.Blt_Un;
						break;
					case QilNodeType.Lt:
						opCode = OpCodes.Bge_Un;
						break;
					case QilNodeType.Le:
						opCode = OpCodes.Bgt_Un;
						break;
					default:
						opCode = OpCodes.Nop;
						break;
					}
				}
				else
				{
					switch (relOp)
					{
					case QilNodeType.Ne:
						opCode = OpCodes.Beq;
						break;
					case QilNodeType.Eq:
						opCode = OpCodes.Bne_Un;
						break;
					case QilNodeType.Gt:
						opCode = OpCodes.Ble;
						break;
					case QilNodeType.Ge:
						opCode = OpCodes.Blt;
						break;
					case QilNodeType.Lt:
						opCode = OpCodes.Bge;
						break;
					case QilNodeType.Le:
						opCode = OpCodes.Bgt;
						break;
					default:
						opCode = OpCodes.Nop;
						break;
					}
				}
				this.helper.Emit(opCode, this.iterCurr.LabelBranch);
				this.iterCurr.Storage = StorageDescriptor.None();
				return;
			}
			switch (relOp)
			{
			case QilNodeType.Eq:
				this.helper.Emit(OpCodes.Ceq);
				goto IL_022D;
			case QilNodeType.Gt:
				this.helper.Emit(OpCodes.Cgt);
				goto IL_022D;
			case QilNodeType.Lt:
				this.helper.Emit(OpCodes.Clt);
				goto IL_022D;
			}
			if (relOp != QilNodeType.Ne)
			{
				if (relOp != QilNodeType.Ge)
				{
					if (relOp != QilNodeType.Le)
					{
						opCode = OpCodes.Nop;
					}
					else
					{
						opCode = OpCodes.Ble_S;
					}
				}
				else
				{
					opCode = OpCodes.Bge_S;
				}
			}
			else
			{
				opCode = OpCodes.Bne_Un_S;
			}
			Label label = this.helper.DefineLabel();
			this.helper.Emit(opCode, label);
			this.helper.ConvBranchToBool(label, true);
			IL_022D:
			this.iterCurr.Storage = StorageDescriptor.Stack(typeof(bool), false);
		}

		// Token: 0x06004346 RID: 17222 RVA: 0x0016F2A4 File Offset: 0x0016D4A4
		private void ZeroCompare(QilNodeType relOp, bool isBoolVal)
		{
			BranchingContext currentBranchingContext = this.iterCurr.CurrentBranchingContext;
			if (currentBranchingContext == BranchingContext.OnTrue)
			{
				this.helper.Emit((relOp == QilNodeType.Eq) ? OpCodes.Brfalse : OpCodes.Brtrue, this.iterCurr.LabelBranch);
				this.iterCurr.Storage = StorageDescriptor.None();
				return;
			}
			if (currentBranchingContext != BranchingContext.OnFalse)
			{
				if (!isBoolVal || relOp == QilNodeType.Eq)
				{
					Label label = this.helper.DefineLabel();
					this.helper.Emit((relOp == QilNodeType.Eq) ? OpCodes.Brfalse : OpCodes.Brtrue, label);
					this.helper.ConvBranchToBool(label, true);
				}
				this.iterCurr.Storage = StorageDescriptor.Stack(typeof(bool), false);
				return;
			}
			this.helper.Emit((relOp == QilNodeType.Eq) ? OpCodes.Brtrue : OpCodes.Brfalse, this.iterCurr.LabelBranch);
			this.iterCurr.Storage = StorageDescriptor.None();
		}

		// Token: 0x06004347 RID: 17223 RVA: 0x0016F390 File Offset: 0x0016D590
		private void StartWriterLoop(QilNode nd, out bool hasOnEnd, out Label lblOnEnd)
		{
			XmlILConstructInfo xmlILConstructInfo = XmlILConstructInfo.Read(nd);
			hasOnEnd = false;
			lblOnEnd = default(Label);
			if (!xmlILConstructInfo.PushToWriterLast || nd.XmlType.IsSingleton)
			{
				return;
			}
			if (!this.iterCurr.HasLabelNext)
			{
				hasOnEnd = true;
				lblOnEnd = this.helper.DefineLabel();
				this.iterCurr.SetIterator(lblOnEnd, StorageDescriptor.None());
			}
		}

		// Token: 0x06004348 RID: 17224 RVA: 0x0016F3F9 File Offset: 0x0016D5F9
		private void EndWriterLoop(QilNode nd, bool hasOnEnd, Label lblOnEnd)
		{
			if (!XmlILConstructInfo.Read(nd).PushToWriterLast)
			{
				return;
			}
			this.iterCurr.Storage = StorageDescriptor.None();
			if (nd.XmlType.IsSingleton)
			{
				return;
			}
			if (hasOnEnd)
			{
				this.iterCurr.LoopToEnd(lblOnEnd);
			}
		}

		// Token: 0x06004349 RID: 17225 RVA: 0x0016F436 File Offset: 0x0016D636
		private bool MightHaveNamespacesAfterAttributes(XmlILConstructInfo info)
		{
			if (info != null)
			{
				info = info.ParentElementInfo;
			}
			return info == null || info.MightHaveNamespacesAfterAttributes;
		}

		// Token: 0x0600434A RID: 17226 RVA: 0x0016F44E File Offset: 0x0016D64E
		private bool ElementCachesAttributes(XmlILConstructInfo info)
		{
			return info.MightHaveDuplicateAttributes || info.MightHaveNamespacesAfterAttributes;
		}

		// Token: 0x0600434B RID: 17227 RVA: 0x0016F460 File Offset: 0x0016D660
		private void BeforeStartChecks(QilNode ndCtor)
		{
			PossibleXmlStates initialStates = XmlILConstructInfo.Read(ndCtor).InitialStates;
			if (initialStates == PossibleXmlStates.WithinSequence)
			{
				this.helper.CallStartTree(this.QilConstructorToNodeType(ndCtor.NodeType));
				return;
			}
			if (initialStates != PossibleXmlStates.EnumAttrs)
			{
				return;
			}
			QilNodeType nodeType = ndCtor.NodeType;
			if (nodeType == QilNodeType.ElementCtor || nodeType - QilNodeType.CommentCtor <= 3)
			{
				this.helper.CallStartElementContent();
			}
		}

		// Token: 0x0600434C RID: 17228 RVA: 0x0016F4B8 File Offset: 0x0016D6B8
		private void AfterEndChecks(QilNode ndCtor)
		{
			if (XmlILConstructInfo.Read(ndCtor).FinalStates == PossibleXmlStates.WithinSequence)
			{
				this.helper.CallEndTree();
			}
		}

		// Token: 0x0600434D RID: 17229 RVA: 0x0016F4D4 File Offset: 0x0016D6D4
		private bool CheckWithinContent(XmlILConstructInfo info)
		{
			PossibleXmlStates initialStates = info.InitialStates;
			return initialStates - PossibleXmlStates.WithinSequence > 2;
		}

		// Token: 0x0600434E RID: 17230 RVA: 0x0016F4F4 File Offset: 0x0016D6F4
		private bool CheckEnumAttrs(XmlILConstructInfo info)
		{
			PossibleXmlStates initialStates = info.InitialStates;
			return initialStates - PossibleXmlStates.WithinSequence > 1;
		}

		// Token: 0x0600434F RID: 17231 RVA: 0x0016F511 File Offset: 0x0016D711
		private XPathNodeType QilXmlToXPathNodeType(XmlNodeKindFlags xmlTypes)
		{
			if (xmlTypes <= XmlNodeKindFlags.Attribute)
			{
				if (xmlTypes == XmlNodeKindFlags.Element)
				{
					return XPathNodeType.Element;
				}
				if (xmlTypes == XmlNodeKindFlags.Attribute)
				{
					return XPathNodeType.Attribute;
				}
			}
			else
			{
				if (xmlTypes == XmlNodeKindFlags.Text)
				{
					return XPathNodeType.Text;
				}
				if (xmlTypes == XmlNodeKindFlags.Comment)
				{
					return XPathNodeType.Comment;
				}
			}
			return XPathNodeType.ProcessingInstruction;
		}

		// Token: 0x06004350 RID: 17232 RVA: 0x0016F535 File Offset: 0x0016D735
		private XPathNodeType QilConstructorToNodeType(QilNodeType typ)
		{
			switch (typ)
			{
			case QilNodeType.ElementCtor:
				return XPathNodeType.Element;
			case QilNodeType.AttributeCtor:
				return XPathNodeType.Attribute;
			case QilNodeType.CommentCtor:
				return XPathNodeType.Comment;
			case QilNodeType.PICtor:
				return XPathNodeType.ProcessingInstruction;
			case QilNodeType.TextCtor:
				return XPathNodeType.Text;
			case QilNodeType.RawTextCtor:
				return XPathNodeType.Text;
			case QilNodeType.DocumentCtor:
				return XPathNodeType.Root;
			case QilNodeType.NamespaceDecl:
				return XPathNodeType.Namespace;
			default:
				return XPathNodeType.All;
			}
		}

		// Token: 0x06004351 RID: 17233 RVA: 0x0016F574 File Offset: 0x0016D774
		private void LoadSelectFilter(XmlNodeKindFlags xmlTypes, QilName ndName)
		{
			if (ndName != null)
			{
				this.helper.CallGetNameFilter(this.helper.StaticData.DeclareNameFilter(ndName.LocalName, ndName.NamespaceUri));
				return;
			}
			if (!XmlILVisitor.IsNodeTypeUnion(xmlTypes))
			{
				this.helper.CallGetTypeFilter(this.QilXmlToXPathNodeType(xmlTypes));
				return;
			}
			if ((xmlTypes & XmlNodeKindFlags.Attribute) != XmlNodeKindFlags.None)
			{
				this.helper.CallGetTypeFilter(XPathNodeType.All);
				return;
			}
			this.helper.CallGetTypeFilter(XPathNodeType.Attribute);
		}

		// Token: 0x06004352 RID: 17234 RVA: 0x000161BF File Offset: 0x000143BF
		private static bool IsNodeTypeUnion(XmlNodeKindFlags xmlTypes)
		{
			return (xmlTypes & (xmlTypes - 1)) > XmlNodeKindFlags.None;
		}

		// Token: 0x06004353 RID: 17235 RVA: 0x0016F5EC File Offset: 0x0016D7EC
		private void StartNestedIterator(QilNode nd)
		{
			IteratorDescriptor iteratorDescriptor = this.iterCurr;
			if (iteratorDescriptor == null)
			{
				this.iterCurr = new IteratorDescriptor(this.helper);
			}
			else
			{
				this.iterCurr = new IteratorDescriptor(iteratorDescriptor);
			}
			this.iterNested = null;
		}

		// Token: 0x06004354 RID: 17236 RVA: 0x0016F629 File Offset: 0x0016D829
		private void StartNestedIterator(QilNode nd, Label lblOnEnd)
		{
			this.StartNestedIterator(nd);
			this.iterCurr.SetIterator(lblOnEnd, StorageDescriptor.None());
		}

		// Token: 0x06004355 RID: 17237 RVA: 0x0016F644 File Offset: 0x0016D844
		private void EndNestedIterator(QilNode nd)
		{
			if (this.iterCurr.IsBranching && this.iterCurr.Storage.Location != ItemLocation.None)
			{
				this.iterCurr.EnsureItemStorageType(nd.XmlType, typeof(bool));
				this.iterCurr.EnsureStackNoCache();
				if (this.iterCurr.CurrentBranchingContext == BranchingContext.OnTrue)
				{
					this.helper.Emit(OpCodes.Brtrue, this.iterCurr.LabelBranch);
				}
				else
				{
					this.helper.Emit(OpCodes.Brfalse, this.iterCurr.LabelBranch);
				}
				this.iterCurr.Storage = StorageDescriptor.None();
			}
			this.iterNested = this.iterCurr;
			this.iterCurr = this.iterCurr.ParentIterator;
		}

		// Token: 0x06004356 RID: 17238 RVA: 0x0016F710 File Offset: 0x0016D910
		private void NestedVisit(QilNode nd, Type itemStorageType, bool isCached)
		{
			if (XmlILConstructInfo.Read(nd).PushToWriterLast)
			{
				this.StartNestedIterator(nd);
				this.Visit(nd);
				this.EndNestedIterator(nd);
				this.iterCurr.Storage = StorageDescriptor.None();
				return;
			}
			if (!isCached && nd.XmlType.IsSingleton)
			{
				this.StartNestedIterator(nd);
				this.Visit(nd);
				this.iterCurr.EnsureNoCache();
				this.iterCurr.EnsureItemStorageType(nd.XmlType, itemStorageType);
				this.EndNestedIterator(nd);
				this.iterCurr.Storage = this.iterNested.Storage;
				return;
			}
			this.NestedVisitEnsureCache(nd, itemStorageType);
		}

		// Token: 0x06004357 RID: 17239 RVA: 0x0016F7B3 File Offset: 0x0016D9B3
		private void NestedVisit(QilNode nd)
		{
			this.NestedVisit(nd, this.GetItemStorageType(nd), !nd.XmlType.IsSingleton);
		}

		// Token: 0x06004358 RID: 17240 RVA: 0x0016F7D4 File Offset: 0x0016D9D4
		private void NestedVisit(QilNode nd, Label lblOnEnd)
		{
			this.StartNestedIterator(nd, lblOnEnd);
			this.Visit(nd);
			this.iterCurr.EnsureNoCache();
			this.iterCurr.EnsureItemStorageType(nd.XmlType, this.GetItemStorageType(nd));
			this.EndNestedIterator(nd);
			this.iterCurr.Storage = this.iterNested.Storage;
		}

		// Token: 0x06004359 RID: 17241 RVA: 0x0016F831 File Offset: 0x0016DA31
		private void NestedVisitEnsureStack(QilNode nd)
		{
			this.NestedVisit(nd);
			this.iterCurr.EnsureStack();
		}

		// Token: 0x0600435A RID: 17242 RVA: 0x0016F845 File Offset: 0x0016DA45
		private void NestedVisitEnsureStack(QilNode ndLeft, QilNode ndRight)
		{
			this.NestedVisitEnsureStack(ndLeft);
			this.NestedVisitEnsureStack(ndRight);
		}

		// Token: 0x0600435B RID: 17243 RVA: 0x0016F855 File Offset: 0x0016DA55
		private void NestedVisitEnsureStack(QilNode nd, Type itemStorageType, bool isCached)
		{
			this.NestedVisit(nd, itemStorageType, isCached);
			this.iterCurr.EnsureStack();
		}

		// Token: 0x0600435C RID: 17244 RVA: 0x0016F86B File Offset: 0x0016DA6B
		private void NestedVisitEnsureLocal(QilNode nd, LocalBuilder loc)
		{
			this.NestedVisit(nd);
			this.iterCurr.EnsureLocal(loc);
		}

		// Token: 0x0600435D RID: 17245 RVA: 0x0016F880 File Offset: 0x0016DA80
		private void NestedVisitWithBranch(QilNode nd, BranchingContext brctxt, Label lblBranch)
		{
			this.StartNestedIterator(nd);
			this.iterCurr.SetBranching(brctxt, lblBranch);
			this.Visit(nd);
			this.EndNestedIterator(nd);
			this.iterCurr.Storage = StorageDescriptor.None();
		}

		// Token: 0x0600435E RID: 17246 RVA: 0x0016F8B8 File Offset: 0x0016DAB8
		private void NestedVisitEnsureCache(QilNode nd, Type itemStorageType)
		{
			bool flag = this.CachesResult(nd);
			Label label = this.helper.DefineLabel();
			if (flag)
			{
				this.StartNestedIterator(nd);
				this.Visit(nd);
				this.EndNestedIterator(nd);
				this.iterCurr.Storage = this.iterNested.Storage;
				if (this.iterCurr.Storage.ItemStorageType == itemStorageType)
				{
					return;
				}
				if (this.iterCurr.Storage.ItemStorageType == typeof(XPathNavigator) || itemStorageType == typeof(XPathNavigator))
				{
					this.iterCurr.EnsureItemStorageType(nd.XmlType, itemStorageType);
					return;
				}
				this.iterCurr.EnsureNoStack("$$$cacheResult");
			}
			Type type = ((this.GetItemStorageType(nd) == typeof(XPathNavigator)) ? typeof(XPathNavigator) : itemStorageType);
			XmlILStorageMethods xmlILStorageMethods = XmlILMethods.StorageMethods[type];
			LocalBuilder localBuilder = this.helper.DeclareLocal("$$$cache", xmlILStorageMethods.SeqType);
			this.helper.Emit(OpCodes.Ldloc, localBuilder);
			if (nd.XmlType.IsSingleton)
			{
				this.NestedVisitEnsureStack(nd, type, false);
				this.helper.CallToken(xmlILStorageMethods.SeqReuseSgl);
				this.helper.Emit(OpCodes.Stloc, localBuilder);
			}
			else
			{
				this.helper.CallToken(xmlILStorageMethods.SeqReuse);
				this.helper.Emit(OpCodes.Stloc, localBuilder);
				this.helper.Emit(OpCodes.Ldloc, localBuilder);
				this.StartNestedIterator(nd, label);
				if (flag)
				{
					this.iterCurr.Storage = this.iterCurr.ParentIterator.Storage;
				}
				else
				{
					this.Visit(nd);
				}
				this.iterCurr.EnsureItemStorageType(nd.XmlType, type);
				this.iterCurr.EnsureStackNoCache();
				this.helper.Call(xmlILStorageMethods.SeqAdd);
				this.helper.Emit(OpCodes.Ldloc, localBuilder);
				this.iterCurr.LoopToEnd(label);
				this.EndNestedIterator(nd);
				this.helper.Emit(OpCodes.Pop);
			}
			this.iterCurr.Storage = StorageDescriptor.Local(localBuilder, itemStorageType, true);
		}

		// Token: 0x0600435F RID: 17247 RVA: 0x0016FAF8 File Offset: 0x0016DCF8
		private bool CachesResult(QilNode nd)
		{
			QilNodeType nodeType = nd.NodeType;
			if (nodeType <= QilNodeType.DocOrderDistinct)
			{
				if (nodeType - QilNodeType.Let > 1)
				{
					OptimizerPatterns optimizerPatterns;
					if (nodeType == QilNodeType.Filter)
					{
						optimizerPatterns = OptimizerPatterns.Read(nd);
						return optimizerPatterns.MatchesPattern(OptimizerPatternName.EqualityIndex);
					}
					if (nodeType != QilNodeType.DocOrderDistinct)
					{
						return false;
					}
					if (nd.XmlType.IsSingleton)
					{
						return false;
					}
					optimizerPatterns = OptimizerPatterns.Read(nd);
					return !optimizerPatterns.MatchesPattern(OptimizerPatternName.JoinAndDod) && !optimizerPatterns.MatchesPattern(OptimizerPatternName.DodReverse);
				}
			}
			else if (nodeType != QilNodeType.Invoke)
			{
				if (nodeType == QilNodeType.TypeAssert)
				{
					QilTargetType qilTargetType = (QilTargetType)nd;
					return this.CachesResult(qilTargetType.Source) && this.GetItemStorageType(qilTargetType.Source) == this.GetItemStorageType(qilTargetType);
				}
				if (nodeType - QilNodeType.XsltInvokeLateBound > 1)
				{
					return false;
				}
			}
			return !nd.XmlType.IsSingleton;
		}

		// Token: 0x06004360 RID: 17248 RVA: 0x0016FBB3 File Offset: 0x0016DDB3
		private Type GetStorageType(QilNode nd)
		{
			return XmlILTypeHelper.GetStorageType(nd.XmlType);
		}

		// Token: 0x06004361 RID: 17249 RVA: 0x0016FBC0 File Offset: 0x0016DDC0
		private Type GetStorageType(XmlQueryType typ)
		{
			return XmlILTypeHelper.GetStorageType(typ);
		}

		// Token: 0x06004362 RID: 17250 RVA: 0x0016FBC8 File Offset: 0x0016DDC8
		private Type GetItemStorageType(QilNode nd)
		{
			return XmlILTypeHelper.GetStorageType(nd.XmlType.Prime);
		}

		// Token: 0x06004363 RID: 17251 RVA: 0x0016FBDA File Offset: 0x0016DDDA
		private Type GetItemStorageType(XmlQueryType typ)
		{
			return XmlILTypeHelper.GetStorageType(typ.Prime);
		}

		// Token: 0x04002B1D RID: 11037
		private QilExpression qil;

		// Token: 0x04002B1E RID: 11038
		private GenerateHelper helper;

		// Token: 0x04002B1F RID: 11039
		private IteratorDescriptor iterCurr;

		// Token: 0x04002B20 RID: 11040
		private IteratorDescriptor iterNested;

		// Token: 0x04002B21 RID: 11041
		private int indexId;
	}
}
