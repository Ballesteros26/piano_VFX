using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace System.Xml.Schema
{
	/// <summary>Returns information about errors encountered by the <see cref="T:System.Xml.Schema.XmlSchemaInference" /> class while inferring a schema from an XML document.</summary>
	// Token: 0x02000404 RID: 1028
	[Serializable]
	public class XmlSchemaInferenceException : XmlSchemaException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Schema.XmlSchemaInferenceException" /> class with the <see cref="T:System.Runtime.Serialization.SerializationInfo" /> and <see cref="T:System.Runtime.Serialization.StreamingContext" /> objects specified that contain all the properties of the <see cref="T:System.Xml.Schema.XmlSchemaInferenceException" />.</summary>
		/// <param name="info">A <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object.</param>
		/// <param name="context">A <see cref="T:System.Runtime.Serialization.StreamingContext" /> object.</param>
		// Token: 0x060027DF RID: 10207 RVA: 0x000EC63F File Offset: 0x000EA83F
		protected XmlSchemaInferenceException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		/// <summary>Streams all the <see cref="T:System.Xml.Schema.XmlSchemaInferenceException" /> object properties into the <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object specified for the <see cref="T:System.Runtime.Serialization.StreamingContext" /> object specified.</summary>
		/// <param name="info">A <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object.</param>
		/// <param name="context">A <see cref="T:System.Runtime.Serialization.StreamingContext" /> object.</param>
		// Token: 0x060027E0 RID: 10208 RVA: 0x000EC649 File Offset: 0x000EA849
		[SecurityPermission(SecurityAction.LinkDemand, SerializationFormatter = true)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Schema.XmlSchemaInferenceException" /> class.</summary>
		// Token: 0x060027E1 RID: 10209 RVA: 0x000EC653 File Offset: 0x000EA853
		public XmlSchemaInferenceException()
			: base(null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Schema.XmlSchemaInferenceException" /> class with the error message specified.</summary>
		/// <param name="message">A description of the error.</param>
		// Token: 0x060027E2 RID: 10210 RVA: 0x000EC65C File Offset: 0x000EA85C
		public XmlSchemaInferenceException(string message)
			: base(message, null, 0, 0)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Schema.XmlSchemaInferenceException" /> class with the error message specified and the original <see cref="T:System.Exception" /> that caused the <see cref="T:System.Xml.Schema.XmlSchemaInferenceException" /> specified.</summary>
		/// <param name="message">A description of the error.</param>
		/// <param name="innerException">An <see cref="T:System.Exception" /> object containing the original exception that caused the <see cref="T:System.Xml.Schema.XmlSchemaInferenceException" />.</param>
		// Token: 0x060027E3 RID: 10211 RVA: 0x000EC668 File Offset: 0x000EA868
		public XmlSchemaInferenceException(string message, Exception innerException)
			: base(message, innerException, 0, 0)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Schema.XmlSchemaInferenceException" /> class with the error message specified, the original <see cref="T:System.Exception" /> that caused the <see cref="T:System.Xml.Schema.XmlSchemaInferenceException" /> specified, and the line number and line position of the error in the XML document specified.</summary>
		/// <param name="message">A description of the error.</param>
		/// <param name="innerException">An <see cref="T:System.Exception" /> object containing the original exception that caused the <see cref="T:System.Xml.Schema.XmlSchemaInferenceException" />.</param>
		/// <param name="lineNumber">The line number in the XML document that caused the <see cref="T:System.Xml.Schema.XmlSchemaInferenceException" />.</param>
		/// <param name="linePosition">The line position in the XML document that caused the <see cref="T:System.Xml.Schema.XmlSchemaInferenceException" />.</param>
		// Token: 0x060027E4 RID: 10212 RVA: 0x000EC674 File Offset: 0x000EA874
		public XmlSchemaInferenceException(string message, Exception innerException, int lineNumber, int linePosition)
			: base(message, innerException, lineNumber, linePosition)
		{
		}

		// Token: 0x060027E5 RID: 10213 RVA: 0x000EC681 File Offset: 0x000EA881
		internal XmlSchemaInferenceException(string res, string[] args)
			: base(res, args, null, null, 0, 0, null)
		{
		}

		// Token: 0x060027E6 RID: 10214 RVA: 0x000EC690 File Offset: 0x000EA890
		internal XmlSchemaInferenceException(string res, string arg)
			: base(res, new string[] { arg }, null, null, 0, 0, null)
		{
		}

		// Token: 0x060027E7 RID: 10215 RVA: 0x000EC6A8 File Offset: 0x000EA8A8
		internal XmlSchemaInferenceException(string res, string arg, string sourceUri, int lineNumber, int linePosition)
			: base(res, new string[] { arg }, null, sourceUri, lineNumber, linePosition, null)
		{
		}

		// Token: 0x060027E8 RID: 10216 RVA: 0x000EC6C2 File Offset: 0x000EA8C2
		internal XmlSchemaInferenceException(string res, string sourceUri, int lineNumber, int linePosition)
			: base(res, null, null, sourceUri, lineNumber, linePosition, null)
		{
		}

		// Token: 0x060027E9 RID: 10217 RVA: 0x000EC6D2 File Offset: 0x000EA8D2
		internal XmlSchemaInferenceException(string res, string[] args, string sourceUri, int lineNumber, int linePosition)
			: base(res, args, null, sourceUri, lineNumber, linePosition, null)
		{
		}

		// Token: 0x060027EA RID: 10218 RVA: 0x000EC6E3 File Offset: 0x000EA8E3
		internal XmlSchemaInferenceException(string res, int lineNumber, int linePosition)
			: base(res, null, null, null, lineNumber, linePosition, null)
		{
		}
	}
}
