using System;
using System.IO;
using System.Windows.Forms;
using System.Text.Json;
using System.Security.Cryptography;
using System.Text;

namespace Pattern_Builder
{
    public class Page
    {
        public string Header { get; set; }
        public string Author { get; set; }
        public string Hash { get; set; }
        public string Body { get; set; }
    }

    public class Builder
    {
        private Page _page = new Page();

        public void SetHeader(string header) => _page.Header = header;
        public void SetAuthor(string author) => _page.Author = author;
        public void SetBody(string body) => _page.Body = body;
        public void SetHash(string hash) => _page.Hash = hash;

        public Page GetPage() => _page;
    }

    public partial class Form1 : Form
    {
        private string _filePath = "";

        public Form1()
        {
            InitializeComponent();
        }

        private void btnOpenFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Текстовые файлы (*.txt)|*.txt|Все файлы (*.*)|*.*";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                _filePath = openFileDialog.FileName;
                lblFilePath.Text = "Выбран: " + _filePath;
            }
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(_filePath))
            {
                MessageBox.Show("Сначала выберите файл!", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                string[] lines = File.ReadAllLines(_filePath);

                if (lines.Length < 3)
                {
                    MessageBox.Show("В файле слишком мало строк.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                Builder builder = new Builder();
                builder.SetHeader(lines[0]);
                builder.SetAuthor(lines[1]);
                builder.SetHash(lines[lines.Length - 1]);

                string bodyText = "";
                for (int i = 2; i < lines.Length - 1; i++)
                {
                    bodyText += lines[i] + "\n";
                }
                builder.SetBody(bodyText);

                Page resultPage = builder.GetPage();

                string textToHash = resultPage.Header + resultPage.Author + resultPage.Body;
                string calculatedHash = CalculateSha256(textToHash);
                bool isHashValid = calculatedHash.Equals(resultPage.Hash, StringComparison.OrdinalIgnoreCase);

                string json = JsonSerializer.Serialize(resultPage, new JsonSerializerOptions
                {
                    WriteIndented = true,
                    Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping
                });

                rtbOutput.Text = json + "\n\n" +
                    "===================================\n" +
                    "РЕЗУЛЬТАТ ПРОВЕРКИ ХЕША:\n" +
                    $"Хеш в файле:     {resultPage.Hash}\n" +
                    $"Вычисленный хеш: {calculatedHash}\n" +
                    $"Статус:          {(isHashValid ? "ХЕШ ВЕРЕН" : "ОШИБКА (данные повреждены)")}";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка: " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private string CalculateSha256(string input)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = Encoding.UTF8.GetBytes(input);
                byte[] hashBytes = sha256.ComputeHash(bytes);

                StringBuilder builder = new StringBuilder();
                foreach (byte b in hashBytes)
                {
                    builder.Append(b.ToString("x2"));
                }
                return builder.ToString();
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(rtbOutput.Text))
            {
                MessageBox.Show("Сначала конвертируйте статью!", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                string userProfile = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
                string downloadsPath = Path.Combine(userProfile, "Downloads");

                if (!Directory.Exists(downloadsPath))
                {
                    downloadsPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                }

                string fileName = $"article_{DateTime.Now:yyyy-MM-dd_HH-mm-ss}.json";
                string fullPath = Path.Combine(downloadsPath, fileName);

                File.WriteAllText(fullPath, rtbOutput.Text);

                MessageBox.Show($"Файл сохранен:\n{fullPath}", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Не удалось сохранить файл: " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}