using System;

namespace is_code_development_4 // Соответствует вашему проекту
{
    // Шаблонный класс для обработки одномерного массива
    public class ArrayProcessor<T> where T : IComparable<T>
    {
        private T[] array; // Внутренний массив данных

        // Конструктор с проверкой входных данных и ручным копированием
        public ArrayProcessor(T[] inputArray)
        {
            if (inputArray == null || inputArray.Length == 0)
                throw new ArgumentException("Массив не может быть пустым или null.");

            array = new T[inputArray.Length];
            for (int i = 0; i < inputArray.Length; i++)
            {
                array[i] = inputArray[i];
            }
        }

        // Свойство для доступа к массиву
        public T[] Array => array;

        // Метод сортировки Шелла
        public void ShellSort()
        {
            int n = array.Length;
            for (int gap = n / 2; gap > 0; gap /= 2)
            {
                for (int i = gap; i < n; i++)
                {
                    T temp = array[i];
                    int j;
                    for (j = i; j >= gap && array[j - gap].CompareTo(temp) > 0; j -= gap)
                    {
                        array[j] = array[j - gap];
                    }
                    array[j] = temp;
                }
            }
        }

        // Метод вычисления размаха
        public T Range()
        {
            if (array == null || array.Length == 0)
                throw new InvalidOperationException("Массив пуст или не инициализирован.");

            T min = array[0];
            T max = array[0];

            for (int i = 1; i < array.Length; i++)
            {
                if (array[i].CompareTo(min) < 0) min = array[i];
                if (array[i].CompareTo(max) > 0) max = array[i];
            }

            // Поддержка размаха только для числовых типов
            if (typeof(T) == typeof(int))
                return (dynamic)(Convert.ToInt32(max) - Convert.ToInt32(min));
            else if (typeof(T) == typeof(double))
                return (dynamic)(Convert.ToDouble(max) - Convert.ToDouble(min));
            else
                throw new NotSupportedException("Размах применим только к числовым типам (int, double).");
        }
    }
}