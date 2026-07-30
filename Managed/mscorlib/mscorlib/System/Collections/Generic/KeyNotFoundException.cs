using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System.Collections.Generic
{
	/// <summary>The exception that is thrown when the key specified for accessing an element in a collection does not match any key in the collection.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x02000A57 RID: 2647
	[ComVisible(true)]
	[Serializable]
	public class KeyNotFoundException : SystemException, ISerializable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.Generic.KeyNotFoundException" /> class using default property values.</summary>
		// Token: 0x060060F6 RID: 24822 RVA: 0x0013E978 File Offset: 0x0013CB78
		public KeyNotFoundException()
			: base(Environment.GetResourceString("The given key was not present in the dictionary."))
		{
			base.SetErrorCode(-2146232969);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.Generic.KeyNotFoundException" /> class with the specified error message.</summary>
		/// <param name="message">The message that describes the error.</param>
		// Token: 0x060060F7 RID: 24823 RVA: 0x0013E995 File Offset: 0x0013CB95
		public KeyNotFoundException(string message)
			: base(message)
		{
			base.SetErrorCode(-2146232969);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.Generic.KeyNotFoundException" /> class with the specified error message and a reference to the inner exception that is the cause of this exception.</summary>
		/// <param name="message">The error message that explains the reason for the exception.</param>
		/// <param name="innerException">The exception that is the cause of the current exception. If the <paramref name="innerException" /> parameter is not null, the current exception is raised in a catch block that handles the inner exception.</param>
		// Token: 0x060060F8 RID: 24824 RVA: 0x0013E9A9 File Offset: 0x0013CBA9
		public KeyNotFoundException(string message, Exception innerException)
			: base(message, innerException)
		{
			base.SetErrorCode(-2146232969);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.Generic.KeyNotFoundException" /> class with serialized data.</summary>
		/// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> that holds the serialized object data about the exception being thrown.</param>
		/// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext" />  that contains contextual information about the source or destination.</param>
		// Token: 0x060060F9 RID: 24825 RVA: 0x00031FC1 File Offset: 0x000301C1
		protected KeyNotFoundException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
