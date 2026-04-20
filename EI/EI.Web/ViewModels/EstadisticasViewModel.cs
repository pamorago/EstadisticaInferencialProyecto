using EI.Web.Models;

namespace EI.Web.ViewModels;

public class EstadisticasViewModel
{
    // ── Parámetros de entrada ────────────────────────────────────────────
    public double NivelConfianza { get; set; } = 0.98;
    public double MediaPoblacionalImc { get; set; } = 27.0;
    public double NivelSignificancia { get; set; } = 0.05;

    // ── a) Promedios de pesos ────────────────────────────────────────────
    public double PromedioGeneralPeso { get; set; }
    public double PromedioHombresPeso { get; set; }
    public double PromedioMujeresPeso { get; set; }
    public int NHombres { get; set; }
    public int NMujeres { get; set; }
    public int NTotal { get; set; }

    // ── b) IC para diferencia de medias (Welch) ─────────────────────────
    public double DiferenciaMedias { get; set; }
    public double S1Hombres { get; set; }
    public double S2Mujeres { get; set; }
    public double ErrorEstandar { get; set; }
    public double GradosLibertad { get; set; }
    public double TCritico { get; set; }
    public double ICLimiteInferior { get; set; }
    public double ICLimiteSuperior { get; set; }

    // ── c) Prueba de hipótesis IMC (cola inferior H₁: μ < μ₀) ──────────
    public double PromedioImcMuestra { get; set; }
    public double DesviacionImcMuestra { get; set; }
    public double TEstadistico { get; set; }
    public double TCriticoHipotesis { get; set; }
    public double ValorP { get; set; }
    public bool RechazaH0 { get; set; }

    // ── Datos para tabla ─────────────────────────────────────────────────
    public ICollection<Paciente> Pacientes { get; set; } = new List<Paciente>();
}
