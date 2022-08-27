public class File : FileSystemElement //класс наследует все от Folder
	{
		public string FileContent { get; set; } = "File is null";
		public File(string nameFile, string pathFromWhereCreate, string userOwnerFile, string Date) : base(nameFile, pathFromWhereCreate, userOwnerFile, Date) { }
	}