using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace ExamApplication
{
    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            Testi testForma = new Testi();
            testForma.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            QuestionAnswers qa = new QuestionAnswers();
            qa.Show();
        }
    }
}
