using Windows.ApplicationModel.Resources;
using YSP.Enums;

namespace OverToolkit.Helpers
{
    /// <summary>
    /// Класс-помощник для работы с локализацией.
    /// </summary>
    public static class LocalizationHelper
    {
        #region Private field
        private static readonly ResourceLoader resourceLoader = ResourceLoader.GetForViewIndependentUse("Resources");
        #endregion

        #region Public methods
        /// <summary>
        /// Получает строку из ресурсов *.resw.
        /// </summary>
        /// <param name="resourceName">Название элемента *.resw.</param>
        /// <returns>Строка элемента *.resw.</returns>
        public static string ToString(string resourceName)
        {
            return resourceLoader.GetString(resourceName);
        }

        /// <summary>
        /// Получает преобразованную в требуемый регистр строку из ресурсов *.resw.
        /// </summary>
        /// <param name="resourceName">Название элемента *.resw.</param>
        /// <param name="textTransform">Требуемый регистр.</param>
        /// <returns>Строка элемента *.resw.</returns>
        public static string ToString(string resourceName, TextTransform textTransform)
        {
#if WINDOWS_APP
            return resourceLoader.GetString(resourceName);
#else
            switch (textTransform)
            {
                case TextTransform.Uppercase: return resourceLoader.GetString(resourceName).ToUpper();
                case TextTransform.Lowercase: return resourceLoader.GetString(resourceName).ToLower();
                default: return resourceLoader.GetString(resourceName);
            }
#endif
        }
#endregion
    }
}
