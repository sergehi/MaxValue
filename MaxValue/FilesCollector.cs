using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MaxValue.Models;

namespace MaxValue
{
    //2.Написать класс, обходящий каталог файлов и выдающий событие при нахождении каждого файла;
    internal class FilesCollector
    {
        public event EventHandler<FileArgs>? FileFoundEvent;
        public CancellationTokenSource CancellationToken { get; } = new CancellationTokenSource();
        protected virtual void OnFileFound(FileArgs e)
        {
            FileFoundEvent?.Invoke(this, e);
        }

        public void CollectFiles(string dirPath)
        {
            if (!Directory.Exists(dirPath))
                return;

            var files = Directory.GetFiles(dirPath);
            foreach (var file in files)
            {
                if (CancellationToken.Token.IsCancellationRequested)
                    return;
                OnFileFound(new FileArgs(file));
            }

            // Рекурсия по подкаталогам
            var subDirs = Directory.GetDirectories(dirPath);
            foreach (var directory in subDirs)
            {
                if (CancellationToken.Token.IsCancellationRequested)
                    return;
                CollectFiles(directory);
            }
        }

    }
}
