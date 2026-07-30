using System;

namespace System
{
	/// <summary>Describes the console key that was pressed, including the character represented by the console key and the state of the SHIFT, ALT, and CTRL modifier keys.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x0200013E RID: 318
	[Serializable]
	public struct ConsoleKeyInfo
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ConsoleKeyInfo" /> structure using the specified character, console key, and modifier keys.</summary>
		/// <param name="keyChar">The Unicode character that corresponds to the <paramref name="key" /> parameter. </param>
		/// <param name="key">The console key that corresponds to the <paramref name="keyChar" /> parameter. </param>
		/// <param name="shift">true to indicate that a SHIFT key was pressed; otherwise, false. </param>
		/// <param name="alt">true to indicate that an ALT key was pressed; otherwise, false. </param>
		/// <param name="control">true to indicate that a CTRL key was pressed; otherwise, false. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The numeric value of the <paramref name="key" /> parameter is less than 0 or greater than 255.</exception>
		// Token: 0x06000B9F RID: 2975 RVA: 0x00035B24 File Offset: 0x00033D24
		public ConsoleKeyInfo(char keyChar, ConsoleKey key, bool shift, bool alt, bool control)
		{
			if (key < (ConsoleKey)0 || key > (ConsoleKey)255)
			{
				throw new ArgumentOutOfRangeException("key", Environment.GetResourceString("Console key values must be between 0 and 255."));
			}
			this._keyChar = keyChar;
			this._key = key;
			this._mods = (ConsoleModifiers)0;
			if (shift)
			{
				this._mods |= ConsoleModifiers.Shift;
			}
			if (alt)
			{
				this._mods |= ConsoleModifiers.Alt;
			}
			if (control)
			{
				this._mods |= ConsoleModifiers.Control;
			}
		}

		/// <summary>Gets the Unicode character represented by the current <see cref="T:System.ConsoleKeyInfo" /> object.</summary>
		/// <returns>An object that corresponds to the console key represented by the current <see cref="T:System.ConsoleKeyInfo" /> object.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170001BE RID: 446
		// (get) Token: 0x06000BA0 RID: 2976 RVA: 0x00035B9C File Offset: 0x00033D9C
		public char KeyChar
		{
			get
			{
				return this._keyChar;
			}
		}

		/// <summary>Gets the console key represented by the current <see cref="T:System.ConsoleKeyInfo" /> object.</summary>
		/// <returns>A value that identifies the console key that was pressed.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170001BF RID: 447
		// (get) Token: 0x06000BA1 RID: 2977 RVA: 0x00035BA4 File Offset: 0x00033DA4
		public ConsoleKey Key
		{
			get
			{
				return this._key;
			}
		}

		/// <summary>Gets a bitwise combination of <see cref="T:System.ConsoleModifiers" /> values that specifies one or more modifier keys pressed simultaneously with the console key.</summary>
		/// <returns>A bitwise combination of the enumeration values. There is no default value.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170001C0 RID: 448
		// (get) Token: 0x06000BA2 RID: 2978 RVA: 0x00035BAC File Offset: 0x00033DAC
		public ConsoleModifiers Modifiers
		{
			get
			{
				return this._mods;
			}
		}

		/// <summary>Gets a value indicating whether the specified object is equal to the current <see cref="T:System.ConsoleKeyInfo" /> object.</summary>
		/// <returns>true if <paramref name="value" /> is a <see cref="T:System.ConsoleKeyInfo" /> object and is equal to the current <see cref="T:System.ConsoleKeyInfo" /> object; otherwise, false.</returns>
		/// <param name="value">An object to compare to the current <see cref="T:System.ConsoleKeyInfo" /> object.</param>
		// Token: 0x06000BA3 RID: 2979 RVA: 0x00035BB4 File Offset: 0x00033DB4
		public override bool Equals(object value)
		{
			return value is ConsoleKeyInfo && this.Equals((ConsoleKeyInfo)value);
		}

		/// <summary>Gets a value indicating whether the specified <see cref="T:System.ConsoleKeyInfo" /> object is equal to the current <see cref="T:System.ConsoleKeyInfo" /> object.</summary>
		/// <returns>true if <paramref name="obj" /> is equal to the current <see cref="T:System.ConsoleKeyInfo" /> object; otherwise, false.</returns>
		/// <param name="obj">An object to compare to the current <see cref="T:System.ConsoleKeyInfo" /> object.</param>
		// Token: 0x06000BA4 RID: 2980 RVA: 0x00035BCC File Offset: 0x00033DCC
		public bool Equals(ConsoleKeyInfo obj)
		{
			return obj._keyChar == this._keyChar && obj._key == this._key && obj._mods == this._mods;
		}

		/// <summary>Indicates whether the specified <see cref="T:System.ConsoleKeyInfo" /> objects are equal.</summary>
		/// <returns>true if <paramref name="a" /> is equal to <paramref name="b" />; otherwise, false.</returns>
		/// <param name="a">The first object to compare.</param>
		/// <param name="b">The second object to compare.</param>
		// Token: 0x06000BA5 RID: 2981 RVA: 0x00035BFA File Offset: 0x00033DFA
		public static bool operator ==(ConsoleKeyInfo a, ConsoleKeyInfo b)
		{
			return a.Equals(b);
		}

		/// <summary>Indicates whether the specified <see cref="T:System.ConsoleKeyInfo" /> objects are not equal.</summary>
		/// <returns>true if <paramref name="a" /> is not equal to <paramref name="b" />; otherwise, false.</returns>
		/// <param name="a">The first object to compare.</param>
		/// <param name="b">The second object to compare.</param>
		// Token: 0x06000BA6 RID: 2982 RVA: 0x00035C04 File Offset: 0x00033E04
		public static bool operator !=(ConsoleKeyInfo a, ConsoleKeyInfo b)
		{
			return !(a == b);
		}

		/// <summary>Returns the hash code for the current <see cref="T:System.ConsoleKeyInfo" /> object.</summary>
		/// <returns>A 32-bit signed integer hash code.</returns>
		// Token: 0x06000BA7 RID: 2983 RVA: 0x00035C10 File Offset: 0x00033E10
		public override int GetHashCode()
		{
			return (int)((ConsoleModifiers)this._keyChar | this._mods);
		}

		// Token: 0x04000879 RID: 2169
		private char _keyChar;

		// Token: 0x0400087A RID: 2170
		private ConsoleKey _key;

		// Token: 0x0400087B RID: 2171
		private ConsoleModifiers _mods;
	}
}
