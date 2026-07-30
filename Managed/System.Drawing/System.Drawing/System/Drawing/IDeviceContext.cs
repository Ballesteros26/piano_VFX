using System;

namespace System.Drawing
{
	/// <summary>Defines methods for obtaining and releasing an existing handle to a Windows device context.</summary>
	// Token: 0x0200002A RID: 42
	public interface IDeviceContext : IDisposable
	{
		/// <summary>Returns the handle to a Windows device context.</summary>
		/// <returns>An <see cref="T:System.IntPtr" /> representing the handle of a device context.</returns>
		// Token: 0x060000BB RID: 187
		IntPtr GetHdc();

		/// <summary>Releases the handle of a Windows device context.</summary>
		// Token: 0x060000BC RID: 188
		void ReleaseHdc();
	}
}
