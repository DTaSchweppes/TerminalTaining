using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DTaSchweppes.Tools
{
 public class TerminalManager : MonoBehaviour
 {
		[SerializeField] private InputField _consoleInput;
		[SerializeField] private Text _consolePath;
		[SerializeField] private Text _consoleText;
		[SerializeField] private Text _consoleUser;
		[SerializeField] private List<FileSystemElement> _foldersList;
		[SerializeField] private Text _fileContent;
		[SerializeField] private AudioSource _startCommand;
		[SerializeField] private AudioSource _createCommand;
		[SerializeField] private AudioSource _openTextFile;
		private string _currentSystemUser = "usr1";
		private string _allFolders = " ";
		private string _currentPath ="/";
		private int _countPathFinded = 0;
		private string _currentDate = "Tue Nov 1 2021";

        private void Awake()
        {
			_consoleUser.text = ">"+_currentSystemUser+ "@linux:~";
		}
        void Update()
		{
			if (Input.GetKeyDown(KeyCode.Return))
				Open();
		}

		public void OpenFolderCD()
			{
				_consoleText.text = "";
				string[] comands = _consoleInput.text.Split(' ');
				var pathForCD = _currentPath;
				print("Путь для поиска " + pathForCD);
				_countPathFinded = 0;
				foreach (Folder folder in _foldersList)
				{
					if (folder.GetPathWhere(pathForCD) != "NotFOD" && folder.GetPathWhere(pathForCD)== comands[1])
					{
						_countPathFinded++;
						print("Нашли папку созданную из " + _currentPath);
						folder.GetPathWhere(pathForCD);
						_currentPath += comands[1]+'/';
					}
				}
				if (_countPathFinded == 0)
				{
					_consoleText.text = "No such file or directory";
				}
				_consolePath.text = _currentPath;
			}

		public void CommandCDExitCurrentFolder()
        {
			string[] pathLocal = _currentPath.Split('/');
			_currentPath = _currentPath.Replace(pathLocal[pathLocal.Length - 2] + "/", "");//переход путем корректировки current path
			if (pathLocal.Length<4) //в корневой каталог
            {
				_currentPath = "/";
			}
			_consolePath.text = _currentPath;
		}
		public void CommandLS()
        {
			_allFolders = "";
			string[] comands = _consoleInput.text.Split(' ');
			_countPathFinded = 0;
			foreach (FileSystemElement filesystem in _foldersList)
			{
				if (filesystem.GetPathWhere(_currentPath) != "NotFOD")
				{
					_countPathFinded++;
					print("LS: Нашли папку созданную из " + _currentPath + " это " + filesystem.NameSelf);
					//_allFolders += folder.GetName() + " ";
					_allFolders += filesystem.NameSelf + " ";
				}
			}
			_consoleText.text = _allFolders;
		}
		public void CommandLSWithFlag()
        {
			_allFolders = "";
			string[] comands = _consoleInput.text.Split(' ');
			_countPathFinded = 0;
			foreach (FileSystemElement filesystem in _foldersList)
			{
				if (filesystem.GetPathWhere(_currentPath) != "NotFOD")
				{
					_countPathFinded++;
					print("LS: Нашли папку созданную из " + _currentPath + " это " + filesystem.NameSelf);
					//_allFolders += folder.GetName() + " ";
					_allFolders += filesystem.NameSelf + " " +filesystem.UserOwner + " "+filesystem.CreateDate+"\n";
				}
			}
			_fileContent.text = _allFolders;
		}
		public void CommandMkDir()
        {
			string[] comands = _consoleInput.text.Split(' ');
			Folder newFolder = new Folder(comands[1], _currentPath, _currentSystemUser, _currentDate);//Создаем объект класса Folder 
			print("Передаваемый при создании " + _currentPath);
			print("Создана папка " + newFolder.NameSelf);
			_foldersList.Add(newFolder); //добавляем созданный объект класса Folder в лист класса Folder
		}

		public void CommandTouch()
		{
			string[] comands = _consoleInput.text.Split(' ');
			File newFolder = new File(comands[1], _currentPath,_currentSystemUser, _currentDate);//Создаем объект класса Folder 
			print("Передаваемый при создании " + _currentPath);
			print("Создана папка " + newFolder.NameSelf);
			_foldersList.Add(newFolder); //добавляем созданный объект класса Folder в лист класса Folder
		}

		public void CommandRMDir()
        {
			string[] comands = _consoleInput.text.Split(' ');
			_foldersList.RemoveAll(folder => folder.NameSelf == comands[1] && folder.PathFromWhereCreate == _currentPath); //проверяем у папки имя как в кмд, и то что она находится по пути, который открыт в терминале
		}

		public void CommandCat()
        {
			_consoleText.text = "";
			string[] comands = _consoleInput.text.Split(' ');
			var pathForCD = _currentPath;
			print("Путь для поиска " + pathForCD);
			_countPathFinded = 0;
			foreach (File file in _foldersList)
			{
				if (file.GetPathWhere(pathForCD) != "NotFOD" && file.GetPathWhere(pathForCD) == comands[1])
				{
					_countPathFinded++;
					_fileContent.text=file.FileContent;
				}
			}
			if (_countPathFinded == 0)
			{
				_consoleText.text = "No such file";
			}
		}
		public void CommandRmFile()
        {
			string[] comands = _consoleInput.text.Split(' ');
			_foldersList.RemoveAll(file => file.NameSelf == comands[1] && file.PathFromWhereCreate == _currentPath);
		}
		public void Open() //метод для ввода
		{
			if (_consoleInput.text.Contains("cd") && (_consoleInput.text.Contains("..")))
			{
				_startCommand.Play();
				CommandCDExitCurrentFolder();
			}
			else if (_consoleInput.text.Contains("cd") && !(_consoleInput.text.Contains("..")))
			{
				_startCommand.Play();
				OpenFolderCD();
			}
			if (_consoleInput.text.Contains("ls"))
            {
				_startCommand.Play();
				CommandLS();
			}
			if (_consoleInput.text.Contains("mkdir"))
			{
				_createCommand.Play();
				CommandMkDir();
			}
			if (_consoleInput.text.Contains("rmdir"))
			{
				_startCommand.Play();
				CommandRMDir();
			}
			if (_consoleInput.text.Contains("touch"))
            {
				_createCommand.Play();
				CommandTouch();
			}
			if (_consoleInput.text.Contains("cat"))
            {
				_openTextFile.Play();
				CommandCat();
			}
			if (_consoleInput.text.Contains("rm"))
			{
				_startCommand.Play();
				CommandRmFile();
			}
			if (_consoleInput.text.Contains("date"))
            {
				_startCommand.Play();
				_consoleText.text = _currentDate;
			}
			if (_consoleInput.text.Contains("ls") && _consoleInput.text.Contains("-l"))
			{
				_startCommand.Play();
				CommandLSWithFlag();
			}
		}
 }
	public class FileSystemElement : MonoBehaviour//наследовали от монобехавиор чтобы и принт видеть и объект саздавался корректно а то не создавался
	{
        private string _nameSelf;
        public string NameSelf
        {
            get { return _nameSelf; }
            set { _nameSelf = value; }
        }
        private string _pathFromWhereCreate;

        public string PathFromWhereCreate
        {
            get { return _pathFromWhereCreate; }
            set { _pathFromWhereCreate = value; }
        }

		public string UserOwner { get; set; } //автоматическое свойства

		public string CreateDate { get; set; }

		public FileSystemElement(string nameElement, string pathFromWhereCreate, string userOwn, string Date) //piblic - иначе из других классов фолдер с конструктором не создать
		{
			this._pathFromWhereCreate = pathFromWhereCreate;
			this._nameSelf = nameElement;
			this.UserOwner = userOwn;
			this.CreateDate = Date;
		}
		public string GetFailFolderDoesNotExist()
		{
			return "No such file or directory";
		}

		public string GetPathWhere(string path) //возвращает путь папки с проверкой существует ли папка (по полному пути) как было в кмд указано
		{
			if (this._pathFromWhereCreate == path) //при CD вписали папку bin например, к ней приплюсовали текущий фолдер / вышло /bin
			{
				return _nameSelf;
			}
			else
			{
				return "NotFOD";
			}
		}
	}
	public class Folder : FileSystemElement//наследовали от монобехавиор чтобы и принт видеть и объект саздавался корректно а то не создавался
	{
		public Folder(string nameFolder, string pathFromWhereCreate, string userOwnerFolder, string Date) : base(nameFolder, pathFromWhereCreate, userOwnerFolder, Date) { } //передали поля в конструктор базового класса
    }

	public class File : FileSystemElement //класс наследует все от Folder
	{
		public string FileContent { get; set; } = "File is null";
		public File(string nameFile, string pathFromWhereCreate, string userOwnerFile, string Date) : base(nameFile, pathFromWhereCreate, userOwnerFile, Date) { }
	}
}