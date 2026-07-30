using System;
using System.Collections;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Runtime.Remoting.Messaging
{
	/// <summary>Implements the <see cref="T:System.Runtime.Remoting.Messaging.IMethodCallMessage" /> interface to create a request message that acts as a method call on a remote object.</summary>
	// Token: 0x02000816 RID: 2070
	[ComVisible(true)]
	public class MethodCallMessageWrapper : InternalMessageWrapper, IMethodCallMessage, IMethodMessage, IMessage
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Remoting.Messaging.MethodCallMessageWrapper" /> class by wrapping an <see cref="T:System.Runtime.Remoting.Messaging.IMethodCallMessage" /> interface.</summary>
		/// <param name="msg">A message that acts as an outgoing method call on a remote object.</param>
		// Token: 0x0600529F RID: 21151 RVA: 0x00123631 File Offset: 0x00121831
		public MethodCallMessageWrapper(IMethodCallMessage msg)
			: base(msg)
		{
			this._args = ((IMethodCallMessage)this.WrappedMessage).Args;
			this._inArgInfo = new ArgInfo(msg.MethodBase, ArgInfoType.In);
		}

		/// <summary>Gets the number of arguments passed to the method. </summary>
		/// <returns>A <see cref="T:System.Int32" /> that represents the number of arguments passed to a method.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x17000E37 RID: 3639
		// (get) Token: 0x060052A0 RID: 21152 RVA: 0x00123662 File Offset: 0x00121862
		public virtual int ArgCount
		{
			[SecurityCritical]
			get
			{
				return ((IMethodCallMessage)this.WrappedMessage).ArgCount;
			}
		}

		/// <summary>Gets an array of arguments passed to the method. </summary>
		/// <returns>An array of type <see cref="T:System.Object" /> that represents the arguments passed to a method.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x17000E38 RID: 3640
		// (get) Token: 0x060052A1 RID: 21153 RVA: 0x00123674 File Offset: 0x00121874
		// (set) Token: 0x060052A2 RID: 21154 RVA: 0x0012367C File Offset: 0x0012187C
		public virtual object[] Args
		{
			[SecurityCritical]
			get
			{
				return this._args;
			}
			set
			{
				this._args = value;
			}
		}

		/// <summary>Gets a value indicating whether the method can accept a variable number of arguments. </summary>
		/// <returns>true if the method can accept a variable number of arguments; otherwise, false.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x17000E39 RID: 3641
		// (get) Token: 0x060052A3 RID: 21155 RVA: 0x00123685 File Offset: 0x00121885
		public virtual bool HasVarArgs
		{
			[SecurityCritical]
			get
			{
				return ((IMethodCallMessage)this.WrappedMessage).HasVarArgs;
			}
		}

		/// <summary>Gets the number of arguments in the method call that are not marked as out parameters. </summary>
		/// <returns>A <see cref="T:System.Int32" /> that represents the number of arguments in the method call that are not marked as out parameters.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x17000E3A RID: 3642
		// (get) Token: 0x060052A4 RID: 21156 RVA: 0x00123697 File Offset: 0x00121897
		public virtual int InArgCount
		{
			[SecurityCritical]
			get
			{
				return this._inArgInfo.GetInOutArgCount();
			}
		}

		/// <summary>Gets an array of arguments in the method call that are not marked as out parameters. </summary>
		/// <returns>An array of type <see cref="T:System.Object" /> that represents arguments in the method call that are not marked as out parameters.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x17000E3B RID: 3643
		// (get) Token: 0x060052A5 RID: 21157 RVA: 0x001236A4 File Offset: 0x001218A4
		public virtual object[] InArgs
		{
			[SecurityCritical]
			get
			{
				return this._inArgInfo.GetInOutArgs(this._args);
			}
		}

		/// <summary>Gets the <see cref="T:System.Runtime.Remoting.Messaging.LogicalCallContext" /> for the current method call. </summary>
		/// <returns>The <see cref="T:System.Runtime.Remoting.Messaging.LogicalCallContext" /> for the current method call.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x17000E3C RID: 3644
		// (get) Token: 0x060052A6 RID: 21158 RVA: 0x001236B7 File Offset: 0x001218B7
		public virtual LogicalCallContext LogicalCallContext
		{
			[SecurityCritical]
			get
			{
				return ((IMethodCallMessage)this.WrappedMessage).LogicalCallContext;
			}
		}

		/// <summary>Gets the <see cref="T:System.Reflection.MethodBase" /> of the called method. </summary>
		/// <returns>The <see cref="T:System.Reflection.MethodBase" /> of the called method.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x17000E3D RID: 3645
		// (get) Token: 0x060052A7 RID: 21159 RVA: 0x001236C9 File Offset: 0x001218C9
		public virtual MethodBase MethodBase
		{
			[SecurityCritical]
			get
			{
				return ((IMethodCallMessage)this.WrappedMessage).MethodBase;
			}
		}

		/// <summary>Gets the name of the invoked method. </summary>
		/// <returns>A <see cref="T:System.String" /> that contains the name of the invoked method.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x17000E3E RID: 3646
		// (get) Token: 0x060052A8 RID: 21160 RVA: 0x001236DB File Offset: 0x001218DB
		public virtual string MethodName
		{
			[SecurityCritical]
			get
			{
				return ((IMethodCallMessage)this.WrappedMessage).MethodName;
			}
		}

		/// <summary>Gets an object that contains the method signature. </summary>
		/// <returns>A <see cref="T:System.Object" /> that contains the method signature.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x17000E3F RID: 3647
		// (get) Token: 0x060052A9 RID: 21161 RVA: 0x001236ED File Offset: 0x001218ED
		public virtual object MethodSignature
		{
			[SecurityCritical]
			get
			{
				return ((IMethodCallMessage)this.WrappedMessage).MethodSignature;
			}
		}

		/// <summary>An <see cref="T:System.Collections.IDictionary" /> that represents a collection of the remoting message's properties. </summary>
		/// <returns>An <see cref="T:System.Collections.IDictionary" /> interface that represents a collection of the remoting message's properties.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x17000E40 RID: 3648
		// (get) Token: 0x060052AA RID: 21162 RVA: 0x001236FF File Offset: 0x001218FF
		public virtual IDictionary Properties
		{
			[SecurityCritical]
			get
			{
				if (this._properties == null)
				{
					this._properties = new MethodCallMessageWrapper.DictionaryWrapper(this, this.WrappedMessage.Properties);
				}
				return this._properties;
			}
		}

		/// <summary>Gets the full type name of the remote object on which the method call is being made. </summary>
		/// <returns>A <see cref="T:System.String" /> that contains the full type name of the remote object on which the method call is being made.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x17000E41 RID: 3649
		// (get) Token: 0x060052AB RID: 21163 RVA: 0x00123726 File Offset: 0x00121926
		public virtual string TypeName
		{
			[SecurityCritical]
			get
			{
				return ((IMethodCallMessage)this.WrappedMessage).TypeName;
			}
		}

		/// <summary>Gets the Uniform Resource Identifier (URI) of the remote object on which the method call is being made. </summary>
		/// <returns>The URI of a remote object.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x17000E42 RID: 3650
		// (get) Token: 0x060052AC RID: 21164 RVA: 0x00123738 File Offset: 0x00121938
		// (set) Token: 0x060052AD RID: 21165 RVA: 0x0012374C File Offset: 0x0012194C
		public virtual string Uri
		{
			[SecurityCritical]
			get
			{
				return ((IMethodCallMessage)this.WrappedMessage).Uri;
			}
			set
			{
				IInternalMessage internalMessage = this.WrappedMessage as IInternalMessage;
				if (internalMessage != null)
				{
					internalMessage.Uri = value;
					return;
				}
				this.Properties["__Uri"] = value;
			}
		}

		/// <summary>Gets a method argument, as an object, at a specified index. </summary>
		/// <returns>The method argument as an object.</returns>
		/// <param name="argNum">The index of the requested argument.</param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x060052AE RID: 21166 RVA: 0x00123781 File Offset: 0x00121981
		[SecurityCritical]
		public virtual object GetArg(int argNum)
		{
			return this._args[argNum];
		}

		/// <summary>Gets the name of a method argument at a specified index. </summary>
		/// <returns>The name of the method argument.</returns>
		/// <param name="index">The index of the requested argument.</param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x060052AF RID: 21167 RVA: 0x0012378B File Offset: 0x0012198B
		[SecurityCritical]
		public virtual string GetArgName(int index)
		{
			return ((IMethodCallMessage)this.WrappedMessage).GetArgName(index);
		}

		/// <summary>Gets a method argument at a specified index that is not marked as an out parameter. </summary>
		/// <returns>The method argument that is not marked as an out parameter.</returns>
		/// <param name="argNum">The index of the requested argument.</param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x060052B0 RID: 21168 RVA: 0x0012379E File Offset: 0x0012199E
		[SecurityCritical]
		public virtual object GetInArg(int argNum)
		{
			return this._args[this._inArgInfo.GetInOutArgIndex(argNum)];
		}

		/// <summary>Gets the name of a method argument at a specified index that is not marked as an out parameter. </summary>
		/// <returns>The name of the method argument that is not marked as an out parameter.</returns>
		/// <param name="index">The index of the requested argument.</param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x060052B1 RID: 21169 RVA: 0x001237B3 File Offset: 0x001219B3
		[SecurityCritical]
		public virtual string GetInArgName(int index)
		{
			return this._inArgInfo.GetInOutArgName(index);
		}

		// Token: 0x04002B17 RID: 11031
		private object[] _args;

		// Token: 0x04002B18 RID: 11032
		private ArgInfo _inArgInfo;

		// Token: 0x04002B19 RID: 11033
		private MethodCallMessageWrapper.DictionaryWrapper _properties;

		// Token: 0x02000817 RID: 2071
		private class DictionaryWrapper : MCMDictionary
		{
			// Token: 0x060052B2 RID: 21170 RVA: 0x001237C1 File Offset: 0x001219C1
			public DictionaryWrapper(IMethodMessage message, IDictionary wrappedDictionary)
				: base(message)
			{
				this._wrappedDictionary = wrappedDictionary;
				base.MethodKeys = MethodCallMessageWrapper.DictionaryWrapper._keys;
			}

			// Token: 0x060052B3 RID: 21171 RVA: 0x001237DC File Offset: 0x001219DC
			protected override IDictionary AllocInternalProperties()
			{
				return this._wrappedDictionary;
			}

			// Token: 0x060052B4 RID: 21172 RVA: 0x001237E4 File Offset: 0x001219E4
			protected override void SetMethodProperty(string key, object value)
			{
				if (key == "__Args")
				{
					((MethodCallMessageWrapper)this._message)._args = (object[])value;
					return;
				}
				base.SetMethodProperty(key, value);
			}

			// Token: 0x060052B5 RID: 21173 RVA: 0x00123812 File Offset: 0x00121A12
			protected override object GetMethodProperty(string key)
			{
				if (key == "__Args")
				{
					return ((MethodCallMessageWrapper)this._message)._args;
				}
				return base.GetMethodProperty(key);
			}

			// Token: 0x04002B1A RID: 11034
			private IDictionary _wrappedDictionary;

			// Token: 0x04002B1B RID: 11035
			private static string[] _keys = new string[] { "__Args" };
		}
	}
}
