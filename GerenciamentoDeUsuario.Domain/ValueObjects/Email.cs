
using System.Net.Mail;

namespace GerenciamentoDeUsuario.Domain.ValueObjects;

public record Email
{
    public string Endereco { get; private set; }
    public Email(string endereco)
    {
        if (string.IsNullOrWhiteSpace(endereco))
            throw new ArgumentException("Email é obrigatório.");
        if (!IsValidEmail(endereco))
            throw new ArgumentException("Email inválido.");
        Endereco = endereco;
    }
    private bool IsValidEmail(string email)
    {
        try
        {
            var addr = new MailAddress(email);
            return addr.Address == email;
        }
        catch
        {
            return false;
        }
    }
}
