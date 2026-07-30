using System;
using System.Reflection;
using System.Runtime.InteropServices;

namespace System.Runtime.CompilerServices
{
	/// <summary>Specifies the details of how a method is implemented. This class cannot be inherited. </summary>
	// Token: 0x0200088C RID: 2188
	[AttributeUsage(AttributeTargets.Constructor | AttributeTargets.Method, Inherited = false)]
	[ComVisible(true)]
	[Serializable]
	public sealed class MethodImplAttribute : Attribute
	{
		// Token: 0x06005467 RID: 21607 RVA: 0x00127688 File Offset: 0x00125888
		internal MethodImplAttribute(MethodImplAttributes methodImplAttributes)
		{
			MethodImplOptions methodImplOptions = MethodImplOptions.Unmanaged | MethodImplOptions.ForwardRef | MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall | MethodImplOptions.Synchronized | MethodImplOptions.NoInlining | MethodImplOptions.AggressiveInlining | MethodImplOptions.NoOptimization;
			this._val = (MethodImplOptions)(methodImplAttributes & (MethodImplAttributes)methodImplOptions);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.CompilerServices.MethodImplAttribute" /> class with the specified <see cref="T:System.Runtime.CompilerServices.MethodImplOptions" /> value.</summary>
		/// <param name="methodImplOptions">A <see cref="T:System.Runtime.CompilerServices.MethodImplOptions" /> value specifying properties of the attributed method. </param>
		// Token: 0x06005468 RID: 21608 RVA: 0x001276AA File Offset: 0x001258AA
		public MethodImplAttribute(MethodImplOptions methodImplOptions)
		{
			this._val = methodImplOptions;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.CompilerServices.MethodImplAttribute" /> class with the specified <see cref="T:System.Runtime.CompilerServices.MethodImplOptions" /> value.</summary>
		/// <param name="value">A bitmask representing the desired <see cref="T:System.Runtime.CompilerServices.MethodImplOptions" /> value which specifies properties of the attributed method. </param>
		// Token: 0x06005469 RID: 21609 RVA: 0x001276AA File Offset: 0x001258AA
		public MethodImplAttribute(short value)
		{
			this._val = (MethodImplOptions)value;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.CompilerServices.MethodImplAttribute" /> class.</summary>
		// Token: 0x0600546A RID: 21610 RVA: 0x00002180 File Offset: 0x00000380
		public MethodImplAttribute()
		{
		}

		/// <summary>Gets the <see cref="T:System.Runtime.CompilerServices.MethodImplOptions" /> value describing the attributed method.</summary>
		/// <returns>The <see cref="T:System.Runtime.CompilerServices.MethodImplOptions" /> value describing the attributed method.</returns>
		// Token: 0x17000EC5 RID: 3781
		// (get) Token: 0x0600546B RID: 21611 RVA: 0x001276B9 File Offset: 0x001258B9
		public MethodImplOptions Value
		{
			get
			{
				return this._val;
			}
		}

		// Token: 0x04002BD1 RID: 11217
		internal MethodImplOptions _val;

		/// <summary>A <see cref="T:System.Runtime.CompilerServices.MethodCodeType" /> value indicating what kind of implementation is provided for this method.</summary>
		// Token: 0x04002BD2 RID: 11218
		public MethodCodeType MethodCodeType;
	}
}
