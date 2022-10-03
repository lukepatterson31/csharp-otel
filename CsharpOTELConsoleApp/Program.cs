using System.Diagnostics;

using OpenTelemetry;
using OpenTelemetry.Exporter;
using OpenTelemetry.Trace;
using OpenTelemetry.Resources;

// Define some important constants to initialize tracing with
var serviceName = "MyCompany.MyProduct.MyService";
var serviceVersion = "1.0.0";

// Configure important OpenTelemetry settings and the console exporter
using var tracerProvider = Sdk.CreateTracerProviderBuilder()
    .AddSource(serviceName)
    .SetResourceBuilder(
        ResourceBuilder.CreateDefault()
            .AddService(serviceName: serviceName, serviceVersion: serviceVersion))
    .AddOtlpExporter(opt =>
    {
        opt.Endpoint = new Uri("http://127.0.0.1:4317");
        opt.Protocol = OtlpExportProtocol.Grpc;
    })
    .Build();

var myActivitySource = new ActivitySource(serviceName);

using var activity = myActivitySource.StartActivity("SayHelloC#");
activity?.SetTag("foo", 1);
activity?.SetTag("bar", "Hello, World!");
activity?.SetTag("baz", new int[] { 1, 2, 3 });