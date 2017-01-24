using System;
using System.Collections.Generic;

public class JsonPacket
{
    enum type
    {
        Login,
        UserInfo
    }

    public class UserInfo
    {
        string UserID;
        string Name;
        string Gold;
        string Level;
        string Single_Score;
        string Multi_Score;
    }

    //C2S

    public class C2S_Login
    {
        string UserId;
    }


    //S2C

    public class S2C_Login
    {
        string UserID;
        string UserName;
        int BestScore;
        int Gold;
    }
}
