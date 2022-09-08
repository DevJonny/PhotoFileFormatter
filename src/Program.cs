using CommandLine;
using Mehroz;
using PhotoFileFormatter;
using TagLib;
using TagLib.Image;
using File = System.IO.File;

Parser.Default.ParseArguments<Options>(args)
    .WithParsed(o =>
    {
        var files = Directory.GetFiles(o.Path);

        foreach (var file in files)
        {
            try
            {
                var taggedFile = TagLib.File.Create(file);
                var tag = taggedFile.Tag as CombinedImageTag;
                var fileName = taggedFile.Name;

                if (tag is null)
                {
                    Console.WriteLine($"Skipping {fileName} as it's not a CombinedImageTag");
                    continue;
                }

                var newFilename = Path.GetFileNameWithoutExtension(fileName);

                var fStop = $"-f{tag.FNumber}";
                
                if (!newFilename.Contains(fStop) && (o.IncludeFStop || o.IncludeAll))
                    newFilename += fStop;

                var exposure = $"-{new Fraction(tag.ExposureTime ?? 0).Denominator}";
                
                if (!newFilename.Contains(exposure) && (o.IncludeExposure || o.IncludeAll))
                    newFilename += exposure;

                var iso = $"-ISO{tag.ISOSpeedRatings}";

                if (!newFilename.Contains(iso) && (o.IncludeIso || o.IncludeAll))
                    newFilename += iso;

                var focalLength = $"{tag.FocalLength}mm";
                
                if (!newFilename.Contains(focalLength) && (o.IncludeFocalLength || o.IncludeAll))
                    newFilename += focalLength;

                newFilename += Path.GetExtension(fileName);
                
                newFilename = $"{Path.GetDirectoryName(file)}/{newFilename}";

                if (o.RemoveTopaz)
                    newFilename = newFilename
                        .Replace("-Edit", string.Empty)
                        .Replace("-Sharpen", string.Empty);
                
                Console.WriteLine($"Old filename: {fileName}");
                Console.WriteLine($"New filename: {newFilename}");

                if (!o.WhatIf)
                    File.Move(fileName, newFilename);
            }
            catch (UnsupportedFormatException)
            {
                Console.WriteLine($"Skipping {Path.GetFileName(file)}");
            }
        }
    });