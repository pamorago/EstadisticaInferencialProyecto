using EI.Web.Data;
using EI.Web.Models;
using EI.Web.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EI.Web.Repository.Implementations;

public class RepositoryPaciente : IRepositoryPaciente
{
    private readonly EIContext _context;

    public RepositoryPaciente(EIContext context)
    {
        _context = context;
    }

    public async Task<ICollection<Paciente>> ListAsync()
    {
        return await _context.Pacientes
                             .OrderBy(p => p.Id)
                             .AsNoTracking()
                             .ToListAsync();
    }

    public async Task<ICollection<Paciente>> ListByGeneroAsync(string genero)
    {
        return await _context.Pacientes
                             .Where(p => p.Genero == genero)
                             .OrderBy(p => p.Id)
                             .AsNoTracking()
                             .ToListAsync();
    }
}
