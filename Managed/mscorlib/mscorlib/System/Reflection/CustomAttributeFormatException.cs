using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System.Reflection
{
	/// <summary>The exception that is thrown when the binary format of a custom attribute is invalid.</summary>
	// Token: 0x02000312 RID: 786
	[ComVisible(true)]
	[Serializable]
	public class CustomAttributeFormatException : FormatException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Reflection.CustomAttributeFormatException" /> class with the default properties.</summary>
		// Token: 0x06002263 RID: 8803 RVA: 0x0008144F File Offset: 0x0007F64F
		public CustomAttributeFormatException()
			: base(Locale.GetText("The Binary format of the custom attribute is invalid."))
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Reflection.CustomAttributeFormatException" /> class with the specified message.</summary>
		/// <param name="message">The message that indicates the reason this exception was thrown. </param>
		// Token: 0x06002264 RID: 8804 RVA: 0x00081461 File Offset: 0x0007F661
		public CustomAttributeFormatException(string message)
			: base(message)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Reflection.CustomAttributeFormatException" /> class with a specified error message and a reference to the inner exception that is the cause of this exception.</summary>
		/// <param name="message">The error message that explains the reason for the exception. </param>
		/// <param name="inner">The exception that is the cause of the current exception. If the <paramref name="inner" /> parameter is not null, the current exception is raised in a catch block that handles the inner exception. </param>
		// Token: 0x06002265 RID: 8805 RVA: 0x0008146A File Offset: 0x0007F66A
		public CustomAttributeFormatException(string message, Exception inner)
			: base(message, inner)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Reflection.CustomAttributeFormatException" /> class with the specified serialization and context information.</summary>
		/// <param name="info">The data for serializing or deserializing the custom attribute. </param>
		/// <param name="context">The source and destination for the custom attribute. </param>
		// Token: 0x06002266 RID: 8806 RVA: 0x00081474 File Offset: 0x0007F674
		protected CustomAttributeFormatException(SerializationInfo info, StreamingContext context)
		{
		}
	}
}
