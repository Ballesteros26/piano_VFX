using System;
using System.Diagnostics;
using Unity;

namespace System.Runtime.ExceptionServices
{
	/// <summary>Represents an exception whose state is captured at a certain point in code. </summary>
	// Token: 0x0200082B RID: 2091
	public sealed class ExceptionDispatchInfo
	{
		// Token: 0x06005380 RID: 21376 RVA: 0x001258D4 File Offset: 0x00123AD4
		private ExceptionDispatchInfo(Exception exception)
		{
			this.m_Exception = exception;
			StackTrace[] captured_traces = exception.captured_traces;
			int num = ((captured_traces == null) ? 0 : captured_traces.Length);
			StackTrace[] array = new StackTrace[num + 1];
			if (num != 0)
			{
				Array.Copy(captured_traces, 0, array, 0, num);
			}
			array[num] = new StackTrace(exception, 0, true);
			this.m_stackTrace = array;
		}

		// Token: 0x17000E99 RID: 3737
		// (get) Token: 0x06005381 RID: 21377 RVA: 0x00125927 File Offset: 0x00123B27
		internal object BinaryStackTraceArray
		{
			get
			{
				return this.m_stackTrace;
			}
		}

		/// <summary>Creates an <see cref="T:System.Runtime.ExceptionServices.ExceptionDispatchInfo" /> object that represents the specified exception at the current point in code. </summary>
		/// <returns>An object that represents the specified exception at the current point in code. </returns>
		/// <param name="source">The exception whose state is captured, and which is represented by the returned object. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="source" /> is null. </exception>
		// Token: 0x06005382 RID: 21378 RVA: 0x0012592F File Offset: 0x00123B2F
		public static ExceptionDispatchInfo Capture(Exception source)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source", Environment.GetResourceString("Object cannot be null."));
			}
			return new ExceptionDispatchInfo(source);
		}

		/// <summary>Gets the exception that is represented by the current instance. </summary>
		/// <returns>The exception that is represented by the current instance. </returns>
		// Token: 0x17000E9A RID: 3738
		// (get) Token: 0x06005383 RID: 21379 RVA: 0x0012594F File Offset: 0x00123B4F
		public Exception SourceException
		{
			get
			{
				return this.m_Exception;
			}
		}

		/// <summary>Throws the exception that is represented by the current <see cref="T:System.Runtime.ExceptionServices.ExceptionDispatchInfo" /> object, after restoring the state that was saved when the exception was captured. </summary>
		// Token: 0x06005384 RID: 21380 RVA: 0x00125957 File Offset: 0x00123B57
		public void Throw()
		{
			this.m_Exception.RestoreExceptionDispatchInfo(this);
			throw this.m_Exception;
		}

		// Token: 0x06005385 RID: 21381 RVA: 0x0012596B File Offset: 0x00123B6B
		public static void Throw(Exception source)
		{
			ExceptionDispatchInfo.Capture(source).Throw();
		}

		// Token: 0x06005386 RID: 21382 RVA: 0x0001FB35 File Offset: 0x0001DD35
		internal ExceptionDispatchInfo()
		{
			ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x04002B68 RID: 11112
		private Exception m_Exception;

		// Token: 0x04002B69 RID: 11113
		private object m_stackTrace;
	}
}
