public class FileSystemElement : MonoBehaviour
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

		public FileSystemElement(string nameElement, string pathFromWhereCreate, string userOwn, string Date) //public - иначе из других классов фолдер с конструктором не создать
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