using UnityEngine;

[System.Serializable]
public class User
{
    [SerializeField] private int userId;
    [SerializeField] private string email;
    [SerializeField] private string password;
    [SerializeField] private string name;
    [SerializeField] private string profileImg;

    public int UserId
    {
        get
        {
            return userId;
        }
        set
        {
            userId = value;
        }
    }

    public string Email
    {
        get
        {
            return email;
        }
        set
        {
            email = value;
        }
    }

    public string Password
    {
        get
        {
            return password;
        }
        set
        {
            password = value;
        }
    }

    public string Name
    {
        get
        {
            return name;
        }
        set
        {
            name = value;
        }
    }

    public string ProfileImg
    {
        get
        {
            return profileImg;
        }
        set
        {
            profileImg = value;
        }
    }

}
