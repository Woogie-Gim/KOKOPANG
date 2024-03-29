import React, { useEffect, useRef, useState } from 'react'
import { LoginBox, UserLogin, LoginButton, UserBox, ProfileBox, DownLoadBox, FriendsBox, SignUpBtn } from '../../styles/Main/LoginForm'
import axios from 'axios';
import { useShallow } from "zustand/react/shallow";
import useAuthStore from '../../stores/AuthStore';
import useUserStore from '../../stores/UserStore';
import { useNavigate } from 'react-router-dom';
import subImg from "../../assets/koko_logo1.png"

interface Profile {
  id: null | number;
  userId: null | number;
  saveFolder: null | string;
  originalName: null | string;
  saveName: null | string;
}

const LoginForm = () => {
  const user = useUserStore();
  const [username, setUsername] = useState("");
  const [password, setPassword] = useState("");
  const [deung, setDeung] = useState(true);
  const [refRef, setRefRef] = useState(true)
  const defaultProfile =
    "https://cdn.pixabay.com/photo/2015/10/05/22/37/blank-profile-picture-973460_1280.png";

  const [avatar, setAvatar] = useState(defaultProfile);
  const [oldProfile, setOldProfile] = useState<Profile>();
  const [profileFile, setProfileFile] = useState<null | string>(null);
  const fileInput = useRef<HTMLInputElement>(null);
  const navigate = useNavigate();

  const { isLogin, login, PATH, token, setToken, setTokenExpireTime,logout } =
    useAuthStore(
      useShallow((state) => ({
        isLogin: state.isLogIn,
        login: state.login,
        PATH: state.PATH,
        setToken: state.setToken,
        token: state.token,
        setTokenExpireTime: state.setTokenExpireTime,
        logout: state.logout,
      }))
    );

  const { setUser, name, setProfileImage } =
    useUserStore(
      useShallow((state) => ({
        setUser: state.setUser,
        name: state.name,
        setProfileImage: state.setProfileImage
      }))
    )

  const changeUsername = (event: React.ChangeEvent<HTMLInputElement>) => {
    setUsername(event.target.value);
  }

  const changePassword = (event: React.ChangeEvent<HTMLInputElement>) => {
    setPassword(event.target.value);
  }

  const Login = async (event: React.FormEvent<HTMLFormElement>) => {
    event.preventDefault();

    let data = new FormData();
    data.append("username", username);
    data.append("password", password);

    try {
      // 로그인 요청
      const loginRes = await axios.post(`${PATH}/login`, data);
      const { authorization, refreshtoken } = loginRes.headers;

      // 토큰 저장
      setToken(authorization, refreshtoken);
      setTokenExpireTime(new Date().getTime());
      setPassword("")
      setUsername("")
      // 사용자 프로필 요청
      const profileRes = await axios.get(`${PATH}/user/profile?email=${username}`, {
        headers: {
          Authorization: authorization,
        },
      });

      console.log(profileRes);
      setUser(profileRes.data);
      setProfileImage(profileRes.data.profileImg);
      setAvatar(profileRes.data.profileImg === null ? defaultProfile: profileRes.data.profileImg)
        // 친구 목록 요청
      const friendsRes = await axios.get(`${PATH}/friend/list?userId=${profileRes.data.userId}`, {
        headers: {
          Authorization: authorization,
        },
      });
      const sortedFriendsList = friendsRes.data.sort((a: any, b: any) => b.friendRating - a.friendRating);
      user.setFriendsList(sortedFriendsList.slice(0, 10));

      login();
      setRefRef(!refRef);
    } catch (error: any) {
      alert("아이디 또는 비밀번호를 확인해주세요!")
      console.log(error.response || error);
    }
  };

  const uploadImg = (event: any) => {
    const { files } = event.target;
    const uploadFile = files[0];

    // back으로 axios요청 시 함께 보낼 이미지 파일
    setProfileFile(files[0]);

    const reader = new FileReader();
    if (uploadFile) {
      reader.readAsDataURL(uploadFile);
      reader.onloadend = () => {
        const result = reader.result as string;
        // Avatar에 띄워줄 사진 파일
        // user.setProfileImage(result);
        setAvatar(result);
      };
    }
  };

  const profileUpload = () => {

    if (!profileFile) return;
    // 프로필이미지를 처음 생성한 경우

    else if (user.profileImage === defaultProfile) {
      // formData형식으로 imgInfo에 파일경로 입력
      const formData = new FormData();
      formData.append("imgInfo", profileFile as string);
      // userId를 함께 보내야 함
      const data = {
        userId: user.userId,
      };
      formData.append("profileImg", JSON.stringify(data));
      axios({
        url: `${PATH}/profile/upload`,
        method: "POST",
        data: formData,
        headers: {
          "Content-Type": "multipart/form-data",
          Authorization: token,
        },
      }).then((res) => {
        user.setProfileImage(avatar);
        setOldProfile({
          id: res.data.id,
          userId: res.data.userId,
          saveFolder: res.data.saveFolder,
          originalName: res.data.originalName,
          saveName: res.data.saveName,
        });
        setRefRef(!refRef)
      })
        .catch((error) => console.log(error))
        ;
    }
    // 프로필사진 수정
    else if (avatar !== user.profileImage) {
      const formData = new FormData();
      // 새로 들어온 프로필 사진 파일
      formData.append("imgInfo", profileFile as string);
      // 이전 프로필 사진 정보를 같이 보내야 함
      // -> back에서 이를 삭제하고 새로 추가함
      const data = {
        id: oldProfile?.id,
        userId: oldProfile?.userId,
        saveFolder: oldProfile?.saveFolder,
        originalName: oldProfile?.originalName,
        saveName: oldProfile?.saveName,
      };

      formData.append("profileImg", JSON.stringify(data));
      axios({
        url: `${PATH}/profile/update`,
        method: "PUT",
        data: formData,
        headers: {
          "Content-Type": "multipart/form-data",
          Authorization: token,
        },
      }).then((res) => {
        user.setProfileImage(avatar);
        setOldProfile({
          id: res.data.id,
          userId: res.data.userId,
          saveFolder: res.data.saveFolder,
          originalName: res.data.originalName,
          saveName: res.data.saveName,
        });
        setRefRef(!refRef)
      }).catch((error) => console.log(error));
    }
    setProfileFile(null);
    setDeung(!deung)
  }

  const downloadApp = () => {
    const fileURL = 'https://drive.google.com/uc?export=download&id=1JIh4kVU99Rk6mmYwzXACibjPNEwhJBnJ';
    // 다운로드 링크를 클릭하여 파일을 다운로드합니다.
    window.open(fileURL, '_blank');
  }

  useEffect(() => {
    if (isLogin) {
      axios
        .get(`${PATH}/profile/read`, {
          params: { userId: user.userId },
          headers: {
            Authorization: token,
          },
        })
        .then((response) => {
          console.log(user.userId, user.profileImage)
          if (!response.data) return;
          console.log(response.data,"라라라랄라ㅏ")
          const image = response.data;
          if (image) {
            setAvatar(
              `${PATH}/profile/getImg/${image.saveFolder}/${image.originalName}/${image.saveName}`
            );
            user.setProfileImage(
              `${PATH}/profile/getImg/${image.saveFolder}/${image.originalName}/${image.saveName}`
            );
            setOldProfile({
              id: image.id,
              userId: image.userId,
              saveFolder: image.saveFolder,
              originalName: image.originalName,
              saveName: image.saveName,
            });
          }
        })
        .catch((error) => console.log(error));

      axios.get(`${PATH}/friend/list?userId=${user.userId}`, {
        headers: {
          Authorization: token,
        },
      })
        .then((res) => {
          const sortedFriendsList = res.data.sort((a: any, b: any) => b.friendRating - a.friendRating);
          user.setFriendsList(sortedFriendsList.slice(0, 10));
        })
        ;
    }
  }, [refRef]);

  return (
    <>
      {isLogin ? <LoginBox>
        <UserBox>
          <div className='img_box'>
            <input
              type="file"
              accept="image/jpg,image/jpeg, image/png"
              name="profile_img"
              onChange={uploadImg}
              ref={fileInput}
              style={{ display: "none" }}
            />
            <img src={avatar} alt="프로필 이미지" />
            {!deung ? <div className='btn' onClick={profileUpload}>등록</div> : <div onClick={() => {
              if (fileInput.current) {
                fileInput.current.click();
              }
              setDeung(!deung)
            }} className='btn'>프로필 이미지 변경</div>}
          </div>
          <ProfileBox>
            <div className='name_box'>
              <div>{name} 님</div>
              <div className='logout' onClick={logout}>로그아웃</div>
            </div>
            <div>Rating: {user.rating}</div>
          </ProfileBox>
        </UserBox>
        <DownLoadBox onClick={downloadApp}>DOWNLOAD</DownLoadBox>
        <FriendsBox>
          <div className='container'>
            <div className='item'>등수</div>
            <div className='item'>친구</div>
            <div className='item'>레이팅</div>
          </div>
          <div style={{ padding: "10px" }}>
            {user.friendsList.map((friend, idx) => (
              <div key={idx} style={{ display: "flex", flexDirection: "row", borderBottom: "1px solid gray", marginBottom: "10px", justifyContent: "center" }}>
                <div className='item1'>{idx + 1}</div>
                <div className='item1'><img src={friend.friendProfileImg === null ? defaultProfile : friend.friendProfileImg} alt="친구 프로필 이미지"
                  style={{ width: "25px", height: "25px", borderRadius: "100px" }}
                /></div>
                <div className='item1' style={{ display: "flex", alignItems: "center" }}>
                  {friend.friendName}
                </div>
                <div className='item1'>{friend.friendRating}</div>
              </div>
            ))}
          </div>
        </FriendsBox>
      </LoginBox> : <LoginBox>
        <div style={{ fontSize: "25px", width: "80%", margin: "20px auto", fontWeight: "700" }}>코코팡 로그인</div>
        <form style={{ margin: "0 auto", width: "80%" }} onSubmit={Login}>
          <UserLogin placeholder='Email' value={username} onChange={changeUsername} />
          <UserLogin placeholder='Password' type='password' value={password} onChange={changePassword} />
          <LoginButton type='submit' value='로그인' />
        </form>
        <SignUpBtn onClick={() => navigate("/signup")}>회원가입</SignUpBtn>
        <img src={subImg} alt="심심이미지" style={{ width:"200px", height: "200px", margin: "0 auto", marginTop: "30px"}}/>
      </LoginBox>}
    </>
  )
}

export default LoginForm;