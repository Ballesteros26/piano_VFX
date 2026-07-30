using System;
using System.ComponentModel;
using System.Xml;

namespace System.Web.UI.WebControls
{
	// Token: 0x02000455 RID: 1109
	internal class XmlDataSourcePropertyDescriptor : PropertyDescriptor
	{
		// Token: 0x0600338C RID: 13196 RVA: 0x0008A03D File Offset: 0x0008823D
		public XmlDataSourcePropertyDescriptor(string name, bool readOnly)
			: base(name, null)
		{
			this.readOnly = readOnly;
		}

		// Token: 0x0600338D RID: 13197 RVA: 0x00008A69 File Offset: 0x00006C69
		public override bool CanResetValue(object o)
		{
			return false;
		}

		// Token: 0x0600338E RID: 13198 RVA: 0x0000393A File Offset: 0x00001B3A
		public override void ResetValue(object o)
		{
		}

		// Token: 0x0600338F RID: 13199 RVA: 0x0008A050 File Offset: 0x00088250
		public override object GetValue(object o)
		{
			XmlDataSourceNodeDescriptor xmlDataSourceNodeDescriptor = o as XmlDataSourceNodeDescriptor;
			if (xmlDataSourceNodeDescriptor != null && xmlDataSourceNodeDescriptor.Node.Attributes != null)
			{
				XmlAttribute xmlAttribute = xmlDataSourceNodeDescriptor.Node.Attributes[this.Name];
				if (xmlAttribute != null)
				{
					return xmlAttribute.Value;
				}
			}
			return string.Empty;
		}

		// Token: 0x06003390 RID: 13200 RVA: 0x0008A09C File Offset: 0x0008829C
		public override void SetValue(object o, object value)
		{
			XmlDataSourceNodeDescriptor xmlDataSourceNodeDescriptor = o as XmlDataSourceNodeDescriptor;
			if (xmlDataSourceNodeDescriptor != null && xmlDataSourceNodeDescriptor.Node.Attributes != null)
			{
				XmlAttribute xmlAttribute = xmlDataSourceNodeDescriptor.Node.Attributes[this.Name];
				if (xmlAttribute != null)
				{
					xmlAttribute.Value = value.ToString();
				}
			}
		}

		// Token: 0x06003391 RID: 13201 RVA: 0x0008A0E6 File Offset: 0x000882E6
		public override bool ShouldSerializeValue(object o)
		{
			return o is XmlNode;
		}

		// Token: 0x17001046 RID: 4166
		// (get) Token: 0x06003392 RID: 13202 RVA: 0x0008A0F1 File Offset: 0x000882F1
		public override Type ComponentType
		{
			get
			{
				return typeof(XmlDataSourceNodeDescriptor);
			}
		}

		// Token: 0x17001047 RID: 4167
		// (get) Token: 0x06003393 RID: 13203 RVA: 0x0008A0FD File Offset: 0x000882FD
		public override bool IsReadOnly
		{
			get
			{
				return this.readOnly;
			}
		}

		// Token: 0x17001048 RID: 4168
		// (get) Token: 0x06003394 RID: 13204 RVA: 0x000363B2 File Offset: 0x000345B2
		public override Type PropertyType
		{
			get
			{
				return typeof(string);
			}
		}

		// Token: 0x04001CD8 RID: 7384
		private bool readOnly;
	}
}
