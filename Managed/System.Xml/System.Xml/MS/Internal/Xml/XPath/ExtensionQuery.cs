using System;
using System.Xml.XPath;
using System.Xml.Xsl;

namespace MS.Internal.Xml.XPath
{
	// Token: 0x0200001D RID: 29
	internal abstract class ExtensionQuery : Query
	{
		// Token: 0x060000B0 RID: 176 RVA: 0x0000366A File Offset: 0x0000186A
		public ExtensionQuery(string prefix, string name)
		{
			this.prefix = prefix;
			this.name = name;
		}

		// Token: 0x060000B1 RID: 177 RVA: 0x00003680 File Offset: 0x00001880
		protected ExtensionQuery(ExtensionQuery other)
			: base(other)
		{
			this.prefix = other.prefix;
			this.name = other.name;
			this.xsltContext = other.xsltContext;
			this.queryIterator = (ResetableIterator)Query.Clone(other.queryIterator);
		}

		// Token: 0x060000B2 RID: 178 RVA: 0x000036CE File Offset: 0x000018CE
		public override void Reset()
		{
			if (this.queryIterator != null)
			{
				this.queryIterator.Reset();
			}
		}

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x060000B3 RID: 179 RVA: 0x000036E3 File Offset: 0x000018E3
		public override XPathNavigator Current
		{
			get
			{
				if (this.queryIterator == null)
				{
					throw XPathException.Create("Expression must evaluate to a node-set.");
				}
				if (this.queryIterator.CurrentPosition == 0)
				{
					this.Advance();
				}
				return this.queryIterator.Current;
			}
		}

		// Token: 0x060000B4 RID: 180 RVA: 0x00003717 File Offset: 0x00001917
		public override XPathNavigator Advance()
		{
			if (this.queryIterator == null)
			{
				throw XPathException.Create("Expression must evaluate to a node-set.");
			}
			if (this.queryIterator.MoveNext())
			{
				return this.queryIterator.Current;
			}
			return null;
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x060000B5 RID: 181 RVA: 0x00003746 File Offset: 0x00001946
		public override int CurrentPosition
		{
			get
			{
				if (this.queryIterator != null)
				{
					return this.queryIterator.CurrentPosition;
				}
				return 0;
			}
		}

		// Token: 0x060000B6 RID: 182 RVA: 0x00003760 File Offset: 0x00001960
		protected object ProcessResult(object value)
		{
			if (value is string)
			{
				return value;
			}
			if (value is double)
			{
				return value;
			}
			if (value is bool)
			{
				return value;
			}
			if (value is XPathNavigator)
			{
				return value;
			}
			if (value is int)
			{
				return (double)((int)value);
			}
			if (value == null)
			{
				this.queryIterator = XPathEmptyIterator.Instance;
				return this;
			}
			ResetableIterator resetableIterator = value as ResetableIterator;
			if (resetableIterator != null)
			{
				this.queryIterator = (ResetableIterator)resetableIterator.Clone();
				return this;
			}
			XPathNodeIterator xpathNodeIterator = value as XPathNodeIterator;
			if (xpathNodeIterator != null)
			{
				this.queryIterator = new XPathArrayIterator(xpathNodeIterator);
				return this;
			}
			IXPathNavigable ixpathNavigable = value as IXPathNavigable;
			if (ixpathNavigable != null)
			{
				return ixpathNavigable.CreateNavigator();
			}
			if (value is short)
			{
				return (double)((short)value);
			}
			if (value is long)
			{
				return (double)((long)value);
			}
			if (value is uint)
			{
				return (uint)value;
			}
			if (value is ushort)
			{
				return (double)((ushort)value);
			}
			if (value is ulong)
			{
				return (ulong)value;
			}
			if (value is float)
			{
				return (double)((float)value);
			}
			if (value is decimal)
			{
				return (double)((decimal)value);
			}
			return value.ToString();
		}

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x060000B7 RID: 183 RVA: 0x000038A0 File Offset: 0x00001AA0
		protected string QName
		{
			get
			{
				if (this.prefix.Length == 0)
				{
					return this.name;
				}
				return this.prefix + ":" + this.name;
			}
		}

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x060000B8 RID: 184 RVA: 0x000038CC File Offset: 0x00001ACC
		public override int Count
		{
			get
			{
				if (this.queryIterator != null)
				{
					return this.queryIterator.Count;
				}
				return 1;
			}
		}

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x060000B9 RID: 185 RVA: 0x000038E3 File Offset: 0x00001AE3
		public override XPathResultType StaticType
		{
			get
			{
				return XPathResultType.Any;
			}
		}

		// Token: 0x0400007C RID: 124
		protected string prefix;

		// Token: 0x0400007D RID: 125
		protected string name;

		// Token: 0x0400007E RID: 126
		protected XsltContext xsltContext;

		// Token: 0x0400007F RID: 127
		private ResetableIterator queryIterator;
	}
}
