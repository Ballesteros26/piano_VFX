using System;

namespace System.Drawing.Imaging
{
	/// <summary>Provides properties that get the frame dimensions of an image. Not inheritable.</summary>
	// Token: 0x02000102 RID: 258
	public sealed class FrameDimension
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Imaging.FrameDimension" /> class using the specified Guid structure.</summary>
		/// <param name="guid">A Guid structure that contains a GUID for this <see cref="T:System.Drawing.Imaging.FrameDimension" /> object. </param>
		// Token: 0x06000C4A RID: 3146 RVA: 0x0001BBBA File Offset: 0x00019DBA
		public FrameDimension(Guid guid)
		{
			this._guid = guid;
		}

		/// <summary>Gets a globally unique identifier (GUID) that represents this <see cref="T:System.Drawing.Imaging.FrameDimension" /> object.</summary>
		/// <returns>A Guid structure that contains a GUID that represents this <see cref="T:System.Drawing.Imaging.FrameDimension" /> object.</returns>
		// Token: 0x1700035B RID: 859
		// (get) Token: 0x06000C4B RID: 3147 RVA: 0x0001BBC9 File Offset: 0x00019DC9
		public Guid Guid
		{
			get
			{
				return this._guid;
			}
		}

		/// <summary>Gets the time dimension.</summary>
		/// <returns>The time dimension.</returns>
		// Token: 0x1700035C RID: 860
		// (get) Token: 0x06000C4C RID: 3148 RVA: 0x0001BBD1 File Offset: 0x00019DD1
		public static FrameDimension Time
		{
			get
			{
				return FrameDimension.s_time;
			}
		}

		/// <summary>Gets the resolution dimension.</summary>
		/// <returns>The resolution dimension.</returns>
		// Token: 0x1700035D RID: 861
		// (get) Token: 0x06000C4D RID: 3149 RVA: 0x0001BBD8 File Offset: 0x00019DD8
		public static FrameDimension Resolution
		{
			get
			{
				return FrameDimension.s_resolution;
			}
		}

		/// <summary>Gets the page dimension.</summary>
		/// <returns>The page dimension.</returns>
		// Token: 0x1700035E RID: 862
		// (get) Token: 0x06000C4E RID: 3150 RVA: 0x0001BBDF File Offset: 0x00019DDF
		public static FrameDimension Page
		{
			get
			{
				return FrameDimension.s_page;
			}
		}

		/// <summary>Returns a value that indicates whether the specified object is a <see cref="T:System.Drawing.Imaging.FrameDimension" /> equivalent to this <see cref="T:System.Drawing.Imaging.FrameDimension" /> object.</summary>
		/// <returns>Returns true if <paramref name="o" /> is a <see cref="T:System.Drawing.Imaging.FrameDimension" /> equivalent to this <see cref="T:System.Drawing.Imaging.FrameDimension" /> object; otherwise, false.</returns>
		/// <param name="o">The object to test. </param>
		// Token: 0x06000C4F RID: 3151 RVA: 0x0001BBE8 File Offset: 0x00019DE8
		public override bool Equals(object o)
		{
			FrameDimension frameDimension = o as FrameDimension;
			return frameDimension != null && this._guid == frameDimension._guid;
		}

		/// <summary>Returns a hash code for this <see cref="T:System.Drawing.Imaging.FrameDimension" /> object.</summary>
		/// <returns>Returns an int value that is the hash code of this <see cref="T:System.Drawing.Imaging.FrameDimension" /> object.</returns>
		// Token: 0x06000C50 RID: 3152 RVA: 0x0001BC12 File Offset: 0x00019E12
		public override int GetHashCode()
		{
			return this._guid.GetHashCode();
		}

		/// <summary>Converts this <see cref="T:System.Drawing.Imaging.FrameDimension" /> object to a human-readable string.</summary>
		/// <returns>A string that represents this <see cref="T:System.Drawing.Imaging.FrameDimension" /> object.</returns>
		// Token: 0x06000C51 RID: 3153 RVA: 0x0001BC28 File Offset: 0x00019E28
		public override string ToString()
		{
			if (this == FrameDimension.s_time)
			{
				return "Time";
			}
			if (this == FrameDimension.s_resolution)
			{
				return "Resolution";
			}
			if (this == FrameDimension.s_page)
			{
				return "Page";
			}
			return "[FrameDimension: " + this._guid + "]";
		}

		// Token: 0x0400098B RID: 2443
		private static FrameDimension s_time = new FrameDimension(new Guid("{6aedbd6d-3fb5-418a-83a6-7f45229dc872}"));

		// Token: 0x0400098C RID: 2444
		private static FrameDimension s_resolution = new FrameDimension(new Guid("{84236f7b-3bd3-428f-8dab-4ea1439ca315}"));

		// Token: 0x0400098D RID: 2445
		private static FrameDimension s_page = new FrameDimension(new Guid("{7462dc86-6180-4c7e-8e3f-ee7333a7a483}"));

		// Token: 0x0400098E RID: 2446
		private Guid _guid;
	}
}
