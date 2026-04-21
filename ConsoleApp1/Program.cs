using System;
using System.Collections.Generic;

namespace Galaktikos
{
    class Program
    {
        /// <summary>
        /// Главная точка входа. Запускает приветствие и вывод списка галактик.
        /// </summary>
        /// <param name="args">Аргументы командной строки (не используются)</param>
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Galaxy News!");
            IterateThroughList(); // Выводим информацию о галактиках
            Console.ReadKey();    // Ждём нажатия клавиши перед закрытием
        }

        /// <summary>
        /// Создаёт коллекцию галактик и выводит их свойства в консоль.
        /// </summary>
        private static void IterateThroughList()
        {
            // Инициализируем список галактик с помощью коллектора объектов
            var theGalaxies = new List<Galaxy>
        {
            new Galaxy() { Name="Tadpole", MegaLightYears=400, GalaxyType=new GType('S')},
            new Galaxy() { Name="Pinwheel", MegaLightYears=25, GalaxyType=new GType('S')},
            new Galaxy() { Name="Cartwheel", MegaLightYears=500, GalaxyType=new GType('L')},
            new Galaxy() { Name="Small Magellanic Cloud", MegaLightYears=.2, GalaxyType=new GType('I')},
            new Galaxy() { Name="Andromeda", MegaLightYears=3, GalaxyType=new GType('S')},
            new Galaxy() { Name="Maffei 1", MegaLightYears=11, GalaxyType=new GType('E')}
        };
            // Перебираем каждую галактику и форматируем вывод
            foreach (Galaxy theGalaxy in theGalaxies)
            {
                Console.WriteLine(theGalaxy.Name + "  " + theGalaxy.MegaLightYears + ",  " + theGalaxy.GalaxyType.MyGType);
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
    /// <summary>
    /// Представляет галактику с именем, расстоянием и морфологическим типом.
    /// </summary>
    public class Galaxy
    {
        /// <summary>Название галактики</summary>
        public string Name { get; set; }
        /// <summary>Расстояние до галактики в миллионах световых лет</summary>
        public double MegaLightYears { get; set; }
        /// <summary>Морфологический тип галактики (спиральная, эллиптическая и т.д.)</summary>
        public GType GalaxyType { get; set; }

    }
    /// <summary>
    /// Класс-обёртка для преобразования символьного кода типа галактики ('S', 'E'...) 
    /// в человекочитаемое значение.
    /// </summary>
    public class GType
    {
        /// <summary>
        /// Инициализирует тип галактики на основе однобуквенного кода.
        /// </summary>
        /// <param name="type">Код типа: 'S'=Spiral, 'E'=Elliptical, 'I'=Irregular, 'L'=Lenticular</param>
        public GType(char type)
        {
            // Сопоставляем буквенный код с перечислением
            switch (type)
            {
                case 'S':
                    MyGType = Type.Spiral;
                    break;
                case 'E':
                    MyGType = Type.Elliptical;
                    break;
                case 'I':
                    MyGType = Type.Irregular;
                    break;
                case 'L':
                    MyGType = Type.Lenticular;
                    break;
                default:
                    break;
            }
        }
        /// <summary>Человекочитаемое название типа галактики</summary>
        /// <remarks>Возвращает значение перечисления Type как object</remarks>
        public object MyGType { get; set; }
        /// <summary>
        /// Внутреннее перечисление типов галактик.
        /// Скрыто от внешнего использования (private).
        /// </summary>
        private enum Type { Spiral, Elliptical, Irregular, Lenticular }
    }
}