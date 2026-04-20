using EI.Web.Repository.Interfaces;
using EI.Web.Services.Interfaces;
using EI.Web.ViewModels;

namespace EI.Web.Services.Implementations;

public class ServiceEstadisticas : IServiceEstadisticas
{
    private readonly IRepositoryPaciente _repository;

    public ServiceEstadisticas(IRepositoryPaciente repository)
    {
        _repository = repository;
    }

    public async Task<EstadisticasViewModel> CalcularAsync(
        double nivelConfianza = 0.98,
        double mediaPoblacionalImc = 27.0,
        double nivelSignificancia = 0.05)
    {
        var todos = await _repository.ListAsync();
        var hombres = await _repository.ListByGeneroAsync("M");
        var mujeres = await _repository.ListByGeneroAsync("F");

        // ── a) Promedios de pesos ────────────────────────────────────────
        double[] pesosH = hombres.Select(p => (double)(p.PesoKg ?? 0)).ToArray();
        double[] pesosM = mujeres.Select(p => (double)(p.PesoKg ?? 0)).ToArray();
        double[] pesosAll = todos.Select(p => (double)(p.PesoKg ?? 0)).ToArray();

        double mediaH = Media(pesosH);
        double mediaM = Media(pesosM);
        double mediaG = Media(pesosAll);

        // ── b) IC para diferencia de medias (Welch t-interval) ──────────
        int n1 = pesosH.Length, n2 = pesosM.Length;
        double s1 = DesvStd(pesosH), s2 = DesvStd(pesosM);

        double v1 = s1 * s1 / n1, v2 = s2 * s2 / n2;
        double se = Math.Sqrt(v1 + v2);

        // Welch–Satterthwaite grados de libertad
        double df = Math.Pow(v1 + v2, 2)
                  / (v1 * v1 / (n1 - 1) + v2 * v2 / (n2 - 1));

        double alpha = 1.0 - nivelConfianza;
        double tCrit = TDistInvCDF(1.0 - alpha / 2.0, df);

        double diffMeans = mediaH - mediaM;
        double lower = diffMeans - tCrit * se;
        double upper = diffMeans + tCrit * se;

        // ── c) Prueba de hipótesis IMC (una cola inferior H₁: μ < μ₀) ──
        double[] imcVals = todos.Select(p => (double)(p.Imc ?? 0)).ToArray();
        double imcMedia = Media(imcVals);
        double imcStd = DesvStd(imcVals);
        int n = imcVals.Length;

        double tStat = (imcMedia - mediaPoblacionalImc) / (imcStd / Math.Sqrt(n));
        // Cola inferior: P(T < t_obs)
        double pValue = TDistCDF(tStat, n - 1);
        bool reject = pValue < nivelSignificancia;

        return new EstadisticasViewModel
        {
            NivelConfianza = nivelConfianza,
            MediaPoblacionalImc = mediaPoblacionalImc,
            NivelSignificancia = nivelSignificancia,

            PromedioGeneralPeso = Math.Round(mediaG, 4),
            PromedioHombresPeso = Math.Round(mediaH, 4),
            PromedioMujeresPeso = Math.Round(mediaM, 4),
            NHombres = n1,
            NMujeres = n2,
            NTotal = n,

            DiferenciaMedias = Math.Round(diffMeans, 4),
            S1Hombres = Math.Round(s1, 4),
            S2Mujeres = Math.Round(s2, 4),
            ErrorEstandar = Math.Round(se, 4),
            GradosLibertad = Math.Round(df, 4),
            TCritico = Math.Round(tCrit, 4),
            ICLimiteInferior = Math.Round(lower, 4),
            ICLimiteSuperior = Math.Round(upper, 4),

            PromedioImcMuestra = Math.Round(imcMedia, 4),
            DesviacionImcMuestra = Math.Round(imcStd, 4),
            TEstadistico = Math.Round(tStat, 4),
            // Valor critico cola inferior: t tal que P(T < t) = alpha (valor negativo)
            TCriticoHipotesis = Math.Round(TDistInvCDF(nivelSignificancia, n - 1), 4),
            ValorP = Math.Round(pValue, 6),
            RechazaH0 = reject,

            Pacientes = todos,
        };
    }

    // ── Estadísticas básicas ─────────────────────────────────────────────
    private static double Media(double[] v) => v.Average();

    private static double DesvStd(double[] v)
    {
        double m = v.Average();
        return Math.Sqrt(v.Sum(x => Math.Pow(x - m, 2)) / (v.Length - 1));
    }

    // ── Distribución t de Student ────────────────────────────────────────
    private static double LnGamma(double x)
    {
        double[] g = { 76.18009172947146, -86.50532032941677, 24.01409824083091,
                       -1.231739572450155, 1.208650973866179e-3, -5.395239384953e-6 };
        double y = x, tmp = x + 5.5;
        tmp -= (x + 0.5) * Math.Log(tmp);
        double ser = 1.000000000190015;
        foreach (var c in g) ser += c / ++y;
        return -tmp + Math.Log(2.5066282746310005 * ser / x);
    }

    private static double BetaCF(double x, double a, double b)
    {
        const int maxIter = 200;
        const double eps = 3e-10, fpMin = 1e-30;
        double qab = a + b, qap = a + 1, qam = a - 1;
        double c = 1, d = 1 - qab * x / qap;
        if (Math.Abs(d) < fpMin) d = fpMin;
        d = 1 / d;
        double h = d;
        for (int m = 1; m <= maxIter; m++)
        {
            int m2 = 2 * m;
            double aa = m * (b - m) * x / ((qam + m2) * (a + m2));
            d = 1 + aa * d; if (Math.Abs(d) < fpMin) d = fpMin;
            c = 1 + aa / c; if (Math.Abs(c) < fpMin) c = fpMin;
            d = 1 / d; h *= d * c;
            aa = -(a + m) * (qab + m) * x / ((a + m2) * (qap + m2));
            d = 1 + aa * d; if (Math.Abs(d) < fpMin) d = fpMin;
            c = 1 + aa / c; if (Math.Abs(c) < fpMin) c = fpMin;
            d = 1 / d;
            double del = d * c; h *= del;
            if (Math.Abs(del - 1) < eps) break;
        }
        return h;
    }

    private static double IncompleteBeta(double x, double a, double b)
    {
        if (x <= 0) return 0; if (x >= 1) return 1;
        double lnB = LnGamma(a) + LnGamma(b) - LnGamma(a + b);
        double bt = Math.Exp(-lnB + a * Math.Log(x) + b * Math.Log(1 - x));
        return x < (a + 1) / (a + b + 2)
            ? bt * BetaCF(x, a, b) / a
            : 1 - bt * BetaCF(1 - x, b, a) / b;
    }

    private static double TDistCDF(double t, double nu)
    {
        double x = nu / (nu + t * t);
        double ib = IncompleteBeta(x, nu / 2, 0.5);
        return t >= 0 ? 1 - 0.5 * ib : 0.5 * ib;
    }

    private static double TDistPDF(double t, double nu)
        => Math.Exp(LnGamma((nu + 1) / 2) - LnGamma(nu / 2)
           - 0.5 * Math.Log(nu * Math.PI)
           - (nu + 1) / 2 * Math.Log(1 + t * t / nu));

    private static double NormInvCDF(double p)
    {
        const double a1 = -3.969683028665376e+01, a2 = 2.209460984245205e+02,
                     a3 = -2.759285104469687e+02, a4 = 1.383577518672690e+02,
                     a5 = -3.066479806614716e+01, a6 = 2.506628277459239e+00;
        const double b1 = -5.447609879822406e+01, b2 = 1.615858368580409e+02,
                     b3 = -1.556989798598866e+02, b4 = 6.680131188771972e+01,
                     b5 = -1.328068155288572e+01;
        const double c1 = -7.784894002430293e-03, c2 = -3.223964580411365e-01,
                     c3 = -2.400758277161838e+00, c4 = -2.549732539343734e+00,
                     c5 = 4.374664141464968e+00, c6 = 2.938163982698783e+00;
        const double d1 = 7.784695709041462e-03, d2 = 3.224671290700398e-01,
                     d3 = 2.445134137142996e+00, d4 = 3.754408661907416e+00;
        const double pLow = 0.02425, pHigh = 1 - pLow;
        if (p < pLow)
        {
            double q = Math.Sqrt(-2 * Math.Log(p));
            return (((((c1 * q + c2) * q + c3) * q + c4) * q + c5) * q + c6) / ((((d1 * q + d2) * q + d3) * q + d4) * q + 1);
        }
        if (p <= pHigh)
        {
            double q = p - 0.5, r = q * q;
            return (((((a1 * r + a2) * r + a3) * r + a4) * r + a5) * r + a6) * q / (((((b1 * r + b2) * r + b3) * r + b4) * r + b5) * r + 1);
        }
        else
        {
            double q = Math.Sqrt(-2 * Math.Log(1 - p));
            return -(((((c1 * q + c2) * q + c3) * q + c4) * q + c5) * q + c6) / ((((d1 * q + d2) * q + d3) * q + d4) * q + 1);
        }
    }

    public static double TDistInvCDF(double p, double nu)
    {
        double t = NormInvCDF(p);
        for (int i = 0; i < 100; i++)
        {
            double cdf = TDistCDF(t, nu);
            double pdf = TDistPDF(t, nu);
            if (pdf < 1e-20) break;
            double delta = (cdf - p) / pdf;
            t -= delta;
            if (Math.Abs(delta) < 1e-12) break;
        }
        return t;
    }
}
