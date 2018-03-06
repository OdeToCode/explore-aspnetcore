using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Movies.Services;
using System;
using System.Threading.Tasks;

namespace Movies.Pages
{
    public class SecretsModel : PageModel
    {
        private readonly KeyVaultCrypto crypto;

        public string EncryptedText { get; set; }
        public string DecryptedText { get; set; }

        public SecretsModel(KeyVaultCrypto keyVaultCrypto)
        {
            this.crypto = keyVaultCrypto;
        }
       
        public async Task OnGet(string encryptedText)
        {
            EncryptedText = encryptedText;
            if (!string.IsNullOrEmpty(encryptedText))
            {
                try
                {
                    DecryptedText = await crypto.DecryptAsync(encryptedText);
                    //DecryptedText = await crypto.DecryptAsync(encryptedText);
                }
                catch(Exception ex)
                {
                    DecryptedText = ex.Message;
                }
            }
        }

        public async Task<IActionResult> OnPostAsync(string word)
        {
            DecryptedText = word;
            if(word != null)
            {
                //EncryptedText = await crypto.EncryptAsync(word);
                EncryptedText = await crypto.WrapKeyAsync(word);
            }

            return RedirectToPage(new { EncryptedText });
        }
    }
}