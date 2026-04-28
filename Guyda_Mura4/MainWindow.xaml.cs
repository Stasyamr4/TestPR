using System;
using System.Windows;
using System.Windows.Media;
using EncryptVigenere;

namespace EncryptVigenere
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void BtnEncrypt_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string text = txtText.Text;
                string key = txtKey.Text;

                if (string.IsNullOrWhiteSpace(text))
                {
                    ShowStatus("Ошибка: текст не может быть пустым.", Brushes.Red);
                    return;
                }
                if (string.IsNullOrWhiteSpace(key))
                {
                    ShowStatus("Ошибка: ключ не может быть пустым.", Brushes.Red);
                    return;
                }

                string encrypted = VigenereCipher.Encrypt(text, key);
                txtResult.Text = encrypted;
                ShowStatus($"Шифрование выполнено успешно. Длина ключа: {key.Length}", Brushes.Green);
            }
            catch (ArgumentException ex)
            {
                ShowStatus($"Ошибка: {ex.Message}", Brushes.Red);
            }
            catch (Exception ex)
            {
                ShowStatus($"Неизвестная ошибка: {ex.Message}", Brushes.Red);
            }
        }

        private void BtnDecrypt_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string text = txtText.Text;
                string key = txtKey.Text;

                if (string.IsNullOrWhiteSpace(text))
                {
                    ShowStatus("Ошибка: текст не может быть пустым.", Brushes.Red);
                    return;
                }
                if (string.IsNullOrWhiteSpace(key))
                {
                    ShowStatus("Ошибка: ключ не может быть пустым.", Brushes.Red);
                    return;
                }

                string decrypted = VigenereCipher.Decrypt(text, key);
                txtResult.Text = decrypted;
                ShowStatus($"Дешифрование выполнено успешно.", Brushes.Green);
            }
            catch (ArgumentException ex)
            {
                ShowStatus($"Ошибка: {ex.Message}", Brushes.Red);
            }
            catch (Exception ex)
            {
                ShowStatus($"Неизвестная ошибка: {ex.Message}", Brushes.Red);
            }
        }

        private void BtnClear_Click(object sender, RoutedEventArgs e)
        {
            txtText.Clear();
            txtKey.Clear();
            txtResult.Clear();
            ShowStatus("Все поля очищены.", Brushes.Black);
        }

        private void ShowStatus(string message, System.Windows.Media.Brush color)
        {
            statusMessage.Content = message;
            statusMessage.Foreground = color;
        }
    }
}