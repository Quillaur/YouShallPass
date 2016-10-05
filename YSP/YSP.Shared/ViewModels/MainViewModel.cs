using OverToolkit.Mvvm;

namespace YSP.ViewModels
{
    public class MainViewModel : BindableBase
    {
        #region Private constants
        private const string simplifiedAlphabet = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890__--" +
            "1234567890abcdefghijklmnopqrstuvwxyz__--ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        private const string extendedAlphabet = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890!@#$%^&*([{}])_+~-" +
            "1234567890abcdefghijklmnopqrstuvwxyz!@#$%^&*([{}])_+~-ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        #endregion

        #region Private fields
        private string websiteURL, masterPassword, generatedPassword;
        private bool useAdditionalCharacters, isClearEnabled;
        private double length;
        #endregion

        #region Constructor
        public MainViewModel()
        {
            ClearCommand = new DelegateCommand(() => WebsiteURL = MasterPassword = GeneratedPassword = string.Empty);

            UseAdditionalCharacters = true;
            Length = 15;
        }
        #endregion

        #region Commands
        public DelegateCommand ClearCommand { get; private set; }
        #endregion

        #region Properties
        public string WebsiteURL
        {
            get { return websiteURL; }
            set
            {
                Set(ref websiteURL, value);
                Generate();
            }
        }

        public string MasterPassword
        {
            get { return masterPassword; }
            set
            {
                Set(ref masterPassword, value);
                Generate();
            }
        }

        public string GeneratedPassword
        {
            get { return generatedPassword; }
            set { Set(ref generatedPassword, value); }
        }

        public bool UseAdditionalCharacters
        {
            get { return useAdditionalCharacters; }
            set
            {
                Set(ref useAdditionalCharacters, value);
                Generate();
            }
        }

        public bool IsClearEnabled
        {
            get { return isClearEnabled; }
            set { Set(ref isClearEnabled, value); }
        }

        public double Length
        {
            get { return length; }
            set
            {
                Set(ref length, value);
                Generate();
            }
        }
        #endregion

        #region Private methods
        private void Generate()
        {
            IsClearEnabled = !string.IsNullOrWhiteSpace(WebsiteURL) || !string.IsNullOrWhiteSpace(MasterPassword);

            if (string.IsNullOrEmpty(WebsiteURL) && string.IsNullOrEmpty(MasterPassword))
            {
                GeneratedPassword = string.Empty;
                return;
            }

            string simplifiedURL = WebsiteURL;

            if (WebsiteURL.IndexOf("www.") == 0)
                simplifiedURL = WebsiteURL.Substring(4);

            string salt = MasterPassword + simplifiedURL.ToLower() + MasterPassword;
            string alphabet = UseAdditionalCharacters ? extendedAlphabet : simplifiedAlphabet;

            int integer, j, i, v, p;
            char temp;

            for (i = alphabet.Length - 1, v = 0, p = 0; i > 0; i--, v++)
            {
                v %= salt.Length;
                p += integer = salt[v];
                j = (integer + v + p) % i;
                temp = alphabet[j];
                alphabet = alphabet.Substring(0, j) + alphabet[i] + alphabet.Substring(j + 1);
                alphabet = alphabet.Substring(0, i) + temp + alphabet.Substring(i + 1);
            }

            GeneratedPassword = alphabet.Substring(1, (int)Length);
        }
        #endregion
    }
}
