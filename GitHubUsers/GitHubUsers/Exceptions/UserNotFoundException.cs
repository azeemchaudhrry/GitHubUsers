using System;

namespace GitHubUsers.Exceptions
{
    public class UserNotFoundException : Exception
    {
        public UserNotFoundException(string message) : base(message)
        {
                
        }
    }
}
