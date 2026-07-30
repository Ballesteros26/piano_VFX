using System;
using System.Diagnostics;

namespace System
{
	/// <summary>Represents a value type that can be assigned null.</summary>
	/// <typeparam name="T">The underlying value type of the <see cref="T:System.Nullable`1" /> generic type.</typeparam>
	/// <filterpriority>1</filterpriority>
	// Token: 0x02000224 RID: 548
	[DebuggerStepThrough]
	[Serializable]
	public struct Nullable<T> where T : struct
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Nullable`1" /> structure to the specified value. </summary>
		/// <param name="value">A value type.</param>
		// Token: 0x060019E4 RID: 6628 RVA: 0x00060586 File Offset: 0x0005E786
		public Nullable(T value)
		{
			this.has_value = true;
			this.value = value;
		}

		/// <summary>Gets a value indicating whether the current <see cref="T:System.Nullable`1" /> object has a value.</summary>
		/// <returns>true if the current <see cref="T:System.Nullable`1" /> object has a value; false if the current <see cref="T:System.Nullable`1" /> object has no value.</returns>
		// Token: 0x1700037C RID: 892
		// (get) Token: 0x060019E5 RID: 6629 RVA: 0x00060596 File Offset: 0x0005E796
		public bool HasValue
		{
			get
			{
				return this.has_value;
			}
		}

		/// <summary>Gets the value of the current <see cref="T:System.Nullable`1" /> value.</summary>
		/// <returns>The value of the current <see cref="T:System.Nullable`1" /> object if the <see cref="P:System.Nullable`1.HasValue" /> property is true. An exception is thrown if the <see cref="P:System.Nullable`1.HasValue" /> property is false.</returns>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="P:System.Nullable`1.HasValue" /> property is false.</exception>
		// Token: 0x1700037D RID: 893
		// (get) Token: 0x060019E6 RID: 6630 RVA: 0x0006059E File Offset: 0x0005E79E
		public T Value
		{
			get
			{
				if (!this.has_value)
				{
					throw new InvalidOperationException("Nullable object must have a value.");
				}
				return this.value;
			}
		}

		/// <summary>Indicates whether the current <see cref="T:System.Nullable`1" /> object is equal to a specified object.</summary>
		/// <returns>true if the <paramref name="other" /> parameter is equal to the current <see cref="T:System.Nullable`1" /> object; otherwise, false. This table describes how equality is defined for the compared values: Return ValueDescriptiontrueThe <see cref="P:System.Nullable`1.HasValue" /> property is false, and the <paramref name="other" /> parameter is null. That is, two null values are equal by definition.-or-The <see cref="P:System.Nullable`1.HasValue" /> property is true, and the value returned by the <see cref="P:System.Nullable`1.Value" /> property is equal to the <paramref name="other" /> parameter.falseThe <see cref="P:System.Nullable`1.HasValue" /> property for the current <see cref="T:System.Nullable`1" /> structure is true, and the <paramref name="other" /> parameter is null.-or-The <see cref="P:System.Nullable`1.HasValue" /> property for the current <see cref="T:System.Nullable`1" /> structure is false, and the <paramref name="other" /> parameter is not null.-or-The <see cref="P:System.Nullable`1.HasValue" /> property for the current <see cref="T:System.Nullable`1" /> structure is true, and the value returned by the <see cref="P:System.Nullable`1.Value" /> property is not equal to the <paramref name="other" /> parameter.</returns>
		/// <param name="other">An object.</param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060019E7 RID: 6631 RVA: 0x000605B9 File Offset: 0x0005E7B9
		public override bool Equals(object other)
		{
			if (other == null)
			{
				return !this.has_value;
			}
			return other is T? && this.Equals((T?)other);
		}

		// Token: 0x060019E8 RID: 6632 RVA: 0x000605DE File Offset: 0x0005E7DE
		private bool Equals(T? other)
		{
			return other.has_value == this.has_value && (!this.has_value || other.value.Equals(this.value));
		}

		/// <summary>Retrieves the hash code of the object returned by the <see cref="P:System.Nullable`1.Value" /> property.</summary>
		/// <returns>The hash code of the object returned by the <see cref="P:System.Nullable`1.Value" /> property if the <see cref="P:System.Nullable`1.HasValue" /> property is true, or zero if the <see cref="P:System.Nullable`1.HasValue" /> property is false. </returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060019E9 RID: 6633 RVA: 0x00060617 File Offset: 0x0005E817
		public override int GetHashCode()
		{
			if (!this.has_value)
			{
				return 0;
			}
			return this.value.GetHashCode();
		}

		/// <summary>Retrieves the value of the current <see cref="T:System.Nullable`1" /> object, or the object's default value.</summary>
		/// <returns>The value of the <see cref="P:System.Nullable`1.Value" /> property if the  <see cref="P:System.Nullable`1.HasValue" /> property is true; otherwise, the default value of the current <see cref="T:System.Nullable`1" /> object. The type of the default value is the type argument of the current <see cref="T:System.Nullable`1" /> object, and the value of the default value consists solely of binary zeroes.</returns>
		// Token: 0x060019EA RID: 6634 RVA: 0x00060634 File Offset: 0x0005E834
		public T GetValueOrDefault()
		{
			return this.value;
		}

		/// <summary>Retrieves the value of the current <see cref="T:System.Nullable`1" /> object, or the specified default value.</summary>
		/// <returns>The value of the <see cref="P:System.Nullable`1.Value" /> property if the <see cref="P:System.Nullable`1.HasValue" /> property is true; otherwise, the <paramref name="defaultValue" /> parameter.</returns>
		/// <param name="defaultValue">A value to return if the <see cref="P:System.Nullable`1.HasValue" /> property is false.</param>
		// Token: 0x060019EB RID: 6635 RVA: 0x0006063C File Offset: 0x0005E83C
		public T GetValueOrDefault(T defaultValue)
		{
			if (!this.has_value)
			{
				return defaultValue;
			}
			return this.value;
		}

		/// <summary>Returns the text representation of the value of the current <see cref="T:System.Nullable`1" /> object.</summary>
		/// <returns>The text representation of the value of the current <see cref="T:System.Nullable`1" /> object if the <see cref="P:System.Nullable`1.HasValue" /> property is true, or an empty string ("") if the <see cref="P:System.Nullable`1.HasValue" /> property is false.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060019EC RID: 6636 RVA: 0x0006064E File Offset: 0x0005E84E
		public override string ToString()
		{
			if (this.has_value)
			{
				return this.value.ToString();
			}
			return string.Empty;
		}

		/// <summary>Creates a new <see cref="T:System.Nullable`1" /> object initialized to a specified value. </summary>
		/// <returns>A <see cref="T:System.Nullable`1" /> object whose <see cref="P:System.Nullable`1.Value" /> property is initialized with the <paramref name="value" /> parameter.</returns>
		/// <param name="value">A value type.</param>
		// Token: 0x060019ED RID: 6637 RVA: 0x0006066F File Offset: 0x0005E86F
		public static implicit operator T?(T value)
		{
			return new T?(value);
		}

		/// <summary>Returns the value of a specified <see cref="T:System.Nullable`1" /> value.</summary>
		/// <returns>The value of the <see cref="P:System.Nullable`1.Value" /> property for the <paramref name="value" /> parameter.</returns>
		/// <param name="value">A <see cref="T:System.Nullable`1" /> value.</param>
		// Token: 0x060019EE RID: 6638 RVA: 0x00060677 File Offset: 0x0005E877
		public static explicit operator T(T? value)
		{
			return value.Value;
		}

		// Token: 0x060019EF RID: 6639 RVA: 0x00060680 File Offset: 0x0005E880
		private static object Box(T? o)
		{
			if (!o.has_value)
			{
				return null;
			}
			return o.value;
		}

		// Token: 0x060019F0 RID: 6640 RVA: 0x00060698 File Offset: 0x0005E898
		private static T? Unbox(object o)
		{
			if (o == null)
			{
				return null;
			}
			return new T?((T)((object)o));
		}

		// Token: 0x04000CD2 RID: 3282
		internal T value;

		// Token: 0x04000CD3 RID: 3283
		internal bool has_value;
	}
}
