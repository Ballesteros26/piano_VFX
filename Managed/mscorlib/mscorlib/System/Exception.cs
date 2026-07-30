using System;
using System.Collections;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security;

namespace System
{
	/// <summary>Represents errors that occur during application execution.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x0200015D RID: 349
	[ComVisible(true)]
	[ComDefaultInterface(typeof(_Exception))]
	[ClassInterface(ClassInterfaceType.None)]
	[Serializable]
	[StructLayout(LayoutKind.Sequential)]
	public class Exception : ISerializable, _Exception
	{
		// Token: 0x06000F02 RID: 3842 RVA: 0x0003E123 File Offset: 0x0003C323
		private void Init()
		{
			this._message = null;
			this._stackTrace = null;
			this._dynamicMethods = null;
			this.HResult = -2146233088;
			this._safeSerializationManager = new SafeSerializationManager();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Exception" /> class.</summary>
		// Token: 0x06000F03 RID: 3843 RVA: 0x0003E150 File Offset: 0x0003C350
		public Exception()
		{
			this.Init();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Exception" /> class with a specified error message.</summary>
		/// <param name="message">The message that describes the error. </param>
		// Token: 0x06000F04 RID: 3844 RVA: 0x0003E15E File Offset: 0x0003C35E
		public Exception(string message)
		{
			this.Init();
			this._message = message;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Exception" /> class with a specified error message and a reference to the inner exception that is the cause of this exception.</summary>
		/// <param name="message">The error message that explains the reason for the exception. </param>
		/// <param name="innerException">The exception that is the cause of the current exception, or a null reference (Nothing in Visual Basic) if no inner exception is specified. </param>
		// Token: 0x06000F05 RID: 3845 RVA: 0x0003E173 File Offset: 0x0003C373
		public Exception(string message, Exception innerException)
		{
			this.Init();
			this._message = message;
			this._innerException = innerException;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Exception" /> class with serialized data.</summary>
		/// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> that holds the serialized object data about the exception being thrown. </param>
		/// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext" /> that contains contextual information about the source or destination. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="info" /> parameter is null. </exception>
		/// <exception cref="T:System.Runtime.Serialization.SerializationException">The class name is null or <see cref="P:System.Exception.HResult" /> is zero (0). </exception>
		// Token: 0x06000F06 RID: 3846 RVA: 0x0003E190 File Offset: 0x0003C390
		[SecuritySafeCritical]
		protected Exception(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			this._className = info.GetString("ClassName");
			this._message = info.GetString("Message");
			this._data = (IDictionary)info.GetValueNoThrow("Data", typeof(IDictionary));
			this._innerException = (Exception)info.GetValue("InnerException", typeof(Exception));
			this._helpURL = info.GetString("HelpURL");
			this._stackTraceString = info.GetString("StackTraceString");
			this._remoteStackTraceString = info.GetString("RemoteStackTraceString");
			this._remoteStackIndex = info.GetInt32("RemoteStackIndex");
			this.HResult = info.GetInt32("HResult");
			this._source = info.GetString("Source");
			this._safeSerializationManager = info.GetValueNoThrow("SafeSerializationManager", typeof(SafeSerializationManager)) as SafeSerializationManager;
			if (this._className == null || this.HResult == 0)
			{
				throw new SerializationException(Environment.GetResourceString("Insufficient state to return the real object."));
			}
			if (context.State == StreamingContextStates.CrossAppDomain)
			{
				this._remoteStackTraceString += this._stackTraceString;
				this._stackTraceString = null;
			}
		}

		/// <summary>Gets a message that describes the current exception.</summary>
		/// <returns>The error message that explains the reason for the exception, or an empty string ("").</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170001E8 RID: 488
		// (get) Token: 0x06000F07 RID: 3847 RVA: 0x0003E2E5 File Offset: 0x0003C4E5
		public virtual string Message
		{
			get
			{
				if (this._message == null)
				{
					if (this._className == null)
					{
						this._className = this.GetClassName();
					}
					return Environment.GetResourceString("Exception of type '{0}' was thrown.", new object[] { this._className });
				}
				return this._message;
			}
		}

		/// <summary>Gets a collection of key/value pairs that provide additional user-defined information about the exception.</summary>
		/// <returns>An object that implements the <see cref="T:System.Collections.IDictionary" /> interface and contains a collection of user-defined key/value pairs. The default is an empty collection.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170001E9 RID: 489
		// (get) Token: 0x06000F08 RID: 3848 RVA: 0x0003E323 File Offset: 0x0003C523
		public virtual IDictionary Data
		{
			[SecuritySafeCritical]
			get
			{
				if (this._data == null)
				{
					if (Exception.IsImmutableAgileException(this))
					{
						this._data = new EmptyReadOnlyDictionaryInternal();
					}
					else
					{
						this._data = new ListDictionaryInternal();
					}
				}
				return this._data;
			}
		}

		// Token: 0x06000F09 RID: 3849 RVA: 0x00015ED5 File Offset: 0x000140D5
		private static bool IsImmutableAgileException(Exception e)
		{
			return false;
		}

		// Token: 0x06000F0A RID: 3850 RVA: 0x0003E354 File Offset: 0x0003C554
		[FriendAccessAllowed]
		internal void AddExceptionDataForRestrictedErrorInfo(string restrictedError, string restrictedErrorReference, string restrictedCapabilitySid, object restrictedErrorObject, bool hasrestrictedLanguageErrorObject = false)
		{
			IDictionary data = this.Data;
			if (data != null)
			{
				data.Add("RestrictedDescription", restrictedError);
				data.Add("RestrictedErrorReference", restrictedErrorReference);
				data.Add("RestrictedCapabilitySid", restrictedCapabilitySid);
				data.Add("__RestrictedErrorObject", (restrictedErrorObject == null) ? null : new Exception.__RestrictedErrorObject(restrictedErrorObject));
				data.Add("__HasRestrictedLanguageErrorObject", hasrestrictedLanguageErrorObject);
			}
		}

		// Token: 0x06000F0B RID: 3851 RVA: 0x0003E3BC File Offset: 0x0003C5BC
		internal bool TryGetRestrictedLanguageErrorObject(out object restrictedErrorObject)
		{
			restrictedErrorObject = null;
			if (this.Data != null && this.Data.Contains("__HasRestrictedLanguageErrorObject"))
			{
				if (this.Data.Contains("__RestrictedErrorObject"))
				{
					Exception.__RestrictedErrorObject _RestrictedErrorObject = this.Data["__RestrictedErrorObject"] as Exception.__RestrictedErrorObject;
					if (_RestrictedErrorObject != null)
					{
						restrictedErrorObject = _RestrictedErrorObject.RealErrorObject;
					}
				}
				return (bool)this.Data["__HasRestrictedLanguageErrorObject"];
			}
			return false;
		}

		// Token: 0x06000F0C RID: 3852 RVA: 0x0003E430 File Offset: 0x0003C630
		private string GetClassName()
		{
			if (this._className == null)
			{
				this._className = this.GetType().ToString();
			}
			return this._className;
		}

		/// <summary>When overridden in a derived class, returns the <see cref="T:System.Exception" /> that is the root cause of one or more subsequent exceptions.</summary>
		/// <returns>The first exception thrown in a chain of exceptions. If the <see cref="P:System.Exception.InnerException" /> property of the current exception is a null reference (Nothing in Visual Basic), this property returns the current exception.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000F0D RID: 3853 RVA: 0x0003E454 File Offset: 0x0003C654
		public virtual Exception GetBaseException()
		{
			Exception ex = this.InnerException;
			Exception ex2 = this;
			while (ex != null)
			{
				ex2 = ex;
				ex = ex.InnerException;
			}
			return ex2;
		}

		/// <summary>Gets the <see cref="T:System.Exception" /> instance that caused the current exception.</summary>
		/// <returns>An instance of Exception that describes the error that caused the current exception. The InnerException property returns the same value as was passed into the constructor, or a null reference (Nothing in Visual Basic) if the inner exception value was not supplied to the constructor. This property is read-only.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170001EA RID: 490
		// (get) Token: 0x06000F0E RID: 3854 RVA: 0x0003E479 File Offset: 0x0003C679
		public Exception InnerException
		{
			get
			{
				return this._innerException;
			}
		}

		// Token: 0x06000F0F RID: 3855
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IRuntimeMethodInfo GetMethodFromStackTrace(object stackTrace);

		/// <summary>Gets the method that throws the current exception.</summary>
		/// <returns>The <see cref="T:System.Reflection.MethodBase" /> that threw the current exception.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x170001EB RID: 491
		// (get) Token: 0x06000F10 RID: 3856 RVA: 0x0003E484 File Offset: 0x0003C684
		public MethodBase TargetSite
		{
			[SecuritySafeCritical]
			get
			{
				StackTrace stackTrace = new StackTrace(this, true);
				if (stackTrace.FrameCount > 0)
				{
					return stackTrace.GetFrame(0).GetMethod();
				}
				return null;
			}
		}

		/// <summary>Gets a string representation of the immediate frames on the call stack.</summary>
		/// <returns>A string that describes the immediate frames of the call stack.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x170001EC RID: 492
		// (get) Token: 0x06000F11 RID: 3857 RVA: 0x0003E4B0 File Offset: 0x0003C6B0
		public virtual string StackTrace
		{
			get
			{
				return this.GetStackTrace(true);
			}
		}

		// Token: 0x06000F12 RID: 3858 RVA: 0x0003E4BC File Offset: 0x0003C6BC
		private string GetStackTrace(bool needFileInfo)
		{
			string text = this._stackTraceString;
			string text2 = this._remoteStackTraceString;
			if (!needFileInfo)
			{
				text = this.StripFileInfo(text, false);
				text2 = this.StripFileInfo(text2, true);
			}
			if (text != null)
			{
				return text2 + text;
			}
			if (this._stackTrace == null)
			{
				return text2;
			}
			string stackTrace = Environment.GetStackTrace(this, needFileInfo);
			return text2 + stackTrace;
		}

		// Token: 0x06000F13 RID: 3859 RVA: 0x0003E510 File Offset: 0x0003C710
		[FriendAccessAllowed]
		internal void SetErrorCode(int hr)
		{
			this.HResult = hr;
		}

		/// <summary>Gets or sets a link to the help file associated with this exception.</summary>
		/// <returns>The Uniform Resource Name (URN) or Uniform Resource Locator (URL).</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170001ED RID: 493
		// (get) Token: 0x06000F14 RID: 3860 RVA: 0x0003E519 File Offset: 0x0003C719
		// (set) Token: 0x06000F15 RID: 3861 RVA: 0x0003E521 File Offset: 0x0003C721
		public virtual string HelpLink
		{
			get
			{
				return this._helpURL;
			}
			set
			{
				this._helpURL = value;
			}
		}

		/// <summary>Gets or sets the name of the application or the object that causes the error.</summary>
		/// <returns>The name of the application or the object that causes the error.</returns>
		/// <exception cref="T:System.ArgumentException">The object must be a runtime <see cref="N:System.Reflection" /> object</exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170001EE RID: 494
		// (get) Token: 0x06000F16 RID: 3862 RVA: 0x0003E52C File Offset: 0x0003C72C
		// (set) Token: 0x06000F17 RID: 3863 RVA: 0x0003E589 File Offset: 0x0003C789
		public virtual string Source
		{
			get
			{
				if (this._source == null)
				{
					StackTrace stackTrace = new StackTrace(this, true);
					if (stackTrace.FrameCount > 0)
					{
						MethodBase method = stackTrace.GetFrame(0).GetMethod();
						if (method != null)
						{
							this._source = method.DeclaringType.Assembly.GetName().Name;
						}
					}
				}
				return this._source;
			}
			set
			{
				this._source = value;
			}
		}

		/// <summary>Creates and returns a string representation of the current exception.</summary>
		/// <returns>A string representation of the current exception.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06000F18 RID: 3864 RVA: 0x0003E592 File Offset: 0x0003C792
		public override string ToString()
		{
			return this.ToString(true, true);
		}

		// Token: 0x06000F19 RID: 3865 RVA: 0x0003E59C File Offset: 0x0003C79C
		private string ToString(bool needFileLineInfo, bool needMessage)
		{
			string text = (needMessage ? this.Message : null);
			string text2;
			if (text == null || text.Length <= 0)
			{
				text2 = this.GetClassName();
			}
			else
			{
				text2 = this.GetClassName() + ": " + text;
			}
			if (this._innerException != null)
			{
				text2 = string.Concat(new string[]
				{
					text2,
					" ---> ",
					this._innerException.ToString(needFileLineInfo, needMessage),
					Environment.NewLine,
					"   ",
					Environment.GetResourceString("--- End of inner exception stack trace ---")
				});
			}
			string stackTrace = this.GetStackTrace(needFileLineInfo);
			if (stackTrace != null)
			{
				text2 = text2 + Environment.NewLine + stackTrace;
			}
			return text2;
		}

		/// <summary>Occurs when an exception is serialized to create an exception state object that contains serialized data about the exception.</summary>
		// Token: 0x14000003 RID: 3
		// (add) Token: 0x06000F1A RID: 3866 RVA: 0x0003E643 File Offset: 0x0003C843
		// (remove) Token: 0x06000F1B RID: 3867 RVA: 0x0003E651 File Offset: 0x0003C851
		protected event EventHandler<SafeSerializationEventArgs> SerializeObjectState
		{
			add
			{
				this._safeSerializationManager.SerializeObjectState += value;
			}
			remove
			{
				this._safeSerializationManager.SerializeObjectState -= value;
			}
		}

		/// <summary>When overridden in a derived class, sets the <see cref="T:System.Runtime.Serialization.SerializationInfo" /> with information about the exception.</summary>
		/// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> that holds the serialized object data about the exception being thrown. </param>
		/// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext" /> that contains contextual information about the source or destination. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="info" /> parameter is a null reference (Nothing in Visual Basic). </exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Read="*AllFiles*" PathDiscovery="*AllFiles*" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="SerializationFormatter" />
		/// </PermissionSet>
		// Token: 0x06000F1C RID: 3868 RVA: 0x0003E660 File Offset: 0x0003C860
		[SecurityCritical]
		public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			string text = this._stackTraceString;
			if (this._stackTrace != null && text == null)
			{
				text = Environment.GetStackTrace(this, true);
			}
			if (this._source == null)
			{
				this._source = this.Source;
			}
			info.AddValue("ClassName", this.GetClassName(), typeof(string));
			info.AddValue("Message", this._message, typeof(string));
			info.AddValue("Data", this._data, typeof(IDictionary));
			info.AddValue("InnerException", this._innerException, typeof(Exception));
			info.AddValue("HelpURL", this._helpURL, typeof(string));
			info.AddValue("StackTraceString", text, typeof(string));
			info.AddValue("RemoteStackTraceString", this._remoteStackTraceString, typeof(string));
			info.AddValue("RemoteStackIndex", this._remoteStackIndex, typeof(int));
			info.AddValue("ExceptionMethod", null);
			info.AddValue("HResult", this.HResult);
			info.AddValue("Source", this._source, typeof(string));
			if (this._safeSerializationManager != null && this._safeSerializationManager.IsActive)
			{
				info.AddValue("SafeSerializationManager", this._safeSerializationManager, typeof(SafeSerializationManager));
				this._safeSerializationManager.CompleteSerialization(this, info, context);
			}
		}

		// Token: 0x06000F1D RID: 3869 RVA: 0x0003E7F8 File Offset: 0x0003C9F8
		internal Exception PrepForRemoting()
		{
			string text;
			if (this._remoteStackIndex == 0)
			{
				text = string.Concat(new object[]
				{
					Environment.NewLine,
					"Server stack trace: ",
					Environment.NewLine,
					this.StackTrace,
					Environment.NewLine,
					Environment.NewLine,
					"Exception rethrown at [",
					this._remoteStackIndex,
					"]: ",
					Environment.NewLine
				});
			}
			else
			{
				text = string.Concat(new object[]
				{
					this.StackTrace,
					Environment.NewLine,
					Environment.NewLine,
					"Exception rethrown at [",
					this._remoteStackIndex,
					"]: ",
					Environment.NewLine
				});
			}
			this._remoteStackTraceString = text;
			this._remoteStackIndex++;
			return this;
		}

		// Token: 0x06000F1E RID: 3870 RVA: 0x0003E8D7 File Offset: 0x0003CAD7
		[OnDeserialized]
		private void OnDeserialized(StreamingContext context)
		{
			this._stackTrace = null;
			if (this._safeSerializationManager == null)
			{
				this._safeSerializationManager = new SafeSerializationManager();
				return;
			}
			this._safeSerializationManager.CompleteDeserialization(this);
		}

		// Token: 0x06000F1F RID: 3871 RVA: 0x0003E900 File Offset: 0x0003CB00
		internal void InternalPreserveStackTrace()
		{
			string stackTrace = this.StackTrace;
			if (stackTrace != null && stackTrace.Length > 0)
			{
				this._remoteStackTraceString = stackTrace + Environment.NewLine;
			}
			this._stackTrace = null;
			this._stackTraceString = null;
		}

		// Token: 0x170001EF RID: 495
		// (get) Token: 0x06000F20 RID: 3872 RVA: 0x0003E93F File Offset: 0x0003CB3F
		internal string RemoteStackTrace
		{
			get
			{
				return this._remoteStackTraceString;
			}
		}

		// Token: 0x06000F21 RID: 3873 RVA: 0x0000213D File Offset: 0x0000033D
		private string StripFileInfo(string stackTrace, bool isRemoteStackTrace)
		{
			return stackTrace;
		}

		// Token: 0x06000F22 RID: 3874 RVA: 0x0003E947 File Offset: 0x0003CB47
		[SecuritySafeCritical]
		internal void RestoreExceptionDispatchInfo(ExceptionDispatchInfo exceptionDispatchInfo)
		{
			this.captured_traces = (StackTrace[])exceptionDispatchInfo.BinaryStackTraceArray;
			this._stackTrace = null;
			this._stackTraceString = null;
		}

		/// <summary>Gets or sets HRESULT, a coded numerical value that is assigned to a specific exception.</summary>
		/// <returns>The HRESULT value.</returns>
		// Token: 0x170001F0 RID: 496
		// (get) Token: 0x06000F23 RID: 3875 RVA: 0x0003E968 File Offset: 0x0003CB68
		// (set) Token: 0x06000F24 RID: 3876 RVA: 0x0003E970 File Offset: 0x0003CB70
		public int HResult
		{
			get
			{
				return this._HResult;
			}
			protected set
			{
				this._HResult = value;
			}
		}

		// Token: 0x06000F25 RID: 3877 RVA: 0x0003E97C File Offset: 0x0003CB7C
		[SecurityCritical]
		internal virtual string InternalToString()
		{
			bool flag = true;
			return this.ToString(flag, true);
		}

		/// <summary>Gets the runtime type of the current instance.</summary>
		/// <returns>A <see cref="T:System.Type" /> object that represents the exact runtime type of the current instance.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000F26 RID: 3878 RVA: 0x00033A19 File Offset: 0x00031C19
		public new Type GetType()
		{
			return base.GetType();
		}

		// Token: 0x170001F1 RID: 497
		// (get) Token: 0x06000F27 RID: 3879 RVA: 0x0003E993 File Offset: 0x0003CB93
		internal bool IsTransient
		{
			[SecuritySafeCritical]
			get
			{
				return Exception.nIsTransient(this._HResult);
			}
		}

		// Token: 0x06000F28 RID: 3880
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool nIsTransient(int hr);

		// Token: 0x06000F29 RID: 3881 RVA: 0x0003E9A0 File Offset: 0x0003CBA0
		[SecuritySafeCritical]
		internal static string GetMessageFromNativeResources(Exception.ExceptionMessageKind kind)
		{
			switch (kind)
			{
			case Exception.ExceptionMessageKind.ThreadAbort:
				return "";
			case Exception.ExceptionMessageKind.ThreadInterrupted:
				return "";
			case Exception.ExceptionMessageKind.OutOfMemory:
				return "Out of memory";
			default:
				return "";
			}
		}

		// Token: 0x06000F2A RID: 3882 RVA: 0x0003E9CF File Offset: 0x0003CBCF
		internal void SetMessage(string s)
		{
			this._message = s;
		}

		// Token: 0x06000F2B RID: 3883 RVA: 0x0003E9D8 File Offset: 0x0003CBD8
		internal void SetStackTrace(string s)
		{
			this._stackTraceString = s;
		}

		// Token: 0x06000F2C RID: 3884 RVA: 0x0003E9E4 File Offset: 0x0003CBE4
		internal Exception FixRemotingException()
		{
			string text = string.Format((this._remoteStackIndex == 0) ? Locale.GetText("{0}{0}Server stack trace: {0}{1}{0}{0}Exception rethrown at [{2}]: {0}") : Locale.GetText("{1}{0}{0}Exception rethrown at [{2}]: {0}"), Environment.NewLine, this.StackTrace, this._remoteStackIndex);
			this._remoteStackTraceString = text;
			this._remoteStackIndex++;
			this._stackTraceString = null;
			return this;
		}

		// Token: 0x06000F2D RID: 3885
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void ReportUnhandledException(Exception exception);

		// Token: 0x04000901 RID: 2305
		[OptionalField]
		private static object s_EDILock = new object();

		// Token: 0x04000902 RID: 2306
		private string _className;

		// Token: 0x04000903 RID: 2307
		internal string _message;

		// Token: 0x04000904 RID: 2308
		private IDictionary _data;

		// Token: 0x04000905 RID: 2309
		private Exception _innerException;

		// Token: 0x04000906 RID: 2310
		private string _helpURL;

		// Token: 0x04000907 RID: 2311
		private object _stackTrace;

		// Token: 0x04000908 RID: 2312
		private string _stackTraceString;

		// Token: 0x04000909 RID: 2313
		private string _remoteStackTraceString;

		// Token: 0x0400090A RID: 2314
		private int _remoteStackIndex;

		// Token: 0x0400090B RID: 2315
		private object _dynamicMethods;

		// Token: 0x0400090C RID: 2316
		internal int _HResult;

		// Token: 0x0400090D RID: 2317
		private string _source;

		// Token: 0x0400090E RID: 2318
		[OptionalField(VersionAdded = 4)]
		private SafeSerializationManager _safeSerializationManager;

		// Token: 0x0400090F RID: 2319
		internal StackTrace[] captured_traces;

		// Token: 0x04000910 RID: 2320
		private IntPtr[] native_trace_ips;

		// Token: 0x04000911 RID: 2321
		private const int _COMPlusExceptionCode = -532462766;

		// Token: 0x0200015E RID: 350
		[Serializable]
		internal class __RestrictedErrorObject
		{
			// Token: 0x06000F2F RID: 3887 RVA: 0x0003EA54 File Offset: 0x0003CC54
			internal __RestrictedErrorObject(object errorObject)
			{
				this._realErrorObject = errorObject;
			}

			// Token: 0x170001F2 RID: 498
			// (get) Token: 0x06000F30 RID: 3888 RVA: 0x0003EA63 File Offset: 0x0003CC63
			public object RealErrorObject
			{
				get
				{
					return this._realErrorObject;
				}
			}

			// Token: 0x04000912 RID: 2322
			[NonSerialized]
			private object _realErrorObject;
		}

		// Token: 0x0200015F RID: 351
		internal enum ExceptionMessageKind
		{
			// Token: 0x04000914 RID: 2324
			ThreadAbort = 1,
			// Token: 0x04000915 RID: 2325
			ThreadInterrupted,
			// Token: 0x04000916 RID: 2326
			OutOfMemory
		}
	}
}
