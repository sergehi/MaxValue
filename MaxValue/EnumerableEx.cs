using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace MaxValue
{
    internal static class EnumerableEx
    {
        //1.Написать обобщённую функцию расширения, находящую и возвращающую максимальный элемент коллекции.
        //Функция должна принимать на вход делегат, преобразующий входной тип в число для возможности поиска максимального значения.
        public static T? MaxValue<T>(this IEnumerable<T> collection, Func<T, float> convertToNumber) where T : class
        {
            if (collection == null || !collection.Any())
                throw new Exception("Неправильный параметр. Коллекция не должна быть пустой или null.");

            return collection.MaxBy(convertToNumber);
        }
    }
}
