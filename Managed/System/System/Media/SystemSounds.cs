using System;

namespace System.Media
{
	/// <summary>Retrieves sounds associated with a set of Windows operating system sound-event types. This class cannot be inherited.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000126 RID: 294
	public sealed class SystemSounds
	{
		// Token: 0x060007F3 RID: 2035 RVA: 0x000020EB File Offset: 0x000002EB
		private SystemSounds()
		{
		}

		/// <summary>Gets the sound associated with the Asterisk program event in the current Windows sound scheme.</summary>
		/// <returns>A <see cref="T:System.Media.SystemSound" /> associated with the Asterisk program event in the current Windows sound scheme.</returns>
		// Token: 0x17000163 RID: 355
		// (get) Token: 0x060007F4 RID: 2036 RVA: 0x0002750C File Offset: 0x0002570C
		public static SystemSound Asterisk
		{
			get
			{
				return new SystemSound("Asterisk");
			}
		}

		/// <summary>Gets the sound associated with the Beep program event in the current Windows sound scheme.</summary>
		/// <returns>A <see cref="T:System.Media.SystemSound" /> associated with the Beep program event in the current Windows sound scheme.</returns>
		// Token: 0x17000164 RID: 356
		// (get) Token: 0x060007F5 RID: 2037 RVA: 0x00027518 File Offset: 0x00025718
		public static SystemSound Beep
		{
			get
			{
				return new SystemSound("Beep");
			}
		}

		/// <summary>Gets the sound associated with the Exclamation program event in the current Windows sound scheme.</summary>
		/// <returns>A <see cref="T:System.Media.SystemSound" /> associated with the Exclamation program event in the current Windows sound scheme.</returns>
		// Token: 0x17000165 RID: 357
		// (get) Token: 0x060007F6 RID: 2038 RVA: 0x00027524 File Offset: 0x00025724
		public static SystemSound Exclamation
		{
			get
			{
				return new SystemSound("Exclamation");
			}
		}

		/// <summary>Gets the sound associated with the Hand program event in the current Windows sound scheme.</summary>
		/// <returns>A <see cref="T:System.Media.SystemSound" /> associated with the Hand program event in the current Windows sound scheme.</returns>
		// Token: 0x17000166 RID: 358
		// (get) Token: 0x060007F7 RID: 2039 RVA: 0x00027530 File Offset: 0x00025730
		public static SystemSound Hand
		{
			get
			{
				return new SystemSound("Hand");
			}
		}

		/// <summary>Gets the sound associated with the Question program event in the current Windows sound scheme.</summary>
		/// <returns>A <see cref="T:System.Media.SystemSound" /> associated with the Question program event in the current Windows sound scheme.</returns>
		// Token: 0x17000167 RID: 359
		// (get) Token: 0x060007F8 RID: 2040 RVA: 0x0002753C File Offset: 0x0002573C
		public static SystemSound Question
		{
			get
			{
				return new SystemSound("Question");
			}
		}
	}
}
