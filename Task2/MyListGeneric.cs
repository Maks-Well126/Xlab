using System;

public class MyList<T>
{
    private T[] _items;
    private int _count;

    // Вывод всех элементов
    public void PrintAll()
    {
        for (int i = 0; i < _count; i++)
        {
            if (i < _count - 1)
                Console.Write(_items[i] + ", ");
            else
                Console.Write(_items[i]);
        }
    }

    // Добавление элемента (Add)
    public void Add(T item)
    {
        if (_items == null)
        {
            _items = new T[4];
        }

        if (_count == _items.Length)
        {
            T[] newArray = new T[_items.Length * 2];
            for (int i = 0; i < _items.Length; i++)
            {
                newArray[i] = _items[i];
            }
            _items = newArray;
        }

        _items[_count] = item;
        _count++;
    }

    // Вставка по индексу (Insert)
    public void Insert(int index, T item)
    {
        if (index < 0 || index > _count)
        {
            Console.WriteLine("\nОшибка: индекс вне диапазона!");
            return;
        }

        if (_items == null)
        {
            _items = new T[4];
        }

        if (_count == _items.Length)
        {
            T[] newArray = new T[_items.Length * 2];
            for (int i = 0; i < _items.Length; i++)
            {
                newArray[i] = _items[i];
            }
            _items = newArray;
        }

        for (int i = _count; i > index; i--)
        {
            _items[i] = _items[i - 1];
        }

        _items[index] = item;
        _count++;
    }

    // Удаление по значению (Remove)
    public void Remove(T item)
    {
        int index = -1;

        for (int i = 0; i < _count; i++)
        {
            if (Equals(_items[i], item)) // универсальное сравнение
            {
                index = i;
                break;
            }
        }

        if (index == -1)
            return;

        for (int i = index; i < _count - 1; i++)
        {
            _items[i] = _items[i + 1];
        }

        _count--;
    }

    // Удаление по индексу (RemoveAt)
    public void RemoveAt(int index)
    {
        if (index < 0 || index >= _count)
        {
            Console.WriteLine("\nНеверный индекс для удаления!");
            return;
        }

        for (int i = index; i < _count - 1; i++)
        {
            _items[i] = _items[i + 1];
        }

        _count--;
    }

    //  Очистка списка (Clear)
    public void Clear()
    {
        _items = null;
        _count = 0;
        Console.WriteLine("\n\nСписок очищен!");
    }

    //  Индексатор
    public T this[int index]
    {
        get
        {
            if (index < 0 || index >= _count)
            {
                Console.WriteLine("Ошибка: индекс вне диапазона!");
                return default; 
            }
            return _items[index];
        }
        set
        {
            if (index < 0 || index >= _count)
            {
                Console.WriteLine("Ошибка: индекс вне диапазона!");
                return;
            }
            _items[index] = value;
        }
    }

    public int Count => _count;


   
}
