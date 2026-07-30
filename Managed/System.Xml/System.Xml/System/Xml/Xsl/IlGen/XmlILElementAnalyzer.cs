using System;
using System.Collections;
using System.Xml.Xsl.Qil;

namespace System.Xml.Xsl.IlGen
{
	// Token: 0x0200066A RID: 1642
	internal class XmlILElementAnalyzer : XmlILStateAnalyzer
	{
		// Token: 0x0600421D RID: 16925 RVA: 0x00160A18 File Offset: 0x0015EC18
		public XmlILElementAnalyzer(QilFactory fac)
			: base(fac)
		{
		}

		// Token: 0x0600421E RID: 16926 RVA: 0x00160A38 File Offset: 0x0015EC38
		public override QilNode Analyze(QilNode ndElem, QilNode ndContent)
		{
			this.parentInfo = XmlILConstructInfo.Write(ndElem);
			this.parentInfo.MightHaveNamespacesAfterAttributes = false;
			this.parentInfo.MightHaveAttributes = false;
			this.parentInfo.MightHaveDuplicateAttributes = false;
			this.parentInfo.MightHaveNamespaces = !this.parentInfo.IsNamespaceInScope;
			this.dupAttrs.Clear();
			return base.Analyze(ndElem, ndContent);
		}

		// Token: 0x0600421F RID: 16927 RVA: 0x00160AA1 File Offset: 0x0015ECA1
		protected override void AnalyzeLoop(QilLoop ndLoop, XmlILConstructInfo info)
		{
			if (ndLoop.XmlType.MaybeMany)
			{
				this.CheckAttributeNamespaceConstruct(ndLoop.XmlType);
			}
			base.AnalyzeLoop(ndLoop, info);
		}

		// Token: 0x06004220 RID: 16928 RVA: 0x00160AC4 File Offset: 0x0015ECC4
		protected override void AnalyzeCopy(QilNode ndCopy, XmlILConstructInfo info)
		{
			if (ndCopy.NodeType == QilNodeType.AttributeCtor)
			{
				this.AnalyzeAttributeCtor(ndCopy as QilBinary, info);
			}
			else
			{
				this.CheckAttributeNamespaceConstruct(ndCopy.XmlType);
			}
			base.AnalyzeCopy(ndCopy, info);
		}

		// Token: 0x06004221 RID: 16929 RVA: 0x00160AF4 File Offset: 0x0015ECF4
		private void AnalyzeAttributeCtor(QilBinary ndAttr, XmlILConstructInfo info)
		{
			if (ndAttr.Left.NodeType == QilNodeType.LiteralQName)
			{
				QilName qilName = ndAttr.Left as QilName;
				this.parentInfo.MightHaveAttributes = true;
				if (!this.parentInfo.MightHaveDuplicateAttributes)
				{
					XmlQualifiedName xmlQualifiedName = new XmlQualifiedName(this.attrNames.Add(qilName.LocalName), this.attrNames.Add(qilName.NamespaceUri));
					int i;
					for (i = 0; i < this.dupAttrs.Count; i++)
					{
						XmlQualifiedName xmlQualifiedName2 = (XmlQualifiedName)this.dupAttrs[i];
						if (xmlQualifiedName2.Name == xmlQualifiedName.Name && xmlQualifiedName2.Namespace == xmlQualifiedName.Namespace)
						{
							this.parentInfo.MightHaveDuplicateAttributes = true;
						}
					}
					if (i >= this.dupAttrs.Count)
					{
						this.dupAttrs.Add(xmlQualifiedName);
					}
				}
				if (!info.IsNamespaceInScope)
				{
					this.parentInfo.MightHaveNamespaces = true;
					return;
				}
			}
			else
			{
				this.CheckAttributeNamespaceConstruct(ndAttr.XmlType);
			}
		}

		// Token: 0x06004222 RID: 16930 RVA: 0x00160BF0 File Offset: 0x0015EDF0
		private void CheckAttributeNamespaceConstruct(XmlQueryType typ)
		{
			if ((typ.NodeKinds & XmlNodeKindFlags.Attribute) != XmlNodeKindFlags.None)
			{
				this.parentInfo.MightHaveAttributes = true;
				this.parentInfo.MightHaveDuplicateAttributes = true;
				this.parentInfo.MightHaveNamespaces = true;
			}
			if ((typ.NodeKinds & XmlNodeKindFlags.Namespace) != XmlNodeKindFlags.None)
			{
				this.parentInfo.MightHaveNamespaces = true;
				if (this.parentInfo.MightHaveAttributes)
				{
					this.parentInfo.MightHaveNamespacesAfterAttributes = true;
				}
			}
		}

		// Token: 0x04002A66 RID: 10854
		private NameTable attrNames = new NameTable();

		// Token: 0x04002A67 RID: 10855
		private ArrayList dupAttrs = new ArrayList();
	}
}
