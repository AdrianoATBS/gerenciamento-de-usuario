
using System.Net.Mail;

namespace GerenciamentoDeUsuario.Domain.ValueObjects;

public class Email
{
    public string Endereco { get; private set; }
    public Email(string endereco)
    {
        if (string.IsNullOrWhiteSpace(endereco))
            throw new ArgumentException("Email é obrigatório.", nameof(endereco));
        if (!IsValidEmail(endereco))
            throw new ArgumentException("Email inválido.", nameof(endereco));
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
