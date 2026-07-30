using System;
using Unity;

namespace System.Xml.Schema
{
	/// <summary>Returns detailed information related to the ValidationEventHandler.</summary>
	// Token: 0x02000421 RID: 1057
	public class ValidationEventArgs : EventArgs
	{
		// Token: 0x060029C3 RID: 10691 RVA: 0x0010010F File Offset: 0x000FE30F
		internal ValidationEventArgs(XmlSchemaException ex)
		{
			this.ex = ex;
			this.severity = XmlSeverityType.Error;
		}

		// Token: 0x060029C4 RID: 10692 RVA: 0x00100125 File Offset: 0x000FE325
		internal ValidationEventArgs(XmlSchemaException ex, XmlSeverityType severity)
		{
			this.ex = ex;
			this.severity = severity;
		}

		/// <summary>Gets the severity of the validation event.</summary>
		/// <returns>An <see cref="T:System.Xml.Schema.XmlSeverityType" /> value representing the severity of the validation event.</returns>
		// Token: 0x170008C4 RID: 2244
		// (get) Token: 0x060029C5 RID: 10693 RVA: 0x0010013B File Offset: 0x000FE33B
		public XmlSeverityType Severity
		{
			get
			{
				return this.severity;
			}
		}

		/// <summary>Gets the <see cref="T:System.Xml.Schema.XmlSchemaException" /> associated with the validation event.</summary>
		/// <returns>The XmlSchemaException associated with the validation event.</returns>
		// Token: 0x170008C5 RID: 2245
		// (get) Token: 0x060029C6 RID: 10694 RVA: 0x00100143 File Offset: 0x000FE343
		public XmlSchemaException Exception
		{
			get
			{
				return this.ex;
			}
		}

		/// <summary>Gets the text description corresponding to the validation event.</summary>
		/// <returns>The text description.</returns>
		// Token: 0x170008C6 RID: 2246
		// (get) Token: 0x060029C7 RID: 10695 RVA: 0x0010014B File Offset: 0x000FE34B
		public string Message
		{
			get
			{
				return this.ex.Message;
			}
		}

		// Token: 0x060029C8 RID: 10696 RVA: 0x000728B0 File Offset: 0x00070AB0
		internal ValidationEventArgs()
		{
			ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x04001C5E RID: 7262
		private XmlSchemaException ex;

		// Token: 0x04001C5F RID: 7263
		private XmlSeverityType severity;
	}
}
