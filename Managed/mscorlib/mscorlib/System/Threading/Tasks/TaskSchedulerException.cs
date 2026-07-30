using System;
using System.Runtime.Serialization;

namespace System.Threading.Tasks
{
	/// <summary>Represents an exception used to communicate an invalid operation by a <see cref="T:System.Threading.Tasks.TaskScheduler" />.</summary>
	// Token: 0x02000522 RID: 1314
	[Serializable]
	public class TaskSchedulerException : Exception
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Threading.Tasks.TaskSchedulerException" /> class with a system-supplied message that describes the error.</summary>
		// Token: 0x06003BFA RID: 15354 RVA: 0x000D8AB0 File Offset: 0x000D6CB0
		public TaskSchedulerException()
			: base(Environment.GetResourceString("An exception was thrown by a TaskScheduler."))
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Threading.Tasks.TaskSchedulerException" /> class with a specified message that describes the error.</summary>
		/// <param name="message">The message that describes the exception. The caller of this constructor is required to ensure that this string has been localized for the current system culture.</param>
		// Token: 0x06003BFB RID: 15355 RVA: 0x00047B84 File Offset: 0x00045D84
		public TaskSchedulerException(string message)
			: base(message)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Threading.Tasks.TaskSchedulerException" /> class using the default error message and a reference to the inner exception that is the cause of this exception.</summary>
		/// <param name="innerException">The exception that is the cause of the current exception.</param>
		// Token: 0x06003BFC RID: 15356 RVA: 0x000D8AC2 File Offset: 0x000D6CC2
		public TaskSchedulerException(Exception innerException)
			: base(Environment.GetResourceString("An exception was thrown by a TaskScheduler."), innerException)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Threading.Tasks.TaskSchedulerException" /> class with a specified error message and a reference to the inner exception that is the cause of this exception.</summary>
		/// <param name="message">The message that describes the exception. The caller of this constructor is required to ensure that this string has been localized for the current system culture. </param>
		/// <param name="innerException">The exception that is the cause of the current exception. If the <paramref name="innerException" /> parameter is not null, the current exception is raised in a catch block that handles the inner exception. </param>
		// Token: 0x06003BFD RID: 15357 RVA: 0x00047B8D File Offset: 0x00045D8D
		public TaskSchedulerException(string message, Exception innerException)
			: base(message, innerException)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Threading.Tasks.TaskSchedulerException" /> class with serialized data.</summary>
		/// <param name="info">The object that holds the serialized object data. </param>
		/// <param name="context">The contextual information about the source or destination. </param>
		// Token: 0x06003BFE RID: 15358 RVA: 0x000325DC File Offset: 0x000307DC
		protected TaskSchedulerException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
