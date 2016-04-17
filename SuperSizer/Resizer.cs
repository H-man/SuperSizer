using ImageProcessor;
using ImageProcessor.Imaging;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;

namespace SuperSizer
{
    /// <summary>
    /// Image Resizer
    /// </summary>
    public class Resizer
    {
        /// <summary>
        /// Resize images
        /// </summary>
        /// <param name="pathToOriginal">original source file</param>
        /// <param name="sizes">target sizes</param>
        /// <param name="overrideExisting">override existing files</param>
        public static void ResizeImages(string pathToOriginal, IReadOnlyList<FileOutputInfo> sizes, bool overrideExisting)
        {
            if (!File.Exists(pathToOriginal))
            {
                throw new ArgumentException($"File not found {pathToOriginal}");
            }

            using (var imgFac = new ImageFactory())
            {
                foreach (var size in sizes)
                {
                    var outDir = Path.Combine(Environment.CurrentDirectory, size.Group);
                    if (!Directory.Exists(outDir))
                    {
                        Directory.CreateDirectory(outDir);
                    }

                    var filename = Path.GetFileNameWithoutExtension(pathToOriginal);
                    filename += $"_{size.Size.Width}_{size.Size.Height}" + Path.GetExtension(pathToOriginal);
                    var outPath = Path.Combine(outDir, filename);
                    if (!overrideExisting && File.Exists(outPath)) return;

                    // crop images to maintain wanted aspect ratio (no stretching)
                    imgFac.Load(pathToOriginal)
                        .Resize(new ResizeLayer(new Size(size.Size.Width, size.Size.Height), ResizeMode.Crop))
                        .Save(outPath);
                };
            }
        }
    }
}