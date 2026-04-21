исходный код программы:

```csharp
using System;
using System.Collections.Generic;

namespace Galaktikos
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Galaxy News!");
            IterateThroughList();
            Console.ReadKey();
        }

        private static void IterateThroughList()
        {
            var theGalaxies = new List<Galaxy>
        {
            new Galaxy() { Name="Tadpole", MegaLightYears=400, GalaxyType=new GType('S')},
            new Galaxy() { Name="Pinwheel", MegaLightYears=25, GalaxyType=new GType('S')},
            new Galaxy() { Name="Cartwheel", MegaLightYears=500, GalaxyType=new GType('L')},
            new Galaxy() { Name="Small Magellanic Cloud", MegaLightYears=.2, GalaxyType=new GType('I')},
            new Galaxy() { Name="Andromeda", MegaLightYears=3, GalaxyType=new GType('S')},
            new Galaxy() { Name="Maffei 1", MegaLightYears=11, GalaxyType=new GType('E')}
        };

            foreach (Galaxy theGalaxy in theGalaxies)
            {
                Console.WriteLine(theGalaxy.Name + "  " + theGalaxy.MegaLightYears + ",  " + theGalaxy.GalaxyType);
            }

            // Expected Output:
            //  Tadpole  400,  Spiral
            //  Pinwheel  25,  Spiral
            //  Cartwheel, 500,  Lenticular
            //  Small Magellanic Cloud .2,  Irregular
            //  Andromeda  3,  Spiral
            //  Maffei 1,  11,  Elliptical
        }
    }

    public class Galaxy
    {
        public string Name { get; set; }

        public double MegaLightYears { get; set; }
        public object GalaxyType { get; set; }

    }

    public class GType
    {
        public GType(char type)
        {
            switch (type)
            {
                case 'S':
                    MyGType = Type.Spiral;
                    break;
                case 'E':
                    MyGType = Type.Elliptical;
                    break;
                case 'l':
                    MyGType = Type.Irregular;
                    break;
                case 'L':
                    MyGType = Type.Lenticular;
                    break;
                default:
                    break;
            }
        }
        public object MyGType { get; set; }
        private enum Type { Spiral, Elliptical, Irregular, Lenticular }
    }
}
```

Результат выполнения:
<img width="975" height="506" alt="image" src="https://github.com/user-attachments/assets/57007b2b-3598-4078-93fc-3777204d8937" />

Поставить точку останова в первой строке и с помощью отладчика пошагово дойти до объявления переменной sum, убедиться, что все значения из кода корректно присвоились в переменные:
<img width="924" height="1014" alt="image" src="https://github.com/user-attachments/assets/3983f94f-fab2-41b7-9d33-d417963b5020" />

поставить точку останова в цикле, чтобы избежать пошагового выполнения и сразу отслеживать изменение значений переменных. Было замечено, что до 4 итерации код правильно считает последовательность
<img width="947" height="1008" alt="image" src="https://github.com/user-attachments/assets/a87682b6-96b3-4947-a20e-fe386bb49cab" />

После 4 итерации при нажатии кнопки продолжить программа вылетает и возвращает последнее число последовательности, посчитанное на шаге 4 (это и есть число 3)
<img width="1455" height="1005" alt="image" src="https://github.com/user-attachments/assets/36af76ec-0872-4605-9da3-8d96faeef2fa" />

Удалим предыдущие точки останова и поставим одну единственную в строке 18: `return n == 0 ? n1 : n2;`
Запустим отладчик. Видно, что функция вернет число 3, не выполнив подсчёт 5 числа (5 итерация не проходит)
<img width="957" height="1003" alt="image" src="https://github.com/user-attachments/assets/0fc95ebe-d8c0-4eba-8879-fcec05efec24" />
Исходя из вышеперечисленного, можно сделать вывод о том, что 5 итерация не проходит из-за ограничений в цикле for
`for (int i = 2; i < n; i++)`
Исправим условие окончания цикла: `for (int i = 2; i <= n; i++)`. Повторно запустим программу с той же точкой останова.
<img width="949" height="1009" alt="image" src="https://github.com/user-attachments/assets/a3170617-489b-45d8-8d71-baff3a800ea9" />
Как видно из окна "Локальные", функция сработала корректно и вернула значение 5.
Запустим программу и убедимся в этом
<img width="977" height="217" alt="image" src="https://github.com/user-attachments/assets/60191bdb-a50c-4e27-9044-d4ae1318b22d" />

## Авторы
**Студенты**: Мура Анастасия и Гуйда Владислав

**Группа**: 3ИСИП-423

**Преподаватель**: Аксёнова Татьяна Геннадьевна

**Дисциплина**: Поддержка и тестирование программных модулей

## Лицензия
Этот проект является учебным и создан исключительно в образовательных целях.
