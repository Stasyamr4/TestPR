using System;
using System.Text;

/// <summary>
/// Класс, реализующий шифр Виженера для шифрования и дешифрования текста.
/// Поддерживает русский и английский алфавиты, сохраняет регистр букв,
/// оставляет неизменными неалфавитные символы (цифры, знаки препинания, пробелы).
/// </summary>
public static class VigenereCipher
{
    // Русский алфавит (без Ё, но можно добавить)
    private const string RussianAlphabet = "АБВГДЕЖЗИЙКЛМНОПРСТУФХЦЧШЩЪЫЬЭЮЯ";
    private const string EnglishAlphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

    /// <summary>
    /// Шифрует открытый текст с использованием ключа.
    /// </summary>
    /// <param name="text">Открытый текст (может содержать буквы, цифры, знаки).</param>
    /// <param name="key">Ключевое слово (только буквы, регистр не важен).</param>
    /// <returns>Зашифрованная строка.</returns>
    /// <exception cref="ArgumentNullException">Если text или key равен null.</exception>
    /// <exception cref="ArgumentException">Если text или key пуст, содержит только пробелы,
    /// или key содержит небуквенные символы.</exception>
    public static string Encrypt(string text, string key)
    {
        ValidateInputs(text, key);
        return Process(text, key, encrypt: true);
    }

    /// <summary>
    /// Дешифрует текст с использованием ключа.
    /// </summary>
    /// <param name="cipher">Зашифрованный текст.</param>
    /// <param name="key">Ключевое слово (должно совпадать с ключом шифрования).</param>
    /// <returns>Расшифрованная строка.</returns>
    /// <exception cref="ArgumentNullException">Если cipher или key равен null.</exception>
    /// <exception cref="ArgumentException">Если cipher или key пуст, содержит только пробелы,
    /// или key содержит небуквенные символы.</exception>
    public static string Decrypt(string cipher, string key)
    {
        ValidateInputs(cipher, key);
        return Process(cipher, key, encrypt: false);
    }

    /// <summary>
    /// Проверяет входные параметры.
    /// </summary>
    private static void ValidateInputs(string text, string key)
    {
        if (text == null)
            throw new ArgumentNullException(nameof(text), "Текст не может быть null.");
        if (key == null)
            throw new ArgumentNullException(nameof(key), "Ключ не может быть null.");

        if (string.IsNullOrWhiteSpace(text))
            throw new ArgumentException("Текст не может быть пустым или состоять только из пробелов.", nameof(text));
        if (string.IsNullOrWhiteSpace(key))
            throw new ArgumentException("Ключ не может быть пустым или состоять только из пробелов.", nameof(key));

        // Проверяем, что ключ состоит только из букв (латиница или кириллица)
        foreach (char c in key)
        {
            if (!IsRussianLetter(c) && !IsEnglishLetter(c))
                throw new ArgumentException($"Ключ может содержать только буквы (русские или английские). Недопустимый символ: '{c}'.", nameof(key));
        }
    }

    /// <summary>
    /// Основной метод обработки (шифрование или дешифрование).
    /// </summary>
    private static string Process(string input, string key, bool encrypt)
    {
        var result = new StringBuilder(input.Length);
        int keyIndex = 0;

        // Фильтруем ключ: оставляем только буквы, переводим в верхний регистр (для единообразия)
        string cleanKey = FilterLetters(key).ToUpperInvariant();

        foreach (char originalChar in input)
        {
            char processedChar;
            if (IsRussianLetter(originalChar))
            {
                char upperChar = char.ToUpperInvariant(originalChar);
                int charIndex = RussianAlphabet.IndexOf(upperChar);
                int keyCharIndex = GetKeyCharIndex(cleanKey, ref keyIndex, RussianAlphabet);
                int newIndex = encrypt
                    ? (charIndex + keyCharIndex) % RussianAlphabet.Length
                    : (charIndex - keyCharIndex + RussianAlphabet.Length) % RussianAlphabet.Length;
                char newUpperChar = RussianAlphabet[newIndex];
                processedChar = char.IsLower(originalChar) ? char.ToLowerInvariant(newUpperChar) : newUpperChar;
            }
            else if (IsEnglishLetter(originalChar))
            {
                char upperChar = char.ToUpperInvariant(originalChar);
                int charIndex = EnglishAlphabet.IndexOf(upperChar);
                int keyCharIndex = GetKeyCharIndex(cleanKey, ref keyIndex, EnglishAlphabet);
                int newIndex = encrypt
                    ? (charIndex + keyCharIndex) % EnglishAlphabet.Length
                    : (charIndex - keyCharIndex + EnglishAlphabet.Length) % EnglishAlphabet.Length;
                char newUpperChar = EnglishAlphabet[newIndex];
                processedChar = char.IsLower(originalChar) ? char.ToLowerInvariant(newUpperChar) : newUpperChar;
            }
            else
            {
                // Неалфавитный символ – оставляем без изменений
                processedChar = originalChar;
            }

            result.Append(processedChar);
        }

        return result.ToString();
    }

    /// <summary>
    /// Получает индекс символа ключа в указанном алфавите (циклически).
    /// </summary>
    private static int GetKeyCharIndex(string cleanKey, ref int keyIndex, string alphabet)
    {
        if (cleanKey.Length == 0) return 0; // не должно случиться из-за валидации
        char keyChar = cleanKey[keyIndex % cleanKey.Length];
        keyIndex++;

        // Ищем символ ключа в нужном алфавите (русском или английском)
        int idx = alphabet.IndexOf(keyChar);
        if (idx == -1)
        {
            // Если символ ключа принадлежит другому алфавиту, пробуем найти в противоположном?
            // По требованиям ключ может быть смешанным, но для сдвига используем алфавит обрабатываемой буквы.
            // Если ключ из другого алфавита, то сдвиг по модулю размера текущего алфавита.
            // Проще: считаем, что ключ может быть на любом языке — берём абсолютный номер в Unicode и модуль.
            // Но для предсказуемости лучше ограничить ключ одним алфавитом. Мы уже проверяли, что ключ состоит из букв.
            // Если буква из другого алфавита, используем её порядковый номер по ASCII/Unicode, но это не по ТЗ.
            // Вместо этого приведём к числовому значению: для упрощения используем код символа по модулю размера алфавита.
            int fallbackIndex = (char.ToUpperInvariant(keyChar) - 'A') % alphabet.Length;
            return fallbackIndex;
        }
        return idx;
    }

    /// <summary>
    /// Оставляет в строке только буквы (латиница или кириллица), остальное отбрасывает.
    /// </summary>
    private static string FilterLetters(string input)
    {
        var sb = new StringBuilder();
        foreach (char c in input)
        {
            if (IsRussianLetter(c) || IsEnglishLetter(c))
                sb.Append(c);
        }
        return sb.ToString();
    }

    /// <summary>
    /// Проверяет, является ли символ русской буквой (включая оба регистра).
    /// </summary>
    private static bool IsRussianLetter(char c)
    {
        c = char.ToUpperInvariant(c);
        return c >= 'А' && c <= 'Я' || c == 'Ё'; // если нужна поддержка Ё
    }

    /// <summary>
    /// Проверяет, является ли символ английской буквой (включая оба регистра).
    /// </summary>
    private static bool IsEnglishLetter(char c)
    {
        c = char.ToUpperInvariant(c);
        return c >= 'A' && c <= 'Z';
    }
}