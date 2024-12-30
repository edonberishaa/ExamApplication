using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Printing;

namespace ExamApplication
{
    public partial class QuestionAnswers : Form
    {
        protected List<QuestionsControl> questionControls;
        protected int currentQuestIndex = 0;
        protected int correctAnswersCount = 0;
        private int questionCount = 0;


        public QuestionAnswers()
        {
            InitializeComponent();
            this.KeyDown += new KeyEventHandler(QuestionAnswers_KeyDown);
            this.KeyPreview = true; // Enable key preview for the form
            questionControls = new List<QuestionsControl>();
            panelQA.AutoScroll = true;


        }
        private void AddQuestion()
        {
            string question = txtQuestion.Text;
            string[] answers = new string[]
            {
                txtAnswer1.Text,
                txtAnswer2.Text,
                txtAnswer3.Text,
                txtAnswer4.Text
            };
            string correctAnswer = GetCorrectAnswer();

            BaseFieldsClass.QuestionsDictionary[question] = correctAnswer;
            BaseFieldsClass.AnswersOptionsList.Add(answers);

            //MessageBox.Show("Question added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

            DisplayQuestionInPanel(question, answers);

            txtQuestion.Clear();
            txtAnswer1.Clear();
            txtAnswer2.Clear();
            txtAnswer3.Clear();
            txtAnswer4.Clear();
            radioButton1.Checked = false;
            radioButton2.Checked = false;
            radioButton3.Checked = false;
            radioButton4.Checked = false;
        }
        private void DisplayQuestionInPanel(string question, string[] answers)
        {
            Panel questionPanel = new Panel
            {
                AutoSize = true,
                BorderStyle = BorderStyle.None,
                Margin = new Padding(10),
                BackColor = Color.Transparent
            };

            int questionNumber = panelQA.Controls.Count + 1;
            Label questionLabel = new Label
            {
                Text = $"{questionNumber}.{question}",
                AutoSize = true,
                Font = new System.Drawing.Font("Arial", 11, System.Drawing.FontStyle.Italic),
                Margin = new Padding(5)
            };

            Panel answerPanel = new Panel { AutoSize = true, Margin = new Padding(5) };

            int answerYPosition = 30;

            for (int i = 0; i < answers.Length; i++)
            {
                if (!string.IsNullOrWhiteSpace(answers[i]))
                {
                    // Convert i to a character starting from 'a'
                    char answerPrefix = (char)(97 + i); // 'a' is ASCII 97

                    Label ansLbl = new Label
                    {
                        Text = $"{answerPrefix}) {answers[i]}", // Add the prefix to the answer text
                        AutoSize = true,
                        Location = new System.Drawing.Point(10, answerYPosition),
                        Font = new System.Drawing.Font("Arial", 10)
                    };

                    answerPanel.Controls.Add(ansLbl);
                    answerYPosition += ansLbl.Height + 5; // Update the position for the next answer
                }
            }

            questionPanel.Controls.Add(questionLabel);
            questionPanel.Controls.Add(answerPanel);

            questionPanel.Location = new System.Drawing.Point(100, panelQA.Controls.Count * (questionPanel.Height + 70));

            panelQA.Controls.Add(questionPanel);

            panelQA.AutoScroll = true;
        }
        private string GetCorrectAnswer()
        {
            if (radioButton1.Checked) return txtAnswer1.Text;
            if (radioButton2.Checked) return txtAnswer2.Text;
            if (radioButton3.Checked) return txtAnswer3.Text;
            if (radioButton4.Checked) return txtAnswer4.Text;
            return null;
        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            AddQuestion();

        }
        private void QuestionAnswers_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.P)
            {
                PrintPanelContents();
            }
        }
        private void PrintPanelContents()
        {
            PrintDocument printDocument = new PrintDocument();
            printDocument.DefaultPageSettings.PaperSize = new PaperSize("A4", 827, 1169);
            printDocument.PrintPage += PrintPage;

            PrintPreviewDialog previewDialog = new PrintPreviewDialog
            {

                Document = printDocument,
                Width = 800,
                Height = 600
            };
            previewDialog.ShowDialog();
        }

        private void PrintPage(object sender, PrintPageEventArgs e)
        {
            int xPosition = e.MarginBounds.Left;
            int yPosition = e.MarginBounds.Top;
            int lineSpacing = 5; 

            Font questionFont = new Font("Arial", 13, FontStyle.Bold);
            Font answerFont = new Font("Arial", 12, FontStyle.Regular);

            foreach (Control control in panelQA.Controls)
            {
                if (control is Panel questionPanel)
                {
                    Label questionLabel = questionPanel.Controls.OfType<Label>().FirstOrDefault();
                    if (questionLabel != null)
                    {
                        e.Graphics.DrawString(questionLabel.Text, questionFont, Brushes.Black, xPosition, yPosition);
                        yPosition += (int)(e.Graphics.MeasureString(questionLabel.Text, questionFont, e.MarginBounds.Width).Height + lineSpacing); // Move down for the answers
                    }

                    char answerPrefix = 'a'; 
                    Panel answerPanel = questionPanel.Controls.OfType<Panel>().FirstOrDefault();
                    if (answerPanel != null) 
                    {
                        foreach (Control answerControl in answerPanel.Controls)
                        {
                            if (answerControl is Label answerLabel)
                            {
                                string answerText = $"{answerLabel.Text}";

                                e.Graphics.DrawString(answerText, answerFont, Brushes.Black, xPosition + 20, yPosition);
                                yPosition += (int)(e.Graphics.MeasureString(answerText, answerFont, e.MarginBounds.Width).Height + lineSpacing); // Update position for next answer
                                answerPrefix++; 
                            }
                        }
                    }

                    yPosition += lineSpacing;
                    // Check if we need to start a new page if the yPosition exceeds the margins
                    if (yPosition + lineSpacing > e.MarginBounds.Bottom)
                    {
                        e.HasMorePages = true;
                        return; // Exit the method, the PrintDocument will call PrintPage again
                    }
                }
            }

            e.HasMorePages = false; // No more pages to print
        }
    }
}
