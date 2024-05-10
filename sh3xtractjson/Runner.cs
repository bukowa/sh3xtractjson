using System.Collections.Concurrent;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using SH3Textractor;
using SilentHunter.FileFormats.Dat;
using SilentHunter.FileFormats.DependencyInjection;
using Particles;
using SilentHunter.FileFormats;
using SilentHunter.FileFormats.Off;
using SilentHunter.FileFormats.Sdl;

namespace sh3xtractjson;

public class Runner {
    public static void Run(IEnumerable<string> fileList, RichTextBoxWriter successWriter, RichTextBoxWriter errorWriter,
                           int                 threads,  ConcurrentQueue<object> que) {
        {
            IServiceCollection svcCollection = new ServiceCollection();
            svcCollection.AddSilentHunterParsers(c => {
                c.Controllers.FromAssembly(typeof(ParticleGenerator).Assembly);
            });
            ServiceProvider svcProvider = svcCollection.BuildServiceProvider();

            var serializeSettings = new JsonSerializerSettings {
                ReferenceLoopHandling      = ReferenceLoopHandling.Ignore,
                PreserveReferencesHandling = PreserveReferencesHandling.Objects,
                ContractResolver           = Serializer.ShouldSerializeContractResolver.Instance,
                Formatting                 = Formatting.Indented
            };

            void Doer(string file, JsonSerializer serializer) {
                ISilentHunterFile? shf = null;

                // switch based on extension
                switch (file.Split(".").Last()) {
                    case "dat":
                        shf = svcProvider.GetRequiredService<DatFile>();
                        break;
                    case "sim":
                        shf = svcProvider.GetRequiredService<DatFile>();
                        break;
                    case "zon":
                        shf = svcProvider.GetRequiredService<DatFile>();
                        break;
                    case "val":
                        shf = svcProvider.GetRequiredService<DatFile>();
                        break;
                    case "cam":
                        shf = svcProvider.GetRequiredService<DatFile>();
                        break;
                    case "dsd":
                        shf = svcProvider.GetRequiredService<DatFile>();
                        break;
                    case "anm":
                        shf = svcProvider.GetRequiredService<DatFile>();
                        break;
                    case "sdl":
                        shf = svcProvider.GetRequiredService<SdlFile>();
                        break;
                    case "off":
                        shf = svcProvider.GetRequiredService<OffFile>();
                        break;
                }

                if (shf == null) {
                    errorWriter.WriteLine("No parser found for: " + file);
                    return;
                }

                try {
                    successWriter.WriteLine("Loading: " + file);

                    Task.Run(async () => {
                        await using var fs =
                            new FileStream(file, FileMode.Open, FileAccess.Read, FileShare.Read, 4096, true);
                        await shf.LoadAsync(fs).ConfigureAwait(false);
                    }).Wait();
                }

                catch (Exception e) {
                    errorWriter.WriteLine("Error loading: " + file + " - " + e.Message);
                    return;
                }

                string newFile = file + ".sh3xtractor.json";
                using (var sw = new StreamWriter(newFile)) {
                    serializer.Serialize(sw, shf);
                    successWriter.WriteLine("Done: " + file);
                }

                if (shf is IDisposable disposable) {
                    disposable.Dispose();
                }
            }

            var options = new ParallelOptions {
                MaxDegreeOfParallelism = threads
            };

            var localSerializer =
                new ThreadLocal<JsonSerializer>(() => JsonSerializer.Create(serializeSettings));

            Parallel.ForEach(fileList, options, (string f) => {
                try {
                    Doer(f, localSerializer.Value);
                }
                finally {
                    que.Enqueue(true);
                }
            });
        }
    }
}