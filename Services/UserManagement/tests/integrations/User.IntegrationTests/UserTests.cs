
using User.Application.Dto;

namespace User.IntegrationTests
{
    public class UserTests: BaseIntegrationTest
    {

        public UserTests(UserIntegrationTestWebFactory factory)
            :base(factory) 
        {
            
        }

       // [Fact]
        public async Task Register_ShouldAdd_NewUserToDatabase()
        {
            //Arrange
            var userTest = new RegisterDto("EEE", "eee", "string@com.ru", "122112");


            await Assert.ThrowsAsync<ArgumentException>(() => Sender.Send(userTest));
        
        ////Act
        //async Task Action()=> Sender.Send(userTest);

        ////Assert
        //await Assert.ThrowsAsync<ArgumentException>(Action);
    }

        internal record RegisterDto ( 
            string FirstName, string LastName, string Email,string Password);
    }
}
