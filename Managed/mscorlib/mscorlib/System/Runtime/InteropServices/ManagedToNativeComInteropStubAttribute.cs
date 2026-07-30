using System;

namespace System.Runtime.InteropServices
{
	/// <summary>Provides support for user customization of interop stubs in managed-to-COM interop scenarios.</summary>
	// Token: 0x020008D4 RID: 2260
	[AttributeUsage(AttributeTargets.Method, Inherited = false, AllowMultiple = false)]
	[ComVisible(false)]
	public sealed class ManagedToNativeComInteropStubAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.InteropServices.ManagedToNativeComInteropStubAttribute" /> class with the specified class type and method name.</summary>
		/// <param name="classType">The class that contains the required stub method. </param>
		/// <param name="methodName">The name of the stub method.</param>
		/// <exception cref="T:System.ArgumentException">The stub method is not in the same assembly as the interface that contains the managed interop method.-or-<paramref name="classType" /> is a generic type.-or-<paramref name="classType" /> is an interface. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="methodName" /> cannot be found.-or-The method is not static or non-generic.-or-The method's parameter list does not match the expected parameter list for the stub.</exception>
		/// <exception cref="T:System.MethodAccessException">The interface that contains the managed interop method has no access to the stub method, because the stub method has private or protected accessibility, or because of a security issue.</exception>
		// Token: 0x0600553F RID: 21823 RVA: 0x00128A52 File Offset: 0x00126C52
		public ManagedToNativeComInteropStubAttribute(Type classType, string methodName)
		{
			this._classType = classType;
			this._methodName = methodName;
		}

		/// <summary>Gets the class that contains the required stub method.</summary>
		/// <returns>The class that contains the customized interop stub.</returns>
		// Token: 0x17000EF2 RID: 3826
		// (get) Token: 0x06005540 RID: 21824 RVA: 0x00128A68 File Offset: 0x00126C68
		public Type ClassType
		{
			get
			{
				return this._classType;
			}
		}

		/// <summary>Gets the name of the stub method.</summary>
		/// <returns>The name of a customized interop stub.</returns>
		// Token: 0x17000EF3 RID: 3827
		// (get) Token: 0x06005541 RID: 21825 RVA: 0x00128A70 File Offset: 0x00126C70
		public string MethodName
		{
			get
			{
				return this._methodName;
			}
		}

		// Token: 0x04002CB8 RID: 11448
		internal Type _classType;

		// Token: 0x04002CB9 RID: 11449
		internal string _methodName;
	}
}
