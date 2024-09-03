using System.Security.Cryptography;

namespace Cedeira.Essentials.NET.System.Security.Cryptography.HashService.Interface
{
    public interface IHashService
    {
        //Genera el hash de una cadena de entrada y devuelve el resultado como una cadena.
        string CreateHash(string input);
        //Genera el hash de una cadena de entrada
        void CreateHash(string input, Stream output);
        //Valida si el hash generado
        bool HashValidate(string input, string hash);   
    }
}
