namespace Slowotok
{
    /// <summary>
    /// Status walidacji znaków dostępnych na planszy gry.
    /// </summary>
    public enum LettersOnBoardValidationStatus
    {
        /// <summary>
        /// Dostępne znaki spełniają założenia.
        /// </summary>
        Ok,

        /// <summary>
        /// Występują niedozwolone znaki lub ich liczba jest niepoprawna.
        /// </summary>
        Incorrect
    }
}
