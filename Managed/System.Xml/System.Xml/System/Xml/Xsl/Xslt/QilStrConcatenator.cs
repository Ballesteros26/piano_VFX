using System;
using System.Text;
using System.Xml.Xsl.Qil;
using System.Xml.Xsl.XPath;

namespace System.Xml.Xsl.Xslt
{
	// Token: 0x02000588 RID: 1416
	internal class QilStrConcatenator
	{
		// Token: 0x06003862 RID: 14434 RVA: 0x0013D0C8 File Offset: 0x0013B2C8
		public QilStrConcatenator(XPathQilFactory f)
		{
			this.f = f;
			this.builder = new StringBuilder();
		}

		// Token: 0x06003863 RID: 14435 RVA: 0x0013D0E2 File Offset: 0x0013B2E2
		public void Reset()
		{
			this.inUse = true;
			this.builder.Length = 0;
			this.concat = null;
		}

		// Token: 0x06003864 RID: 14436 RVA: 0x0013D100 File Offset: 0x0013B300
		private void FlushBuilder()
		{
			if (this.concat == null)
			{
				this.concat = this.f.BaseFactory.Sequence();
			}
			if (this.builder.Length != 0)
			{
				this.concat.Add(this.f.String(this.builder.ToString()));
				this.builder.Length = 0;
			}
		}

		// Token: 0x06003865 RID: 14437 RVA: 0x0013D165 File Offset: 0x0013B365
		public void Append(string value)
		{
			this.builder.Append(value);
		}

		// Token: 0x06003866 RID: 14438 RVA: 0x0013D174 File Offset: 0x0013B374
		public void Append(char value)
		{
			this.builder.Append(value);
		}

		// Token: 0x06003867 RID: 14439 RVA: 0x0013D183 File Offset: 0x0013B383
		public void Append(QilNode value)
		{
			if (value != null)
			{
				if (value.NodeType == QilNodeType.LiteralString)
				{
					this.builder.Append((QilLiteral)value);
					return;
				}
				this.FlushBuilder();
				this.concat.Add(value);
			}
		}

		// Token: 0x06003868 RID: 14440 RVA: 0x0013D1BC File Offset: 0x0013B3BC
		public QilNode ToQil()
		{
			this.inUse = false;
			if (this.concat == null)
			{
				return this.f.String(this.builder.ToString());
			}
			this.FlushBuilder();
			return this.f.StrConcat(this.concat);
		}

		// Token: 0x0400248D RID: 9357
		private XPathQilFactory f;

		// Token: 0x0400248E RID: 9358
		private StringBuilder builder;

		// Token: 0x0400248F RID: 9359
		private QilList concat;

		// Token: 0x04002490 RID: 9360
		private bool inUse;
	}
}
