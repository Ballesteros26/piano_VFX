using System;
using System.Collections;
using System.Xml.XPath;
using MS.Internal.Xml.XPath;

namespace System.Xml.Schema
{
	// Token: 0x02000388 RID: 904
	internal class Asttree
	{
		// Token: 0x1700073F RID: 1855
		// (get) Token: 0x06002489 RID: 9353 RVA: 0x000DE192 File Offset: 0x000DC392
		internal ArrayList SubtreeArray
		{
			get
			{
				return this.fAxisArray;
			}
		}

		// Token: 0x0600248A RID: 9354 RVA: 0x000DE19A File Offset: 0x000DC39A
		public Asttree(string xPath, bool isField, XmlNamespaceManager nsmgr)
		{
			this.xpathexpr = xPath;
			this.isField = isField;
			this.nsmgr = nsmgr;
			this.CompileXPath(xPath, isField, nsmgr);
		}

		// Token: 0x0600248B RID: 9355 RVA: 0x000DE1C0 File Offset: 0x000DC3C0
		private static bool IsNameTest(Axis ast)
		{
			return ast.TypeOfAxis == Axis.AxisType.Child && ast.NodeType == XPathNodeType.Element;
		}

		// Token: 0x0600248C RID: 9356 RVA: 0x000DE1D6 File Offset: 0x000DC3D6
		internal static bool IsAttribute(Axis ast)
		{
			return ast.TypeOfAxis == Axis.AxisType.Attribute && ast.NodeType == XPathNodeType.Attribute;
		}

		// Token: 0x0600248D RID: 9357 RVA: 0x000DE1EC File Offset: 0x000DC3EC
		private static bool IsDescendantOrSelf(Axis ast)
		{
			return ast.TypeOfAxis == Axis.AxisType.DescendantOrSelf && ast.NodeType == XPathNodeType.All && ast.AbbrAxis;
		}

		// Token: 0x0600248E RID: 9358 RVA: 0x000DE209 File Offset: 0x000DC409
		internal static bool IsSelf(Axis ast)
		{
			return ast.TypeOfAxis == Axis.AxisType.Self && ast.NodeType == XPathNodeType.All && ast.AbbrAxis;
		}

		// Token: 0x0600248F RID: 9359 RVA: 0x000DE228 File Offset: 0x000DC428
		public void CompileXPath(string xPath, bool isField, XmlNamespaceManager nsmgr)
		{
			if (xPath == null || xPath.Length == 0)
			{
				throw new XmlSchemaException("The XPath for selector or field cannot be empty.", string.Empty);
			}
			string[] array = xPath.Split(new char[] { '|' });
			ArrayList arrayList = new ArrayList(array.Length);
			this.fAxisArray = new ArrayList(array.Length);
			try
			{
				for (int i = 0; i < array.Length; i++)
				{
					Axis axis = (Axis)XPathParser.ParseXPathExpresion(array[i]);
					arrayList.Add(axis);
				}
			}
			catch
			{
				throw new XmlSchemaException("'{0}' is an invalid XPath for selector or field.", xPath);
			}
			int j = 0;
			while (j < arrayList.Count)
			{
				Axis axis2 = (Axis)arrayList[j];
				Axis axis3;
				if ((axis3 = axis2) == null)
				{
					throw new XmlSchemaException("'{0}' is an invalid XPath for selector or field.", xPath);
				}
				Axis axis4 = axis3;
				if (Asttree.IsAttribute(axis3))
				{
					if (!isField)
					{
						throw new XmlSchemaException("'{0}' is an invalid XPath for selector. Selector cannot have an XPath selection with an attribute node.", xPath);
					}
					this.SetURN(axis3, nsmgr);
					try
					{
						axis3 = (Axis)axis3.Input;
						goto IL_012A;
					}
					catch
					{
						throw new XmlSchemaException("'{0}' is an invalid XPath for selector or field.", xPath);
					}
					goto IL_00DF;
				}
				IL_012A:
				if (axis3 == null || (!Asttree.IsNameTest(axis3) && !Asttree.IsSelf(axis3)))
				{
					axis4.Input = null;
					if (axis3 == null)
					{
						if (Asttree.IsSelf(axis2) && axis2.Input != null)
						{
							this.fAxisArray.Add(new ForwardAxis(DoubleLinkAxis.ConvertTree((Axis)axis2.Input), false));
						}
						else
						{
							this.fAxisArray.Add(new ForwardAxis(DoubleLinkAxis.ConvertTree(axis2), false));
						}
					}
					else
					{
						if (!Asttree.IsDescendantOrSelf(axis3))
						{
							throw new XmlSchemaException("'{0}' is an invalid XPath for selector or field.", xPath);
						}
						try
						{
							axis3 = (Axis)axis3.Input;
						}
						catch
						{
							throw new XmlSchemaException("'{0}' is an invalid XPath for selector or field.", xPath);
						}
						if (axis3 == null || !Asttree.IsSelf(axis3) || axis3.Input != null)
						{
							throw new XmlSchemaException("'{0}' is an invalid XPath for selector or field.", xPath);
						}
						if (Asttree.IsSelf(axis2) && axis2.Input != null)
						{
							this.fAxisArray.Add(new ForwardAxis(DoubleLinkAxis.ConvertTree((Axis)axis2.Input), true));
						}
						else
						{
							this.fAxisArray.Add(new ForwardAxis(DoubleLinkAxis.ConvertTree(axis2), true));
						}
					}
					j++;
					continue;
				}
				IL_00DF:
				if (Asttree.IsSelf(axis3) && axis2 != axis3)
				{
					axis4.Input = axis3.Input;
				}
				else
				{
					axis4 = axis3;
					if (Asttree.IsNameTest(axis3))
					{
						this.SetURN(axis3, nsmgr);
					}
				}
				try
				{
					axis3 = (Axis)axis3.Input;
				}
				catch
				{
					throw new XmlSchemaException("'{0}' is an invalid XPath for selector or field.", xPath);
				}
				goto IL_012A;
			}
		}

		// Token: 0x06002490 RID: 9360 RVA: 0x000DE4BC File Offset: 0x000DC6BC
		private void SetURN(Axis axis, XmlNamespaceManager nsmgr)
		{
			if (axis.Prefix.Length != 0)
			{
				axis.Urn = nsmgr.LookupNamespace(axis.Prefix);
				if (axis.Urn == null)
				{
					throw new XmlSchemaException("The prefix '{0}' in XPath cannot be resolved.", axis.Prefix);
				}
			}
			else
			{
				if (axis.Name.Length != 0)
				{
					axis.Urn = null;
					return;
				}
				axis.Urn = "";
			}
		}

		// Token: 0x040018D8 RID: 6360
		private ArrayList fAxisArray;

		// Token: 0x040018D9 RID: 6361
		private string xpathexpr;

		// Token: 0x040018DA RID: 6362
		private bool isField;

		// Token: 0x040018DB RID: 6363
		private XmlNamespaceManager nsmgr;
	}
}
