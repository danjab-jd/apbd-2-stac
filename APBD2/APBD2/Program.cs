using APBD2.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Threading.Tasks;

namespace APBD2
{
    public class Program
    {
        private static readonly string WorkingDir = Environment.CurrentDirectory;
        private static readonly string ProjectDir = Directory.GetParent(WorkingDir).Parent.Parent.FullName;

        public static async Task Main(string[] args)
        {
            string path = args[0];
            string resultPath = args[1];

            string logsPath = @$"{ProjectDir}\MyLogs\myLogs.txt";

            //var fi = new FileInfo(path);
            FileInfo fi = new(path);

            HashSet<Student> studentSet = new HashSet<Student>(new MyComparer());
            
            await using StreamWriter streamWriter = new StreamWriter(logsPath);

            using (StreamReader stream = new(fi.OpenRead()))
            {
                string line = null;

                while ((line = await stream.ReadLineAsync()) != null)
                {
                    string[] elements = line.Split(',');

                    if (elements.Length != 9)
                    {
                        await AppendLine(streamWriter, $"Niewystarczająca liczba elementów tablicy opisujących studenta!");

                        continue;
                    }
                    
                    if (IsStudentValuesValid(elements))
                    {
                        bool result = studentSet.Add(CreateStudent(elements));

                        if (!result)
                        {
                            await AppendLine(streamWriter, $"Duplikat studenta o indeksie {elements[4]}");
                        }
                    }
                    else
                    {
                        await AppendLine(streamWriter, $"Jedna z wartości opisujących studenta jest pusta!");
                    }
                }
            }

            string json = ParseToJson(studentSet);

            await using StreamWriter resultStreamWriter = new(resultPath);

            await AppendLine(resultStreamWriter, json);

        }

        private static bool IsStudentValuesValid(string[] elements)
        {
            bool isValid = true;

            foreach (string str in elements)
            {
                if (string.IsNullOrWhiteSpace(str))
                {
                    isValid = false;
                }

            }

            return isValid;
        }

        private static Student CreateStudent(string[] elements)
        {
            return new Student
            {
                Name = elements[0],
                Surname = elements[1],
                Study = new()
                {
                    Name = elements[2],
                    Mode = elements[3]
                },
                Index = int.Parse(elements[4]),
                BirthDate = DateTime.Parse(elements[5]),
                Email = elements[6],
                MotherName = elements[7],
                FatherName = elements[8]
            };
        }

        private static string ParseToJson(HashSet<Student> students)
        {
            Result resToParse = new()
            {
                CreatedAt = DateTime.Now,
                Author = "Daniel Jabłoński",
                Students = students
            };

            return JsonSerializer.Serialize(resToParse, new JsonSerializerOptions
            {
                Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });
        }

        private static async Task AppendLine(StreamWriter streamWriter, string line)
        {
            await streamWriter.WriteLineAsync(line);
        }
    }
}
