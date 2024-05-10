using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using SH3Textractor;
using SilentHunter.FileFormats.Dat;
using SilentHunter.FileFormats.DependencyInjection;

namespace sh3xtractjson;

public class Runner {
    public static void Run(string filePath, RichTextBoxWriter successWriter, RichTextBoxWriter errorWriter) {
        {
            IServiceCollection svcCollection = new ServiceCollection();
            svcCollection.AddSilentHunterParsers(_ => { });
            ServiceProvider svcProvider = svcCollection.BuildServiceProvider();

            var fileList = Files.RecursiveFileSearch(filePath);
            fileList = fileList.Where(s =>
                                          s.EndsWith(".dat")
                                          || s.EndsWith(".sim")
                                          || s.EndsWith(".zon")
                                          || s.EndsWith(".val")
                                          || s.EndsWith(".cam")
                                          || s.EndsWith(".dsd")
                                          || s.EndsWith(".anm")
                                          || s.EndsWith(".sdl")
                                          || s.EndsWith(".off"))
                               .ToList();

            var serializeSettings = new JsonSerializerSettings {
                ReferenceLoopHandling      = ReferenceLoopHandling.Ignore,
                PreserveReferencesHandling = PreserveReferencesHandling.Objects,
                ContractResolver           = Serializer.ShouldSerializeContractResolver.Instance,
                Formatting                 = Formatting.Indented
            };

            void Doer(string file, JsonSerializer serializer) {
                DatFile datFile = svcProvider.GetRequiredService<DatFile>();
                
                try {
                    successWriter.WriteLine("Loading: " + file);
                    datFile.LoadAsync(file).Wait();
                }
                catch (Exception e) {
                    errorWriter.WriteLine("Error loading: " + file + " - " + e.Message);
                    return;
                }

                string newFile = file + ".sh3xtractor.json";
                using (StreamWriter sw = new StreamWriter(newFile)) {
                    serializer.Serialize(sw, datFile);
                    Console.WriteLine("Saved to: " + newFile);
                }

                datFile.Dispose();
                successWriter.WriteLine("Done: " + file);
            }

            var options = new ParallelOptions {
                MaxDegreeOfParallelism = Environment.ProcessorCount == 1 ? 1 : Environment.ProcessorCount - 1
            };

            var localSerializer =
                new ThreadLocal<JsonSerializer>(() => JsonSerializer.Create(serializeSettings));

            Parallel.ForEach(fileList, options, (string f) => { Doer(f, localSerializer.Value); });
        }
    }
}