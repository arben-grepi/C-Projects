using System.Security.Cryptography;

byte[] Iv = RandomNumberGenerator.GetBytes(16);

Console.WriteLine(Convert.ToBase64String(Iv));