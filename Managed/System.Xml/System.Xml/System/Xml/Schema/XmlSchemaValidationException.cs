using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace System.Xml.Schema
{
	/// <summary>Represents the exception thrown when XML Schema Definition Language (XSD) schema validation errors and warnings are encountered in an XML document being validated. </summary>
	// Token: 0x02000489 RID: 1161
	[Serializable]
	public class XmlSchemaValidationException : XmlSchemaException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Schema.XmlSchemaValidationException" /> class with the <see cref="T:System.Runtime.Serialization.SerializationInfo" /> and <see cref="T:System.Runtime.Serialization.StreamingContext" /> objects specified.</summary>
		/// <param name="info">A <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object.</param>
		/// <param name="context">A <see cref="T:System.Runtime.Serialization.StreamingContext" /> object.</param>
		// Token: 0x06002D95 RID: 11669 RVA: 0x000EC63F File Offset: 0x000EA83F
		protected XmlSchemaValidationException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		/// <summary>Constructs a new <see cref="T:System.Xml.Schema.XmlSchemaValidationException" /> object with the given <see cref="T:System.Runtime.Serialization.SerializationInfo" /> and <see cref="T:System.Runtime.Serialization.StreamingContext" /> information that contains all the properties of the <see cref="T:System.Xml.Schema.XmlSchemaValidationException" />.</summary>
		/// <param name="info">
		///   <see cref="T:System.Runtime.Serialization.SerializationInfo" />
		/// </param>
		/// <param name="context">
		///   <see cref="T:System.Runtime.Serialization.StreamingContext" />
		/// </param>
		// Token: 0x06002D96 RID: 11670 RVA: 0x000EC649 File Offset: 0x000EA849
		[SecurityPermission(SecurityAction.LinkDemand, SerializationFormatter = true)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Schema.XmlSchemaValidationException" /> class.</summary>
		// Token: 0x06002D97 RID: 11671 RVA: 0x000EC653 File Offset: 0x000EA853
		public XmlSchemaValidationException()
			: base(null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Schema.XmlSchemaValidationException" /> class with the exception message specified.</summary>
		/// <param name="message">A string description of the error condition.</param>
		// Token: 0x06002D98 RID: 11672 RVA: 0x000EC65C File Offset: 0x000EA85C
		public XmlSchemaValidationException(string message)
			: base(message, null, 0, 0)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Schema.XmlSchemaValidationException" /> class with the exception message and original <see cref="T:System.Exception" /> object that caused this exception specified.</summary>
		/// <param name="message">A string description of the error condition.</param>
		/// <param name="innerException">The original <see cref="T:System.Exception" /> object that caused this exception.</param>
		// Token: 0x06002D99 RID: 11673 RVA: 0x000EC668 File Offset: 0x000EA868
		public XmlSchemaValidationException(string message, Exception innerException)
			: base(message, innerException, 0, 0)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Schema.XmlSchemaValidationException" /> class with the exception message specified, and the original <see cref="T:System.Exception" /> object, line number, and line position of the XML that cause this exception specified.</summary>
		/// <param name="message">A string description of the error condition.</param>
		/// <param name="innerException">The original <see cref="T:System.Exception" /> object that caused this exception.</param>
		/// <param name="lineNumber">The line number of the XML that caused this exception.</param>
		/// <param name="linePosition">The line position of the XML that caused this exception.</param>
		// Token: 0x06002D9A RID: 11674 RVA: 0x000EC674 File Offset: 0x000EA874
		public XmlSchemaValidationException(string message, Exception innerException, int lineNumber, int linePosition)
			: base(message, innerException, lineNumber, linePosition)
		{
		}

		// Token: 0x06002D9B RID: 11675 RVA: 0x000EC681 File Offset: 0x000EA881
		internal XmlSchemaValidationException(string res, string[] args)
			: base(res, args, null, null, 0, 0, null)
		{
		}

		// Token: 0x06002D9C RID: 11676 RVA: 0x000EC690 File Offset: 0x000EA890
		internal XmlSchemaValidationException(string res, string arg)
			: base(res, new string[] { arg }, null, null, 0, 0, null)
		{
		}

		// Token: 0x06002D9D RID: 11677 RVA: 0x000EC6A8 File Offset: 0x000EA8A8
		internal XmlSchemaValidationException(string res, string arg, string sourceUri, int lineNumber, int linePosition)
			: base(res, new string[] { arg }, null, sourceUri, lineNumber, linePosition, null)
		{
		}

		// Token: 0x06002D9E RID: 11678 RVA: 0x000EC6C2 File Offset: 0x000EA8C2
		internal XmlSchemaValidationException(string res, string sourceUri, int lineNumber, int linePosition)
			: base(res, null, null, sourceUri, lineNumber, linePosition, null)
		{
		}

		// Token: 0x06002D9F RID: 11679 RVA: 0x000EC6D2 File Offset: 0x000EA8D2
		internal XmlSchemaValidationException(string res, string[] args, string sourceUri, int lineNumber, int linePosition)
			: base(res, args, null, sourceUri, lineNumber, linePosition, null)
		{
		}

		// Token: 0x06002DA0 RID: 11680 RVA: 0x0010A34F File Offset: 0x0010854F
		internal XmlSchemaValidationException(string res, string[] args, Exception innerException, string sourceUri, int lineNumber, int linePosition)
			: base(res, args, innerException, sourceUri, lineNumber, linePosition, null)
		{
		}

		// Token: 0x06002DA1 RID: 11681 RVA: 0x0010A361 File Offset: 0x00108561
		internal XmlSchemaValidationException(string res, string[] args, object sourceNode)
			: base(res, args, null, null, 0, 0, null)
		{
			this.sourceNodeObject = sourceNode;
		}

		// Token: 0x06002DA2 RID: 11682 RVA: 0x0010A377 File Offset: 0x00108577
		internal XmlSchemaValidationException(string res, string[] args, string sourceUri, object sourceNode)
			: base(res, args, null, sourceUri, 0, 0, null)
		{
			this.sourceNodeObject = sourceNode;
		}

		// Token: 0x06002DA3 RID: 11683 RVA: 0x0010A38E File Offset: 0x0010858E
		internal XmlSchemaValidationException(string res, string[] args, string sourceUri, int lineNumber, int linePosition, XmlSchemaObject source, object sourceNode)
			: base(res, args, null, sourceUri, lineNumber, linePosition, source)
		{
			this.sourceNodeObject = sourceNode;
		}

		/// <summary>Gets the XML node that caused this <see cref="T:System.Xml.Schema.XmlSchemaValidationException" />.</summary>
		/// <returns>The XML node that caused this <see cref="T:System.Xml.Schema.XmlSchemaValidationException" />.</returns>
		// Token: 0x17000A11 RID: 2577
		// (get) Token: 0x06002DA4 RID: 11684 RVA: 0x0010A3A8 File Offset: 0x001085A8
		public object SourceObject
		{
			get
			{
				return this.sourceNodeObject;
			}
		}

		/// <summary>Sets the XML node that causes the error.</summary>
		/// <param name="sourceObject">The source object.</param>
		// Token: 0x06002DA5 RID: 11685 RVA: 0x0010A3B0 File Offset: 0x001085B0
		protected internal void SetSourceObject(object sourceObject)
		{
			this.sourceNodeObject = sourceObject;
		}

		// Token: 0x04001E38 RID: 7736
		private object sourceNodeObject;
	}
}
