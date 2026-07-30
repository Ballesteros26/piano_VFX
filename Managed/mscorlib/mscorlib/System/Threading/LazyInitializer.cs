using System;
using System.Security.Permissions;

namespace System.Threading
{
	/// <summary>Provides lazy initialization routines.</summary>
	// Token: 0x02000458 RID: 1112
	[HostProtection(SecurityAction.LinkDemand, Synchronization = true, ExternalThreading = true)]
	public static class LazyInitializer
	{
		/// <summary>Initializes a target reference type with the type's default constructor if it hasn't already been initialized.</summary>
		/// <returns>The initialized reference of type <paramref name="T" />.</returns>
		/// <param name="target">A reference of type <paramref name="T" /> to initialize if it has not already been initialized.</param>
		/// <typeparam name="T">The type of the reference to be initialized.</typeparam>
		/// <exception cref="T:System.MemberAccessException">Permissions to access the constructor of type <paramref name="T" /> were missing.</exception>
		/// <exception cref="T:System.MissingMemberException">Type <paramref name="T" /> does not have a default constructor.</exception>
		// Token: 0x06003526 RID: 13606 RVA: 0x000C46A9 File Offset: 0x000C28A9
		public static T EnsureInitialized<T>(ref T target) where T : class
		{
			if (Volatile.Read<T>(ref target) != null)
			{
				return target;
			}
			return LazyInitializer.EnsureInitializedCore<T>(ref target, LazyHelpers<T>.s_activatorFactorySelector);
		}

		/// <summary>Initializes a target reference type by using a specified function if it hasn't already been initialized.</summary>
		/// <returns>The initialized value of type <paramref name="T" />.</returns>
		/// <param name="target">The reference of type <paramref name="T" /> to initialize if it hasn't already been initialized.</param>
		/// <param name="valueFactory">The function that is called to initialize the reference.</param>
		/// <typeparam name="T">The reference type of the reference to be initialized.</typeparam>
		/// <exception cref="T:System.MissingMemberException">Type <paramref name="T" /> does not have a default constructor.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="valueFactory" /> returned null (Nothing in Visual Basic).</exception>
		// Token: 0x06003527 RID: 13607 RVA: 0x000C46CA File Offset: 0x000C28CA
		public static T EnsureInitialized<T>(ref T target, Func<T> valueFactory) where T : class
		{
			if (Volatile.Read<T>(ref target) != null)
			{
				return target;
			}
			return LazyInitializer.EnsureInitializedCore<T>(ref target, valueFactory);
		}

		// Token: 0x06003528 RID: 13608 RVA: 0x000C46E8 File Offset: 0x000C28E8
		private static T EnsureInitializedCore<T>(ref T target, Func<T> valueFactory) where T : class
		{
			T t = valueFactory();
			if (t == null)
			{
				throw new InvalidOperationException(Environment.GetResourceString("ValueFactory returned null."));
			}
			Interlocked.CompareExchange<T>(ref target, t, default(T));
			return target;
		}

		/// <summary>Initializes a target reference or value type with its default constructor if it hasn't already been initialized.</summary>
		/// <returns>The initialized value of type <paramref name="T" />.</returns>
		/// <param name="target">A reference or value of type <paramref name="T" /> to initialize if it hasn't already been initialized.</param>
		/// <param name="initialized">A reference to a Boolean value that determines whether the target has already been initialized.</param>
		/// <param name="syncLock">A reference to an object used as the mutually exclusive lock for initializing <paramref name="target" />. If <paramref name="syncLock" /> is null, a new object will be instantiated.</param>
		/// <typeparam name="T">The type of the reference to be initialized.</typeparam>
		/// <exception cref="T:System.MemberAccessException">Permissions to access the constructor of type <paramref name="T" /> were missing.</exception>
		/// <exception cref="T:System.MissingMemberException">Type <paramref name="T" /> does not have a default constructor.</exception>
		// Token: 0x06003529 RID: 13609 RVA: 0x000C472B File Offset: 0x000C292B
		public static T EnsureInitialized<T>(ref T target, ref bool initialized, ref object syncLock)
		{
			if (Volatile.Read(ref initialized))
			{
				return target;
			}
			return LazyInitializer.EnsureInitializedCore<T>(ref target, ref initialized, ref syncLock, LazyHelpers<T>.s_activatorFactorySelector);
		}

		/// <summary>Initializes a target reference or value type by using a specified function if it hasn't already been initialized.</summary>
		/// <returns>The initialized value of type <paramref name="T" />.</returns>
		/// <param name="target">A reference or value of type <paramref name="T" /> to initialize if it hasn't already been initialized.</param>
		/// <param name="initialized">A reference to a Boolean value that determines whether the target has already been initialized.</param>
		/// <param name="syncLock">A reference to an object used as the mutually exclusive lock for initializing <paramref name="target" />. If <paramref name="syncLock" /> is null, a new object will be instantiated.</param>
		/// <param name="valueFactory">The function that is called to initialize the reference or value.</param>
		/// <typeparam name="T">The type of the reference to be initialized.</typeparam>
		/// <exception cref="T:System.MemberAccessException">Permissions to access the constructor of type <paramref name="T" /> were missing.</exception>
		/// <exception cref="T:System.MissingMemberException">Type <paramref name="T" /> does not have a default constructor.</exception>
		// Token: 0x0600352A RID: 13610 RVA: 0x000C4749 File Offset: 0x000C2949
		public static T EnsureInitialized<T>(ref T target, ref bool initialized, ref object syncLock, Func<T> valueFactory)
		{
			if (Volatile.Read(ref initialized))
			{
				return target;
			}
			return LazyInitializer.EnsureInitializedCore<T>(ref target, ref initialized, ref syncLock, valueFactory);
		}

		// Token: 0x0600352B RID: 13611 RVA: 0x000C4764 File Offset: 0x000C2964
		private static T EnsureInitializedCore<T>(ref T target, ref bool initialized, ref object syncLock, Func<T> valueFactory)
		{
			object obj = syncLock;
			if (obj == null)
			{
				object obj2 = new object();
				obj = Interlocked.CompareExchange(ref syncLock, obj2, null);
				if (obj == null)
				{
					obj = obj2;
				}
			}
			object obj3 = obj;
			lock (obj3)
			{
				if (!Volatile.Read(ref initialized))
				{
					target = valueFactory();
					Volatile.Write(ref initialized, true);
				}
			}
			return target;
		}
	}
}
