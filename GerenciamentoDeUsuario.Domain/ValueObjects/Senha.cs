using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GerenciamentoDeUsuario.Domain.ValueObjects;

public class Senha
{
    public string Hash { get; private set; }
    public Senha(string hash)
    {
        if (string.IsNullOrWhiteSpace(hash))
            throw new ArgumentException("Hash da senha é obrigatório.", nameof(hash));
        if(hash.Length < 5) 
            throw new ArgumentException("Hash da senha é inválido.", nameof(hash));
       
       Hash = hash;

    }

    public override bool Equals(object obj)
    {
        if (obj is Senha outraSenha)
        {
            return this.Hash == outraSenha.Hash; 
        }
        return false;
    }

    public override int GetHashCode()
    {
        return Hash.GetHashCode();
    }

    public static implicit operator Senha(string v)
    {
        return new Senha(v);
    }

}
