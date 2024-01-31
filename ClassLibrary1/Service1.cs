using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.ServiceModel;
using System.Xml.Serialization;

namespace ClassLibrary1
{
    public class Service1 : IService1
    {
        private List<User> users = new List<User>();
        private Aes aes = Aes.Create();
        private string filePath = "users.xml";
        private byte[] key;
        private byte[] iv;

        public List<Movie> ReadMovies()
        {
            List<Movie> movies = new List<Movie>();
            try
            {
                string[] lines = File.ReadAllLines("film.txt");
               
                foreach (string line in lines)
                {
                    if (string.IsNullOrWhiteSpace(line) || line.StartsWith("№"))
                        continue;
                    string[] fields = line.Split(';');

                    Movie movie = new Movie();
                    movie.Number = int.Parse(fields[0]);
                    movie.Title = fields[1];
                    movie.Country = fields[2];
                    movie.Genres = fields[3].Replace(" ", "").Split(',');
                    movie.CoverPath = fields[4];
                    movie.Link = fields[5];
                    movies.Add(movie);
                }
                
            }
            catch(Exception exception)
            {
                Console.WriteLine(exception.Message);
            }
            return movies;
        }

        public Service1()
        {
            Initialize();
        }

        private void Initialize()
        {
            if (File.Exists("key.bin") && File.Exists("iv.bin"))
            {
                aes.Key = File.ReadAllBytes("key.bin");
                aes.IV = File.ReadAllBytes("iv.bin");
            }
            else
            {
                aes.GenerateKey();
                aes.GenerateIV();
                File.WriteAllBytes("key.bin", aes.Key);
                File.WriteAllBytes("iv.bin", aes.IV);
            }
            key = aes.Key;
            iv = aes.IV;
            LoadUsers();
        }

        private void LoadUsers()
        {
            if (File.Exists(filePath))
            {
                var serializer = new XmlSerializer(typeof(List<User>));
                var stream = new FileStream(filePath, FileMode.Open);
                aes.Key = key;
                aes.IV = iv;
                using (var cryptoStream = new CryptoStream(stream, aes.CreateDecryptor(), CryptoStreamMode.Read))
                {
                    users = (List<User>)serializer.Deserialize(cryptoStream);
                }
            }
        }

        private void SaveUsers()
        {
            var serializer = new XmlSerializer(typeof(List<User>));
            var stream = new FileStream(filePath, FileMode.Create);
            aes.Key = key;
            aes.IV = iv;
            using (var cryptoStream = new CryptoStream(stream, aes.CreateEncryptor(), CryptoStreamMode.Write))
            {
                serializer.Serialize(cryptoStream, users);
            }
        }

        public bool Login(string username, string password)
        {
            return users.Any(u => u.Username == username && u.Password == password);
        }

        public string Register(string username, string password)
        {
            if (string.IsNullOrWhiteSpace(username))
            {
                return "Имя пользователя не может быть пустым";
            }
            else if (password.Length < 8)
            {
                return "Пароль должен быть не менее 8 символов";
            }
            else if (users.Any(u => u.Username == username))
            {
                return "Имя пользователя уже существует";
            }

            users.Add(new User { Username = username, Password = password });
            SaveUsers();
            return "Успешно";
        }
    }
}