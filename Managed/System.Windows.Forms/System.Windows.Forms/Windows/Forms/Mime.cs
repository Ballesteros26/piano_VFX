using System;
using System.Collections;
using System.Collections.Specialized;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace System.Windows.Forms
{
	// Token: 0x0200025D RID: 605
	internal class Mime
	{
		// Token: 0x06002793 RID: 10131 RVA: 0x00096F74 File Offset: 0x00095174
		private Mime()
		{
			Mime.Aliases = new NameValueCollection(StringComparer.CurrentCultureIgnoreCase);
			Mime.SubClasses = new NameValueCollection(StringComparer.CurrentCultureIgnoreCase);
			Mime.GlobalPatternsShort = new NameValueCollection(StringComparer.CurrentCultureIgnoreCase);
			Mime.GlobalPatternsLong = new NameValueCollection(StringComparer.CurrentCultureIgnoreCase);
			Mime.GlobalLiterals = new NameValueCollection(StringComparer.CurrentCultureIgnoreCase);
			Mime.GlobalSufPref = new NameValueCollection(StringComparer.CurrentCultureIgnoreCase);
			Mime.Matches80Plus = new ArrayList();
			Mime.MatchesBelow80 = new ArrayList();
			FDOMimeConfigReader fdomimeConfigReader = new FDOMimeConfigReader();
			int num = fdomimeConfigReader.Init();
			if (num >= 32)
			{
				this.buffer = new byte[num];
				this.mime_available = true;
			}
		}

		// Token: 0x170009BF RID: 2495
		// (get) Token: 0x06002795 RID: 10133 RVA: 0x0009704C File Offset: 0x0009524C
		public static bool MimeAvailable
		{
			get
			{
				return Mime.Instance.mime_available;
			}
		}

		// Token: 0x06002796 RID: 10134 RVA: 0x00097058 File Offset: 0x00095258
		public static string GetMimeTypeForFile(string filename)
		{
			object obj = Mime.lock_object;
			lock (obj)
			{
				Mime.Instance.StartByFileName(filename);
			}
			return Mime.Instance.global_result;
		}

		// Token: 0x06002797 RID: 10135 RVA: 0x000970B0 File Offset: 0x000952B0
		public static string GetMimeTypeForData(byte[] data)
		{
			object obj = Mime.lock_object;
			lock (obj)
			{
				Mime.Instance.StartDataLookup(data);
			}
			return Mime.Instance.global_result;
		}

		// Token: 0x06002798 RID: 10136 RVA: 0x00097108 File Offset: 0x00095308
		public static string GetMimeTypeForString(string input)
		{
			object obj = Mime.lock_object;
			lock (obj)
			{
				Mime.Instance.StartStringLookup(input);
			}
			return Mime.Instance.global_result;
		}

		// Token: 0x06002799 RID: 10137 RVA: 0x00097160 File Offset: 0x00095360
		public static string GetMimeAlias(string mimetype)
		{
			return Mime.Aliases[mimetype];
		}

		// Token: 0x0600279A RID: 10138 RVA: 0x00097170 File Offset: 0x00095370
		public static string GetMimeSubClass(string mimetype)
		{
			return Mime.SubClasses[mimetype];
		}

		// Token: 0x0600279B RID: 10139 RVA: 0x00097180 File Offset: 0x00095380
		public static void CleanFileCache()
		{
			object obj = Mime.lock_object;
			lock (obj)
			{
				Mime.Instance.mime_file_cache.Clear();
			}
		}

		// Token: 0x0600279C RID: 10140 RVA: 0x000971D0 File Offset: 0x000953D0
		private void StartByFileName(string filename)
		{
			if (this.mime_file_cache.ContainsKey(filename))
			{
				this.global_result = this.mime_file_cache[filename];
				return;
			}
			this.current_file_name = filename;
			this.is_zero_file = false;
			this.global_result = "application/octet-stream";
			this.GoByFileName();
			this.mime_file_cache.Add(this.current_file_name, this.global_result);
			if (this.mime_file_cache.Count > 3000)
			{
				IEnumerator enumerator = this.mime_file_cache.GetEnumerator();
				int num = 2500;
				while (enumerator.MoveNext())
				{
					object obj = enumerator.Current;
					this.mime_file_cache.Remove(obj.ToString());
					num--;
					if (num == 0)
					{
						break;
					}
				}
			}
		}

		// Token: 0x0600279D RID: 10141 RVA: 0x00097294 File Offset: 0x00095494
		private void StartDataLookup(byte[] data)
		{
			this.global_result = "application/octet-stream";
			Array.Clear(this.buffer, 0, this.buffer.Length);
			if (data.Length > this.buffer.Length)
			{
				Array.Copy(data, this.buffer, this.buffer.Length);
			}
			else
			{
				Array.Copy(data, this.buffer, data.Length);
			}
			if (this.CheckMatch80Plus())
			{
				return;
			}
			if (this.CheckMatchBelow80())
			{
				return;
			}
			this.CheckForBinaryOrText();
		}

		// Token: 0x0600279E RID: 10142 RVA: 0x00097318 File Offset: 0x00095518
		private void StartStringLookup(string input)
		{
			this.global_result = "text/plain";
			this.search_string = input;
			if (this.CheckForContentTypeString())
			{
				return;
			}
		}

		// Token: 0x0600279F RID: 10143 RVA: 0x00097338 File Offset: 0x00095538
		private void GoByFileName()
		{
			if (!Mime.MimeAvailable || !this.OpenFile())
			{
				this.CheckGlobalPatterns();
				return;
			}
			if (!this.is_zero_file && this.CheckMatch80Plus())
			{
				return;
			}
			if (this.CheckGlobalPatterns())
			{
				return;
			}
			if (this.is_zero_file)
			{
				return;
			}
			if (this.CheckMatchBelow80())
			{
				return;
			}
			this.CheckForBinaryOrText();
		}

		// Token: 0x060027A0 RID: 10144 RVA: 0x000973A4 File Offset: 0x000955A4
		private bool CheckMatch80Plus()
		{
			foreach (object obj in Mime.Matches80Plus)
			{
				Match match = (Match)obj;
				if (this.TestMatch(match))
				{
					this.global_result = match.MimeType;
					return true;
				}
			}
			return false;
		}

		// Token: 0x060027A1 RID: 10145 RVA: 0x00097430 File Offset: 0x00095630
		private bool FastEndsWidth(string input, string value)
		{
			if (value.Length > input.Length)
			{
				return false;
			}
			int num = input.Length - 1;
			for (int i = value.Length - 1; i > -1; i--)
			{
				if (value.get_Chars(i) != input.get_Chars(num))
				{
					return false;
				}
				num--;
			}
			return true;
		}

		// Token: 0x060027A2 RID: 10146 RVA: 0x0009748C File Offset: 0x0009568C
		private bool FastStartsWith(string input, string value)
		{
			if (value.Length > input.Length)
			{
				return false;
			}
			for (int i = 0; i < value.Length; i++)
			{
				if (value.get_Chars(i) != input.get_Chars(i))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060027A3 RID: 10147 RVA: 0x000974DC File Offset: 0x000956DC
		private int FastIndexOf(string input, char value)
		{
			if (input.Length == 0)
			{
				return -1;
			}
			for (int i = 0; i < input.Length; i++)
			{
				if (input.get_Chars(i) == value)
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x060027A4 RID: 10148 RVA: 0x00097520 File Offset: 0x00095720
		private int FastIndexOf(string input, string value)
		{
			if (input.Length == 0)
			{
				return -1;
			}
			for (int i = 0; i < input.Length - value.Length; i++)
			{
				if (input.get_Chars(i) == value.get_Chars(0))
				{
					int num = 0;
					for (int j = 1; j < value.Length; j++)
					{
						if (input.get_Chars(i + j) != value.get_Chars(j))
						{
							break;
						}
						num++;
					}
					if (num == value.Length - 1)
					{
						return i;
					}
				}
			}
			return -1;
		}

		// Token: 0x060027A5 RID: 10149 RVA: 0x000975B4 File Offset: 0x000957B4
		private void CheckGlobalResult()
		{
			int num = this.FastIndexOf(this.global_result, ',');
			if (num != -1)
			{
				this.global_result = this.global_result.Substring(0, num);
			}
		}

		// Token: 0x060027A6 RID: 10150 RVA: 0x000975EC File Offset: 0x000957EC
		private bool CheckGlobalPatterns()
		{
			string fileName = Path.GetFileName(this.current_file_name);
			for (int i = 0; i < Mime.GlobalLiterals.Count; i++)
			{
				string key = Mime.GlobalLiterals.GetKey(i);
				if (this.FastIndexOf(key, '[') == -1)
				{
					if (this.FastIndexOf(fileName, key) != -1)
					{
						this.global_result = Mime.GlobalLiterals[i];
						this.CheckGlobalResult();
						return true;
					}
				}
				else if (Regex.IsMatch(fileName, key))
				{
					this.global_result = Mime.GlobalLiterals[i];
					this.CheckGlobalResult();
					return true;
				}
			}
			if (this.FastIndexOf(fileName, '.') != -1)
			{
				for (int j = 0; j < Mime.GlobalPatternsLong.Count; j++)
				{
					string key2 = Mime.GlobalPatternsLong.GetKey(j);
					if (this.FastEndsWidth(fileName, key2))
					{
						this.global_result = Mime.GlobalPatternsLong[j];
						this.CheckGlobalResult();
						return true;
					}
					if (this.FastEndsWidth(fileName.ToLower(), key2))
					{
						this.global_result = Mime.GlobalPatternsLong[j];
						this.CheckGlobalResult();
						return true;
					}
				}
				string extension = Path.GetExtension(this.current_file_name);
				if (extension.Length != 0)
				{
					string text = Mime.GlobalPatternsShort[extension];
					if (text != null)
					{
						this.global_result = text;
						this.CheckGlobalResult();
						return true;
					}
					text = Mime.GlobalPatternsShort[extension.ToLower()];
					if (text != null)
					{
						this.global_result = text;
						this.CheckGlobalResult();
						return true;
					}
				}
			}
			for (int k = 0; k < Mime.GlobalSufPref.Count; k++)
			{
				string key3 = Mime.GlobalSufPref.GetKey(k);
				if (key3.get_Chars(0) == '*')
				{
					if (this.FastEndsWidth(fileName, key3.Replace("*", string.Empty)))
					{
						this.global_result = Mime.GlobalSufPref[k];
						this.CheckGlobalResult();
						return true;
					}
				}
				else if (this.FastStartsWith(fileName, key3.Replace("*", string.Empty)))
				{
					this.global_result = Mime.GlobalSufPref[k];
					this.CheckGlobalResult();
					return true;
				}
			}
			return false;
		}

		// Token: 0x060027A7 RID: 10151 RVA: 0x00097830 File Offset: 0x00095A30
		private bool CheckMatchBelow80()
		{
			foreach (object obj in Mime.MatchesBelow80)
			{
				Match match = (Match)obj;
				if (this.TestMatch(match))
				{
					this.global_result = match.MimeType;
					return true;
				}
			}
			return false;
		}

		// Token: 0x060027A8 RID: 10152 RVA: 0x000978BC File Offset: 0x00095ABC
		private void CheckForBinaryOrText()
		{
			for (int i = 0; i < 32; i++)
			{
				char c = Convert.ToChar(this.buffer[i]);
				if (c != '\t' && c != '\n' && c != '\r' && c != '\f' && c < ' ')
				{
					this.global_result = "application/octet-stream";
					return;
				}
			}
			this.global_result = "text/plain";
		}

		// Token: 0x060027A9 RID: 10153 RVA: 0x0009792C File Offset: 0x00095B2C
		private bool TestMatch(Match match)
		{
			foreach (object obj in match.Matchlets)
			{
				Matchlet matchlet = (Matchlet)obj;
				if (this.TestMatchlet(matchlet))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060027AA RID: 10154 RVA: 0x000979AC File Offset: 0x00095BAC
		private bool TestMatchlet(Matchlet matchlet)
		{
			if (matchlet.Offset + matchlet.ByteValue.Length > this.bytes_read)
			{
				return false;
			}
			for (int i = 0; i < matchlet.OffsetLength; i++)
			{
				if (matchlet.Offset + i + matchlet.ByteValue.Length > this.bytes_read)
				{
					return false;
				}
				if (matchlet.Mask == null)
				{
					if (this.buffer[matchlet.Offset + i] == matchlet.ByteValue[0])
					{
						if (matchlet.ByteValue.Length == 1)
						{
							if (matchlet.Matchlets.Count <= 0)
							{
								return true;
							}
							foreach (object obj in matchlet.Matchlets)
							{
								Matchlet matchlet2 = (Matchlet)obj;
								if (this.TestMatchlet(matchlet2))
								{
									return true;
								}
							}
						}
						int num = 0;
						if (matchlet.ByteValue.Length > 2)
						{
							if (this.buffer[matchlet.Offset + i + matchlet.ByteValue.Length - 1] != matchlet.ByteValue[matchlet.ByteValue.Length - 1])
							{
								return false;
							}
							num = 1;
						}
						for (int j = 1; j < matchlet.ByteValue.Length - num; j++)
						{
							if (this.buffer[matchlet.Offset + i + j] != matchlet.ByteValue[j])
							{
								return false;
							}
						}
						if (matchlet.Matchlets.Count <= 0)
						{
							return true;
						}
						foreach (object obj2 in matchlet.Matchlets)
						{
							Matchlet matchlet3 = (Matchlet)obj2;
							if (this.TestMatchlet(matchlet3))
							{
								return true;
							}
						}
					}
				}
				else if ((this.buffer[matchlet.Offset + i] & matchlet.Mask[0]) == (matchlet.ByteValue[0] & matchlet.Mask[0]))
				{
					if (matchlet.ByteValue.Length == 1)
					{
						if (matchlet.Matchlets.Count <= 0)
						{
							return true;
						}
						foreach (object obj3 in matchlet.Matchlets)
						{
							Matchlet matchlet4 = (Matchlet)obj3;
							if (this.TestMatchlet(matchlet4))
							{
								return true;
							}
						}
					}
					int num2 = 0;
					if (matchlet.ByteValue.Length > 2)
					{
						if ((this.buffer[matchlet.Offset + i + matchlet.ByteValue.Length - 1] & matchlet.Mask[matchlet.ByteValue.Length - 1]) != (matchlet.ByteValue[matchlet.ByteValue.Length - 1] & matchlet.Mask[matchlet.ByteValue.Length - 1]))
						{
							return false;
						}
						num2 = 1;
					}
					for (int k = 1; k < matchlet.ByteValue.Length - num2; k++)
					{
						if ((this.buffer[matchlet.Offset + i + k] & matchlet.Mask[k]) != (matchlet.ByteValue[k] & matchlet.Mask[k]))
						{
							return false;
						}
					}
					if (matchlet.Matchlets.Count <= 0)
					{
						return true;
					}
					foreach (object obj4 in matchlet.Matchlets)
					{
						Matchlet matchlet5 = (Matchlet)obj4;
						if (this.TestMatchlet(matchlet5))
						{
							return true;
						}
					}
				}
			}
			return false;
		}

		// Token: 0x060027AB RID: 10155 RVA: 0x00097DF8 File Offset: 0x00095FF8
		private bool OpenFile()
		{
			try
			{
				this.file_stream = new FileStream(this.current_file_name, 3, 1);
				if (this.file_stream.Length == 0L)
				{
					this.global_result = "application/x-zerosize";
					this.is_zero_file = true;
				}
				else
				{
					this.bytes_read = this.file_stream.Read(this.buffer, 0, this.buffer.Length);
					if (this.bytes_read < this.buffer.Length)
					{
						Array.Clear(this.buffer, this.bytes_read, this.buffer.Length - this.bytes_read);
					}
				}
				this.file_stream.Close();
			}
			catch (Exception)
			{
				return false;
			}
			return true;
		}

		// Token: 0x060027AC RID: 10156 RVA: 0x00097ED0 File Offset: 0x000960D0
		private bool CheckForContentTypeString()
		{
			int num = this.search_string.IndexOf("Content-type:");
			if (num != -1)
			{
				num += 13;
				this.global_result = string.Empty;
				while (this.search_string.get_Chars(num) != ';')
				{
					this.global_result += this.search_string.get_Chars(num++);
				}
				this.global_result.Trim();
				return true;
			}
			byte[] bytes = new ASCIIEncoding().GetBytes(this.search_string);
			Array.Clear(this.buffer, 0, this.buffer.Length);
			if (bytes.Length > this.buffer.Length)
			{
				Array.Copy(bytes, this.buffer, this.buffer.Length);
			}
			else
			{
				Array.Copy(bytes, this.buffer, bytes.Length);
			}
			return this.CheckMatch80Plus() || this.CheckMatchBelow80();
		}

		// Token: 0x040013C1 RID: 5057
		private const string octet_stream = "application/octet-stream";

		// Token: 0x040013C2 RID: 5058
		private const string text_plain = "text/plain";

		// Token: 0x040013C3 RID: 5059
		private const string zero_file = "application/x-zerosize";

		// Token: 0x040013C4 RID: 5060
		private const int mime_file_cache_max_size = 3000;

		// Token: 0x040013C5 RID: 5061
		public static Mime Instance = new Mime();

		// Token: 0x040013C6 RID: 5062
		private string current_file_name;

		// Token: 0x040013C7 RID: 5063
		private string global_result = "application/octet-stream";

		// Token: 0x040013C8 RID: 5064
		private FileStream file_stream;

		// Token: 0x040013C9 RID: 5065
		private byte[] buffer;

		// Token: 0x040013CA RID: 5066
		private StringDictionary mime_file_cache = new StringDictionary();

		// Token: 0x040013CB RID: 5067
		private string search_string;

		// Token: 0x040013CC RID: 5068
		private static object lock_object = new object();

		// Token: 0x040013CD RID: 5069
		private bool is_zero_file;

		// Token: 0x040013CE RID: 5070
		private int bytes_read;

		// Token: 0x040013CF RID: 5071
		private bool mime_available;

		// Token: 0x040013D0 RID: 5072
		public static NameValueCollection Aliases;

		// Token: 0x040013D1 RID: 5073
		public static NameValueCollection SubClasses;

		// Token: 0x040013D2 RID: 5074
		public static NameValueCollection GlobalPatternsShort;

		// Token: 0x040013D3 RID: 5075
		public static NameValueCollection GlobalPatternsLong;

		// Token: 0x040013D4 RID: 5076
		public static NameValueCollection GlobalLiterals;

		// Token: 0x040013D5 RID: 5077
		public static NameValueCollection GlobalSufPref;

		// Token: 0x040013D6 RID: 5078
		public static ArrayList Matches80Plus;

		// Token: 0x040013D7 RID: 5079
		public static ArrayList MatchesBelow80;
	}
}
