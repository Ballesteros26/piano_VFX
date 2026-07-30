using System;

namespace System.Xml.Xsl.Xslt
{
	// Token: 0x020005AA RID: 1450
	internal abstract class XslVisitor<T>
	{
		// Token: 0x06003936 RID: 14646 RVA: 0x001410D8 File Offset: 0x0013F2D8
		protected virtual T Visit(XslNode node)
		{
			switch (node.NodeType)
			{
			case XslNodeType.ApplyImports:
				return this.VisitApplyImports(node);
			case XslNodeType.ApplyTemplates:
				return this.VisitApplyTemplates(node);
			case XslNodeType.Attribute:
				return this.VisitAttribute((NodeCtor)node);
			case XslNodeType.AttributeSet:
				return this.VisitAttributeSet((AttributeSet)node);
			case XslNodeType.CallTemplate:
				return this.VisitCallTemplate(node);
			case XslNodeType.Choose:
				return this.VisitChoose(node);
			case XslNodeType.Comment:
				return this.VisitComment(node);
			case XslNodeType.Copy:
				return this.VisitCopy(node);
			case XslNodeType.CopyOf:
				return this.VisitCopyOf(node);
			case XslNodeType.Element:
				return this.VisitElement((NodeCtor)node);
			case XslNodeType.Error:
				return this.VisitError(node);
			case XslNodeType.ForEach:
				return this.VisitForEach(node);
			case XslNodeType.If:
				return this.VisitIf(node);
			case XslNodeType.Key:
				return this.VisitKey((Key)node);
			case XslNodeType.List:
				return this.VisitList(node);
			case XslNodeType.LiteralAttribute:
				return this.VisitLiteralAttribute(node);
			case XslNodeType.LiteralElement:
				return this.VisitLiteralElement(node);
			case XslNodeType.Message:
				return this.VisitMessage(node);
			case XslNodeType.Nop:
				return this.VisitNop(node);
			case XslNodeType.Number:
				return this.VisitNumber((Number)node);
			case XslNodeType.Otherwise:
				return this.VisitOtherwise(node);
			case XslNodeType.Param:
				return this.VisitParam((VarPar)node);
			case XslNodeType.PI:
				return this.VisitPI(node);
			case XslNodeType.Sort:
				return this.VisitSort((Sort)node);
			case XslNodeType.Template:
				return this.VisitTemplate((Template)node);
			case XslNodeType.Text:
				return this.VisitText((Text)node);
			case XslNodeType.UseAttributeSet:
				return this.VisitUseAttributeSet(node);
			case XslNodeType.ValueOf:
				return this.VisitValueOf(node);
			case XslNodeType.ValueOfDoe:
				return this.VisitValueOfDoe(node);
			case XslNodeType.Variable:
				return this.VisitVariable((VarPar)node);
			case XslNodeType.WithParam:
				return this.VisitWithParam((VarPar)node);
			default:
				return this.VisitUnknown(node);
			}
		}

		// Token: 0x06003937 RID: 14647 RVA: 0x001412AB File Offset: 0x0013F4AB
		protected virtual T VisitApplyImports(XslNode node)
		{
			return this.VisitChildren(node);
		}

		// Token: 0x06003938 RID: 14648 RVA: 0x001412AB File Offset: 0x0013F4AB
		protected virtual T VisitApplyTemplates(XslNode node)
		{
			return this.VisitChildren(node);
		}

		// Token: 0x06003939 RID: 14649 RVA: 0x001412AB File Offset: 0x0013F4AB
		protected virtual T VisitAttribute(NodeCtor node)
		{
			return this.VisitChildren(node);
		}

		// Token: 0x0600393A RID: 14650 RVA: 0x001412AB File Offset: 0x0013F4AB
		protected virtual T VisitAttributeSet(AttributeSet node)
		{
			return this.VisitChildren(node);
		}

		// Token: 0x0600393B RID: 14651 RVA: 0x001412AB File Offset: 0x0013F4AB
		protected virtual T VisitCallTemplate(XslNode node)
		{
			return this.VisitChildren(node);
		}

		// Token: 0x0600393C RID: 14652 RVA: 0x001412AB File Offset: 0x0013F4AB
		protected virtual T VisitChoose(XslNode node)
		{
			return this.VisitChildren(node);
		}

		// Token: 0x0600393D RID: 14653 RVA: 0x001412AB File Offset: 0x0013F4AB
		protected virtual T VisitComment(XslNode node)
		{
			return this.VisitChildren(node);
		}

		// Token: 0x0600393E RID: 14654 RVA: 0x001412AB File Offset: 0x0013F4AB
		protected virtual T VisitCopy(XslNode node)
		{
			return this.VisitChildren(node);
		}

		// Token: 0x0600393F RID: 14655 RVA: 0x001412AB File Offset: 0x0013F4AB
		protected virtual T VisitCopyOf(XslNode node)
		{
			return this.VisitChildren(node);
		}

		// Token: 0x06003940 RID: 14656 RVA: 0x001412AB File Offset: 0x0013F4AB
		protected virtual T VisitElement(NodeCtor node)
		{
			return this.VisitChildren(node);
		}

		// Token: 0x06003941 RID: 14657 RVA: 0x001412AB File Offset: 0x0013F4AB
		protected virtual T VisitError(XslNode node)
		{
			return this.VisitChildren(node);
		}

		// Token: 0x06003942 RID: 14658 RVA: 0x001412AB File Offset: 0x0013F4AB
		protected virtual T VisitForEach(XslNode node)
		{
			return this.VisitChildren(node);
		}

		// Token: 0x06003943 RID: 14659 RVA: 0x001412AB File Offset: 0x0013F4AB
		protected virtual T VisitIf(XslNode node)
		{
			return this.VisitChildren(node);
		}

		// Token: 0x06003944 RID: 14660 RVA: 0x001412AB File Offset: 0x0013F4AB
		protected virtual T VisitKey(Key node)
		{
			return this.VisitChildren(node);
		}

		// Token: 0x06003945 RID: 14661 RVA: 0x001412AB File Offset: 0x0013F4AB
		protected virtual T VisitList(XslNode node)
		{
			return this.VisitChildren(node);
		}

		// Token: 0x06003946 RID: 14662 RVA: 0x001412AB File Offset: 0x0013F4AB
		protected virtual T VisitLiteralAttribute(XslNode node)
		{
			return this.VisitChildren(node);
		}

		// Token: 0x06003947 RID: 14663 RVA: 0x001412AB File Offset: 0x0013F4AB
		protected virtual T VisitLiteralElement(XslNode node)
		{
			return this.VisitChildren(node);
		}

		// Token: 0x06003948 RID: 14664 RVA: 0x001412AB File Offset: 0x0013F4AB
		protected virtual T VisitMessage(XslNode node)
		{
			return this.VisitChildren(node);
		}

		// Token: 0x06003949 RID: 14665 RVA: 0x001412AB File Offset: 0x0013F4AB
		protected virtual T VisitNop(XslNode node)
		{
			return this.VisitChildren(node);
		}

		// Token: 0x0600394A RID: 14666 RVA: 0x001412AB File Offset: 0x0013F4AB
		protected virtual T VisitNumber(Number node)
		{
			return this.VisitChildren(node);
		}

		// Token: 0x0600394B RID: 14667 RVA: 0x001412AB File Offset: 0x0013F4AB
		protected virtual T VisitOtherwise(XslNode node)
		{
			return this.VisitChildren(node);
		}

		// Token: 0x0600394C RID: 14668 RVA: 0x001412AB File Offset: 0x0013F4AB
		protected virtual T VisitParam(VarPar node)
		{
			return this.VisitChildren(node);
		}

		// Token: 0x0600394D RID: 14669 RVA: 0x001412AB File Offset: 0x0013F4AB
		protected virtual T VisitPI(XslNode node)
		{
			return this.VisitChildren(node);
		}

		// Token: 0x0600394E RID: 14670 RVA: 0x001412AB File Offset: 0x0013F4AB
		protected virtual T VisitSort(Sort node)
		{
			return this.VisitChildren(node);
		}

		// Token: 0x0600394F RID: 14671 RVA: 0x001412AB File Offset: 0x0013F4AB
		protected virtual T VisitTemplate(Template node)
		{
			return this.VisitChildren(node);
		}

		// Token: 0x06003950 RID: 14672 RVA: 0x001412AB File Offset: 0x0013F4AB
		protected virtual T VisitText(Text node)
		{
			return this.VisitChildren(node);
		}

		// Token: 0x06003951 RID: 14673 RVA: 0x001412AB File Offset: 0x0013F4AB
		protected virtual T VisitUseAttributeSet(XslNode node)
		{
			return this.VisitChildren(node);
		}

		// Token: 0x06003952 RID: 14674 RVA: 0x001412AB File Offset: 0x0013F4AB
		protected virtual T VisitValueOf(XslNode node)
		{
			return this.VisitChildren(node);
		}

		// Token: 0x06003953 RID: 14675 RVA: 0x001412AB File Offset: 0x0013F4AB
		protected virtual T VisitValueOfDoe(XslNode node)
		{
			return this.VisitChildren(node);
		}

		// Token: 0x06003954 RID: 14676 RVA: 0x001412AB File Offset: 0x0013F4AB
		protected virtual T VisitVariable(VarPar node)
		{
			return this.VisitChildren(node);
		}

		// Token: 0x06003955 RID: 14677 RVA: 0x001412AB File Offset: 0x0013F4AB
		protected virtual T VisitWithParam(VarPar node)
		{
			return this.VisitChildren(node);
		}

		// Token: 0x06003956 RID: 14678 RVA: 0x001412AB File Offset: 0x0013F4AB
		protected virtual T VisitUnknown(XslNode node)
		{
			return this.VisitChildren(node);
		}

		// Token: 0x06003957 RID: 14679 RVA: 0x001412B4 File Offset: 0x0013F4B4
		protected virtual T VisitChildren(XslNode node)
		{
			foreach (XslNode xslNode in node.Content)
			{
				this.Visit(xslNode);
			}
			return default(T);
		}
	}
}
