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
    public partial class Testi : Form
    {
        protected List<QuestionsControl> questionControls;
        protected int currentQuestIndex = 0;
        protected int correctAnswersCount = 0;

        public Testi()
        {
            InitializeComponent();
            LoadQuestions();
        }

        private void LoadQuestions()
        {
            questionControls = new List<QuestionsControl>();
            int index = 0;
            foreach (var entry in BaseFieldsClass.QuestionsDictionary)
            {
                var questionControl = new QuestionsControl();
                questionControl.SetQuestion(entry.Key, BaseFieldsClass.AnswersOptionsList[index], entry.Value);
                questionControls.Add(questionControl);
                index++;
            }
            ShowQuestion(0);
        }
        private void ShowQuestion(int index)
        {
            if (index >= 0 && index < questionControls.Count)
            {
                currentQuestIndex = index;
                panelQuestion.Controls.Clear();  // Clear previous question control
                var currentQuestionControl = questionControls[index];
                currentQuestionControl.Dock = DockStyle.Fill;  // Dock the control to fill the panel
                panelQuestion.Controls.Add(currentQuestionControl);
            }
            else
            {
                ShowResults();  // Show results if no more questions
            }
        }

        private void ShowResults()
        {
            double percentage = (double)correctAnswersCount / questionControls.Count * 100;
            string message = $"You got {correctAnswersCount} out of {questionControls.Count} correct.\n" +
                             $"Your score: {percentage:F2}%";

            var resultDialog = MessageBox.Show(message + "\n\nDo you want to try again?",
                                                "Quiz Results",
                                                MessageBoxButtons.YesNo,
                                                MessageBoxIcon.Information);
            if (resultDialog == DialogResult.Yes)
            {
                correctAnswersCount = 0;  
                currentQuestIndex = 0;  
                LoadQuestions();  
                ShowQuestion(0); 
            }
            else if (resultDialog == DialogResult.No)
            {
                this.Close();
            }
        }
        private void btnSubmit_Click(object sender, EventArgs e)
        {
            int correctAnswers = 0;
            foreach (var questionControl in questionControls)
            {
                if (questionControl.IsCorrect())
                {
                    correctAnswers++;
                }
            }

        }
        private void NextQuestion()
        {
            if (currentQuestIndex < questionControls.Count)
            {
                if (questionControls[currentQuestIndex].IsCorrect())
                {
                    correctAnswersCount++;  // Increment correct answer count
                }
                ShowQuestion(currentQuestIndex + 1);  // Show next question
            }
        }
        
        private void btnSubmit_Click_1(object sender, EventArgs e)
        {
            int correctAnswers = 0;
            foreach (var questionControl in questionControls)
            {
                if (questionControl.IsCorrect())
                {
                    correctAnswers++;
                }
            }
            NextQuestion();
        }
    }
}
