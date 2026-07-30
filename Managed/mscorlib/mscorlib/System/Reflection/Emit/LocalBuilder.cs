using System;
using System.Runtime.InteropServices;
using Unity;

namespace System.Reflection.Emit
{
	/// <summary>Represents a local variable within a method or constructor.</summary>
	// Token: 0x02000368 RID: 872
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof(_LocalBuilder))]
	[ComVisible(true)]
	[StructLayout(LayoutKind.Sequential)]
	public sealed class LocalBuilder : LocalVariableInfo, _LocalBuilder
	{
		// Token: 0x06002748 RID: 10056 RVA: 0x0008BAAC File Offset: 0x00089CAC
		internal LocalBuilder(Type t, ILGenerator ilgen)
		{
			this.type = t;
			this.ilgen = ilgen;
		}

		/// <summary>Sets the name and lexical scope of this local variable.</summary>
		/// <param name="name">The name of the local variable. </param>
		/// <param name="startOffset">The beginning offset of the lexical scope of the local variable. </param>
		/// <param name="endOffset">The ending offset of the lexical scope of the local variable. </param>
		/// <exception cref="T:System.InvalidOperationException">The containing type has been created with <see cref="M:System.Reflection.Emit.TypeBuilder.CreateType" />.-or- There is no symbolic writer defined for the containing module. </exception>
		/// <exception cref="T:System.NotSupportedException">This local is defined in a dynamic method, rather than in a method of a dynamic type.</exception>
		// Token: 0x06002749 RID: 10057 RVA: 0x0008BAC2 File Offset: 0x00089CC2
		public void SetLocalSymInfo(string name, int startOffset, int endOffset)
		{
			this.name = name;
			this.startOffset = startOffset;
			this.endOffset = endOffset;
		}

		/// <summary>Sets the name of this local variable.</summary>
		/// <param name="name">The name of the local variable. </param>
		/// <exception cref="T:System.InvalidOperationException">The containing type has been created with <see cref="M:System.Reflection.Emit.TypeBuilder.CreateType" />.-or- There is no symbolic writer defined for the containing module. </exception>
		/// <exception cref="T:System.NotSupportedException">This local is defined in a dynamic method, rather than in a method of a dynamic type.</exception>
		// Token: 0x0600274A RID: 10058 RVA: 0x0008BAD9 File Offset: 0x00089CD9
		public void SetLocalSymInfo(string name)
		{
			this.SetLocalSymInfo(name, 0, 0);
		}

		/// <summary>Gets the type of the local variable.</summary>
		/// <returns>The <see cref="T:System.Type" /> of the local variable.</returns>
		// Token: 0x1700062D RID: 1581
		// (get) Token: 0x0600274B RID: 10059 RVA: 0x00081D6F File Offset: 0x0007FF6F
		public override Type LocalType
		{
			get
			{
				return this.type;
			}
		}

		/// <summary>Gets a value indicating whether the object referred to by the local variable is pinned in memory.</summary>
		/// <returns>true if the object referred to by the local variable is pinned in memory; otherwise, false.</returns>
		// Token: 0x1700062E RID: 1582
		// (get) Token: 0x0600274C RID: 10060 RVA: 0x00081D5F File Offset: 0x0007FF5F
		public override bool IsPinned
		{
			get
			{
				return this.is_pinned;
			}
		}

		/// <summary>Gets the zero-based index of the local variable within the method body.</summary>
		/// <returns>An integer value that represents the order of declaration of the local variable within the method body.</returns>
		// Token: 0x1700062F RID: 1583
		// (get) Token: 0x0600274D RID: 10061 RVA: 0x00081D67 File Offset: 0x0007FF67
		public override int LocalIndex
		{
			get
			{
				return (int)this.position;
			}
		}

		// Token: 0x17000630 RID: 1584
		// (get) Token: 0x0600274E RID: 10062 RVA: 0x0008BAE4 File Offset: 0x00089CE4
		internal string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x17000631 RID: 1585
		// (get) Token: 0x0600274F RID: 10063 RVA: 0x0008BAEC File Offset: 0x00089CEC
		internal int StartOffset
		{
			get
			{
				return this.startOffset;
			}
		}

		// Token: 0x17000632 RID: 1586
		// (get) Token: 0x06002750 RID: 10064 RVA: 0x0008BAF4 File Offset: 0x00089CF4
		internal int EndOffset
		{
			get
			{
				return this.endOffset;
			}
		}

		/// <summary>Maps a set of names to a corresponding set of dispatch identifiers.</summary>
		/// <param name="riid">Reserved for future use. Must be IID_NULL.</param>
		/// <param name="rgszNames">Passed-in array of names to be mapped.</param>
		/// <param name="cNames">Count of the names to be mapped.</param>
		/// <param name="lcid">The locale context in which to interpret the names.</param>
		/// <param name="rgDispId">Caller-allocated array which receives the IDs corresponding to the names.</param>
		/// <exception cref="T:System.NotImplementedException">Late-bound access using the COM IDispatch interface is not supported.</exception>
		// Token: 0x06002751 RID: 10065 RVA: 0x0002126B File Offset: 0x0001F46B
		void _LocalBuilder.GetIDsOfNames([In] ref Guid riid, IntPtr rgszNames, uint cNames, uint lcid, IntPtr rgDispId)
		{
			throw new NotImplementedException();
		}

		/// <summary>Retrieves the type information for an object, which can then be used to get the type information for an interface.</summary>
		/// <param name="iTInfo">The type information to return.</param>
		/// <param name="lcid">The locale identifier for the type information.</param>
		/// <param name="ppTInfo">Receives a pointer to the requested type information object.</param>
		/// <exception cref="T:System.NotImplementedException">Late-bound access using the COM IDispatch interface is not supported.</exception>
		// Token: 0x06002752 RID: 10066 RVA: 0x0002126B File Offset: 0x0001F46B
		void _LocalBuilder.GetTypeInfo(uint iTInfo, uint lcid, IntPtr ppTInfo)
		{
			throw new NotImplementedException();
		}

		/// <summary>Retrieves the number of type information interfaces that an object provides (either 0 or 1).</summary>
		/// <param name="pcTInfo">Points to a location that receives the number of type information interfaces provided by the object.</param>
		/// <exception cref="T:System.NotImplementedException">Late-bound access using the COM IDispatch interface is not supported.</exception>
		// Token: 0x06002753 RID: 10067 RVA: 0x0002126B File Offset: 0x0001F46B
		void _LocalBuilder.GetTypeInfoCount(out uint pcTInfo)
		{
			throw new NotImplementedException();
		}

		/// <summary>Provides access to properties and methods exposed by an object.</summary>
		/// <param name="dispIdMember">Identifies the member.</param>
		/// <param name="riid">Reserved for future use. Must be IID_NULL.</param>
		/// <param name="lcid">The locale context in which to interpret arguments.</param>
		/// <param name="wFlags">Flags describing the context of the call.</param>
		/// <param name="pDispParams">Pointer to a structure containing an array of arguments, an array of argument DISPIDs for named arguments, and counts for the number of elements in the arrays.</param>
		/// <param name="pVarResult">Pointer to the location where the result is to be stored.</param>
		/// <param name="pExcepInfo">Pointer to a structure that contains exception information.</param>
		/// <param name="puArgErr">The index of the first argument that has an error.</param>
		/// <exception cref="T:System.NotImplementedException">Late-bound access using the COM IDispatch interface is not supported.</exception>
		// Token: 0x06002754 RID: 10068 RVA: 0x0002126B File Offset: 0x0001F46B
		void _LocalBuilder.Invoke(uint dispIdMember, [In] ref Guid riid, uint lcid, short wFlags, IntPtr pDispParams, IntPtr pVarResult, IntPtr pExcepInfo, IntPtr puArgErr)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06002755 RID: 10069 RVA: 0x0001FB35 File Offset: 0x0001DD35
		internal LocalBuilder()
		{
			ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x0400145E RID: 5214
		private string name;

		// Token: 0x0400145F RID: 5215
		internal ILGenerator ilgen;

		// Token: 0x04001460 RID: 5216
		private int startOffset;

		// Token: 0x04001461 RID: 5217
		private int endOffset;
	}
}
