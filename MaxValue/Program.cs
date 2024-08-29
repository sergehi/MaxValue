//1.Написать обобщённую функцию расширения, находящую и возвращающую максимальный элемент коллекции.
//Функция должна принимать на вход делегат, преобразующий входной тип в число для возможности поиска максимального значения.
//  public static T GetMax(this IEnumerable collection, Func<T, float> convertToNumber) where T : class;
// См: EnumerableEx.cs

//2.Написать класс, обходящий каталог файлов и выдающий событие при нахождении каждого файла;
// См: FilesCollector.cs

//3.Оформить событие и его аргументы с использованием .NET соглашений:
//  public event EventHandler FileFound; FileArgs – будет содержать имя файла и наследоваться от EventArgs
// См: FileArgs.cs и FilesCollector.cs


//4. Добавить возможность отмены дальнейшего поиска из обработчика;
// См: FilesCollector.CancellationToken в FilesCollector.cs

//5.Вывести в консоль сообщения, возникающие при срабатывании событий и результат поиска максимального элемента.
// См: Ниже

using MaxValue;
using MaxValue.Models;
internal class Program
{
    static List<FoundFile> FoundFilesList = new List<FoundFile>();

    private static void Main(string[] args)
    {
        if (args.Length < 1)
        {
            Console.WriteLine("Не указан путь к папке для перебора файлов");
            Console.WriteLine("Использование: MaxValue ПУТЬ");
            Console.WriteLine("Где ПУТЬ - каталог для перебора файлов. Поиск производится рекурсивно.");
            return;
        }
        try
        {
            string rootPath = args[0];
            // Поиск файлов в каталоге rootPath и подкаталогах
            FilesCollector collector = new FilesCollector();
            collector.FileFoundEvent += OnFileFound;
            FoundFilesList.Clear();
            collector.CollectFiles(rootPath);
            // Вывод максимального значения в коллекции.
            // Здесь используется поле value класса FoundFile, туда прописываются размеры файла, но можно писать и другие значения
            DisplayConclusion();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка: {ex.Message}");
        }

    }

    private static void DisplayConclusion()
    {
        Console.WriteLine("------------------------------------------------------------------------");
        Console.WriteLine("Поиск завершен");
        Console.WriteLine($"Обнаружено файлов: {FoundFilesList.Count} ");
        Console.WriteLine("");
        FoundFile? maxFile = FoundFilesList.MaxValue(x => x.GetValue());
        if (maxFile is not null)
            Console.WriteLine($"Файл с максимальным значением: {maxFile.GetName()}, значение: {maxFile.GetValue():0.000}");
    }

    private static void OnFileFound(object? sender, FileArgs e)
    {
        FoundFile foundFile = new FoundFile(e.FilePath);
        Console.WriteLine($"Найден файл: {foundFile.GetName()}, значение: {foundFile.GetValue():0.000}");
        FoundFilesList.Add(foundFile);

        // Примитивная логика для отмены поиска - ограничение количества файлов до 100
        if (FoundFilesList.Count > 99)
            (sender as FilesCollector)?.CancellationToken.Cancel();
    }
}