using System;
using System.ComponentModel.Design;
using System.Globalization;
using System.IO;
namespace MultiParadigm_Lab1_1
{
    class Task_1
    {
        public static void Main(string[] args)
        {
            int MaxFrequentCount = 25;
            string Path = "Path";
            String Content = File.ReadAllText(Path);
            Content += "$";
            String[] IgWord = { "a", "in", "an", "for", "the" };
            String UCase = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            String LCase = ",.!?-;“”abcdefghijklmnopqrstuvwxyz";
            String TempContent = "";
            int i = 0;
            StrCheck:
            if (Content[i] == ' ' || Content[i] == '\n')
            {
                i++;
                TempContent += ' ';
                goto StrCheck;
            }
            if (Content[i] == '\r')
            {
                i++;
                goto StrCheck;
            }
            if (Content[i] == '$')
            {
                TempContent += ' ';
                TempContent += '$';
                goto StrCheckEnd;
            }
            bool Lower = false;
            int LCaseIndex = 0;
            LCaseCheck:
            if (Content[i] == LCase[LCaseIndex]) Lower = true;
            {
                LCaseIndex++;
            }
            if (LCaseIndex != LCase.Length && Lower == false)
            {
                goto LCaseCheck;
            }
            if (Lower)
            {
                TempContent += Content[i];
            }
            else
            {
                int j = 0;
                getLCase:
                if (Content[i] != UCase[j])
                {
                    j++;
                    goto getLCase;
                }
                TempContent += LCase[j];
            }
            i++;
            goto StrCheck;
            StrCheckEnd:
            String Word = "";
            Object[,] Result = new Object[20000, 2];
            int WordCount = 0;
            i = 0;
            WordCheck:
            if (TempContent[i] == ' ')
            {
                int IgWordCount = 0;
                bool IgState = false;
                IgWordcheck:
                if (Word.Equals(IgWord[IgWordCount]))
                {
                    IgState = true;
                }
                else
                {
                    IgWordCount++;
                    if (!(IgWordCount >= IgWord.Length)) goto IgWordcheck;
                }
                if (IgState)
                {
                    Word = "";
                    i++;
                    goto WordCheck;
                }
                int j = 0;
                Insert:
                if (Word.Equals((String)Result[j, 0]))
                {
                    Result[j, 1] = (int)Result[j, 1] + 1;
                    i++;
                    Word = "";
                    goto WordCheck;
                }
                if (Result[j, 0] == null)
                {
                    if (Word.Equals(""))
                    {
                        i++;
                        Word = "";
                        goto WordCheck;
                    }
                    WordCount = j;
                    Result[j, 0] = Word;
                    Result[j, 1] = 1;
                    i++;
                    Word = "";
                    goto WordCheck;
                }
                j++;
                goto Insert;
            }
            if (TempContent[i] == '$')
            {
                WordCount++;
                goto PrepFrequencies;
            }
            Word += TempContent[i];
            i++;
            goto WordCheck;
            PrepFrequencies:
            int[] Frequency = new int[WordCount];
            i = 0;
            AddFrequency:
            Frequency[i] = (int)Result[i, 1];
            i++;
            if (i < WordCount)
            {
                goto AddFrequency;
            }
            int Size = Frequency.Length;
            int Temp;
            int Write = 0;
            Out:
            Write++;
            int Sort = 0;
            In:
            if (Frequency[Sort] < Frequency[Sort + 1])
            {
                Temp = Frequency[Sort + 1];
                Frequency[Sort + 1] = Frequency[Sort];
                Frequency[Sort] = Temp;
            }
            Sort++;
            if (Sort < Size - 1)
            {
                goto In;
            }

            if (Write < Size)
            {
                goto Out;
            }
            i = 0;
            int PresFrequency = Frequency[0];
            int Max = MaxFrequentCount;
            int PrevFrequency;
            FrequencyPrint:
            int k = 0;
            ResultPrint:
            if ((int)Result[k, 1] == PresFrequency)
            {
                Console.WriteLine(Result[k, 0] + " - " + PresFrequency);
            }
            k++;
            if (k != WordCount)
            {
                goto ResultPrint;
            }
            DecFrequency:
            if (PresFrequency == 1)
            {
                PresFrequency = 0;
                goto End;
            }
            i++;
            PrevFrequency = PresFrequency;
            if (i >= Frequency.Length)
            {
                goto Exit;
            }
            PresFrequency = Frequency[i];
            if (PresFrequency == PrevFrequency)
            {
                goto DecFrequency;
            }
            End:
            Max--;
            if (Max != 0 && PresFrequency != 0)
            {
                goto FrequencyPrint;
            }
            Exit:
            i = 0;
        }
    }
}