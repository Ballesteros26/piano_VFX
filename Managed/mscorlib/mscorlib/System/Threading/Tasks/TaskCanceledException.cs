using System;
using System.Runtime.Serialization;

namespace System.Threading.Tasks
{
	/// <summary>Represents an exception used to communicate task cancellation.</summary>
	// Token: 0x0200050B RID: 1291
	[Serializable]
	public class TaskCanceledException : OperationCanceledException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Threading.Tasks.TaskCanceledException" /> class with a system-supplied message that describes the error.</summary>
		// Token: 0x06003B1D RID: 15133 RVA: 0x000D64BD File Offset: 0x000D46BD
		public TaskCanceledException()
			: base(Environment.GetResourceString("A task was canceled."))
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Threading.Tasks.TaskCanceledException" /> class with a specified message that describes the error.</summary>
		/// <param name="message">The message that describes the exception. The caller of this constructor is required to ensure that this string has been localized for the current system culture.</param>
		// Token: 0x06003B1E RID: 15134 RVA: 0x000D64CF File Offset: 0x000D46CF
		public TaskCanceledException(string message)
			: base(message)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Threading.Tasks.TaskCanceledException" /> class with a specified error message and a reference to the inner exception that is the cause of this exception.</summary>
		/// <param name="message">The message that describes the exception. The caller of this constructor is required to ensure that this string has been localized for the current system culture. </param>
		/// <param name="innerException">The exception that is the cause of the current exception. If the <paramref name="innerException" /> parameter is not null, the current exception is raised in a catch block that handles the inner exception. </param>
		// Token: 0x06003B1F RID: 15135 RVA: 0x000D64D8 File Offset: 0x000D46D8
		public TaskCanceledException(string message, Exception innerException)
			: base(message, innerException)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Threading.Tasks.TaskCanceledException" /> class with a reference to the <see cref="T:System.Threading.Tasks.Task" /> that has been canceled.</summary>
		/// <param name="task">A task that has been canceled.</param>
		// Token: 0x06003B20 RID: 15136 RVA: 0x000D64E4 File Offset: 0x000D46E4
		public TaskCanceledException(Task task)
			: base(Environment.GetResourceString("A task was canceled."), (task != null) ? task.CancellationToken : default(CancellationToken))
		{
			this.m_canceledTask = task;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Threading.Tasks.TaskCanceledException" /> class with serialized data.</summary>
		/// <param name="info">The object that holds the serialized object data. </param>
		/// <param name="context">The contextual information about the source or destination. </param>
		// Token: 0x06003B21 RID: 15137 RVA: 0x000D651C File Offset: 0x000D471C
		protected TaskCanceledException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		/// <summary>Gets the task associated with this exception.</summary>
		/// <returns>A reference to the <see cref="T:System.Threading.Tasks.Task" /> that is associated with this exception.</returns>
		// Token: 0x170009B9 RID: 2489
		// (get) Token: 0x06003B22 RID: 15138 RVA: 0x000D6526 File Offset: 0x000D4726
		public Task Task
		{
			get
			{
				return this.m_canceledTask;
			}
		}

		// Token: 0x04001EE6 RID: 7910
		[NonSerialized]
		private Task m_canceledTask;
	}
}
