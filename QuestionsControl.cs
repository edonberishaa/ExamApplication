using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ExamApplication
{
    public partial class QuestionsControl : UserControl
    {
        public string CorrectAnswer { get; set; }
        public QuestionsControl()
        {
            InitializeComponent();
        }
        public void SetQuestion(string question, string[] answers,string correctAnswer)
        {
            lblQuestion.Text = question;

            radioButton1.Text = answers[0];
            radioButton2.Text = answers[1];
            radioButton3.Text = answers[2];
            radioButton4.Text = answers[3];
            CorrectAnswer = correctAnswer;
        }
        public bool IsCorrect()
        {
            string selectedAnswer = GetSelectedAnswer();
            return selectedAnswer == CorrectAnswer;

        }
        private string GetSelectedAnswer()
        {
            if (radioButton1.Checked)
                return radioButton1.Text;
            else if (radioButton2.Checked)
                return radioButton2.Text;
            else if (radioButton3.Checked)
                return radioButton3.Text;
            else if (radioButton4.Checked)
                return radioButton4.Text;
            else
                return string.Empty;
        }
    }
}
