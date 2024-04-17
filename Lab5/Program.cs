using System;
using System.Collections.Generic;

public abstract class Disk
{
    public string Name { get; set; }
    public double Size { get; set; }

    public Disk(string name, double size)
    {
        Name = name;
        Size = size;
    }

    public abstract void DisplayInfo();
}

public class Directory : Disk
{
    public List<Disk> Contents { get; set; }

    public Directory(string name, double size) : base(name, size)
    {
        Contents = new List<Disk>();
    }

    public override void DisplayInfo()
    {
        Console.WriteLine($"Type: Directory, Name: {Name}, Size: {Size}MB");
    }

    public void AddItem(Disk item)
    {
        Contents.Add(item);
    }

    public int CountAudioFiles()
    {
        int count = 0;
        foreach (Disk item in Contents)
        {
            if (item is Mp3 || item is Archive)
            {
                count++;
            }
            else if (item is Directory)
            {
                count += ((Directory)item).CountAudioFiles();
            }
        }
        return count;
    }
}

public class DocxFile : File
{
    public DocxFile(string name, double size) : base(name, size)
    {
    }

    public override void DisplayInfo()
    {
        Console.WriteLine($"Type: DOCX File, Name: {Name}, Size: {Size}MB");
    }

    public void Run()
    {
        Console.WriteLine($"Opening DOCX file: {Name}");
    }
}

public class PdfFile : File
{
    public PdfFile(string name, double size) : base(name, size)
    {
    }

    public override void DisplayInfo()
    {
        Console.WriteLine($"Type: PDF File, Name: {Name}, Size: {Size}MB");
    }

    public void Run()
    {
        Console.WriteLine($"Opening PDF file: {Name}");
    }
}


public class File : Disk
{
    public File(string name, double size) : base(name, size)
    {
    }

    public override void DisplayInfo()
    {
        Console.WriteLine($"Type: File, Name: {Name}, Size: {Size}MB");
    }
}

public class Archive : Directory
{
    public Archive(string name, double size) : base(name, size)
    {
    }

    public override void DisplayInfo()
    {
        Console.WriteLine($"Type: Archive, Name: {Name}, Size: {Size}MB");
    }
}

public class Mp3 : File
{
    public Mp3(string name, double size) : base(name, size)
    {
    }

    public override void DisplayInfo()
    {
        Console.WriteLine($"Type: MP3, Name: {Name}, Size: {Size}MB");
    }

    public void Run()
    {
        Console.WriteLine($"Playing MP3 file: {Name}");
    }
}

class Program
{
    static void Main(string[] args)
    {
        Directory root = new Directory("Root", 3);
        Directory music = new Directory("Music", 6);
        File textFile = new File("TextFile.txt", 1.5);
        Archive backup = new Archive("Backup", 10);
        Mp3 song1 = new Mp3("Song1.mp3", 5);
        Mp3 song2 = new Mp3("Song2.mp3", 6);
        DocxFile document1 = new DocxFile("Document1.docx", 2);
        PdfFile document2 = new PdfFile("Document2.pdf", 3);

        root.AddItem(textFile);
        root.AddItem(music);
        music.AddItem(song1);
        music.AddItem(song2);
        root.AddItem(backup);
        root.AddItem(document1);
        root.AddItem(document2);

        root.DisplayInfo();
        Console.WriteLine($"Number of audio files: {root.CountAudioFiles()}");

        foreach (var item in music.Contents)
        {
            item.DisplayInfo();
            if (item is Mp3)
            {
                ((Mp3)item).Run();
            }
        }
        foreach (var item in root.Contents)
        {
            item.DisplayInfo();
            if (item is DocxFile)
            {
                ((DocxFile)item).Run();
            }
            else if (item is PdfFile)
            {
                ((PdfFile)item).Run();
            }
        }
    }
}
