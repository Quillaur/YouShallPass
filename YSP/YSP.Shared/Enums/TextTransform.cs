namespace YSP.Enums
{
    /// <summary>
    /// Возможные регистры строк.
    /// </summary>
    public enum TextTransform
    {
        /// <summary>
        /// Исходный.
        /// </summary>
        None,
        /// <summary>
        /// Все буквы в верхнем регистре.
        /// </summary>
        Uppercase,
        /// <summary>
        /// Все буквы в нижнем регистре.
        /// </summary>
        Lowercase,
        /// <summary>
        /// Каждое слово с заглавной буквы.
        /// </summary>
        Capitalize
    }
}
