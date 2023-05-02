using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Exporters.Csv;
using BenchmarkDotNet.Exporters;

namespace BenchmarkEF.Tests
{
    
    public class BenchConfig : ManualConfig
    {
        public BenchConfig()
        {
            AddExporter(CsvMeasurementsExporter.Default);
            AddExporter(RPlotExporter.Default);
        }
    }
}