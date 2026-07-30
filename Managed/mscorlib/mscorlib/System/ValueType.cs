using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;

namespace System
{
	/// <summary>Provides the base class for value types.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x0200024E RID: 590
	[ComVisible(true)]
	[Serializable]
	public abstract class ValueType
	{
		// Token: 0x06001B8F RID: 7055
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool InternalEquals(object o1, object o2, out object[] fields);

		// Token: 0x06001B90 RID: 7056 RVA: 0x00067FB8 File Offset: 0x000661B8
		internal static bool DefaultEquals(object o1, object o2)
		{
			if (o1 == null && o2 == null)
			{
				return true;
			}
			if (o1 == null || o2 == null)
			{
				return false;
			}
			RuntimeType runtimeType = (RuntimeType)o1.GetType();
			RuntimeType runtimeType2 = (RuntimeType)o2.GetType();
			if (runtimeType != runtimeType2)
			{
				return false;
			}
			object[] array;
			bool flag = ValueType.InternalEquals(o1, o2, out array);
			if (array == null)
			{
				return flag;
			}
			for (int i = 0; i < array.Length; i += 2)
			{
				object obj = array[i];
				object obj2 = array[i + 1];
				if (obj == null)
				{
					if (obj2 != null)
					{
						return false;
					}
				}
				else if (!obj.Equals(obj2))
				{
					return false;
				}
			}
			return true;
		}

		/// <summary>Indicates whether this instance and a specified object are equal.</summary>
		/// <returns>true if <paramref name="obj" /> and this instance are the same type and represent the same value; otherwise, false. </returns>
		/// <param name="obj">The object to compare with the current instance. </param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06001B91 RID: 7057 RVA: 0x0003D817 File Offset: 0x0003BA17
		public override bool Equals(object obj)
		{
			return ValueType.DefaultEquals(this, obj);
		}

		// Token: 0x06001B92 RID: 7058
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern int InternalGetHashCode(object o, out object[] fields);

		/// <summary>Returns the hash code for this instance.</summary>
		/// <returns>A 32-bit signed integer that is the hash code for this instance.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06001B93 RID: 7059 RVA: 0x00068038 File Offset: 0x00066238
		public override int GetHashCode()
		{
			object[] array;
			int num = ValueType.InternalGetHashCode(this, out array);
			if (array != null)
			{
				for (int i = 0; i < array.Length; i++)
				{
					if (array[i] != null)
					{
						num ^= array[i].GetHashCode();
					}
				}
			}
			return num;
		}

		// Token: 0x06001B94 RID: 7060 RVA: 0x00068070 File Offset: 0x00066270
		internal static int GetHashCodeOfPtr(IntPtr ptr)
		{
			int num = (int)ptr;
			int num2 = ValueType.Internal.hash_code_of_ptr_seed;
			if (num2 == 0)
			{
				num2 = num;
				Interlocked.CompareExchange(ref ValueType.Internal.hash_code_of_ptr_seed, num2, 0);
				num2 = ValueType.Internal.hash_code_of_ptr_seed;
			}
			return num - num2;
		}

		/// <summary>Returns the fully qualified type name of this instance.</summary>
		/// <returns>A <see cref="T:System.String" /> containing a fully qualified type name.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06001B95 RID: 7061 RVA: 0x000680A5 File Offset: 0x000662A5
		public override string ToString()
		{
			return base.GetType().FullName;
		}

		// Token: 0x0200024F RID: 591
		private static class Internal
		{
			// Token: 0x04000F6A RID: 3946
			public static int hash_code_of_ptr_seed;
		}
	}
}
