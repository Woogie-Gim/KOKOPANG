using UnityEngine;

[System.Serializable]
public class User
{
    [SerializeField] private int id;
    [SerializeField] private string email;
    [SerializeField] private string password;
    [SerializeField] private string name;

    public int Id
    {
        get
        {
            return id;
        }
        set
        {
            id = value;
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

}
