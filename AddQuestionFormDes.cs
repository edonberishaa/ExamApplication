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
    public partial class AddQuestionFormDes : Form
    {
        protected Dictionary<string, string> questionsDictionary;
        protected List<string[]> answersOptionsList;
        protected List<QuestionsControl> questionControls;
        protected int currentQuestIndex = 0;
        protected int correctAnswersCount = 0;
        public AddQuestionFormDes()
        {
            InitializeComponent();
            questionsDictionary = new Dictionary<string, string>();
            answersOptionsList = new List<string[]>();
            questionControls = new List<QuestionsControl>();

        }
    }
}
