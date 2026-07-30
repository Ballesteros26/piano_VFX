using System;
using System.ComponentModel;
using System.Globalization;
using System.Xml.Serialization;

namespace System.Web.Services.Description
{
	/// <summary>Represents a text pattern for which the HTTP transmission is searched. This class cannot be inherited.</summary>
	// Token: 0x020000D4 RID: 212
	public sealed class MimeTextMatch
	{
		/// <summary>Gets or sets the name of the <see cref="T:System.Web.Services.Description.MimeTextMatch" />.</summary>
		/// <returns>The name of the <see cref="T:System.Web.Services.Description.MimeTextMatch" />.</returns>
		// Token: 0x1700015D RID: 349
		// (get) Token: 0x06000554 RID: 1364 RVA: 0x00018B2C File Offset: 0x00016D2C
		// (set) Token: 0x06000555 RID: 1365 RVA: 0x00018B42 File Offset: 0x00016D42
		[XmlAttribute("name")]
		public string Name
		{
			get
			{
				if (this.name != null)
				{
					return this.name;
				}
				return string.Empty;
			}
			set
			{
				this.name = value;
			}
		}

		/// <summary>Gets or sets a value indicating the MIME format of the text to be searched.</summary>
		/// <returns>A string indicating the MIME format of the text to be searched.</returns>
		// Token: 0x1700015E RID: 350
		// (get) Token: 0x06000556 RID: 1366 RVA: 0x00018B4B File Offset: 0x00016D4B
		// (set) Token: 0x06000557 RID: 1367 RVA: 0x00018B61 File Offset: 0x00016D61
		[XmlAttribute("type")]
		public string Type
		{
			get
			{
				if (this.type != null)
				{
					return this.type;
				}
				return string.Empty;
			}
			set
			{
				this.type = value;
			}
		}

		/// <summary>Gets or sets a value indicating the number of groups in which to place the results of the text search.</summary>
		/// <returns>A 32-bit signed integer. The default value is 1.</returns>
		/// <exception cref="T:System.ArgumentException">The property value is negative. </exception>
		// Token: 0x1700015F RID: 351
		// (get) Token: 0x06000558 RID: 1368 RVA: 0x00018B6A File Offset: 0x00016D6A
		// (set) Token: 0x06000559 RID: 1369 RVA: 0x00018B72 File Offset: 0x00016D72
		[DefaultValue(1)]
		[XmlAttribute("group")]
		public int Group
		{
			get
			{
				return this.group;
			}
			set
			{
				if (value < 0)
				{
					throw new ArgumentException(Res.GetString("WebNegativeValue", new object[] { "group" }));
				}
				this.group = value;
			}
		}

		/// <summary>Gets or sets a value indicating the zero-based index of a <see cref="T:System.Web.Services.Description.MimeTextMatch" /> within a group.</summary>
		/// <returns>A 32-bit signed integer. The default value is 0, indicating that the <see cref="T:System.Web.Services.Description.MimeTextMatch" /> is the first instance within a group.</returns>
		/// <exception cref="T:System.ArgumentException">The property value is negative. </exception>
		// Token: 0x17000160 RID: 352
		// (get) Token: 0x0600055A RID: 1370 RVA: 0x00018B9D File Offset: 0x00016D9D
		// (set) Token: 0x0600055B RID: 1371 RVA: 0x00018BA5 File Offset: 0x00016DA5
		[DefaultValue(0)]
		[XmlAttribute("capture")]
		public int Capture
		{
			get
			{
				return this.capture;
			}
			set
			{
				if (value < 0)
				{
					throw new ArgumentException(Res.GetString("WebNegativeValue", new object[] { "capture" }));
				}
				this.capture = value;
			}
		}

		/// <summary>Gets or sets a value indicating the number of times the search is to be performed.</summary>
		/// <returns>A 32-bit signed integer. The default value is 1.</returns>
		/// <exception cref="T:System.ArgumentException">The property value is negative. </exception>
		// Token: 0x17000161 RID: 353
		// (get) Token: 0x0600055C RID: 1372 RVA: 0x00018BD0 File Offset: 0x00016DD0
		// (set) Token: 0x0600055D RID: 1373 RVA: 0x00018BD8 File Offset: 0x00016DD8
		[XmlIgnore]
		public int Repeats
		{
			get
			{
				return this.repeats;
			}
			set
			{
				if (value < 0)
				{
					throw new ArgumentException(Res.GetString("WebNegativeValue", new object[] { "repeats" }));
				}
				this.repeats = value;
			}
		}

		/// <summary>Gets or sets a value indicating the number of times the search is to be performed.</summary>
		/// <returns>A string indicating the number of times the search is to be performed. The default value is "1".</returns>
		// Token: 0x17000162 RID: 354
		// (get) Token: 0x0600055E RID: 1374 RVA: 0x00018C03 File Offset: 0x00016E03
		// (set) Token: 0x0600055F RID: 1375 RVA: 0x00018C28 File Offset: 0x00016E28
		[DefaultValue("1")]
		[XmlAttribute("repeats")]
		public string RepeatsString
		{
			get
			{
				if (this.repeats != 2147483647)
				{
					return this.repeats.ToString(CultureInfo.InvariantCulture);
				}
				return "*";
			}
			set
			{
				if (value == "*")
				{
					this.repeats = int.MaxValue;
					return;
				}
				this.Repeats = int.Parse(value, CultureInfo.InvariantCulture);
			}
		}

		/// <summary>Gets or sets the text pattern for the search.</summary>
		/// <returns>A string representing the text for which to search the HTTP transmission. The default value is an empty string ("").</returns>
		// Token: 0x17000163 RID: 355
		// (get) Token: 0x06000560 RID: 1376 RVA: 0x00018C54 File Offset: 0x00016E54
		// (set) Token: 0x06000561 RID: 1377 RVA: 0x00018C6A File Offset: 0x00016E6A
		[XmlAttribute("pattern")]
		public string Pattern
		{
			get
			{
				if (this.pattern != null)
				{
					return this.pattern;
				}
				return string.Empty;
			}
			set
			{
				this.pattern = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether the search should disregard the case of the text to be searched.</summary>
		/// <returns>true if the search should disregard case; otherwise, false. The default is false.</returns>
		// Token: 0x17000164 RID: 356
		// (get) Token: 0x06000562 RID: 1378 RVA: 0x00018C73 File Offset: 0x00016E73
		// (set) Token: 0x06000563 RID: 1379 RVA: 0x00018C7B File Offset: 0x00016E7B
		[XmlAttribute("ignoreCase")]
		public bool IgnoreCase
		{
			get
			{
				return this.ignoreCase;
			}
			set
			{
				this.ignoreCase = value;
			}
		}

		/// <summary>Gets the collection of text pattern matches that have been found by the search.</summary>
		/// <returns>A <see cref="T:System.Web.Services.Description.MimeTextMatchCollection" /> representing the members of the <see cref="P:System.Web.Services.Description.MimeTextMatch.Group" /> property.</returns>
		// Token: 0x17000165 RID: 357
		// (get) Token: 0x06000564 RID: 1380 RVA: 0x00018C84 File Offset: 0x00016E84
		[XmlElement("match")]
		public MimeTextMatchCollection Matches
		{
			get
			{
				return this.matches;
			}
		}

		// Token: 0x0400038C RID: 908
		private string name;

		// Token: 0x0400038D RID: 909
		private string type;

		// Token: 0x0400038E RID: 910
		private int repeats = 1;

		// Token: 0x0400038F RID: 911
		private string pattern;

		// Token: 0x04000390 RID: 912
		private int group = 1;

		// Token: 0x04000391 RID: 913
		private int capture;

		// Token: 0x04000392 RID: 914
		private bool ignoreCase;

		// Token: 0x04000393 RID: 915
		private MimeTextMatchCollection matches = new MimeTextMatchCollection();
	}
}
