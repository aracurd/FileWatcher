using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Mvvm.POCO;
using FileWatcher.Classes;
using FileWatcher.Enums;

namespace FileWatcher.ViewModels
{
    [POCOViewModel]
    public class MainViewModel
    {
        private Watcher _watcher;
        private FileRepository _fileRepository;
        
        public virtual string FileContent { get; set; } = "File empty.";

        public virtual string AddToFileContent { get; set; }

        public virtual FileStatus FileStatus { get; set; } = FileStatus.Unknown;

        public virtual bool WatcherStatus { get; set; } 

        public virtual string FileName { get; set; }

        public virtual string ErrorMessage { get;  set; }

        public void CreateFile()
        {
            if (string.IsNullOrEmpty(FileName))
            {
                ErrorMessage = "File Name is Null";
                return;
            }

            _fileRepository = new FileRepository(FileName);
            _fileRepository.CreateFile(FileName);
            _watcher = new Watcher(FileName);
            _watcher.FileChange += FileCangedHandler;
            FileStatus = FileStatus.Created;
        }

        public bool CanWatcherToggle() => FileStatus == FileStatus.Created || FileStatus == FileStatus.Updated;

        public void WatcherToggle()
        {
            if (!_watcher.IsFileExist())
            {
                ErrorMessage = "File is not exist";
                return;
            }

            if (!WatcherStatus)
            {
                _watcher.StartWatcher();
                WatcherStatus = true;
            }
            else
            {
                _watcher.TokenSource.Cancel();
                WatcherStatus = false;
            }
        }

        
        public void AddDataToFile()
        {
            if (string.IsNullOrEmpty(AddToFileContent))
            {
                ErrorMessage = "Cant add empty data";
                return;
            }

            

           _fileRepository.AddDataToFile(AddToFileContent);

            AddToFileContent = default(string);
        }

        private void FileCangedHandler()
        {
            FileContent =_fileRepository.ReadDataFromFile();
            FileStatus = FileStatus.Updated;
        }


        //This is a helper POCO method for creating a MainViewModel instance 
        //public static MainViewModel Create() => ViewModelSource.Create(() => new MainViewModel());
    }
}
