using System;

namespace System.Runtime.InteropServices.WindowsRuntime
{
	/// <summary>Specifies the default interface of a managed Windows Runtime class.</summary>
	// Token: 0x02000958 RID: 2392
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface, AllowMultiple = false, Inherited = false)]
	public sealed class DefaultInterfaceAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.InteropServices.WindowsRuntime.DefaultInterfaceAttribute" /> class. </summary>
		/// <param name="defaultInterface">The interface type that is specified as the default interface for the class the attribute is applied to. </param>
		// Token: 0x06005927 RID: 22823 RVA: 0x0012AB4F File Offset: 0x00128D4F
		public DefaultInterfaceAttribute(Type defaultInterface)
		{
			this.m_defaultInterface = defaultInterface;
		}

		/// <summary>Gets the type of the default interface. </summary>
		/// <returns>The type of the default interface. </returns>
		// Token: 0x17000FAC RID: 4012
		// (get) Token: 0x06005928 RID: 22824 RVA: 0x0012AB5E File Offset: 0x00128D5E
		public Type DefaultInterface
		{
			get
			{
				return this.m_defaultInterface;
			}
		}

		// Token: 0x04002E03 RID: 11779
		private Type m_defaultInterface;
	}
}
