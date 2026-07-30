using System;
using System.Globalization;
using System.IO;
using System.Text;

namespace System.CodeDom.Compiler
{
	/// <summary>Provides a text writer that can indent new lines by a tab string token.</summary>
	// Token: 0x020007B6 RID: 1974
	public class IndentedTextWriter : TextWriter
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.Compiler.IndentedTextWriter" /> class using the specified text writer and default tab string.</summary>
		/// <param name="writer">The <see cref="T:System.IO.TextWriter" /> to use for output. </param>
		// Token: 0x06003F90 RID: 16272 RVA: 0x000DFD80 File Offset: 0x000DDF80
		public IndentedTextWriter(TextWriter writer)
			: this(writer, "    ")
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.Compiler.IndentedTextWriter" /> class using the specified text writer and tab string.</summary>
		/// <param name="writer">The <see cref="T:System.IO.TextWriter" /> to use for output. </param>
		/// <param name="tabString">The tab string to use for indentation. </param>
		// Token: 0x06003F91 RID: 16273 RVA: 0x000DFD8E File Offset: 0x000DDF8E
		public IndentedTextWriter(TextWriter writer, string tabString)
			: base(CultureInfo.InvariantCulture)
		{
			this._writer = writer;
			this._tabString = tabString;
			this._indentLevel = 0;
			this._tabsPending = false;
		}

		/// <summary>Gets the encoding for the text writer to use.</summary>
		/// <returns>An <see cref="T:System.Text.Encoding" /> that indicates the encoding for the text writer to use.</returns>
		// Token: 0x17000F50 RID: 3920
		// (get) Token: 0x06003F92 RID: 16274 RVA: 0x000DFDB7 File Offset: 0x000DDFB7
		public override Encoding Encoding
		{
			get
			{
				return this._writer.Encoding;
			}
		}

		/// <summary>Gets or sets the new line character to use.</summary>
		/// <returns>The new line character to use.</returns>
		// Token: 0x17000F51 RID: 3921
		// (get) Token: 0x06003F93 RID: 16275 RVA: 0x000DFDC4 File Offset: 0x000DDFC4
		// (set) Token: 0x06003F94 RID: 16276 RVA: 0x000DFDD1 File Offset: 0x000DDFD1
		public override string NewLine
		{
			get
			{
				return this._writer.NewLine;
			}
			set
			{
				this._writer.NewLine = value;
			}
		}

		/// <summary>Gets or sets the number of spaces to indent.</summary>
		/// <returns>The number of spaces to indent.</returns>
		// Token: 0x17000F52 RID: 3922
		// (get) Token: 0x06003F95 RID: 16277 RVA: 0x000DFDDF File Offset: 0x000DDFDF
		// (set) Token: 0x06003F96 RID: 16278 RVA: 0x000DFDE7 File Offset: 0x000DDFE7
		public int Indent
		{
			get
			{
				return this._indentLevel;
			}
			set
			{
				this._indentLevel = Math.Max(value, 0);
			}
		}

		/// <summary>Gets the <see cref="T:System.IO.TextWriter" /> to use.</summary>
		/// <returns>The <see cref="T:System.IO.TextWriter" /> to use.</returns>
		// Token: 0x17000F53 RID: 3923
		// (get) Token: 0x06003F97 RID: 16279 RVA: 0x000DFDF6 File Offset: 0x000DDFF6
		public TextWriter InnerWriter
		{
			get
			{
				return this._writer;
			}
		}

		/// <summary>Closes the document being written to.</summary>
		// Token: 0x06003F98 RID: 16280 RVA: 0x000DFDFE File Offset: 0x000DDFFE
		public override void Close()
		{
			this._writer.Close();
		}

		/// <summary>Flushes the stream.</summary>
		// Token: 0x06003F99 RID: 16281 RVA: 0x000DFE0B File Offset: 0x000DE00B
		public override void Flush()
		{
			this._writer.Flush();
		}

		/// <summary>Outputs the tab string once for each level of indentation according to the <see cref="P:System.CodeDom.Compiler.IndentedTextWriter.Indent" /> property.</summary>
		// Token: 0x06003F9A RID: 16282 RVA: 0x000DFE18 File Offset: 0x000DE018
		protected virtual void OutputTabs()
		{
			if (this._tabsPending)
			{
				for (int i = 0; i < this._indentLevel; i++)
				{
					this._writer.Write(this._tabString);
				}
				this._tabsPending = false;
			}
		}

		/// <summary>Writes the specified string to the text stream.</summary>
		/// <param name="s">The string to write. </param>
		// Token: 0x06003F9B RID: 16283 RVA: 0x000DFE56 File Offset: 0x000DE056
		public override void Write(string s)
		{
			this.OutputTabs();
			this._writer.Write(s);
		}

		/// <summary>Writes the text representation of a Boolean value to the text stream.</summary>
		/// <param name="value">The Boolean value to write. </param>
		// Token: 0x06003F9C RID: 16284 RVA: 0x000DFE6A File Offset: 0x000DE06A
		public override void Write(bool value)
		{
			this.OutputTabs();
			this._writer.Write(value);
		}

		/// <summary>Writes a character to the text stream.</summary>
		/// <param name="value">The character to write. </param>
		// Token: 0x06003F9D RID: 16285 RVA: 0x000DFE7E File Offset: 0x000DE07E
		public override void Write(char value)
		{
			this.OutputTabs();
			this._writer.Write(value);
		}

		/// <summary>Writes a character array to the text stream.</summary>
		/// <param name="buffer">The character array to write. </param>
		// Token: 0x06003F9E RID: 16286 RVA: 0x000DFE92 File Offset: 0x000DE092
		public override void Write(char[] buffer)
		{
			this.OutputTabs();
			this._writer.Write(buffer);
		}

		/// <summary>Writes a subarray of characters to the text stream.</summary>
		/// <param name="buffer">The character array to write data from. </param>
		/// <param name="index">Starting index in the buffer. </param>
		/// <param name="count">The number of characters to write. </param>
		// Token: 0x06003F9F RID: 16287 RVA: 0x000DFEA6 File Offset: 0x000DE0A6
		public override void Write(char[] buffer, int index, int count)
		{
			this.OutputTabs();
			this._writer.Write(buffer, index, count);
		}

		/// <summary>Writes the text representation of a Double to the text stream.</summary>
		/// <param name="value">The double to write. </param>
		// Token: 0x06003FA0 RID: 16288 RVA: 0x000DFEBC File Offset: 0x000DE0BC
		public override void Write(double value)
		{
			this.OutputTabs();
			this._writer.Write(value);
		}

		/// <summary>Writes the text representation of a Single to the text stream.</summary>
		/// <param name="value">The single to write. </param>
		// Token: 0x06003FA1 RID: 16289 RVA: 0x000DFED0 File Offset: 0x000DE0D0
		public override void Write(float value)
		{
			this.OutputTabs();
			this._writer.Write(value);
		}

		/// <summary>Writes the text representation of an integer to the text stream.</summary>
		/// <param name="value">The integer to write. </param>
		// Token: 0x06003FA2 RID: 16290 RVA: 0x000DFEE4 File Offset: 0x000DE0E4
		public override void Write(int value)
		{
			this.OutputTabs();
			this._writer.Write(value);
		}

		/// <summary>Writes the text representation of an 8-byte integer to the text stream.</summary>
		/// <param name="value">The 8-byte integer to write. </param>
		// Token: 0x06003FA3 RID: 16291 RVA: 0x000DFEF8 File Offset: 0x000DE0F8
		public override void Write(long value)
		{
			this.OutputTabs();
			this._writer.Write(value);
		}

		/// <summary>Writes the text representation of an object to the text stream.</summary>
		/// <param name="value">The object to write. </param>
		// Token: 0x06003FA4 RID: 16292 RVA: 0x000DFF0C File Offset: 0x000DE10C
		public override void Write(object value)
		{
			this.OutputTabs();
			this._writer.Write(value);
		}

		/// <summary>Writes out a formatted string, using the same semantics as specified.</summary>
		/// <param name="format">The formatting string. </param>
		/// <param name="arg0">The object to write into the formatted string. </param>
		// Token: 0x06003FA5 RID: 16293 RVA: 0x000DFF20 File Offset: 0x000DE120
		public override void Write(string format, object arg0)
		{
			this.OutputTabs();
			this._writer.Write(format, arg0);
		}

		/// <summary>Writes out a formatted string, using the same semantics as specified.</summary>
		/// <param name="format">The formatting string to use. </param>
		/// <param name="arg0">The first object to write into the formatted string. </param>
		/// <param name="arg1">The second object to write into the formatted string. </param>
		// Token: 0x06003FA6 RID: 16294 RVA: 0x000DFF35 File Offset: 0x000DE135
		public override void Write(string format, object arg0, object arg1)
		{
			this.OutputTabs();
			this._writer.Write(format, arg0, arg1);
		}

		/// <summary>Writes out a formatted string, using the same semantics as specified.</summary>
		/// <param name="format">The formatting string to use. </param>
		/// <param name="arg">The argument array to output. </param>
		// Token: 0x06003FA7 RID: 16295 RVA: 0x000DFF4B File Offset: 0x000DE14B
		public override void Write(string format, params object[] arg)
		{
			this.OutputTabs();
			this._writer.Write(format, arg);
		}

		/// <summary>Writes the specified string to a line without tabs.</summary>
		/// <param name="s">The string to write. </param>
		// Token: 0x06003FA8 RID: 16296 RVA: 0x000DFF60 File Offset: 0x000DE160
		public void WriteLineNoTabs(string s)
		{
			this._writer.WriteLine(s);
		}

		/// <summary>Writes the specified string, followed by a line terminator, to the text stream.</summary>
		/// <param name="s">The string to write. </param>
		// Token: 0x06003FA9 RID: 16297 RVA: 0x000DFF6E File Offset: 0x000DE16E
		public override void WriteLine(string s)
		{
			this.OutputTabs();
			this._writer.WriteLine(s);
			this._tabsPending = true;
		}

		/// <summary>Writes a line terminator.</summary>
		// Token: 0x06003FAA RID: 16298 RVA: 0x000DFF89 File Offset: 0x000DE189
		public override void WriteLine()
		{
			this.OutputTabs();
			this._writer.WriteLine();
			this._tabsPending = true;
		}

		/// <summary>Writes the text representation of a Boolean, followed by a line terminator, to the text stream.</summary>
		/// <param name="value">The Boolean to write. </param>
		// Token: 0x06003FAB RID: 16299 RVA: 0x000DFFA3 File Offset: 0x000DE1A3
		public override void WriteLine(bool value)
		{
			this.OutputTabs();
			this._writer.WriteLine(value);
			this._tabsPending = true;
		}

		/// <summary>Writes a character, followed by a line terminator, to the text stream.</summary>
		/// <param name="value">The character to write. </param>
		// Token: 0x06003FAC RID: 16300 RVA: 0x000DFFBE File Offset: 0x000DE1BE
		public override void WriteLine(char value)
		{
			this.OutputTabs();
			this._writer.WriteLine(value);
			this._tabsPending = true;
		}

		/// <summary>Writes a character array, followed by a line terminator, to the text stream.</summary>
		/// <param name="buffer">The character array to write. </param>
		// Token: 0x06003FAD RID: 16301 RVA: 0x000DFFD9 File Offset: 0x000DE1D9
		public override void WriteLine(char[] buffer)
		{
			this.OutputTabs();
			this._writer.WriteLine(buffer);
			this._tabsPending = true;
		}

		/// <summary>Writes a subarray of characters, followed by a line terminator, to the text stream.</summary>
		/// <param name="buffer">The character array to write data from. </param>
		/// <param name="index">Starting index in the buffer. </param>
		/// <param name="count">The number of characters to write. </param>
		// Token: 0x06003FAE RID: 16302 RVA: 0x000DFFF4 File Offset: 0x000DE1F4
		public override void WriteLine(char[] buffer, int index, int count)
		{
			this.OutputTabs();
			this._writer.WriteLine(buffer, index, count);
			this._tabsPending = true;
		}

		/// <summary>Writes the text representation of a Double, followed by a line terminator, to the text stream.</summary>
		/// <param name="value">The double to write. </param>
		// Token: 0x06003FAF RID: 16303 RVA: 0x000E0011 File Offset: 0x000DE211
		public override void WriteLine(double value)
		{
			this.OutputTabs();
			this._writer.WriteLine(value);
			this._tabsPending = true;
		}

		/// <summary>Writes the text representation of a Single, followed by a line terminator, to the text stream.</summary>
		/// <param name="value">The single to write. </param>
		// Token: 0x06003FB0 RID: 16304 RVA: 0x000E002C File Offset: 0x000DE22C
		public override void WriteLine(float value)
		{
			this.OutputTabs();
			this._writer.WriteLine(value);
			this._tabsPending = true;
		}

		/// <summary>Writes the text representation of an integer, followed by a line terminator, to the text stream.</summary>
		/// <param name="value">The integer to write. </param>
		// Token: 0x06003FB1 RID: 16305 RVA: 0x000E0047 File Offset: 0x000DE247
		public override void WriteLine(int value)
		{
			this.OutputTabs();
			this._writer.WriteLine(value);
			this._tabsPending = true;
		}

		/// <summary>Writes the text representation of an 8-byte integer, followed by a line terminator, to the text stream.</summary>
		/// <param name="value">The 8-byte integer to write. </param>
		// Token: 0x06003FB2 RID: 16306 RVA: 0x000E0062 File Offset: 0x000DE262
		public override void WriteLine(long value)
		{
			this.OutputTabs();
			this._writer.WriteLine(value);
			this._tabsPending = true;
		}

		/// <summary>Writes the text representation of an object, followed by a line terminator, to the text stream.</summary>
		/// <param name="value">The object to write. </param>
		// Token: 0x06003FB3 RID: 16307 RVA: 0x000E007D File Offset: 0x000DE27D
		public override void WriteLine(object value)
		{
			this.OutputTabs();
			this._writer.WriteLine(value);
			this._tabsPending = true;
		}

		/// <summary>Writes out a formatted string, followed by a line terminator, using the same semantics as specified.</summary>
		/// <param name="format">The formatting string. </param>
		/// <param name="arg0">The object to write into the formatted string. </param>
		// Token: 0x06003FB4 RID: 16308 RVA: 0x000E0098 File Offset: 0x000DE298
		public override void WriteLine(string format, object arg0)
		{
			this.OutputTabs();
			this._writer.WriteLine(format, arg0);
			this._tabsPending = true;
		}

		/// <summary>Writes out a formatted string, followed by a line terminator, using the same semantics as specified.</summary>
		/// <param name="format">The formatting string to use. </param>
		/// <param name="arg0">The first object to write into the formatted string. </param>
		/// <param name="arg1">The second object to write into the formatted string. </param>
		// Token: 0x06003FB5 RID: 16309 RVA: 0x000E00B4 File Offset: 0x000DE2B4
		public override void WriteLine(string format, object arg0, object arg1)
		{
			this.OutputTabs();
			this._writer.WriteLine(format, arg0, arg1);
			this._tabsPending = true;
		}

		/// <summary>Writes out a formatted string, followed by a line terminator, using the same semantics as specified.</summary>
		/// <param name="format">The formatting string to use. </param>
		/// <param name="arg">The argument array to output. </param>
		// Token: 0x06003FB6 RID: 16310 RVA: 0x000E00D1 File Offset: 0x000DE2D1
		public override void WriteLine(string format, params object[] arg)
		{
			this.OutputTabs();
			this._writer.WriteLine(format, arg);
			this._tabsPending = true;
		}

		/// <summary>Writes the text representation of a UInt32, followed by a line terminator, to the text stream.</summary>
		/// <param name="value">A UInt32 to output. </param>
		// Token: 0x06003FB7 RID: 16311 RVA: 0x000E00ED File Offset: 0x000DE2ED
		[CLSCompliant(false)]
		public override void WriteLine(uint value)
		{
			this.OutputTabs();
			this._writer.WriteLine(value);
			this._tabsPending = true;
		}

		// Token: 0x04002E77 RID: 11895
		private readonly TextWriter _writer;

		// Token: 0x04002E78 RID: 11896
		private readonly string _tabString;

		// Token: 0x04002E79 RID: 11897
		private int _indentLevel;

		// Token: 0x04002E7A RID: 11898
		private bool _tabsPending;

		/// <summary>Specifies the default tab string. This field is constant. </summary>
		// Token: 0x04002E7B RID: 11899
		public const string DefaultTabString = "    ";
	}
}
