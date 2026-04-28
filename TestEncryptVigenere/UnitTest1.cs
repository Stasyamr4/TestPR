using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using VigenereCipherTests;

namespace VigenereCipherTests
{
    [TestClass]
    public class VigenereCipherTests
    {
        [TestMethod]
        public void EncryptDecrypt_EnglishText_KeyShorter_ReturnsOriginal()
        {
            // TC_FUNC_1
            string original = "HELLO WORLD";
            string key = "KEY";

            string encrypted = VigenereCipher.Encrypt(original, key);
            string decrypted = VigenereCipher.Decrypt(encrypted, key);

            Assert.AreEqual(original, decrypted);
            Assert.AreNotEqual(original, encrypted);
        }

        [TestMethod]
        public void EncryptDecrypt_RussianText_ReturnsOriginal()
        {
            // TC_FUNC_2
            string original = "Привет Мир";
            string key = "Секрет";

            string encrypted = VigenereCipher.Encrypt(original, key);
            string decrypted = VigenereCipher.Decrypt(encrypted, key);

            Assert.AreEqual(original, decrypted);
        }

        [TestMethod]
        public void EncryptDecrypt_KeyLongerThanText_TruncatesKey()
        {
            // TC_FUNC_3 - лишние символы ключа игнорируются (цикличность не нужна, т.к. ключ длиннее текста)
            string original = "ABC";
            string key = "LONGKEYWORD";

            string encrypted = VigenereCipher.Encrypt(original, key);
            string decrypted = VigenereCipher.Decrypt(encrypted, key);

            Assert.AreEqual(original, decrypted);
        }

        [TestMethod]
        public void Encrypt_NonAlphabeticChars_RemainUnchanged()
        {
            // TC_FUNC_4
            string text = "Hello, 123!";
            string key = "KEY";

            string encrypted = VigenereCipher.Encrypt(text, key);

            // Проверяем, что неалфавитные символы не изменились
            Assert.AreEqual(',', encrypted[5]);
            Assert.AreEqual(' ', encrypted[6]);
            Assert.AreEqual('1', encrypted[7]);
            Assert.AreEqual('2', encrypted[8]);
            Assert.AreEqual('3', encrypted[9]);
            Assert.AreEqual('!', encrypted[10]);
        }

        [TestMethod]
        public void Encrypt_UpperCaseLowerCasePreserved()
        {
            // TC_FUNC_5
            string text = "AbC";
            string key = "X";

            string encrypted = VigenereCipher.Encrypt(text, key);

            // Регистр букв должен сохраниться
            Assert.IsTrue(char.IsUpper(encrypted[0]));
            Assert.IsTrue(char.IsLower(encrypted[1]));
            Assert.IsTrue(char.IsUpper(encrypted[2]));
        }

        [TestMethod]
        public void EncryptDecrypt_MixedAlphabets_EnglishAndRussian()
        {
            // Дополнительный: смешанный текст (англ + рус)
            string original = "Hello Привет";
            string key = "KeyКлюч";

            string encrypted = VigenereCipher.Encrypt(original, key);
            string decrypted = VigenereCipher.Decrypt(encrypted, key);

            Assert.AreEqual(original, decrypted);
        }

        [TestMethod]
        public void Decrypt_ReturnsOriginalAfterEncrypt_WithKeyContainingBothCases()
        {
            // TC_UI_1 - ключ регистронезависим
            string original = "CONFIDENTIAL";
            string keyUpper = "SECRET";
            string keyLower = "secret";

            string encrypted = VigenereCipher.Encrypt(original, keyUpper);
            string decrypted = VigenereCipher.Decrypt(encrypted, keyLower);

            Assert.AreEqual(original, decrypted);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Encrypt_EmptyText_ThrowsArgumentException()
        {
            // TC_NEG_1
            VigenereCipher.Encrypt("", "KEY");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Decrypt_EmptyText_ThrowsArgumentException()
        {
            VigenereCipher.Decrypt("", "KEY");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Encrypt_EmptyKey_ThrowsArgumentException()
        {
            // TC_NEG_2
            VigenereCipher.Encrypt("TEST", "");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Decrypt_EmptyKey_ThrowsArgumentException()
        {
            VigenereCipher.Decrypt("TEST", "");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Encrypt_NullText_ThrowsArgumentNullException()
        {
            // TC_NEG_3
            VigenereCipher.Encrypt(null, "KEY");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Decrypt_NullText_ThrowsArgumentNullException()
        {
            VigenereCipher.Decrypt(null, "KEY");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Encrypt_NullKey_ThrowsArgumentNullException()
        {
            VigenereCipher.Encrypt("TEST", null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Decrypt_NullKey_ThrowsArgumentNullException()
        {
            VigenereCipher.Decrypt("TEST", null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Encrypt_KeyWithDigits_ThrowsArgumentException()
        {
            // TC_NEG_4 - ключ с небуквенными символами
            VigenereCipher.Encrypt("DATA", "KE1Y!");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Decrypt_KeyWithSpecialChars_ThrowsArgumentException()
        {
            VigenereCipher.Decrypt("DATA", "KEY@");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Encrypt_KeyWithWhitespaceOnly_ThrowsArgumentException()
        {
            // ключ состоит только из пробелов
            VigenereCipher.Encrypt("TEST", "   ");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Encrypt_TextContainsOnlyWhitespace_ThrowsArgumentException()
        {
            // текст состоит только из пробелов - не считается корректным текстом
            VigenereCipher.Encrypt("   ", "KEY");
        }
        [TestMethod]
        public void Encrypt_KnownEnglishExample_ReturnsExpectedCipher()
        {
            // Пример из классики: "ATTACKATDAWN" с ключом "LEMON" -> "LXFOPVEFRNHR"
            string plain = "ATTACKATDAWN";
            string key = "LEMON";
            string expected = "LXFOPVEFRNHR";

            string actual = VigenereCipher.Encrypt(plain, key);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Decrypt_KnownEnglishExample_ReturnsOriginal()
        {
            string cipher = "LXFOPVEFRNHR";
            string key = "LEMON";
            string expected = "ATTACKATDAWN";

            string actual = VigenereCipher.Decrypt(cipher, key);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void EncryptDecrypt_CyrillicKnownExample_WorksCorrectly()
        {
            // Простой пример для русского: сдвиг 'А' + 'А' = 'А' (0+0=0)
            string original = "АБВ";
            string key = "А";
            // 'А'+'А'='А'; 'Б'+'А'='Б'; 'В'+'А'='В'  -> шифротекст = "АБВ"
            string encrypted = VigenereCipher.Encrypt(original, key);
            Assert.AreEqual("АБВ", encrypted);

            string decrypted = VigenereCipher.Decrypt(encrypted, key);
            Assert.AreEqual(original, decrypted);
        }
    }
}