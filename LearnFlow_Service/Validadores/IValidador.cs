namespace LearnFlow_Service.Validadores
{
    public interface IValidador
    {
        bool ValidarSenha(string senha);
        bool ValidarSenhaConfirmacao(string senha, string senhaConfirmacao);
    }
}