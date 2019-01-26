using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.IO;

namespace _1612018_CS_MiniGame
{
    class Question
    {
        public int isPicked = 0;
        public string imgName;
        public string ans1, ans2;
        public string Hint;
        public string trueAns;
        //public string answer;

        public void GetValueFromFile(StreamReader sr)
        {
            imgName = sr.ReadLine();
            ans1 = sr.ReadLine();
            ans2 = sr.ReadLine();
            Hint = sr.ReadLine();
            trueAns = sr.ReadLine();
        }
    }
    /// <summary>
    /// Interaction logic for GameWindow.xaml
    /// </summary>
    public partial class GameWindow : Window
    {
        // Các biến toàn cục
        int isChecked = 0; // Kiểm tra xem người chơi đã chọn đáp án chưa ?

        // Đếm số câu hỏi đã được chọn
        int numberOfQuiz = 0;
        int correctAns = 0; // Số câu trả lời đúng
        // Tạo mảng 10 câu hỏi sẽ được trình chiếu ra
        Question[] quizToBeAsked = new Question[10];

        public GameWindow()
        {
            InitializeComponent();

            //Thiết lập mở file
            // file src.txt sẽ được để ngay folder chưa file .exe
            FileStream fs = new FileStream("src.txt", FileMode.Open);
            StreamReader sr = new StreamReader(fs);

            // Tạo danh sách lưu các câu hỏi vào
            List<Question> arr = new List<Question>();


            // Đọc giá trị vào danh sach các câu hỏi
            while (!sr.EndOfStream)
            {
                Question cell = new Question();
                cell.GetValueFromFile(sr);

                arr.Add(cell);
            }

            // Đóng luồng đọc file lại
            sr.Close();

            RandomAnswer(arr);

            // Gán lại số câu hỏi để làm biến đếm
            numberOfQuiz = 0;

            // Hiển thị câu hỏi đầu
            PrepareForNextQuiz();
        }

        void RandomAnswer(List<Question> arr)
        {
            // Random chọn ngẫu nhiên
            Random rand = new Random();
            int randNum;

            while (numberOfQuiz < 10)
            {
                randNum = rand.Next(0, arr.Count - 1);
                if (arr[randNum].isPicked == 0)
                {
                    arr[randNum].isPicked = 1;
                    quizToBeAsked[numberOfQuiz] = arr[randNum];
                    numberOfQuiz++;
                }
            }
        }

        // Hảm nạp thông tin cho câu hỏi tiếp theo vào
        void PrepareForNextQuiz()
        {
            //Hiển thị câu hỏi đầu
            DisplayImage.Source = new BitmapImage(new Uri("source/" + quizToBeAsked[numberOfQuiz].imgName, UriKind.Relative));
            // Sử dụng StringBuilder để tránh việc dùng + tạo nhiều chuỗi trung gian
            StringBuilder tmp  = new StringBuilder();
            tmp.Append("Quiz: ");
            tmp.Append(numberOfQuiz + 1);
            tmp.Append("/10");

            txtQuiz.Text = tmp.ToString();

            // Gán chuỗi Hint
            tmp = new StringBuilder();
            tmp.Append("Hint: ");
            tmp.Append(quizToBeAsked[numberOfQuiz].Hint);

            txtHint.Text = tmp.ToString();
            answer1Button.Content = quizToBeAsked[numberOfQuiz].ans1;
            answer2Button.Content = quizToBeAsked[numberOfQuiz].ans2;
        }

        private void btnAns1(object sender, RoutedEventArgs e)
        {
            if (isChecked == 0)
            {
                if (quizToBeAsked[numberOfQuiz].trueAns.CompareTo(answer1Button.Content) == 0)
                {
                    correctAns++;
                    // Đổi màu cho nút bấm, báo hiểu chọn đúng
                    answer1Button.Background = new SolidColorBrush(Colors.Green);
                }
                else
                {
                    // Đổi màu cho nút bấm, báo hiểu chọn sai
                    answer1Button.Background = new SolidColorBrush(Colors.Red);
                }
                isChecked = 1;
            }
        }

        private void btnAns2(object sender, RoutedEventArgs e)
        {
            if (isChecked == 0)
            {
                if (quizToBeAsked[numberOfQuiz].trueAns.CompareTo(answer2Button.Content) == 0)
                {
                    correctAns++;
                    // Đổi màu cho nút bấm, báo hiểu chọn đúng
                    answer2Button.Background = new SolidColorBrush(Colors.Green);
                }
                else
                {
                    // Đổi màu cho nút bấm, báo hiểu chọn sai
                    answer2Button.Background = new SolidColorBrush(Colors.Red);
                }
                isChecked = 1;
            }
        }

        private void btnNext(object sender, RoutedEventArgs e)
        {
            if (numberOfQuiz < 9)
            {
                // Chuyển sang câu hỏi mới
                isChecked = 0;
                numberOfQuiz++;

                // reset mau cho 2 nut bấm
                answer1Button.Background = new SolidColorBrush(Color.FromArgb(255, 221, 221, 221));
                answer2Button.Background = new SolidColorBrush(Color.FromArgb(255, 221, 221, 221));

                PrepareForNextQuiz();

                if (numberOfQuiz == 9)
                {
                    nextButton.Content = "FINISH";
                }
            }
            else
            {
                // Hiện thông báo điểm số
                MessageBox.Show("Correct answer: " + correctAns, "Point");

                // QUay lại màn hình Start
                MainWindow sreen = new MainWindow();
                this.Close();
                sreen.Show();
            }
        }
    }
}
