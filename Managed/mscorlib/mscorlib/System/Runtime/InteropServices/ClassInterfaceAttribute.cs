using System;

namespace System.Runtime.InteropServices
{
	/// <summary>Indicates the type of class interface to be generated for a class exposed to COM, if an interface is generated at all.</summary>
	// Token: 0x020008AB RID: 2219
	[ComVisible(true)]
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class, Inherited = false)]
	public sealed class ClassInterfaceAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.InteropServices.ClassInterfaceAttribute" /> class with the specified <see cref="T:System.Runtime.InteropServices.ClassInterfaceType" /> enumeration member.</summary>
		/// <param name="classInterfaceType">One of the <see cref="T:System.Runtime.InteropServices.ClassInterfaceType" /> values that describes the type of interface that is generated for a class. </param>
		// Token: 0x060054E2 RID: 21730 RVA: 0x00128398 File Offset: 0x00126598
		public ClassInterfaceAttribute(ClassInterfaceType classInterfaceType)
		{
			this._val = classInterfaceType;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.InteropServices.ClassInterfaceAttribute" /> class with the specified <see cref="T:System.Runtime.InteropServices.ClassInterfaceType" /> enumeration value.</summary>
		/// <param name="classInterfaceType">Describes the type of interface that is generated for a class. </param>
		// Token: 0x060054E3 RID: 21731 RVA: 0x00128398 File Offset: 0x00126598
		public ClassInterfaceAttribute(short classInterfaceType)
		{
			this._val = (ClassInterfaceType)classInterfaceType;
		}

		/// <summary>Gets the <see cref="T:System.Runtime.InteropServices.ClassInterfaceType" /> value that describes which type of interface should be generated for the class.</summary>
		/// <returns>The <see cref="T:System.Runtime.InteropServices.ClassInterfaceType" /> value that describes which type of interface should be generated for the class.</returns>
		// Token: 0x17000ED3 RID: 3795
		// (get) Token: 0x060054E4 RID: 21732 RVA: 0x001283A7 File Offset: 0x001265A7
		public ClassInterfaceType Value
		{
			get
			{
				return this._val;
			}
		}

		// Token: 0x04002C01 RID: 11265
		internal ClassInterfaceType _val;
	}
}
