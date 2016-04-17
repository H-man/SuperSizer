using System;
using System.Diagnostics;
using System.Linq;

namespace SuperSizer
{
    class Program
    {
        static void Main(string[] args)
        {

            if (!args.Any())
            {
                Console.WriteLine("Usage: Supersizer [sourceImage] [overrideExisting (true/false)] [group (optional)]");
                Console.WriteLine(" Example: Supersizer appIcon.png true generic");
                return;
            }
            var overrideExisting = true;

            if (args.Count() > 1 && args[1].ToLower() == "false")
            {
                overrideExisting = false;
            }


            var fileName = args[0];
            var sizes = Params.LoadFromFile("output.txt");

            if (args.Count() > 2)
            {
                sizes = sizes.Where(x => x.Group.ToLower() == args[2].ToLower().Trim()).ToList();
            }

            Console.WriteLine($"Starting to generate {sizes.Count} images from {args[0]}. Override existing images: {overrideExisting}");
            var sw = new Stopwatch();
            sw.Start();
            Resizer.ResizeImages(fileName, sizes, overrideExisting);
            sw.Stop();
            Console.WriteLine($"All done in {sw.ElapsedMilliseconds} milliseconds");
        }
    }
}