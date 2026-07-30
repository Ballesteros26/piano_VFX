using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Security;
using System.Xml.XPath;
using System.Xml.Xsl.Runtime;
using MS.Internal.Xml.XPath;

namespace System.Xml.Xsl.XsltOld
{
	// Token: 0x0200053C RID: 1340
	internal class RootAction : TemplateBaseAction
	{
		// Token: 0x17000B7C RID: 2940
		// (get) Token: 0x0600362F RID: 13871 RVA: 0x0012F7E8 File Offset: 0x0012D9E8
		internal XsltOutput Output
		{
			get
			{
				if (this.output == null)
				{
					this.output = new XsltOutput();
				}
				return this.output;
			}
		}

		// Token: 0x06003630 RID: 13872 RVA: 0x0012F803 File Offset: 0x0012DA03
		internal override void Compile(Compiler compiler)
		{
			base.CompileDocument(compiler, false);
		}

		// Token: 0x06003631 RID: 13873 RVA: 0x0012F80D File Offset: 0x0012DA0D
		internal void InsertKey(XmlQualifiedName name, int MatchKey, int UseKey)
		{
			if (this.keyList == null)
			{
				this.keyList = new List<Key>();
			}
			this.keyList.Add(new Key(name, MatchKey, UseKey));
		}

		// Token: 0x06003632 RID: 13874 RVA: 0x0012F838 File Offset: 0x0012DA38
		internal AttributeSetAction GetAttributeSet(XmlQualifiedName name)
		{
			AttributeSetAction attributeSetAction = (AttributeSetAction)this.attributeSetTable[name];
			if (attributeSetAction == null)
			{
				throw XsltException.Create("A reference to attribute set '{0}' cannot be resolved. An 'xsl:attribute-set' of this name must be declared at the top level of the stylesheet.", new string[] { name.ToString() });
			}
			return attributeSetAction;
		}

		// Token: 0x06003633 RID: 13875 RVA: 0x0012F878 File Offset: 0x0012DA78
		public void PorcessAttributeSets(Stylesheet rootStylesheet)
		{
			this.MirgeAttributeSets(rootStylesheet);
			foreach (object obj in this.attributeSetTable.Values)
			{
				AttributeSetAction attributeSetAction = (AttributeSetAction)obj;
				if (attributeSetAction.containedActions != null)
				{
					attributeSetAction.containedActions.Reverse();
				}
			}
			this.CheckAttributeSets_RecurceInList(new Hashtable(), this.attributeSetTable.Keys);
		}

		// Token: 0x06003634 RID: 13876 RVA: 0x0012F900 File Offset: 0x0012DB00
		private void MirgeAttributeSets(Stylesheet stylesheet)
		{
			if (stylesheet.AttributeSetTable != null)
			{
				foreach (object obj in stylesheet.AttributeSetTable.Values)
				{
					AttributeSetAction attributeSetAction = (AttributeSetAction)obj;
					ArrayList containedActions = attributeSetAction.containedActions;
					AttributeSetAction attributeSetAction2 = (AttributeSetAction)this.attributeSetTable[attributeSetAction.Name];
					if (attributeSetAction2 == null)
					{
						attributeSetAction2 = new AttributeSetAction();
						attributeSetAction2.name = attributeSetAction.Name;
						attributeSetAction2.containedActions = new ArrayList();
						this.attributeSetTable[attributeSetAction.Name] = attributeSetAction2;
					}
					ArrayList containedActions2 = attributeSetAction2.containedActions;
					if (containedActions != null)
					{
						int num = containedActions.Count - 1;
						while (0 <= num)
						{
							containedActions2.Add(containedActions[num]);
							num--;
						}
					}
				}
			}
			foreach (object obj2 in stylesheet.Imports)
			{
				Stylesheet stylesheet2 = (Stylesheet)obj2;
				this.MirgeAttributeSets(stylesheet2);
			}
		}

		// Token: 0x06003635 RID: 13877 RVA: 0x0012FA3C File Offset: 0x0012DC3C
		private void CheckAttributeSets_RecurceInList(Hashtable markTable, ICollection setQNames)
		{
			foreach (object obj in setQNames)
			{
				XmlQualifiedName xmlQualifiedName = (XmlQualifiedName)obj;
				object obj2 = markTable[xmlQualifiedName];
				if (obj2 == "P")
				{
					throw XsltException.Create("Circular reference in the definition of attribute set '{0}'.", new string[] { xmlQualifiedName.ToString() });
				}
				if (obj2 != "D")
				{
					markTable[xmlQualifiedName] = "P";
					this.CheckAttributeSets_RecurceInContainer(markTable, this.GetAttributeSet(xmlQualifiedName));
					markTable[xmlQualifiedName] = "D";
				}
			}
		}

		// Token: 0x06003636 RID: 13878 RVA: 0x0012FAE4 File Offset: 0x0012DCE4
		private void CheckAttributeSets_RecurceInContainer(Hashtable markTable, ContainerAction container)
		{
			if (container.containedActions == null)
			{
				return;
			}
			foreach (object obj in container.containedActions)
			{
				Action action = (Action)obj;
				if (action is UseAttributeSetsAction)
				{
					this.CheckAttributeSets_RecurceInList(markTable, ((UseAttributeSetsAction)action).UsedSets);
				}
				else if (action is ContainerAction)
				{
					this.CheckAttributeSets_RecurceInContainer(markTable, (ContainerAction)action);
				}
			}
		}

		// Token: 0x06003637 RID: 13879 RVA: 0x0012FB70 File Offset: 0x0012DD70
		internal void AddDecimalFormat(XmlQualifiedName name, DecimalFormat formatinfo)
		{
			DecimalFormat decimalFormat = (DecimalFormat)this.decimalFormatTable[name];
			if (decimalFormat != null)
			{
				NumberFormatInfo info = decimalFormat.info;
				NumberFormatInfo info2 = formatinfo.info;
				if (info.NumberDecimalSeparator != info2.NumberDecimalSeparator || info.NumberGroupSeparator != info2.NumberGroupSeparator || info.PositiveInfinitySymbol != info2.PositiveInfinitySymbol || info.NegativeSign != info2.NegativeSign || info.NaNSymbol != info2.NaNSymbol || info.PercentSymbol != info2.PercentSymbol || info.PerMilleSymbol != info2.PerMilleSymbol || decimalFormat.zeroDigit != formatinfo.zeroDigit || decimalFormat.digit != formatinfo.digit || decimalFormat.patternSeparator != formatinfo.patternSeparator)
				{
					throw XsltException.Create("Decimal format '{0}' has a duplicate declaration.", new string[] { name.ToString() });
				}
			}
			this.decimalFormatTable[name] = formatinfo;
		}

		// Token: 0x06003638 RID: 13880 RVA: 0x0012FC7F File Offset: 0x0012DE7F
		internal DecimalFormat GetDecimalFormat(XmlQualifiedName name)
		{
			return this.decimalFormatTable[name] as DecimalFormat;
		}

		// Token: 0x17000B7D RID: 2941
		// (get) Token: 0x06003639 RID: 13881 RVA: 0x0012FC92 File Offset: 0x0012DE92
		internal List<Key> KeyList
		{
			get
			{
				return this.keyList;
			}
		}

		// Token: 0x0600363A RID: 13882 RVA: 0x0012FC9C File Offset: 0x0012DE9C
		internal override void Execute(Processor processor, ActionFrame frame)
		{
			switch (frame.State)
			{
			case 0:
			{
				frame.AllocateVariables(this.variableCount);
				XPathNavigator xpathNavigator = processor.Document.Clone();
				xpathNavigator.MoveToRoot();
				frame.InitNodeSet(new XPathSingletonIterator(xpathNavigator));
				if (this.containedActions != null && this.containedActions.Count > 0)
				{
					processor.PushActionFrame(frame);
				}
				frame.State = 2;
				return;
			}
			case 1:
				break;
			case 2:
				frame.NextNode(processor);
				if (processor.Debugger != null)
				{
					processor.PopDebuggerStack();
				}
				processor.PushTemplateLookup(frame.NodeSet, null, null);
				frame.State = 3;
				return;
			case 3:
				frame.Finished();
				break;
			default:
				return;
			}
		}

		// Token: 0x04002280 RID: 8832
		private const int QueryInitialized = 2;

		// Token: 0x04002281 RID: 8833
		private const int RootProcessed = 3;

		// Token: 0x04002282 RID: 8834
		private Hashtable attributeSetTable = new Hashtable();

		// Token: 0x04002283 RID: 8835
		private Hashtable decimalFormatTable = new Hashtable();

		// Token: 0x04002284 RID: 8836
		private List<Key> keyList;

		// Token: 0x04002285 RID: 8837
		private XsltOutput output;

		// Token: 0x04002286 RID: 8838
		public Stylesheet builtInSheet;

		// Token: 0x04002287 RID: 8839
		public PermissionSet permissions;
	}
}
