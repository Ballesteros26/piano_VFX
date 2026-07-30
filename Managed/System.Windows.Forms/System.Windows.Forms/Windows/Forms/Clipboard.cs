using System;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Threading;

namespace System.Windows.Forms
{
	/// <summary>Provides methods to place data on and retrieve data from the system Clipboard. This class cannot be inherited.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x0200007B RID: 123
	public sealed class Clipboard
	{
		// Token: 0x060005A7 RID: 1447 RVA: 0x00017DBC File Offset: 0x00015FBC
		private Clipboard()
		{
		}

		// Token: 0x060005A8 RID: 1448 RVA: 0x00017DC4 File Offset: 0x00015FC4
		private static bool ConvertToClipboardData(ref int type, object obj, out byte[] data)
		{
			data = null;
			return false;
		}

		// Token: 0x060005A9 RID: 1449 RVA: 0x00017DCC File Offset: 0x00015FCC
		private static bool ConvertFromClipboardData(int type, IntPtr data, out object obj)
		{
			obj = null;
			return data == IntPtr.Zero && false;
		}

		/// <summary>Removes all data from the Clipboard.</summary>
		/// <exception cref="T:System.Runtime.InteropServices.ExternalException">The Clipboard could not be cleared. This typically occurs when the Clipboard is being used by another process.</exception>
		/// <exception cref="T:System.Threading.ThreadStateException">The current thread is not in single-threaded apartment (STA) mode. Add the <see cref="T:System.STAThreadAttribute" /> to your application's Main method.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060005AA RID: 1450 RVA: 0x00017DE4 File Offset: 0x00015FE4
		public static void Clear()
		{
			IntPtr intPtr = XplatUI.ClipboardOpen(false);
			XplatUI.ClipboardStore(intPtr, null, 0, null);
		}

		/// <summary>Indicates whether there is data on the Clipboard in the <see cref="F:System.Windows.Forms.DataFormats.WaveAudio" /> format.</summary>
		/// <returns>true if there is audio data on the Clipboard; otherwise, false.</returns>
		/// <exception cref="T:System.Runtime.InteropServices.ExternalException">The Clipboard could not be cleared. This typically occurs when the Clipboard is being used by another process.</exception>
		/// <exception cref="T:System.Threading.ThreadStateException">The current thread is not in single-threaded apartment (STA) mode. Add the <see cref="T:System.STAThreadAttribute" /> to your application's Main method.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x060005AB RID: 1451 RVA: 0x00017E04 File Offset: 0x00016004
		public static bool ContainsAudio()
		{
			return Clipboard.ClipboardContainsFormat(new string[] { DataFormats.WaveAudio });
		}

		/// <summary>Indicates whether there is data on the Clipboard that is in the specified format or can be converted to that format. </summary>
		/// <returns>true if there is data on the Clipboard that is in the specified <paramref name="format" /> or can be converted to that format; otherwise, false.</returns>
		/// <param name="format">The format of the data to look for. See <see cref="T:System.Windows.Forms.DataFormats" /> for predefined formats.</param>
		/// <exception cref="T:System.Runtime.InteropServices.ExternalException">The Clipboard could not be cleared. This typically occurs when the Clipboard is being used by another process.</exception>
		/// <exception cref="T:System.Threading.ThreadStateException">The current thread is not in single-threaded apartment (STA) mode. Add the <see cref="T:System.STAThreadAttribute" /> to your application's Main method.</exception>
		// Token: 0x060005AC RID: 1452 RVA: 0x00017E1C File Offset: 0x0001601C
		public static bool ContainsData(string format)
		{
			return Clipboard.ClipboardContainsFormat(new string[] { format });
		}

		/// <summary>Indicates whether there is data on the Clipboard that is in the <see cref="F:System.Windows.Forms.DataFormats.FileDrop" /> format or can be converted to that format.</summary>
		/// <returns>true if there is a file drop list on the Clipboard; otherwise, false.</returns>
		/// <exception cref="T:System.Runtime.InteropServices.ExternalException">The Clipboard could not be cleared. This typically occurs when the Clipboard is being used by another process.</exception>
		/// <exception cref="T:System.Threading.ThreadStateException">The current thread is not in single-threaded apartment (STA) mode. Add the <see cref="T:System.STAThreadAttribute" /> to your application's Main method.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x060005AD RID: 1453 RVA: 0x00017E30 File Offset: 0x00016030
		public static bool ContainsFileDropList()
		{
			return Clipboard.ClipboardContainsFormat(new string[] { DataFormats.FileDrop });
		}

		/// <summary>Indicates whether there is data on the Clipboard that is in the <see cref="F:System.Windows.Forms.DataFormats.Bitmap" /> format or can be converted to that format.</summary>
		/// <returns>true if there is image data on the Clipboard; otherwise, false.</returns>
		/// <exception cref="T:System.Runtime.InteropServices.ExternalException">The Clipboard could not be cleared. This typically occurs when the Clipboard is being used by another process.</exception>
		/// <exception cref="T:System.Threading.ThreadStateException">The current thread is not in single-threaded apartment (STA) mode. Add the <see cref="T:System.STAThreadAttribute" /> to your application's Main method.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x060005AE RID: 1454 RVA: 0x00017E48 File Offset: 0x00016048
		public static bool ContainsImage()
		{
			return Clipboard.ClipboardContainsFormat(new string[] { DataFormats.Bitmap });
		}

		/// <summary>Indicates whether there is data on the Clipboard in the <see cref="F:System.Windows.Forms.TextDataFormat.Text" /> or <see cref="F:System.Windows.Forms.TextDataFormat.UnicodeText" /> format, depending on the operating system.</summary>
		/// <returns>true if there is text data on the Clipboard; otherwise, false.</returns>
		/// <exception cref="T:System.Runtime.InteropServices.ExternalException">The Clipboard could not be cleared. This typically occurs when the Clipboard is being used by another process.</exception>
		/// <exception cref="T:System.Threading.ThreadStateException">The current thread is not in single-threaded apartment (STA) mode. Add the <see cref="T:System.STAThreadAttribute" /> to your application's Main method.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x060005AF RID: 1455 RVA: 0x00017E60 File Offset: 0x00016060
		public static bool ContainsText()
		{
			return Clipboard.ClipboardContainsFormat(new string[]
			{
				DataFormats.Text,
				DataFormats.UnicodeText
			});
		}

		/// <summary>Indicates whether there is text data on the Clipboard in the format indicated by the specified <see cref="T:System.Windows.Forms.TextDataFormat" /> value.</summary>
		/// <returns>true if there is text data on the Clipboard in the value specified for <paramref name="format" />; otherwise, false.</returns>
		/// <param name="format">One of the <see cref="T:System.Windows.Forms.TextDataFormat" /> values.</param>
		/// <exception cref="T:System.Runtime.InteropServices.ExternalException">The Clipboard could not be cleared. This typically occurs when the Clipboard is being used by another process.</exception>
		/// <exception cref="T:System.Threading.ThreadStateException">The current thread is not in single-threaded apartment (STA) mode. Add the <see cref="T:System.STAThreadAttribute" /> to your application's Main method.</exception>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">
		///   <paramref name="format" /> is not a valid <see cref="T:System.Windows.Forms.TextDataFormat" /> value.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x060005B0 RID: 1456 RVA: 0x00017E80 File Offset: 0x00016080
		public static bool ContainsText(TextDataFormat format)
		{
			switch (format)
			{
			case TextDataFormat.Text:
				return Clipboard.ClipboardContainsFormat(new string[] { DataFormats.Text });
			case TextDataFormat.UnicodeText:
				return Clipboard.ClipboardContainsFormat(new string[] { DataFormats.UnicodeText });
			case TextDataFormat.Rtf:
				return Clipboard.ClipboardContainsFormat(new string[] { DataFormats.Rtf });
			case TextDataFormat.Html:
				return Clipboard.ClipboardContainsFormat(new string[] { DataFormats.Html });
			case TextDataFormat.CommaSeparatedValue:
				return Clipboard.ClipboardContainsFormat(new string[] { DataFormats.CommaSeparatedValue });
			default:
				return false;
			}
		}

		/// <summary>Retrieves an audio stream from the Clipboard.</summary>
		/// <returns>A <see cref="T:System.IO.Stream" /> containing audio data or null if the Clipboard does not contain any data in the <see cref="F:System.Windows.Forms.DataFormats.WaveAudio" /> format.</returns>
		/// <exception cref="T:System.Runtime.InteropServices.ExternalException">The Clipboard could not be cleared. This typically occurs when the Clipboard is being used by another process.</exception>
		/// <exception cref="T:System.Threading.ThreadStateException">The current thread is not in single-threaded apartment (STA) mode. Add the <see cref="T:System.STAThreadAttribute" /> to your application's Main method.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x060005B1 RID: 1457 RVA: 0x00017F14 File Offset: 0x00016114
		public static Stream GetAudioStream()
		{
			IDataObject dataObject = Clipboard.GetDataObject();
			if (dataObject == null)
			{
				return null;
			}
			return (Stream)dataObject.GetData(DataFormats.WaveAudio, true);
		}

		/// <summary>Retrieves data from the Clipboard in the specified format.</summary>
		/// <returns>An <see cref="T:System.Object" /> representing the Clipboard data or null if the Clipboard does not contain any data that is in the specified <paramref name="format" /> or can be converted to that format.</returns>
		/// <param name="format">The format of the data to retrieve. See <see cref="T:System.Windows.Forms.DataFormats" /> for predefined formats.</param>
		/// <exception cref="T:System.Runtime.InteropServices.ExternalException">The Clipboard could not be cleared. This typically occurs when the Clipboard is being used by another process.</exception>
		/// <exception cref="T:System.Threading.ThreadStateException">The current thread is not in single-threaded apartment (STA) mode. Add the <see cref="T:System.STAThreadAttribute" /> to your application's Main method.</exception>
		// Token: 0x060005B2 RID: 1458 RVA: 0x00017F40 File Offset: 0x00016140
		public static object GetData(string format)
		{
			IDataObject dataObject = Clipboard.GetDataObject();
			if (dataObject == null)
			{
				return null;
			}
			return dataObject.GetData(format, true);
		}

		/// <summary>Retrieves the data that is currently on the system Clipboard.</summary>
		/// <returns>An <see cref="T:System.Windows.Forms.IDataObject" /> that represents the data currently on the Clipboard, or null if there is no data on the Clipboard.</returns>
		/// <exception cref="T:System.Runtime.InteropServices.ExternalException">Data could not be retrieved from the Clipboard. This typically occurs when the Clipboard is being used by another process.</exception>
		/// <exception cref="T:System.Threading.ThreadStateException">The current thread is not in single-threaded apartment (STA) mode and the <see cref="P:System.Windows.Forms.Application.MessageLoop" /> property value is true. Add the <see cref="T:System.STAThreadAttribute" /> to your application's Main method. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x060005B3 RID: 1459 RVA: 0x00017F64 File Offset: 0x00016164
		public static IDataObject GetDataObject()
		{
			return Clipboard.GetDataObject(false);
		}

		/// <summary>Retrieves a collection of file names from the Clipboard. </summary>
		/// <returns>A <see cref="T:System.Collections.Specialized.StringCollection" /> containing file names or null if the Clipboard does not contain any data that is in the <see cref="F:System.Windows.Forms.DataFormats.FileDrop" /> format or can be converted to that format.</returns>
		/// <exception cref="T:System.Runtime.InteropServices.ExternalException">The Clipboard could not be cleared. This typically occurs when the Clipboard is being used by another process.</exception>
		/// <exception cref="T:System.Threading.ThreadStateException">The current thread is not in single-threaded apartment (STA) mode. Add the <see cref="T:System.STAThreadAttribute" /> to your application's Main method.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x060005B4 RID: 1460 RVA: 0x00017F6C File Offset: 0x0001616C
		public static StringCollection GetFileDropList()
		{
			IDataObject dataObject = Clipboard.GetDataObject();
			if (dataObject == null)
			{
				return null;
			}
			return (StringCollection)dataObject.GetData(DataFormats.FileDrop, true);
		}

		/// <summary>Retrieves an image from the Clipboard.</summary>
		/// <returns>An <see cref="T:System.Drawing.Image" /> representing the Clipboard image data or null if the Clipboard does not contain any data that is in the <see cref="F:System.Windows.Forms.DataFormats.Bitmap" /> format or can be converted to that format.</returns>
		/// <exception cref="T:System.Runtime.InteropServices.ExternalException">The Clipboard could not be cleared. This typically occurs when the Clipboard is being used by another process.</exception>
		/// <exception cref="T:System.Threading.ThreadStateException">The current thread is not in single-threaded apartment (STA) mode. Add the <see cref="T:System.STAThreadAttribute" /> to your application's Main method.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x060005B5 RID: 1461 RVA: 0x00017F98 File Offset: 0x00016198
		public static Image GetImage()
		{
			IDataObject dataObject = Clipboard.GetDataObject();
			if (dataObject == null)
			{
				return null;
			}
			return (Image)dataObject.GetData(DataFormats.Bitmap, true);
		}

		/// <summary>Retrieves text data from the Clipboard in the <see cref="F:System.Windows.Forms.TextDataFormat.Text" /> or <see cref="F:System.Windows.Forms.TextDataFormat.UnicodeText" /> format, depending on the operating system.</summary>
		/// <returns>The Clipboard text data or <see cref="F:System.String.Empty" /> if the Clipboard does not contain data in the <see cref="F:System.Windows.Forms.TextDataFormat.Text" /> or <see cref="F:System.Windows.Forms.TextDataFormat.UnicodeText" /> format, depending on the operating system.</returns>
		/// <exception cref="T:System.Runtime.InteropServices.ExternalException">The Clipboard could not be cleared. This typically occurs when the Clipboard is being used by another process.</exception>
		/// <exception cref="T:System.Threading.ThreadStateException">The current thread is not in single-threaded apartment (STA) mode. Add the <see cref="T:System.STAThreadAttribute" /> to your application's Main method.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x060005B6 RID: 1462 RVA: 0x00017FC4 File Offset: 0x000161C4
		public static string GetText()
		{
			return Clipboard.GetText(TextDataFormat.UnicodeText);
		}

		/// <summary>Retrieves text data from the Clipboard in the format indicated by the specified <see cref="T:System.Windows.Forms.TextDataFormat" /> value.</summary>
		/// <returns>The Clipboard text data or <see cref="F:System.String.Empty" /> if the Clipboard does not contain data in the specified format.</returns>
		/// <param name="format">One of the <see cref="T:System.Windows.Forms.TextDataFormat" /> values.</param>
		/// <exception cref="T:System.Runtime.InteropServices.ExternalException">The Clipboard could not be cleared. This typically occurs when the Clipboard is being used by another process.</exception>
		/// <exception cref="T:System.Threading.ThreadStateException">The current thread is not in single-threaded apartment (STA) mode. Add the <see cref="T:System.STAThreadAttribute" /> to your application's Main method.</exception>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">
		///   <paramref name="format" /> is not a valid <see cref="T:System.Windows.Forms.TextDataFormat" /> value.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x060005B7 RID: 1463 RVA: 0x00017FCC File Offset: 0x000161CC
		public static string GetText(TextDataFormat format)
		{
			if (!Enum.IsDefined(typeof(TextDataFormat), format))
			{
				throw new InvalidEnumArgumentException(string.Format("Enum argument value '{0}' is not valid for TextDataFormat", format));
			}
			IDataObject dataObject = Clipboard.GetDataObject();
			if (dataObject == null)
			{
				return string.Empty;
			}
			string text;
			switch (format)
			{
			default:
				text = (string)dataObject.GetData(DataFormats.Text, true);
				break;
			case TextDataFormat.UnicodeText:
				text = (string)dataObject.GetData(DataFormats.UnicodeText, true);
				break;
			case TextDataFormat.Rtf:
				text = (string)dataObject.GetData(DataFormats.Rtf, true);
				break;
			case TextDataFormat.Html:
				text = (string)dataObject.GetData(DataFormats.Html, true);
				break;
			case TextDataFormat.CommaSeparatedValue:
				text = (string)dataObject.GetData(DataFormats.CommaSeparatedValue, true);
				break;
			}
			return (text != null) ? text : string.Empty;
		}

		/// <summary>Clears the Clipboard and then adds a <see cref="T:System.Byte" /> array in the <see cref="F:System.Windows.Forms.DataFormats.WaveAudio" /> format after converting it to a <see cref="T:System.IO.Stream" />.</summary>
		/// <param name="audioBytes">A <see cref="T:System.Byte" /> array containing the audio data.</param>
		/// <exception cref="T:System.Runtime.InteropServices.ExternalException">The Clipboard could not be cleared. This typically occurs when the Clipboard is being used by another process.</exception>
		/// <exception cref="T:System.Threading.ThreadStateException">The current thread is not in single-threaded apartment (STA) mode. Add the <see cref="T:System.STAThreadAttribute" /> to your application's Main method.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="audioBytes" /> is null.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060005B8 RID: 1464 RVA: 0x000180C0 File Offset: 0x000162C0
		public static void SetAudio(byte[] audioBytes)
		{
			if (audioBytes == null)
			{
				throw new ArgumentNullException("audioBytes");
			}
			MemoryStream memoryStream = new MemoryStream(audioBytes);
			Clipboard.SetAudio(memoryStream);
		}

		/// <summary>Clears the Clipboard and then adds a <see cref="T:System.IO.Stream" /> in the <see cref="F:System.Windows.Forms.DataFormats.WaveAudio" /> format.</summary>
		/// <param name="audioStream">A <see cref="T:System.IO.Stream" /> containing the audio data.</param>
		/// <exception cref="T:System.Runtime.InteropServices.ExternalException">The Clipboard could not be cleared. This typically occurs when the Clipboard is being used by another process.</exception>
		/// <exception cref="T:System.Threading.ThreadStateException">The current thread is not in single-threaded apartment (STA) mode. Add the <see cref="T:System.STAThreadAttribute" /> to your application's Main method.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="audioStream" /> is null.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060005B9 RID: 1465 RVA: 0x000180EC File Offset: 0x000162EC
		public static void SetAudio(Stream audioStream)
		{
			if (audioStream == null)
			{
				throw new ArgumentNullException("audioStream");
			}
			Clipboard.SetData(DataFormats.WaveAudio, audioStream);
		}

		/// <summary>Clears the Clipboard and then adds data in the specified format.</summary>
		/// <param name="format">The format of the data to set. See <see cref="T:System.Windows.Forms.DataFormats" /> for predefined formats.</param>
		/// <param name="data">An <see cref="T:System.Object" /> representing the data to add.</param>
		/// <exception cref="T:System.Runtime.InteropServices.ExternalException">The Clipboard could not be cleared. This typically occurs when the Clipboard is being used by another process.</exception>
		/// <exception cref="T:System.Threading.ThreadStateException">The current thread is not in single-threaded apartment (STA) mode. Add the <see cref="T:System.STAThreadAttribute" /> to your application's Main method.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="data" /> is null.</exception>
		// Token: 0x060005BA RID: 1466 RVA: 0x0001810C File Offset: 0x0001630C
		public static void SetData(string format, object data)
		{
			if (data == null)
			{
				throw new ArgumentNullException("data");
			}
			DataObject dataObject = new DataObject(format, data);
			Clipboard.SetDataObject(dataObject);
		}

		/// <summary>Clears the Clipboard and then places nonpersistent data on it.</summary>
		/// <param name="data">The data to place on the Clipboard. </param>
		/// <exception cref="T:System.Runtime.InteropServices.ExternalException">Data could not be placed on the Clipboard. This typically occurs when the Clipboard is being used by another process.</exception>
		/// <exception cref="T:System.Threading.ThreadStateException">The current thread is not in single-threaded apartment (STA) mode. Add the <see cref="T:System.STAThreadAttribute" /> to your application's Main method.</exception>
		/// <exception cref="T:System.ArgumentNullException">The value of <paramref name="data" /> is null. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060005BB RID: 1467 RVA: 0x00018138 File Offset: 0x00016338
		public static void SetDataObject(object data)
		{
			Clipboard.SetDataObject(data, false);
		}

		/// <summary>Clears the Clipboard and then places data on it and specifies whether the data should remain after the application exits.</summary>
		/// <param name="data">The data to place on the Clipboard. </param>
		/// <param name="copy">true if you want data to remain on the Clipboard after this application exits; otherwise, false. </param>
		/// <exception cref="T:System.Runtime.InteropServices.ExternalException">Data could not be placed on the Clipboard. This typically occurs when the Clipboard is being used by another process.</exception>
		/// <exception cref="T:System.Threading.ThreadStateException">The current thread is not in single-threaded apartment (STA) mode. Add the <see cref="T:System.STAThreadAttribute" /> to your application's Main method.</exception>
		/// <exception cref="T:System.ArgumentNullException">The value of <paramref name="data" /> is null. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060005BC RID: 1468 RVA: 0x00018144 File Offset: 0x00016344
		public static void SetDataObject(object data, bool copy)
		{
			Clipboard.SetDataObject(data, copy, 10, 100);
		}

		// Token: 0x060005BD RID: 1469 RVA: 0x00018154 File Offset: 0x00016354
		internal static void SetDataObjectImpl(object data, bool copy)
		{
			XplatUI.ObjectToClipboard objectToClipboard = new XplatUI.ObjectToClipboard(Clipboard.ConvertToClipboardData);
			IntPtr intPtr = XplatUI.ClipboardOpen(false);
			XplatUI.ClipboardStore(intPtr, null, 0, null);
			int num = -1;
			if (data is IDataObject)
			{
				IDataObject dataObject = data as IDataObject;
				string[] formats = dataObject.GetFormats();
				for (int i = 0; i < formats.Length; i++)
				{
					DataFormats.Format format = DataFormats.GetFormat(formats[i]);
					if (format != null && format.Name != DataFormats.StringFormat)
					{
						num = format.Id;
					}
					object data2 = dataObject.GetData(formats[i]);
					if (Clipboard.IsDataSerializable(data2))
					{
						format.is_serializable = true;
					}
					XplatUI.ClipboardStore(intPtr, data2, num, objectToClipboard);
				}
			}
			else
			{
				DataFormats.Format format = DataFormats.Format.Find(data.GetType().FullName);
				if (format != null && format.Name != DataFormats.StringFormat)
				{
					num = format.Id;
				}
				XplatUI.ClipboardStore(intPtr, data, num, objectToClipboard);
			}
			XplatUI.ClipboardClose(intPtr);
		}

		// Token: 0x060005BE RID: 1470 RVA: 0x00018254 File Offset: 0x00016454
		private static bool IsDataSerializable(object obj)
		{
			if (obj is ISerializable)
			{
				return true;
			}
			AttributeCollection attributes = TypeDescriptor.GetAttributes(obj);
			return attributes[typeof(SerializableAttribute)] != null;
		}

		/// <summary>Clears the Clipboard and then attempts to place data on it the specified number of times and with the specified delay between attempts, optionally leaving the data on the Clipboard after the application exits.</summary>
		/// <param name="data">The data to place on the Clipboard.</param>
		/// <param name="copy">true if you want data to remain on the Clipboard after this application exits; otherwise, false.</param>
		/// <param name="retryTimes">The number of times to attempt placing the data on the Clipboard.</param>
		/// <param name="retryDelay">The number of milliseconds to pause between attempts. </param>
		/// <exception cref="T:System.Threading.ThreadStateException">The current thread is not in single-threaded apartment (STA) mode. Add the <see cref="T:System.STAThreadAttribute" /> to your application's Main method. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="data" /> is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="retryTimes" /> is less than zero.-or-<paramref name="retryDelay" /> is less than zero.</exception>
		/// <exception cref="T:System.Runtime.InteropServices.ExternalException">Data could not be placed on the Clipboard. This typically occurs when the Clipboard is being used by another process.</exception>
		// Token: 0x060005BF RID: 1471 RVA: 0x0001828C File Offset: 0x0001648C
		public static void SetDataObject(object data, bool copy, int retryTimes, int retryDelay)
		{
			if (data == null)
			{
				throw new ArgumentNullException("data");
			}
			if (retryTimes < 0)
			{
				throw new ArgumentOutOfRangeException("retryTimes");
			}
			if (retryDelay < 0)
			{
				throw new ArgumentOutOfRangeException("retryDelay");
			}
			bool flag = true;
			do
			{
				flag = false;
				retryTimes--;
				try
				{
					Clipboard.SetDataObjectImpl(data, copy);
				}
				catch (ExternalException)
				{
					if (retryTimes <= 0)
					{
						throw;
					}
					flag = true;
					Thread.Sleep(retryDelay);
				}
			}
			while (flag && retryTimes > 0);
		}

		/// <summary>Clears the Clipboard and then adds a collection of file names in the <see cref="F:System.Windows.Forms.DataFormats.FileDrop" /> format.</summary>
		/// <param name="filePaths">A <see cref="T:System.Collections.Specialized.StringCollection" /> containing the file names.</param>
		/// <exception cref="T:System.Runtime.InteropServices.ExternalException">The Clipboard could not be cleared. This typically occurs when the Clipboard is being used by another process.</exception>
		/// <exception cref="T:System.Threading.ThreadStateException">The current thread is not in single-threaded apartment (STA) mode. Add the <see cref="T:System.STAThreadAttribute" /> to your application's Main method.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="filePaths" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="filePaths" /> does not contain any strings.-or-At least one of the strings in <paramref name="filePaths" /> is <see cref="F:System.String.Empty" />, contains only white space, contains one or more invalid characters as defined by <see cref="F:System.IO.Path.InvalidPathChars" />, is null, contains a colon (:), or exceeds the system-defined maximum length.See the <see cref="P:System.Exception.InnerException" /> property of the <see cref="T:System.ArgumentException" /> for more information.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060005C0 RID: 1472 RVA: 0x00018324 File Offset: 0x00016524
		[MonoInternalNote("Needs additional checks for valid paths, see MSDN")]
		public static void SetFileDropList(StringCollection filePaths)
		{
			if (filePaths == null)
			{
				throw new ArgumentNullException("filePaths");
			}
			Clipboard.SetData(DataFormats.FileDrop, filePaths);
		}

		/// <summary>Clears the Clipboard and then adds an <see cref="T:System.Drawing.Image" /> in the <see cref="F:System.Windows.Forms.DataFormats.Bitmap" /> format.</summary>
		/// <param name="image">The <see cref="T:System.Drawing.Image" /> to add to the Clipboard.</param>
		/// <exception cref="T:System.Runtime.InteropServices.ExternalException">The Clipboard could not be cleared. This typically occurs when the Clipboard is being used by another process.</exception>
		/// <exception cref="T:System.Threading.ThreadStateException">The current thread is not in single-threaded apartment (STA) mode. Add the <see cref="T:System.STAThreadAttribute" /> to your application's Main method.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="image" /> is null.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060005C1 RID: 1473 RVA: 0x00018344 File Offset: 0x00016544
		public static void SetImage(Image image)
		{
			if (image == null)
			{
				throw new ArgumentNullException("image");
			}
			Clipboard.SetData(DataFormats.Bitmap, image);
		}

		/// <summary>Clears the Clipboard and then adds text data in the <see cref="F:System.Windows.Forms.TextDataFormat.Text" /> or <see cref="F:System.Windows.Forms.TextDataFormat.UnicodeText" /> format, depending on the operating system.</summary>
		/// <param name="text">The text to add to the Clipboard.</param>
		/// <exception cref="T:System.Runtime.InteropServices.ExternalException">The Clipboard could not be cleared. This typically occurs when the Clipboard is being used by another process.</exception>
		/// <exception cref="T:System.Threading.ThreadStateException">The current thread is not in single-threaded apartment (STA) mode. Add the <see cref="T:System.STAThreadAttribute" /> to your application's Main method.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="text" /> is null or <see cref="F:System.String.Empty" />.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060005C2 RID: 1474 RVA: 0x00018364 File Offset: 0x00016564
		public static void SetText(string text)
		{
			if (string.IsNullOrEmpty(text))
			{
				throw new ArgumentNullException("text");
			}
			Clipboard.SetData(DataFormats.UnicodeText, text);
		}

		/// <summary>Clears the Clipboard and then adds text data in the format indicated by the specified <see cref="T:System.Windows.Forms.TextDataFormat" /> value.</summary>
		/// <param name="text">The text to add to the Clipboard.</param>
		/// <param name="format">One of the <see cref="T:System.Windows.Forms.TextDataFormat" /> values.</param>
		/// <exception cref="T:System.Runtime.InteropServices.ExternalException">The Clipboard could not be cleared. This typically occurs when the Clipboard is being used by another process.</exception>
		/// <exception cref="T:System.Threading.ThreadStateException">The current thread is not in single-threaded apartment (STA) mode. Add the <see cref="T:System.STAThreadAttribute" /> to your application's Main method.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="text" /> is null or <see cref="F:System.String.Empty" />.</exception>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">
		///   <paramref name="format" /> is not a valid <see cref="T:System.Windows.Forms.TextDataFormat" /> value.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060005C3 RID: 1475 RVA: 0x00018388 File Offset: 0x00016588
		public static void SetText(string text, TextDataFormat format)
		{
			if (string.IsNullOrEmpty(text))
			{
				throw new ArgumentNullException("text");
			}
			if (!Enum.IsDefined(typeof(TextDataFormat), format))
			{
				throw new InvalidEnumArgumentException(string.Format("Enum argument value '{0}' is not valid for TextDataFormat", format));
			}
			switch (format)
			{
			case TextDataFormat.Text:
				Clipboard.SetData(DataFormats.Text, text);
				break;
			case TextDataFormat.UnicodeText:
				Clipboard.SetData(DataFormats.UnicodeText, text);
				break;
			case TextDataFormat.Rtf:
				Clipboard.SetData(DataFormats.Rtf, text);
				break;
			case TextDataFormat.Html:
				Clipboard.SetData(DataFormats.Html, text);
				break;
			case TextDataFormat.CommaSeparatedValue:
				Clipboard.SetData(DataFormats.CommaSeparatedValue, text);
				break;
			}
		}

		// Token: 0x060005C4 RID: 1476 RVA: 0x0001844C File Offset: 0x0001664C
		internal static IDataObject GetDataObject(bool primary_selection)
		{
			XplatUI.ClipboardToObject clipboardToObject = new XplatUI.ClipboardToObject(Clipboard.ConvertFromClipboardData);
			IntPtr intPtr = XplatUI.ClipboardOpen(primary_selection);
			int[] array = XplatUI.ClipboardAvailableFormats(intPtr);
			if (array == null)
			{
				return null;
			}
			DataObject dataObject = new DataObject();
			for (int i = 0; i < array.Length; i++)
			{
				DataFormats.Format format = DataFormats.GetFormat(array[i]);
				if (format != null)
				{
					object obj = XplatUI.ClipboardRetrieve(intPtr, array[i], clipboardToObject);
					if (obj != null)
					{
						dataObject.SetData(format.Name, obj);
						if (format.Name == DataFormats.Dib)
						{
							dataObject.SetData(DataFormats.Bitmap, obj);
						}
					}
				}
			}
			XplatUI.ClipboardClose(intPtr);
			return dataObject;
		}

		// Token: 0x060005C5 RID: 1477 RVA: 0x000184F8 File Offset: 0x000166F8
		internal static bool ClipboardContainsFormat(params string[] formats)
		{
			IntPtr intPtr = XplatUI.ClipboardOpen(false);
			int[] array = XplatUI.ClipboardAvailableFormats(intPtr);
			if (array == null)
			{
				return false;
			}
			foreach (int num in array)
			{
				DataFormats.Format format = DataFormats.GetFormat(num);
				if (format != null && formats.Contains(format.Name))
				{
					return true;
				}
			}
			return false;
		}
	}
}
