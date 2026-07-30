using System;
using System.Collections.Generic;
using System.Security;
using Microsoft.Win32;
using Microsoft.Win32.SafeHandles;

namespace System.IO
{
	// Token: 0x02000397 RID: 919
	internal class FileSystemEnumerableIterator<TSource> : Iterator<TSource>
	{
		// Token: 0x06002ACF RID: 10959 RVA: 0x00098B14 File Offset: 0x00096D14
		[SecuritySafeCritical]
		internal FileSystemEnumerableIterator(string path, string originalUserPath, string searchPattern, SearchOption searchOption, SearchResultHandler<TSource> resultHandler, bool checkHost)
		{
			this.searchStack = new List<Directory.SearchData>();
			string text = FileSystemEnumerableIterator<TSource>.NormalizeSearchPattern(searchPattern);
			if (text.Length == 0)
			{
				this.empty = true;
				return;
			}
			this._resultHandler = resultHandler;
			this.searchOption = searchOption;
			this.fullPath = Path.GetFullPathInternal(path);
			string fullSearchString = FileSystemEnumerableIterator<TSource>.GetFullSearchString(this.fullPath, text);
			this.normalizedSearchPath = Path.GetDirectoryName(fullSearchString);
			string[] array = new string[]
			{
				Directory.GetDemandDir(this.fullPath, true),
				Directory.GetDemandDir(this.normalizedSearchPath, true)
			};
			this._checkHost = checkHost;
			this.searchCriteria = FileSystemEnumerableIterator<TSource>.GetNormalizedSearchCriteria(fullSearchString, this.normalizedSearchPath);
			string directoryName = Path.GetDirectoryName(text);
			string text2 = originalUserPath;
			if (directoryName != null && directoryName.Length != 0)
			{
				text2 = Path.Combine(text2, directoryName);
			}
			this.userPath = text2;
			this.searchData = new Directory.SearchData(this.normalizedSearchPath, this.userPath, searchOption);
			this.CommonInit();
		}

		// Token: 0x06002AD0 RID: 10960 RVA: 0x00098C00 File Offset: 0x00096E00
		[SecurityCritical]
		private void CommonInit()
		{
			string text = Path.InternalCombine(this.searchData.fullPath, this.searchCriteria);
			Win32Native.WIN32_FIND_DATA win32_FIND_DATA = new Win32Native.WIN32_FIND_DATA();
			int num;
			this._hnd = new SafeFindHandle(MonoIO.FindFirstFile(text, out win32_FIND_DATA.cFileName, out win32_FIND_DATA.dwFileAttributes, out num));
			if (this._hnd.IsInvalid)
			{
				int num2 = num;
				if (num2 != 2 && num2 != 18)
				{
					this.HandleError(num2, this.searchData.fullPath);
				}
				else
				{
					this.empty = this.searchData.searchOption == SearchOption.TopDirectoryOnly;
				}
			}
			if (this.searchData.searchOption == SearchOption.TopDirectoryOnly)
			{
				if (this.empty)
				{
					this._hnd.Dispose();
					return;
				}
				SearchResult searchResult = this.CreateSearchResult(this.searchData, win32_FIND_DATA);
				if (this._resultHandler.IsResultIncluded(searchResult))
				{
					this.current = this._resultHandler.CreateObject(searchResult);
					return;
				}
			}
			else
			{
				this._hnd.Dispose();
				this.searchStack.Add(this.searchData);
			}
		}

		// Token: 0x06002AD1 RID: 10961 RVA: 0x00098CF8 File Offset: 0x00096EF8
		[SecuritySafeCritical]
		private FileSystemEnumerableIterator(string fullPath, string normalizedSearchPath, string searchCriteria, string userPath, SearchOption searchOption, SearchResultHandler<TSource> resultHandler, bool checkHost)
		{
			this.fullPath = fullPath;
			this.normalizedSearchPath = normalizedSearchPath;
			this.searchCriteria = searchCriteria;
			this._resultHandler = resultHandler;
			this.userPath = userPath;
			this.searchOption = searchOption;
			this._checkHost = checkHost;
			this.searchStack = new List<Directory.SearchData>();
			if (searchCriteria != null)
			{
				string[] array = new string[]
				{
					Directory.GetDemandDir(fullPath, true),
					Directory.GetDemandDir(normalizedSearchPath, true)
				};
				this.searchData = new Directory.SearchData(normalizedSearchPath, userPath, searchOption);
				this.CommonInit();
				return;
			}
			this.empty = true;
		}

		// Token: 0x06002AD2 RID: 10962 RVA: 0x00098D85 File Offset: 0x00096F85
		protected override Iterator<TSource> Clone()
		{
			return new FileSystemEnumerableIterator<TSource>(this.fullPath, this.normalizedSearchPath, this.searchCriteria, this.userPath, this.searchOption, this._resultHandler, this._checkHost);
		}

		// Token: 0x06002AD3 RID: 10963 RVA: 0x00098DB8 File Offset: 0x00096FB8
		[SecuritySafeCritical]
		protected override void Dispose(bool disposing)
		{
			try
			{
				if (this._hnd != null)
				{
					this._hnd.Dispose();
				}
			}
			finally
			{
				base.Dispose(disposing);
			}
		}

		// Token: 0x06002AD4 RID: 10964 RVA: 0x00098DF4 File Offset: 0x00096FF4
		[SecuritySafeCritical]
		public override bool MoveNext()
		{
			Win32Native.WIN32_FIND_DATA win32_FIND_DATA = new Win32Native.WIN32_FIND_DATA();
			switch (this.state)
			{
			case 1:
				if (this.empty)
				{
					this.state = 4;
					goto IL_0278;
				}
				if (this.searchData.searchOption == SearchOption.TopDirectoryOnly)
				{
					this.state = 3;
					if (this.current != null)
					{
						return true;
					}
					goto IL_0192;
				}
				else
				{
					this.state = 2;
				}
				break;
			case 2:
				break;
			case 3:
				goto IL_0192;
			case 4:
				goto IL_0278;
			default:
				return false;
			}
			IL_0175:
			while (this.searchStack.Count > 0)
			{
				this.searchData = this.searchStack[0];
				this.searchStack.RemoveAt(0);
				this.AddSearchableDirsToStack(this.searchData);
				string text = Path.InternalCombine(this.searchData.fullPath, this.searchCriteria);
				int num;
				this._hnd = new SafeFindHandle(MonoIO.FindFirstFile(text, out win32_FIND_DATA.cFileName, out win32_FIND_DATA.dwFileAttributes, out num));
				if (this._hnd.IsInvalid)
				{
					int num2 = num;
					if (num2 == 2 || num2 == 18 || num2 == 3)
					{
						continue;
					}
					this._hnd.Dispose();
					this.HandleError(num2, this.searchData.fullPath);
				}
				this.state = 3;
				this.needsParentPathDiscoveryDemand = true;
				SearchResult searchResult = this.CreateSearchResult(this.searchData, win32_FIND_DATA);
				if (this._resultHandler.IsResultIncluded(searchResult))
				{
					if (this.needsParentPathDiscoveryDemand)
					{
						this.DoDemand(this.searchData.fullPath);
						this.needsParentPathDiscoveryDemand = false;
					}
					this.current = this._resultHandler.CreateObject(searchResult);
					return true;
				}
				goto IL_0192;
			}
			this.state = 4;
			goto IL_0278;
			IL_0192:
			if (this.searchData != null && this._hnd != null)
			{
				int num3;
				while (MonoIO.FindNextFile(this._hnd.DangerousGetHandle(), out win32_FIND_DATA.cFileName, out win32_FIND_DATA.dwFileAttributes, out num3))
				{
					SearchResult searchResult2 = this.CreateSearchResult(this.searchData, win32_FIND_DATA);
					if (this._resultHandler.IsResultIncluded(searchResult2))
					{
						if (this.needsParentPathDiscoveryDemand)
						{
							this.DoDemand(this.searchData.fullPath);
							this.needsParentPathDiscoveryDemand = false;
						}
						this.current = this._resultHandler.CreateObject(searchResult2);
						return true;
					}
				}
				int num4 = num3;
				if (this._hnd != null)
				{
					this._hnd.Dispose();
				}
				if (num4 != 0 && num4 != 18 && num4 != 2)
				{
					this.HandleError(num4, this.searchData.fullPath);
				}
			}
			if (this.searchData.searchOption != SearchOption.TopDirectoryOnly)
			{
				this.state = 2;
				goto IL_0175;
			}
			this.state = 4;
			IL_0278:
			base.Dispose();
			return false;
		}

		// Token: 0x06002AD5 RID: 10965 RVA: 0x00099080 File Offset: 0x00097280
		[SecurityCritical]
		private SearchResult CreateSearchResult(Directory.SearchData localSearchData, Win32Native.WIN32_FIND_DATA findData)
		{
			string text = Path.InternalCombine(localSearchData.userPath, findData.cFileName);
			return new SearchResult(Path.InternalCombine(localSearchData.fullPath, findData.cFileName), text, findData);
		}

		// Token: 0x06002AD6 RID: 10966 RVA: 0x000990B7 File Offset: 0x000972B7
		[SecurityCritical]
		private void HandleError(int hr, string path)
		{
			base.Dispose();
			__Error.WinIOError(hr, path);
		}

		// Token: 0x06002AD7 RID: 10967 RVA: 0x000990C8 File Offset: 0x000972C8
		[SecurityCritical]
		private void AddSearchableDirsToStack(Directory.SearchData localSearchData)
		{
			string text = Path.InternalCombine(localSearchData.fullPath, "*");
			SafeFindHandle safeFindHandle = null;
			Win32Native.WIN32_FIND_DATA win32_FIND_DATA = new Win32Native.WIN32_FIND_DATA();
			try
			{
				int num;
				safeFindHandle = new SafeFindHandle(MonoIO.FindFirstFile(text, out win32_FIND_DATA.cFileName, out win32_FIND_DATA.dwFileAttributes, out num));
				if (safeFindHandle.IsInvalid)
				{
					int num2 = num;
					if (num2 == 2 || num2 == 18 || num2 == 3)
					{
						return;
					}
					this.HandleError(num2, localSearchData.fullPath);
				}
				int num3 = 0;
				do
				{
					if (FileSystemEnumerableHelpers.IsDir(win32_FIND_DATA))
					{
						string text2 = Path.InternalCombine(localSearchData.fullPath, win32_FIND_DATA.cFileName);
						string text3 = Path.InternalCombine(localSearchData.userPath, win32_FIND_DATA.cFileName);
						SearchOption searchOption = localSearchData.searchOption;
						Directory.SearchData searchData = new Directory.SearchData(text2, text3, searchOption);
						this.searchStack.Insert(num3++, searchData);
					}
				}
				while (MonoIO.FindNextFile(safeFindHandle.DangerousGetHandle(), out win32_FIND_DATA.cFileName, out win32_FIND_DATA.dwFileAttributes, out num));
			}
			finally
			{
				if (safeFindHandle != null)
				{
					safeFindHandle.Dispose();
				}
			}
		}

		// Token: 0x06002AD8 RID: 10968 RVA: 0x00002194 File Offset: 0x00000394
		[SecurityCritical]
		internal void DoDemand(string fullPathToDemand)
		{
		}

		// Token: 0x06002AD9 RID: 10969 RVA: 0x000991C4 File Offset: 0x000973C4
		private static string NormalizeSearchPattern(string searchPattern)
		{
			string text = searchPattern.TrimEnd(Path.TrimEndChars);
			if (text.Equals("."))
			{
				text = "*";
			}
			Path.CheckSearchPattern(text);
			return text;
		}

		// Token: 0x06002ADA RID: 10970 RVA: 0x000991F8 File Offset: 0x000973F8
		private static string GetNormalizedSearchCriteria(string fullSearchString, string fullPathMod)
		{
			string text;
			if (Path.IsDirectorySeparator(fullPathMod[fullPathMod.Length - 1]))
			{
				text = fullSearchString.Substring(fullPathMod.Length);
			}
			else
			{
				text = fullSearchString.Substring(fullPathMod.Length + 1);
			}
			return text;
		}

		// Token: 0x06002ADB RID: 10971 RVA: 0x0009923C File Offset: 0x0009743C
		private static string GetFullSearchString(string fullPath, string searchPattern)
		{
			string text = Path.InternalCombine(fullPath, searchPattern);
			char c = text[text.Length - 1];
			if (Path.IsDirectorySeparator(c) || c == Path.VolumeSeparatorChar)
			{
				text += "*";
			}
			return text;
		}

		// Token: 0x04001689 RID: 5769
		private const int STATE_INIT = 1;

		// Token: 0x0400168A RID: 5770
		private const int STATE_SEARCH_NEXT_DIR = 2;

		// Token: 0x0400168B RID: 5771
		private const int STATE_FIND_NEXT_FILE = 3;

		// Token: 0x0400168C RID: 5772
		private const int STATE_FINISH = 4;

		// Token: 0x0400168D RID: 5773
		private SearchResultHandler<TSource> _resultHandler;

		// Token: 0x0400168E RID: 5774
		private List<Directory.SearchData> searchStack;

		// Token: 0x0400168F RID: 5775
		private Directory.SearchData searchData;

		// Token: 0x04001690 RID: 5776
		private string searchCriteria;

		// Token: 0x04001691 RID: 5777
		[SecurityCritical]
		private SafeFindHandle _hnd;

		// Token: 0x04001692 RID: 5778
		private bool needsParentPathDiscoveryDemand;

		// Token: 0x04001693 RID: 5779
		private bool empty;

		// Token: 0x04001694 RID: 5780
		private string userPath;

		// Token: 0x04001695 RID: 5781
		private SearchOption searchOption;

		// Token: 0x04001696 RID: 5782
		private string fullPath;

		// Token: 0x04001697 RID: 5783
		private string normalizedSearchPath;

		// Token: 0x04001698 RID: 5784
		private bool _checkHost;
	}
}
