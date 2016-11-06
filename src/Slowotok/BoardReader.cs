using System;
using System.Text;

namespace Slowotok
{
    /// <summary>
    /// Klasa odpowiedzialna za wczytywanie danych o planszy.
    /// </summary>
    public class BoardReader
    {
        /// <summary>
        /// Pobiera zestaw znaków występujący na planszy gry Słowotok.
        /// </summary>
        /// <returns></returns>
        public string ReadBoard()
        {
            // Rozwiązanie problemu konwersji polskich znaków (np. ś -> s)
            Console.InputEncoding = Encoding.Unicode;
            Console.OutputEncoding = Encoding.Unicode;
            Console.WriteLine("Podaj wszystkie litery występujące na planszy w jednym wierszu (wierszami od góry, od lewej do prawej):");
            string chars;
            do
            {
                chars = Console.In.ReadLine();
                chars = chars != null ? chars.ToLowerInvariant() : string.Empty;
                if (LettersOnBoardValidationStatus.Ok != Board.ValidateBoardLetters(chars))
                {
                    Console.WriteLine("Niepoprawny zestaw znaków na planszy. Podaj go jeszcze raz:");
                }
                else
                {
                    break;
                }
            } while (true);
            return chars;
        }
    }
}
