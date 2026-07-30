using System;
using System.Collections.Generic;
using System.Xml.Xsl.Qil;

namespace System.Xml.Xsl.Xslt
{
	// Token: 0x02000578 RID: 1400
	internal class InvokeGenerator : QilCloneVisitor
	{
		// Token: 0x0600379F RID: 14239 RVA: 0x001356C4 File Offset: 0x001338C4
		public InvokeGenerator(XsltQilFactory f, bool debug)
			: base(f.BaseFactory)
		{
			this.debug = debug;
			this.fac = f;
			this.iterStack = new Stack<QilIterator>();
		}

		// Token: 0x060037A0 RID: 14240 RVA: 0x001356EC File Offset: 0x001338EC
		public QilNode GenerateInvoke(QilFunction func, IList<XslNode> actualArgs)
		{
			this.iterStack.Clear();
			this.formalArgs = func.Arguments;
			this.invokeArgs = this.fac.ActualParameterList();
			this.curArg = 0;
			while (this.curArg < this.formalArgs.Count)
			{
				QilParameter qilParameter = (QilParameter)this.formalArgs[this.curArg];
				QilNode qilNode = this.FindActualArg(qilParameter, actualArgs);
				if (qilNode == null)
				{
					if (this.debug)
					{
						if (qilParameter.Name.NamespaceUri == "urn:schemas-microsoft-com:xslt-debug")
						{
							qilNode = base.Clone(qilParameter.DefaultValue);
						}
						else
						{
							qilNode = this.fac.DefaultValueMarker();
						}
					}
					else
					{
						qilNode = base.Clone(qilParameter.DefaultValue);
					}
				}
				XmlQueryType xmlType = qilParameter.XmlType;
				if (!qilNode.XmlType.IsSubtypeOf(xmlType))
				{
					qilNode = this.fac.TypeAssert(qilNode, xmlType);
				}
				this.invokeArgs.Add(qilNode);
				this.curArg++;
			}
			QilNode qilNode2 = this.fac.Invoke(func, this.invokeArgs);
			while (this.iterStack.Count != 0)
			{
				qilNode2 = this.fac.Loop(this.iterStack.Pop(), qilNode2);
			}
			return qilNode2;
		}

		// Token: 0x060037A1 RID: 14241 RVA: 0x00135828 File Offset: 0x00133A28
		private QilNode FindActualArg(QilParameter formalArg, IList<XslNode> actualArgs)
		{
			QilName name = formalArg.Name;
			foreach (XslNode xslNode in actualArgs)
			{
				if (xslNode.Name.Equals(name))
				{
					return ((VarPar)xslNode).Value;
				}
			}
			return null;
		}

		// Token: 0x060037A2 RID: 14242 RVA: 0x00135890 File Offset: 0x00133A90
		protected override QilNode VisitReference(QilNode n)
		{
			QilNode qilNode = base.FindClonedReference(n);
			if (qilNode != null)
			{
				return qilNode;
			}
			int i = 0;
			while (i < this.curArg)
			{
				if (n == this.formalArgs[i])
				{
					if (this.invokeArgs[i] is QilLiteral)
					{
						return this.invokeArgs[i].ShallowClone(this.fac.BaseFactory);
					}
					if (!(this.invokeArgs[i] is QilIterator))
					{
						QilIterator qilIterator = this.fac.BaseFactory.Let(this.invokeArgs[i]);
						this.iterStack.Push(qilIterator);
						this.invokeArgs[i] = qilIterator;
					}
					return this.invokeArgs[i];
				}
				else
				{
					i++;
				}
			}
			return n;
		}

		// Token: 0x060037A3 RID: 14243 RVA: 0x0000206B File Offset: 0x0000026B
		protected override QilNode VisitFunction(QilFunction n)
		{
			return n;
		}

		// Token: 0x040023B4 RID: 9140
		private bool debug;

		// Token: 0x040023B5 RID: 9141
		private Stack<QilIterator> iterStack;

		// Token: 0x040023B6 RID: 9142
		private QilList formalArgs;

		// Token: 0x040023B7 RID: 9143
		private QilList invokeArgs;

		// Token: 0x040023B8 RID: 9144
		private int curArg;

		// Token: 0x040023B9 RID: 9145
		private XsltQilFactory fac;
	}
}
