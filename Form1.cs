using System;
using System.Windows.Forms;
using System.ComponentModel;

namespace is_code_development_4
{
    public partial class Form1 : Form
    {
        private Label lblInput; // Метка для ввода массива
        private TextBox txtInput; // Поле для ввода элементов массива
        private Label lblType; // Метка для выбора типа данных
        private ComboBox cmbType; // Выпадающий список для выбора типа
        private Button btnSort; // Кнопка для сортировки
        private Button btnRange; // Кнопка для вычисления размаха
        private TextBox txtOutput; // Поле для вывода результатов
        private IContainer components = null; // Контейнер для компонентов

        public Form1()
        {
            components = new Container();
            InitializeComponents();
        }

        // Инициализация элементов интерфейса
        private void InitializeComponents()
        {
            // Настройка формы
            this.Text = "Обработка массивов";
            this.Size = new System.Drawing.Size(500, 450);
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;

            // Метка для ввода массива с примером
            lblInput = new Label
            {
                Text = "Введите элементы массива (через запятую):\nПример: 64, 34, 25 или apple, banana",
                Location = new System.Drawing.Point(20, 20),
                Size = new System.Drawing.Size(300, 40)
            };

            // Поле для ввода элементов
            txtInput = new TextBox
            {
                Location = new System.Drawing.Point(20, 70),
                Size = new System.Drawing.Size(440, 30)
            };

            // Метка для выбора типа данных
            lblType = new Label
            {
                Text = "Выберите тип данных:",
                Location = new System.Drawing.Point(20, 110),
                Size = new System.Drawing.Size(150, 20)
            };

            // Выпадающий список для выбора типа
            cmbType = new ComboBox
            {
                Location = new System.Drawing.Point(170, 110),
                Size = new System.Drawing.Size(150, 20),
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            cmbType.Items.AddRange(new string[] { "Целые числа (int)", "Дробные числа (double)", "Строки (string)" });
            cmbType.SelectedIndex = 0;

            // Кнопка "Сортировать"
            btnSort = new Button
            {
                Text = "Сортировать (метод Шелла)",
                Location = new System.Drawing.Point(20, 140),
                Size = new System.Drawing.Size(200, 30)
            };
            btnSort.Click += BtnSort_Click;

            // Кнопка "Вычислить размах"
            btnRange = new Button
            {
                Text = "Вычислить размах",
                Location = new System.Drawing.Point(230, 140),
                Size = new System.Drawing.Size(200, 30)
            };
            btnRange.Click += BtnRange_Click;

            // Поле для вывода результатов
            txtOutput = new TextBox
            {
                Location = new System.Drawing.Point(20, 180),
                Size = new System.Drawing.Size(440, 250),
                Multiline = true,
                ReadOnly = true,
                ScrollBars = ScrollBars.Vertical
            };

            // Добавление элементов на форму
            this.Controls.AddRange(new Control[] { lblInput, txtInput, lblType, cmbType, btnSort, btnRange, txtOutput });
        }

        private void BtnSort_Click(object sender, EventArgs e)
        {
            try
            {
                txtOutput.Clear();
                string[] elements = txtInput.Text.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                if (elements.Length == 0)
                    throw new ArgumentException("Введите хотя бы один элемент массива.");

                string selectedType = cmbType.SelectedItem.ToString();
                if (selectedType == "Целые числа (int)")
                {
                    var processor = new ArrayProcessor<int>(Array.ConvertAll(elements, s => Convert.ToInt32(s.Trim())));
                    txtOutput.AppendText("Исходный массив: " + string.Join(", ", processor.Array) + "\r\n");
                    processor.ShellSort();
                    txtOutput.AppendText("Отсортированный массив: " + string.Join(", ", processor.Array) + "\r\n");
                }
                else if (selectedType == "Дробные числа (double)")
                {
                    var processor = new ArrayProcessor<double>(Array.ConvertAll(elements, s => Convert.ToDouble(s.Trim())));
                    txtOutput.AppendText("Исходный массив: " + string.Join(", ", processor.Array) + "\r\n");
                    processor.ShellSort();
                    txtOutput.AppendText("Отсортированный массив: " + string.Join(", ", processor.Array) + "\r\n");
                }
                else if (selectedType == "Строки (string)")
                {
                    var processor = new ArrayProcessor<string>(Array.ConvertAll(elements, s => s.Trim()));
                    txtOutput.AppendText("Исходный массив: " + string.Join(", ", processor.Array) + "\r\n");
                    processor.ShellSort();
                    txtOutput.AppendText("Отсортированный массив: " + string.Join(", ", processor.Array) + "\r\n");
                }
            }
            catch (FormatException ex)
            {
                MessageBox.Show("Ошибка: введённые данные не соответствуют выбранному типу (например, используйте числа для int или double).", "Ошибка ввода", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Произошла ошибка: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnRange_Click(object sender, EventArgs e)
        {
            try
            {
                txtOutput.Clear();
                string[] elements = txtInput.Text.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                if (elements.Length == 0)
                    throw new ArgumentException("Введите хотя бы один элемент массива.");

                string selectedType = cmbType.SelectedItem.ToString();
                if (selectedType == "Целые числа (int)")
                {
                    var processor = new ArrayProcessor<int>(Array.ConvertAll(elements, s => Convert.ToInt32(s.Trim())));
                    txtOutput.AppendText("Размах: " + processor.Range() + "\r\n");
                }
                else if (selectedType == "Дробные числа (double)")
                {
                    var processor = new ArrayProcessor<double>(Array.ConvertAll(elements, s => Convert.ToDouble(s.Trim())));
                    txtOutput.AppendText("Размах: " + processor.Range() + "\r\n");
                }
                else if (selectedType == "Строки (string)")
                {
                    txtOutput.AppendText("Размах: N/A (не применимо для строк)\r\n");
                }
            }
            catch (FormatException ex)
            {
                MessageBox.Show("Ошибка: введённые данные не соответствуют выбранному типу (например, используйте числа для int или double).", "Ошибка ввода", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (NotSupportedException ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Произошла ошибка: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null)
                components.Dispose();
            base.Dispose(disposing);
        }
    }
}