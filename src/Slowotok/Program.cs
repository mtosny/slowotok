using System;
using System.Linq;

namespace Slowotok
{
    public class Program
    {
        /// <summary>
        /// Określa możliwe do ułożenia słowa z liter występujących na planszy gry Słowotok.
        /// Na początku prosi o podane dostępnych liter w grze w odpowiedniej kolejności. Następnie
        /// korzystając z listy słów dostępnych w słownikach poszukuje słów możliwych do ułożenia
        /// uwzględniają w tym celu częstoś występowania liter w słowie i dostępnych na planszy.
        /// Dla możliwych do ułożenia stara się znaleźć ścieżkę suwania palcem po planszy, 
        /// umożliwiającą napisanie słowa. Na koniec wypisuje co najwyżej 50 najdłuższych słów,
        /// które są możliwe do napisania, a wszystkie możliwe rozwiązania zapisuje do pliku.
        /// </summary>
        public static void Main()
        {
            var boardCharacters = new BoardReader().ReadBoard();
            var board = new Board(boardCharacters);

            var wordsPossibleToSwap = new WordsReader()
                .GetWords()
                .Select(word => new Word(word))
                .Where(board.IsPossibleToCombineWord)
                .Where(word => board.IsPossibleToSwipeWord(word))
                // przy korzystaniu z wielu słowników mogą wystąpić powtórzenia, których warto się pozbyć
                .Distinct()
                .OrderByDescending(word => word.Value.Length)
                .ToList();

            wordsPossibleToSwap.Take(50).ToList().ForEach(word => Console.WriteLine(word.Value));

            var filePathName = string.Format("solutions_{0}.txt", boardCharacters);
            new SolutionsWriter().SaveToJson(board.Solutions, filePathName);
        }
    }
}
