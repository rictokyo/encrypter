using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Encrypter
{
    internal class SampleData : MainViewModel
    {
        public SampleData()
        {
            TextToEncrypt = "abc";
        }
    }
    public abstract class ViewModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    internal class MainViewModel : ViewModelBase
    {
        private string encryptedText;
        private string textToDecrypt;

        public MainViewModel()
        {

        }

        public string TextToEncrypt
        {
            get { return textToDecrypt; }
            set
            {
                try
                {
                    textToDecrypt = value;
                    OnPropertyChanged();

                    if (textToDecrypt.StartsWith(":"))
                    {
                        var textToDecrypt1 = textToDecrypt.Split(':')[1];
                        if (!string.IsNullOrEmpty(textToDecrypt1))
                        { EncryptedText = Encryption.AESThenHMAC.SimpleDecryptWithPassword(textToDecrypt1, "passwordMustBe12"); }
                        else
                        {
                            EncryptedText = string.Empty;
                        }
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(textToDecrypt))
                        { EncryptedText = Encryption.AESThenHMAC.SimpleEncryptWithPassword(textToDecrypt, "passwordMustBe12"); }
                        else
                        {
                            EncryptedText = string.Empty;
                        }
                    }
                }
                catch (Exception exc)
                {
                    EncryptedText = string.Empty;
                    Debug.WriteLine(exc.Message);
                }
            }
        }
        public string EncryptedText { get { return encryptedText; } set { encryptedText = value; OnPropertyChanged(); } }
    }
}

