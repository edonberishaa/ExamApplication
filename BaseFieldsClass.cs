using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamApplication
{
    public static class BaseFieldsClass
    {
        public static Dictionary<string, string> QuestionsDictionary { get; } = new Dictionary<string, string>();
        public static List<string[]> AnswersOptionsList { get; } = new List<string[]>();

    }
}
