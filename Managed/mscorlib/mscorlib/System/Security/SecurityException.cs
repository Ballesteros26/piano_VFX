using System;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security.Permissions;
using System.Security.Policy;
using System.Text;

namespace System.Security
{
	/// <summary>The exception that is thrown when a security error is detected.</summary>
	// Token: 0x0200054C RID: 1356
	[ComVisible(true)]
	[Serializable]
	public class SecurityException : SystemException
	{
		/// <summary>Gets or sets the security action that caused the exception.</summary>
		/// <returns>One of the <see cref="T:System.Security.Permissions.SecurityAction" /> values.</returns>
		// Token: 0x170009F8 RID: 2552
		// (get) Token: 0x06003D0A RID: 15626 RVA: 0x000DBF4C File Offset: 0x000DA14C
		// (set) Token: 0x06003D0B RID: 15627 RVA: 0x000DBF54 File Offset: 0x000DA154
		[ComVisible(false)]
		public SecurityAction Action
		{
			get
			{
				return this._action;
			}
			set
			{
				this._action = value;
			}
		}

		/// <summary>Gets or sets the denied security permission, permission set, or permission set collection that caused a demand to fail.</summary>
		/// <returns>A permission, permission set, or permission set collection object.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence, ControlPolicy" />
		/// </PermissionSet>
		// Token: 0x170009F9 RID: 2553
		// (get) Token: 0x06003D0C RID: 15628 RVA: 0x000DBF5D File Offset: 0x000DA15D
		// (set) Token: 0x06003D0D RID: 15629 RVA: 0x000DBF65 File Offset: 0x000DA165
		[ComVisible(false)]
		public object DenySetInstance
		{
			[SecurityPermission(SecurityAction.Demand, ControlEvidence = true, ControlPolicy = true)]
			get
			{
				return this._denyset;
			}
			set
			{
				this._denyset = value;
			}
		}

		/// <summary>Gets or sets information about the failed assembly.</summary>
		/// <returns>An <see cref="T:System.Reflection.AssemblyName" /> that identifies the failed assembly.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence, ControlPolicy" />
		/// </PermissionSet>
		// Token: 0x170009FA RID: 2554
		// (get) Token: 0x06003D0E RID: 15630 RVA: 0x000DBF6E File Offset: 0x000DA16E
		// (set) Token: 0x06003D0F RID: 15631 RVA: 0x000DBF76 File Offset: 0x000DA176
		[ComVisible(false)]
		public AssemblyName FailedAssemblyInfo
		{
			[SecurityPermission(SecurityAction.Demand, ControlEvidence = true, ControlPolicy = true)]
			get
			{
				return this._assembly;
			}
			set
			{
				this._assembly = value;
			}
		}

		/// <summary>Gets or sets the information about the method associated with the exception.</summary>
		/// <returns>A <see cref="T:System.Reflection.MethodInfo" /> object describing the method.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence, ControlPolicy" />
		/// </PermissionSet>
		// Token: 0x170009FB RID: 2555
		// (get) Token: 0x06003D10 RID: 15632 RVA: 0x000DBF7F File Offset: 0x000DA17F
		// (set) Token: 0x06003D11 RID: 15633 RVA: 0x000DBF87 File Offset: 0x000DA187
		[ComVisible(false)]
		public MethodInfo Method
		{
			[SecurityPermission(SecurityAction.Demand, ControlEvidence = true, ControlPolicy = true)]
			get
			{
				return this._method;
			}
			set
			{
				this._method = value;
			}
		}

		/// <summary>Gets or sets the permission, permission set, or permission set collection that is part of the permit-only stack frame that caused a security check to fail.</summary>
		/// <returns>A permission, permission set, or permission set collection object.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence, ControlPolicy" />
		/// </PermissionSet>
		// Token: 0x170009FC RID: 2556
		// (get) Token: 0x06003D12 RID: 15634 RVA: 0x000DBF90 File Offset: 0x000DA190
		// (set) Token: 0x06003D13 RID: 15635 RVA: 0x000DBF98 File Offset: 0x000DA198
		[ComVisible(false)]
		public object PermitOnlySetInstance
		{
			[SecurityPermission(SecurityAction.Demand, ControlEvidence = true, ControlPolicy = true)]
			get
			{
				return this._permitset;
			}
			set
			{
				this._permitset = value;
			}
		}

		/// <summary>Gets or sets the URL of the assembly that caused the exception.</summary>
		/// <returns>A URL that identifies the location of the assembly.</returns>
		// Token: 0x170009FD RID: 2557
		// (get) Token: 0x06003D14 RID: 15636 RVA: 0x000DBFA1 File Offset: 0x000DA1A1
		// (set) Token: 0x06003D15 RID: 15637 RVA: 0x000DBFA9 File Offset: 0x000DA1A9
		public string Url
		{
			[SecurityPermission(SecurityAction.Demand, ControlEvidence = true, ControlPolicy = true)]
			get
			{
				return this._url;
			}
			set
			{
				this._url = value;
			}
		}

		/// <summary>Gets or sets the zone of the assembly that caused the exception.</summary>
		/// <returns>One of the <see cref="T:System.Security.SecurityZone" /> values that identifies the zone of the assembly that caused the exception.</returns>
		// Token: 0x170009FE RID: 2558
		// (get) Token: 0x06003D16 RID: 15638 RVA: 0x000DBFB2 File Offset: 0x000DA1B2
		// (set) Token: 0x06003D17 RID: 15639 RVA: 0x000DBFBA File Offset: 0x000DA1BA
		public SecurityZone Zone
		{
			get
			{
				return this._zone;
			}
			set
			{
				this._zone = value;
			}
		}

		/// <summary>Gets or sets the demanded security permission, permission set, or permission set collection that failed.</summary>
		/// <returns>A permission, permission set, or permission set collection object.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence, ControlPolicy" />
		/// </PermissionSet>
		// Token: 0x170009FF RID: 2559
		// (get) Token: 0x06003D18 RID: 15640 RVA: 0x000DBFC3 File Offset: 0x000DA1C3
		// (set) Token: 0x06003D19 RID: 15641 RVA: 0x000DBFCB File Offset: 0x000DA1CB
		[ComVisible(false)]
		public object Demanded
		{
			[SecurityPermission(SecurityAction.Demand, ControlEvidence = true, ControlPolicy = true)]
			get
			{
				return this._demanded;
			}
			set
			{
				this._demanded = value;
			}
		}

		/// <summary>Gets or sets the first permission in a permission set or permission set collection that failed the demand.</summary>
		/// <returns>An <see cref="T:System.Security.IPermission" /> object representing the first permission that failed.</returns>
		// Token: 0x17000A00 RID: 2560
		// (get) Token: 0x06003D1A RID: 15642 RVA: 0x000DBFD4 File Offset: 0x000DA1D4
		// (set) Token: 0x06003D1B RID: 15643 RVA: 0x000DBFDC File Offset: 0x000DA1DC
		public IPermission FirstPermissionThatFailed
		{
			[SecurityPermission(SecurityAction.Demand, ControlEvidence = true, ControlPolicy = true)]
			get
			{
				return this._firstperm;
			}
			set
			{
				this._firstperm = value;
			}
		}

		/// <summary>Gets or sets the state of the permission that threw the exception.</summary>
		/// <returns>The state of the permission at the time the exception was thrown.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence, ControlPolicy" />
		/// </PermissionSet>
		// Token: 0x17000A01 RID: 2561
		// (get) Token: 0x06003D1C RID: 15644 RVA: 0x000DBFE5 File Offset: 0x000DA1E5
		// (set) Token: 0x06003D1D RID: 15645 RVA: 0x000DBFED File Offset: 0x000DA1ED
		public string PermissionState
		{
			[SecurityPermission(SecurityAction.Demand, ControlEvidence = true, ControlPolicy = true)]
			get
			{
				return this.permissionState;
			}
			set
			{
				this.permissionState = value;
			}
		}

		/// <summary>Gets or sets the type of the permission that failed.</summary>
		/// <returns>The type of the permission that failed.</returns>
		// Token: 0x17000A02 RID: 2562
		// (get) Token: 0x06003D1E RID: 15646 RVA: 0x000DBFF6 File Offset: 0x000DA1F6
		// (set) Token: 0x06003D1F RID: 15647 RVA: 0x000DBFFE File Offset: 0x000DA1FE
		public Type PermissionType
		{
			get
			{
				return this.permissionType;
			}
			set
			{
				this.permissionType = value;
			}
		}

		/// <summary>Gets or sets the granted permission set of the assembly that caused the <see cref="T:System.Security.SecurityException" />.</summary>
		/// <returns>The XML representation of the granted set of the assembly.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence, ControlPolicy" />
		/// </PermissionSet>
		// Token: 0x17000A03 RID: 2563
		// (get) Token: 0x06003D20 RID: 15648 RVA: 0x000DC007 File Offset: 0x000DA207
		// (set) Token: 0x06003D21 RID: 15649 RVA: 0x000DC00F File Offset: 0x000DA20F
		public string GrantedSet
		{
			[SecurityPermission(SecurityAction.Demand, ControlEvidence = true, ControlPolicy = true)]
			get
			{
				return this._granted;
			}
			set
			{
				this._granted = value;
			}
		}

		/// <summary>Gets or sets the refused permission set of the assembly that caused the <see cref="T:System.Security.SecurityException" />.</summary>
		/// <returns>The XML representation of the refused permission set of the assembly.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence, ControlPolicy" />
		/// </PermissionSet>
		// Token: 0x17000A04 RID: 2564
		// (get) Token: 0x06003D22 RID: 15650 RVA: 0x000DC018 File Offset: 0x000DA218
		// (set) Token: 0x06003D23 RID: 15651 RVA: 0x000DC020 File Offset: 0x000DA220
		public string RefusedSet
		{
			[SecurityPermission(SecurityAction.Demand, ControlEvidence = true, ControlPolicy = true)]
			get
			{
				return this._refused;
			}
			set
			{
				this._refused = value;
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.SecurityException" /> class with default properties.</summary>
		// Token: 0x06003D24 RID: 15652 RVA: 0x000DC029 File Offset: 0x000DA229
		public SecurityException()
			: this(Locale.GetText("A security error has been detected."))
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.SecurityException" /> class with a specified error message.</summary>
		/// <param name="message">The error message that explains the reason for the exception. </param>
		// Token: 0x06003D25 RID: 15653 RVA: 0x000DC03B File Offset: 0x000DA23B
		public SecurityException(string message)
			: base(message)
		{
			base.HResult = -2146233078;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.SecurityException" /> class with serialized data.</summary>
		/// <param name="info">The object that holds the serialized object data. </param>
		/// <param name="context">The contextual information about the source or destination. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="info " />is null.</exception>
		// Token: 0x06003D26 RID: 15654 RVA: 0x000DC050 File Offset: 0x000DA250
		protected SecurityException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			base.HResult = -2146233078;
			SerializationInfoEnumerator enumerator = info.GetEnumerator();
			while (enumerator.MoveNext())
			{
				if (enumerator.Name == "PermissionState")
				{
					this.permissionState = (string)enumerator.Value;
					return;
				}
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.SecurityException" /> class with a specified error message and a reference to the inner exception that is the cause of this exception.</summary>
		/// <param name="message">The error message that explains the reason for the exception. </param>
		/// <param name="inner">The exception that is the cause of the current exception. If the <paramref name="inner" /> parameter is not null, the current exception is raised in a catch block that handles the inner exception. </param>
		// Token: 0x06003D27 RID: 15655 RVA: 0x000DC0A5 File Offset: 0x000DA2A5
		public SecurityException(string message, Exception inner)
			: base(message, inner)
		{
			base.HResult = -2146233078;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.SecurityException" /> class with a specified error message and the permission type that caused the exception to be thrown.</summary>
		/// <param name="message">The error message that explains the reason for the exception. </param>
		/// <param name="type">The type of the permission that caused the exception to be thrown. </param>
		// Token: 0x06003D28 RID: 15656 RVA: 0x000DC0BA File Offset: 0x000DA2BA
		public SecurityException(string message, Type type)
			: base(message)
		{
			base.HResult = -2146233078;
			this.permissionType = type;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.SecurityException" /> class with a specified error message, the permission type that caused the exception to be thrown, and the permission state.</summary>
		/// <param name="message">The error message that explains the reason for the exception. </param>
		/// <param name="type">The type of the permission that caused the exception to be thrown. </param>
		/// <param name="state">The state of the permission that caused the exception to be thrown. </param>
		// Token: 0x06003D29 RID: 15657 RVA: 0x000DC0D5 File Offset: 0x000DA2D5
		public SecurityException(string message, Type type, string state)
			: base(message)
		{
			base.HResult = -2146233078;
			this.permissionType = type;
			this.permissionState = state;
		}

		// Token: 0x06003D2A RID: 15658 RVA: 0x000DC0F7 File Offset: 0x000DA2F7
		internal SecurityException(string message, PermissionSet granted, PermissionSet refused)
			: base(message)
		{
			base.HResult = -2146233078;
			this._granted = granted.ToString();
			this._refused = refused.ToString();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.SecurityException" /> class for an exception caused by a Deny on the stack.  </summary>
		/// <param name="message">The error message that explains the reason for the exception.</param>
		/// <param name="deny">The denied permission or permission set.</param>
		/// <param name="permitOnly">The permit-only permission or permission set.</param>
		/// <param name="method">A <see cref="T:System.Reflection.MethodInfo" /> that identifies the method that encountered the exception.</param>
		/// <param name="demanded">The demanded permission, permission set, or permission set collection.</param>
		/// <param name="permThatFailed">An <see cref="T:System.Security.IPermission" /> that identifies the permission that failed.</param>
		// Token: 0x06003D2B RID: 15659 RVA: 0x000DC123 File Offset: 0x000DA323
		public SecurityException(string message, object deny, object permitOnly, MethodInfo method, object demanded, IPermission permThatFailed)
			: base(message)
		{
			base.HResult = -2146233078;
			this._denyset = deny;
			this._permitset = permitOnly;
			this._method = method;
			this._demanded = demanded;
			this._firstperm = permThatFailed;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.SecurityException" /> class for an exception caused by an insufficient grant set. </summary>
		/// <param name="message">The error message that explains the reason for the exception.</param>
		/// <param name="assemblyName">An <see cref="T:System.Reflection.AssemblyName" /> that specifies the name of the assembly that caused the exception.</param>
		/// <param name="grant">A <see cref="T:System.Security.PermissionSet" /> that represents the permissions granted the assembly.</param>
		/// <param name="refused">A <see cref="T:System.Security.PermissionSet" /> that represents the refused permission or permission set.</param>
		/// <param name="method">A <see cref="T:System.Reflection.MethodInfo" /> that represents the method that encountered the exception.</param>
		/// <param name="action">One of the <see cref="T:System.Security.Permissions.SecurityAction" /> values.</param>
		/// <param name="demanded">The demanded permission, permission set, or permission set collection.</param>
		/// <param name="permThatFailed">An <see cref="T:System.Security.IPermission" /> that represents the permission that failed.</param>
		/// <param name="evidence">The <see cref="T:System.Security.Policy.Evidence" /> for the assembly that caused the exception.</param>
		// Token: 0x06003D2C RID: 15660 RVA: 0x000DC160 File Offset: 0x000DA360
		public SecurityException(string message, AssemblyName assemblyName, PermissionSet grant, PermissionSet refused, MethodInfo method, SecurityAction action, object demanded, IPermission permThatFailed, Evidence evidence)
			: base(message)
		{
			base.HResult = -2146233078;
			this._assembly = assemblyName;
			this._granted = ((grant == null) ? string.Empty : grant.ToString());
			this._refused = ((refused == null) ? string.Empty : refused.ToString());
			this._method = method;
			this._action = action;
			this._demanded = demanded;
			this._firstperm = permThatFailed;
			if (this._firstperm != null)
			{
				this.permissionType = this._firstperm.GetType();
			}
			this._evidence = evidence;
		}

		/// <summary>Sets the <see cref="T:System.Runtime.Serialization.SerializationInfo" /> with information about the <see cref="T:System.Security.SecurityException" />.</summary>
		/// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> that holds the serialized object data about the exception being thrown. </param>
		/// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext" /> that contains contextual information about the source or destination. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="info" /> parameter is null. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Read="*AllFiles*" PathDiscovery="*AllFiles*" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence, ControlPolicy" />
		/// </PermissionSet>
		// Token: 0x06003D2D RID: 15661 RVA: 0x000DC1F8 File Offset: 0x000DA3F8
		[SecurityCritical]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			try
			{
				info.AddValue("PermissionState", this.permissionState);
			}
			catch (SecurityException)
			{
			}
		}

		/// <summary>Returns a representation of the current <see cref="T:System.Security.SecurityException" />.</summary>
		/// <returns>A string representation of the current <see cref="T:System.Security.SecurityException" />.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Read="*AllFiles*" PathDiscovery="*AllFiles*" />
		///   <IPermission class="System.Security.Permissions.ReflectionPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="MemberAccess" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence, ControlPolicy" />
		/// </PermissionSet>
		// Token: 0x06003D2E RID: 15662 RVA: 0x000DC234 File Offset: 0x000DA434
		[SecuritySafeCritical]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder(base.ToString());
			try
			{
				if (this.permissionType != null)
				{
					stringBuilder.AppendFormat("{0}Type: {1}", Environment.NewLine, this.PermissionType);
				}
				if (this._method != null)
				{
					string text = this._method.ToString();
					int num = text.IndexOf(" ") + 1;
					stringBuilder.AppendFormat("{0}Method: {1} {2}.{3}", new object[]
					{
						Environment.NewLine,
						this._method.ReturnType.Name,
						this._method.ReflectedType,
						text.Substring(num)
					});
				}
				if (this.permissionState != null)
				{
					stringBuilder.AppendFormat("{0}State: {1}", Environment.NewLine, this.PermissionState);
				}
				if (this._granted != null && this._granted.Length > 0)
				{
					stringBuilder.AppendFormat("{0}Granted: {1}", Environment.NewLine, this.GrantedSet);
				}
				if (this._refused != null && this._refused.Length > 0)
				{
					stringBuilder.AppendFormat("{0}Refused: {1}", Environment.NewLine, this.RefusedSet);
				}
				if (this._demanded != null)
				{
					stringBuilder.AppendFormat("{0}Demanded: {1}", Environment.NewLine, this.Demanded);
				}
				if (this._firstperm != null)
				{
					stringBuilder.AppendFormat("{0}Failed Permission: {1}", Environment.NewLine, this.FirstPermissionThatFailed);
				}
				if (this._evidence != null)
				{
					stringBuilder.AppendFormat("{0}Evidences:", Environment.NewLine);
					foreach (object obj in this._evidence)
					{
						if (!(obj is Hash))
						{
							stringBuilder.AppendFormat("{0}\t{1}", Environment.NewLine, obj);
						}
					}
				}
			}
			catch (SecurityException)
			{
			}
			return stringBuilder.ToString();
		}

		// Token: 0x04001F71 RID: 8049
		private string permissionState;

		// Token: 0x04001F72 RID: 8050
		private Type permissionType;

		// Token: 0x04001F73 RID: 8051
		private string _granted;

		// Token: 0x04001F74 RID: 8052
		private string _refused;

		// Token: 0x04001F75 RID: 8053
		private object _demanded;

		// Token: 0x04001F76 RID: 8054
		private IPermission _firstperm;

		// Token: 0x04001F77 RID: 8055
		private MethodInfo _method;

		// Token: 0x04001F78 RID: 8056
		private Evidence _evidence;

		// Token: 0x04001F79 RID: 8057
		private SecurityAction _action;

		// Token: 0x04001F7A RID: 8058
		private object _denyset;

		// Token: 0x04001F7B RID: 8059
		private object _permitset;

		// Token: 0x04001F7C RID: 8060
		private AssemblyName _assembly;

		// Token: 0x04001F7D RID: 8061
		private string _url;

		// Token: 0x04001F7E RID: 8062
		private SecurityZone _zone;
	}
}
