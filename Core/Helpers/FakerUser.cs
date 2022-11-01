using Bogus;
using Yordan.GoRestSpecflow.Core.Support.Enums;
using Yordan.GoRestSpecflow.Core.Support.Models;

namespace Yordan.GoRestSpecflow.Core.Helpers
{
    public static class FakerUser
    {
        public static UserRequest CreateUser()
        {
            var fakerUser = new Faker<UserRequest>()
                .RuleFor(x => x.Gender, f => f.PickRandom<Bogus.DataSets.Name.Gender>().ToString())
                .RuleFor(x => x.FirstName, (f, x) => f.Name.FirstName(Enum.Parse<Bogus.DataSets.Name.Gender>(x.Gender)))
                .RuleFor(x => x.LastName, (f, x) => f.Name.LastName(Enum.Parse<Bogus.DataSets.Name.Gender>(x.Gender)))
                .RuleFor(x => x.Name, f => f.Name.FullName())
                .RuleFor(x => x.Email, (f, x) => f.Internet.Email())
                .RuleFor(x => x.Status, f => f.PickRandom<Status>().ToString());

            return fakerUser.Generate();
        }

        public static UserRequest CreateInvalidUser()
        {
            var fakerUser = new Faker<UserRequest>()
                .RuleFor(x => x.Gender, f => f.PickRandom<Bogus.DataSets.Name.Gender>().ToString())
                .RuleFor(x => x.FirstName, (f, x) => f.Name.FirstName(Enum.Parse<Bogus.DataSets.Name.Gender>(x.Gender)))
                .RuleFor(x => x.LastName, (f, x) => f.Name.LastName(Enum.Parse<Bogus.DataSets.Name.Gender>(x.Gender)))
                .RuleFor(x => x.Name, f => f.Name.FullName())
                .RuleFor(x => x.Status, f => f.PickRandom<Status>().ToString());

            return fakerUser.Generate();
        }
    }
}
