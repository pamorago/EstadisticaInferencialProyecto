namespace EI.Web.Models;

public partial class Paciente
{
    public int Id { get; set; }
    public string Nombre { get; set; } = null!;
    public int? Edad { get; set; }
    public decimal? PesoKg { get; set; }
    public decimal? EstaturaM { get; set; }
    public decimal? Imc { get; set; }
    public int? TglMgDl { get; set; }
    public int? PresionSistolica { get; set; }
    public int? PresionDiastolica { get; set; }
    public string? Padecimientos { get; set; }
    public DateOnly? Fecha { get; set; }
    public string? Genero { get; set; }  // 'M' = Hombre, 'F' = Mujer
}
