using System;

namespace System.Xml.Schema
{
	// Token: 0x0200038E RID: 910
	internal class CompiledIdentityConstraint
	{
		// Token: 0x17000754 RID: 1876
		// (get) Token: 0x060024E3 RID: 9443 RVA: 0x000DF615 File Offset: 0x000DD815
		public CompiledIdentityConstraint.ConstraintRole Role
		{
			get
			{
				return this.role;
			}
		}

		// Token: 0x17000755 RID: 1877
		// (get) Token: 0x060024E4 RID: 9444 RVA: 0x000DF61D File Offset: 0x000DD81D
		public Asttree Selector
		{
			get
			{
				return this.selector;
			}
		}

		// Token: 0x17000756 RID: 1878
		// (get) Token: 0x060024E5 RID: 9445 RVA: 0x000DF625 File Offset: 0x000DD825
		public Asttree[] Fields
		{
			get
			{
				return this.fields;
			}
		}

		// Token: 0x060024E6 RID: 9446 RVA: 0x000DF62D File Offset: 0x000DD82D
		private CompiledIdentityConstraint()
		{
		}

		// Token: 0x060024E7 RID: 9447 RVA: 0x000DF64C File Offset: 0x000DD84C
		public CompiledIdentityConstraint(XmlSchemaIdentityConstraint constraint, XmlNamespaceManager nsmgr)
		{
			this.name = constraint.QualifiedName;
			try
			{
				this.selector = new Asttree(constraint.Selector.XPath, false, nsmgr);
			}
			catch (XmlSchemaException ex)
			{
				ex.SetSource(constraint.Selector);
				throw ex;
			}
			XmlSchemaObjectCollection xmlSchemaObjectCollection = constraint.Fields;
			this.fields = new Asttree[xmlSchemaObjectCollection.Count];
			for (int i = 0; i < xmlSchemaObjectCollection.Count; i++)
			{
				try
				{
					this.fields[i] = new Asttree(((XmlSchemaXPath)xmlSchemaObjectCollection[i]).XPath, true, nsmgr);
				}
				catch (XmlSchemaException ex2)
				{
					ex2.SetSource(constraint.Fields[i]);
					throw ex2;
				}
			}
			if (constraint is XmlSchemaUnique)
			{
				this.role = CompiledIdentityConstraint.ConstraintRole.Unique;
				return;
			}
			if (constraint is XmlSchemaKey)
			{
				this.role = CompiledIdentityConstraint.ConstraintRole.Key;
				return;
			}
			this.role = CompiledIdentityConstraint.ConstraintRole.Keyref;
			this.refer = ((XmlSchemaKeyref)constraint).Refer;
		}

		// Token: 0x040018FA RID: 6394
		internal XmlQualifiedName name = XmlQualifiedName.Empty;

		// Token: 0x040018FB RID: 6395
		private CompiledIdentityConstraint.ConstraintRole role;

		// Token: 0x040018FC RID: 6396
		private Asttree selector;

		// Token: 0x040018FD RID: 6397
		private Asttree[] fields;

		// Token: 0x040018FE RID: 6398
		internal XmlQualifiedName refer = XmlQualifiedName.Empty;

		// Token: 0x040018FF RID: 6399
		public static readonly CompiledIdentityConstraint Empty = new CompiledIdentityConstraint();

		// Token: 0x0200038F RID: 911
		public enum ConstraintRole
		{
			// Token: 0x04001901 RID: 6401
			Unique,
			// Token: 0x04001902 RID: 6402
			Key,
			// Token: 0x04001903 RID: 6403
			Keyref
		}
	}
}
