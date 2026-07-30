using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace System.Xml.Xsl.Qil
{
	// Token: 0x0200064E RID: 1614
	internal class QilXmlWriter : QilScopedVisitor
	{
		// Token: 0x0600410D RID: 16653 RVA: 0x0015B545 File Offset: 0x00159745
		public QilXmlWriter(XmlWriter writer)
			: this(writer, QilXmlWriter.Options.Annotations | QilXmlWriter.Options.TypeInfo | QilXmlWriter.Options.LineInfo | QilXmlWriter.Options.NodeIdentity | QilXmlWriter.Options.NodeLocation)
		{
		}

		// Token: 0x0600410E RID: 16654 RVA: 0x0015B550 File Offset: 0x00159750
		public QilXmlWriter(XmlWriter writer, QilXmlWriter.Options options)
		{
			this.writer = writer;
			this.ngen = new QilXmlWriter.NameGenerator();
			this.options = options;
		}

		// Token: 0x0600410F RID: 16655 RVA: 0x0015B571 File Offset: 0x00159771
		public void ToXml(QilNode node)
		{
			this.VisitAssumeReference(node);
		}

		// Token: 0x06004110 RID: 16656 RVA: 0x0015B57C File Offset: 0x0015977C
		protected virtual void WriteAnnotations(object ann)
		{
			string text = null;
			string text2 = null;
			if (ann == null)
			{
				return;
			}
			if (ann is string)
			{
				text = ann as string;
			}
			else if (ann is IQilAnnotation)
			{
				text2 = (ann as IQilAnnotation).Name;
				text = ann.ToString();
			}
			else if (ann is IList<object>)
			{
				foreach (object obj in ((IList<object>)ann))
				{
					this.WriteAnnotations(obj);
				}
				return;
			}
			if (text != null && text.Length != 0)
			{
				this.writer.WriteComment((text2 != null && text2.Length != 0) ? (text2 + ": " + text) : text);
			}
		}

		// Token: 0x06004111 RID: 16657 RVA: 0x0015B638 File Offset: 0x00159838
		protected virtual void WriteLineInfo(QilNode node)
		{
			this.writer.WriteAttributeString("lineInfo", string.Format(CultureInfo.InvariantCulture, "[{0},{1} -- {2},{3}]", new object[]
			{
				node.SourceLine.Start.Line,
				node.SourceLine.Start.Pos,
				node.SourceLine.End.Line,
				node.SourceLine.End.Pos
			}));
		}

		// Token: 0x06004112 RID: 16658 RVA: 0x0015B6D6 File Offset: 0x001598D6
		protected virtual void WriteXmlType(QilNode node)
		{
			this.writer.WriteAttributeString("xmlType", node.XmlType.ToString(((this.options & QilXmlWriter.Options.RoundTripTypeInfo) != QilXmlWriter.Options.None) ? "S" : "G"));
		}

		// Token: 0x06004113 RID: 16659 RVA: 0x0015B70C File Offset: 0x0015990C
		protected override QilNode VisitChildren(QilNode node)
		{
			if (node is QilLiteral)
			{
				this.writer.WriteValue(Convert.ToString(((QilLiteral)node).Value, CultureInfo.InvariantCulture));
				return node;
			}
			if (node is QilReference)
			{
				QilReference qilReference = (QilReference)node;
				this.writer.WriteAttributeString("id", this.ngen.NameOf(node));
				if (qilReference.DebugName != null)
				{
					this.writer.WriteAttributeString("name", qilReference.DebugName.ToString());
				}
				if (node.NodeType == QilNodeType.Parameter)
				{
					QilParameter qilParameter = (QilParameter)node;
					if (qilParameter.DefaultValue != null)
					{
						this.VisitAssumeReference(qilParameter.DefaultValue);
					}
					return node;
				}
			}
			return base.VisitChildren(node);
		}

		// Token: 0x06004114 RID: 16660 RVA: 0x0015B7C0 File Offset: 0x001599C0
		protected override QilNode VisitReference(QilNode node)
		{
			QilReference qilReference = (QilReference)node;
			string text = this.ngen.NameOf(node);
			if (text == null)
			{
				text = "OUT-OF-SCOPE REFERENCE";
			}
			this.writer.WriteStartElement("RefTo");
			this.writer.WriteAttributeString("id", text);
			if (qilReference.DebugName != null)
			{
				this.writer.WriteAttributeString("name", qilReference.DebugName.ToString());
			}
			this.writer.WriteEndElement();
			return node;
		}

		// Token: 0x06004115 RID: 16661 RVA: 0x0015B83C File Offset: 0x00159A3C
		protected override QilNode VisitQilExpression(QilExpression qil)
		{
			IList<QilNode> list = new QilXmlWriter.ForwardRefFinder().Find(qil);
			if (list != null && list.Count > 0)
			{
				this.writer.WriteStartElement("ForwardDecls");
				foreach (QilNode qilNode in list)
				{
					this.writer.WriteStartElement(Enum.GetName(typeof(QilNodeType), qilNode.NodeType));
					this.writer.WriteAttributeString("id", this.ngen.NameOf(qilNode));
					this.WriteXmlType(qilNode);
					if (qilNode.NodeType == QilNodeType.Function)
					{
						this.Visit(qilNode[0]);
						this.Visit(qilNode[2]);
					}
					this.writer.WriteEndElement();
				}
				this.writer.WriteEndElement();
			}
			return this.VisitChildren(qil);
		}

		// Token: 0x06004116 RID: 16662 RVA: 0x0015B93C File Offset: 0x00159B3C
		protected override QilNode VisitLiteralType(QilLiteral value)
		{
			this.writer.WriteString(value.ToString(((this.options & QilXmlWriter.Options.TypeInfo) != QilXmlWriter.Options.None) ? "G" : "S"));
			return value;
		}

		// Token: 0x06004117 RID: 16663 RVA: 0x0015B96B File Offset: 0x00159B6B
		protected override QilNode VisitLiteralQName(QilName value)
		{
			this.writer.WriteAttributeString("name", value.ToString());
			return value;
		}

		// Token: 0x06004118 RID: 16664 RVA: 0x0015B984 File Offset: 0x00159B84
		protected override void BeginScope(QilNode node)
		{
			this.ngen.NameOf(node);
		}

		// Token: 0x06004119 RID: 16665 RVA: 0x0015B993 File Offset: 0x00159B93
		protected override void EndScope(QilNode node)
		{
			this.ngen.ClearName(node);
		}

		// Token: 0x0600411A RID: 16666 RVA: 0x0015B9A4 File Offset: 0x00159BA4
		protected override void BeforeVisit(QilNode node)
		{
			base.BeforeVisit(node);
			if ((this.options & QilXmlWriter.Options.Annotations) != QilXmlWriter.Options.None)
			{
				this.WriteAnnotations(node.Annotation);
			}
			this.writer.WriteStartElement("", Enum.GetName(typeof(QilNodeType), node.NodeType), "");
			if ((this.options & (QilXmlWriter.Options.TypeInfo | QilXmlWriter.Options.RoundTripTypeInfo)) != QilXmlWriter.Options.None)
			{
				this.WriteXmlType(node);
			}
			if ((this.options & QilXmlWriter.Options.LineInfo) != QilXmlWriter.Options.None && node.SourceLine != null)
			{
				this.WriteLineInfo(node);
			}
		}

		// Token: 0x0600411B RID: 16667 RVA: 0x0015BA27 File Offset: 0x00159C27
		protected override void AfterVisit(QilNode node)
		{
			this.writer.WriteEndElement();
			base.AfterVisit(node);
		}

		// Token: 0x040028DD RID: 10461
		protected XmlWriter writer;

		// Token: 0x040028DE RID: 10462
		protected QilXmlWriter.Options options;

		// Token: 0x040028DF RID: 10463
		private QilXmlWriter.NameGenerator ngen;

		// Token: 0x0200064F RID: 1615
		[Flags]
		public enum Options
		{
			// Token: 0x040028E1 RID: 10465
			None = 0,
			// Token: 0x040028E2 RID: 10466
			Annotations = 1,
			// Token: 0x040028E3 RID: 10467
			TypeInfo = 2,
			// Token: 0x040028E4 RID: 10468
			RoundTripTypeInfo = 4,
			// Token: 0x040028E5 RID: 10469
			LineInfo = 8,
			// Token: 0x040028E6 RID: 10470
			NodeIdentity = 16,
			// Token: 0x040028E7 RID: 10471
			NodeLocation = 32
		}

		// Token: 0x02000650 RID: 1616
		internal class ForwardRefFinder : QilVisitor
		{
			// Token: 0x0600411C RID: 16668 RVA: 0x0015BA3B File Offset: 0x00159C3B
			public IList<QilNode> Find(QilExpression qil)
			{
				this.Visit(qil);
				return this.fwdrefs;
			}

			// Token: 0x0600411D RID: 16669 RVA: 0x0015BA4B File Offset: 0x00159C4B
			protected override QilNode Visit(QilNode node)
			{
				if (node is QilIterator || node is QilFunction)
				{
					this.backrefs.Add(node);
				}
				return base.Visit(node);
			}

			// Token: 0x0600411E RID: 16670 RVA: 0x0015BA70 File Offset: 0x00159C70
			protected override QilNode VisitReference(QilNode node)
			{
				if (!this.backrefs.Contains(node) && !this.fwdrefs.Contains(node))
				{
					this.fwdrefs.Add(node);
				}
				return node;
			}

			// Token: 0x040028E8 RID: 10472
			private List<QilNode> fwdrefs = new List<QilNode>();

			// Token: 0x040028E9 RID: 10473
			private List<QilNode> backrefs = new List<QilNode>();
		}

		// Token: 0x02000651 RID: 1617
		private sealed class NameGenerator
		{
			// Token: 0x06004120 RID: 16672 RVA: 0x0015BABC File Offset: 0x00159CBC
			public NameGenerator()
			{
				string text = "$";
				this.len = (this.zero = text.Length);
				this.start = 'a';
				this.end = 'z';
				this.name = new StringBuilder(text, this.len + 2);
				this.name.Append(this.start);
			}

			// Token: 0x06004121 RID: 16673 RVA: 0x0015BB20 File Offset: 0x00159D20
			public string NextName()
			{
				string text = this.name.ToString();
				char c = this.name[this.len];
				if (c == this.end)
				{
					this.name[this.len] = this.start;
					int num = this.len;
					while (num-- > this.zero && this.name[num] == this.end)
					{
						this.name[num] = this.start;
					}
					if (num < this.zero)
					{
						this.len++;
						this.name.Append(this.start);
					}
					else
					{
						StringBuilder stringBuilder = this.name;
						int num2 = num;
						char c2 = stringBuilder[num2];
						stringBuilder[num2] = c2 + '\u0001';
					}
				}
				else
				{
					this.name[this.len] = c + '\u0001';
				}
				return text;
			}

			// Token: 0x06004122 RID: 16674 RVA: 0x0015BC0C File Offset: 0x00159E0C
			public string NameOf(QilNode n)
			{
				object annotation = n.Annotation;
				QilXmlWriter.NameGenerator.NameAnnotation nameAnnotation = annotation as QilXmlWriter.NameGenerator.NameAnnotation;
				string text;
				if (nameAnnotation == null)
				{
					text = this.NextName();
					n.Annotation = new QilXmlWriter.NameGenerator.NameAnnotation(text, annotation);
				}
				else
				{
					text = nameAnnotation.Name;
				}
				return text;
			}

			// Token: 0x06004123 RID: 16675 RVA: 0x0015BC4A File Offset: 0x00159E4A
			public void ClearName(QilNode n)
			{
				if (n.Annotation is QilXmlWriter.NameGenerator.NameAnnotation)
				{
					n.Annotation = ((QilXmlWriter.NameGenerator.NameAnnotation)n.Annotation).PriorAnnotation;
				}
			}

			// Token: 0x040028EA RID: 10474
			private StringBuilder name;

			// Token: 0x040028EB RID: 10475
			private int len;

			// Token: 0x040028EC RID: 10476
			private int zero;

			// Token: 0x040028ED RID: 10477
			private char start;

			// Token: 0x040028EE RID: 10478
			private char end;

			// Token: 0x02000652 RID: 1618
			private class NameAnnotation : ListBase<object>
			{
				// Token: 0x06004124 RID: 16676 RVA: 0x0015BC6F File Offset: 0x00159E6F
				public NameAnnotation(string s, object a)
				{
					this.Name = s;
					this.PriorAnnotation = a;
				}

				// Token: 0x17000CBB RID: 3259
				// (get) Token: 0x06004125 RID: 16677 RVA: 0x00003242 File Offset: 0x00001442
				public override int Count
				{
					get
					{
						return 1;
					}
				}

				// Token: 0x17000CBC RID: 3260
				public override object this[int index]
				{
					get
					{
						if (index == 0)
						{
							return this.PriorAnnotation;
						}
						throw new IndexOutOfRangeException();
					}
					set
					{
						throw new NotSupportedException();
					}
				}

				// Token: 0x040028EF RID: 10479
				public string Name;

				// Token: 0x040028F0 RID: 10480
				public object PriorAnnotation;
			}
		}
	}
}
