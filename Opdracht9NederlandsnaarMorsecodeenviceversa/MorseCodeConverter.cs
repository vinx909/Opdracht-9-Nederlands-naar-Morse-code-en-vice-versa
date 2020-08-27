using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Opdracht9NederlandsnaarMorsecodeenviceversa
{
    public class MorseCodeConverter
    {
        private const char space = ' ';
        private const char morseSpaceBetweenCharacters = ' ';
        private const char morseSpaceBetweenWords = ' ';
        private const string emptyString = "";
        private const string codeBlockNotFoundOpening = "[";
        private const string codeBlockNotFoundClosing = "]";
        private const string codeBlockNotFoundMessage = "\r\n\r\npart of the morse code was not found. the parts that where not found are between " + codeBlockNotFoundOpening + " and " + codeBlockNotFoundClosing;

        private static List<MorseCodeOption> morseCodeOptions;

        public static string ConvertToMorseCode(string text)
        {
            SetupMorseCodeOption();
            string toReturn = "";
            char[] characters = text.ToCharArray();
            foreach (char character in characters)
            {
                if (character.Equals(space))
                {
                    toReturn += morseSpaceBetweenWords;
                }
                else
                {
                    foreach(MorseCodeOption option in morseCodeOptions)
                    {
                        if (option.IsSameSymbol(character))
                        {
                            toReturn += option.GetMorseCode();
                        }
                    }
                }
                toReturn += morseSpaceBetweenCharacters;
            }
            return toReturn;
        }

        public static string ConvertToLetter(string text)
        {
            SetupMorseCodeOption();
            string toReturn = "";
            char[] characters = text.ToCharArray();
            string currentCodeBlock = emptyString;
            bool nothingNotFound = true;
            foreach(char character in characters)
            {
                if(character.Equals(morseSpaceBetweenCharacters))
                {
                    if (currentCodeBlock.Equals(emptyString)){
                        if (character.Equals(morseSpaceBetweenWords))
                        {
                            toReturn += space;
                        }
                    }
                    else
                    {
                        try
                        {
                            toReturn += GetSymbolFromMorseCode(currentCodeBlock, ref nothingNotFound);
                        }
                        catch(ExceptionMorseCodeNotFound exception)
                        {
                            toReturn += exception.GetNotFoundMessage();
                            nothingNotFound = false;
                        }
                        currentCodeBlock = emptyString;
                    }
                }
                else
                {
                    if (character.Equals(morseSpaceBetweenWords))
                    {
                        toReturn += space;
                    }
                    else
                    {
                        currentCodeBlock += character;
                    }
                }
            }
            if (!currentCodeBlock.Equals(emptyString))
            {
                try
                {
                    toReturn += GetSymbolFromMorseCode(currentCodeBlock, ref nothingNotFound);
                }
                catch (ExceptionMorseCodeNotFound exception)
                {
                    toReturn += exception.GetNotFoundMessage();
                    nothingNotFound = false;
                }
            }
            if (nothingNotFound == false)
            {
                toReturn += codeBlockNotFoundMessage;
            }
            return toReturn;
        }

        private static char GetSymbolFromMorseCode(string morseCode, ref bool nothingNotFound)
        {
            foreach (MorseCodeOption option in morseCodeOptions)
            {
                if (option.IsSameMorseCode(morseCode))
                {
                    return option.GetSymbol();
                }
            }
            ExceptionMorseCodeNotFound exception = new ExceptionMorseCodeNotFound();
            exception.SetNotFoundMessage(codeBlockNotFoundOpening + morseCode + codeBlockNotFoundClosing);
            throw exception;
            
        }

        private static void SetupMorseCodeOption()
        {
            if(morseCodeOptions == null)
            {
                morseCodeOptions = new List<MorseCodeOption>();
                
                //setup after and in the same Order as "Letters, numbers, punctuation, prosigns for Morse code and non-English variants" from https://en.wikipedia.org/wiki/Morse_code on 27-08-2020
                //letters
                morseCodeOptions.Add(new MorseCodeOption('a', "._"));
                morseCodeOptions.Add(new MorseCodeOption('b', "_..."));
                morseCodeOptions.Add(new MorseCodeOption('c', "_._."));
                morseCodeOptions.Add(new MorseCodeOption('d', "_.."));
                morseCodeOptions.Add(new MorseCodeOption('e', "."));
                morseCodeOptions.Add(new MorseCodeOption('f', ".._."));
                morseCodeOptions.Add(new MorseCodeOption('g', "__."));
                morseCodeOptions.Add(new MorseCodeOption('h', "...."));
                morseCodeOptions.Add(new MorseCodeOption('i', ".."));
                morseCodeOptions.Add(new MorseCodeOption('j', ".___"));
                morseCodeOptions.Add(new MorseCodeOption('k', "_._"));
                morseCodeOptions.Add(new MorseCodeOption('l', "._.."));
                morseCodeOptions.Add(new MorseCodeOption('m', "__"));
                morseCodeOptions.Add(new MorseCodeOption('n', "_."));
                morseCodeOptions.Add(new MorseCodeOption('o', "___"));
                morseCodeOptions.Add(new MorseCodeOption('p', ".__."));
                morseCodeOptions.Add(new MorseCodeOption('q', "__._"));
                morseCodeOptions.Add(new MorseCodeOption('r', "._."));
                morseCodeOptions.Add(new MorseCodeOption('s', "..."));
                morseCodeOptions.Add(new MorseCodeOption('t', "_"));
                morseCodeOptions.Add(new MorseCodeOption('u', ".._"));
                morseCodeOptions.Add(new MorseCodeOption('v', "..._"));
                morseCodeOptions.Add(new MorseCodeOption('w', ".__"));
                morseCodeOptions.Add(new MorseCodeOption('x', "_.._"));
                morseCodeOptions.Add(new MorseCodeOption('y', "_.__"));
                morseCodeOptions.Add(new MorseCodeOption('z', "__.."));
                //numbers
                morseCodeOptions.Add(new MorseCodeOption('0', "_____"));
                morseCodeOptions.Add(new MorseCodeOption('1', ".____"));
                morseCodeOptions.Add(new MorseCodeOption('2', "..___"));
                morseCodeOptions.Add(new MorseCodeOption('3', "...__"));
                morseCodeOptions.Add(new MorseCodeOption('4', "...._"));
                morseCodeOptions.Add(new MorseCodeOption('5', "....."));
                morseCodeOptions.Add(new MorseCodeOption('6', "_...."));
                morseCodeOptions.Add(new MorseCodeOption('7', "__..."));
                morseCodeOptions.Add(new MorseCodeOption('8', "___.."));
                morseCodeOptions.Add(new MorseCodeOption('9', "____."));
                //punctuation
                morseCodeOptions.Add(new MorseCodeOption('.', "._._._"));
                morseCodeOptions.Add(new MorseCodeOption(',', "__..__"));
                morseCodeOptions.Add(new MorseCodeOption('?', "..__.."));
                morseCodeOptions.Add(new MorseCodeOption('\'', ".____."));
                morseCodeOptions.Add(new MorseCodeOption('!', "_._.__"));
                morseCodeOptions.Add(new MorseCodeOption('/', "_.._."));
                morseCodeOptions.Add(new MorseCodeOption('(', "_.__."));
                morseCodeOptions.Add(new MorseCodeOption(')', "_.__._"));
                morseCodeOptions.Add(new MorseCodeOption('&', "._..."));
                morseCodeOptions.Add(new MorseCodeOption(':', "___..."));
                morseCodeOptions.Add(new MorseCodeOption(';', "_._._."));
                morseCodeOptions.Add(new MorseCodeOption('=', "_..._"));
                morseCodeOptions.Add(new MorseCodeOption('+', "._._."));
                morseCodeOptions.Add(new MorseCodeOption('-', "_...._"));
                morseCodeOptions.Add(new MorseCodeOption('_', "..__._"));
                morseCodeOptions.Add(new MorseCodeOption('"', "._.._."));
                morseCodeOptions.Add(new MorseCodeOption('$', "..._.._"));
                morseCodeOptions.Add(new MorseCodeOption('@', ".__._."));

                //test if there are no repeats
                for (int i = 0; i < morseCodeOptions.Count; i++)
                {
                    for(int o = i + 1; o < morseCodeOptions.Count; o++)
                    {
                        if (morseCodeOptions[i].IsSameSymbol(morseCodeOptions[o].GetSymbol()))
                        {
                            throw new Exception("morseCodeOptions has the same symbol as index " + i + " and " + o);
                        }
                        if (morseCodeOptions[i].IsSameMorseCode(morseCodeOptions[o].GetMorseCode()))
                        {
                            throw new Exception("morseCodeOptions has the same symbol as index " + i + " and " + o);
                        }
                    }
                }
            }
        }

        private class MorseCodeOption
        {
            private char symbol;
            private string morseCode;

            internal MorseCodeOption(char symbol, string morseCode)
            {
                this.symbol = char.ToLower(symbol);
                this.morseCode = morseCode;
            }
            internal char GetSymbol()
            {
                return symbol;
            }
            internal string GetMorseCode()
            {
                return morseCode;
            }
            internal bool IsSameSymbol(char symbol)
            {
                return this.symbol.Equals(char.ToLower(symbol));
            }
            internal bool IsSameMorseCode(string morseCode)
            {
                return this.morseCode.Equals(morseCode);
            }
        }

        [Serializable]
        private class ExceptionMorseCodeNotFound : Exception
        {
            string notFoundMessage;

            public ExceptionMorseCodeNotFound()
            {
            }
            public ExceptionMorseCodeNotFound(string message) : base(message)
            {
            }
            public ExceptionMorseCodeNotFound(string message, Exception innerException) : base(message, innerException)
            {
            }
            protected ExceptionMorseCodeNotFound(SerializationInfo info, StreamingContext context) : base(info, context)
            {
            }

            internal void SetNotFoundMessage(string message)
            {
                notFoundMessage = message;
            }
            internal string GetNotFoundMessage()
            {
                return notFoundMessage;
            }
        }
    }
}