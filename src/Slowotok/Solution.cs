using System.Collections.Generic;

namespace Slowotok
{
    /// <summary>
    /// Rozwiązanie - słowo, które da się ułożyć suwając palcem po planszy.
    /// </summary>
    public class Solution
    {
        /// <summary>
        /// Słowo będące rozwiązaniem.
        /// </summary>
        public Word Word { get; set; }

        /// <summary>
        /// Lista pól, nad którymi trzeba przesunąć palcem, aby napisać rozwiązaanie.
        /// </summary>
        public List<int> Moves { get; set; }
    }
}
