int result = Fibonacci(5);
Console.WriteLine(result);


/// <summary>
/// Вычисляет n-е число Фибоначчи итеративным способом.
/// </summary>
/// <param name="n">Порядковый номер числа (нумерация с 0: F(0)=0, F(1)=1)</param>
/// <returns>n-е число Фибоначчи</returns>
static int Fibonacci(int n)
{
    Console.WriteLine("The output is: ");
    int n1 = 0;
    int n2 = 1;
    int sum;

    // Цикл начинается с 2, так как первые два числа уже заданы
    for (int i = 2; i <= n; i++)
    {
        sum = n1 + n2; // Следующее число = сумма двух предыдущих
        n1 = n2;
        n2 = sum;
    }
    // При n=0 возвращаем n1, иначе n2 (последнее вычисленное значение)
    return n == 0 ? n1 : n2;
}