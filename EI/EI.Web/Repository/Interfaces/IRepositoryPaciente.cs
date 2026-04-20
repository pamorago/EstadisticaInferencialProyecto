using EI.Web.Models;

namespace EI.Web.Repository.Interfaces;

public interface IRepositoryPaciente
{
    Task<ICollection<Paciente>> ListAsync();
    Task<ICollection<Paciente>> ListByGeneroAsync(string genero);
}
