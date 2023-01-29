using ProjetoLocadoraDeVeiculos.Models;

namespace ProjetoLocadoraDeVeiculos.Repositorios
{
    public interface IUsuarioRepositorio
    {
        Usuario BuscarPorEmail(string email);
        Usuario  BuscarPorEmailECpf(string email, string cpf);
        List<Usuario> BuscarTodos();
        Usuario BuscarPorID(int id);
    }
}
