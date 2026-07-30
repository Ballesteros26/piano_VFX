using System;
using System.Diagnostics;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security.Permissions;
using System.Threading;

namespace System
{
	/// <summary>Provides support for lazy initialization.</summary>
	/// <typeparam name="T">The type of object that is being lazily initialized.</typeparam>
	// Token: 0x020000FE RID: 254
	[DebuggerTypeProxy(typeof(System_LazyDebugView<>))]
	[DebuggerDisplay("ThreadSafetyMode={Mode}, IsValueCreated={IsValueCreated}, IsValueFaulted={IsValueFaulted}, Value={ValueForDebugDisplay}")]
	[ComVisible(false)]
	[HostProtection(SecurityAction.LinkDemand, Synchronization = true, ExternalThreading = true)]
	[Serializable]
	public class Lazy<T>
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Lazy`1" /> class. When lazy initialization occurs, the default constructor of the target type is used.</summary>
		// Token: 0x06000977 RID: 2423 RVA: 0x0003125A File Offset: 0x0002F45A
		public Lazy()
			: this(LazyThreadSafetyMode.ExecutionAndPublication)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Lazy`1" /> class. When lazy initialization occurs, the specified initialization function is used.</summary>
		/// <param name="valueFactory">The delegate that is invoked to produce the lazily initialized value when it is needed.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="valueFactory" /> is null. </exception>
		// Token: 0x06000978 RID: 2424 RVA: 0x00031263 File Offset: 0x0002F463
		public Lazy(Func<T> valueFactory)
			: this(valueFactory, LazyThreadSafetyMode.ExecutionAndPublication)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Lazy`1" /> class. When lazy initialization occurs, the default constructor of the target type and the specified initialization mode are used.</summary>
		/// <param name="isThreadSafe">true to make this instance usable concurrently by multiple threads; false to make the instance usable by only one thread at a time. </param>
		// Token: 0x06000979 RID: 2425 RVA: 0x0003126D File Offset: 0x0002F46D
		public Lazy(bool isThreadSafe)
			: this(isThreadSafe ? LazyThreadSafetyMode.ExecutionAndPublication : LazyThreadSafetyMode.None)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Lazy`1" /> class that uses the default constructor of <paramref name="T" /> and the specified thread-safety mode.</summary>
		/// <param name="mode">One of the enumeration values that specifies the thread safety mode. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="mode" /> contains an invalid value. </exception>
		// Token: 0x0600097A RID: 2426 RVA: 0x0003127C File Offset: 0x0002F47C
		public Lazy(LazyThreadSafetyMode mode)
		{
			this.m_threadSafeObj = Lazy<T>.GetObjectFromMode(mode);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Lazy`1" /> class. When lazy initialization occurs, the specified initialization function and initialization mode are used.</summary>
		/// <param name="valueFactory">The delegate that is invoked to produce the lazily initialized value when it is needed.</param>
		/// <param name="isThreadSafe">true to make this instance usable concurrently by multiple threads; false to make this instance usable by only one thread at a time.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="valueFactory" /> is null. </exception>
		// Token: 0x0600097B RID: 2427 RVA: 0x00031290 File Offset: 0x0002F490
		public Lazy(Func<T> valueFactory, bool isThreadSafe)
			: this(valueFactory, isThreadSafe ? LazyThreadSafetyMode.ExecutionAndPublication : LazyThreadSafetyMode.None)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Lazy`1" /> class that uses the specified initialization function and thread-safety mode.</summary>
		/// <param name="valueFactory">The delegate that is invoked to produce the lazily initialized value when it is needed.</param>
		/// <param name="mode">One of the enumeration values that specifies the thread safety mode. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="mode" /> contains an invalid value. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="valueFactory" /> is null. </exception>
		// Token: 0x0600097C RID: 2428 RVA: 0x000312A0 File Offset: 0x0002F4A0
		public Lazy(Func<T> valueFactory, LazyThreadSafetyMode mode)
		{
			if (valueFactory == null)
			{
				throw new ArgumentNullException("valueFactory");
			}
			this.m_threadSafeObj = Lazy<T>.GetObjectFromMode(mode);
			this.m_valueFactory = valueFactory;
		}

		// Token: 0x0600097D RID: 2429 RVA: 0x000312C9 File Offset: 0x0002F4C9
		private static object GetObjectFromMode(LazyThreadSafetyMode mode)
		{
			if (mode == LazyThreadSafetyMode.ExecutionAndPublication)
			{
				return new object();
			}
			if (mode == LazyThreadSafetyMode.PublicationOnly)
			{
				return LazyHelpers.PUBLICATION_ONLY_SENTINEL;
			}
			if (mode != LazyThreadSafetyMode.None)
			{
				throw new ArgumentOutOfRangeException("mode", Environment.GetResourceString("The mode argument specifies an invalid value."));
			}
			return null;
		}

		// Token: 0x0600097E RID: 2430 RVA: 0x000312F8 File Offset: 0x0002F4F8
		[OnSerializing]
		private void OnSerializing(StreamingContext context)
		{
			T value = this.Value;
		}

		/// <summary>Creates and returns a string representation of the <see cref="P:System.Lazy`1.Value" /> property for this instance.</summary>
		/// <returns>The result of calling the <see cref="M:System.Object.ToString" /> method on the <see cref="P:System.Lazy`1.Value" /> property for this instance, if the value has been created (that is, if the <see cref="P:System.Lazy`1.IsValueCreated" /> property returns true). Otherwise, a string indicating that the value has not been created. </returns>
		/// <exception cref="T:System.NullReferenceException">The <see cref="P:System.Lazy`1.Value" /> property is null.</exception>
		// Token: 0x0600097F RID: 2431 RVA: 0x00031304 File Offset: 0x0002F504
		public override string ToString()
		{
			if (!this.IsValueCreated)
			{
				return Environment.GetResourceString("Value is not created.");
			}
			T value = this.Value;
			return value.ToString();
		}

		// Token: 0x17000190 RID: 400
		// (get) Token: 0x06000980 RID: 2432 RVA: 0x00031338 File Offset: 0x0002F538
		internal T ValueForDebugDisplay
		{
			get
			{
				if (!this.IsValueCreated)
				{
					return default(T);
				}
				return ((Lazy<T>.Boxed)this.m_boxed).m_value;
			}
		}

		// Token: 0x17000191 RID: 401
		// (get) Token: 0x06000981 RID: 2433 RVA: 0x00031367 File Offset: 0x0002F567
		internal LazyThreadSafetyMode Mode
		{
			get
			{
				if (this.m_threadSafeObj == null)
				{
					return LazyThreadSafetyMode.None;
				}
				if (this.m_threadSafeObj == LazyHelpers.PUBLICATION_ONLY_SENTINEL)
				{
					return LazyThreadSafetyMode.PublicationOnly;
				}
				return LazyThreadSafetyMode.ExecutionAndPublication;
			}
		}

		// Token: 0x17000192 RID: 402
		// (get) Token: 0x06000982 RID: 2434 RVA: 0x00031383 File Offset: 0x0002F583
		internal bool IsValueFaulted
		{
			get
			{
				return this.m_boxed is Lazy<T>.LazyInternalExceptionHolder;
			}
		}

		/// <summary>Gets a value that indicates whether a value has been created for this <see cref="T:System.Lazy`1" /> instance.</summary>
		/// <returns>true if a value has been created for this <see cref="T:System.Lazy`1" /> instance; otherwise, false.</returns>
		// Token: 0x17000193 RID: 403
		// (get) Token: 0x06000983 RID: 2435 RVA: 0x00031393 File Offset: 0x0002F593
		public bool IsValueCreated
		{
			get
			{
				return this.m_boxed != null && this.m_boxed is Lazy<T>.Boxed;
			}
		}

		/// <summary>Gets the lazily initialized value of the current <see cref="T:System.Lazy`1" /> instance.</summary>
		/// <returns>The lazily initialized value of the current <see cref="T:System.Lazy`1" /> instance.</returns>
		/// <exception cref="T:System.MemberAccessException">The <see cref="T:System.Lazy`1" /> instance is initialized to use the default constructor of the type that is being lazily initialized, and permissions to access the constructor are missing. </exception>
		/// <exception cref="T:System.MissingMemberException">The <see cref="T:System.Lazy`1" /> instance is initialized to use the default constructor of the type that is being lazily initialized, and that type does not have a public, parameterless constructor. </exception>
		/// <exception cref="T:System.InvalidOperationException">The initialization function tries to access <see cref="P:System.Lazy`1.Value" /> on this instance. </exception>
		// Token: 0x17000194 RID: 404
		// (get) Token: 0x06000984 RID: 2436 RVA: 0x000313B0 File Offset: 0x0002F5B0
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public T Value
		{
			get
			{
				if (this.m_boxed != null)
				{
					Lazy<T>.Boxed boxed = this.m_boxed as Lazy<T>.Boxed;
					if (boxed != null)
					{
						return boxed.m_value;
					}
					(this.m_boxed as Lazy<T>.LazyInternalExceptionHolder).m_edi.Throw();
				}
				Debugger.NotifyOfCrossThreadDependency();
				return this.LazyInitValue();
			}
		}

		// Token: 0x06000985 RID: 2437 RVA: 0x00031400 File Offset: 0x0002F600
		private T LazyInitValue()
		{
			Lazy<T>.Boxed boxed = null;
			LazyThreadSafetyMode mode = this.Mode;
			if (mode == LazyThreadSafetyMode.None)
			{
				boxed = this.CreateValue();
				this.m_boxed = boxed;
			}
			else if (mode == LazyThreadSafetyMode.PublicationOnly)
			{
				boxed = this.CreateValue();
				if (boxed == null || Interlocked.CompareExchange(ref this.m_boxed, boxed, null) != null)
				{
					boxed = (Lazy<T>.Boxed)this.m_boxed;
				}
				else
				{
					this.m_valueFactory = Lazy<T>.ALREADY_INVOKED_SENTINEL;
				}
			}
			else
			{
				object obj = Volatile.Read<object>(ref this.m_threadSafeObj);
				bool flag = false;
				try
				{
					if (obj != Lazy<T>.ALREADY_INVOKED_SENTINEL)
					{
						Monitor.Enter(obj, ref flag);
					}
					if (this.m_boxed == null)
					{
						boxed = this.CreateValue();
						this.m_boxed = boxed;
						Volatile.Write<object>(ref this.m_threadSafeObj, Lazy<T>.ALREADY_INVOKED_SENTINEL);
					}
					else
					{
						boxed = this.m_boxed as Lazy<T>.Boxed;
						if (boxed == null)
						{
							(this.m_boxed as Lazy<T>.LazyInternalExceptionHolder).m_edi.Throw();
						}
					}
				}
				finally
				{
					if (flag)
					{
						Monitor.Exit(obj);
					}
				}
			}
			return boxed.m_value;
		}

		// Token: 0x06000986 RID: 2438 RVA: 0x000314F4 File Offset: 0x0002F6F4
		private Lazy<T>.Boxed CreateValue()
		{
			Lazy<T>.Boxed boxed = null;
			LazyThreadSafetyMode mode = this.Mode;
			if (this.m_valueFactory != null)
			{
				try
				{
					if (mode != LazyThreadSafetyMode.PublicationOnly && this.m_valueFactory == Lazy<T>.ALREADY_INVOKED_SENTINEL)
					{
						throw new InvalidOperationException(Environment.GetResourceString("ValueFactory attempted to access the Value property of this instance."));
					}
					Func<T> valueFactory = this.m_valueFactory;
					if (mode != LazyThreadSafetyMode.PublicationOnly)
					{
						this.m_valueFactory = Lazy<T>.ALREADY_INVOKED_SENTINEL;
					}
					else if (valueFactory == Lazy<T>.ALREADY_INVOKED_SENTINEL)
					{
						return null;
					}
					return new Lazy<T>.Boxed(valueFactory());
				}
				catch (Exception ex)
				{
					if (mode != LazyThreadSafetyMode.PublicationOnly)
					{
						this.m_boxed = new Lazy<T>.LazyInternalExceptionHolder(ex);
					}
					throw;
				}
			}
			try
			{
				boxed = new Lazy<T>.Boxed((T)((object)Activator.CreateInstance(typeof(T))));
			}
			catch (MissingMethodException)
			{
				Exception ex2 = new MissingMemberException(Environment.GetResourceString("The lazily-initialized type does not have a public, parameterless constructor."));
				if (mode != LazyThreadSafetyMode.PublicationOnly)
				{
					this.m_boxed = new Lazy<T>.LazyInternalExceptionHolder(ex2);
				}
				throw ex2;
			}
			return boxed;
		}

		// Token: 0x0400070F RID: 1807
		private static readonly Func<T> ALREADY_INVOKED_SENTINEL = () => default(T);

		// Token: 0x04000710 RID: 1808
		private object m_boxed;

		// Token: 0x04000711 RID: 1809
		[NonSerialized]
		private Func<T> m_valueFactory;

		// Token: 0x04000712 RID: 1810
		[NonSerialized]
		private object m_threadSafeObj;

		// Token: 0x020000FF RID: 255
		[Serializable]
		private class Boxed
		{
			// Token: 0x06000988 RID: 2440 RVA: 0x000315FF File Offset: 0x0002F7FF
			internal Boxed(T value)
			{
				this.m_value = value;
			}

			// Token: 0x04000713 RID: 1811
			internal T m_value;
		}

		// Token: 0x02000100 RID: 256
		private class LazyInternalExceptionHolder
		{
			// Token: 0x06000989 RID: 2441 RVA: 0x0003160E File Offset: 0x0002F80E
			internal LazyInternalExceptionHolder(Exception ex)
			{
				this.m_edi = ExceptionDispatchInfo.Capture(ex);
			}

			// Token: 0x04000714 RID: 1812
			internal ExceptionDispatchInfo m_edi;
		}
	}
}
