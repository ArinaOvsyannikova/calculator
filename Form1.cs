using System.Diagnostics;

namespace калькулятор
{
    public partial class Form1 : Form
    {
        public string D = "";//действие
        public string nam1 = "0"; //1 набранное число
        public bool k = false; //флаг для начала набора 2 числа
        public bool k1;//флаг для инженерного

        public Form1()
        {
            k1 = false;

            InitializeComponent();
        }
        //0123456789
        private void button1_Click(object sender, EventArgs e)
        {
            Button B = (Button)sender;
            string buttonText = B.Text;

            if (buttonText == "e")
            {
                textBox1.Text = Math.E.ToString();

            }
            else if (buttonText == "π")
            {
                textBox1.Text = Math.PI.ToString();

            }
            else
            {
                if (k)
                {
                    k = false;
                    textBox1.Text = "0";


                }

                if (textBox1.Text == "0")
                {
                    textBox1.Text = buttonText;

                }
                else
                {
                    textBox1.Text += buttonText;

                }
            }
        }
        //c 
        private void button6_Click(object sender, EventArgs e)
        {
            textBox1.Text = "0";
            label1.Text = "";
        }
        // ввод занака
        private void button20_Click(object sender, EventArgs e)
        {
            Button B = (Button)sender;
            D = B.Text;
            nam1 = textBox1.Text;
            k = true;
            label1.Text = nam1 + D;

        }
        //обработка от 2х переменных
        private void button24_Click(object sender, EventArgs e)
        {
            try
            {

                double n1 = Convert.ToDouble(nam1);
                if (n1 == double.NaN) throw new FormatException();
                double n2 = Convert.ToDouble(textBox1.Text);
                Console.WriteLine(n2);
                Console.WriteLine(D);
                double res = n2;
                label1.Text += n2.ToString() + "=";
                switch (D)
                {
                    case "+": res = n1 + n2; break;
                    case "-": res = n1 - n2; break;
                    case "*": res = n1 * n2; break;
                    case "/":
                        if (n2 != 0)
                            res = n1 / n2;
                        else
                            MessageBox.Show("делить на 0 нельзя");
                        break;
                    case "%": res = (n1 / 100) * n2; break;
                    case "x^y":
                        if (n1 >= 0) res = Math.Pow(n1, n2);
                        else if (n1 < 0 && n2 - (int)n2 == 0)
                            res = Math.Pow(n1, n2);
                        else MessageBox.Show("некорректный ввод");
                        break;
                    case "log":
                        if ((n1 != 1) && (n1 > 0) && (n2 > 0))
                            res = Math.Log(n2, n1);
                        else
                            MessageBox.Show("недопустимое значение для логарифма");
                        break;

                    case "mod":
                        if (n2 != 0)
                            res = n1 % n2;
                        else
                            MessageBox.Show("делить на 0 нельзя");
                        break;
                }

                textBox1.Text = res.ToString();
                label1.Text += res.ToString() + " ";
                k = false;
            }
            catch (FormatException)
            {
                MessageBox.Show("Неверный формат. Пожалуйста, введите корректное число.");
            }
        }
        //обработка функций от 1 переменной
        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                Button B = (Button)sender;
                double n1 = Convert.ToDouble(textBox1.Text);
                double res = n1;

                if (B.Text == "+-")
                {
                    res = -n1;
                    textBox1.Text = res.ToString();
                    return;
                }
                D = B.Text;
                label1.Text = D + " " + n1.ToString();
                switch (D)
                {
                    case "1/x":
                        if (n1 != 0) res = 1 / n1;              // Вычисление обратного значения
                        else throw new DivideByZeroException(); // Обработка деления на ноль
                        break;

                    case "sqrt":
                        if (n1 >= 0) res = Math.Sqrt(n1);        // Вычисление квадратного корня
                        else throw new Exception("Квадратный корень отрицательного числа не определен."); // Обработка отрицательного числа под корнем
                        break;

                    case "x^2": res = Math.Pow(n1, 2); break; // Возведение в квадрат
                    case "x^3": res = Math.Pow(n1, 3); break; // Возведение в куб
                    case "10^x": res = Math.Pow(10, n1); break; // Возведение 10 в степень
                    case "|x|": res = Math.Abs(n1); break; // Модуль числа
                    case "n!":
                        if (n1 < 0) throw new Exception("Факториал не определен для отрицательных чисел."); // Обработка отрицательного числа для факториала
                        if (n1 > 18) throw new FormatException("Слишком большое число"); //обработка исключения для огромных чисел
                        res = 1;
                        for (int i = 1; i <= n1; i++) { res *= i; }
                        break; // Вычисление факториала
                    case "ln":
                        if (n1 > 0) res = Math.Log(n1); // Натуральный логарифм
                        else throw new Exception("Натуральный логарифм не определен для не положительных чисел."); // Обработка неположительных чисел для логарифма
                        break;

                    case "sin":
                        if (n1 != 0 && Math.Abs((n1 / Math.PI) % 1) <= double.Epsilon)
                            res = 0;
                        else
                            res = Math.Sin(n1 * Math.PI / 180); break;
                    case "cos":
                        if (n1 != 0 && ((Math.Abs((n1 / Math.PI)) % 2) <= (1 + double.Epsilon)) && (Math.Abs((n1 / (Math.PI / 2)) % 2) >= (1 - double.Epsilon)))
                            res = 0;
                        else
                            res = Math.Cos(n1 * Math.PI / 180); break;

                    case "tan":
                        if (n1 != 0 && ((Math.Abs((n1 / Math.PI)) % 2) <= (1 + double.Epsilon)) && (Math.Abs((n1 / (Math.PI / 2)) % 2) >= (1 - double.Epsilon)))
                        {
                            MessageBox.Show("Тангенс не существует для угла 90° и -90°.");
                            return;
                        }
                        res = Math.Tan(n1 * Math.PI / 180);
                        break;

                    case "cot":
                        if (n1 != 0 && Math.Abs((n1 / Math.PI) % 1) <= double.Epsilon)
                        {
                            MessageBox.Show("Котангенс не существует для угла 0° и 180°.");
                            return;
                        }
                        res = 1 / Math.Tan(n1 * Math.PI / 180);
                        break;

                    case "asin":
                        if (n1 > 1) { MessageBox.Show("не может быть >1"); return; }
                        else
                            res = Math.Asin(n1); break;
                    case "acos":
                        if (n1 > 1) { MessageBox.Show("не может быть >1"); return; }
                        else
                            res = Math.Acos(n1); break;
                    case "atan":

                        res = Math.Atan(n1); break;
                    case "acot":

                        res = Math.Atan(1 / n1); break;
                }
                textBox1.Text = res.ToString();
                label1.Text += "=" + res.ToString() + " ";

            }
            catch (FormatException)
            {
                MessageBox.Show("Неверный формат. Пожалуйста, введите корректное число.");
            }
            catch (DivideByZeroException)
            {
                MessageBox.Show("Деление на ноль недопустимо.");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        //,
        private void button23_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Contains(","))
            { textBox1.Text = textBox1.Text; }
            else
            { textBox1.Text = textBox1.Text + ","; }
        }
        //<--
        private void button7_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            { textBox1.Text = "0"; }
            textBox1.Text = textBox1.Text.Substring(0, textBox1.Text.Length - 1);

        }
        //инж
        private void button25_Click(object sender, EventArgs e)
        {
            if (k1 == false)
            { this.Size = new Size(749, 757); textBox1.Size = new Size(749, 118); k1 = true; button5.Text = "ал";  }
            else { this.Size = new Size(387, 757); textBox1.Size = new Size(366, 118); k1 = false; button5.Text = "ин"; textBox1.Text = "0"; }
        }
        //исключения для переполнения/    {

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.Text == "∞")
            {
                MessageBox.Show("неверный ввод");
                textBox1.Text = "0";
                label1.Text = "";
            }
            if (textBox1.Text == "не число")
            {
                MessageBox.Show("неверный ввод");
                textBox1.Text = "0";
                label1.Text = "";

            }
            if (k1 == false && textBox1.Text.Length > 9)
            {
                textBox1.Text = textBox1.Text.Substring(0, 9);
            }
            if (k1 == true && textBox1.Text.Length > 19)
            {
                textBox1.Text = textBox1.Text.Substring(0, 19);

            }

        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && (e.KeyChar != ',' || textBox1.Text.Contains(",")))
            {
                e.Handled = true;
            }
        }
    }
}
