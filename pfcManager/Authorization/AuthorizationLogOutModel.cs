using System;
using System.Collections.Generic;
using System.Text;

namespace pfcManager.Authorization
{
    internal class AuthorizationLogOutModel : 
        AuthorizationEntryModel
    {
        public AuthorizationLogOutModel() :
            base()
        {
            Login = "";
            Password = "";
            ClearLoginPassword();
        }
    }
}
