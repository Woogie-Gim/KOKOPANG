using UnityEngine;

[System.Serializable]
public class Friendship
{
    [SerializeField] int userId;
    [SerializeField] int friendId;
    [SerializeField] string friendName;
    [SerializeField] int friendRating;
    [SerializeField] string friendProfileImg;
    [SerializeField] bool isWaiting;
    [SerializeField] bool isFrom;

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

    public int FriendId
    {
        get
        {
            return friendId;
        }
        set
        {
            friendId = value;
        }
    }

    public string FriendName
    {
        get
        {
            return friendName;
        }
        set
        {
            friendName = value;
        }
    }

    public int FriendRating
    {
        get
        {
            return friendRating;
        }
        set
        {
            friendRating = value;
        }
    }

    public string FriendProfileImg
    {
        get
        {
            return friendProfileImg;
        }
        set
        {
            friendProfileImg = value;
        }
    }

    public bool IsWaiting
    {
        get
        {
            return isWaiting;
        }
        set
        {
            isWaiting = value;
        }
    }

    public bool IsFrom
    {
        get
        {
            return isFrom;
        }
        set
        {
            isFrom = value;
        }
    }
}
