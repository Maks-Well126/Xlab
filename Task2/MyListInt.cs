public class MyList
{
    private int[] _items;

    private int _count;

    public void PrintAll() //вывод
    {
        for (int i = 0; i < _count; i++)
        {
            if (i < _count - 1)
                Console.Write(_items[i] + ", ");
            else
                Console.Write(_items[i]);
        }
    }

    public void Add(int item)// Add
    {

        if (_items == null)
        {
            _items = new int[4];
        }

        if (_count == _items.Length)
        {

            int[] newArray = new int[_items.Length * 2];

            for (int i = 0; i < _items.Length; i++)
            {
                newArray[i] = _items[i];
            }

            _items = newArray;
        }

        _items[_count] = item;
        _count++;
    }

    public void Insert(int index, int item) //Insert
    {
        if (index < 0 || index >= _count)
        {
            Console.WriteLine("\nОшибка: индекс вне диапазона!");
            return;
        }

        if (_count == _items.Length) //проверка места
        {

            int[] newArray = new int[_items.Length * 2];

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

    public void Remove(int item) //Remove
    {
        int index = -1;

        for (int i = 0; i < _count; i++)
        {
            if (_items[i] == item)
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


    public void RemoveAt(int index) //RemoveAt
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

    public void Clear()
    {
        _items = null;
        _count = 0; // дополнительная проверка
        Console.WriteLine("\n\nСписок очищен!");
    }



    public int this[int index]//индексатор
    {
        get
        {
            if (index < 0 || index >= _count)
            {
                Console.WriteLine("Ошибка: индекс вне диапазона!");
                return default; // вернёт 0, если индекс неверный
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
}