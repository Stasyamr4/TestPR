## Отладка консольного приложения "Буквы" на языке C#

исходный код программы:

```csharp
using System;

class ArrayExample
{
    static void Main()
    {
        char[] letters = { 'f', 'r', 'e', 'd', ' ', 's', 'm', 'i', 't', 'h' };
        string name = "";
        int[] a = new int[10];
        for (int i = 0; i < letters.Length; i++)
        {
            name += letters[i];
            a[i] = i + 1;
            SendMessage(name, a[i]);
        }
        Console.ReadKey();
    }

    static void SendMessage(string name, int msg)
    {
        Console.WriteLine("Hello, " + name + "! Count to " + msg);
    }
}
```

Результат выполнения:
<img width="979" height="325" alt="image" src="https://github.com/user-attachments/assets/4d3416fd-eb47-46f3-81ab-4c9936f83a67" />


Поставить точку останова в строке 12
<img width="619" height="159" alt="image" src="https://github.com/user-attachments/assets/c2fdb3d0-2399-4ead-af99-a085f4c6a54f" />


Начать отладку проекта. Навести указатель мыши на интересующую переменную для получения подробной информации о ней (какое именно значение примет переменная в цикле при конкретной итерации).
<img width="416" height="209" alt="image" src="https://github.com/user-attachments/assets/f70c45df-0450-46bf-98dd-a7d470e79cfa" />


С помощью кнопки "Шаг с заходом" или клавиши F11 дойти до строки 20. Нажать клавишу F11 для захода в функцию. Желтая стрелка слева указывает текущее положение отладчика в коде.
<img width="731" height="244" alt="image" src="https://github.com/user-attachments/assets/db26988c-faa5-4e7a-b466-e1ccd094cf64" />

Повторно нажать F11 для входа в метод. Желтая стрелка остановится на строке 21: `Console.WriteLine("Hello, " + name + "! Count to " + msg);`
<img width="787" height="105" alt="image" src="https://github.com/user-attachments/assets/11b1739f-8067-4329-83e6-4cd410804af4" />

Чтобы покинуть функцию и вернуться в цикл for, нажать сочетание клавиш `Shift + F11`. Выполнится код в теле функции, произойдет вывод в консоль, а отладчик переместится в цикл for, в строку с вызовом нашего метода `SendMessage(name, a[i]);`
<img width="929" height="372" alt="image" src="https://github.com/user-attachments/assets/d4868957-d0b7-4b8e-93c2-829dd9f5d53e" />

<img width="569" height="99" alt="image" src="https://github.com/user-attachments/assets/e7be4ad9-505a-4cef-8a79-12f5b563198a" />

Также во время отладки можно воспользоваться функцией `Выполнить до этого места`. В нашем случае наведем указатель мыши на строку, в которой происходит вывод в консоль (21) и воспользуемся вышеназванной функцией. Произойдет выполнение кода до данной строки (включительно), соответственно в консоли появится сообщение, что говорит о том, что функция сработала успешно.
<img width="797" height="167" alt="image" src="https://github.com/user-attachments/assets/2b3e2465-3869-4da0-bbd5-7498436de48b" />

**Вывод в консоль происходит успешно, буквы не пропадают, по итогу выполнения программы сообщение выводится полностью корректно.**
## Авторы
**Студенты**: Мура Анастасия и Гуйда Владислав

**Группа**: 3ИСИП-423

**Преподаватель**: Аксёнова Татьяна Геннадьевна

**Дисциплина**: Поддержка и тестирование программных модулей

## Лицензия
Этот проект является учебным и создан исключительно в образовательных целях.
