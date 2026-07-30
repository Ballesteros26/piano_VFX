using System;
using System.CodeDom;
using System.Runtime.Serialization;

namespace System.ComponentModel.Design.Serialization
{
	/// <summary>The exception that is thrown when line number information is available for a serialization error.</summary>
	// Token: 0x0200014F RID: 335
	[Serializable]
	public class CodeDomSerializerException : SystemException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Design.Serialization.CodeDomSerializerException" /> class using the specified exception and line information.</summary>
		/// <param name="ex">The exception to throw. </param>
		/// <param name="linePragma">A <see cref="T:System.CodeDom.CodeLinePragma" /> that indicates where the exception occurred. </param>
		// Token: 0x06000A2D RID: 2605 RVA: 0x00014764 File Offset: 0x00012964
		public CodeDomSerializerException(Exception ex, CodeLinePragma linePragma)
			: base(string.Empty, ex)
		{
			this.linePragma = linePragma;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Design.Serialization.CodeDomSerializerException" /> class using the specified message and line information.</summary>
		/// <param name="message">A message describing the exception. </param>
		/// <param name="linePragma">A <see cref="T:System.CodeDom.CodeLinePragma" /> that indicates where the exception occurred. </param>
		// Token: 0x06000A2E RID: 2606 RVA: 0x00014779 File Offset: 0x00012979
		public CodeDomSerializerException(string message, CodeLinePragma linePragma)
			: base(message)
		{
			this.linePragma = linePragma;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Design.Serialization.CodeDomSerializerException" /> class using the specified serialization data and context.</summary>
		/// <param name="info">Stores the data that was being used to serialize or deserialize the object that the <see cref="T:System.ComponentModel.Design.Serialization.CodeDomSerializer" /> was serializing or deserializing. </param>
		/// <param name="context">Describes the source and destination of the stream that generated the exception, as well as a means for serialization to retain that context and an additional caller-defined context. </param>
		// Token: 0x06000A2F RID: 2607 RVA: 0x00014789 File Offset: 0x00012989
		[MonoTODO]
		protected CodeDomSerializerException(SerializationInfo info, StreamingContext context)
		{
			throw new NotImplementedException();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Design.Serialization.CodeDomSerializerException" /> class. </summary>
		/// <param name="message">A message describing the exception. </param>
		/// <param name="manager">The <see cref="T:System.ComponentModel.Design.Serialization.IDesignerSerializationManager" /> object from which to extract the context.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="Manager" /> is null.</exception>
		// Token: 0x06000A30 RID: 2608 RVA: 0x00014789 File Offset: 0x00012989
		[MonoTODO]
		public CodeDomSerializerException(string message, IDesignerSerializationManager manager)
		{
			throw new NotImplementedException();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Design.Serialization.CodeDomSerializerException" /> class. </summary>
		/// <param name="ex">The exception to throw. </param>
		/// <param name="manager">The <see cref="T:System.ComponentModel.Design.Serialization.IDesignerSerializationManager" /> object from which to extract the context.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="Manager" /> is null.</exception>
		// Token: 0x06000A31 RID: 2609 RVA: 0x00014789 File Offset: 0x00012989
		[MonoTODO]
		public CodeDomSerializerException(Exception ex, IDesignerSerializationManager manager)
		{
			throw new NotImplementedException();
		}

		/// <summary>Sets the <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object with the parameter name and additional exception information.</summary>
		/// <param name="info">Stores the data that was being used to serialize or deserialize the object that the <see cref="T:System.ComponentModel.Design.Serialization.CodeDomSerializer" /> was serializing or deserializing. </param>
		/// <param name="context">Describes the source and destination of the stream that generated the exception, as well as a means for serialization to retain that context and an additional caller-defined context. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="info" /> parameter is null. </exception>
		// Token: 0x06000A32 RID: 2610 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoTODO]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			throw new NotImplementedException();
		}

		/// <summary>Gets or sets the line information for the error associated with this exception.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeLinePragma" /> that indicates the line information for the error.</returns>
		// Token: 0x17000219 RID: 537
		// (get) Token: 0x06000A33 RID: 2611 RVA: 0x00014796 File Offset: 0x00012996
		public CodeLinePragma LinePragma
		{
			get
			{
				return this.linePragma;
			}
		}

		// Token: 0x04000254 RID: 596
		private CodeLinePragma linePragma;
	}
}
