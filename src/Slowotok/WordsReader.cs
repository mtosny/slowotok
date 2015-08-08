using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Slowotok
{
    /// <summary>
    /// Klasa odpowiedzialna za wczytywanie listy słów ze słowników.
    /// </summary>
    public class WordsReader
    {
        /// <summary>
        /// Zwraca listę słów ze wszystkich słowników dostępnych w katalogu /dict.
        /// Jeżeli to samo słowo występuje w różnych słownikach zostanie ono zwrócone więcej niż raz.
        /// </summary>
        /// <returns>lista słów ze słowników</returns>
        public IEnumerable<string> GetWords()
        {
            foreach (var filePath in Directory.GetFiles("./dict/"))
            {
                using (var sr = new StreamReader(filePath, Encoding.UTF8))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        yield return line;
                    }
                }
            }
        }
    }
}
