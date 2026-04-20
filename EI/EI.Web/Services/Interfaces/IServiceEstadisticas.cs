using EI.Web.ViewModels;

namespace EI.Web.Services.Interfaces;

public interface IServiceEstadisticas
{
    Task<EstadisticasViewModel> CalcularAsync(
        double nivelConfianza = 0.98,
        double mediaPoblacionalImc = 27.0,
        double nivelSignificancia = 0.05);
}
