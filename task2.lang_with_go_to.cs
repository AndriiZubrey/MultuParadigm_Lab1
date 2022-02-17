using System;
using System.IO;
namespace MultiParadigm_Lab1_2
{
    public class Task_2
    {
        public static void Main(string[] args)
        {
            string Path = "Path";
            String[] Content = File.ReadAllLines(Path);
            //int Lines = 45;
            String UCase = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            String LCase = "abcdefghijklmnopqrstuvwxyz ";
            int LineCount = 1;
            string[] LLine = new string[Content.Length];
            string TempContent;
            ViewLine:
            TempContent = "";
            string Line = Content[LineCount - 1];
            if (Line.Length == 0)
            {
                goto NextLine;
            }
            int i = 0;
            StrCheck:
            int LCaseIndex = 0;
            bool Lower = false;
            LCaseCheck:
            if (Content[LineCount - 1][i] == LCase[LCaseIndex])
            {
                Lower = true;
            }
            LCaseIndex++;
            if (LCaseIndex != LCase.Length && Lower == false)
            {
                goto LCaseCheck;
            }
            if (Lower)
            {
                TempContent += Content[LineCount - 1][i];
            }
            else
            {
                int j = 0;
                getLCase:
                if (Content[LineCount - 1][i] != UCase[j])
                {
                    j++;
                    if (j != UCase.Length)
                    {
                        goto getLCase;
                    }
                }
                if (j != LCase.Length) TempContent += LCase[j];
                else if (Content[LineCount - 1][i] != '.' 
                && Content[LineCount - 1][i] != ',' 
                && Content[LineCount - 1][i] != '?' 
                && Content[LineCount - 1][i] != '!'
                && Content[LineCount - 1][i] != '“' 
                && Content[LineCount - 1][i] != '”'
                && Content[LineCount - 1][i] != ';')
                {
                    TempContent += Content[LineCount - 1][i];
                }
            }
            i++;
            if (i != Line.Length)
            {
                goto StrCheck;
            }
            NextLine:
            if (TempContent != null) LLine[LineCount - 1] = TempContent;
            LineCount++;
            if (LineCount != Content.Length)
            {
                goto ViewLine;
            }
            object[,] Result = new object[20000, 4];
            int Page = 1;
            int LineNum = 1;
            ViewPage:
            //int PrevPage = 0;
            Line = LLine[LineNum - 1];
            if (LLine.Length == 0)
            {
                goto GoToNextLine;
            }
            i = 0;
            String Word = "";
            ViewAll:
            if (Line[i] == ' ')
            {
                int ResultCount = 0;
                ViewSameWord:
                if (Word.Equals((String)Result[ResultCount, 0]))
                {
                    Result[ResultCount, 1] = (int)Result[ResultCount, 1] + 1;
                    if ((int)Result[ResultCount, 3] != Page)
                    {
                        Result[ResultCount, 2] = (string)Result[ResultCount, 2] + "," + Page;
                        Result[ResultCount, 3] = Page;
                    }
                    Word = "";
                    goto NextSym;
                }
                if (Result[ResultCount, 0] != null)
                {
                    ResultCount++;
                    goto ViewSameWord;
                }
                Result[ResultCount, 0] = Word;
                Result[ResultCount, 1] = 1;
                Result[ResultCount, 2] = "" + Page;
                Result[ResultCount, 3] = Page;
                Word = "";
                goto NextSym;
            }
            Word += Line[i];
            NextSym:
            i++;
            if (i != Line.Length)
            {
                goto ViewAll;
            }
            GoToNextLine:
            LineNum++;
            if (LineNum % 45 == 0)
            {
                Page++;
            }
            if (LineNum < Content.Length)
            {
                goto ViewPage;
            }
            int k = 0;
            string[] Words = new string[20000];
            GetWords:
            if (Result[k, 0] != null)
            {
                Words[k] = (string)Result[k, 0];
                k++;
                goto GetWords;
            }
            string[] TempWords = Words;
            Words = new string[k];
            int WordCount = 0;
            Rewrite:
            Words[WordCount] = TempWords[WordCount];
            WordCount++;
            if (WordCount != k)
            {
                goto Rewrite;
            }
            string Temp;
            int Write = 0;
            Out:
            Write++;
            int Sort = 0;
            In:
            if (Words[Sort].CompareTo(Words[Sort + 1]) > 0)
            {
                Temp = Words[Sort + 1];
                Words[Sort + 1] = Words[Sort];
                Words[Sort] = Temp;
            }
            Sort++;
            if (Sort < Words.Length - 1)
            {
                goto In;
            }
            if (Write < Words.Length)
            {
                goto Out;
            }
            int Amount = Words.Length;
            int f = 0;
            Print:
            if (f == Amount)
            {
                goto Exit;
            }
            int l = 0;
            PrintView:
            if (f != Amount && Words[f].Equals((String)Result[l, 0]))
            {
                Console.WriteLine(Result[l, 0] + " - " + Result[l, 2]);
                f++;
                goto Print;
            }
            l++;
            goto PrintView;
            Exit:
            i = 0;
        }
    }
}