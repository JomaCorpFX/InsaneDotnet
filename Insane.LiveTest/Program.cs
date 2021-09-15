using Insane.Converter;
using Insane.Cryptography;
using Insane.EntityFrameworkCore;
using Insane.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.Serialization;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace Insane.LiveTest
{
    class Program
    {

        public static void RsaManagerTests()
        {
            //string data = "HelloWorld!!!";

            //Console.WriteLine("█ BER");
            //RsaKeyPair keyPair = RsaExtensions.CreateKeyPair(4096, RsaKeyEncoding.Ber);
            //Console.WriteLine("Public:\n" + keyPair.PublicKey);
            //Console.WriteLine("Private:\n" + keyPair.PrivateKey);
            //string encrypted = RsaExtensions.EncryptToBase64(data, keyPair.PublicKey);
            //Console.WriteLine("Encrypted: " + encrypted);
            //Console.WriteLine("Decrypted: " + RsaExtensions.DecryptFromBase64(encrypted, keyPair.PrivateKey));

            //Console.WriteLine("█ XML");
            //keyPair = RsaExtensions.CreateKeyPair(4096, RsaKeyEncoding.Xml, false);
            //Console.WriteLine("Public:\n" + keyPair.PublicKey);
            //Console.WriteLine("Private:\n" + keyPair.PrivateKey);
            //encrypted = RsaExtensions.EncryptToBase64(data, keyPair.PublicKey);
            //Console.WriteLine("Encrypted: " + encrypted);
            //Console.WriteLine("Decrypted: " + RsaExtensions.DecryptFromBase64(encrypted, keyPair.PrivateKey));

            //Console.WriteLine("█ XML INDENTED");
            //keyPair = RsaExtensions.CreateKeyPair(4096, RsaKeyEncoding.Xml);
            //Console.WriteLine("Public:\n" + keyPair.PublicKey);
            //Console.WriteLine("Private:\n" + keyPair.PrivateKey);
            //encrypted = RsaExtensions.EncryptToBase64(data, keyPair.PublicKey);
            //Console.WriteLine("Encrypted: " + encrypted);
            //Console.WriteLine("Decrypted: " + RsaExtensions.DecryptFromBase64(encrypted, keyPair.PrivateKey));

            //Console.WriteLine("█ JSON");
            //keyPair = RsaExtensions.CreateKeyPair(4096, RsaKeyEncoding.Json, false);
            //Console.WriteLine("Public:\n" + keyPair.PublicKey);
            //Console.WriteLine("Private:\n" + keyPair.PrivateKey);
            //encrypted = RsaExtensions.EncryptToBase64(data, keyPair.PublicKey);
            //Console.WriteLine("Encrypted: " + encrypted);
            //Console.WriteLine("Decrypted: " + RsaExtensions.DecryptFromBase64(encrypted, keyPair.PrivateKey));

            //Console.WriteLine("█ JSON INDENTED");
            //keyPair = RsaExtensions.CreateKeyPair(512, RsaKeyEncoding.Json);
            //Console.WriteLine("Public:\n" + keyPair.PublicKey);
            //Console.WriteLine("Private:\n" + keyPair.PrivateKey);
            //encrypted = RsaExtensions.EncryptToBase64(data, keyPair.PublicKey);
            //Console.WriteLine("Encrypted: " + encrypted);
            //Console.WriteLine("Decrypted: " + RsaExtensions.DecryptFromBase64(encrypted, keyPair.PrivateKey));

            //Console.WriteLine("█ PEM");
            //keyPair = RsaExtensions.CreateKeyPair(4096, RsaKeyEncoding.Pem);
            //Console.WriteLine("Public:\n" + keyPair.PublicKey);
            //Console.WriteLine("Private:\n" + keyPair.PrivateKey);
            //encrypted = RsaExtensions.EncryptToBase64(data, keyPair.PublicKey);
            //Console.WriteLine("Encrypted: " + encrypted);
            //Console.WriteLine("Decrypted: " + RsaExtensions.DecryptFromBase64(encrypted, keyPair.PrivateKey));
        }

        static void HashManagerTests()
        {
            //string str = "A";
            //str = HashExtensions.ToBase64(str, 0, false);
            //Console.WriteLine(str);
            //Console.WriteLine(HashExtensions.ToString(HashExtensions.FromBase64(str)));
        }


        public record JwtToken(string Token, string Jti, DateTimeOffset ExpirationTime)
        {
            public string x { get; set; } = "";

        }

        public class Greetings
        {
            public Greetings(string text)
            {
                Text = text;
            }

            public string Text { get; }
        }
        public static string CallMe(IConfiguration configuration, string name, string lastName)
        {
            return $"{configuration.GetValue("Value", ":)")} {name} {lastName}";
        }

        public class PersonProtector : IEntityProtector<Person>
        {
            public PersonProtector(AesStringValueConverter converter)
            {
                Converter = converter;
            }

            public AesStringValueConverter Converter { get; }

            public Person Protect(Person entity)
            {
                entity.Name = Converter.Convert(entity.Name);
                return entity;
            }

            public Person Unprotect(Person entity)
            {
                entity.Name = Converter.Deconvert(entity.Name);
                return entity;
            }
        }

        public class Person:IEntity
        {

            public int Id { get; set; }

            public string Name { get; set; } = null!;
           

            public Person()
            {
                
            }
            

        }

        static void Main(string[] args)
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();

            IConfiguration configuration = new ConfigurationBuilder().
                   SetBasePath(Directory.GetCurrentDirectory())
                   .AddJsonFile("appsettings.json", false, true)
                   .Build();

            //var method = typeof(Program).GetMethod("CallMe");
            //var exCall = Expression.Call(null, method!, Expression.Constant(configuration), Expression.Constant("Joma"), Expression.Constant("Espinoza"));
            //ServiceProvider serviceProvider = new ServiceCollection()
            //    .AddTransient<Greetings>((provider) =>
            //    {
            //        return new Greetings(Expression.Lambda<Func<string>>(exCall).Compile()());
            //    })
            //    .BuildServiceProvider();
            //string methodName = "AddScoped";
            //var methodInfo = typeof(ServiceCollectionServiceExtensions).GetMethods()
            //                            .Where(m => m.Name.Equals(methodName))
            //                            .Select(m => new
            //                            {
            //                                Method = m,
            //                                Args = m.GetGenericArguments(),
            //                                Params = m.GetParameters()
            //                            })
            //                            .Where(x => x.Args.Length == 2 && x.Params.Length == 2 && "TService".Equals(x.Args[0].Name) && "TImplementation".Equals(x.Args[1].Name))
            //                            .First();

            //var methodInfo2 = typeof(ServiceCollectionServiceExtensions).GetMethod(methodName, 0, new Type[] { typeof(IServiceCollection), typeof(Func<, >) });
            //var methods = typeof(ServiceCollectionServiceExtensions).GetMethods().Where(m => m.Name.Equals(methodName));
            //Console.WriteLine(methodInfo.Equals(methodInfo2));
            //Console.WriteLine(serviceProvider.GetService<Greetings>()!.Text);
            //Console.ReadLine();
            //Console.WriteLine(serviceProvider.GetService<Greetings>()!.Text);
            //Console.ReadLine();
            //Console.WriteLine(serviceProvider.GetService<Greetings>()!.Text);
            //Console.ReadLine();
            IEncoder encoder = Base64Encoder.Instance;
            HmacResult result = new HmacResult("MTAw", "MTAw", HashAlgorithm.Sha512, encoder);
            string json = result.Serialize();
            Console.WriteLine(json);
            result = HmacResult.Deserialize(json, encoder)!;
            Console.WriteLine(encoder.Name());
            var person = new Person() { Name = "Joma", Id = 100};
            
            IEntityProtector<Person> protector = new PersonProtector(new AesStringValueConverter(new AesEncryptor("hello123", Base64Encoder.Instance)));
            person.Protect(protector);
            Console.WriteLine(System.Text.Json.JsonSerializer.Serialize(person));
            person.Unprotect(protector);
            Console.WriteLine(System.Text.Json.JsonSerializer.Serialize(person));
            Console.WriteLine("grape".ToHash(encoder));
            

            sw.Stop();
            Console.ReadLine();
        }
    }





}
