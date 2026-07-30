using System;
using System.Xml.Xsl.Qil;

namespace System.Xml.Xsl.Xslt
{
	// Token: 0x020005A3 RID: 1443
	internal static class AstFactory
	{
		// Token: 0x060038CA RID: 14538 RVA: 0x0013F228 File Offset: 0x0013D428
		public static XslNode XslNode(XslNodeType nodeType, QilName name, string arg, XslVersion xslVer)
		{
			return new XslNode(nodeType, name, arg, xslVer);
		}

		// Token: 0x060038CB RID: 14539 RVA: 0x0013F233 File Offset: 0x0013D433
		public static XslNode ApplyImports(QilName mode, Stylesheet sheet, XslVersion xslVer)
		{
			return new XslNode(XslNodeType.ApplyImports, mode, sheet, xslVer);
		}

		// Token: 0x060038CC RID: 14540 RVA: 0x0013F23E File Offset: 0x0013D43E
		public static XslNodeEx ApplyTemplates(QilName mode, string select, XsltInput.ContextInfo ctxInfo, XslVersion xslVer)
		{
			return new XslNodeEx(XslNodeType.ApplyTemplates, mode, select, ctxInfo, xslVer);
		}

		// Token: 0x060038CD RID: 14541 RVA: 0x0013F24A File Offset: 0x0013D44A
		public static XslNodeEx ApplyTemplates(QilName mode)
		{
			return new XslNodeEx(XslNodeType.ApplyTemplates, mode, null, XslVersion.Version10);
		}

		// Token: 0x060038CE RID: 14542 RVA: 0x0013F255 File Offset: 0x0013D455
		public static NodeCtor Attribute(string nameAvt, string nsAvt, XslVersion xslVer)
		{
			return new NodeCtor(XslNodeType.Attribute, nameAvt, nsAvt, xslVer);
		}

		// Token: 0x060038CF RID: 14543 RVA: 0x0013F260 File Offset: 0x0013D460
		public static AttributeSet AttributeSet(QilName name)
		{
			return new AttributeSet(name, XslVersion.Version10);
		}

		// Token: 0x060038D0 RID: 14544 RVA: 0x0013F269 File Offset: 0x0013D469
		public static XslNodeEx CallTemplate(QilName name, XsltInput.ContextInfo ctxInfo)
		{
			return new XslNodeEx(XslNodeType.CallTemplate, name, null, ctxInfo, XslVersion.Version10);
		}

		// Token: 0x060038D1 RID: 14545 RVA: 0x0013F275 File Offset: 0x0013D475
		public static XslNode Choose()
		{
			return new XslNode(XslNodeType.Choose);
		}

		// Token: 0x060038D2 RID: 14546 RVA: 0x0013F27D File Offset: 0x0013D47D
		public static XslNode Comment()
		{
			return new XslNode(XslNodeType.Comment);
		}

		// Token: 0x060038D3 RID: 14547 RVA: 0x0013F285 File Offset: 0x0013D485
		public static XslNode Copy()
		{
			return new XslNode(XslNodeType.Copy);
		}

		// Token: 0x060038D4 RID: 14548 RVA: 0x0013F28D File Offset: 0x0013D48D
		public static XslNode CopyOf(string select, XslVersion xslVer)
		{
			return new XslNode(XslNodeType.CopyOf, null, select, xslVer);
		}

		// Token: 0x060038D5 RID: 14549 RVA: 0x0013F299 File Offset: 0x0013D499
		public static NodeCtor Element(string nameAvt, string nsAvt, XslVersion xslVer)
		{
			return new NodeCtor(XslNodeType.Element, nameAvt, nsAvt, xslVer);
		}

		// Token: 0x060038D6 RID: 14550 RVA: 0x0013F2A5 File Offset: 0x0013D4A5
		public static XslNode Error(string message)
		{
			return new XslNode(XslNodeType.Error, null, message, XslVersion.Version10);
		}

		// Token: 0x060038D7 RID: 14551 RVA: 0x0013F2B1 File Offset: 0x0013D4B1
		public static XslNodeEx ForEach(string select, XsltInput.ContextInfo ctxInfo, XslVersion xslVer)
		{
			return new XslNodeEx(XslNodeType.ForEach, null, select, ctxInfo, xslVer);
		}

		// Token: 0x060038D8 RID: 14552 RVA: 0x0013F2BE File Offset: 0x0013D4BE
		public static XslNode If(string test, XslVersion xslVer)
		{
			return new XslNode(XslNodeType.If, null, test, xslVer);
		}

		// Token: 0x060038D9 RID: 14553 RVA: 0x0013F2CA File Offset: 0x0013D4CA
		public static Key Key(QilName name, string match, string use, XslVersion xslVer)
		{
			return new Key(name, match, use, xslVer);
		}

		// Token: 0x060038DA RID: 14554 RVA: 0x0013F2D5 File Offset: 0x0013D4D5
		public static XslNode List()
		{
			return new XslNode(XslNodeType.List);
		}

		// Token: 0x060038DB RID: 14555 RVA: 0x0013F2DE File Offset: 0x0013D4DE
		public static XslNode LiteralAttribute(QilName name, string value, XslVersion xslVer)
		{
			return new XslNode(XslNodeType.LiteralAttribute, name, value, xslVer);
		}

		// Token: 0x060038DC RID: 14556 RVA: 0x0013F2EA File Offset: 0x0013D4EA
		public static XslNode LiteralElement(QilName name)
		{
			return new XslNode(XslNodeType.LiteralElement, name, null, XslVersion.Version10);
		}

		// Token: 0x060038DD RID: 14557 RVA: 0x0013F2F6 File Offset: 0x0013D4F6
		public static XslNode Message(bool term)
		{
			return new XslNode(XslNodeType.Message, null, term, XslVersion.Version10);
		}

		// Token: 0x060038DE RID: 14558 RVA: 0x0013F307 File Offset: 0x0013D507
		public static XslNode Nop()
		{
			return new XslNode(XslNodeType.Nop);
		}

		// Token: 0x060038DF RID: 14559 RVA: 0x0013F310 File Offset: 0x0013D510
		public static Number Number(NumberLevel level, string count, string from, string value, string format, string lang, string letterValue, string groupingSeparator, string groupingSize, XslVersion xslVer)
		{
			return new Number(level, count, from, value, format, lang, letterValue, groupingSeparator, groupingSize, xslVer);
		}

		// Token: 0x060038E0 RID: 14560 RVA: 0x0013F332 File Offset: 0x0013D532
		public static XslNode Otherwise()
		{
			return new XslNode(XslNodeType.Otherwise);
		}

		// Token: 0x060038E1 RID: 14561 RVA: 0x0013F33B File Offset: 0x0013D53B
		public static XslNode PI(string name, XslVersion xslVer)
		{
			return new XslNode(XslNodeType.PI, null, name, xslVer);
		}

		// Token: 0x060038E2 RID: 14562 RVA: 0x0013F347 File Offset: 0x0013D547
		public static Sort Sort(string select, string lang, string dataType, string order, string caseOrder, XslVersion xslVer)
		{
			return new Sort(select, lang, dataType, order, caseOrder, xslVer);
		}

		// Token: 0x060038E3 RID: 14563 RVA: 0x0013F356 File Offset: 0x0013D556
		public static Template Template(QilName name, string match, QilName mode, double priority, XslVersion xslVer)
		{
			return new Template(name, match, mode, priority, xslVer);
		}

		// Token: 0x060038E4 RID: 14564 RVA: 0x0013F363 File Offset: 0x0013D563
		public static XslNode Text(string data)
		{
			return new Text(data, SerializationHints.None, XslVersion.Version10);
		}

		// Token: 0x060038E5 RID: 14565 RVA: 0x0013F36D File Offset: 0x0013D56D
		public static XslNode Text(string data, SerializationHints hints)
		{
			return new Text(data, hints, XslVersion.Version10);
		}

		// Token: 0x060038E6 RID: 14566 RVA: 0x0013F377 File Offset: 0x0013D577
		public static XslNode UseAttributeSet(QilName name)
		{
			return new XslNode(XslNodeType.UseAttributeSet, name, null, XslVersion.Version10);
		}

		// Token: 0x060038E7 RID: 14567 RVA: 0x0013F383 File Offset: 0x0013D583
		public static VarPar VarPar(XslNodeType nt, QilName name, string select, XslVersion xslVer)
		{
			return new VarPar(nt, name, select, xslVer);
		}

		// Token: 0x060038E8 RID: 14568 RVA: 0x0013F38E File Offset: 0x0013D58E
		public static VarPar WithParam(QilName name)
		{
			return AstFactory.VarPar(XslNodeType.WithParam, name, null, XslVersion.Version10);
		}

		// Token: 0x060038E9 RID: 14569 RVA: 0x0013F39A File Offset: 0x0013D59A
		public static QilName QName(string local, string uri, string prefix)
		{
			return AstFactory.f.LiteralQName(local, uri, prefix);
		}

		// Token: 0x060038EA RID: 14570 RVA: 0x0013F3A9 File Offset: 0x0013D5A9
		public static QilName QName(string local)
		{
			return AstFactory.f.LiteralQName(local);
		}

		// Token: 0x04002518 RID: 9496
		private static QilFactory f = new QilFactory();
	}
}
