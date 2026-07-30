using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.ExceptionServices;
using System.Runtime.Serialization;
using System.Security;

namespace System
{
	/// <summary>Represents one or more errors that occur during application execution.</summary>
	// Token: 0x020000F8 RID: 248
	[DebuggerDisplay("Count = {InnerExceptionCount}")]
	[Serializable]
	public class AggregateException : Exception
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.AggregateException" /> class with a system-supplied message that describes the error.</summary>
		// Token: 0x06000945 RID: 2373 RVA: 0x00030A55 File Offset: 0x0002EC55
		public AggregateException()
			: base(Environment.GetResourceString("One or more errors occurred."))
		{
			this.m_innerExceptions = new ReadOnlyCollection<Exception>(new Exception[0]);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.AggregateException" /> class with a specified message that describes the error.</summary>
		/// <param name="message">The message that describes the exception. The caller of this constructor is required to ensure that this string has been localized for the current system culture.</param>
		// Token: 0x06000946 RID: 2374 RVA: 0x00030A78 File Offset: 0x0002EC78
		public AggregateException(string message)
			: base(message)
		{
			this.m_innerExceptions = new ReadOnlyCollection<Exception>(new Exception[0]);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.AggregateException" /> class with a specified error message and a reference to the inner exception that is the cause of this exception.</summary>
		/// <param name="message">The message that describes the exception. The caller of this constructor is required to ensure that this string has been localized for the current system culture. </param>
		/// <param name="innerException">The exception that is the cause of the current exception. If the <paramref name="innerException" /> parameter is not null, the current exception is raised in a catch block that handles the inner exception. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="innerException" /> argument is null.</exception>
		// Token: 0x06000947 RID: 2375 RVA: 0x00030A92 File Offset: 0x0002EC92
		public AggregateException(string message, Exception innerException)
			: base(message, innerException)
		{
			if (innerException == null)
			{
				throw new ArgumentNullException("innerException");
			}
			this.m_innerExceptions = new ReadOnlyCollection<Exception>(new Exception[] { innerException });
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.AggregateException" /> class with references to the inner exceptions that are the cause of this exception.</summary>
		/// <param name="innerExceptions">The exceptions that are the cause of the current exception.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="innerExceptions" /> argument is null.</exception>
		/// <exception cref="T:System.ArgumentException">An element of <paramref name="innerExceptions" /> is null.</exception>
		// Token: 0x06000948 RID: 2376 RVA: 0x00030ABF File Offset: 0x0002ECBF
		public AggregateException(IEnumerable<Exception> innerExceptions)
			: this(Environment.GetResourceString("One or more errors occurred."), innerExceptions)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.AggregateException" /> class with references to the inner exceptions that are the cause of this exception.</summary>
		/// <param name="innerExceptions">The exceptions that are the cause of the current exception.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="innerExceptions" /> argument is null.</exception>
		/// <exception cref="T:System.ArgumentException">An element of <paramref name="innerExceptions" /> is null.</exception>
		// Token: 0x06000949 RID: 2377 RVA: 0x00030AD2 File Offset: 0x0002ECD2
		public AggregateException(params Exception[] innerExceptions)
			: this(Environment.GetResourceString("One or more errors occurred."), innerExceptions)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.AggregateException" /> class with a specified error message and references to the inner exceptions that are the cause of this exception.</summary>
		/// <param name="message">The error message that explains the reason for the exception.</param>
		/// <param name="innerExceptions">The exceptions that are the cause of the current exception.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="innerExceptions" /> argument is null.</exception>
		/// <exception cref="T:System.ArgumentException">An element of <paramref name="innerExceptions" /> is null.</exception>
		// Token: 0x0600094A RID: 2378 RVA: 0x00030AE5 File Offset: 0x0002ECE5
		public AggregateException(string message, IEnumerable<Exception> innerExceptions)
			: this(message, (innerExceptions as IList<Exception>) ?? ((innerExceptions == null) ? null : new List<Exception>(innerExceptions)))
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.AggregateException" /> class with a specified error message and references to the inner exceptions that are the cause of this exception.</summary>
		/// <param name="message">The error message that explains the reason for the exception.</param>
		/// <param name="innerExceptions">The exceptions that are the cause of the current exception.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="innerExceptions" /> argument is null.</exception>
		/// <exception cref="T:System.ArgumentException">An element of <paramref name="innerExceptions" /> is null.</exception>
		// Token: 0x0600094B RID: 2379 RVA: 0x00030B04 File Offset: 0x0002ED04
		public AggregateException(string message, params Exception[] innerExceptions)
			: this(message, innerExceptions)
		{
		}

		// Token: 0x0600094C RID: 2380 RVA: 0x00030B10 File Offset: 0x0002ED10
		private AggregateException(string message, IList<Exception> innerExceptions)
			: base(message, (innerExceptions != null && innerExceptions.Count > 0) ? innerExceptions[0] : null)
		{
			if (innerExceptions == null)
			{
				throw new ArgumentNullException("innerExceptions");
			}
			Exception[] array = new Exception[innerExceptions.Count];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = innerExceptions[i];
				if (array[i] == null)
				{
					throw new ArgumentException(Environment.GetResourceString("An element of innerExceptions was null."));
				}
			}
			this.m_innerExceptions = new ReadOnlyCollection<Exception>(array);
		}

		// Token: 0x0600094D RID: 2381 RVA: 0x00030B8D File Offset: 0x0002ED8D
		internal AggregateException(IEnumerable<ExceptionDispatchInfo> innerExceptionInfos)
			: this(Environment.GetResourceString("One or more errors occurred."), innerExceptionInfos)
		{
		}

		// Token: 0x0600094E RID: 2382 RVA: 0x00030BA0 File Offset: 0x0002EDA0
		internal AggregateException(string message, IEnumerable<ExceptionDispatchInfo> innerExceptionInfos)
			: this(message, (innerExceptionInfos as IList<ExceptionDispatchInfo>) ?? ((innerExceptionInfos == null) ? null : new List<ExceptionDispatchInfo>(innerExceptionInfos)))
		{
		}

		// Token: 0x0600094F RID: 2383 RVA: 0x00030BC0 File Offset: 0x0002EDC0
		private AggregateException(string message, IList<ExceptionDispatchInfo> innerExceptionInfos)
			: base(message, (innerExceptionInfos != null && innerExceptionInfos.Count > 0 && innerExceptionInfos[0] != null) ? innerExceptionInfos[0].SourceException : null)
		{
			if (innerExceptionInfos == null)
			{
				throw new ArgumentNullException("innerExceptionInfos");
			}
			Exception[] array = new Exception[innerExceptionInfos.Count];
			for (int i = 0; i < array.Length; i++)
			{
				ExceptionDispatchInfo exceptionDispatchInfo = innerExceptionInfos[i];
				if (exceptionDispatchInfo != null)
				{
					array[i] = exceptionDispatchInfo.SourceException;
				}
				if (array[i] == null)
				{
					throw new ArgumentException(Environment.GetResourceString("An element of innerExceptions was null."));
				}
			}
			this.m_innerExceptions = new ReadOnlyCollection<Exception>(array);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.AggregateException" /> class with serialized data.</summary>
		/// <param name="info">The object that holds the serialized object data. </param>
		/// <param name="context">The contextual information about the source or destination. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="info" /> argument is null.</exception>
		/// <exception cref="T:System.Runtime.Serialization.SerializationException">The exception could not be deserialized correctly.</exception>
		// Token: 0x06000950 RID: 2384 RVA: 0x00030C58 File Offset: 0x0002EE58
		[SecurityCritical]
		protected AggregateException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			Exception[] array = info.GetValue("InnerExceptions", typeof(Exception[])) as Exception[];
			if (array == null)
			{
				throw new SerializationException(Environment.GetResourceString("The serialization stream contains no inner exceptions."));
			}
			this.m_innerExceptions = new ReadOnlyCollection<Exception>(array);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.AggregateException" /> class with serialized data.</summary>
		/// <param name="info">The object that holds the serialized object data. </param>
		/// <param name="context">The contextual information about the source or destination. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="info" /> argument is null.</exception>
		// Token: 0x06000951 RID: 2385 RVA: 0x00030CB8 File Offset: 0x0002EEB8
		[SecurityCritical]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			base.GetObjectData(info, context);
			Exception[] array = new Exception[this.m_innerExceptions.Count];
			this.m_innerExceptions.CopyTo(array, 0);
			info.AddValue("InnerExceptions", array, typeof(Exception[]));
		}

		/// <summary>Returns the <see cref="T:System.AggregateException" /> that is the root cause of this exception.</summary>
		/// <returns>Returns the <see cref="T:System.AggregateException" /> that is the root cause of this exception.</returns>
		// Token: 0x06000952 RID: 2386 RVA: 0x00030D10 File Offset: 0x0002EF10
		public override Exception GetBaseException()
		{
			Exception ex = this;
			AggregateException ex2 = this;
			while (ex2 != null && ex2.InnerExceptions.Count == 1)
			{
				ex = ex.InnerException;
				ex2 = ex as AggregateException;
			}
			return ex;
		}

		/// <summary>Gets a read-only collection of the <see cref="T:System.Exception" /> instances that caused the current exception.</summary>
		/// <returns>Returns a read-only collection of the <see cref="T:System.Exception" /> instances that caused the current exception.</returns>
		// Token: 0x17000182 RID: 386
		// (get) Token: 0x06000953 RID: 2387 RVA: 0x00030D43 File Offset: 0x0002EF43
		public ReadOnlyCollection<Exception> InnerExceptions
		{
			get
			{
				return this.m_innerExceptions;
			}
		}

		/// <summary>Invokes a handler on each <see cref="T:System.Exception" /> contained by this <see cref="T:System.AggregateException" />.</summary>
		/// <param name="predicate">The predicate to execute for each exception. The predicate accepts as an argument the <see cref="T:System.Exception" /> to be processed and returns a Boolean to indicate whether the exception was handled.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="predicate" /> argument is null.</exception>
		/// <exception cref="T:System.AggregateException">An exception contained by this <see cref="T:System.AggregateException" /> was not handled.</exception>
		// Token: 0x06000954 RID: 2388 RVA: 0x00030D4C File Offset: 0x0002EF4C
		public void Handle(Func<Exception, bool> predicate)
		{
			if (predicate == null)
			{
				throw new ArgumentNullException("predicate");
			}
			List<Exception> list = null;
			for (int i = 0; i < this.m_innerExceptions.Count; i++)
			{
				if (!predicate(this.m_innerExceptions[i]))
				{
					if (list == null)
					{
						list = new List<Exception>();
					}
					list.Add(this.m_innerExceptions[i]);
				}
			}
			if (list != null)
			{
				throw new AggregateException(this.Message, list);
			}
		}

		/// <summary>Flattens an <see cref="T:System.AggregateException" /> instances into a single, new instance.</summary>
		/// <returns>A new, flattened <see cref="T:System.AggregateException" />.</returns>
		// Token: 0x06000955 RID: 2389 RVA: 0x00030DC0 File Offset: 0x0002EFC0
		public AggregateException Flatten()
		{
			List<Exception> list = new List<Exception>();
			List<AggregateException> list2 = new List<AggregateException>();
			list2.Add(this);
			int num = 0;
			while (list2.Count > num)
			{
				IList<Exception> innerExceptions = list2[num++].InnerExceptions;
				for (int i = 0; i < innerExceptions.Count; i++)
				{
					Exception ex = innerExceptions[i];
					if (ex != null)
					{
						AggregateException ex2 = ex as AggregateException;
						if (ex2 != null)
						{
							list2.Add(ex2);
						}
						else
						{
							list.Add(ex);
						}
					}
				}
			}
			return new AggregateException(this.Message, list);
		}

		/// <summary>Creates and returns a string representation of the current <see cref="T:System.AggregateException" />.</summary>
		/// <returns>A string representation of the current exception.</returns>
		// Token: 0x06000956 RID: 2390 RVA: 0x00030E4C File Offset: 0x0002F04C
		public override string ToString()
		{
			string text = base.ToString();
			for (int i = 0; i < this.m_innerExceptions.Count; i++)
			{
				text = string.Format(CultureInfo.InvariantCulture, Environment.GetResourceString("{0}{1}---> (Inner Exception #{2}) {3}{4}{5}"), new object[]
				{
					text,
					Environment.NewLine,
					i,
					this.m_innerExceptions[i].ToString(),
					"<---",
					Environment.NewLine
				});
			}
			return text;
		}

		// Token: 0x17000183 RID: 387
		// (get) Token: 0x06000957 RID: 2391 RVA: 0x00030ECB File Offset: 0x0002F0CB
		private int InnerExceptionCount
		{
			get
			{
				return this.InnerExceptions.Count;
			}
		}

		// Token: 0x040006FE RID: 1790
		private ReadOnlyCollection<Exception> m_innerExceptions;
	}
}
