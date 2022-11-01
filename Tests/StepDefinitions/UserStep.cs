using FluentAssertions;
using FluentAssertions.Execution;
using Newtonsoft.Json;
using System.Text;
using Yordan.GoRestSpecflow.Core.Config;
using Yordan.GoRestSpecflow.Core.ContextContainers;
using Yordan.GoRestSpecflow.Core.Helpers;
using Yordan.GoRestSpecflow.Core.Support.Models;

namespace GoRestSpecflow.StepDefinitions
{
    [Binding]
    public sealed class UserStep
    {
        private TestContextContainer _context;
        private readonly BaseConfig _baseConfig;
        private HttpResponseMessage _response;
        private UserRequest _userRequest;

        public UserStep(BaseConfig baseConfig, TestContextContainer context)
        {
            _baseConfig = baseConfig;
            _context = context;
        }

        //GetAll
        [Given(@"I want to prepare a request")]
        public void GivenIWantToPrepareARequest()
        {
        }

        [When(@"I get all users from the (.*) endpoint")]
        public async Task WhenIGetAllUsersFromTheUsersEndpoint(string endpoint)
        {
            _response = await _context.HttpClient.GetAsync($"{_baseConfig.HttpClientConfig.BaseUrl}{endpoint}");
        }

        [Then(@"The response status code should be (.*)")]
        public void ThenTheResponseStatusCodeShouldBeOK(string statusCode)
        {
            _response.StatusCode.ToString().Should().Be(statusCode);
        }

        [Then(@"The respons should contain a list of users")]
        public async Task ThenTheResponsShouldContainAListOfUsers()
        {
            var content = await _response.Content.ReadAsStringAsync();
            var expectedResponse = JsonConvert.DeserializeObject<List<UserRequest>>(content);

            expectedResponse.Should().NotBeEmpty();
        }

        //Post
        [Given(@"I have the following user data")]
        public void GivenIHaveTheFollowingUserData()
        {
        }

        [When(@"I send a request to the (.*) endpoint")]
        public async Task WhenISendARequestToTheUsersEndpoint(string endpoint)
        {
            _response = await CreateUser(endpoint);
        }

        [Then(@"The response status code shuld be (.*)")]
        public void ThenTheResponseStatusCodeShuldBeCreated(string statusCode)
        {
            _response.StatusCode.ToString().Should().Be(statusCode);
        }

        [Then(@"The user should be created successfully")]
        public async Task ThenTheUserShouldBeCreatedSuccessfully()
        {
            var actualResponse =
                JsonConvert.DeserializeObject<User>(await _response.Content.ReadAsStringAsync());
            using (new AssertionScope())
            {
                actualResponse.Id.Should().NotBe(null);
                actualResponse.Name.Should().Be(_userRequest.Name);
            }
        }

        //PUT
        [Given(@"I have a created user already")]
        public void GivenIHaveACreatedUserAlready()
        {
        }

        [When(@"I send an update request to the (.*) endpoint")]
        public async Task WhenISendAnUpdateRequestToTheUsersEndpoint(string endpoint)
        {
            var response = await CreateUser(endpoint);

            var createdUser = JsonConvert.DeserializeObject<User>(await response.Content.ReadAsStringAsync());
            var body = GenerateBody(createdUser);

            _response = await _context.HttpClient.PutAsync($"{_baseConfig.HttpClientConfig.BaseUrl}{endpoint}/{createdUser.Id}", body);
        }

        [Then(@"The status code from response should be (.*)")]
        public void ThenTheStatusCodeFromResponseShouldBeOK(string statusCode)
        {
            _response.StatusCode.ToString().Should().Be(statusCode);
        }

        [Then(@"The user should be updated successfully")]
        public async Task ThenTheUserShouldBeUpdatedSuccessfully()
        {
            var actualResponse =
                JsonConvert.DeserializeObject<User>(await _response.Content.ReadAsStringAsync());
            using (new AssertionScope())
            {
                actualResponse.Id.Should().NotBe(null);
                actualResponse.Name.Should().Be(_userRequest.Name);
            }
        }

        //Delete
        [Given(@"I have created a user to delete already")]
        public void GivenIHaveCreatedAUserToDeleteAlready()
        {
        }

        [When(@"I send a delete request to the (.*) endpoint")]
        public async Task WhenISendADeleteRequestToTheUsersEndpoint(string endpoint)
        {
            var response = await CreateUser(endpoint);

            var createdUser = JsonConvert.DeserializeObject<User>(await response.Content.ReadAsStringAsync());

            _response = await _context.HttpClient.DeleteAsync($"{_baseConfig.HttpClientConfig.BaseUrl}{endpoint}/{createdUser.Id}");
        }

        [Then(@"The delete request status code should be (.*)")]
        public void ThenTheDeleteRequestStatusCodeShouldBeNoContent(string statusCode)
        {
            _response.StatusCode.ToString().Should().Be(statusCode);
        }

        //Invalid user
        [When(@"I send an invalid request to the (.*) endpoint")]
        public async Task WhenISendAnInvalidRequestToTheUsersEndpoint(string endpoint)
        {
            _response = await CreateInvalidUser(endpoint);
        }

        [Then(@"The invalid user status code should be (.*)")]
        public void ThenTheInvalidUserStatusCodeShouldBeBadRequest(string statusCode)
        {
            _response.StatusCode.ToString().Should().Be(statusCode);
        }

        [Then(@"The property (.*) should be the error field")]
        public async Task ThenThePropertyEmailShouldBeTheErrorField(string field)
        {
            var errorResponse =
               JsonConvert.DeserializeObject<List<ErrorModel>>(await _response.Content.ReadAsStringAsync());

            foreach (var item in errorResponse)
            {
                item.Field.Should().Be(field);
            }
        }

        public async Task<HttpResponseMessage> CreateUser(string endpoint)
        {
            var user = FakerUser.CreateUser();
            _userRequest = user;
            var body = GenerateBody(user);

            var response = await _context.HttpClient.PostAsync($"{_baseConfig.HttpClientConfig.BaseUrl}{endpoint}", body);

            return response;
        }


        public async Task<HttpResponseMessage> CreateInvalidUser(string endpoint)
        {
            var user = FakerUser.CreateInvalidUser();
            var body = GenerateBody(user);

            var response = await _context.HttpClient.PostAsync($"{_baseConfig.HttpClientConfig.BaseUrl}{endpoint}", body);

            return response;
        }

        public StringContent GenerateBody<T>(T user)
        {
            var content = JsonConvert.SerializeObject(user);
            return new StringContent(content, Encoding.UTF8, "application/json");
        }
    }
}