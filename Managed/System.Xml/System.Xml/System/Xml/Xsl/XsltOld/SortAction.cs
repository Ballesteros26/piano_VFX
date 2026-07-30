using System;
using System.Globalization;
using System.Xml.XPath;

namespace System.Xml.Xsl.XsltOld
{
	// Token: 0x0200053E RID: 1342
	internal class SortAction : CompiledAction
	{
		// Token: 0x0600365C RID: 13916 RVA: 0x00130B28 File Offset: 0x0012ED28
		private string ParseLang(string value)
		{
			if (value == null)
			{
				return null;
			}
			if (XmlComplianceUtil.IsValidLanguageID(value.ToCharArray(), 0, value.Length) || (value.Length != 0 && CultureInfo.GetCultureInfo(value) != null))
			{
				return value;
			}
			if (this.forwardCompatibility)
			{
				return null;
			}
			throw XsltException.Create("'{1}' is an invalid value for the '{0}' attribute.", new string[] { "lang", value });
		}

		// Token: 0x0600365D RID: 13917 RVA: 0x00130B88 File Offset: 0x0012ED88
		private XmlDataType ParseDataType(string value, InputScopeManager manager)
		{
			if (value == null)
			{
				return XmlDataType.Text;
			}
			if (value == "text")
			{
				return XmlDataType.Text;
			}
			if (value == "number")
			{
				return XmlDataType.Number;
			}
			string text;
			string text2;
			PrefixQName.ParseQualifiedName(value, out text, out text2);
			manager.ResolveXmlNamespace(text);
			if (text.Length == 0 && !this.forwardCompatibility)
			{
				throw XsltException.Create("'{1}' is an invalid value for the '{0}' attribute.", new string[] { "data-type", value });
			}
			return XmlDataType.Text;
		}

		// Token: 0x0600365E RID: 13918 RVA: 0x00130BF8 File Offset: 0x0012EDF8
		private XmlSortOrder ParseOrder(string value)
		{
			if (value == null)
			{
				return XmlSortOrder.Ascending;
			}
			if (value == "ascending")
			{
				return XmlSortOrder.Ascending;
			}
			if (value == "descending")
			{
				return XmlSortOrder.Descending;
			}
			if (this.forwardCompatibility)
			{
				return XmlSortOrder.Ascending;
			}
			throw XsltException.Create("'{1}' is an invalid value for the '{0}' attribute.", new string[] { "order", value });
		}

		// Token: 0x0600365F RID: 13919 RVA: 0x00130C50 File Offset: 0x0012EE50
		private XmlCaseOrder ParseCaseOrder(string value)
		{
			if (value == null)
			{
				return XmlCaseOrder.None;
			}
			if (value == "upper-first")
			{
				return XmlCaseOrder.UpperFirst;
			}
			if (value == "lower-first")
			{
				return XmlCaseOrder.LowerFirst;
			}
			if (this.forwardCompatibility)
			{
				return XmlCaseOrder.None;
			}
			throw XsltException.Create("'{1}' is an invalid value for the '{0}' attribute.", new string[] { "case-order", value });
		}

		// Token: 0x06003660 RID: 13920 RVA: 0x00130CA8 File Offset: 0x0012EEA8
		internal override void Compile(Compiler compiler)
		{
			base.CompileAttributes(compiler);
			base.CheckEmpty(compiler);
			if (this.selectKey == -1)
			{
				this.selectKey = compiler.AddQuery(".");
			}
			this.forwardCompatibility = compiler.ForwardCompatibility;
			this.manager = compiler.CloneScopeManager();
			this.lang = this.ParseLang(CompiledAction.PrecalculateAvt(ref this.langAvt));
			this.dataType = this.ParseDataType(CompiledAction.PrecalculateAvt(ref this.dataTypeAvt), this.manager);
			this.order = this.ParseOrder(CompiledAction.PrecalculateAvt(ref this.orderAvt));
			this.caseOrder = this.ParseCaseOrder(CompiledAction.PrecalculateAvt(ref this.caseOrderAvt));
			if (this.langAvt == null && this.dataTypeAvt == null && this.orderAvt == null && this.caseOrderAvt == null)
			{
				this.sort = new Sort(this.selectKey, this.lang, this.dataType, this.order, this.caseOrder);
			}
		}

		// Token: 0x06003661 RID: 13921 RVA: 0x00130DA0 File Offset: 0x0012EFA0
		internal override bool CompileAttribute(Compiler compiler)
		{
			string localName = compiler.Input.LocalName;
			string value = compiler.Input.Value;
			if (Ref.Equal(localName, compiler.Atoms.Select))
			{
				this.selectKey = compiler.AddQuery(value);
			}
			else if (Ref.Equal(localName, compiler.Atoms.Lang))
			{
				this.langAvt = Avt.CompileAvt(compiler, value);
			}
			else if (Ref.Equal(localName, compiler.Atoms.DataType))
			{
				this.dataTypeAvt = Avt.CompileAvt(compiler, value);
			}
			else if (Ref.Equal(localName, compiler.Atoms.Order))
			{
				this.orderAvt = Avt.CompileAvt(compiler, value);
			}
			else
			{
				if (!Ref.Equal(localName, compiler.Atoms.CaseOrder))
				{
					return false;
				}
				this.caseOrderAvt = Avt.CompileAvt(compiler, value);
			}
			return true;
		}

		// Token: 0x06003662 RID: 13922 RVA: 0x00130E78 File Offset: 0x0012F078
		internal override void Execute(Processor processor, ActionFrame frame)
		{
			processor.AddSort((this.sort != null) ? this.sort : new Sort(this.selectKey, (this.langAvt == null) ? this.lang : this.ParseLang(this.langAvt.Evaluate(processor, frame)), (this.dataTypeAvt == null) ? this.dataType : this.ParseDataType(this.dataTypeAvt.Evaluate(processor, frame), this.manager), (this.orderAvt == null) ? this.order : this.ParseOrder(this.orderAvt.Evaluate(processor, frame)), (this.caseOrderAvt == null) ? this.caseOrder : this.ParseCaseOrder(this.caseOrderAvt.Evaluate(processor, frame))));
			frame.Finished();
		}

		// Token: 0x040022BE RID: 8894
		private int selectKey = -1;

		// Token: 0x040022BF RID: 8895
		private Avt langAvt;

		// Token: 0x040022C0 RID: 8896
		private Avt dataTypeAvt;

		// Token: 0x040022C1 RID: 8897
		private Avt orderAvt;

		// Token: 0x040022C2 RID: 8898
		private Avt caseOrderAvt;

		// Token: 0x040022C3 RID: 8899
		private string lang;

		// Token: 0x040022C4 RID: 8900
		private XmlDataType dataType = XmlDataType.Text;

		// Token: 0x040022C5 RID: 8901
		private XmlSortOrder order = XmlSortOrder.Ascending;

		// Token: 0x040022C6 RID: 8902
		private XmlCaseOrder caseOrder;

		// Token: 0x040022C7 RID: 8903
		private Sort sort;

		// Token: 0x040022C8 RID: 8904
		private bool forwardCompatibility;

		// Token: 0x040022C9 RID: 8905
		private InputScopeManager manager;
	}
}
