namespace BitzArt.CA.SampleApp;

public class TelemetryOptions
{
    public const string SectionName = "Telemetry";

    // General options

    public bool Enabled { get; set; }

    // Tracing options

    public bool LogRepositoryCommands { get; set; }

    // OTLP Exporter options

    public bool UseOtlpExporter { get; set; }

    public string? OtlpEndpoint { get; set; }

    // Console Exporter options

    public bool UseConsoleExporter { get; set; }
}
