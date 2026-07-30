using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace System.Data.Design
{
	/// <summary>The exception that is thrown when a name conflict occurs while a strongly typed <see cref="T:System.Data.DataSet" /> is being generated.</summary>
	// Token: 0x020000EE RID: 238
	[Serializable]
	public class TypedDataSetGeneratorException : DataException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Data.Design.TypedDataSetGeneratorException" /> class with a system-supplied message that describes the error.</summary>
		// Token: 0x060006CB RID: 1739 RVA: 0x0000A592 File Offset: 0x00008792
		public TypedDataSetGeneratorException()
			: base(Locale.GetText("System error."))
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.Design.TypedDataSetGeneratorException" /> class by passing in a collection of errors.</summary>
		/// <param name="list">An <see cref="T:System.Collections.IList" /> of errors.</param>
		// Token: 0x060006CC RID: 1740 RVA: 0x0000A5A4 File Offset: 0x000087A4
		public TypedDataSetGeneratorException(IList list)
			: base(Locale.GetText("System error."))
		{
			this.errorList = list;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.Design.TypedDataSetGeneratorException" /> class, using the specified serialization information and streaming context.</summary>
		/// <param name="info">A <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object.</param>
		/// <param name="context">A <see cref="T:System.Runtime.Serialization.StreamingContext" /> structure.</param>
		// Token: 0x060006CD RID: 1741 RVA: 0x0000A5C0 File Offset: 0x000087C0
		protected TypedDataSetGeneratorException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			int @int = info.GetInt32("KEY_ARRAYCOUNT");
			this.errorList = new ArrayList(@int);
			for (int i = 0; i < @int; i++)
			{
				this.errorList.Add(info.GetString("KEY_ARRAYVALUES" + i));
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.Design.TypedDataSetGeneratorException" /> class with a specified message that describes the error.</summary>
		/// <param name="message">The message that describes the exception. The caller of this constructor is required to ensure that this string has been localized for the current system culture.</param>
		// Token: 0x060006CE RID: 1742 RVA: 0x0000A61B File Offset: 0x0000881B
		public TypedDataSetGeneratorException(string message)
			: base(message)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.Design.TypedDataSetGeneratorException" /> class with the specified string and inner exception.</summary>
		/// <param name="message">The message that describes the exception. The caller of this constructor is required to ensure that this string has been localized for the current system culture. </param>
		/// <param name="innerException">The exception that is the cause of the current exception. If the <paramref name="innerException" /> parameter is not null, the current exception is raised in a catch block that handles the inner exception. </param>
		// Token: 0x060006CF RID: 1743 RVA: 0x0000A624 File Offset: 0x00008824
		public TypedDataSetGeneratorException(string message, Exception innerException)
			: base(message, innerException)
		{
		}

		/// <summary>Gets a dynamic list of generated errors.</summary>
		/// <returns>The error list.</returns>
		// Token: 0x1700019A RID: 410
		// (get) Token: 0x060006D0 RID: 1744 RVA: 0x0000A62E File Offset: 0x0000882E
		public IList ErrorList
		{
			get
			{
				return this.errorList;
			}
		}

		/// <summary>Implements the ISerializable interface and returns the data that you must have to serialize the <see cref="T:System.Data.Design.TypedDataSetGeneratorException" /> object.</summary>
		/// <param name="info">A <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object.</param>
		/// <param name="context">A <see cref="T:System.Runtime.Serialization.StreamingContext" /> structure.</param>
		// Token: 0x060006D1 RID: 1745 RVA: 0x0000A638 File Offset: 0x00008838
		[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			int num = ((this.errorList != null) ? this.ErrorList.Count : 0);
			info.AddValue("KEY_ARRAYCOUNT", num);
			for (int i = 0; i < num; i++)
			{
				info.AddValue("KEY_ARRAYVALUES" + i, this.ErrorList[i]);
			}
		}

		// Token: 0x04000163 RID: 355
		private IList errorList;
	}
}
