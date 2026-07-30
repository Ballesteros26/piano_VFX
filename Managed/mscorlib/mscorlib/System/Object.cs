using System;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;

namespace System
{
	/// <summary>Supports all classes in the .NET Framework class hierarchy and provides low-level services to derived classes. This is the ultimate base class of all classes in the .NET Framework; it is the root of the type hierarchy.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x02000227 RID: 551
	[ComVisible(true)]
	[ClassInterface(ClassInterfaceType.AutoDual)]
	[Serializable]
	public class Object
	{
		/// <summary>Determines whether the specified object is equal to the current object.</summary>
		/// <returns>true if the specified object  is equal to the current object; otherwise, false.</returns>
		/// <param name="obj">The object to compare with the current object. </param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06001A46 RID: 6726 RVA: 0x0003CBCA File Offset: 0x0003ADCA
		public virtual bool Equals(object obj)
		{
			return this == obj;
		}

		/// <summary>Determines whether the specified object instances are considered equal.</summary>
		/// <returns>true if the objects are considered equal; otherwise, false. If both <paramref name="objA" /> and <paramref name="objB" /> are null, the method returns true.</returns>
		/// <param name="objA">The first object to compare. </param>
		/// <param name="objB">The second object to compare. </param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06001A47 RID: 6727 RVA: 0x000636B4 File Offset: 0x000618B4
		public static bool Equals(object objA, object objB)
		{
			return objA == objB || (objA != null && objB != null && objA.Equals(objB));
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Object" /> class.</summary>
		// Token: 0x06001A48 RID: 6728 RVA: 0x00002194 File Offset: 0x00000394
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		public Object()
		{
		}

		/// <summary>Allows an object to try to free resources and perform other cleanup operations before it is reclaimed by garbage collection.</summary>
		// Token: 0x06001A49 RID: 6729 RVA: 0x00002194 File Offset: 0x00000394
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		protected virtual void Finalize()
		{
		}

		/// <summary>Serves as a hash function for a particular type. </summary>
		/// <returns>A hash code for the current object.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06001A4A RID: 6730 RVA: 0x000636CB File Offset: 0x000618CB
		public virtual int GetHashCode()
		{
			return object.InternalGetHashCode(this);
		}

		/// <summary>Gets the <see cref="T:System.Type" /> of the current instance.</summary>
		/// <returns>The exact runtime type of the current instance.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06001A4B RID: 6731
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern Type GetType();

		/// <summary>Creates a shallow copy of the current <see cref="T:System.Object" />.</summary>
		/// <returns>A shallow copy of the current <see cref="T:System.Object" />.</returns>
		// Token: 0x06001A4C RID: 6732
		[MethodImpl(MethodImplOptions.InternalCall)]
		protected extern object MemberwiseClone();

		/// <summary>Returns a string that represents the current object.</summary>
		/// <returns>A string that represents the current object.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06001A4D RID: 6733 RVA: 0x000636D3 File Offset: 0x000618D3
		public virtual string ToString()
		{
			return this.GetType().ToString();
		}

		/// <summary>Determines whether the specified <see cref="T:System.Object" /> instances are the same instance.</summary>
		/// <returns>true if <paramref name="objA" /> is the same instance as <paramref name="objB" /> or if both are null; otherwise, false.</returns>
		/// <param name="objA">The first object to compare. </param>
		/// <param name="objB">The second object  to compare. </param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06001A4E RID: 6734 RVA: 0x0003CBCA File Offset: 0x0003ADCA
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		public static bool ReferenceEquals(object objA, object objB)
		{
			return objA == objB;
		}

		// Token: 0x06001A4F RID: 6735
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern int InternalGetHashCode(object o);

		// Token: 0x06001A50 RID: 6736 RVA: 0x00002194 File Offset: 0x00000394
		private void FieldGetter(string typeName, string fieldName, ref object val)
		{
		}

		// Token: 0x06001A51 RID: 6737 RVA: 0x00002194 File Offset: 0x00000394
		private void FieldSetter(string typeName, string fieldName, object val)
		{
		}
	}
}
