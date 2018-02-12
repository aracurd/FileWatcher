using System.IO;
using System.Text;

namespace FileWatcher.Classes
{
    public class FileRepository
    {


        public FileRepository()
        {
            FileName = default(string);
        }

        public FileRepository(string fileName)
        {
            FileName = fileName;
        }

        public string FileName { get; set; }
     
        public void CreateFile(string fileName = null)
        {
            if (string.IsNullOrEmpty(fileName))
              return;

            using ( var fs = new FileStream(fileName, FileMode.CreateNew,FileAccess.ReadWrite)) { }
        }

        public void AddDataToFile(string data)
        {
            if (string.IsNullOrEmpty(FileName))
             return;
            
            using (var sr = new StreamWriter(FileName, true))
            {
                sr.WriteLine(data);
            }
        }

        public string ReadDataFromFile()
        {
            if (string.IsNullOrEmpty(FileName))
                return null;

            string result;

            using (var sr = new StreamReader(FileName))
            {
                result = sr.ReadToEnd();
            }

            return result;
        }
    }
}
