using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace SimpleFileBrowser
{
	// Token: 0x02000005 RID: 5
	public class FileBrowser : MonoBehaviour, IListViewAdapter
	{
		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000013 RID: 19 RVA: 0x000023F8 File Offset: 0x000005F8
		// (set) Token: 0x06000014 RID: 20 RVA: 0x000023FF File Offset: 0x000005FF
		public static bool IsOpen { get; private set; }

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000015 RID: 21 RVA: 0x00002407 File Offset: 0x00000607
		// (set) Token: 0x06000016 RID: 22 RVA: 0x0000240E File Offset: 0x0000060E
		public static bool Success { get; private set; }

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000017 RID: 23 RVA: 0x00002416 File Offset: 0x00000616
		// (set) Token: 0x06000018 RID: 24 RVA: 0x0000241D File Offset: 0x0000061D
		public static string Result { get; private set; }

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000019 RID: 25 RVA: 0x00002425 File Offset: 0x00000625
		// (set) Token: 0x0600001A RID: 26 RVA: 0x0000242C File Offset: 0x0000062C
		public static bool AskPermissions
		{
			get
			{
				return FileBrowser.m_askPermissions;
			}
			set
			{
				FileBrowser.m_askPermissions = value;
			}
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x0600001B RID: 27 RVA: 0x00002434 File Offset: 0x00000634
		// (set) Token: 0x0600001C RID: 28 RVA: 0x0000243B File Offset: 0x0000063B
		public static bool SingleClickMode
		{
			get
			{
				return FileBrowser.m_singleClickMode;
			}
			set
			{
				FileBrowser.m_singleClickMode = value;
			}
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x0600001D RID: 29 RVA: 0x00002444 File Offset: 0x00000644
		private static FileBrowser Instance
		{
			get
			{
				if (FileBrowser.m_instance == null)
				{
					FileBrowser.m_instance = global::UnityEngine.Object.Instantiate<GameObject>(Resources.Load<GameObject>("SimpleFileBrowserCanvas")).GetComponent<FileBrowser>();
					global::UnityEngine.Object.DontDestroyOnLoad(FileBrowser.m_instance.gameObject);
					FileBrowser.m_instance.gameObject.SetActive(false);
				}
				return FileBrowser.m_instance;
			}
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x0600001E RID: 30 RVA: 0x0000249B File Offset: 0x0000069B
		// (set) Token: 0x0600001F RID: 31 RVA: 0x000024A4 File Offset: 0x000006A4
		private string CurrentPath
		{
			get
			{
				return this.m_currentPath;
			}
			set
			{
				if (value != null)
				{
					value = this.GetPathWithoutTrailingDirectorySeparator(value.Trim());
				}
				if (value == null)
				{
					return;
				}
				if (this.m_currentPath != value)
				{
					if (!FileBrowserHelpers.DirectoryExists(value))
					{
						return;
					}
					this.m_currentPath = value;
					this.pathInputField.text = this.m_currentPath;
					if (this.currentPathIndex == -1 || this.pathsFollowed[this.currentPathIndex] != this.m_currentPath)
					{
						this.currentPathIndex++;
						if (this.currentPathIndex < this.pathsFollowed.Count)
						{
							this.pathsFollowed[this.currentPathIndex] = value;
							for (int i = this.pathsFollowed.Count - 1; i >= this.currentPathIndex + 1; i--)
							{
								this.pathsFollowed.RemoveAt(i);
							}
						}
						else
						{
							this.pathsFollowed.Add(this.m_currentPath);
						}
					}
					this.backButton.interactable = this.currentPathIndex > 0;
					this.forwardButton.interactable = this.currentPathIndex < this.pathsFollowed.Count - 1;
					this.upButton.interactable = Directory.GetParent(this.m_currentPath) != null;
					this.m_searchString = string.Empty;
					this.searchInputField.text = this.m_searchString;
					this.filesScrollRect.verticalNormalizedPosition = 1f;
					this.filenameImage.color = Color.white;
					if (this.m_folderSelectMode)
					{
						this.filenameInputField.text = string.Empty;
					}
				}
				this.RefreshFiles(true);
			}
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000020 RID: 32 RVA: 0x00002638 File Offset: 0x00000838
		// (set) Token: 0x06000021 RID: 33 RVA: 0x00002640 File Offset: 0x00000840
		private string SearchString
		{
			get
			{
				return this.m_searchString;
			}
			set
			{
				if (this.m_searchString != value)
				{
					this.m_searchString = value;
					this.searchInputField.text = this.m_searchString;
					this.RefreshFiles(false);
				}
			}
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000022 RID: 34 RVA: 0x0000266F File Offset: 0x0000086F
		public int SelectedFilePosition
		{
			get
			{
				return this.m_selectedFilePosition;
			}
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000023 RID: 35 RVA: 0x00002677 File Offset: 0x00000877
		// (set) Token: 0x06000024 RID: 36 RVA: 0x00002680 File Offset: 0x00000880
		private FileBrowserItem SelectedFile
		{
			get
			{
				return this.m_selectedFile;
			}
			set
			{
				if (value == null)
				{
					if (this.m_selectedFile != null)
					{
						this.m_selectedFile.Deselect();
					}
					this.m_selectedFilePosition = -1;
					this.m_selectedFile = null;
					return;
				}
				if (this.m_selectedFilePosition != value.Position)
				{
					if (this.m_selectedFile != null)
					{
						this.m_selectedFile.Deselect();
					}
					this.m_selectedFile = value;
					this.m_selectedFilePosition = value.Position;
					if (this.m_folderSelectMode || !this.m_selectedFile.IsDirectory)
					{
						this.filenameInputField.text = this.m_selectedFile.Name;
					}
					this.m_selectedFile.Select();
				}
			}
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000025 RID: 37 RVA: 0x0000272E File Offset: 0x0000092E
		// (set) Token: 0x06000026 RID: 38 RVA: 0x00002736 File Offset: 0x00000936
		private bool AcceptNonExistingFilename
		{
			get
			{
				return this.m_acceptNonExistingFilename;
			}
			set
			{
				this.m_acceptNonExistingFilename = value;
			}
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000027 RID: 39 RVA: 0x0000273F File Offset: 0x0000093F
		// (set) Token: 0x06000028 RID: 40 RVA: 0x00002748 File Offset: 0x00000948
		private bool FolderSelectMode
		{
			get
			{
				return this.m_folderSelectMode;
			}
			set
			{
				if (this.m_folderSelectMode != value)
				{
					this.m_folderSelectMode = value;
					if (this.m_folderSelectMode)
					{
						this.filtersDropdown.options[0].text = "Folders";
						this.filtersDropdown.value = 0;
						this.filtersDropdown.RefreshShownValue();
						this.filtersDropdown.interactable = false;
					}
					else
					{
						this.filtersDropdown.options[0].text = this.filters[0].ToString();
						this.filtersDropdown.interactable = true;
					}
					Text text = this.filenameInputField.placeholder as Text;
					if (text != null)
					{
						text.text = (this.m_folderSelectMode ? "" : "Filename");
					}
				}
			}
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000029 RID: 41 RVA: 0x00002817 File Offset: 0x00000A17
		// (set) Token: 0x0600002A RID: 42 RVA: 0x00002824 File Offset: 0x00000A24
		private string Title
		{
			get
			{
				return this.titleText.text;
			}
			set
			{
				this.titleText.text = value;
			}
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x0600002B RID: 43 RVA: 0x00002832 File Offset: 0x00000A32
		// (set) Token: 0x0600002C RID: 44 RVA: 0x0000283F File Offset: 0x00000A3F
		private string SubmitButtonText
		{
			get
			{
				return this.submitButtonText.text;
			}
			set
			{
				this.submitButtonText.text = value;
			}
		}

		// Token: 0x0600002D RID: 45 RVA: 0x00002850 File Offset: 0x00000A50
		private void Awake()
		{
			FileBrowser.m_instance = this;
			this.rectTransform = (RectTransform)base.transform;
			this.windowTR = (RectTransform)this.window.transform;
			this.ItemHeight = ((RectTransform)this.itemPrefab.transform).sizeDelta.y;
			this.nullPointerEventData = new PointerEventData(null);
			this.DEFAULT_PATH = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
			this.InitializeFiletypeIcons();
			this.filetypeIcons = null;
			FileBrowser.SetExcludedExtensions(this.excludeExtensions);
			this.excludeExtensions = null;
			this.backButton.interactable = false;
			this.forwardButton.interactable = false;
			this.upButton.interactable = false;
			InputField inputField = this.filenameInputField;
			inputField.onValidateInput = (InputField.OnValidateInput)Delegate.Combine(inputField.onValidateInput, new InputField.OnValidateInput(this.OnValidateFilenameInput));
			this.allFilesFilter = new FileBrowser.Filter("All Files (.*)");
			this.filters.Add(this.allFilesFilter);
			this.window.Initialize(this);
			this.listView.SetAdapter(this);
		}

		// Token: 0x0600002E RID: 46 RVA: 0x00002965 File Offset: 0x00000B65
		private void OnRectTransformDimensionsChange()
		{
			this.canvasDimensionsChanged = true;
		}

		// Token: 0x0600002F RID: 47 RVA: 0x0000296E File Offset: 0x00000B6E
		private void LateUpdate()
		{
			if (this.canvasDimensionsChanged)
			{
				this.canvasDimensionsChanged = false;
				this.EnsureWindowIsWithinBounds();
			}
		}

		// Token: 0x06000030 RID: 48 RVA: 0x00002985 File Offset: 0x00000B85
		private void OnApplicationFocus(bool focus)
		{
			if (focus)
			{
				this.RefreshFiles(true);
			}
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000031 RID: 49 RVA: 0x00002991 File Offset: 0x00000B91
		// (set) Token: 0x06000032 RID: 50 RVA: 0x00002994 File Offset: 0x00000B94
		public OnItemClickedHandler OnItemClicked
		{
			get
			{
				return null;
			}
			set
			{
			}
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000033 RID: 51 RVA: 0x00002996 File Offset: 0x00000B96
		public int Count
		{
			get
			{
				return this.validFileEntries.Count;
			}
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000034 RID: 52 RVA: 0x000029A3 File Offset: 0x00000BA3
		// (set) Token: 0x06000035 RID: 53 RVA: 0x000029AB File Offset: 0x00000BAB
		public float ItemHeight { get; private set; }

		// Token: 0x06000036 RID: 54 RVA: 0x000029B4 File Offset: 0x00000BB4
		public ListItem CreateItem()
		{
			FileBrowserItem fileBrowserItem = global::UnityEngine.Object.Instantiate<FileBrowserItem>(this.itemPrefab, this.filesContainer, false);
			fileBrowserItem.SetFileBrowser(this);
			return fileBrowserItem;
		}

		// Token: 0x06000037 RID: 55 RVA: 0x000029D0 File Offset: 0x00000BD0
		public void SetItemContent(ListItem item)
		{
			FileBrowserItem fileBrowserItem = (FileBrowserItem)item;
			FileSystemEntry fileSystemEntry = this.validFileEntries[item.Position];
			bool isDirectory = fileSystemEntry.IsDirectory;
			Sprite sprite;
			if (isDirectory)
			{
				sprite = this.folderIcon;
			}
			else if (!this.filetypeToIcon.TryGetValue(fileSystemEntry.Extension.ToLowerInvariant(), out sprite))
			{
				sprite = this.defaultIcon;
			}
			fileBrowserItem.SetFile(sprite, fileSystemEntry.Name, isDirectory);
			fileBrowserItem.SetHidden((fileSystemEntry.Attributes & FileAttributes.Hidden) == FileAttributes.Hidden);
			if (item.Position == this.m_selectedFilePosition)
			{
				this.m_selectedFile = fileBrowserItem;
				fileBrowserItem.Select();
				return;
			}
			fileBrowserItem.Deselect();
		}

		// Token: 0x06000038 RID: 56 RVA: 0x00002A6C File Offset: 0x00000C6C
		private void InitializeFiletypeIcons()
		{
			this.filetypeToIcon = new Dictionary<string, Sprite>();
			for (int i = 0; i < this.filetypeIcons.Length; i++)
			{
				FileBrowser.FiletypeIcon filetypeIcon = this.filetypeIcons[i];
				this.filetypeToIcon[filetypeIcon.extension] = filetypeIcon.icon;
			}
		}

		// Token: 0x06000039 RID: 57 RVA: 0x00002ABC File Offset: 0x00000CBC
		private void InitializeQuickLinks()
		{
			Vector2 vector = new Vector2(0f, -this.quickLinksContainer.sizeDelta.y);
			if (this.generateQuickLinksForDrives)
			{
				string[] logicalDrives = Directory.GetLogicalDrives();
				for (int i = 0; i < logicalDrives.Length; i++)
				{
					this.AddQuickLink(this.driveIcon, logicalDrives[i], logicalDrives[i], ref vector);
				}
			}
			for (int j = 0; j < this.quickLinks.Length; j++)
			{
				FileBrowser.QuickLink quickLink = this.quickLinks[j];
				string folderPath = Environment.GetFolderPath(quickLink.target);
				this.AddQuickLink(quickLink.icon, quickLink.name, folderPath, ref vector);
			}
			this.quickLinks = null;
			this.quickLinksContainer.sizeDelta = new Vector2(0f, -vector.y);
		}

		// Token: 0x0600003A RID: 58 RVA: 0x00002B84 File Offset: 0x00000D84
		public void OnBackButtonPressed()
		{
			if (this.currentPathIndex > 0)
			{
				List<string> list = this.pathsFollowed;
				int num = this.currentPathIndex - 1;
				this.currentPathIndex = num;
				this.CurrentPath = list[num];
			}
		}

		// Token: 0x0600003B RID: 59 RVA: 0x00002BBC File Offset: 0x00000DBC
		public void OnForwardButtonPressed()
		{
			if (this.currentPathIndex < this.pathsFollowed.Count - 1)
			{
				List<string> list = this.pathsFollowed;
				int num = this.currentPathIndex + 1;
				this.currentPathIndex = num;
				this.CurrentPath = list[num];
			}
		}

		// Token: 0x0600003C RID: 60 RVA: 0x00002C00 File Offset: 0x00000E00
		public void OnUpButtonPressed()
		{
			DirectoryInfo parent = Directory.GetParent(this.m_currentPath);
			if (parent != null)
			{
				this.CurrentPath = parent.FullName;
			}
		}

		// Token: 0x0600003D RID: 61 RVA: 0x00002C28 File Offset: 0x00000E28
		public void OnSubmitButtonClicked()
		{
			string text = this.m_currentPath;
			string text2 = this.filenameInputField.text.Trim();
			if (text2.Length > 0)
			{
				text = Path.Combine(text, text2);
			}
			if (File.Exists(text))
			{
				if (!this.m_folderSelectMode)
				{
					this.OnOperationSuccessful(text);
					return;
				}
				this.filenameImage.color = this.wrongFilenameColor;
				return;
			}
			else if (Directory.Exists(text))
			{
				if (this.m_folderSelectMode)
				{
					this.OnOperationSuccessful(text);
					return;
				}
				if (this.m_currentPath == text)
				{
					this.filenameImage.color = this.wrongFilenameColor;
					return;
				}
				this.CurrentPath = text;
				return;
			}
			else
			{
				if (this.m_acceptNonExistingFilename)
				{
					if (!this.m_folderSelectMode && this.filters[this.filtersDropdown.value].defaultExtension != null)
					{
						text = Path.ChangeExtension(text, this.filters[this.filtersDropdown.value].defaultExtension);
					}
					this.OnOperationSuccessful(text);
					return;
				}
				this.filenameImage.color = this.wrongFilenameColor;
				return;
			}
		}

		// Token: 0x0600003E RID: 62 RVA: 0x00002D30 File Offset: 0x00000F30
		public void OnCancelButtonClicked()
		{
			this.OnOperationCanceled(true);
		}

		// Token: 0x0600003F RID: 63 RVA: 0x00002D3C File Offset: 0x00000F3C
		private void OnOperationSuccessful(string path)
		{
			FileBrowser.Success = true;
			FileBrowser.Result = path;
			this.Hide();
			FileBrowser.OnSuccess onSuccess = this.onSuccess;
			this.onSuccess = null;
			this.onCancel = null;
			if (onSuccess != null)
			{
				onSuccess(path);
			}
		}

		// Token: 0x06000040 RID: 64 RVA: 0x00002D7C File Offset: 0x00000F7C
		private void OnOperationCanceled(bool invokeCancelCallback)
		{
			FileBrowser.Success = false;
			FileBrowser.Result = null;
			this.Hide();
			FileBrowser.OnCancel onCancel = this.onCancel;
			this.onSuccess = null;
			this.onCancel = null;
			if (invokeCancelCallback && onCancel != null)
			{
				onCancel();
			}
		}

		// Token: 0x06000041 RID: 65 RVA: 0x00002DBC File Offset: 0x00000FBC
		public void OnPathChanged(string newPath)
		{
			this.CurrentPath = newPath;
		}

		// Token: 0x06000042 RID: 66 RVA: 0x00002DC5 File Offset: 0x00000FC5
		public void OnSearchStringChanged(string newSearchString)
		{
			this.SearchString = newSearchString;
		}

		// Token: 0x06000043 RID: 67 RVA: 0x00002DCE File Offset: 0x00000FCE
		public void OnFilterChanged()
		{
			this.RefreshFiles(false);
		}

		// Token: 0x06000044 RID: 68 RVA: 0x00002DCE File Offset: 0x00000FCE
		public void OnShowHiddenFilesToggleChanged()
		{
			this.RefreshFiles(false);
		}

		// Token: 0x06000045 RID: 69 RVA: 0x00002DD7 File Offset: 0x00000FD7
		public void OnQuickLinkSelected(FileBrowserQuickLink quickLink)
		{
			if (quickLink != null)
			{
				this.CurrentPath = quickLink.TargetPath;
			}
		}

		// Token: 0x06000046 RID: 70 RVA: 0x00002DEE File Offset: 0x00000FEE
		public void OnItemSelected(FileBrowserItem item)
		{
			this.SelectedFile = item;
		}

		// Token: 0x06000047 RID: 71 RVA: 0x00002DF7 File Offset: 0x00000FF7
		public void OnItemOpened(FileBrowserItem item)
		{
			if (item.IsDirectory)
			{
				this.CurrentPath = Path.Combine(this.m_currentPath, item.Name);
				return;
			}
			this.OnSubmitButtonClicked();
		}

		// Token: 0x06000048 RID: 72 RVA: 0x00002E1F File Offset: 0x0000101F
		public char OnValidateFilenameInput(string text, int charIndex, char addedChar)
		{
			if (addedChar == '\n')
			{
				this.OnSubmitButtonClicked();
				return '\0';
			}
			return addedChar;
		}

		// Token: 0x06000049 RID: 73 RVA: 0x00002E30 File Offset: 0x00001030
		public void Show(string initialPath)
		{
			if (FileBrowser.AskPermissions)
			{
				FileBrowser.RequestPermission();
			}
			if (!FileBrowser.quickLinksInitialized)
			{
				FileBrowser.quickLinksInitialized = true;
				this.InitializeQuickLinks();
			}
			this.SelectedFile = null;
			this.m_searchString = string.Empty;
			this.searchInputField.text = this.m_searchString;
			this.filesScrollRect.verticalNormalizedPosition = 1f;
			this.filenameInputField.text = string.Empty;
			this.filenameImage.color = Color.white;
			FileBrowser.IsOpen = true;
			FileBrowser.Success = false;
			FileBrowser.Result = null;
			base.gameObject.SetActive(true);
			this.CurrentPath = this.GetInitialPath(initialPath);
		}

		// Token: 0x0600004A RID: 74 RVA: 0x00002EDC File Offset: 0x000010DC
		public void Hide()
		{
			FileBrowser.IsOpen = false;
			this.currentPathIndex = -1;
			this.pathsFollowed.Clear();
			this.backButton.interactable = false;
			this.forwardButton.interactable = false;
			this.upButton.interactable = false;
			base.gameObject.SetActive(false);
		}

		// Token: 0x0600004B RID: 75 RVA: 0x00002F34 File Offset: 0x00001134
		public void RefreshFiles(bool pathChanged)
		{
			if (pathChanged)
			{
				if (!string.IsNullOrEmpty(this.m_currentPath))
				{
					this.allFileEntries = FileBrowserHelpers.GetEntriesInDirectory(this.m_currentPath);
				}
				else
				{
					this.allFileEntries = null;
				}
			}
			this.SelectedFile = null;
			if (!this.showHiddenFilesToggle.isOn)
			{
				this.ignoredFileAttributes |= FileAttributes.Hidden;
			}
			else
			{
				this.ignoredFileAttributes &= ~FileAttributes.Hidden;
			}
			string text = this.m_searchString.ToLower();
			this.validFileEntries.Clear();
			if (this.allFileEntries != null)
			{
				for (int i = 0; i < this.allFileEntries.Length; i++)
				{
					try
					{
						FileSystemEntry fileSystemEntry = this.allFileEntries[i];
						if (!fileSystemEntry.IsDirectory)
						{
							if (this.m_folderSelectMode)
							{
								goto IL_014D;
							}
							if ((fileSystemEntry.Attributes & this.ignoredFileAttributes) != (FileAttributes)0)
							{
								goto IL_014D;
							}
							string text2 = fileSystemEntry.Extension.ToLowerInvariant();
							if (this.excludedExtensionsSet.Contains(text2))
							{
								goto IL_014D;
							}
							HashSet<string> extensions = this.filters[this.filtersDropdown.value].extensions;
							if (extensions != null && !extensions.Contains(text2))
							{
								goto IL_014D;
							}
						}
						else if ((fileSystemEntry.Attributes & this.ignoredFileAttributes) != (FileAttributes)0)
						{
							goto IL_014D;
						}
						if (this.m_searchString.Length == 0 || fileSystemEntry.Name.ToLower().Contains(text))
						{
							this.validFileEntries.Add(fileSystemEntry);
						}
					}
					catch (Exception ex)
					{
						Debug.LogException(ex);
					}
					IL_014D:;
				}
			}
			this.listView.UpdateList();
			this.filesScrollRect.OnScroll(this.nullPointerEventData);
		}

		// Token: 0x0600004C RID: 76 RVA: 0x000030CC File Offset: 0x000012CC
		private bool AddQuickLink(Sprite icon, string name, string path, ref Vector2 anchoredPos)
		{
			if (string.IsNullOrEmpty(path))
			{
				return false;
			}
			if (!Directory.Exists(path))
			{
				return false;
			}
			if (this.addedQuickLinksSet.Contains(path))
			{
				return false;
			}
			FileBrowserQuickLink fileBrowserQuickLink = global::UnityEngine.Object.Instantiate<FileBrowserQuickLink>(this.quickLinkPrefab, this.quickLinksContainer, false);
			fileBrowserQuickLink.SetFileBrowser(this);
			if (icon != null)
			{
				fileBrowserQuickLink.SetQuickLink(icon, name, path);
			}
			else
			{
				fileBrowserQuickLink.SetQuickLink(this.folderIcon, name, path);
			}
			fileBrowserQuickLink.TransformComponent.anchoredPosition = anchoredPos;
			anchoredPos.y -= this.ItemHeight;
			this.addedQuickLinksSet.Add(path);
			return true;
		}

		// Token: 0x0600004D RID: 77 RVA: 0x0000316C File Offset: 0x0000136C
		public void EnsureWindowIsWithinBounds()
		{
			Vector2 sizeDelta = this.rectTransform.sizeDelta;
			Vector2 sizeDelta2 = this.windowTR.sizeDelta;
			if (sizeDelta2.x > sizeDelta.x)
			{
				sizeDelta2.x = sizeDelta.x;
			}
			if (sizeDelta2.y > sizeDelta.y)
			{
				sizeDelta2.y = sizeDelta.y;
			}
			Vector2 anchoredPosition = this.windowTR.anchoredPosition;
			Vector2 vector = sizeDelta * 0.5f;
			Vector2 vector2 = sizeDelta2 * 0.5f;
			Vector2 vector3 = anchoredPosition - vector2 + vector;
			Vector2 vector4 = anchoredPosition + vector2 + vector;
			if (vector3.x < 0f)
			{
				anchoredPosition.x -= vector3.x;
			}
			else if (vector4.x > sizeDelta.x)
			{
				anchoredPosition.x -= vector4.x - sizeDelta.x;
			}
			if (vector3.y < 0f)
			{
				anchoredPosition.y -= vector3.y;
			}
			else if (vector4.y > sizeDelta.y)
			{
				anchoredPosition.y -= vector4.y - sizeDelta.y;
			}
			this.windowTR.anchoredPosition = anchoredPosition;
			this.windowTR.sizeDelta = sizeDelta2;
		}

		// Token: 0x0600004E RID: 78 RVA: 0x000032B8 File Offset: 0x000014B8
		private string GetPathWithoutTrailingDirectorySeparator(string path)
		{
			if (string.IsNullOrEmpty(path))
			{
				return null;
			}
			try
			{
				if (Path.GetDirectoryName(path) != null)
				{
					char c = path[path.Length - 1];
					if (c == Path.DirectorySeparatorChar || c == Path.AltDirectorySeparatorChar)
					{
						path = path.Substring(0, path.Length - 1);
					}
				}
			}
			catch
			{
				return null;
			}
			return path;
		}

		// Token: 0x0600004F RID: 79 RVA: 0x00003324 File Offset: 0x00001524
		private int CalculateLengthOfDropdownText(string str)
		{
			int num = 0;
			Font font = this.filterItemTemplate.font;
			CharacterInfo characterInfo = default(CharacterInfo);
			font.RequestCharactersInTexture(str, this.filterItemTemplate.fontSize, this.filterItemTemplate.fontStyle);
			for (int i = 0; i < str.Length; i++)
			{
				if (!font.GetCharacterInfo(str[i], out characterInfo, this.filterItemTemplate.fontSize))
				{
					num += 5;
				}
				num += characterInfo.advance;
			}
			return num;
		}

		// Token: 0x06000050 RID: 80 RVA: 0x000033A0 File Offset: 0x000015A0
		private string GetInitialPath(string initialPath)
		{
			if (string.IsNullOrEmpty(initialPath) || !Directory.Exists(initialPath))
			{
				if (this.CurrentPath.Length == 0)
				{
					initialPath = this.DEFAULT_PATH;
				}
				else
				{
					initialPath = this.CurrentPath;
				}
			}
			this.m_currentPath = string.Empty;
			return initialPath;
		}

		// Token: 0x06000051 RID: 81 RVA: 0x000033E0 File Offset: 0x000015E0
		public static bool ShowSaveDialog(FileBrowser.OnSuccess onSuccess, FileBrowser.OnCancel onCancel, bool folderMode = false, string initialPath = null, string title = "Save", string saveButtonText = "Save")
		{
			if (FileBrowser.Instance.gameObject.activeSelf)
			{
				Debug.LogError("Error: Multiple dialogs are not allowed!");
				return false;
			}
			FileBrowser.Instance.onSuccess = onSuccess;
			FileBrowser.Instance.onCancel = onCancel;
			FileBrowser.Instance.FolderSelectMode = folderMode;
			FileBrowser.Instance.Title = title;
			FileBrowser.Instance.SubmitButtonText = saveButtonText;
			FileBrowser.Instance.AcceptNonExistingFilename = !folderMode;
			FileBrowser.Instance.Show(initialPath);
			return true;
		}

		// Token: 0x06000052 RID: 82 RVA: 0x00003460 File Offset: 0x00001660
		public static bool ShowLoadDialog(FileBrowser.OnSuccess onSuccess, FileBrowser.OnCancel onCancel, bool folderMode = false, string initialPath = null, string title = "Load", string loadButtonText = "Select")
		{
			if (FileBrowser.Instance.gameObject.activeSelf)
			{
				Debug.LogError("Error: Multiple dialogs are not allowed!");
				return false;
			}
			FileBrowser.Instance.onSuccess = onSuccess;
			FileBrowser.Instance.onCancel = onCancel;
			FileBrowser.Instance.FolderSelectMode = folderMode;
			FileBrowser.Instance.Title = title;
			FileBrowser.Instance.SubmitButtonText = loadButtonText;
			FileBrowser.Instance.AcceptNonExistingFilename = false;
			FileBrowser.Instance.Show(initialPath);
			return true;
		}

		// Token: 0x06000053 RID: 83 RVA: 0x000034DA File Offset: 0x000016DA
		public static void HideDialog(bool invokeCancelCallback = false)
		{
			FileBrowser.Instance.OnOperationCanceled(invokeCancelCallback);
		}

		// Token: 0x06000054 RID: 84 RVA: 0x000034E7 File Offset: 0x000016E7
		public static IEnumerator WaitForSaveDialog(bool folderMode = false, string initialPath = null, string title = "Save", string saveButtonText = "Save")
		{
			if (!FileBrowser.ShowSaveDialog(null, null, folderMode, initialPath, title, saveButtonText))
			{
				yield break;
			}
			while (FileBrowser.Instance.gameObject.activeSelf)
			{
				yield return null;
			}
			yield break;
		}

		// Token: 0x06000055 RID: 85 RVA: 0x0000350B File Offset: 0x0000170B
		public static IEnumerator WaitForLoadDialog(bool folderMode = false, string initialPath = null, string title = "Load", string loadButtonText = "Select")
		{
			if (!FileBrowser.ShowLoadDialog(null, null, folderMode, initialPath, title, loadButtonText))
			{
				yield break;
			}
			while (FileBrowser.Instance.gameObject.activeSelf)
			{
				yield return null;
			}
			yield break;
		}

		// Token: 0x06000056 RID: 86 RVA: 0x00003530 File Offset: 0x00001730
		public static bool AddQuickLink(string name, string path, Sprite icon = null)
		{
			if (!FileBrowser.quickLinksInitialized)
			{
				FileBrowser.quickLinksInitialized = true;
				if (FileBrowser.AskPermissions)
				{
					FileBrowser.RequestPermission();
				}
				FileBrowser.Instance.InitializeQuickLinks();
			}
			Vector2 vector = new Vector2(0f, -FileBrowser.Instance.quickLinksContainer.sizeDelta.y);
			if (FileBrowser.Instance.AddQuickLink(icon, name, path, ref vector))
			{
				FileBrowser.Instance.quickLinksContainer.sizeDelta = new Vector2(0f, -vector.y);
				return true;
			}
			return false;
		}

		// Token: 0x06000057 RID: 87 RVA: 0x000035B8 File Offset: 0x000017B8
		public static void SetExcludedExtensions(params string[] excludedExtensions)
		{
			FileBrowser.Instance.excludedExtensionsSet.Clear();
			if (excludedExtensions != null)
			{
				for (int i = 0; i < excludedExtensions.Length; i++)
				{
					FileBrowser.Instance.excludedExtensionsSet.Add(excludedExtensions[i].ToLowerInvariant());
				}
			}
		}

		// Token: 0x06000058 RID: 88 RVA: 0x00003600 File Offset: 0x00001800
		public static void SetFilters(bool showAllFilesFilter, IEnumerable<string> filters)
		{
			FileBrowser.SetFiltersPreProcessing(showAllFilesFilter);
			if (filters != null)
			{
				foreach (string text in filters)
				{
					if (text != null && text.Length > 0)
					{
						FileBrowser.Instance.filters.Add(new FileBrowser.Filter(null, text));
					}
				}
			}
			FileBrowser.SetFiltersPostProcessing();
		}

		// Token: 0x06000059 RID: 89 RVA: 0x00003674 File Offset: 0x00001874
		public static void SetFilters(bool showAllFilesFilter, params string[] filters)
		{
			FileBrowser.SetFiltersPreProcessing(showAllFilesFilter);
			if (filters != null)
			{
				for (int i = 0; i < filters.Length; i++)
				{
					if (filters[i] != null && filters[i].Length > 0)
					{
						FileBrowser.Instance.filters.Add(new FileBrowser.Filter(null, filters[i]));
					}
				}
			}
			FileBrowser.SetFiltersPostProcessing();
		}

		// Token: 0x0600005A RID: 90 RVA: 0x000036C8 File Offset: 0x000018C8
		public static void SetFilters(bool showAllFilesFilter, IEnumerable<FileBrowser.Filter> filters)
		{
			FileBrowser.SetFiltersPreProcessing(showAllFilesFilter);
			if (filters != null)
			{
				foreach (FileBrowser.Filter filter in filters)
				{
					if (filter != null && filter.defaultExtension.Length > 0)
					{
						FileBrowser.Instance.filters.Add(filter);
					}
				}
			}
			FileBrowser.SetFiltersPostProcessing();
		}

		// Token: 0x0600005B RID: 91 RVA: 0x00003738 File Offset: 0x00001938
		public static void SetFilters(bool showAllFilesFilter, params FileBrowser.Filter[] filters)
		{
			FileBrowser.SetFiltersPreProcessing(showAllFilesFilter);
			if (filters != null)
			{
				for (int i = 0; i < filters.Length; i++)
				{
					if (filters[i] != null && filters[i].defaultExtension.Length > 0)
					{
						FileBrowser.Instance.filters.Add(filters[i]);
					}
				}
			}
			FileBrowser.SetFiltersPostProcessing();
		}

		// Token: 0x0600005C RID: 92 RVA: 0x00003788 File Offset: 0x00001988
		private static void SetFiltersPreProcessing(bool showAllFilesFilter)
		{
			FileBrowser.Instance.showAllFilesFilter = showAllFilesFilter;
			FileBrowser.Instance.filters.Clear();
			if (showAllFilesFilter)
			{
				FileBrowser.Instance.filters.Add(FileBrowser.Instance.allFilesFilter);
			}
		}

		// Token: 0x0600005D RID: 93 RVA: 0x000037C0 File Offset: 0x000019C0
		private static void SetFiltersPostProcessing()
		{
			List<FileBrowser.Filter> list = FileBrowser.Instance.filters;
			if (list.Count == 0)
			{
				list.Add(FileBrowser.Instance.allFilesFilter);
			}
			int num = 100;
			List<string> list2 = new List<string>(list.Count);
			for (int i = 0; i < list.Count; i++)
			{
				string text = list[i].ToString();
				list2.Add(text);
				num = Mathf.Max(num, FileBrowser.Instance.CalculateLengthOfDropdownText(text));
			}
			Vector2 sizeDelta = FileBrowser.Instance.filtersDropdownContainer.sizeDelta;
			sizeDelta.x = (float)(num + 28);
			FileBrowser.Instance.filtersDropdownContainer.sizeDelta = sizeDelta;
			FileBrowser.Instance.filtersDropdown.ClearOptions();
			FileBrowser.Instance.filtersDropdown.AddOptions(list2);
			FileBrowser.Instance.filtersDropdown.value = 0;
		}

		// Token: 0x0600005E RID: 94 RVA: 0x0000389C File Offset: 0x00001A9C
		public static bool SetDefaultFilter(string defaultFilter)
		{
			if (defaultFilter != null)
			{
				defaultFilter = defaultFilter.ToLowerInvariant();
				for (int i = 0; i < FileBrowser.Instance.filters.Count; i++)
				{
					HashSet<string> extensions = FileBrowser.Instance.filters[i].extensions;
					if (extensions != null && extensions.Contains(defaultFilter))
					{
						FileBrowser.Instance.filtersDropdown.value = i;
						FileBrowser.Instance.filtersDropdown.RefreshShownValue();
						return true;
					}
				}
				return false;
			}
			if (FileBrowser.Instance.showAllFilesFilter)
			{
				FileBrowser.Instance.filtersDropdown.value = 0;
				FileBrowser.Instance.filtersDropdown.RefreshShownValue();
				return true;
			}
			return false;
		}

		// Token: 0x0600005F RID: 95 RVA: 0x00003941 File Offset: 0x00001B41
		public static FileBrowser.Permission CheckPermission()
		{
			return FileBrowser.Permission.Granted;
		}

		// Token: 0x06000060 RID: 96 RVA: 0x00003941 File Offset: 0x00001B41
		public static FileBrowser.Permission RequestPermission()
		{
			return FileBrowser.Permission.Granted;
		}

		// Token: 0x04000007 RID: 7
		private const string ALL_FILES_FILTER_TEXT = "All Files (.*)";

		// Token: 0x04000008 RID: 8
		private const string FOLDERS_FILTER_TEXT = "Folders";

		// Token: 0x04000009 RID: 9
		private string DEFAULT_PATH;

		// Token: 0x0400000D RID: 13
		private static bool m_askPermissions = true;

		// Token: 0x0400000E RID: 14
		private static bool m_singleClickMode = false;

		// Token: 0x0400000F RID: 15
		private static FileBrowser m_instance = null;

		// Token: 0x04000010 RID: 16
		[Header("References")]
		[SerializeField]
		private FileBrowserMovement window;

		// Token: 0x04000011 RID: 17
		private RectTransform windowTR;

		// Token: 0x04000012 RID: 18
		[SerializeField]
		private FileBrowserItem itemPrefab;

		// Token: 0x04000013 RID: 19
		[SerializeField]
		private FileBrowserQuickLink quickLinkPrefab;

		// Token: 0x04000014 RID: 20
		[SerializeField]
		private Text titleText;

		// Token: 0x04000015 RID: 21
		[SerializeField]
		private Button backButton;

		// Token: 0x04000016 RID: 22
		[SerializeField]
		private Button forwardButton;

		// Token: 0x04000017 RID: 23
		[SerializeField]
		private Button upButton;

		// Token: 0x04000018 RID: 24
		[SerializeField]
		private InputField pathInputField;

		// Token: 0x04000019 RID: 25
		[SerializeField]
		private InputField searchInputField;

		// Token: 0x0400001A RID: 26
		[SerializeField]
		private RectTransform quickLinksContainer;

		// Token: 0x0400001B RID: 27
		[SerializeField]
		private RectTransform filesContainer;

		// Token: 0x0400001C RID: 28
		[SerializeField]
		private ScrollRect filesScrollRect;

		// Token: 0x0400001D RID: 29
		[SerializeField]
		private RecycledListView listView;

		// Token: 0x0400001E RID: 30
		[SerializeField]
		private InputField filenameInputField;

		// Token: 0x0400001F RID: 31
		[SerializeField]
		private Image filenameImage;

		// Token: 0x04000020 RID: 32
		[SerializeField]
		private Dropdown filtersDropdown;

		// Token: 0x04000021 RID: 33
		[SerializeField]
		private RectTransform filtersDropdownContainer;

		// Token: 0x04000022 RID: 34
		[SerializeField]
		private Text filterItemTemplate;

		// Token: 0x04000023 RID: 35
		[SerializeField]
		private Toggle showHiddenFilesToggle;

		// Token: 0x04000024 RID: 36
		[SerializeField]
		private Text submitButtonText;

		// Token: 0x04000025 RID: 37
		[Header("Icons")]
		[SerializeField]
		private Sprite folderIcon;

		// Token: 0x04000026 RID: 38
		[SerializeField]
		private Sprite driveIcon;

		// Token: 0x04000027 RID: 39
		[SerializeField]
		private Sprite defaultIcon;

		// Token: 0x04000028 RID: 40
		[SerializeField]
		private FileBrowser.FiletypeIcon[] filetypeIcons;

		// Token: 0x04000029 RID: 41
		private Dictionary<string, Sprite> filetypeToIcon;

		// Token: 0x0400002A RID: 42
		[Header("Other")]
		public Color normalFileColor = Color.white;

		// Token: 0x0400002B RID: 43
		public Color hoveredFileColor = new Color32(225, 225, byte.MaxValue, byte.MaxValue);

		// Token: 0x0400002C RID: 44
		public Color selectedFileColor = new Color32(0, 175, byte.MaxValue, byte.MaxValue);

		// Token: 0x0400002D RID: 45
		public Color wrongFilenameColor = new Color32(byte.MaxValue, 100, 100, byte.MaxValue);

		// Token: 0x0400002E RID: 46
		public int minWidth = 380;

		// Token: 0x0400002F RID: 47
		public int minHeight = 300;

		// Token: 0x04000030 RID: 48
		[SerializeField]
		private string[] excludeExtensions;

		// Token: 0x04000031 RID: 49
		[SerializeField]
		private FileBrowser.QuickLink[] quickLinks;

		// Token: 0x04000032 RID: 50
		private static bool quickLinksInitialized;

		// Token: 0x04000033 RID: 51
		private readonly HashSet<string> excludedExtensionsSet = new HashSet<string>();

		// Token: 0x04000034 RID: 52
		private readonly HashSet<string> addedQuickLinksSet = new HashSet<string>();

		// Token: 0x04000035 RID: 53
		[SerializeField]
		private bool generateQuickLinksForDrives = true;

		// Token: 0x04000036 RID: 54
		private RectTransform rectTransform;

		// Token: 0x04000037 RID: 55
		private FileAttributes ignoredFileAttributes = FileAttributes.System;

		// Token: 0x04000038 RID: 56
		private FileSystemEntry[] allFileEntries;

		// Token: 0x04000039 RID: 57
		private readonly List<FileSystemEntry> validFileEntries = new List<FileSystemEntry>();

		// Token: 0x0400003A RID: 58
		private readonly List<FileBrowser.Filter> filters = new List<FileBrowser.Filter>();

		// Token: 0x0400003B RID: 59
		private FileBrowser.Filter allFilesFilter;

		// Token: 0x0400003C RID: 60
		private bool showAllFilesFilter = true;

		// Token: 0x0400003D RID: 61
		private int currentPathIndex = -1;

		// Token: 0x0400003E RID: 62
		private readonly List<string> pathsFollowed = new List<string>();

		// Token: 0x0400003F RID: 63
		private bool canvasDimensionsChanged;

		// Token: 0x04000040 RID: 64
		private PointerEventData nullPointerEventData;

		// Token: 0x04000041 RID: 65
		private string m_currentPath = string.Empty;

		// Token: 0x04000042 RID: 66
		private string m_searchString = string.Empty;

		// Token: 0x04000043 RID: 67
		private int m_selectedFilePosition = -1;

		// Token: 0x04000044 RID: 68
		private FileBrowserItem m_selectedFile;

		// Token: 0x04000045 RID: 69
		private bool m_acceptNonExistingFilename;

		// Token: 0x04000046 RID: 70
		private bool m_folderSelectMode;

		// Token: 0x04000047 RID: 71
		private FileBrowser.OnSuccess onSuccess;

		// Token: 0x04000048 RID: 72
		private FileBrowser.OnCancel onCancel;

		// Token: 0x020001D0 RID: 464
		public enum Permission
		{
			// Token: 0x04000ADB RID: 2779
			Denied,
			// Token: 0x04000ADC RID: 2780
			Granted,
			// Token: 0x04000ADD RID: 2781
			ShouldAsk
		}

		// Token: 0x020001D1 RID: 465
		[Serializable]
		private struct FiletypeIcon
		{
			// Token: 0x04000ADE RID: 2782
			public string extension;

			// Token: 0x04000ADF RID: 2783
			public Sprite icon;
		}

		// Token: 0x020001D2 RID: 466
		[Serializable]
		private struct QuickLink
		{
			// Token: 0x04000AE0 RID: 2784
			public Environment.SpecialFolder target;

			// Token: 0x04000AE1 RID: 2785
			public string name;

			// Token: 0x04000AE2 RID: 2786
			public Sprite icon;
		}

		// Token: 0x020001D3 RID: 467
		public class Filter
		{
			// Token: 0x06000B9E RID: 2974 RVA: 0x00025328 File Offset: 0x00023528
			internal Filter(string name)
			{
				this.name = name;
				this.extensions = null;
				this.defaultExtension = null;
			}

			// Token: 0x06000B9F RID: 2975 RVA: 0x00025345 File Offset: 0x00023545
			public Filter(string name, string extension)
			{
				this.name = name;
				extension = extension.ToLowerInvariant();
				this.extensions = new HashSet<string> { extension };
				this.defaultExtension = extension;
			}

			// Token: 0x06000BA0 RID: 2976 RVA: 0x00025378 File Offset: 0x00023578
			public Filter(string name, params string[] extensions)
			{
				this.name = name;
				for (int i = 0; i < extensions.Length; i++)
				{
					extensions[i] = extensions[i].ToLowerInvariant();
				}
				this.extensions = new HashSet<string>(extensions);
				this.defaultExtension = extensions[0];
			}

			// Token: 0x06000BA1 RID: 2977 RVA: 0x000253C0 File Offset: 0x000235C0
			public override string ToString()
			{
				string text = "";
				if (this.name != null)
				{
					text += this.name;
				}
				if (this.extensions != null)
				{
					if (this.name != null)
					{
						text += " (";
					}
					int num = 0;
					foreach (string text2 in this.extensions)
					{
						if (num++ > 0)
						{
							text = text + ", " + text2;
						}
						else
						{
							text += text2;
						}
					}
					if (this.name != null)
					{
						text += ")";
					}
				}
				return text;
			}

			// Token: 0x04000AE3 RID: 2787
			public readonly string name;

			// Token: 0x04000AE4 RID: 2788
			public readonly HashSet<string> extensions;

			// Token: 0x04000AE5 RID: 2789
			public readonly string defaultExtension;
		}

		// Token: 0x020001D4 RID: 468
		// (Invoke) Token: 0x06000BA3 RID: 2979
		public delegate void OnSuccess(string path);

		// Token: 0x020001D5 RID: 469
		// (Invoke) Token: 0x06000BA7 RID: 2983
		public delegate void OnCancel();
	}
}
