using Spotify.Core.Exceptions;
using Spotify.Domain.Accounts.Exceptions;

namespace Spotify.Domain.Accounts.ValueObjects;

public class CPF
{
    private readonly CPFException Validation = new CPFException();
    public CPF() { }

    public CPF(string numero)
    {
        Numero = numero;

        if (IsValido() == false)
        {
            Validation.AddError(new BusinessValidation()
            {
                ErrorMessage = "CPF Inválido",
                ErrorName = nameof(CPFException)
            });

            Validation.ValidateAndThrow();
        }
    }

    public string Numero { get; set; }

    public string NumeroFormatado()
    {
        return Convert.ToInt64(Numero).ToString("###.###.###-##");
    }

    public bool IsValido()
    {
        int[] multiplicador1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
        int[] multiplicador2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };
        string tempCpf;
        string digito;
        int soma;
        int resto;
        var cpf = Numero.Trim().Replace(".", "").Replace("-", "");

        if (cpf.Length != 11)
            return false;

        tempCpf = cpf.Substring(0, 9);
        soma = 0;

        for (int i = 0; i < 9; i++)
            soma += int.Parse(tempCpf[i].ToString()) * multiplicador1[i];
        resto = soma % 11;

        if (resto < 2)
            resto = 0;
        else
            resto = 11 - resto;

        digito = resto.ToString();
        tempCpf = tempCpf + digito;
        soma = 0;

        for (int i = 0; i < 10; i++)
            soma += int.Parse(tempCpf[i].ToString()) * multiplicador2[i];

        resto = soma % 11;

        if (resto < 2)
            resto = 0;
        else
            resto = 11 - resto;

        digito = digito + resto.ToString();

        return cpf.EndsWith(digito);
    }
}