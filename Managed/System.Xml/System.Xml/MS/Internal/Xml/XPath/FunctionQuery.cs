using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.XPath;
using System.Xml.Xsl;

namespace MS.Internal.Xml.XPath
{
	// Token: 0x02000025 RID: 37
	internal sealed class FunctionQuery : ExtensionQuery
	{
		// Token: 0x060000E6 RID: 230 RVA: 0x00004151 File Offset: 0x00002351
		public FunctionQuery(string prefix, string name, List<Query> args)
			: base(prefix, name)
		{
			this.args = args;
		}

		// Token: 0x060000E7 RID: 231 RVA: 0x00004164 File Offset: 0x00002364
		private FunctionQuery(FunctionQuery other)
			: base(other)
		{
			this.function = other.function;
			Query[] array = new Query[other.args.Count];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = Query.Clone(other.args[i]);
			}
			this.args = array;
			this.args = array;
		}

		// Token: 0x060000E8 RID: 232 RVA: 0x000041C8 File Offset: 0x000023C8
		public override void SetXsltContext(XsltContext context)
		{
			if (context == null)
			{
				throw XPathException.Create("Namespace Manager or XsltContext needed. This query has a prefix, variable, or user-defined function.");
			}
			if (this.xsltContext != context)
			{
				this.xsltContext = context;
				foreach (Query query in this.args)
				{
					query.SetXsltContext(context);
				}
				XPathResultType[] array = new XPathResultType[this.args.Count];
				for (int i = 0; i < this.args.Count; i++)
				{
					array[i] = this.args[i].StaticType;
				}
				this.function = this.xsltContext.ResolveFunction(this.prefix, this.name, array);
				if (this.function == null)
				{
					throw XPathException.Create("The function '{0}()' is undefined.", base.QName);
				}
			}
		}

		// Token: 0x060000E9 RID: 233 RVA: 0x000042A8 File Offset: 0x000024A8
		public override object Evaluate(XPathNodeIterator nodeIterator)
		{
			if (this.xsltContext == null)
			{
				throw XPathException.Create("Namespace Manager or XsltContext needed. This query has a prefix, variable, or user-defined function.");
			}
			object[] array = new object[this.args.Count];
			for (int i = 0; i < this.args.Count; i++)
			{
				array[i] = this.args[i].Evaluate(nodeIterator);
				if (array[i] is XPathNodeIterator)
				{
					array[i] = new XPathSelectionIterator(nodeIterator.Current, this.args[i]);
				}
			}
			object obj;
			try
			{
				obj = base.ProcessResult(this.function.Invoke(this.xsltContext, array, nodeIterator.Current));
			}
			catch (Exception ex)
			{
				throw XPathException.Create("Function '{0}()' has failed.", base.QName, ex);
			}
			return obj;
		}

		// Token: 0x060000EA RID: 234 RVA: 0x00004370 File Offset: 0x00002570
		public override XPathNavigator MatchNode(XPathNavigator navigator)
		{
			if (this.name != "key" && this.prefix.Length != 0)
			{
				throw XPathException.Create("'{0}' is an invalid XSLT pattern.");
			}
			this.Evaluate(new XPathSingletonIterator(navigator, true));
			XPathNavigator xpathNavigator;
			while ((xpathNavigator = this.Advance()) != null)
			{
				if (xpathNavigator.IsSamePosition(navigator))
				{
					return xpathNavigator;
				}
			}
			return xpathNavigator;
		}

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x060000EB RID: 235 RVA: 0x000043D0 File Offset: 0x000025D0
		public override XPathResultType StaticType
		{
			get
			{
				XPathResultType xpathResultType = ((this.function != null) ? this.function.ReturnType : XPathResultType.Any);
				if (xpathResultType == XPathResultType.Error)
				{
					xpathResultType = XPathResultType.Any;
				}
				return xpathResultType;
			}
		}

		// Token: 0x060000EC RID: 236 RVA: 0x000043FB File Offset: 0x000025FB
		public override XPathNodeIterator Clone()
		{
			return new FunctionQuery(this);
		}

		// Token: 0x060000ED RID: 237 RVA: 0x00004404 File Offset: 0x00002604
		public override void PrintQuery(XmlWriter w)
		{
			w.WriteStartElement(base.GetType().Name);
			w.WriteAttributeString("name", (this.prefix.Length != 0) ? (this.prefix + ":" + this.name) : this.name);
			foreach (Query query in this.args)
			{
				query.PrintQuery(w);
			}
			w.WriteEndElement();
		}

		// Token: 0x040000AB RID: 171
		private IList<Query> args;

		// Token: 0x040000AC RID: 172
		private IXsltContextFunction function;
	}
}
