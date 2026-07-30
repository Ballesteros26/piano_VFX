using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Security;
using System.Threading;

namespace System.Runtime.InteropServices.WindowsRuntime
{
	/// <summary>Provides helper methods for marshaling data between the .NET Framework and the Windows Runtime.</summary>
	// Token: 0x02000962 RID: 2402
	public static class WindowsRuntimeMarshal
	{
		/// <summary>Adds the specified event handler to a Windows Runtime event.</summary>
		/// <param name="addMethod">A delegate that represents the method that adds event handlers to the Windows Runtime event. </param>
		/// <param name="removeMethod">A delegate that represents the method that removes event handlers from the Windows Runtime event. </param>
		/// <param name="handler">A delegate the represents the event handler that is added. </param>
		/// <typeparam name="T">The type of the delegate that represents the event handler. </typeparam>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="addMethod" /> is null. -or-<paramref name="removeMethod" /> is null.</exception>
		// Token: 0x06005948 RID: 22856 RVA: 0x0012B040 File Offset: 0x00129240
		[SecurityCritical]
		public static void AddEventHandler<T>(Func<T, EventRegistrationToken> addMethod, Action<EventRegistrationToken> removeMethod, T handler)
		{
			if (addMethod == null)
			{
				throw new ArgumentNullException("addMethod");
			}
			if (removeMethod == null)
			{
				throw new ArgumentNullException("removeMethod");
			}
			if (handler == null)
			{
				return;
			}
			object target = removeMethod.Target;
			if (target == null || Marshal.IsComObject(target))
			{
				WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.AddEventHandler<T>(addMethod, removeMethod, handler);
				return;
			}
			WindowsRuntimeMarshal.ManagedEventRegistrationImpl.AddEventHandler<T>(addMethod, removeMethod, handler);
		}

		/// <summary>Removes the specified event handler from a Windows Runtime event. </summary>
		/// <param name="removeMethod">A delegate that represents the method that removes event handlers from the Windows Runtime event. </param>
		/// <param name="handler">The event handler that is removed. </param>
		/// <typeparam name="T">The type of the delegate that represents the event handler. </typeparam>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="removeMethod" /> is null. </exception>
		// Token: 0x06005949 RID: 22857 RVA: 0x0012B098 File Offset: 0x00129298
		[SecurityCritical]
		public static void RemoveEventHandler<T>(Action<EventRegistrationToken> removeMethod, T handler)
		{
			if (removeMethod == null)
			{
				throw new ArgumentNullException("removeMethod");
			}
			if (handler == null)
			{
				return;
			}
			object target = removeMethod.Target;
			if (target == null || Marshal.IsComObject(target))
			{
				WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.RemoveEventHandler<T>(removeMethod, handler);
				return;
			}
			WindowsRuntimeMarshal.ManagedEventRegistrationImpl.RemoveEventHandler<T>(removeMethod, handler);
		}

		/// <summary>Removes all the event handlers that can be removed by using the specified method. </summary>
		/// <param name="removeMethod">A delegate that represents the method that removes event handlers from the Windows Runtime event. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="removeMethod" /> is null. </exception>
		// Token: 0x0600594A RID: 22858 RVA: 0x0012B0E0 File Offset: 0x001292E0
		[SecurityCritical]
		public static void RemoveAllEventHandlers(Action<EventRegistrationToken> removeMethod)
		{
			if (removeMethod == null)
			{
				throw new ArgumentNullException("removeMethod");
			}
			object target = removeMethod.Target;
			if (target == null || Marshal.IsComObject(target))
			{
				WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.RemoveAllEventHandlers(removeMethod);
				return;
			}
			WindowsRuntimeMarshal.ManagedEventRegistrationImpl.RemoveAllEventHandlers(removeMethod);
		}

		// Token: 0x0600594B RID: 22859 RVA: 0x0012B11C File Offset: 0x0012931C
		internal static int GetRegistrationTokenCacheSize()
		{
			int num = 0;
			if (WindowsRuntimeMarshal.ManagedEventRegistrationImpl.s_eventRegistrations != null)
			{
				ConditionalWeakTable<object, Dictionary<MethodInfo, Dictionary<object, WindowsRuntimeMarshal.EventRegistrationTokenList>>> s_eventRegistrations = WindowsRuntimeMarshal.ManagedEventRegistrationImpl.s_eventRegistrations;
				lock (s_eventRegistrations)
				{
					num += WindowsRuntimeMarshal.ManagedEventRegistrationImpl.s_eventRegistrations.Keys.Count;
				}
			}
			if (WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.s_eventRegistrations != null)
			{
				Dictionary<WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.EventCacheKey, WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.EventCacheEntry> s_eventRegistrations2 = WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.s_eventRegistrations;
				lock (s_eventRegistrations2)
				{
					num += WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.s_eventRegistrations.Count;
				}
			}
			return num;
		}

		// Token: 0x0600594C RID: 22860 RVA: 0x0012B1BC File Offset: 0x001293BC
		internal static void CallRemoveMethods(Action<EventRegistrationToken> removeMethod, List<EventRegistrationToken> tokensToRemove)
		{
			List<Exception> list = new List<Exception>();
			foreach (EventRegistrationToken eventRegistrationToken in tokensToRemove)
			{
				try
				{
					removeMethod(eventRegistrationToken);
				}
				catch (Exception ex)
				{
					list.Add(ex);
				}
			}
			if (list.Count > 0)
			{
				throw new AggregateException(list.ToArray());
			}
		}

		// Token: 0x0600594D RID: 22861 RVA: 0x0012B240 File Offset: 0x00129440
		[SecurityCritical]
		internal unsafe static string HStringToString(IntPtr hstring)
		{
			if (hstring == IntPtr.Zero)
			{
				return string.Empty;
			}
			uint num;
			return new string(UnsafeNativeMethods.WindowsGetStringRawBuffer(hstring, &num), 0, checked((int)num));
		}

		// Token: 0x0600594E RID: 22862 RVA: 0x0012B274 File Offset: 0x00129474
		internal static Exception GetExceptionForHR(int hresult, Exception innerException, string messageResource)
		{
			Exception ex;
			if (innerException != null)
			{
				string text = innerException.Message;
				if (text == null && messageResource != null)
				{
					text = Environment.GetResourceString(messageResource);
				}
				ex = new Exception(text, innerException);
			}
			else
			{
				ex = new Exception((messageResource != null) ? Environment.GetResourceString(messageResource) : null);
			}
			ex.SetErrorCode(hresult);
			return ex;
		}

		// Token: 0x0600594F RID: 22863 RVA: 0x0012B2BE File Offset: 0x001294BE
		internal static Exception GetExceptionForHR(int hresult, Exception innerException)
		{
			return WindowsRuntimeMarshal.GetExceptionForHR(hresult, innerException, null);
		}

		// Token: 0x06005950 RID: 22864 RVA: 0x0012B2C8 File Offset: 0x001294C8
		[SecurityCritical]
		private static bool RoOriginateLanguageException(int error, string message, IntPtr languageException)
		{
			if (WindowsRuntimeMarshal.s_haveBlueErrorApis)
			{
				try
				{
					return UnsafeNativeMethods.RoOriginateLanguageException(error, message, languageException);
				}
				catch (EntryPointNotFoundException)
				{
					WindowsRuntimeMarshal.s_haveBlueErrorApis = false;
				}
				return false;
			}
			return false;
		}

		// Token: 0x06005951 RID: 22865 RVA: 0x0012B304 File Offset: 0x00129504
		[SecurityCritical]
		private static void RoReportUnhandledError(IRestrictedErrorInfo error)
		{
			if (WindowsRuntimeMarshal.s_haveBlueErrorApis)
			{
				try
				{
					UnsafeNativeMethods.RoReportUnhandledError(error);
				}
				catch (EntryPointNotFoundException)
				{
					WindowsRuntimeMarshal.s_haveBlueErrorApis = false;
				}
			}
		}

		// Token: 0x06005952 RID: 22866 RVA: 0x0012B33C File Offset: 0x0012953C
		[SecuritySafeCritical]
		[FriendAccessAllowed]
		internal static bool ReportUnhandledError(Exception e)
		{
			if (!AppDomain.IsAppXModel())
			{
				return false;
			}
			if (!WindowsRuntimeMarshal.s_haveBlueErrorApis)
			{
				return false;
			}
			if (e != null)
			{
				IntPtr intPtr = IntPtr.Zero;
				IntPtr zero = IntPtr.Zero;
				try
				{
					intPtr = Marshal.GetIUnknownForObject(e);
					if (intPtr != IntPtr.Zero)
					{
						Marshal.QueryInterface(intPtr, ref WindowsRuntimeMarshal.s_iidIErrorInfo, out zero);
						if (zero != IntPtr.Zero && WindowsRuntimeMarshal.RoOriginateLanguageException(Marshal.GetHRForException_WinRT(e), e.Message, zero))
						{
							IRestrictedErrorInfo restrictedErrorInfo = UnsafeNativeMethods.GetRestrictedErrorInfo();
							if (restrictedErrorInfo != null)
							{
								WindowsRuntimeMarshal.RoReportUnhandledError(restrictedErrorInfo);
								return true;
							}
						}
					}
				}
				finally
				{
					if (zero != IntPtr.Zero)
					{
						Marshal.Release(zero);
					}
					if (intPtr != IntPtr.Zero)
					{
						Marshal.Release(intPtr);
					}
				}
				return false;
			}
			return false;
		}

		/// <summary>Returns an object that implements the activation factory interface for the specified Windows Runtime type. </summary>
		/// <returns>An object that implements the activation factory interface. </returns>
		/// <param name="type">The Windows Runtime type to get the activation factory interface for. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="type" /> does not represent a Windows Runtime type (that is, belonging to the Windows Runtime itself or defined in a Windows Runtime component). -or-The object specified for <paramref name="type" /> was not provided by the common language runtime type system. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="type" /> is null. </exception>
		/// <exception cref="T:System.TypeLoadException">The specified Windows Runtime class is not properly registered. For example, the .winmd file was located, but the Windows Runtime failed to locate the implementation. </exception>
		// Token: 0x06005953 RID: 22867 RVA: 0x0012B404 File Offset: 0x00129604
		[SecurityCritical]
		public static IActivationFactory GetActivationFactory(Type type)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			if (type.IsWindowsRuntimeObject && type.IsImport)
			{
				return (IActivationFactory)Marshal.GetNativeActivationFactory(type);
			}
			throw new NotSupportedException();
		}

		/// <summary>Allocates a Windows Runtime HSTRING and copies the specified managed string to it. </summary>
		/// <returns>An unmanaged pointer to the new HSTRING, or <see cref="F:System.IntPtr.Zero" /> if <paramref name="s" /> is <see cref="F:System.String.Empty" />. </returns>
		/// <param name="s">The managed string to copy. </param>
		/// <exception cref="T:System.PlatformNotSupportedException">The Windows Runtime is not supported on the current version of the operating system. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="s" /> is null. </exception>
		// Token: 0x06005954 RID: 22868 RVA: 0x0012B43C File Offset: 0x0012963C
		[SecurityCritical]
		public unsafe static IntPtr StringToHString(string s)
		{
			if (!Environment.IsWinRTSupported)
			{
				throw new PlatformNotSupportedException(Environment.GetResourceString("Windows Runtime is not supported on this operating system."));
			}
			if (s == null)
			{
				throw new ArgumentNullException("s");
			}
			IntPtr intPtr;
			Marshal.ThrowExceptionForHR(UnsafeNativeMethods.WindowsCreateString(s, s.Length, &intPtr), new IntPtr(-1));
			return intPtr;
		}

		/// <summary>Returns a managed string that contains a copy of the specified Windows Runtime HSTRING. </summary>
		/// <returns>A managed string that contains a copy of the HSTRING if <paramref name="ptr" /> is not <see cref="F:System.IntPtr.Zero" />; otherwise, <see cref="F:System.String.Empty" />. </returns>
		/// <param name="ptr">An unmanaged pointer to the HSTRING to copy. </param>
		/// <exception cref="T:System.PlatformNotSupportedException">The Windows Runtime is not supported on the current version of the operating system. </exception>
		// Token: 0x06005955 RID: 22869 RVA: 0x0012B489 File Offset: 0x00129689
		[SecurityCritical]
		public static string PtrToStringHString(IntPtr ptr)
		{
			if (!Environment.IsWinRTSupported)
			{
				throw new PlatformNotSupportedException(Environment.GetResourceString("Windows Runtime is not supported on this operating system."));
			}
			return WindowsRuntimeMarshal.HStringToString(ptr);
		}

		/// <summary>Frees the specified Windows Runtime HSTRING. </summary>
		/// <param name="ptr">The address of the HSTRING to free.</param>
		/// <exception cref="T:System.PlatformNotSupportedException">The Windows Runtime is not supported on the current version of the operating system. </exception>
		// Token: 0x06005956 RID: 22870 RVA: 0x0012B4A8 File Offset: 0x001296A8
		[SecurityCritical]
		public static void FreeHString(IntPtr ptr)
		{
			if (!Environment.IsWinRTSupported)
			{
				throw new PlatformNotSupportedException(Environment.GetResourceString("Windows Runtime is not supported on this operating system."));
			}
			if (ptr != IntPtr.Zero)
			{
				UnsafeNativeMethods.WindowsDeleteString(ptr);
			}
		}

		// Token: 0x04002E0D RID: 11789
		private static bool s_haveBlueErrorApis = true;

		// Token: 0x04002E0E RID: 11790
		private static Guid s_iidIErrorInfo = new Guid(485667104, 21629, 4123, 142, 101, 8, 0, 43, 43, 209, 25);

		// Token: 0x02000963 RID: 2403
		internal struct EventRegistrationTokenList
		{
			// Token: 0x06005958 RID: 22872 RVA: 0x0012B518 File Offset: 0x00129718
			internal EventRegistrationTokenList(EventRegistrationToken token)
			{
				this.firstToken = token;
				this.restTokens = null;
			}

			// Token: 0x06005959 RID: 22873 RVA: 0x0012B528 File Offset: 0x00129728
			internal EventRegistrationTokenList(WindowsRuntimeMarshal.EventRegistrationTokenList list)
			{
				this.firstToken = list.firstToken;
				this.restTokens = list.restTokens;
			}

			// Token: 0x0600595A RID: 22874 RVA: 0x0012B544 File Offset: 0x00129744
			public bool Push(EventRegistrationToken token)
			{
				bool flag = false;
				if (this.restTokens == null)
				{
					this.restTokens = new List<EventRegistrationToken>();
					flag = true;
				}
				this.restTokens.Add(token);
				return flag;
			}

			// Token: 0x0600595B RID: 22875 RVA: 0x0012B578 File Offset: 0x00129778
			public bool Pop(out EventRegistrationToken token)
			{
				if (this.restTokens == null || this.restTokens.Count == 0)
				{
					token = this.firstToken;
					return false;
				}
				int num = this.restTokens.Count - 1;
				token = this.restTokens[num];
				this.restTokens.RemoveAt(num);
				return true;
			}

			// Token: 0x0600595C RID: 22876 RVA: 0x0012B5D5 File Offset: 0x001297D5
			public void CopyTo(List<EventRegistrationToken> tokens)
			{
				tokens.Add(this.firstToken);
				if (this.restTokens != null)
				{
					tokens.AddRange(this.restTokens);
				}
			}

			// Token: 0x04002E0F RID: 11791
			private EventRegistrationToken firstToken;

			// Token: 0x04002E10 RID: 11792
			private List<EventRegistrationToken> restTokens;
		}

		// Token: 0x02000964 RID: 2404
		internal static class ManagedEventRegistrationImpl
		{
			// Token: 0x0600595D RID: 22877 RVA: 0x0012B5F8 File Offset: 0x001297F8
			[SecurityCritical]
			internal static void AddEventHandler<T>(Func<T, EventRegistrationToken> addMethod, Action<EventRegistrationToken> removeMethod, T handler)
			{
				Dictionary<object, WindowsRuntimeMarshal.EventRegistrationTokenList> eventRegistrationTokenTable = WindowsRuntimeMarshal.ManagedEventRegistrationImpl.GetEventRegistrationTokenTable(removeMethod.Target, removeMethod);
				EventRegistrationToken eventRegistrationToken = addMethod(handler);
				Dictionary<object, WindowsRuntimeMarshal.EventRegistrationTokenList> dictionary = eventRegistrationTokenTable;
				lock (dictionary)
				{
					WindowsRuntimeMarshal.EventRegistrationTokenList eventRegistrationTokenList;
					if (!eventRegistrationTokenTable.TryGetValue(handler, out eventRegistrationTokenList))
					{
						eventRegistrationTokenList = new WindowsRuntimeMarshal.EventRegistrationTokenList(eventRegistrationToken);
						eventRegistrationTokenTable[handler] = eventRegistrationTokenList;
					}
					else if (eventRegistrationTokenList.Push(eventRegistrationToken))
					{
						eventRegistrationTokenTable[handler] = eventRegistrationTokenList;
					}
				}
			}

			// Token: 0x0600595E RID: 22878 RVA: 0x0012B684 File Offset: 0x00129884
			private static Dictionary<object, WindowsRuntimeMarshal.EventRegistrationTokenList> GetEventRegistrationTokenTable(object instance, Action<EventRegistrationToken> removeMethod)
			{
				ConditionalWeakTable<object, Dictionary<MethodInfo, Dictionary<object, WindowsRuntimeMarshal.EventRegistrationTokenList>>> conditionalWeakTable = WindowsRuntimeMarshal.ManagedEventRegistrationImpl.s_eventRegistrations;
				Dictionary<object, WindowsRuntimeMarshal.EventRegistrationTokenList> dictionary3;
				lock (conditionalWeakTable)
				{
					Dictionary<MethodInfo, Dictionary<object, WindowsRuntimeMarshal.EventRegistrationTokenList>> dictionary = null;
					if (!WindowsRuntimeMarshal.ManagedEventRegistrationImpl.s_eventRegistrations.TryGetValue(instance, out dictionary))
					{
						dictionary = new Dictionary<MethodInfo, Dictionary<object, WindowsRuntimeMarshal.EventRegistrationTokenList>>();
						WindowsRuntimeMarshal.ManagedEventRegistrationImpl.s_eventRegistrations.Add(instance, dictionary);
					}
					Dictionary<object, WindowsRuntimeMarshal.EventRegistrationTokenList> dictionary2 = null;
					if (!dictionary.TryGetValue(removeMethod.Method, out dictionary2))
					{
						dictionary2 = new Dictionary<object, WindowsRuntimeMarshal.EventRegistrationTokenList>();
						dictionary.Add(removeMethod.Method, dictionary2);
					}
					dictionary3 = dictionary2;
				}
				return dictionary3;
			}

			// Token: 0x0600595F RID: 22879 RVA: 0x0012B710 File Offset: 0x00129910
			[SecurityCritical]
			internal static void RemoveEventHandler<T>(Action<EventRegistrationToken> removeMethod, T handler)
			{
				Dictionary<object, WindowsRuntimeMarshal.EventRegistrationTokenList> eventRegistrationTokenTable = WindowsRuntimeMarshal.ManagedEventRegistrationImpl.GetEventRegistrationTokenTable(removeMethod.Target, removeMethod);
				Dictionary<object, WindowsRuntimeMarshal.EventRegistrationTokenList> dictionary = eventRegistrationTokenTable;
				EventRegistrationToken eventRegistrationToken;
				lock (dictionary)
				{
					WindowsRuntimeMarshal.EventRegistrationTokenList eventRegistrationTokenList;
					if (!eventRegistrationTokenTable.TryGetValue(handler, out eventRegistrationTokenList))
					{
						return;
					}
					if (!eventRegistrationTokenList.Pop(out eventRegistrationToken))
					{
						eventRegistrationTokenTable.Remove(handler);
					}
				}
				removeMethod(eventRegistrationToken);
			}

			// Token: 0x06005960 RID: 22880 RVA: 0x0012B784 File Offset: 0x00129984
			[SecurityCritical]
			internal static void RemoveAllEventHandlers(Action<EventRegistrationToken> removeMethod)
			{
				Dictionary<object, WindowsRuntimeMarshal.EventRegistrationTokenList> eventRegistrationTokenTable = WindowsRuntimeMarshal.ManagedEventRegistrationImpl.GetEventRegistrationTokenTable(removeMethod.Target, removeMethod);
				List<EventRegistrationToken> list = new List<EventRegistrationToken>();
				Dictionary<object, WindowsRuntimeMarshal.EventRegistrationTokenList> dictionary = eventRegistrationTokenTable;
				lock (dictionary)
				{
					foreach (WindowsRuntimeMarshal.EventRegistrationTokenList eventRegistrationTokenList in eventRegistrationTokenTable.Values)
					{
						eventRegistrationTokenList.CopyTo(list);
					}
					eventRegistrationTokenTable.Clear();
				}
				WindowsRuntimeMarshal.CallRemoveMethods(removeMethod, list);
			}

			// Token: 0x04002E11 RID: 11793
			internal static volatile ConditionalWeakTable<object, Dictionary<MethodInfo, Dictionary<object, WindowsRuntimeMarshal.EventRegistrationTokenList>>> s_eventRegistrations = new ConditionalWeakTable<object, Dictionary<MethodInfo, Dictionary<object, WindowsRuntimeMarshal.EventRegistrationTokenList>>>();
		}

		// Token: 0x02000965 RID: 2405
		internal static class NativeOrStaticEventRegistrationImpl
		{
			// Token: 0x06005962 RID: 22882 RVA: 0x0012B830 File Offset: 0x00129A30
			[SecuritySafeCritical]
			private static object GetInstanceKey(Action<EventRegistrationToken> removeMethod)
			{
				object target = removeMethod.Target;
				if (target == null)
				{
					return removeMethod.Method.DeclaringType;
				}
				return Marshal.GetRawIUnknownForComObjectNoAddRef(target);
			}

			// Token: 0x06005963 RID: 22883 RVA: 0x0012B860 File Offset: 0x00129A60
			[SecurityCritical]
			internal static void AddEventHandler<T>(Func<T, EventRegistrationToken> addMethod, Action<EventRegistrationToken> removeMethod, T handler)
			{
				object instanceKey = WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.GetInstanceKey(removeMethod);
				EventRegistrationToken eventRegistrationToken = addMethod(handler);
				bool flag = false;
				try
				{
					WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.s_eventCacheRWLock.AcquireReaderLock(-1);
					try
					{
						WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.TokenListCount tokenListCount;
						ConditionalWeakTable<object, WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.EventRegistrationTokenListWithCount> orCreateEventRegistrationTokenTable = WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.GetOrCreateEventRegistrationTokenTable(instanceKey, removeMethod, out tokenListCount);
						ConditionalWeakTable<object, WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.EventRegistrationTokenListWithCount> conditionalWeakTable = orCreateEventRegistrationTokenTable;
						lock (conditionalWeakTable)
						{
							WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.EventRegistrationTokenListWithCount eventRegistrationTokenListWithCount;
							if (orCreateEventRegistrationTokenTable.FindEquivalentKeyUnsafe(handler, out eventRegistrationTokenListWithCount) == null)
							{
								eventRegistrationTokenListWithCount = new WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.EventRegistrationTokenListWithCount(tokenListCount, eventRegistrationToken);
								orCreateEventRegistrationTokenTable.Add(handler, eventRegistrationTokenListWithCount);
							}
							else
							{
								eventRegistrationTokenListWithCount.Push(eventRegistrationToken);
							}
							flag = true;
						}
					}
					finally
					{
						WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.s_eventCacheRWLock.ReleaseReaderLock();
					}
				}
				catch (Exception)
				{
					if (!flag)
					{
						removeMethod(eventRegistrationToken);
					}
					throw;
				}
			}

			// Token: 0x06005964 RID: 22884 RVA: 0x0012B930 File Offset: 0x00129B30
			private static ConditionalWeakTable<object, WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.EventRegistrationTokenListWithCount> GetEventRegistrationTokenTableNoCreate(object instance, Action<EventRegistrationToken> removeMethod, out WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.TokenListCount tokenListCount)
			{
				return WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.GetEventRegistrationTokenTableInternal(instance, removeMethod, out tokenListCount, false);
			}

			// Token: 0x06005965 RID: 22885 RVA: 0x0012B93B File Offset: 0x00129B3B
			private static ConditionalWeakTable<object, WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.EventRegistrationTokenListWithCount> GetOrCreateEventRegistrationTokenTable(object instance, Action<EventRegistrationToken> removeMethod, out WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.TokenListCount tokenListCount)
			{
				return WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.GetEventRegistrationTokenTableInternal(instance, removeMethod, out tokenListCount, true);
			}

			// Token: 0x06005966 RID: 22886 RVA: 0x0012B948 File Offset: 0x00129B48
			private static ConditionalWeakTable<object, WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.EventRegistrationTokenListWithCount> GetEventRegistrationTokenTableInternal(object instance, Action<EventRegistrationToken> removeMethod, out WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.TokenListCount tokenListCount, bool createIfNotFound)
			{
				WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.EventCacheKey eventCacheKey;
				eventCacheKey.target = instance;
				eventCacheKey.method = removeMethod.Method;
				Dictionary<WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.EventCacheKey, WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.EventCacheEntry> dictionary = WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.s_eventRegistrations;
				ConditionalWeakTable<object, WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.EventRegistrationTokenListWithCount> registrationTable;
				lock (dictionary)
				{
					WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.EventCacheEntry eventCacheEntry;
					if (!WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.s_eventRegistrations.TryGetValue(eventCacheKey, out eventCacheEntry))
					{
						if (!createIfNotFound)
						{
							tokenListCount = null;
							return null;
						}
						eventCacheEntry = default(WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.EventCacheEntry);
						eventCacheEntry.registrationTable = new ConditionalWeakTable<object, WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.EventRegistrationTokenListWithCount>();
						eventCacheEntry.tokenListCount = new WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.TokenListCount(eventCacheKey);
						WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.s_eventRegistrations.Add(eventCacheKey, eventCacheEntry);
					}
					tokenListCount = eventCacheEntry.tokenListCount;
					registrationTable = eventCacheEntry.registrationTable;
				}
				return registrationTable;
			}

			// Token: 0x06005967 RID: 22887 RVA: 0x0012B9F8 File Offset: 0x00129BF8
			[SecurityCritical]
			internal static void RemoveEventHandler<T>(Action<EventRegistrationToken> removeMethod, T handler)
			{
				object instanceKey = WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.GetInstanceKey(removeMethod);
				WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.s_eventCacheRWLock.AcquireReaderLock(-1);
				EventRegistrationToken eventRegistrationToken;
				try
				{
					WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.TokenListCount tokenListCount;
					ConditionalWeakTable<object, WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.EventRegistrationTokenListWithCount> eventRegistrationTokenTableNoCreate = WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.GetEventRegistrationTokenTableNoCreate(instanceKey, removeMethod, out tokenListCount);
					if (eventRegistrationTokenTableNoCreate == null)
					{
						return;
					}
					ConditionalWeakTable<object, WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.EventRegistrationTokenListWithCount> conditionalWeakTable = eventRegistrationTokenTableNoCreate;
					lock (conditionalWeakTable)
					{
						WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.EventRegistrationTokenListWithCount eventRegistrationTokenListWithCount;
						object obj = eventRegistrationTokenTableNoCreate.FindEquivalentKeyUnsafe(handler, out eventRegistrationTokenListWithCount);
						if (eventRegistrationTokenListWithCount == null)
						{
							return;
						}
						if (!eventRegistrationTokenListWithCount.Pop(out eventRegistrationToken))
						{
							eventRegistrationTokenTableNoCreate.Remove(obj);
						}
					}
				}
				finally
				{
					WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.s_eventCacheRWLock.ReleaseReaderLock();
				}
				removeMethod(eventRegistrationToken);
			}

			// Token: 0x06005968 RID: 22888 RVA: 0x0012BAA0 File Offset: 0x00129CA0
			[SecurityCritical]
			internal static void RemoveAllEventHandlers(Action<EventRegistrationToken> removeMethod)
			{
				object instanceKey = WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.GetInstanceKey(removeMethod);
				List<EventRegistrationToken> list = new List<EventRegistrationToken>();
				WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.s_eventCacheRWLock.AcquireReaderLock(-1);
				try
				{
					WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.TokenListCount tokenListCount;
					ConditionalWeakTable<object, WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.EventRegistrationTokenListWithCount> eventRegistrationTokenTableNoCreate = WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.GetEventRegistrationTokenTableNoCreate(instanceKey, removeMethod, out tokenListCount);
					if (eventRegistrationTokenTableNoCreate == null)
					{
						return;
					}
					ConditionalWeakTable<object, WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.EventRegistrationTokenListWithCount> conditionalWeakTable = eventRegistrationTokenTableNoCreate;
					lock (conditionalWeakTable)
					{
						foreach (WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.EventRegistrationTokenListWithCount eventRegistrationTokenListWithCount in eventRegistrationTokenTableNoCreate.Values)
						{
							eventRegistrationTokenListWithCount.CopyTo(list);
						}
						eventRegistrationTokenTableNoCreate.Clear();
					}
				}
				finally
				{
					WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.s_eventCacheRWLock.ReleaseReaderLock();
				}
				WindowsRuntimeMarshal.CallRemoveMethods(removeMethod, list);
			}

			// Token: 0x04002E12 RID: 11794
			internal static volatile Dictionary<WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.EventCacheKey, WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.EventCacheEntry> s_eventRegistrations = new Dictionary<WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.EventCacheKey, WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.EventCacheEntry>(new WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.EventCacheKeyEqualityComparer());

			// Token: 0x04002E13 RID: 11795
			private static volatile WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.MyReaderWriterLock s_eventCacheRWLock = new WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.MyReaderWriterLock();

			// Token: 0x02000966 RID: 2406
			internal struct EventCacheKey
			{
				// Token: 0x0600596A RID: 22890 RVA: 0x0012BB87 File Offset: 0x00129D87
				public override string ToString()
				{
					return string.Concat(new object[] { "(", this.target, ", ", this.method, ")" });
				}

				// Token: 0x04002E14 RID: 11796
				internal object target;

				// Token: 0x04002E15 RID: 11797
				internal MethodInfo method;
			}

			// Token: 0x02000967 RID: 2407
			internal class EventCacheKeyEqualityComparer : IEqualityComparer<WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.EventCacheKey>
			{
				// Token: 0x0600596B RID: 22891 RVA: 0x0012BBBE File Offset: 0x00129DBE
				public bool Equals(WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.EventCacheKey lhs, WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.EventCacheKey rhs)
				{
					return object.Equals(lhs.target, rhs.target) && object.Equals(lhs.method, rhs.method);
				}

				// Token: 0x0600596C RID: 22892 RVA: 0x0012BBE6 File Offset: 0x00129DE6
				public int GetHashCode(WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.EventCacheKey key)
				{
					return key.target.GetHashCode() ^ key.method.GetHashCode();
				}
			}

			// Token: 0x02000968 RID: 2408
			internal class EventRegistrationTokenListWithCount
			{
				// Token: 0x0600596E RID: 22894 RVA: 0x0012BBFF File Offset: 0x00129DFF
				internal EventRegistrationTokenListWithCount(WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.TokenListCount tokenListCount, EventRegistrationToken token)
				{
					this._tokenListCount = tokenListCount;
					this._tokenListCount.Inc();
					this._tokenList = new WindowsRuntimeMarshal.EventRegistrationTokenList(token);
				}

				// Token: 0x0600596F RID: 22895 RVA: 0x0012BC28 File Offset: 0x00129E28
				~EventRegistrationTokenListWithCount()
				{
					this._tokenListCount.Dec();
				}

				// Token: 0x06005970 RID: 22896 RVA: 0x0012BC5C File Offset: 0x00129E5C
				public void Push(EventRegistrationToken token)
				{
					this._tokenList.Push(token);
				}

				// Token: 0x06005971 RID: 22897 RVA: 0x0012BC6B File Offset: 0x00129E6B
				public bool Pop(out EventRegistrationToken token)
				{
					return this._tokenList.Pop(out token);
				}

				// Token: 0x06005972 RID: 22898 RVA: 0x0012BC79 File Offset: 0x00129E79
				public void CopyTo(List<EventRegistrationToken> tokens)
				{
					this._tokenList.CopyTo(tokens);
				}

				// Token: 0x04002E16 RID: 11798
				private WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.TokenListCount _tokenListCount;

				// Token: 0x04002E17 RID: 11799
				private WindowsRuntimeMarshal.EventRegistrationTokenList _tokenList;
			}

			// Token: 0x02000969 RID: 2409
			internal class TokenListCount
			{
				// Token: 0x06005973 RID: 22899 RVA: 0x0012BC87 File Offset: 0x00129E87
				internal TokenListCount(WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.EventCacheKey key)
				{
					this._key = key;
				}

				// Token: 0x17000FB5 RID: 4021
				// (get) Token: 0x06005974 RID: 22900 RVA: 0x0012BC96 File Offset: 0x00129E96
				internal WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.EventCacheKey Key
				{
					get
					{
						return this._key;
					}
				}

				// Token: 0x06005975 RID: 22901 RVA: 0x0012BC9E File Offset: 0x00129E9E
				internal void Inc()
				{
					Interlocked.Increment(ref this._count);
				}

				// Token: 0x06005976 RID: 22902 RVA: 0x0012BCAC File Offset: 0x00129EAC
				internal void Dec()
				{
					WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.s_eventCacheRWLock.AcquireWriterLock(-1);
					try
					{
						if (Interlocked.Decrement(ref this._count) == 0)
						{
							this.CleanupCache();
						}
					}
					finally
					{
						WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.s_eventCacheRWLock.ReleaseWriterLock();
					}
				}

				// Token: 0x06005977 RID: 22903 RVA: 0x0012BCF8 File Offset: 0x00129EF8
				private void CleanupCache()
				{
					WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.s_eventRegistrations.Remove(this._key);
				}

				// Token: 0x04002E18 RID: 11800
				private int _count;

				// Token: 0x04002E19 RID: 11801
				private WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.EventCacheKey _key;
			}

			// Token: 0x0200096A RID: 2410
			internal struct EventCacheEntry
			{
				// Token: 0x04002E1A RID: 11802
				internal ConditionalWeakTable<object, WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.EventRegistrationTokenListWithCount> registrationTable;

				// Token: 0x04002E1B RID: 11803
				internal WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.TokenListCount tokenListCount;
			}

			// Token: 0x0200096B RID: 2411
			internal class ReaderWriterLockTimedOutException : ApplicationException
			{
			}

			// Token: 0x0200096C RID: 2412
			internal class MyReaderWriterLock
			{
				// Token: 0x06005979 RID: 22905 RVA: 0x00002111 File Offset: 0x00000311
				internal MyReaderWriterLock()
				{
				}

				// Token: 0x0600597A RID: 22906 RVA: 0x0012BD18 File Offset: 0x00129F18
				internal void AcquireReaderLock(int millisecondsTimeout)
				{
					this.EnterMyLock();
					while (this.owners < 0 || this.numWriteWaiters != 0U)
					{
						if (this.readEvent == null)
						{
							this.LazyCreateEvent(ref this.readEvent, false);
						}
						else
						{
							this.WaitOnEvent(this.readEvent, ref this.numReadWaiters, millisecondsTimeout);
						}
					}
					this.owners++;
					this.ExitMyLock();
				}

				// Token: 0x0600597B RID: 22907 RVA: 0x0012BD80 File Offset: 0x00129F80
				internal void AcquireWriterLock(int millisecondsTimeout)
				{
					this.EnterMyLock();
					while (this.owners != 0)
					{
						if (this.writeEvent == null)
						{
							this.LazyCreateEvent(ref this.writeEvent, true);
						}
						else
						{
							this.WaitOnEvent(this.writeEvent, ref this.numWriteWaiters, millisecondsTimeout);
						}
					}
					this.owners = -1;
					this.ExitMyLock();
				}

				// Token: 0x0600597C RID: 22908 RVA: 0x0012BDD6 File Offset: 0x00129FD6
				internal void ReleaseReaderLock()
				{
					this.EnterMyLock();
					this.owners--;
					this.ExitAndWakeUpAppropriateWaiters();
				}

				// Token: 0x0600597D RID: 22909 RVA: 0x0012BDF2 File Offset: 0x00129FF2
				internal void ReleaseWriterLock()
				{
					this.EnterMyLock();
					this.owners++;
					this.ExitAndWakeUpAppropriateWaiters();
				}

				// Token: 0x0600597E RID: 22910 RVA: 0x0012BE10 File Offset: 0x0012A010
				private void LazyCreateEvent(ref EventWaitHandle waitEvent, bool makeAutoResetEvent)
				{
					this.ExitMyLock();
					EventWaitHandle eventWaitHandle;
					if (makeAutoResetEvent)
					{
						eventWaitHandle = new AutoResetEvent(false);
					}
					else
					{
						eventWaitHandle = new ManualResetEvent(false);
					}
					this.EnterMyLock();
					if (waitEvent == null)
					{
						waitEvent = eventWaitHandle;
					}
				}

				// Token: 0x0600597F RID: 22911 RVA: 0x0012BE44 File Offset: 0x0012A044
				private void WaitOnEvent(EventWaitHandle waitEvent, ref uint numWaiters, int millisecondsTimeout)
				{
					waitEvent.Reset();
					numWaiters += 1U;
					bool flag = false;
					this.ExitMyLock();
					try
					{
						if (!waitEvent.WaitOne(millisecondsTimeout, false))
						{
							throw new WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.ReaderWriterLockTimedOutException();
						}
						flag = true;
					}
					finally
					{
						this.EnterMyLock();
						numWaiters -= 1U;
						if (!flag)
						{
							this.ExitMyLock();
						}
					}
				}

				// Token: 0x06005980 RID: 22912 RVA: 0x0012BEA0 File Offset: 0x0012A0A0
				private void ExitAndWakeUpAppropriateWaiters()
				{
					if (this.owners == 0 && this.numWriteWaiters > 0U)
					{
						this.ExitMyLock();
						this.writeEvent.Set();
						return;
					}
					if (this.owners >= 0 && this.numReadWaiters != 0U)
					{
						this.ExitMyLock();
						this.readEvent.Set();
						return;
					}
					this.ExitMyLock();
				}

				// Token: 0x06005981 RID: 22913 RVA: 0x0012BEFB File Offset: 0x0012A0FB
				private void EnterMyLock()
				{
					if (Interlocked.CompareExchange(ref this.myLock, 1, 0) != 0)
					{
						this.EnterMyLockSpin();
					}
				}

				// Token: 0x06005982 RID: 22914 RVA: 0x0012BF14 File Offset: 0x0012A114
				private void EnterMyLockSpin()
				{
					int num = 0;
					for (;;)
					{
						if (num < 3 && Environment.ProcessorCount > 1)
						{
							Thread.SpinWait(20);
						}
						else
						{
							Thread.Sleep(0);
						}
						if (Interlocked.CompareExchange(ref this.myLock, 1, 0) == 0)
						{
							break;
						}
						num++;
					}
				}

				// Token: 0x06005983 RID: 22915 RVA: 0x0012BF53 File Offset: 0x0012A153
				private void ExitMyLock()
				{
					this.myLock = 0;
				}

				// Token: 0x04002E1C RID: 11804
				private int myLock;

				// Token: 0x04002E1D RID: 11805
				private int owners;

				// Token: 0x04002E1E RID: 11806
				private uint numWriteWaiters;

				// Token: 0x04002E1F RID: 11807
				private uint numReadWaiters;

				// Token: 0x04002E20 RID: 11808
				private EventWaitHandle writeEvent;

				// Token: 0x04002E21 RID: 11809
				private EventWaitHandle readEvent;
			}
		}
	}
}
