using System.Security.Cryptography;

namespace Cedeira.Essentials.NET.System.Security.Cryptography.HashService.Interface
{
    public interface IHashCedeira
    {
        //Genera el hash de una cadena de entrada y devuelve el resultado como una cadena.
        string CaculateHash(string input);
        //Genera el hash de una cadena de entrada y completa un stream 
        void CalculateHash(string input, Stream output);
        //Valida si el hash generado
        bool HashValidate(string input, string hash);   
    }
}
