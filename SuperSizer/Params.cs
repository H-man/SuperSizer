using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;

namespace SuperSizer
{
    public class FileOutputInfo
    {
        public string Group { get; set; }
        public Size Size { get; set; }

        public FileOutputInfo(string group, Size size)
        {
            Group = group;
            Size = size;
        }
    }
    public class Params
    {
        /// <summary>
        /// Load target groups and sizes from file
        /// </summary>
        /// <param name="file">file</param>
        /// <returns></returns>
        public static List<FileOutputInfo> LoadFromFile(string file)
        {
            if (!File.Exists(file))
            {
                throw new ArgumentException($"File {file} not found");
            }
            var ret = new List<FileOutputInfo>();

            var lines = File.ReadAllLines(file);
            foreach (var line in lines)
            {
                if (string.IsNullOrEmpty(line)) continue;
                if (line.Trim().StartsWith("#")) continue;
                var split = line.Trim().Split(new[] { ";" }, StringSplitOptions.RemoveEmptyEntries);
                ret.Add(new FileOutputInfo(split[0], new Size(int.Parse(split[1]), int.Parse(split[2]))));
            }
            return ret;
        }
    }
}