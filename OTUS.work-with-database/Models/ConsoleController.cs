using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using OTUS.work_with_database.Interfaces;
using OTUS.work_with_database.Models.StorageManagers;

namespace OTUS.work_with_database.Models
{
    public class ConsoleController
    {
        private readonly UserStorageManager _users;
        private readonly CategoryStorageManager _categories;
        private readonly AdvertisementStorageManager _advertisements;

        public ConsoleController(IUnitOfWork uow)
        {
            _users = new UserStorageManager(uow);
            _categories = new CategoryStorageManager(uow);
            _advertisements = new AdvertisementStorageManager(uow);
        }
        public async Task RunAsync()
        {
            Console.WriteLine($"Select command. Available commands:{Environment.NewLine}" +
                              $"FillWithInitialValues(), Exit(){Environment.NewLine}" +
                              $"--------------------------------------------------------------------------{Environment.NewLine}" +
                              $"GetAllUsers(), GetUserById(id), CreateUser(name, surname, age, email),{Environment.NewLine}" +
                              $"UpdateUser(id, name, surname, age, email), DeleteUser(id){Environment.NewLine}" +
                              $"--------------------------------------------------------------------------{Environment.NewLine}" +
                              $"GetAllCategories(), GetCategoryById(id), CreateCategory(name, description),{Environment.NewLine}" +
                              $"UpdateCategory(id, name, description), DeleteCategory(id){Environment.NewLine}" +
                              $"--------------------------------------------------------------------------{Environment.NewLine}" +
                              $"GetAllAdvertisements(), GetAdvertisementById(id), CreateAdvertisement(name, description, price, userId, categoryId){Environment.NewLine}" +
                              $"UpdateAdvertisements(id, name, description, price, userId, categoryId), DeleteAdvertisement(id){Environment.NewLine}");
            await ExecuteCommand();
        }

        private async Task ExecuteCommand()
        {
            var command = Console.ReadLine();
            if (String.IsNullOrWhiteSpace(command))
            {
                await Restart("Command can not be empty");
                return;
            }

            var pattern = "^(\\w*)\\(([\\w\\d,@_\\s.]*)\\)$";
            var parsed = Regex.Match(command, pattern);
            var commandName = parsed.Groups[1].Value.Trim();
            var arguments = parsed.Groups[2].Value.Split(", ");

            switch (commandName)
            {
                case "FillWithInitialValues":
                    await _users.FillTableWithInitialValues();
                    await _categories.FillTableWithInitialValues();
                    await _advertisements.FillTableWithInitialValues();
                    Console.WriteLine("Users, Categories and Advertisements filled with initial values");
                    break;
                case "GetAllUsers":
                    Console.WriteLine(String.Join($"{Environment.NewLine}", await _users.Get()));
                    break;
                case "GetUserById":
                    Console.WriteLine(await _users.Get(long.Parse(arguments[0])));
                    break;
                case "CreateUser":
                    var newUser = new User()
                    {
                        Id = 0, Name = arguments[0], Surname = arguments[1], Age = Byte.Parse(arguments[2]),
                        Email = arguments[3]
                    };
                    Console.WriteLine($"Records affected: {await _users.CreateAsync(newUser)}");
                    break;
                case "UpdateUser":
                    var updatedUser = new User()
                    {
                        Id = Int64.Parse(arguments[0]),
                        Name = arguments[1],
                        Surname = arguments[2],
                        Age = Byte.Parse(arguments[3]),
                        Email = arguments[4]
                    };
                    Console.WriteLine($"Records affected: {await _users.UpdateAsync(updatedUser)}{Environment.NewLine}");
                    break;
                case "DeleteUser":
                    Console.WriteLine($"Records affected: {await _users.DeleteAsync(Int64.Parse(arguments[0]))}{Environment.NewLine}");
                    break;
                case "GetAllCategories":
                    Console.WriteLine(String.Join($"{Environment.NewLine}", await _categories.Get()));
                    break;
                case "GetCategoryById":
                    Console.WriteLine($"{await _categories.Get(long.Parse(arguments[0]))}{Environment.NewLine}");
                    break;
                case "CreateCategory":
                    var newCategory = new Category()
                    {
                        Id = 0,
                        Name = arguments[0],
                        Description = arguments[1]
                    };
                    Console.WriteLine($"Records affected: {await _categories.CreateAsync(newCategory)}{Environment.NewLine}");
                    break;
                case "UpdateCategory":
                    var updatedCategory = new Category()
                    {
                        Id = Int64.Parse(arguments[0]),
                        Name = arguments[1],
                        Description = arguments[2]
                    };
                    Console.WriteLine($"Records affected: {await _categories.UpdateAsync(updatedCategory)}{Environment.NewLine}");
                    break;
                case "DeleteCategory":
                    Console.WriteLine($"Records affected: {await _categories.DeleteAsync(Int64.Parse(arguments[0]))}{Environment.NewLine}");
                    break;
                case "GetAllAdvertisements":
                    Console.WriteLine(String.Join($"{Environment.NewLine}", await _advertisements.Get()));
                    break;
                case "GetAdvertisementById":
                    Console.WriteLine($"{await _advertisements.Get(long.Parse(arguments[0]))}{Environment.NewLine}");
                    break;
                case "CreateAdvertisement":
                    var newAdvertisement = new Advertisement()
                    {
                        Id = 0,
                        Name = arguments[0],
                        Description = arguments[1],
                        Price = Double.Parse(arguments[2]),
                        UserId = long.Parse(arguments[3]),
                        CategoryId = long.Parse(arguments[4])
                    };
                    Console.WriteLine($"Records affected: {await _advertisements.CreateAsync(newAdvertisement)}{Environment.NewLine}");
                    break;
                case "UpdateAdvertisement":
                    var updatedAdvertisement = new Advertisement()
                    {
                        Id = Int64.Parse(arguments[0]),
                        Name = arguments[1],
                        Description = arguments[2],
                        Price = Double.Parse(arguments[3]),
                        UserId = long.Parse(arguments[4]),
                        CategoryId = long.Parse(arguments[5])
                    };
                    Console.WriteLine($"Records affected: {await _advertisements.UpdateAsync(updatedAdvertisement)}{Environment.NewLine}");
                    break;
                case "DeleteAdvertisement":
                    Console.WriteLine($"Records affected: {await _advertisements.DeleteAsync(Int64.Parse(arguments[0]))}{Environment.NewLine}");
                    break;
                case "Exit":
                    return;
                case "":
                    await Restart($"Command name can not be empty{Environment.NewLine}");
                    return;
                default:
                    await Restart($"Invalid command name.{Environment.NewLine}");
                    return;
            }

            await ExecuteCommand();
        }

        private async Task Restart(string message)
        {
            Console.WriteLine(message + Environment.NewLine);
            await ExecuteCommand();
        }
    }
}