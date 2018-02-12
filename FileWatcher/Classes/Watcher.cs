using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace FileWatcher.Classes
{
    public class Watcher
    {
        public DateTime _startWriteTime;

        public event Action FileChange;

        private readonly string _fileDir;

        public Watcher(string file)
        {
            _fileDir = file;
            _startWriteTime = File.GetLastWriteTime(_fileDir);
        }

        public CancellationToken Token { get; set; }

        public CancellationTokenSource TokenSource { get; set; }

        public bool IsFileExist() => File.Exists(_fileDir);

        public Task StartWatcher()
        {
            CancellationTokenSource tokenSource = new CancellationTokenSource();
            var watch = new Task(() => Watch(tokenSource.Token), tokenSource.Token);
            watch.Start();
            TokenSource = tokenSource;
            Token = tokenSource.Token;
            return watch;
        }

        private  void Watch(CancellationToken token)
        {
            if (!IsFileExist())
             return;

            while (true)
            {
                if (token.IsCancellationRequested)
                    token.ThrowIfCancellationRequested();

                var currentWriteTime = File.GetLastWriteTime(_fileDir);

                if (_startWriteTime < currentWriteTime)
                {

                    FileChange?.Invoke();
                    _startWriteTime = currentWriteTime;

                }

                Thread.Sleep(1000);
            }
        }

    }
}
