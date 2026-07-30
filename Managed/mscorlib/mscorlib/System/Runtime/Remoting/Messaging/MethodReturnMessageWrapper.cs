using System;
using System.Collections;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Runtime.Remoting.Messaging
{
	/// <summary>Implements the <see cref="T:System.Runtime.Remoting.Messaging.IMethodReturnMessage" /> interface to create a message that acts as a response to a method call on a remote object.</summary>
	// Token: 0x0200081C RID: 2076
	[ComVisible(true)]
	public class MethodReturnMessageWrapper : InternalMessageWrapper, IMethodReturnMessage, IMethodMessage, IMessage
	{
		/// <summary>Wraps an <see cref="T:System.Runtime.Remoting.Messaging.IMethodReturnMessage" /> to create a <see cref="T:System.Runtime.Remoting.Messaging.MethodReturnMessageWrapper" />. </summary>
		/// <param name="msg">A message that acts as an outgoing method call on a remote object.</param>
		// Token: 0x060052FD RID: 21245 RVA: 0x00124904 File Offset: 0x00122B04
		public MethodReturnMessageWrapper(IMethodReturnMessage msg)
			: base(msg)
		{
			if (msg.Exception != null)
			{
				this._exception = msg.Exception;
				this._args = new object[0];
				return;
			}
			this._args = msg.Args;
			this._return = msg.ReturnValue;
			if (msg.MethodBase != null)
			{
				this._outArgInfo = new ArgInfo(msg.MethodBase, ArgInfoType.Out);
			}
		}

		/// <summary>Gets the number of arguments passed to the method. </summary>
		/// <returns>A <see cref="T:System.Int32" /> that represents the number of arguments passed to a method.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x17000E61 RID: 3681
		// (get) Token: 0x060052FE RID: 21246 RVA: 0x00124971 File Offset: 0x00122B71
		public virtual int ArgCount
		{
			[SecurityCritical]
			get
			{
				return this._args.Length;
			}
		}

		/// <summary>Gets an array of arguments passed to the method. </summary>
		/// <returns>An array of type <see cref="T:System.Object" /> that represents the arguments passed to a method.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x17000E62 RID: 3682
		// (get) Token: 0x060052FF RID: 21247 RVA: 0x0012497B File Offset: 0x00122B7B
		// (set) Token: 0x06005300 RID: 21248 RVA: 0x00124983 File Offset: 0x00122B83
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

		/// <summary>Gets the exception thrown during the method call, or null if the method did not throw an exception. </summary>
		/// <returns>The <see cref="T:System.Exception" /> thrown during the method call, or null if the method did not throw an exception.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x17000E63 RID: 3683
		// (get) Token: 0x06005301 RID: 21249 RVA: 0x0012498C File Offset: 0x00122B8C
		// (set) Token: 0x06005302 RID: 21250 RVA: 0x00124994 File Offset: 0x00122B94
		public virtual Exception Exception
		{
			[SecurityCritical]
			get
			{
				return this._exception;
			}
			set
			{
				this._exception = value;
			}
		}

		/// <summary>Gets a flag that indicates whether the method can accept a variable number of arguments. </summary>
		/// <returns>true if the method can accept a variable number of arguments; otherwise, false.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x17000E64 RID: 3684
		// (get) Token: 0x06005303 RID: 21251 RVA: 0x0012499D File Offset: 0x00122B9D
		public virtual bool HasVarArgs
		{
			[SecurityCritical]
			get
			{
				return ((IMethodReturnMessage)this.WrappedMessage).HasVarArgs;
			}
		}

		/// <summary>Gets the <see cref="T:System.Runtime.Remoting.Messaging.LogicalCallContext" /> for the current method call. </summary>
		/// <returns>The <see cref="T:System.Runtime.Remoting.Messaging.LogicalCallContext" /> for the current method call.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x17000E65 RID: 3685
		// (get) Token: 0x06005304 RID: 21252 RVA: 0x001249AF File Offset: 0x00122BAF
		public virtual LogicalCallContext LogicalCallContext
		{
			[SecurityCritical]
			get
			{
				return ((IMethodReturnMessage)this.WrappedMessage).LogicalCallContext;
			}
		}

		/// <summary>Gets the <see cref="T:System.Reflection.MethodBase" /> of the called method. </summary>
		/// <returns>The <see cref="T:System.Reflection.MethodBase" /> of the called method.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x17000E66 RID: 3686
		// (get) Token: 0x06005305 RID: 21253 RVA: 0x001249C1 File Offset: 0x00122BC1
		public virtual MethodBase MethodBase
		{
			[SecurityCritical]
			get
			{
				return ((IMethodReturnMessage)this.WrappedMessage).MethodBase;
			}
		}

		/// <summary>Gets the name of the invoked method. </summary>
		/// <returns>A <see cref="T:System.String" /> that contains the name of the invoked method.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x17000E67 RID: 3687
		// (get) Token: 0x06005306 RID: 21254 RVA: 0x001249D3 File Offset: 0x00122BD3
		public virtual string MethodName
		{
			[SecurityCritical]
			get
			{
				return ((IMethodReturnMessage)this.WrappedMessage).MethodName;
			}
		}

		/// <summary>Gets an object that contains the method signature. </summary>
		/// <returns>A <see cref="T:System.Object" /> that contains the method signature.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x17000E68 RID: 3688
		// (get) Token: 0x06005307 RID: 21255 RVA: 0x001249E5 File Offset: 0x00122BE5
		public virtual object MethodSignature
		{
			[SecurityCritical]
			get
			{
				return ((IMethodReturnMessage)this.WrappedMessage).MethodSignature;
			}
		}

		/// <summary>Gets the number of arguments in the method call that are marked as ref parameters or out parameters. </summary>
		/// <returns>A <see cref="T:System.Int32" /> that represents the number of arguments in the method call that are marked as ref parameters or out parameters.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x17000E69 RID: 3689
		// (get) Token: 0x06005308 RID: 21256 RVA: 0x001249F7 File Offset: 0x00122BF7
		public virtual int OutArgCount
		{
			[SecurityCritical]
			get
			{
				if (this._outArgInfo == null)
				{
					return 0;
				}
				return this._outArgInfo.GetInOutArgCount();
			}
		}

		/// <summary>Gets an array of arguments in the method call that are marked as ref parameters or out parameters. </summary>
		/// <returns>An array of type <see cref="T:System.Object" /> that represents the arguments in the method call that are marked as ref parameters or out parameters.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x17000E6A RID: 3690
		// (get) Token: 0x06005309 RID: 21257 RVA: 0x00124A0E File Offset: 0x00122C0E
		public virtual object[] OutArgs
		{
			[SecurityCritical]
			get
			{
				if (this._outArgInfo == null)
				{
					return this._args;
				}
				return this._outArgInfo.GetInOutArgs(this._args);
			}
		}

		/// <summary>An <see cref="T:System.Collections.IDictionary" /> interface that represents a collection of the remoting message's properties. </summary>
		/// <returns>An <see cref="T:System.Collections.IDictionary" /> interface that represents a collection of the remoting message's properties.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x17000E6B RID: 3691
		// (get) Token: 0x0600530A RID: 21258 RVA: 0x00124A30 File Offset: 0x00122C30
		public virtual IDictionary Properties
		{
			[SecurityCritical]
			get
			{
				if (this._properties == null)
				{
					this._properties = new MethodReturnMessageWrapper.DictionaryWrapper(this, this.WrappedMessage.Properties);
				}
				return this._properties;
			}
		}

		/// <summary>Gets the return value of the method call. </summary>
		/// <returns>A <see cref="T:System.Object" /> that represents the return value of the method call.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x17000E6C RID: 3692
		// (get) Token: 0x0600530B RID: 21259 RVA: 0x00124A57 File Offset: 0x00122C57
		// (set) Token: 0x0600530C RID: 21260 RVA: 0x00124A5F File Offset: 0x00122C5F
		public virtual object ReturnValue
		{
			[SecurityCritical]
			get
			{
				return this._return;
			}
			set
			{
				this._return = value;
			}
		}

		/// <summary>Gets the full type name of the remote object on which the method call is being made. </summary>
		/// <returns>A <see cref="T:System.String" /> that contains the full type name of the remote object on which the method call is being made.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x17000E6D RID: 3693
		// (get) Token: 0x0600530D RID: 21261 RVA: 0x00124A68 File Offset: 0x00122C68
		public virtual string TypeName
		{
			[SecurityCritical]
			get
			{
				return ((IMethodReturnMessage)this.WrappedMessage).TypeName;
			}
		}

		/// <summary>Gets the Uniform Resource Identifier (URI) of the remote object on which the method call is being made. </summary>
		/// <returns>The URI of a remote object.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x17000E6E RID: 3694
		// (get) Token: 0x0600530E RID: 21262 RVA: 0x00124A7A File Offset: 0x00122C7A
		// (set) Token: 0x0600530F RID: 21263 RVA: 0x00124A8C File Offset: 0x00122C8C
		public string Uri
		{
			[SecurityCritical]
			get
			{
				return ((IMethodReturnMessage)this.WrappedMessage).Uri;
			}
			set
			{
				this.Properties["__Uri"] = value;
			}
		}

		/// <summary>Gets a method argument, as an object, at a specified index. </summary>
		/// <returns>The method argument as an object.</returns>
		/// <param name="argNum">The index of the requested argument.</param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x06005310 RID: 21264 RVA: 0x00124A9F File Offset: 0x00122C9F
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
		// Token: 0x06005311 RID: 21265 RVA: 0x00124AA9 File Offset: 0x00122CA9
		[SecurityCritical]
		public virtual string GetArgName(int index)
		{
			return ((IMethodReturnMessage)this.WrappedMessage).GetArgName(index);
		}

		/// <summary>Returns the specified argument marked as a ref parameter or an out parameter. </summary>
		/// <returns>The specified argument marked as a ref parameter or an out parameter.</returns>
		/// <param name="argNum">The index of the requested argument.</param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x06005312 RID: 21266 RVA: 0x00124ABC File Offset: 0x00122CBC
		[SecurityCritical]
		public virtual object GetOutArg(int argNum)
		{
			return this._args[this._outArgInfo.GetInOutArgIndex(argNum)];
		}

		/// <summary>Returns the name of the specified argument marked as a ref parameter or an out parameter. </summary>
		/// <returns>The argument name, or null if the current method is not implemented.</returns>
		/// <param name="index">The index of the requested argument.</param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x06005313 RID: 21267 RVA: 0x00124AD1 File Offset: 0x00122CD1
		[SecurityCritical]
		public virtual string GetOutArgName(int index)
		{
			return this._outArgInfo.GetInOutArgName(index);
		}

		// Token: 0x04002B34 RID: 11060
		private object[] _args;

		// Token: 0x04002B35 RID: 11061
		private ArgInfo _outArgInfo;

		// Token: 0x04002B36 RID: 11062
		private MethodReturnMessageWrapper.DictionaryWrapper _properties;

		// Token: 0x04002B37 RID: 11063
		private Exception _exception;

		// Token: 0x04002B38 RID: 11064
		private object _return;

		// Token: 0x0200081D RID: 2077
		private class DictionaryWrapper : MethodReturnDictionary
		{
			// Token: 0x06005314 RID: 21268 RVA: 0x00124ADF File Offset: 0x00122CDF
			public DictionaryWrapper(IMethodReturnMessage message, IDictionary wrappedDictionary)
				: base(message)
			{
				this._wrappedDictionary = wrappedDictionary;
				base.MethodKeys = MethodReturnMessageWrapper.DictionaryWrapper._keys;
			}

			// Token: 0x06005315 RID: 21269 RVA: 0x00124AFA File Offset: 0x00122CFA
			protected override IDictionary AllocInternalProperties()
			{
				return this._wrappedDictionary;
			}

			// Token: 0x06005316 RID: 21270 RVA: 0x00124B04 File Offset: 0x00122D04
			protected override void SetMethodProperty(string key, object value)
			{
				if (key == "__Args")
				{
					((MethodReturnMessageWrapper)this._message)._args = (object[])value;
					return;
				}
				if (key == "__Return")
				{
					((MethodReturnMessageWrapper)this._message)._return = value;
					return;
				}
				base.SetMethodProperty(key, value);
			}

			// Token: 0x06005317 RID: 21271 RVA: 0x00124B5C File Offset: 0x00122D5C
			protected override object GetMethodProperty(string key)
			{
				if (key == "__Args")
				{
					return ((MethodReturnMessageWrapper)this._message)._args;
				}
				if (key == "__Return")
				{
					return ((MethodReturnMessageWrapper)this._message)._return;
				}
				return base.GetMethodProperty(key);
			}

			// Token: 0x04002B39 RID: 11065
			private IDictionary _wrappedDictionary;

			// Token: 0x04002B3A RID: 11066
			private static string[] _keys = new string[] { "__Args", "__Return" };
		}
	}
}
