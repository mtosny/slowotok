using System;
using System.Collections.Generic;
using System.Linq;

namespace Slowotok
{
    /// <summary>
    ///     Reprezentacja planszy gry Słowotok.
    /// </summary>
    public class Board
    {
        /// <summary>
        ///     Ilość znaków na planszy.
        /// </summary>
        public const int CharsCount = 16;

        /// <summary>
        ///     Lista akceptowanych znaków na planszy.
        /// </summary>
        public static readonly List<char> AcceptableCharacters = new List<char>
        {
            'a', 'ą', 'b', 'c', 'ć', 'd', 'e', 'ę', 'f', 'g', 'h', 'i', 'j',
            'k', 'l', 'ł', 'm', 'n', 'ń', 'o', 'ó', 'p', 'q', 'r', 's', 'ś',
            't', 'u', 'v', 'w', 'x', 'y', 'z', 'ź', 'ż'
        };

        /// <summary>
        ///     Lista indeksów sąsiednich pól dla danego indeksu pola planszy.
        /// </summary>
        private static readonly List<int>[] Neighbours =
        {
            new List<int> {1, 4, 5},
            new List<int> {0, 2, 4, 5, 6},
            new List<int> {1, 3, 5, 6, 7},
            new List<int> {2, 6, 7},
            new List<int> {0, 1, 5, 8, 9},
            new List<int> {0, 1, 2, 4, 6, 8, 9, 10},
            new List<int> {1, 2, 3, 5, 7, 9, 10, 11},
            new List<int> {2, 3, 6, 10, 11},
            new List<int> {4, 5, 9, 12, 13},
            new List<int> {4, 5, 6, 8, 10, 12, 13, 14},
            new List<int> {5, 6, 7, 9, 11, 13, 14, 15},
            new List<int> {6, 7, 10, 14, 15},
            new List<int> {8, 9, 13},
            new List<int> {8, 9, 10, 12, 14},
            new List<int> {9, 10, 11, 13, 15},
            new List<int> {10, 11, 14}
        };

        /// <summary>
        ///     Częstość występowania danego znaku na planszy.
        /// </summary>
        private readonly Dictionary<char, int> _charFrequency = new Dictionary<char, int>();

        /// <summary>
        ///     Lista znaków dostępnych na planszy. Długość tego ciągu wynosi <see cref="CharsCount" />.
        ///     Ich kolejność na planszy, w zależności od indeksu w słowie, jest następująca:
        ///     0123
        ///     4567
        ///     89ab
        ///     cdef
        /// </summary>
        private readonly string _chars;

        private readonly List<Solution> _solutions = new List<Solution>();
        private List<int> _solutionInvertedMoves;

        /// <summary>
        ///     Czy dane pole planszy jest zajęte (litera w nim się znajdująca została już użyta podczas
        ///     pisania danego słowa).
        /// </summary>
        private bool[] _used = new bool[CharsCount];

        /// <summary>
        ///     Tworzy instancję planszy gry Słowotok.
        /// </summary>
        /// <param name="chars">lista znaków dostępnych na planszy podana wierszami od lewej do prawej</param>
        public Board(string chars)
        {
            if (LettersOnBoardValidationStatus.Ok != ValidateBoardLetters(chars))
            {
                throw new Exception("Niepoprawny zestaw znaków na planszy");
            }
            _chars = chars;
            Initialize();
        }

        /// <summary>
        /// Lista rozwiązań.
        /// </summary>
        public List<Solution> Solutions
        {
            get { return _solutions; }
        }

        /// <summary>
        ///     Sprawdz czy przekazany zestaw liter planszy jest dopuszczalny.
        /// </summary>
        /// <param name="chars"></param>
        /// <returns></returns>
        public static LettersOnBoardValidationStatus ValidateBoardLetters(string chars)
        {
            if (chars == null || chars.Length != CharsCount || !chars.All(c => AcceptableCharacters.Contains(c)))
            {
                return LettersOnBoardValidationStatus.Incorrect;
            }
            return LettersOnBoardValidationStatus.Ok;
        }

        /// <summary>
        ///     Zwraca listę sąsiednich pól dla danego indeksu pola planszy.
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        private static List<int> GetNeighbours(int index)
        {
            return Neighbours[index];
        }

        /// <summary>
        ///     Sprawdza, czy z liter dostępnych na planszy możliwe jest ułożenie podanego słowa.
        ///     Brane są tutaj pod uwagę jedynie zestaw dostępnych liter i częstość ich występowania.
        /// </summary>
        /// <param name="word"></param>
        /// <returns></returns>
        public bool IsPossibleToCombineWord(Word word)
        {
            return word.UniqueChars().All(c => word.CharFrequency(c) <= CharFrequency(c));
        }

        /// <summary>
        ///     Sprawdza, czy dane słowo może być napisane na planszy, przeciągając po niej palcem.
        ///     Metoda rekurencyjna.
        /// </summary>
        /// <param name="w">słowo do napisania na planszy</param>
        /// <param name="charIndex">indeks litery w słowie, którą chcemy zaznaczyć w aktualnym kroku</param>
        /// <param name="boardIndex">numer pola plaszy, które został zazanczone w ostatnim kroku</param>
        /// <returns>czy możliwe jest napisanie słowa na planszy</returns>
        public bool IsPossibleToSwipeWord(Word w, int charIndex = 0, int boardIndex = 0)
        {
            if (charIndex >= w.Value.Length)
            {
                // całe słowo zostało napisane na planszy
                _solutionInvertedMoves = new List<int>();
                return true;
            }

            char currentChar = w.Value[charIndex];
            List<int> indexesToCheck;
            if (charIndex == 0)
            {
                // rozpoczynanie pisania słowa na planszy, pobieranie indeksów pól planszy, zawierających
                // literę, na jaką rozpoczyna się słowo
                indexesToCheck = GetBoardIndexesWithChar(currentChar).ToList();
                _used = new bool[CharsCount];
            }
            else
            {
                // w trakcie pisania słowa pobierane są sąsiednie pola do ostatnio wykorzystanego
                indexesToCheck = GetNeighbours(boardIndex);
            }

            // dla każdego pola, na które można się poruszyć, sprawdzane jest czy można z jego wykorzystaniem
            // kontynuować pisanie słowa
            bool result = indexesToCheck.Any(i =>
            {
                if (_chars[i] == currentChar && !_used[i])
                {
                    // dane pole planszy jest oznaczone jako użyte
                    _used[i] = true;
                    // sprawdzanie czy można dopisać pozostałą część słowa z użyciem niewykorzystanych pól planszy
                    bool innerResult = IsPossibleToSwipeWord(w, charIndex + 1, i);
                    // pole planszy jest odznaczane (może być znów wykorzystane do przesunięcia nad nie palca)
                    _used[i] = false;
                    if (innerResult)
                    {
                        // jeżeli udało się napisa słowo zwracana jest taka informacja
                        _solutionInvertedMoves.Add(i);
                        return true;
                    }
                }
                // nie udało się dokończyć pisania słowa
                return false;
            });

            if (charIndex == 0 && result)
            {
                _solutionInvertedMoves.Reverse();
                _solutions.Add(new Solution {Word = w, Moves = _solutionInvertedMoves});
            }

            return result;
        }

        /// <summary>
        ///     Zadania inicjujące stan obiektu.
        /// </summary>
        private void Initialize()
        {
            // Sprawdza jak często dana litera występuje na planszy. Ponieważ ta operacja jest wykonywana
            // dość często w trakcie działania programu warto wyznaczyć jej rezultaty tylko raz a później
            // odnosić się do już przygotowanych wartości.
            AcceptableCharacters.ForEach(c => _charFrequency.Add(c, _chars.Count(s => s == c)));
        }

        /// <summary>
        ///     Określa ile razy podany znak pojawia się na planszy.
        /// </summary>
        /// <param name="c">znak, którego częstość na planszy ma być określona</param>
        /// <returns>częstość wystąpienia podanego znaku na planszy</returns>
        private int CharFrequency(char c)
        {
            return _charFrequency.ContainsKey(c) ? _charFrequency[c] : 0;
        }

        /// <summary>
        ///     Zwraca listę indeksów pól na planszy, które zawierają dany znak.
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        private IEnumerable<int> GetBoardIndexesWithChar(char c)
        {
            for (int i = 0; i < _chars.Length; i++)
            {
                if (_chars[i] == c)
                {
                    yield return i;
                }
            }
        }
    }
}