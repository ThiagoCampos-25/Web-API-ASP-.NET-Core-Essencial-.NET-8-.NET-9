namespace APICatalogo.Repositories
{
    public interface IUnitOfWorkk
    {
        IProdutoRepository ProdutoRepository { get; }

        ICategoriaRepository CategoriaRepository { get; }

        void Commit();
        
    }
}
