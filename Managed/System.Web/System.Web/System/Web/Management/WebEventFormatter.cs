using System;

namespace System.Web.Management
{
	/// <summary>Formats ASP.NET health monitoring event information.</summary>
	// Token: 0x02000532 RID: 1330
	public class WebEventFormatter
	{
		// Token: 0x06003A50 RID: 14928 RVA: 0x00002050 File Offset: 0x00000250
		internal WebEventFormatter()
		{
		}

		/// <summary>Gets or sets the indentation level.</summary>
		/// <returns>The number of tabs used for the indentation level. </returns>
		// Token: 0x17001209 RID: 4617
		// (get) Token: 0x06003A51 RID: 14929 RVA: 0x0009D713 File Offset: 0x0009B913
		// (set) Token: 0x06003A52 RID: 14930 RVA: 0x0009D71B File Offset: 0x0009B91B
		public int IndentationLevel
		{
			get
			{
				return this.indentation_level;
			}
			set
			{
				this.indentation_level = value;
			}
		}

		/// <summary>Gets or sets the tab size.</summary>
		/// <returns>The number of spaces in a tab.</returns>
		// Token: 0x1700120A RID: 4618
		// (get) Token: 0x06003A53 RID: 14931 RVA: 0x0009D724 File Offset: 0x0009B924
		// (set) Token: 0x06003A54 RID: 14932 RVA: 0x0009D72C File Offset: 0x0009B92C
		public int TabSize
		{
			get
			{
				return this.tab_size;
			}
			set
			{
				this.tab_size = value;
			}
		}

		/// <summary>Appends the specified string and a carriage return to the event information.</summary>
		/// <param name="s">The string to add to the event information.</param>
		// Token: 0x06003A55 RID: 14933 RVA: 0x00003A1F File Offset: 0x00001C1F
		public void AppendLine(string s)
		{
			throw new NotImplementedException();
		}

		/// <summary>Returns the event information in string format.</summary>
		/// <returns>The event information.</returns>
		// Token: 0x06003A56 RID: 14934 RVA: 0x00003A1F File Offset: 0x00001C1F
		public new string ToString()
		{
			throw new NotImplementedException();
		}

		// Token: 0x04001FBC RID: 8124
		private int indentation_level;

		// Token: 0x04001FBD RID: 8125
		private int tab_size;
	}
}
