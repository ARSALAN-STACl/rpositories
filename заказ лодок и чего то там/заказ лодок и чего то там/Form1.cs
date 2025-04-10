using System;
using System.IO;
using System.Windows.Forms;

namespace заказ_лодок_и_чего_то_там
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            comboBox1.Items.AddRange(new string[] { "Шлюпка", "Парусная лодка", "Галера" });
            comboBox2.Items.AddRange(new string[] { "Белый", "Синий", "Красный", "Зеленый" });
            comboBox3.Items.AddRange(new string[] { "Новый", "В обработке", "Завершен" });
            comboBox4.Items.AddRange(new string[] { "Да", "Нет" });
            checkedListBox1.Items.AddRange(new string[] { "Дерево", "Алюминий", "Карбон" , "дуб" , "сосна" , "лиственица"});
        }
        private void UpdatePrice()
        {
            int basePrice = 0;
            int accessoriesPrice = 0;
            int materialsPrice = 0;

            // Цена лодки в зависимости от типа
            switch (comboBox1.SelectedItem?.ToString())
            {
                case "Шлюпка":
                    basePrice = 3000;
                    break;
                case "Парусная лодка":
                    basePrice = 8000;
                    break;
                case "Галера":
                    basePrice = 10000;
                    break;
            }

            // Количество мест 
            basePrice += (int)numericUpDown1.Value * 500;

            // Если есть мачта
            if (comboBox4.SelectedItem?.ToString() == "Да")
            {
                basePrice += 3600;
            }

            // Подсчет стоимости аксессуаров
            if (checkBox1.Checked) accessoriesPrice += 400; // Холодильник
            if (checkBox2.Checked) accessoriesPrice += 250; // Зонтик
            if (checkBox3.Checked) accessoriesPrice += 500; // Навес
            if (checkBox4.Checked) accessoriesPrice += 400; // Весла
            if (checkBox5.Checked) accessoriesPrice += 200; // Спасательный жилет

            // Подсчет стоимости материалов
            foreach (var item in checkedListBox1.CheckedItems)
            {
                switch (item.ToString())
                {
                    case "Дерево":
                        materialsPrice += 3000;
                        break;
                    case "Алюминий":
                        materialsPrice += 5000;
                        break;
                    case "Карбон":
                        materialsPrice += 8000;
                        break;
                    case "дуб":
                        materialsPrice += 8000;
                        break;
                    case "сосна":
                        materialsPrice += 8000;
                        break;
                    case "лиственица":
                        materialsPrice += 8000;
                        break;
                }
            }

            // Обновление Label 12 13
            label12.Text = $"Аксессуары: {accessoriesPrice} руб.";
            label13.Text = $"Общая стоимость: {basePrice + accessoriesPrice + materialsPrice} руб.";
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e) { UpdatePrice(); }
        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e) { UpdatePrice(); }
        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e) { UpdatePrice(); }
        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e) { UpdatePrice(); }
        private void numericUpDown1_ValueChanged(object sender, EventArgs e) { UpdatePrice(); }
        private void checkBox1_CheckedChanged(object sender, EventArgs e) { UpdatePrice(); }
        private void checkBox2_CheckedChanged(object sender, EventArgs e) { UpdatePrice(); }
        private void checkBox3_CheckedChanged(object sender, EventArgs e) { UpdatePrice(); }
        private void checkBox4_CheckedChanged(object sender, EventArgs e) { UpdatePrice(); }
        private void checkBox5_CheckedChanged(object sender, EventArgs e) { UpdatePrice(); }
        private void checkedListBox1_SelectedIndexChanged(object sender, EventArgs e) { UpdatePrice(); }

        private void button1_Click(object sender, EventArgs e)
        {
            string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            string filePath = Path.Combine(desktopPath, "ЗАКАЗ.txt");

            using (StreamWriter writer = new StreamWriter(filePath))
            {
                writer.WriteLine($"Номер заказа: {textBox1.Text}");
                writer.WriteLine($"ФИО заказчика: {textBox2.Text}");
                writer.WriteLine($"Тип лодки: {comboBox1.SelectedItem}");
                writer.WriteLine($"Цвет лодки: {comboBox2.SelectedItem}");
                writer.WriteLine($"Статус заказа: {comboBox3.SelectedItem}");
                writer.WriteLine($"Наличие мачты: {comboBox4.SelectedItem}");
                writer.WriteLine($"Количество мест: {numericUpDown1.Value}");
                writer.WriteLine("Материалы:"); 
                foreach (var item in checkedListBox1.CheckedItems)
                {                    writer.WriteLine($" - {item}");
                }
                writer.WriteLine("Дополнительные аксессуары:");
                if (checkBox1.Checked) writer.WriteLine(" - Холодильник");
                if (checkBox2.Checked) writer.WriteLine(" - Зонтик");
                if (checkBox3.Checked) writer.WriteLine(" - Навес");
                if (checkBox4.Checked) writer.WriteLine(" - Весла");
                if (checkBox5.Checked) writer.WriteLine(" - Спасательный жилет");
                writer.WriteLine(label12.Text);
                writer.WriteLine(label13.Text);
            }

            MessageBox.Show("Заказ сохранен в файл 'ЗАКАЗ.txt' на рабочем столе!", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}