## Отладка консольного приложения "Галактики" на языке C#
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

Поставить точку останова в строке вывода галактик в цикле foreach и посмотреть, какой тип галактики возвращается:
<img width="1336" height="108" alt="image" src="https://github.com/user-attachments/assets/ebc24eb9-42b0-4430-87ea-8b4ae8a77d86" />

Как видно из скриншота, тип галактики определяется корректно, но в консоль возвращается имя поля класса.
В том же коде при отладке поместим курсор в конец theGalaxy.GalaxyType и изменим его на theGalaxy.GalaxyType.MyGType.
Нажать F11, чтобы выполнить текущую строку кода
F11 перемещает отладчик (и выполняет код) по одной инструкции за раз.
При попытке перейти к отладчику появится диалоговое окно "Горячая перезагрузка", указывающее, что изменения не могут быть скомпилированы. Нажать на кнопку "Изменить".
<img width="618" height="188" alt="image" src="https://github.com/user-attachments/assets/38fd8d4d-9dde-468a-9d2a-9fd6b55a8746" />

Программа вернула ошибку. В классе Galaxy обнаружили, что свойство класса 
`GalaxyType` указано как Galaxy, а не как object.
<img width="678" height="589" alt="image" src="https://github.com/user-attachments/assets/10765e14-7aef-4993-8e6a-9468b53a17f8" />

Изменим свойство класса `GalaxyType` на следующее:
`public GType GalaxyType { get; set; }`
<img width="489" height="125" alt="image" src="https://github.com/user-attachments/assets/e1dc2255-738f-4393-b504-86c0f85740c9" />

После отладки тип галактик отображается правильно, но у галактики `Small Magellanic Cloud` не выводится тип и программа завершается с кодом -1.
<img width="552" height="250" alt="image" src="https://github.com/user-attachments/assets/255cf50e-6c99-461a-8cf0-f8ce8cb15fd1" />

Установить точку останова в типах галактик, в строке перед switch, чтобы посмотреть, какой тип присваивается галактике. Дойдя до нужной галактики, заметим, что ей присваивается тип `I`, но в switch такого типа нет, поэтому программа переходит к разделу default.
<img width="922" height="546" alt="image" src="https://github.com/user-attachments/assets/92a908f8-6a33-4725-9a5b-a76e7d4c4699" />

```csharp
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
```
Изменим `case "l"` на `case "I"`. Запустим программу и убедимся в правильности выполнения:
<img width="977" height="265" alt="image" src="https://github.com/user-attachments/assets/fc77dbcd-6e94-47df-b704-4a346593fc2f" />

**После отладки программа работает корректно, все галактики и их данные выводятся.**
## Авторы
**Студенты**: Мура Анастасия и Гуйда Владислав

**Группа**: 3ИСИП-423

**Преподаватель**: Аксёнова Татьяна Геннадьевна

**Дисциплина**: Поддержка и тестирование программных модулей

## Лицензия
Этот проект является учебным и создан исключительно в образовательных целях.
