using System.Collections.Generic;
using System.Linq;

namespace Slowotok
{
    /// <summary>
    /// Reprezentacja słowa.
    /// </summary>
    public class Word
    {
        private readonly string _value;

        public Word(string value)
        {
            _value = value;
        }

        /// <summary>
        /// Zwraca listę unikalnych znaków w słowie.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<char> UniqueChars()
        {
            return _value.Distinct();
        }
        
        /// <summary>
        /// Zwraca częstość wystąpienia danej litery w słowie.
        /// </summary>
        /// <param name="c">znak, którego częstość w słowie ma być określona</param>
        /// <returns>częstość wystąpienia podanego znaku w słowie</returns>
        public int CharFrequency(char c)
        {
            return _value.Count(s => s == c);
        }

        /// <summary>
        /// Literał słowa.
        /// </summary>
        public string Value
        {
            get { return _value; }
        }
    }
}
