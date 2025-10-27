
class Programm
{
    static void Main()
    {

        //
        //Обычный List
        //

        MyList listInt = new MyList();//Создание Add
        listInt.Add(21);
        listInt.Add(33);
        listInt.Add(68);

        Console.WriteLine("Начальный список(Add)");
        listInt.PrintAll();

        Console.WriteLine("\n\nПроверка Insert:");
        
        listInt.Insert(1, 15); // Проверка вставки Insert
        listInt.PrintAll();

        listInt.Insert(10, 99); // сообщение об ошибке

        listInt.Remove(33); // remove
        listInt.Remove(200); // не существует — ничего не произойдёт

        Console.WriteLine("\nПосле удаления(Remove):");
        listInt.PrintAll();


        Console.WriteLine("\n\nПроверка RemoveAt:");
        listInt.RemoveAt(0);   // удаляем первый элемент
        listInt.PrintAll();

        listInt.RemoveAt(10);  // неправильный индекс

        Console.WriteLine("\nПроверка индексатора:");//индексатор
        Console.WriteLine(listInt[1]); // получить элемент
        listInt[1] = 55;               // изменить элемент
        listInt.PrintAll();

        listInt.Clear();//очистка списка
        listInt.PrintAll();

        //
        // List Generic
        // 
        Console.WriteLine("\n\nЛист Generic");
        //  Пример с числами
        MyList<int> list = new MyList<int>();
        list.Add(21);
        list.Add(33);
        list.Add(68);

        Console.WriteLine("Начальный список(Add):");
        list.PrintAll();

        Console.WriteLine("\n\nПроверка Insert:");
        list.Insert(1, 15);
        list.PrintAll();

        list.Insert(10, 99); // ошибка

        list.Remove(33);
        list.Remove(200);

        Console.WriteLine("\nПосле удаления(Remove):");
        list.PrintAll();

        Console.WriteLine("\n\nПроверка RemoveAt:");
        list.RemoveAt(0);
        list.PrintAll();

        list.RemoveAt(10);

        Console.WriteLine("\nПроверка индексатора:");
        Console.WriteLine(list[1]);
        list[1] = 55;
        list.PrintAll();

        list.Clear();
        list.PrintAll();

        //Пример со строкой 
        Console.WriteLine("\n\nПример с MyList<string>:");
        MyList<string> words = new MyList<string>();
        words.Add("Привет");
        words.Add("Мир");
        words.Insert(1, "тебе");
        words.PrintAll();

    }

}