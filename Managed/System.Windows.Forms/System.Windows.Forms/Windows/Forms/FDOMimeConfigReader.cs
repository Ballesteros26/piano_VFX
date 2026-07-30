using System;
using System.Collections.Specialized;
using System.IO;
using System.Text;

namespace System.Windows.Forms
{
	// Token: 0x0200025E RID: 606
	internal class FDOMimeConfigReader
	{
		// Token: 0x060027AE RID: 10158 RVA: 0x00097FE0 File Offset: 0x000961E0
		public int Init()
		{
			this.CheckFDOMimePaths();
			if (!this.fdo_mime_available)
			{
				return -1;
			}
			this.ReadMagicData();
			this.ReadGlobsData();
			this.ReadSubclasses();
			this.ReadAliases();
			this.shared_mime_paths = null;
			this.br = null;
			return this.max_offset_and_range;
		}

		// Token: 0x060027AF RID: 10159 RVA: 0x0009802C File Offset: 0x0009622C
		private void CheckFDOMimePaths()
		{
			if (Directory.Exists("/usr/share/mime"))
			{
				this.shared_mime_paths.Add("/usr/share/mime/");
			}
			else if (Directory.Exists("/usr/local/share/mime"))
			{
				this.shared_mime_paths.Add("/usr/local/share/mime/");
			}
			if (Directory.Exists(Environment.GetFolderPath(5) + "/.local/share/mime"))
			{
				this.shared_mime_paths.Add(Environment.GetFolderPath(5) + "/.local/share/mime/");
			}
			if (this.shared_mime_paths.Count == 0)
			{
				return;
			}
			this.fdo_mime_available = true;
		}

		// Token: 0x060027B0 RID: 10160 RVA: 0x000980CC File Offset: 0x000962CC
		private void ReadMagicData()
		{
			foreach (string text in this.shared_mime_paths)
			{
				if (File.Exists(text + "/magic"))
				{
					try
					{
						FileStream fileStream = File.OpenRead(text + "/magic");
						this.br = new BinaryReader(fileStream);
						if (this.CheckMagicHeader())
						{
							this.MakeMatches();
						}
						this.br.Close();
						fileStream.Close();
					}
					catch (Exception)
					{
					}
				}
			}
		}

		// Token: 0x060027B1 RID: 10161 RVA: 0x000981AC File Offset: 0x000963AC
		private void MakeMatches()
		{
			Matchlet[] array = new Matchlet[30];
			while (this.br.PeekChar() != -1)
			{
				int num = -1;
				string text = this.ReadPriorityAndMimeType(ref num);
				if (text != null)
				{
					Match match = new Match();
					match.Priority = num;
					match.MimeType = text;
					do
					{
						int num2 = 0;
						if (this.br.PeekChar() != 62)
						{
							StringBuilder stringBuilder = new StringBuilder();
							while (this.br.PeekChar() != 62)
							{
								char c = this.br.ReadChar();
								stringBuilder.Append(c);
							}
							num2 = Convert.ToInt32(stringBuilder.ToString());
						}
						int num3 = 0;
						if (this.br.PeekChar() == 62)
						{
							this.br.ReadChar();
							num3 = this.ReadValue();
						}
						int num4 = 0;
						byte[] array2 = null;
						if (this.br.PeekChar() == 61)
						{
							this.br.ReadChar();
							byte b = this.br.ReadByte();
							byte b2 = this.br.ReadByte();
							num4 = (int)b * 256 + (int)b2;
							array2 = this.br.ReadBytes(num4);
						}
						byte[] array3 = null;
						if (this.br.PeekChar() == 38)
						{
							this.br.ReadChar();
							array3 = this.br.ReadBytes(num4);
						}
						if (this.br.PeekChar() == 126)
						{
							this.br.ReadChar();
							char c = this.br.ReadChar();
							int num5 = Convert.ToInt32((int)(c - '0'));
							if (num5 > 1 && BitConverter.IsLittleEndian)
							{
								if (num5 == 2)
								{
									if (array2 != null)
									{
										for (int i = 0; i < array2.Length; i += 2)
										{
											byte b3 = array2[i];
											byte b4 = array2[i + 1];
											array2[i] = b4;
											array2[i + 1] = b3;
										}
									}
									if (array3 != null)
									{
										for (int j = 0; j < array3.Length; j += 2)
										{
											byte b5 = array3[j];
											byte b6 = array3[j + 1];
											array3[j] = b6;
											array3[j + 1] = b5;
										}
									}
								}
								else if (num5 == 4)
								{
									if (array2 != null)
									{
										for (int k = 0; k < array2.Length; k += 4)
										{
											byte b7 = array2[k];
											byte b8 = array2[k + 1];
											byte b9 = array2[k + 2];
											byte b10 = array2[k + 3];
											array2[k] = b10;
											array2[k + 1] = b9;
											array2[k + 2] = b8;
											array2[k + 3] = b7;
										}
									}
									if (array3 != null)
									{
										for (int l = 0; l < array3.Length; l += 4)
										{
											byte b11 = array3[l];
											byte b12 = array3[l + 1];
											byte b13 = array3[l + 2];
											byte b14 = array3[l + 3];
											array3[l] = b14;
											array3[l + 1] = b13;
											array3[l + 2] = b12;
											array3[l + 3] = b11;
										}
									}
								}
							}
						}
						int num6 = 1;
						if (this.br.PeekChar() == 43)
						{
							this.br.ReadChar();
							num6 = this.ReadValue();
						}
						this.br.ReadChar();
						array[num2] = new Matchlet();
						array[num2].Offset = num3;
						array[num2].OffsetLength = num6;
						array[num2].ByteValue = array2;
						if (array3 != null)
						{
							array[num2].Mask = array3;
						}
						if (num2 == 0)
						{
							match.Matchlets.Add(array[num2]);
						}
						else
						{
							array[num2 - 1].Matchlets.Add(array[num2]);
						}
						if (this.max_offset_and_range < array[num2].Offset + array[num2].OffsetLength + array[num2].ByteValue.Length + 1)
						{
							this.max_offset_and_range = array[num2].Offset + array[num2].OffsetLength + array[num2].ByteValue.Length + 1;
						}
					}
					while (this.br.PeekChar() != 91);
					if (num < 80)
					{
						Mime.MatchesBelow80.Add(match);
					}
					else
					{
						Mime.Matches80Plus.Add(match);
					}
				}
			}
		}

		// Token: 0x060027B2 RID: 10162 RVA: 0x000985EC File Offset: 0x000967EC
		private void ReadGlobsData()
		{
			foreach (string text in this.shared_mime_paths)
			{
				if (File.Exists(text + "/globs"))
				{
					try
					{
						StreamReader streamReader = new StreamReader(text + "/globs");
						while (streamReader.Peek() != -1)
						{
							string text2 = streamReader.ReadLine().Trim();
							if (!text2.StartsWith("#"))
							{
								string[] array = text2.Split(new char[] { ':' });
								if (array[1].IndexOf('*') > -1 && array[1].IndexOf('.') == -1)
								{
									Mime.GlobalSufPref.Add(array[1], array[0]);
								}
								else if (array[1].IndexOf('*') == -1)
								{
									Mime.GlobalLiterals.Add(array[1], array[0]);
								}
								else
								{
									string[] array2 = array[1].Split(new char[] { '.' });
									if (array2.Length > 2)
									{
										Mime.GlobalPatternsLong.Add(array[1].Remove(0, 1), array[0]);
									}
									else
									{
										Mime.GlobalPatternsShort.Add(array[1].Remove(0, 1), array[0]);
									}
								}
							}
						}
						streamReader.Close();
					}
					catch (Exception)
					{
					}
				}
			}
		}

		// Token: 0x060027B3 RID: 10163 RVA: 0x000987A4 File Offset: 0x000969A4
		private void ReadSubclasses()
		{
			foreach (string text in this.shared_mime_paths)
			{
				if (File.Exists(text + "/subclasses"))
				{
					try
					{
						StreamReader streamReader = new StreamReader(text + "/subclasses");
						while (streamReader.Peek() != -1)
						{
							string text2 = streamReader.ReadLine().Trim();
							if (!text2.StartsWith("#"))
							{
								string[] array = text2.Split(new char[] { ' ' });
								Mime.SubClasses.Add(array[0], array[1]);
							}
						}
						streamReader.Close();
					}
					catch (Exception)
					{
					}
				}
			}
		}

		// Token: 0x060027B4 RID: 10164 RVA: 0x000988B8 File Offset: 0x00096AB8
		private void ReadAliases()
		{
			foreach (string text in this.shared_mime_paths)
			{
				if (File.Exists(text + "/aliases"))
				{
					try
					{
						StreamReader streamReader = new StreamReader(text + "/aliases");
						while (streamReader.Peek() != -1)
						{
							string text2 = streamReader.ReadLine().Trim();
							if (!text2.StartsWith("#"))
							{
								string[] array = text2.Split(new char[] { ' ' });
								Mime.Aliases.Add(array[0], array[1]);
							}
						}
						streamReader.Close();
					}
					catch (Exception)
					{
					}
				}
			}
		}

		// Token: 0x060027B5 RID: 10165 RVA: 0x000989CC File Offset: 0x00096BCC
		private int ReadValue()
		{
			StringBuilder stringBuilder = new StringBuilder();
			while (this.br.PeekChar() != 61 && this.br.PeekChar() != 10)
			{
				char c = this.br.ReadChar();
				stringBuilder.Append(c);
			}
			return Convert.ToInt32(stringBuilder.ToString());
		}

		// Token: 0x060027B6 RID: 10166 RVA: 0x00098A30 File Offset: 0x00096C30
		private string ReadPriorityAndMimeType(ref int priority)
		{
			if (this.br.ReadChar() == '[')
			{
				StringBuilder stringBuilder = new StringBuilder();
				for (;;)
				{
					char c = this.br.ReadChar();
					if (c == ':')
					{
						break;
					}
					stringBuilder.Append(c);
				}
				priority = Convert.ToInt32(stringBuilder.ToString());
				StringBuilder stringBuilder2 = new StringBuilder();
				for (;;)
				{
					char c2 = this.br.ReadChar();
					if (c2 == ']')
					{
						break;
					}
					stringBuilder2.Append(c2);
				}
				if (this.br.ReadChar() == '\n')
				{
					return stringBuilder2.ToString();
				}
			}
			return null;
		}

		// Token: 0x060027B7 RID: 10167 RVA: 0x00098AD0 File Offset: 0x00096CD0
		private bool CheckMagicHeader()
		{
			char[] array = this.br.ReadChars(10);
			string text = new string(array);
			return !(text != "MIME-Magic") && this.br.ReadByte() == 0 && this.br.ReadChar() == '\n';
		}

		// Token: 0x040013D8 RID: 5080
		private bool fdo_mime_available;

		// Token: 0x040013D9 RID: 5081
		private StringCollection shared_mime_paths = new StringCollection();

		// Token: 0x040013DA RID: 5082
		private BinaryReader br;

		// Token: 0x040013DB RID: 5083
		private int max_offset_and_range;
	}
}
