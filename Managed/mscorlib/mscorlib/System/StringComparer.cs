using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.InteropServices;

namespace System
{
	/// <summary>Represents a string comparison operation that uses specific case and culture-based or ordinal comparison rules.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020001C4 RID: 452
	[ComVisible(true)]
	[Serializable]
	public abstract class StringComparer : IComparer, IEqualityComparer, IComparer<string>, IEqualityComparer<string>
	{
		/// <summary>Gets a <see cref="T:System.StringComparer" /> object that performs a case-sensitive string comparison using the word comparison rules of the invariant culture.</summary>
		/// <returns>A new <see cref="T:System.StringComparer" /> object.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x1700023D RID: 573
		// (get) Token: 0x0600140E RID: 5134 RVA: 0x0005190C File Offset: 0x0004FB0C
		public static StringComparer InvariantCulture
		{
			get
			{
				return StringComparer._invariantCulture;
			}
		}

		/// <summary>Gets a <see cref="T:System.StringComparer" /> object that performs a case-insensitive string comparison using the word comparison rules of the invariant culture.</summary>
		/// <returns>A new <see cref="T:System.StringComparer" /> object.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x1700023E RID: 574
		// (get) Token: 0x0600140F RID: 5135 RVA: 0x00051913 File Offset: 0x0004FB13
		public static StringComparer InvariantCultureIgnoreCase
		{
			get
			{
				return StringComparer._invariantCultureIgnoreCase;
			}
		}

		/// <summary>Gets a <see cref="T:System.StringComparer" /> object that performs a case-sensitive string comparison using the word comparison rules of the current culture.</summary>
		/// <returns>A new <see cref="T:System.StringComparer" /> object.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x1700023F RID: 575
		// (get) Token: 0x06001410 RID: 5136 RVA: 0x0005191A File Offset: 0x0004FB1A
		public static StringComparer CurrentCulture
		{
			get
			{
				return new CultureAwareComparer(CultureInfo.CurrentCulture, false);
			}
		}

		/// <summary>Gets a <see cref="T:System.StringComparer" /> object that performs case-insensitive string comparisons using the word comparison rules of the current culture.</summary>
		/// <returns>A new <see cref="T:System.StringComparer" /> object.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x17000240 RID: 576
		// (get) Token: 0x06001411 RID: 5137 RVA: 0x00051927 File Offset: 0x0004FB27
		public static StringComparer CurrentCultureIgnoreCase
		{
			get
			{
				return new CultureAwareComparer(CultureInfo.CurrentCulture, true);
			}
		}

		/// <summary>Gets a <see cref="T:System.StringComparer" /> object that performs a case-sensitive ordinal string comparison.</summary>
		/// <returns>A <see cref="T:System.StringComparer" /> object.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000241 RID: 577
		// (get) Token: 0x06001412 RID: 5138 RVA: 0x00051934 File Offset: 0x0004FB34
		public static StringComparer Ordinal
		{
			get
			{
				return StringComparer._ordinal;
			}
		}

		/// <summary>Gets a <see cref="T:System.StringComparer" /> object that performs a case-insensitive ordinal string comparison.</summary>
		/// <returns>A <see cref="T:System.StringComparer" /> object.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000242 RID: 578
		// (get) Token: 0x06001413 RID: 5139 RVA: 0x0005193B File Offset: 0x0004FB3B
		public static StringComparer OrdinalIgnoreCase
		{
			get
			{
				return StringComparer._ordinalIgnoreCase;
			}
		}

		/// <summary>Creates a <see cref="T:System.StringComparer" /> object that compares strings according to the rules of a specified culture.</summary>
		/// <returns>A new <see cref="T:System.StringComparer" /> object that performs string comparisons according to the comparison rules used by the <paramref name="culture" /> parameter and the case rule specified by the <paramref name="ignoreCase" /> parameter.</returns>
		/// <param name="culture">A culture whose linguistic rules are used to perform a string comparison.</param>
		/// <param name="ignoreCase">true to specify that comparison operations be case-insensitive; false to specify that comparison operations be case-sensitive.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="culture" /> is null.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001414 RID: 5140 RVA: 0x00051942 File Offset: 0x0004FB42
		public static StringComparer Create(CultureInfo culture, bool ignoreCase)
		{
			if (culture == null)
			{
				throw new ArgumentNullException("culture");
			}
			return new CultureAwareComparer(culture, ignoreCase);
		}

		/// <summary>When overridden in a derived class, compares two objects and returns an indication of their relative sort order.</summary>
		/// <returns>A signed integer that indicates the relative values of <paramref name="x" /> and <paramref name="y" />, as shown in the following table.ValueMeaningLess than zero<paramref name="x" /> is less than <paramref name="y" />. -or-<paramref name="x" /> is null.Zero<paramref name="x" /> is equal to <paramref name="y" />.Greater than zero<paramref name="x" /> is greater than <paramref name="y" />.-or-<paramref name="y" /> is null.</returns>
		/// <param name="x">An object to compare to <paramref name="y" />.</param>
		/// <param name="y">An object to compare to <paramref name="x" />.</param>
		/// <exception cref="T:System.ArgumentException">Neither <paramref name="x" /> nor <paramref name="y" /> is a <see cref="T:System.String" /> object, and neither <paramref name="x" /> nor <paramref name="y" /> implements the <see cref="T:System.IComparable" /> interface.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001415 RID: 5141 RVA: 0x0005195C File Offset: 0x0004FB5C
		public int Compare(object x, object y)
		{
			if (x == y)
			{
				return 0;
			}
			if (x == null)
			{
				return -1;
			}
			if (y == null)
			{
				return 1;
			}
			string text = x as string;
			if (text != null)
			{
				string text2 = y as string;
				if (text2 != null)
				{
					return this.Compare(text, text2);
				}
			}
			IComparable comparable = x as IComparable;
			if (comparable != null)
			{
				return comparable.CompareTo(y);
			}
			throw new ArgumentException(Environment.GetResourceString("At least one object must implement IComparable."));
		}

		/// <summary>When overridden in a derived class, indicates whether two objects are equal.</summary>
		/// <returns>true if <paramref name="x" /> and <paramref name="y" /> refer to the same object, or <paramref name="x" /> and <paramref name="y" /> are both the same type of object and those objects are equal; otherwise, false.</returns>
		/// <param name="x">An object to compare to <paramref name="y" />.</param>
		/// <param name="y">An object to compare to <paramref name="x" />.</param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001416 RID: 5142 RVA: 0x000519B8 File Offset: 0x0004FBB8
		public bool Equals(object x, object y)
		{
			if (x == y)
			{
				return true;
			}
			if (x == null || y == null)
			{
				return false;
			}
			string text = x as string;
			if (text != null)
			{
				string text2 = y as string;
				if (text2 != null)
				{
					return this.Equals(text, text2);
				}
			}
			return x.Equals(y);
		}

		/// <summary>When overridden in a derived class, gets the hash code for the specified object.</summary>
		/// <returns>A 32-bit signed hash code calculated from the value of the <paramref name="obj" /> parameter.</returns>
		/// <param name="obj">An object.</param>
		/// <exception cref="T:System.ArgumentException">Not enough memory is available to allocate the buffer that is required to compute the hash code.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="obj" /> is null.</exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06001417 RID: 5143 RVA: 0x000519F8 File Offset: 0x0004FBF8
		public int GetHashCode(object obj)
		{
			if (obj == null)
			{
				throw new ArgumentNullException("obj");
			}
			string text = obj as string;
			if (text != null)
			{
				return this.GetHashCode(text);
			}
			return obj.GetHashCode();
		}

		/// <summary>When overridden in a derived class, compares two strings and returns an indication of their relative sort order.</summary>
		/// <returns>A signed integer that indicates the relative values of <paramref name="x" /> and <paramref name="y" />, as shown in the following table.ValueMeaningLess than zero<paramref name="x" /> is less than <paramref name="y" />.-or-<paramref name="x" /> is null.Zero<paramref name="x" /> is equal to <paramref name="y" />.Greater than zero<paramref name="x" /> is greater than <paramref name="y" />.-or-<paramref name="y" /> is null.</returns>
		/// <param name="x">A string to compare to <paramref name="y" />.</param>
		/// <param name="y">A string to compare to <paramref name="x" />.</param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001418 RID: 5144
		public abstract int Compare(string x, string y);

		/// <summary>When overridden in a derived class, indicates whether two strings are equal.</summary>
		/// <returns>true if <paramref name="x" /> and <paramref name="y" /> refer to the same object, or <paramref name="x" /> and <paramref name="y" /> are equal; otherwise, false.</returns>
		/// <param name="x">A string to compare to <paramref name="y" />.</param>
		/// <param name="y">A string to compare to <paramref name="x" />.</param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001419 RID: 5145
		public abstract bool Equals(string x, string y);

		/// <summary>When overridden in a derived class, gets the hash code for the specified string.</summary>
		/// <returns>A 32-bit signed hash code calculated from the value of the <paramref name="obj" /> parameter.</returns>
		/// <param name="obj">A string.</param>
		/// <exception cref="T:System.ArgumentException">Not enough memory is available to allocate the buffer that is required to compute the hash code.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="obj" /> is null. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0600141A RID: 5146
		public abstract int GetHashCode(string obj);

		// Token: 0x04000AE6 RID: 2790
		private static readonly StringComparer _invariantCulture = new CultureAwareComparer(CultureInfo.InvariantCulture, false);

		// Token: 0x04000AE7 RID: 2791
		private static readonly StringComparer _invariantCultureIgnoreCase = new CultureAwareComparer(CultureInfo.InvariantCulture, true);

		// Token: 0x04000AE8 RID: 2792
		private static readonly StringComparer _ordinal = new OrdinalComparer(false);

		// Token: 0x04000AE9 RID: 2793
		private static readonly StringComparer _ordinalIgnoreCase = new OrdinalComparer(true);
	}
}
