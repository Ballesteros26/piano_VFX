using System;
using System.Xml.Xsl.Qil;

namespace System.Xml.Xsl.IlGen
{
	// Token: 0x02000661 RID: 1633
	internal class OptimizerPatterns : IQilAnnotation
	{
		// Token: 0x060041C3 RID: 16835 RVA: 0x0015F860 File Offset: 0x0015DA60
		public static OptimizerPatterns Read(QilNode nd)
		{
			XmlILAnnotation xmlILAnnotation = nd.Annotation as XmlILAnnotation;
			OptimizerPatterns optimizerPatterns = ((xmlILAnnotation != null) ? xmlILAnnotation.Patterns : null);
			if (optimizerPatterns == null)
			{
				if (!nd.XmlType.MaybeMany)
				{
					if (OptimizerPatterns.ZeroOrOneDefault == null)
					{
						optimizerPatterns = new OptimizerPatterns();
						optimizerPatterns.AddPattern(OptimizerPatternName.IsDocOrderDistinct);
						optimizerPatterns.AddPattern(OptimizerPatternName.SameDepth);
						optimizerPatterns.isReadOnly = true;
						OptimizerPatterns.ZeroOrOneDefault = optimizerPatterns;
					}
					else
					{
						optimizerPatterns = OptimizerPatterns.ZeroOrOneDefault;
					}
				}
				else if (nd.XmlType.IsDod)
				{
					if (OptimizerPatterns.DodDefault == null)
					{
						optimizerPatterns = new OptimizerPatterns();
						optimizerPatterns.AddPattern(OptimizerPatternName.IsDocOrderDistinct);
						optimizerPatterns.isReadOnly = true;
						OptimizerPatterns.DodDefault = optimizerPatterns;
					}
					else
					{
						optimizerPatterns = OptimizerPatterns.DodDefault;
					}
				}
				else if (OptimizerPatterns.MaybeManyDefault == null)
				{
					optimizerPatterns = new OptimizerPatterns();
					optimizerPatterns.isReadOnly = true;
					OptimizerPatterns.MaybeManyDefault = optimizerPatterns;
				}
				else
				{
					optimizerPatterns = OptimizerPatterns.MaybeManyDefault;
				}
			}
			return optimizerPatterns;
		}

		// Token: 0x060041C4 RID: 16836 RVA: 0x0015F93C File Offset: 0x0015DB3C
		public static OptimizerPatterns Write(QilNode nd)
		{
			XmlILAnnotation xmlILAnnotation = XmlILAnnotation.Write(nd);
			OptimizerPatterns optimizerPatterns = xmlILAnnotation.Patterns;
			if (optimizerPatterns == null || optimizerPatterns.isReadOnly)
			{
				optimizerPatterns = new OptimizerPatterns();
				xmlILAnnotation.Patterns = optimizerPatterns;
				if (!nd.XmlType.MaybeMany)
				{
					optimizerPatterns.AddPattern(OptimizerPatternName.IsDocOrderDistinct);
					optimizerPatterns.AddPattern(OptimizerPatternName.SameDepth);
				}
				else if (nd.XmlType.IsDod)
				{
					optimizerPatterns.AddPattern(OptimizerPatternName.IsDocOrderDistinct);
				}
			}
			return optimizerPatterns;
		}

		// Token: 0x060041C5 RID: 16837 RVA: 0x0015F9A4 File Offset: 0x0015DBA4
		public static void Inherit(QilNode ndSrc, QilNode ndDst, OptimizerPatternName pattern)
		{
			OptimizerPatterns optimizerPatterns = OptimizerPatterns.Read(ndSrc);
			if (optimizerPatterns.MatchesPattern(pattern))
			{
				OptimizerPatterns optimizerPatterns2 = OptimizerPatterns.Write(ndDst);
				optimizerPatterns2.AddPattern(pattern);
				switch (pattern)
				{
				case OptimizerPatternName.DodReverse:
				case OptimizerPatternName.JoinAndDod:
					optimizerPatterns2.AddArgument(OptimizerPatternArgument.ElementQName, optimizerPatterns.GetArgument(OptimizerPatternArgument.ElementQName));
					return;
				case OptimizerPatternName.EqualityIndex:
					optimizerPatterns2.AddArgument(OptimizerPatternArgument.StepNode, optimizerPatterns.GetArgument(OptimizerPatternArgument.StepNode));
					optimizerPatterns2.AddArgument(OptimizerPatternArgument.StepInput, optimizerPatterns.GetArgument(OptimizerPatternArgument.StepInput));
					return;
				case OptimizerPatternName.FilterAttributeKind:
				case OptimizerPatternName.IsDocOrderDistinct:
				case OptimizerPatternName.IsPositional:
				case OptimizerPatternName.SameDepth:
					break;
				case OptimizerPatternName.FilterContentKind:
					optimizerPatterns2.AddArgument(OptimizerPatternArgument.ElementQName, optimizerPatterns.GetArgument(OptimizerPatternArgument.ElementQName));
					return;
				case OptimizerPatternName.FilterElements:
					optimizerPatterns2.AddArgument(OptimizerPatternArgument.ElementQName, optimizerPatterns.GetArgument(OptimizerPatternArgument.ElementQName));
					return;
				case OptimizerPatternName.MaxPosition:
					optimizerPatterns2.AddArgument(OptimizerPatternArgument.ElementQName, optimizerPatterns.GetArgument(OptimizerPatternArgument.ElementQName));
					return;
				case OptimizerPatternName.Step:
					optimizerPatterns2.AddArgument(OptimizerPatternArgument.StepNode, optimizerPatterns.GetArgument(OptimizerPatternArgument.StepNode));
					optimizerPatterns2.AddArgument(OptimizerPatternArgument.StepInput, optimizerPatterns.GetArgument(OptimizerPatternArgument.StepInput));
					return;
				case OptimizerPatternName.SingleTextRtf:
					optimizerPatterns2.AddArgument(OptimizerPatternArgument.ElementQName, optimizerPatterns.GetArgument(OptimizerPatternArgument.ElementQName));
					break;
				default:
					return;
				}
			}
		}

		// Token: 0x060041C6 RID: 16838 RVA: 0x0015FA90 File Offset: 0x0015DC90
		public void AddArgument(OptimizerPatternArgument argId, object arg)
		{
			switch (argId)
			{
			case OptimizerPatternArgument.StepNode:
				this.arg0 = arg;
				return;
			case OptimizerPatternArgument.StepInput:
				this.arg1 = arg;
				return;
			case OptimizerPatternArgument.ElementQName:
				this.arg2 = arg;
				return;
			default:
				return;
			}
		}

		// Token: 0x060041C7 RID: 16839 RVA: 0x0015FACC File Offset: 0x0015DCCC
		public object GetArgument(OptimizerPatternArgument argNum)
		{
			object obj = null;
			switch (argNum)
			{
			case OptimizerPatternArgument.StepNode:
				obj = this.arg0;
				break;
			case OptimizerPatternArgument.StepInput:
				obj = this.arg1;
				break;
			case OptimizerPatternArgument.ElementQName:
				obj = this.arg2;
				break;
			}
			return obj;
		}

		// Token: 0x060041C8 RID: 16840 RVA: 0x0015FB0B File Offset: 0x0015DD0B
		public void AddPattern(OptimizerPatternName pattern)
		{
			this.patterns |= 1 << (int)pattern;
		}

		// Token: 0x060041C9 RID: 16841 RVA: 0x0015FB20 File Offset: 0x0015DD20
		public bool MatchesPattern(OptimizerPatternName pattern)
		{
			return (this.patterns & (1 << (int)pattern)) != 0;
		}

		// Token: 0x17000CCF RID: 3279
		// (get) Token: 0x060041CA RID: 16842 RVA: 0x0015FB32 File Offset: 0x0015DD32
		public virtual string Name
		{
			get
			{
				return "Patterns";
			}
		}

		// Token: 0x060041CB RID: 16843 RVA: 0x0015FB3C File Offset: 0x0015DD3C
		public override string ToString()
		{
			string text = "";
			for (int i = 0; i < OptimizerPatterns.PatternCount; i++)
			{
				if (this.MatchesPattern((OptimizerPatternName)i))
				{
					if (text.Length != 0)
					{
						text += ", ";
					}
					string text2 = text;
					OptimizerPatternName optimizerPatternName = (OptimizerPatternName)i;
					text = text2 + optimizerPatternName.ToString();
				}
			}
			return text;
		}

		// Token: 0x04002A2D RID: 10797
		private static readonly int PatternCount = Enum.GetValues(typeof(OptimizerPatternName)).Length;

		// Token: 0x04002A2E RID: 10798
		private int patterns;

		// Token: 0x04002A2F RID: 10799
		private bool isReadOnly;

		// Token: 0x04002A30 RID: 10800
		private object arg0;

		// Token: 0x04002A31 RID: 10801
		private object arg1;

		// Token: 0x04002A32 RID: 10802
		private object arg2;

		// Token: 0x04002A33 RID: 10803
		private static volatile OptimizerPatterns ZeroOrOneDefault;

		// Token: 0x04002A34 RID: 10804
		private static volatile OptimizerPatterns MaybeManyDefault;

		// Token: 0x04002A35 RID: 10805
		private static volatile OptimizerPatterns DodDefault;
	}
}
