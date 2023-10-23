using Microsoft.EntityFrameworkCore;
using MinhaAPI.Models;

namespace MinhaAPI.Data;
public class LojaDbContext : DbContext
{
    
    public DbSet <Cliente> Cliente { get; set; }
    public DbSet<Categoria> Categoria { get; set; }
    public DbSet<Funcionario> Funcionario { get; set; }
    public DbSet<Setor> Setor { get; set; }
    public DbSet<Carrinho> Carrinhos { get; set; }
    public DbSet<Estoque> Estoque { get; set; }
    public DbSet<Produto> Produtos { get; set; }
    public DbSet<Venda> Venda { get; set; }
    
    

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("DataSource=loja.db;Cache=Shared");
    }

}