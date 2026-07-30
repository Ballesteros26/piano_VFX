using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace System.ComponentModel.DataAnnotations
{
	/// <summary>Validates file name extensions.</summary>
	// Token: 0x02000017 RID: 23
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter, AllowMultiple = false)]
	public sealed class FileExtensionsAttribute : DataTypeAttribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.DataAnnotations.FileExtensionsAttribute" /> class.</summary>
		// Token: 0x06000089 RID: 137 RVA: 0x00003383 File Offset: 0x00001583
		public FileExtensionsAttribute()
			: base(DataType.Upload)
		{
			base.DefaultErrorMessage = "The {0} field only accepts files with the following extensions: {1}";
		}

		/// <summary>Gets or sets the file name extensions.</summary>
		/// <returns>The file name extensions, or the default file extensions (".png", ".jpg", ".jpeg", and ".gif") if the property is not set.</returns>
		// Token: 0x17000027 RID: 39
		// (get) Token: 0x0600008A RID: 138 RVA: 0x00003398 File Offset: 0x00001598
		// (set) Token: 0x0600008B RID: 139 RVA: 0x000033B3 File Offset: 0x000015B3
		public string Extensions
		{
			get
			{
				if (!string.IsNullOrWhiteSpace(this._extensions))
				{
					return this._extensions;
				}
				return "png,jpg,jpeg,gif";
			}
			set
			{
				this._extensions = value;
			}
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x0600008C RID: 140 RVA: 0x000033BC File Offset: 0x000015BC
		private string ExtensionsFormatted
		{
			get
			{
				return this.ExtensionsParsed.Aggregate((string left, string right) => left + ", " + right);
			}
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x0600008D RID: 141 RVA: 0x000033E8 File Offset: 0x000015E8
		private string ExtensionsNormalized
		{
			get
			{
				return this.Extensions.Replace(" ", "").Replace(".", "").ToLowerInvariant();
			}
		}

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x0600008E RID: 142 RVA: 0x00003413 File Offset: 0x00001613
		private IEnumerable<string> ExtensionsParsed
		{
			get
			{
				return from e in this.ExtensionsNormalized.Split(new char[] { ',' })
					select "." + e;
			}
		}

		/// <summary>Applies formatting to an error message, based on the data field where the error occurred.</summary>
		/// <returns>The formatted error message.</returns>
		/// <param name="name">The name of the field that caused the validation failure.</param>
		// Token: 0x0600008F RID: 143 RVA: 0x0000344F File Offset: 0x0000164F
		public override string FormatErrorMessage(string name)
		{
			return string.Format(CultureInfo.CurrentCulture, base.ErrorMessageString, name, this.ExtensionsFormatted);
		}

		/// <summary>Checks that the specified file name extension or extensions is valid.</summary>
		/// <returns>true if the file name extension is valid; otherwise, false.</returns>
		/// <param name="value">A comma delimited list of valid file extensions.</param>
		// Token: 0x06000090 RID: 144 RVA: 0x00003468 File Offset: 0x00001668
		public override bool IsValid(object value)
		{
			if (value == null)
			{
				return true;
			}
			string text = value as string;
			return text != null && this.ValidateExtension(text);
		}

		// Token: 0x06000091 RID: 145 RVA: 0x00003490 File Offset: 0x00001690
		private bool ValidateExtension(string fileName)
		{
			bool flag;
			try
			{
				flag = this.ExtensionsParsed.Contains(Path.GetExtension(fileName).ToLowerInvariant());
			}
			catch (ArgumentException)
			{
				flag = false;
			}
			return flag;
		}

		// Token: 0x04000072 RID: 114
		private string _extensions;
	}
}
