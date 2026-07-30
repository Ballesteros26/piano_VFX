using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System.Threading
{
	/// <summary>The exception that is thrown when one thread acquires a <see cref="T:System.Threading.Mutex" /> object that another thread has abandoned by exiting without releasing it.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x02000469 RID: 1129
	[ComVisible(false)]
	[Serializable]
	public class AbandonedMutexException : SystemException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Threading.AbandonedMutexException" /> class with default values.</summary>
		// Token: 0x060035AD RID: 13741 RVA: 0x000C6B2A File Offset: 0x000C4D2A
		public AbandonedMutexException()
			: base(Environment.GetResourceString("The wait completed due to an abandoned mutex."))
		{
			base.SetErrorCode(-2146233043);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Threading.AbandonedMutexException" /> class with a specified error message.</summary>
		/// <param name="message">An error message that explains the reason for the exception.</param>
		// Token: 0x060035AE RID: 13742 RVA: 0x000C6B4E File Offset: 0x000C4D4E
		public AbandonedMutexException(string message)
			: base(message)
		{
			base.SetErrorCode(-2146233043);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Threading.AbandonedMutexException" /> class with a specified error message and inner exception. </summary>
		/// <param name="message">An error message that explains the reason for the exception.</param>
		/// <param name="inner">The exception that is the cause of the current exception. If the <paramref name="inner" /> parameter is not null, the current exception is raised in a catch block that handles the inner exception.</param>
		// Token: 0x060035AF RID: 13743 RVA: 0x000C6B69 File Offset: 0x000C4D69
		public AbandonedMutexException(string message, Exception inner)
			: base(message, inner)
		{
			base.SetErrorCode(-2146233043);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Threading.AbandonedMutexException" /> class with a specified index for the abandoned mutex, if applicable, and a <see cref="T:System.Threading.Mutex" /> object that represents the mutex.</summary>
		/// <param name="location">The index of the abandoned mutex in the array of wait handles if the exception is thrown for the <see cref="Overload:System.Threading.WaitHandle.WaitAny" /> method, or –1 if the exception is thrown for the <see cref="Overload:System.Threading.WaitHandle.WaitOne" /> or <see cref="Overload:System.Threading.WaitHandle.WaitAll" /> methods.</param>
		/// <param name="handle">A <see cref="T:System.Threading.Mutex" /> object that represents the abandoned mutex.</param>
		// Token: 0x060035B0 RID: 13744 RVA: 0x000C6B85 File Offset: 0x000C4D85
		public AbandonedMutexException(int location, WaitHandle handle)
			: base(Environment.GetResourceString("The wait completed due to an abandoned mutex."))
		{
			base.SetErrorCode(-2146233043);
			this.SetupException(location, handle);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Threading.AbandonedMutexException" /> class with a specified error message, the index of the abandoned mutex, if applicable, and the abandoned mutex. </summary>
		/// <param name="message">An error message that explains the reason for the exception.</param>
		/// <param name="location">The index of the abandoned mutex in the array of wait handles if the exception is thrown for the <see cref="Overload:System.Threading.WaitHandle.WaitAny" /> method, or –1 if the exception is thrown for the <see cref="Overload:System.Threading.WaitHandle.WaitOne" /> or <see cref="Overload:System.Threading.WaitHandle.WaitAll" /> methods.</param>
		/// <param name="handle">A <see cref="T:System.Threading.Mutex" /> object that represents the abandoned mutex.</param>
		// Token: 0x060035B1 RID: 13745 RVA: 0x000C6BB1 File Offset: 0x000C4DB1
		public AbandonedMutexException(string message, int location, WaitHandle handle)
			: base(message)
		{
			base.SetErrorCode(-2146233043);
			this.SetupException(location, handle);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Threading.AbandonedMutexException" /> class with a specified error message, the inner exception, the index for the abandoned mutex, if applicable, and a <see cref="T:System.Threading.Mutex" /> object that represents the mutex.</summary>
		/// <param name="message">An error message that explains the reason for the exception.</param>
		/// <param name="inner">The exception that is the cause of the current exception. If the <paramref name="inner" /> parameter is not null, the current exception is raised in a catch block that handles the inner exception.</param>
		/// <param name="location">The index of the abandoned mutex in the array of wait handles if the exception is thrown for the <see cref="Overload:System.Threading.WaitHandle.WaitAny" /> method, or –1 if the exception is thrown for the <see cref="Overload:System.Threading.WaitHandle.WaitOne" /> or <see cref="Overload:System.Threading.WaitHandle.WaitAll" /> methods.</param>
		/// <param name="handle">A <see cref="T:System.Threading.Mutex" /> object that represents the abandoned mutex.</param>
		// Token: 0x060035B2 RID: 13746 RVA: 0x000C6BD4 File Offset: 0x000C4DD4
		public AbandonedMutexException(string message, Exception inner, int location, WaitHandle handle)
			: base(message, inner)
		{
			base.SetErrorCode(-2146233043);
			this.SetupException(location, handle);
		}

		// Token: 0x060035B3 RID: 13747 RVA: 0x000C6BF9 File Offset: 0x000C4DF9
		private void SetupException(int location, WaitHandle handle)
		{
			this.m_MutexIndex = location;
			if (handle != null)
			{
				this.m_Mutex = handle as Mutex;
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Threading.AbandonedMutexException" /> class with serialized data.</summary>
		/// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object that holds the serialized object data about the exception being thrown. </param>
		/// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext" /> object that contains contextual information about the source or destination.</param>
		// Token: 0x060035B4 RID: 13748 RVA: 0x000C6C11 File Offset: 0x000C4E11
		protected AbandonedMutexException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		/// <summary>Gets the abandoned mutex that caused the exception, if known.</summary>
		/// <returns>A <see cref="T:System.Threading.Mutex" /> object that represents the abandoned mutex, or null if the abandoned mutex could not be identified.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170008FF RID: 2303
		// (get) Token: 0x060035B5 RID: 13749 RVA: 0x000C6C22 File Offset: 0x000C4E22
		public Mutex Mutex
		{
			get
			{
				return this.m_Mutex;
			}
		}

		/// <summary>Gets the index of the abandoned mutex that caused the exception, if known.</summary>
		/// <returns>The index, in the array of wait handles passed to the <see cref="Overload:System.Threading.WaitHandle.WaitAny" /> method, of the <see cref="T:System.Threading.Mutex" /> object that represents the abandoned mutex, or –1 if the index of the abandoned mutex could not be determined.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000900 RID: 2304
		// (get) Token: 0x060035B6 RID: 13750 RVA: 0x000C6C2A File Offset: 0x000C4E2A
		public int MutexIndex
		{
			get
			{
				return this.m_MutexIndex;
			}
		}

		// Token: 0x04001C9F RID: 7327
		private int m_MutexIndex = -1;

		// Token: 0x04001CA0 RID: 7328
		private Mutex m_Mutex;
	}
}
